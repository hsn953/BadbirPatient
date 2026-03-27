using BADBIR.Api.Data;
using BADBIR.Api.Data.Entities;
using BADBIR.Api.Data.Entities.Papp;
using BADBIR.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BADBIR.Api.Controllers;

/// <summary>
/// Administrative endpoints callable by the Clinician System service account.
/// The most important endpoint here is the data-promotion endpoint (ADR-014):
/// it copies all papp holding-table data for a patient into the live tables,
/// replacing the legacy 5-minute SQL Agent job with an immediate API call.
/// </summary>
[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Clinician,Administrator")]
public class AdminController : ControllerBase
{
    private readonly BadbirDbContext _db;

    public AdminController(BadbirDbContext db)
    {
        _db = db;
    }

    // ─────────────────────────────────────────────────────────────────────────
    // POST api/admin/patients/{patientId}/promote
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Promotes all papp holding-table form data for the given patient into the
    /// live BADBIR tables.
    ///
    /// This endpoint is called by the Clinician System once the clinician has:
    ///   1. Taken paper/in-person consent from the patient.
    ///   2. Created the bbPatientCohortHistory record (chid) in their system.
    ///   3. Created the bbPatientCohortTracking record (fupId) in their system.
    ///   4. Confirmed the drug(s) and disease diagnosis.
    ///
    /// The Patient API then:
    ///   a. Copies each papp form row to the corresponding live table using the
    ///      supplied FupId, with qSourceID = 2 (Patient via Portal).
    ///   b. Sets bbPatient.statusid = 1 (Current).
    ///   c. Marks every promoted papp row as DataStatus = 1 (Approved).
    ///   d. Sets BbPappPatientCohortTracking.ImportedFupId = fupId.
    /// </summary>
    [HttpPost("patients/{patientId:int}/promote")]
    public async Task<ActionResult<PromoteResultDto>> PromotePatient(
        int patientId,
        [FromBody] PromoteRequestDto request)
    {
        // ── 1. Validate patient exists ────────────────────────────────────────
        var patient = await _db.BbPatients.FirstOrDefaultAsync(p => p.Patientid == patientId);
        if (patient is null)
            return NotFound(new { error = $"Patient {patientId} not found." });

        // ── 2. Find the papp tracking row (must be DataStatus=0) ──────────────
        var pappTracking = await _db.PappCohortTrackings
            .FirstOrDefaultAsync(t => t.PatientId == patientId && t.DataStatus == 0);

        if (pappTracking is null)
            return NotFound(new { error = $"No pending papp visit found for patient {patientId}." });

        // ── 3. Validate the live FupId exists in bbPatientCohortTracking ──────
        var liveFupExists = await _db.BbPatientCohortTrackings
            .AnyAsync(t => t.FupId == request.FupId);

        if (!liveFupExists)
            return BadRequest(new { error = $"FupId {request.FupId} not found in bbPatientCohortTracking. " +
                                            "Please create it in the Clinician System first." });

        int pappFupId  = pappTracking.PappFupId;
        int fupId      = request.FupId;
        var promoted   = new List<string>();
        const int qSourcePatientPortal = 2;

        await using var tx = await _db.Database.BeginTransactionAsync();
        try
        {
            // ── 4. Promote DLQI ───────────────────────────────────────────────
            var pappDlqi = await _db.PappDlqis
                .FirstOrDefaultAsync(d => d.PappFupId == pappFupId && d.DataStatus == 0);

            if (pappDlqi is not null)
            {
                var liveDlqiExists = await _db.BbPatientDlqis.AnyAsync(d => d.FupId == fupId);
                if (!liveDlqiExists)
                {
                    _db.BbPatientDlqis.Add(new BbPatientDlqi
                    {
                        FupId          = fupId,
                        Diagnosis      = pappDlqi.Diagnosis,
                        ItchsoreScore  = pappDlqi.ItchsoreScore,
                        EmbscScore     = pappDlqi.EmbscScore,
                        ShophgScore    = pappDlqi.ShophgScore,
                        ClothesScore   = pappDlqi.ClothesScore,
                        SocleisScore   = pappDlqi.SocleisScore,
                        SportScore     = pappDlqi.SportScore,
                        WorkstudScore  = pappDlqi.WorkstudScore,
                        WorkstudnoScore = pappDlqi.WorkstudnoScore,
                        PartcrfScore   = pappDlqi.PartcrfScore,
                        SexdifScore    = pappDlqi.SexdifScore,
                        TreatmentScore = pappDlqi.TreatmentScore,
                        TotalScore     = pappDlqi.TotalScore,
                        Datecomp       = pappDlqi.Datecomp,
                        SkipBreakup    = pappDlqi.SkipBreakup,
                        QSourceId      = qSourcePatientPortal,
                        Createdbyid    = request.ClinicianId, Createdbyname = request.ClinicianName,
                        Createddate    = DateTime.UtcNow,
                        Lastupdatedbyid = request.ClinicianId, Lastupdatedbyname = request.ClinicianName,
                        Lastupdateddate = DateTime.UtcNow
                    });
                }
                pappDlqi.DataStatus = 1;
                promoted.Add("DLQI");
            }

            // ── 5. Promote Lifestyle ──────────────────────────────────────────
            var pappLs = await _db.PappLifestyles
                .FirstOrDefaultAsync(l => l.PappFupId == pappFupId && l.DataStatus == 0);

            if (pappLs is not null)
            {
                var liveLsExists = await _db.BbPatientLifestyles.AnyAsync(l => l.FupId == fupId);
                if (!liveLsExists)
                {
                    _db.BbPatientLifestyles.Add(new BbPatientLifestyle
                    {
                        FupId              = fupId,
                        Birthtown          = pappLs.Birthtown,
                        Birthcountry       = pappLs.Birthcountry,
                        Workstatusid       = pappLs.Workstatusid,
                        Occupation         = pappLs.Occupation,
                        Ethnicityid        = pappLs.Ethnicityid,
                        Otherethnicity     = pappLs.Otherethnicity,
                        Outdooroccupation  = pappLs.Outdooroccupation,
                        Livetropical       = pappLs.Livetropical,
                        Eversmoked         = pappLs.Eversmoked,
                        Eversmokednumbercigsperday = pappLs.Eversmokednumbercigsperday,
                        Agestart           = pappLs.Agestart, Agestop = pappLs.Agestop,
                        Currentlysmoke     = pappLs.Currentlysmoke,
                        Currentlysmokenumbercigsperday = pappLs.Currentlysmokenumbercigsperday,
                        Drnkbeeravg        = pappLs.Drnkbeeravg, Drnkwineavg = pappLs.Drnkwineavg,
                        Drnkspiritsavg     = pappLs.Drnkspiritsavg,
                        Drinkalcohol       = pappLs.Drinkalcohol, Drnkunitsavg = pappLs.Drnkunitsavg,
                        Admittedtohospital = pappLs.Admittedtohospital,
                        Newdrugs           = pappLs.Newdrugs, Newclinics = pappLs.Newclinics,
                        Systolic           = pappLs.Systolic, Diastolic = pappLs.Diastolic,
                        Height             = pappLs.Height, Weight = pappLs.Weight, Waist = pappLs.Waist,
                        WeightMissing      = pappLs.WeightMissing, WaistMissing = pappLs.WaistMissing,
                        SmokingMissing     = pappLs.SmokingMissing, DrinkingMissing = pappLs.DrinkingMissing,
                        Createdbyid        = request.ClinicianId, Createdbyname = request.ClinicianName,
                        Createddate        = DateTime.UtcNow,
                        Lastupdatedbyid    = request.ClinicianId, Lastupdatedbyname = request.ClinicianName,
                        Lastupdateddate    = DateTime.UtcNow
                    });
                }
                pappLs.DataStatus = 1;
                promoted.Add("Lifestyle");
            }

            // ── 6. Promote CAGE ───────────────────────────────────────────────
            var pappCage = await _db.PappCages
                .FirstOrDefaultAsync(c => c.PappFupId == pappFupId && c.DataStatus == 0);

            if (pappCage is not null)
            {
                _db.BbPatientCages.Add(new BbPatientCage
                {
                    FupId        = fupId,
                    Cutdown      = pappCage.Cutdown, Annoyed = pappCage.Annoyed,
                    Guilty       = pappCage.Guilty, Earlymorning = pappCage.Earlymorning,
                    Datecomp     = pappCage.Datecomp,
                    QSourceId    = qSourcePatientPortal,
                    Createdbyid  = request.ClinicianId, Createdbyname = request.ClinicianName,
                    Createddate  = DateTime.UtcNow,
                    Lastupdatedbyid = request.ClinicianId, Lastupdatedbyname = request.ClinicianName,
                    Lastupdateddate = DateTime.UtcNow
                });
                pappCage.DataStatus = 1;
                promoted.Add("CAGE");
            }

            // ── 7. Promote PGA Score ──────────────────────────────────────────
            var pappPga = await _db.PappPgaScores
                .FirstOrDefaultAsync(p => p.PappFupId == pappFupId && p.DataStatus == 0);

            if (pappPga is not null)
            {
                _db.BbPatientPasiScores.Add(new BbPatientPasiScores
                {
                    FupId      = fupId,
                    Psglobid   = pappPga.Pgascore,
                    Pasidate   = pappPga.DateScored,
                    PgaSource  = qSourcePatientPortal,
                    Createdbyid = request.ClinicianId, Createdbyname = request.ClinicianName,
                    Createddate = DateTime.UtcNow,
                    Lastupdatedbyid = request.ClinicianId, Lastupdatedbyname = request.ClinicianName,
                    Lastupdateddate = DateTime.UtcNow
                });
                pappPga.DataStatus = 1;
                promoted.Add("PGA");
            }

            // ── 8. Mark papp tracking row as promoted ─────────────────────────
            pappTracking.ImportedFupId = fupId;
            pappTracking.DataStatus    = 1;

            // ── 9. Update patient status to Current (1) ───────────────────────
            patient.Statusid          = 1;
            patient.Lastupdatedbyid   = request.ClinicianId;
            patient.Lastupdatedbyname = request.ClinicianName;
            patient.Lastupdateddate   = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }

        return Ok(new PromoteResultDto
        {
            PatientId      = patientId,
            FupId          = fupId,
            PromotedForms  = promoted,
            PromotedAt     = DateTime.UtcNow
        });
    }

    // ─────────────────────────────────────────────────────────────────────────
    // POST api/admin/patients/{patientId}/reject
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Rejects a patient's pending papp registration — marks all DataStatus = 2 (Rejected)
    /// and sets bbPatient.statusid = 4 (Invalid).
    /// Used when a clinician determines the patient does not meet eligibility criteria.
    /// </summary>
    [HttpPost("patients/{patientId:int}/reject")]
    public async Task<IActionResult> RejectPatient(int patientId, [FromBody] RejectRequestDto request)
    {
        var patient = await _db.BbPatients.FirstOrDefaultAsync(p => p.Patientid == patientId);
        if (patient is null)
            return NotFound(new { error = $"Patient {patientId} not found." });

        var pappTracking = await _db.PappCohortTrackings
            .FirstOrDefaultAsync(t => t.PatientId == patientId && t.DataStatus == 0);

        if (pappTracking is null)
            return NotFound(new { error = $"No pending papp visit found for patient {patientId}." });

        int pappFupId = pappTracking.PappFupId;

        // Mark all papp form rows as Rejected
        await _db.PappDlqis
            .Where(d => d.PappFupId == pappFupId && d.DataStatus == 0)
            .ExecuteUpdateAsync(s => s.SetProperty(d => d.DataStatus, (byte)2));
        await _db.PappLifestyles
            .Where(l => l.PappFupId == pappFupId && l.DataStatus == 0)
            .ExecuteUpdateAsync(s => s.SetProperty(l => l.DataStatus, (byte)2));
        await _db.PappCages
            .Where(c => c.PappFupId == pappFupId && c.DataStatus == 0)
            .ExecuteUpdateAsync(s => s.SetProperty(c => c.DataStatus, (byte)2));
        await _db.PappEuroqols
            .Where(e => e.PappFupId == pappFupId && e.DataStatus == 0)
            .ExecuteUpdateAsync(s => s.SetProperty(e => e.DataStatus, (byte)2));
        await _db.PappHads
            .Where(h => h.PappFupId == pappFupId && h.DataStatus == 0)
            .ExecuteUpdateAsync(s => s.SetProperty(h => h.DataStatus, (byte)2));
        await _db.PappHaqs
            .Where(h => h.PappFupId == pappFupId && h.DataStatus == 0)
            .ExecuteUpdateAsync(s => s.SetProperty(h => h.DataStatus, (byte)2));
        await _db.PappPgaScores
            .Where(p => p.PappFupId == pappFupId && p.DataStatus == 0)
            .ExecuteUpdateAsync(s => s.SetProperty(p => p.DataStatus, (byte)2));

        pappTracking.DataStatus    = 2;
        pappTracking.Comments      = request.Reason;

        patient.Statusid           = 4; // Invalid
        patient.Lastupdatedbyid    = request.ClinicianId;
        patient.Lastupdatedbyname  = request.ClinicianName;
        patient.Lastupdateddate    = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return Ok(new { patientId, rejectedAt = DateTime.UtcNow, reason = request.Reason });
    }
}
