using BADBIR.Api.Data;
using BADBIR.Api.Data.Entities;
using BADBIR.Api.Data.Entities.Papp;
using BADBIR.Api.Services;
using BADBIR.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BADBIR.Api.Controllers;

/// <summary>
/// Handles patient-submitted PRO form saves into the papp holding tables.
/// A patient may only submit/view their own forms.
/// All data written here stays in the papp tables until the clinician calls
/// POST /api/admin/patients/{patientId}/promote to copy it to the live tables.
/// </summary>
[ApiController]
[Route("api/forms")]
[Authorize]
public class FormsController : ControllerBase
{
    private readonly BadbirDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEncryptionService _enc;

    public FormsController(BadbirDbContext db, UserManager<ApplicationUser> userManager, IEncryptionService enc)
    {
        _db = db;
        _userManager = userManager;
        _enc = enc;
    }

    // ─────────────────────────────────────────────────────────────────────────
    // DLQI
    // ─────────────────────────────────────────────────────────────────────────

    // GET api/forms/dlqi
    /// <summary>Returns the DLQI holding record for the caller's current papp visit.</summary>
    [HttpGet("dlqi")]
    public async Task<ActionResult<PappDlqiDto>> GetDlqi()
    {
        var pappFupId = await GetCurrentPappFupIdAsync();
        if (pappFupId is null) return Forbid();

        var dlqi = await _db.PappDlqis
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.PappFupId == pappFupId && d.DataStatus == 0);

        return dlqi is null ? NotFound() : Ok(MapDlqi(dlqi));
    }

    // POST api/forms/dlqi
    /// <summary>Saves or updates the DLQI form in the papp holding table.</summary>
    [HttpPost("dlqi")]
    public async Task<ActionResult<PappDlqiDto>> SaveDlqi(PappDlqiSubmitDto dto)
    {
        var pappFupId = await GetCurrentPappFupIdAsync();
        if (pappFupId is null) return Forbid();

        var existing = await _db.PappDlqis
            .FirstOrDefaultAsync(d => d.PappFupId == pappFupId && d.DataStatus == 0);

        if (existing is null)
        {
            var entity = MapFromDlqiDto(dto, pappFupId.Value);
            _db.PappDlqis.Add(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDlqi), MapDlqi(entity));
        }

        UpdateDlqiFromDto(existing, dto);
        await _db.SaveChangesAsync();
        return Ok(MapDlqi(existing));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // LIFESTYLE
    // ─────────────────────────────────────────────────────────────────────────

    // GET api/forms/lifestyle
    [HttpGet("lifestyle")]
    public async Task<ActionResult<PappLifestyleDto>> GetLifestyle()
    {
        var pappFupId = await GetCurrentPappFupIdAsync();
        if (pappFupId is null) return Forbid();

        var ls = await _db.PappLifestyles
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.PappFupId == pappFupId && l.DataStatus == 0);

        if (ls is null) return NotFound();
        _enc.DecryptPappLifestyle(ls);
        return Ok(MapLifestyle(ls));
    }

    // POST api/forms/lifestyle
    [HttpPost("lifestyle")]
    public async Task<ActionResult<PappLifestyleDto>> SaveLifestyle(PappLifestyleSubmitDto dto)
    {
        var pappFupId = await GetCurrentPappFupIdAsync();
        if (pappFupId is null) return Forbid();

        var existing = await _db.PappLifestyles
            .FirstOrDefaultAsync(l => l.PappFupId == pappFupId && l.DataStatus == 0);

        if (existing is null)
        {
            var entity = MapFromLifestyleDto(dto, pappFupId.Value);
            _enc.EncryptPappLifestyle(entity);
            _db.PappLifestyles.Add(entity);
            await _db.SaveChangesAsync();
            _enc.DecryptPappLifestyle(entity);
            return CreatedAtAction(nameof(GetLifestyle), MapLifestyle(entity));
        }

        UpdateLifestyleFromDto(existing, dto);
        _enc.EncryptPappLifestyle(existing);
        await _db.SaveChangesAsync();
        _enc.DecryptPappLifestyle(existing);
        return Ok(MapLifestyle(existing));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // CAGE
    // ─────────────────────────────────────────────────────────────────────────

    // GET api/forms/cage
    [HttpGet("cage")]
    public async Task<ActionResult<PappCageDto>> GetCage()
    {
        var pappFupId = await GetCurrentPappFupIdAsync();
        if (pappFupId is null) return Forbid();

        var cage = await _db.PappCages
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.PappFupId == pappFupId && c.DataStatus == 0);

        return cage is null ? NotFound() : Ok(MapCage(cage));
    }

    // POST api/forms/cage
    [HttpPost("cage")]
    public async Task<ActionResult<PappCageDto>> SaveCage(PappCageSubmitDto dto)
    {
        var pappFupId = await GetCurrentPappFupIdAsync();
        if (pappFupId is null) return Forbid();

        var existing = await _db.PappCages
            .FirstOrDefaultAsync(c => c.PappFupId == pappFupId && c.DataStatus == 0);

        if (existing is null)
        {
            var entity = new BbPappPatientCage
            {
                PappFupId    = pappFupId.Value,
                Cutdown      = dto.SkipForm ? null : dto.Cutdown,
                Annoyed      = dto.SkipForm ? null : dto.Annoyed,
                Guilty       = dto.SkipForm ? null : dto.Guilty,
                Earlymorning = dto.SkipForm ? null : dto.Earlymorning,
                Datecomp     = DateTime.UtcNow,
                DataStatus   = 0,
                Createdbyid  = 0, Createdbyname = "PatientPortal", Createddate = DateTime.UtcNow,
                Lastupdatedbyid = 0, Lastupdatedbyname = "PatientPortal", Lastupdateddate = DateTime.UtcNow
            };
            _db.PappCages.Add(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCage), MapCage(entity));
        }

        existing.Cutdown = dto.SkipForm ? null : dto.Cutdown;
        existing.Annoyed = dto.SkipForm ? null : dto.Annoyed;
        existing.Guilty  = dto.SkipForm ? null : dto.Guilty;
        existing.Earlymorning = dto.SkipForm ? null : dto.Earlymorning;
        existing.Lastupdateddate = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(MapCage(existing));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // EuroQol
    // ─────────────────────────────────────────────────────────────────────────

    // GET api/forms/euroqol
    [HttpGet("euroqol")]
    public async Task<ActionResult<PappEuroqolDto>> GetEuroqol()
    {
        var pappFupId = await GetCurrentPappFupIdAsync();
        if (pappFupId is null) return Forbid();

        var eq = await _db.PappEuroqols
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.PappFupId == pappFupId && e.DataStatus == 0);

        return eq is null ? NotFound() : Ok(MapEuroqol(eq));
    }

    // POST api/forms/euroqol
    [HttpPost("euroqol")]
    public async Task<ActionResult<PappEuroqolDto>> SaveEuroqol(PappEuroqolSubmitDto dto)
    {
        var pappFupId = await GetCurrentPappFupIdAsync();
        if (pappFupId is null) return Forbid();

        var existing = await _db.PappEuroqols
            .FirstOrDefaultAsync(e => e.PappFupId == pappFupId && e.DataStatus == 0);

        if (existing is null)
        {
            var entity = new BbPappPatientEuroqol
            {
                PappFupId    = pappFupId.Value,
                Mobility     = dto.SkipForm ? null : dto.Mobility,
                Selfcare     = dto.SkipForm ? null : dto.Selfcare,
                Usualacts    = dto.SkipForm ? null : dto.Usualacts,
                Paindisc     = dto.SkipForm ? null : dto.Paindisc,
                Anxdepr      = dto.SkipForm ? null : dto.Anxdepr,
                Comphealth   = dto.SkipForm ? null : dto.Comphealth,
                Howyoufeel   = dto.SkipForm ? null : dto.Howyoufeel,
                DateCompleted = DateTime.UtcNow,
                DataStatus   = 0,
                Createdbyid  = 0, Createdbyname = "PatientPortal", Createddate = DateTime.UtcNow,
                Lastupdatedbyid = 0, Lastupdatedbyname = "PatientPortal", Lastupdateddate = DateTime.UtcNow
            };
            _db.PappEuroqols.Add(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEuroqol), MapEuroqol(entity));
        }

        existing.Mobility   = dto.SkipForm ? null : dto.Mobility;
        existing.Selfcare   = dto.SkipForm ? null : dto.Selfcare;
        existing.Usualacts  = dto.SkipForm ? null : dto.Usualacts;
        existing.Paindisc   = dto.SkipForm ? null : dto.Paindisc;
        existing.Anxdepr    = dto.SkipForm ? null : dto.Anxdepr;
        existing.Comphealth = dto.SkipForm ? null : dto.Comphealth;
        existing.Howyoufeel = dto.SkipForm ? null : dto.Howyoufeel;
        existing.Lastupdateddate = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(MapEuroqol(existing));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // HADS
    // ─────────────────────────────────────────────────────────────────────────

    // GET api/forms/hads
    [HttpGet("hads")]
    public async Task<ActionResult<PappHadDto>> GetHads()
    {
        var pappFupId = await GetCurrentPappFupIdAsync();
        if (pappFupId is null) return Forbid();

        var had = await _db.PappHads
            .AsNoTracking()
            .FirstOrDefaultAsync(h => h.PappFupId == pappFupId && h.DataStatus == 0);

        return had is null ? NotFound() : Ok(MapHad(had));
    }

    // POST api/forms/hads
    [HttpPost("hads")]
    public async Task<ActionResult<PappHadDto>> SaveHads(PappHadSubmitDto dto)
    {
        var pappFupId = await GetCurrentPappFupIdAsync();
        if (pappFupId is null) return Forbid();

        // When SkipForm=true, nullify all items and mark as non-countable
        int? anxietyScore    = dto.SkipForm ? null : (dto.Q01tense ?? 0) + (dto.Q03frightened ?? 0) + (dto.Q05worry ?? 0) +
                              (dto.Q07relaxed ?? 0) + (dto.Q09butterflies ?? 0) + (dto.Q11restless ?? 0) + (dto.Q13panic ?? 0);
        int? depressionScore = dto.SkipForm ? null : (dto.Q02enjoy ?? 0) + (dto.Q04laugh ?? 0) + (dto.Q06cheerful ?? 0) +
                              (dto.Q08slowed ?? 0) + (dto.Q10appearence ?? 0) + (dto.Q12enjoyment ?? 0) + (dto.Q14goodbook ?? 0);

        bool allAnswered = !dto.SkipForm && new[] { dto.Q01tense, dto.Q02enjoy, dto.Q03frightened, dto.Q04laugh,
                                   dto.Q05worry, dto.Q06cheerful, dto.Q07relaxed, dto.Q08slowed,
                                   dto.Q09butterflies, dto.Q10appearence, dto.Q11restless, dto.Q12enjoyment,
                                   dto.Q13panic, dto.Q14goodbook }
                           .All(q => q.HasValue);

        var existing = await _db.PappHads
            .FirstOrDefaultAsync(h => h.PappFupId == pappFupId && h.DataStatus == 0);

        if (existing is null)
        {
            var entity = new BbPappPatientHad
            {
                PappFupId        = pappFupId.Value,
                Q01tense         = dto.SkipForm ? null : dto.Q01tense,   Q02enjoy      = dto.SkipForm ? null : dto.Q02enjoy,
                Q03frightened    = dto.SkipForm ? null : dto.Q03frightened, Q04laugh    = dto.SkipForm ? null : dto.Q04laugh,
                Q05worry         = dto.SkipForm ? null : dto.Q05worry,   Q06cheerful   = dto.SkipForm ? null : dto.Q06cheerful,
                Q07relaxed       = dto.SkipForm ? null : dto.Q07relaxed, Q08slowed     = dto.SkipForm ? null : dto.Q08slowed,
                Q09butterflies   = dto.SkipForm ? null : dto.Q09butterflies, Q10appearence = dto.SkipForm ? null : dto.Q10appearence,
                Q11restless      = dto.SkipForm ? null : dto.Q11restless, Q12enjoyment  = dto.SkipForm ? null : dto.Q12enjoyment,
                Q13panic         = dto.SkipForm ? null : dto.Q13panic,   Q14goodbook   = dto.SkipForm ? null : dto.Q14goodbook,
                ScoreAnxiety     = anxietyScore,
                ResultAnxiety    = anxietyScore.HasValue ? HadsResult(anxietyScore.Value) : null,
                ScoreDepression  = depressionScore,
                ResultDepression = depressionScore.HasValue ? HadsResult(depressionScore.Value) : null,
                DateScored       = DateTime.UtcNow,
                IsCountable      = allAnswered,
                DataStatus       = 0,
                Createdbyid      = 0, Createdbyname = "PatientPortal", Createddate = DateTime.UtcNow,
                Lastupdatedbyid  = 0, Lastupdatedbyname = "PatientPortal", Lastupdateddate = DateTime.UtcNow
            };
            _db.PappHads.Add(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHads), MapHad(entity));
        }

        existing.Q01tense = dto.SkipForm ? null : dto.Q01tense; existing.Q02enjoy = dto.SkipForm ? null : dto.Q02enjoy;
        existing.Q03frightened = dto.SkipForm ? null : dto.Q03frightened; existing.Q04laugh = dto.SkipForm ? null : dto.Q04laugh;
        existing.Q05worry = dto.SkipForm ? null : dto.Q05worry; existing.Q06cheerful = dto.SkipForm ? null : dto.Q06cheerful;
        existing.Q07relaxed = dto.SkipForm ? null : dto.Q07relaxed; existing.Q08slowed = dto.SkipForm ? null : dto.Q08slowed;
        existing.Q09butterflies = dto.SkipForm ? null : dto.Q09butterflies; existing.Q10appearence = dto.SkipForm ? null : dto.Q10appearence;
        existing.Q11restless = dto.SkipForm ? null : dto.Q11restless; existing.Q12enjoyment = dto.SkipForm ? null : dto.Q12enjoyment;
        existing.Q13panic = dto.SkipForm ? null : dto.Q13panic; existing.Q14goodbook = dto.SkipForm ? null : dto.Q14goodbook;
        existing.ScoreAnxiety = anxietyScore;
        existing.ResultAnxiety = anxietyScore.HasValue ? HadsResult(anxietyScore.Value) : null;
        existing.ScoreDepression = depressionScore;
        existing.ResultDepression = depressionScore.HasValue ? HadsResult(depressionScore.Value) : null;
        existing.IsCountable = allAnswered;
        existing.Lastupdateddate = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(MapHad(existing));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // HAQ
    // ─────────────────────────────────────────────────────────────────────────

    // GET api/forms/haq
    [HttpGet("haq")]
    public async Task<ActionResult<PappHaqDto>> GetHaq()
    {
        var pappFupId = await GetCurrentPappFupIdAsync();
        if (pappFupId is null) return Forbid();

        var haq = await _db.PappHaqs
            .AsNoTracking()
            .FirstOrDefaultAsync(h => h.PappFupId == pappFupId && h.DataStatus == 0);

        return haq is null ? NotFound() : Ok(MapHaq(haq));
    }

    // POST api/forms/haq
    [HttpPost("haq")]
    public async Task<ActionResult<PappHaqDto>> SaveHaq(PappHaqSubmitDto dto)
    {
        var pappFupId = await GetCurrentPappFupIdAsync();
        if (pappFupId is null) return Forbid();

        // Category scores = max of items in each category, raised by 1 if any aids used.
        // HAQ-DI = mean of 8 category scores (rounded to 3 d.p.)
        var catScores = new[]
        {
            CategoryScore(dto.Dressself, dto.Shampoo,   dto.Dressing.HasValue && dto.Dressing > 0),
            CategoryScore(dto.Standchair, dto.Bed,      false),
            CategoryScore(dto.Cutmeat, dto.Liftglass, dto.Openmilk, dto.Specialutensils.HasValue && dto.Specialutensils > 0),
            CategoryScore(dto.Walkflat, dto.Climbsteps, (dto.Cane.HasValue && dto.Cane > 0) || (dto.Crutches.HasValue && dto.Crutches > 0) || (dto.Walker.HasValue && dto.Walker > 0) || (dto.Wheelchair.HasValue && dto.Wheelchair > 0)),
            CategoryScore(dto.Washdry, dto.Bath, dto.Toilet, (dto.Loolift.HasValue && dto.Loolift > 0) || (dto.Bathseat.HasValue && dto.Bathseat > 0) || (dto.Bathrail.HasValue && dto.Bathrail > 0)),
            CategoryScore(dto.Reachabove, dto.Bend, (dto.Longreach.HasValue && dto.Longreach > 0)),
            CategoryScore(dto.Cardoor, dto.Openjar, dto.Turntap, (dto.Jaropener.HasValue && dto.Jaropener > 0)),
            CategoryScore(dto.Shop, dto.Getincar, dto.Housework, false)
        };
        var haqDi = Math.Round(catScores.Average(), 3);

        var existing = await _db.PappHaqs
            .FirstOrDefaultAsync(h => h.PappFupId == pappFupId && h.DataStatus == 0);

        if (existing is null)
        {
            var entity = BuildHaqEntity(dto, pappFupId.Value, catScores, haqDi);
            _db.PappHaqs.Add(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHaq), MapHaq(entity));
        }

        UpdateHaqFromDto(existing, dto, catScores, haqDi);
        await _db.SaveChangesAsync();
        return Ok(MapHaq(existing));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // PGA Score
    // ─────────────────────────────────────────────────────────────────────────

    // GET api/forms/pga
    [HttpGet("pga")]
    public async Task<ActionResult<PappPgaDto>> GetPga()
    {
        var pappFupId = await GetCurrentPappFupIdAsync();
        if (pappFupId is null) return Forbid();

        var pga = await _db.PappPgaScores
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PappFupId == pappFupId && p.DataStatus == 0);

        return pga is null ? NotFound() : Ok(new PappPgaDto { PappFupId = pga.PappFupId, Pgascore = pga.Pgascore, DateScored = pga.DateScored });
    }

    // POST api/forms/pga
    [HttpPost("pga")]
    public async Task<ActionResult<PappPgaDto>> SavePga(PappPgaSubmitDto dto)
    {
        var pappFupId = await GetCurrentPappFupIdAsync();
        if (pappFupId is null) return Forbid();

        if (!dto.SkipForm && dto.Pgascore is not null && (dto.Pgascore < 1 || dto.Pgascore > 5))
            return BadRequest("PGA score must be between 1 (Clear) and 5 (Severe).");

        var pgascore = dto.SkipForm ? null : dto.Pgascore;

        var existing = await _db.PappPgaScores
            .FirstOrDefaultAsync(p => p.PappFupId == pappFupId && p.DataStatus == 0);

        if (existing is null)
        {
            var entity = new BbPappPatientPgaScore
            {
                PappFupId   = pappFupId.Value,
                Pgascore    = pgascore,
                DateScored  = DateTime.UtcNow,
                DataStatus  = 0,
                Createdbyid = 0, Createdbyname = "PatientPortal", Createddate = DateTime.UtcNow,
                Lastupdatedbyid = 0, Lastupdatedbyname = "PatientPortal", Lastupdateddate = DateTime.UtcNow
            };
            _db.PappPgaScores.Add(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPga), new PappPgaDto { PappFupId = entity.PappFupId, Pgascore = entity.Pgascore, DateScored = entity.DateScored });
        }

        existing.Pgascore = pgascore;
        existing.Lastupdateddate = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(new PappPgaDto { PappFupId = existing.PappFupId, Pgascore = existing.Pgascore, DateScored = existing.DateScored });
    }

    // ─────────────────────────────────────────────────────────────────────────
    // SAPASI
    // ─────────────────────────────────────────────────────────────────────────

    // GET api/forms/sapasi
    /// <summary>Returns the SAPASI holding record for the caller's current papp visit.</summary>
    [HttpGet("sapasi")]
    public async Task<ActionResult<PappSapasiDto>> GetSapasi()
    {
        var pappFupId = await GetCurrentPappFupIdAsync();
        if (pappFupId is null) return Forbid();

        var sapasi = await _db.PappSapasis
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.PappFupId == pappFupId && s.DataStatus == 0);

        return sapasi is null ? NotFound() : Ok(MapSapasi(sapasi));
    }

    // POST api/forms/sapasi
    /// <summary>
    /// Saves or updates the SAPASI form in the papp holding table.
    ///
    /// The server calculates the SAPASI total score from the four region scores:
    ///   RegionScore = (Erythema + Induration + Desquamation) × Coverage × RegionWeight
    ///   SAPASI Total = Head + Trunk + UpperLimbs + LowerLimbs
    ///   Weights: Head=0.1, Trunk=0.3, UpperLimbs=0.2, LowerLimbs=0.4
    ///   Max score ≈ 48 (using coverage bands 0–4 and severity items 0–4).
    ///
    /// Patients may leave any region fields null (paper-based blank equivalent).
    /// Setting <c>SkipForm = true</c> records an explicit patient skip and stores
    /// all fields as null with a zero score.
    /// </summary>
    [HttpPost("sapasi")]
    public async Task<ActionResult<PappSapasiDto>> SaveSapasi(PappSapasiSubmitDto dto)
    {
        var pappFupId = await GetCurrentPappFupIdAsync();
        if (pappFupId is null) return Forbid();

        var score = dto.SkipForm ? 0f : ComputeSapasiScore(dto);

        var existing = await _db.PappSapasis
            .FirstOrDefaultAsync(s => s.PappFupId == pappFupId && s.DataStatus == 0);

        if (existing is null)
        {
            var entity = BuildSapasiEntity(dto, pappFupId.Value, score);
            _db.PappSapasis.Add(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSapasi), MapSapasi(entity));
        }

        UpdateSapasiFromDto(existing, dto, score);
        await _db.SaveChangesAsync();
        return Ok(MapSapasi(existing));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // Private helpers
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Gets the PappFupId of the caller's current (DataStatus=0) papp visit.
    /// Returns null if the user is not linked to a patient or has no papp visit.
    /// </summary>
    private async Task<int?> GetCurrentPappFupIdAsync()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return null;

        var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
        if (user?.PatientId is null) return null;

        var pappFup = await _db.PappCohortTrackings
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.PatientId == user.PatientId && t.DataStatus == 0);

        return pappFup?.PappFupId;
    }

    private static int HadsResult(int score) => score switch
    {
        <= 7  => 0, // Normal
        <= 10 => 1, // Borderline
        _     => 2  // Abnormal
    };

    private static double CategoryScore(params (int? value, bool aidUsed)[] items) => throw new NotSupportedException("use overloads below");

    private static double CategoryScore(int? a, int? b, bool aid = false)
    {
        var max = Math.Max(a ?? 0, b ?? 0);
        if (aid && max < 2) max = 2;
        return max;
    }

    private static double CategoryScore(int? a, int? b, int? c, bool aid = false)
    {
        var max = Math.Max(Math.Max(a ?? 0, b ?? 0), c ?? 0);
        if (aid && max < 2) max = 2;
        return max;
    }

    private static BbPappPatientHaq BuildHaqEntity(PappHaqSubmitDto dto, int pappFupId, double[] cats, double haqDi) => new()
    {
        PappFupId = pappFupId,
        Missingdata = dto.Missingdata, Missingdatadetails = dto.Missingdatadetails,
        Dressself = dto.Dressself, Shampoo = dto.Shampoo,
        Standchair = dto.Standchair, Bed = dto.Bed,
        Cutmeat = dto.Cutmeat, Liftglass = dto.Liftglass, Openmilk = dto.Openmilk,
        Walkflat = dto.Walkflat, Climbsteps = dto.Climbsteps,
        Washdry = dto.Washdry, Bath = dto.Bath, Toilet = dto.Toilet,
        Reachabove = dto.Reachabove, Bend = dto.Bend,
        Cardoor = dto.Cardoor, Openjar = dto.Openjar, Turntap = dto.Turntap,
        Shop = dto.Shop, Getincar = dto.Getincar, Housework = dto.Housework,
        Cane = dto.Cane, Crutches = dto.Crutches, Walker = dto.Walker, Wheelchair = dto.Wheelchair,
        Specialutensils = dto.Specialutensils, Specialchair = dto.Specialchair,
        Dressing = dto.Dressing, Dressingdetails = dto.Dressingdetails,
        Loolift = dto.Loolift, Bathseat = dto.Bathseat, Bathrail = dto.Bathrail,
        Longreach = dto.Longreach, Jaropener = dto.Jaropener, Deviceother = dto.Deviceother,
        Dressgroom = (int)cats[0], Rising = (int)cats[1], Eating = (int)cats[2], Walking = (int)cats[3],
        Hygiene = (int)cats[4], Reach = (int)cats[5], Gripping = (int)cats[6], Errands = (int)cats[7],
        Totalscore = (int)cats.Sum(), Haqscore = haqDi,
        DateScored = DateTime.UtcNow, DataStatus = 0,
        Createdbyid = 0, Createdbyname = "PatientPortal", Createddate = DateTime.UtcNow,
        Lastupdatedbyid = 0, Lastupdatedbyname = "PatientPortal", Lastupdateddate = DateTime.UtcNow
    };

    private static void UpdateHaqFromDto(BbPappPatientHaq existing, PappHaqSubmitDto dto, double[] cats, double haqDi)
    {
        existing.Missingdata = dto.Missingdata; existing.Missingdatadetails = dto.Missingdatadetails;
        existing.Dressself = dto.Dressself; existing.Shampoo = dto.Shampoo;
        existing.Standchair = dto.Standchair; existing.Bed = dto.Bed;
        existing.Cutmeat = dto.Cutmeat; existing.Liftglass = dto.Liftglass; existing.Openmilk = dto.Openmilk;
        existing.Walkflat = dto.Walkflat; existing.Climbsteps = dto.Climbsteps;
        existing.Washdry = dto.Washdry; existing.Bath = dto.Bath; existing.Toilet = dto.Toilet;
        existing.Reachabove = dto.Reachabove; existing.Bend = dto.Bend;
        existing.Cardoor = dto.Cardoor; existing.Openjar = dto.Openjar; existing.Turntap = dto.Turntap;
        existing.Shop = dto.Shop; existing.Getincar = dto.Getincar; existing.Housework = dto.Housework;
        existing.Dressgroom = (int)cats[0]; existing.Rising = (int)cats[1]; existing.Eating = (int)cats[2];
        existing.Walking = (int)cats[3]; existing.Hygiene = (int)cats[4]; existing.Reach = (int)cats[5];
        existing.Gripping = (int)cats[6]; existing.Errands = (int)cats[7];
        existing.Totalscore = (int)cats.Sum(); existing.Haqscore = haqDi;
        existing.Lastupdateddate = DateTime.UtcNow;
    }

    private static BbPappPatientDlqi MapFromDlqiDto(PappDlqiSubmitDto dto, int pappFupId) => new()
    {
        PappFupId = pappFupId,
        Diagnosis = dto.Diagnosis,
        ItchsoreScore = dto.ItchsoreScore, EmbscScore = dto.EmbscScore, ShophgScore = dto.ShophgScore,
        ClothesScore = dto.ClothesScore, SocleisScore = dto.SocleisScore, SportScore = dto.SportScore,
        WorkstudScore = dto.WorkstudScore, WorkstudnoScore = dto.WorkstudnoScore,
        PartcrfScore = dto.PartcrfScore, SexdifScore = dto.SexdifScore, TreatmentScore = dto.TreatmentScore,
        TotalScore = ComputeDlqiTotal(dto),
        Datecomp = DateTime.UtcNow, SkipBreakup = dto.SkipBreakup ? 1 : 0,
        DataStatus = 0,
        Createdbyid = 0, Createdbyname = "PatientPortal", Createddate = DateTime.UtcNow,
        Lastupdatedbyid = 0, Lastupdatedbyname = "PatientPortal", Lastupdateddate = DateTime.UtcNow
    };

    private static void UpdateDlqiFromDto(BbPappPatientDlqi existing, PappDlqiSubmitDto dto)
    {
        existing.Diagnosis = dto.Diagnosis;
        existing.ItchsoreScore = dto.ItchsoreScore; existing.EmbscScore = dto.EmbscScore;
        existing.ShophgScore = dto.ShophgScore; existing.ClothesScore = dto.ClothesScore;
        existing.SocleisScore = dto.SocleisScore; existing.SportScore = dto.SportScore;
        existing.WorkstudScore = dto.WorkstudScore; existing.WorkstudnoScore = dto.WorkstudnoScore;
        existing.PartcrfScore = dto.PartcrfScore; existing.SexdifScore = dto.SexdifScore;
        existing.TreatmentScore = dto.TreatmentScore;
        existing.TotalScore = ComputeDlqiTotal(dto);
        existing.SkipBreakup = dto.SkipBreakup ? 1 : 0;
        existing.Lastupdateddate = DateTime.UtcNow;
    }

    private static BbPappPatientLifestyle MapFromLifestyleDto(PappLifestyleSubmitDto dto, int pappFupId) => new()
    {
        PappFupId = pappFupId,
        Birthtown = dto.Birthtown, Birthcountry = dto.Birthcountry,
        Workstatusid = dto.Workstatusid, Occupation = dto.Occupation,
        Ethnicityid = dto.Ethnicityid, Otherethnicity = dto.Otherethnicity,
        Outdooroccupation = dto.Outdooroccupation, Livetropical = dto.Livetropical,
        Eversmoked = dto.Eversmoked, Eversmokednumbercigsperday = dto.Eversmokednumbercigsperday,
        Agestart = dto.Agestart, Agestop = dto.Agestop,
        Currentlysmoke = dto.Currentlysmoke, Currentlysmokenumbercigsperday = dto.Currentlysmokenumbercigsperday,
        Drnkbeeravg = dto.Drnkbeeravg, Drnkwineavg = dto.Drnkwineavg, Drnkspiritsavg = dto.Drnkspiritsavg,
        Drinkalcohol = dto.Drinkalcohol, Drnkunitsavg = dto.Drnkunitsavg,
        Admittedtohospital = dto.Admittedtohospital, Newdrugs = dto.Newdrugs, Newclinics = dto.Newclinics,
        Systolic = dto.Systolic, Diastolic = dto.Diastolic,
        Height = dto.Height, Weight = dto.Weight, Waist = dto.Waist,
        WeightMissing = dto.WeightMissing, WaistMissing = dto.WaistMissing,
        SmokingMissing = dto.SmokingMissing, DrinkingMissing = dto.DrinkingMissing,
        DateCompleted = DateTime.UtcNow, DataStatus = 0,
        Createdbyid = 0, Createdbyname = "PatientPortal", Createddate = DateTime.UtcNow,
        Lastupdatedbyid = 0, Lastupdatedbyname = "PatientPortal", Lastupdateddate = DateTime.UtcNow
    };

    private static void UpdateLifestyleFromDto(BbPappPatientLifestyle existing, PappLifestyleSubmitDto dto)
    {
        existing.Birthtown = dto.Birthtown; existing.Birthcountry = dto.Birthcountry;
        existing.Workstatusid = dto.Workstatusid; existing.Occupation = dto.Occupation;
        existing.Ethnicityid = dto.Ethnicityid; existing.Otherethnicity = dto.Otherethnicity;
        existing.Outdooroccupation = dto.Outdooroccupation; existing.Livetropical = dto.Livetropical;
        existing.Eversmoked = dto.Eversmoked; existing.Eversmokednumbercigsperday = dto.Eversmokednumbercigsperday;
        existing.Agestart = dto.Agestart; existing.Agestop = dto.Agestop;
        existing.Currentlysmoke = dto.Currentlysmoke; existing.Currentlysmokenumbercigsperday = dto.Currentlysmokenumbercigsperday;
        existing.Drnkbeeravg = dto.Drnkbeeravg; existing.Drnkwineavg = dto.Drnkwineavg; existing.Drnkspiritsavg = dto.Drnkspiritsavg;
        existing.Drinkalcohol = dto.Drinkalcohol; existing.Drnkunitsavg = dto.Drnkunitsavg;
        existing.Admittedtohospital = dto.Admittedtohospital; existing.Newdrugs = dto.Newdrugs; existing.Newclinics = dto.Newclinics;
        existing.Systolic = dto.Systolic; existing.Diastolic = dto.Diastolic;
        existing.Height = dto.Height; existing.Weight = dto.Weight; existing.Waist = dto.Waist;
        existing.WeightMissing = dto.WeightMissing; existing.WaistMissing = dto.WaistMissing;
        existing.SmokingMissing = dto.SmokingMissing; existing.DrinkingMissing = dto.DrinkingMissing;
        existing.Lastupdateddate = DateTime.UtcNow;
    }

    private static int ComputeDlqiTotal(PappDlqiSubmitDto dto) =>
        (dto.ItchsoreScore ?? 0) + (dto.EmbscScore ?? 0) + (dto.ShophgScore ?? 0) +
        (dto.ClothesScore ?? 0) + (dto.SocleisScore ?? 0) + (dto.SportScore ?? 0) +
        (dto.WorkstudScore ?? 0) + (dto.WorkstudnoScore ?? 0) +
        (dto.PartcrfScore ?? 0) + (dto.SexdifScore ?? 0) + (dto.TreatmentScore ?? 0);

    // ── Mapping to DTOs ───────────────────────────────────────────────────────

    private static PappDlqiDto MapDlqi(BbPappPatientDlqi d) => new()
    {
        PappDlqiId = d.PappDlqiId, PappFupId = d.PappFupId, Diagnosis = d.Diagnosis,
        ItchsoreScore = d.ItchsoreScore, EmbscScore = d.EmbscScore, ShophgScore = d.ShophgScore,
        ClothesScore = d.ClothesScore, SocleisScore = d.SocleisScore, SportScore = d.SportScore,
        WorkstudScore = d.WorkstudScore, WorkstudnoScore = d.WorkstudnoScore,
        PartcrfScore = d.PartcrfScore, SexdifScore = d.SexdifScore, TreatmentScore = d.TreatmentScore,
        TotalScore = d.TotalScore, Datecomp = d.Datecomp, SkipBreakup = d.SkipBreakup == 1
    };

    private static PappLifestyleDto MapLifestyle(BbPappPatientLifestyle l) => new()
    {
        PappLifestyleId = l.PappLifestyleId, PappFupId = l.PappFupId,
        Birthtown = l.Birthtown, Birthcountry = l.Birthcountry,
        Workstatusid = l.Workstatusid, Occupation = l.Occupation,
        Ethnicityid = l.Ethnicityid, Otherethnicity = l.Otherethnicity,
        Eversmoked = l.Eversmoked, Currentlysmoke = l.Currentlysmoke,
        Drinkalcohol = l.Drinkalcohol, Drnkunitsavg = l.Drnkunitsavg,
        Height = l.Height, Weight = l.Weight, Waist = l.Waist,
        SmokingMissing = l.SmokingMissing, DrinkingMissing = l.DrinkingMissing
    };

    private static PappCageDto MapCage(BbPappPatientCage c) => new()
    {
        PappCageId = c.PappCageId, PappFupId = c.PappFupId,
        Cutdown = c.Cutdown, Annoyed = c.Annoyed, Guilty = c.Guilty, Earlymorning = c.Earlymorning
    };

    private static PappEuroqolDto MapEuroqol(BbPappPatientEuroqol e) => new()
    {
        PappEuroqolId = e.PappEuroqolId, PappFupId = e.PappFupId,
        Mobility = e.Mobility, Selfcare = e.Selfcare, Usualacts = e.Usualacts,
        Paindisc = e.Paindisc, Anxdepr = e.Anxdepr, Comphealth = e.Comphealth, Howyoufeel = e.Howyoufeel
    };

    private static PappHadDto MapHad(BbPappPatientHad h) => new()
    {
        PappHadId = h.PappHadId, PappFupId = h.PappFupId,
        Q01tense = h.Q01tense, Q02enjoy = h.Q02enjoy, Q03frightened = h.Q03frightened,
        Q04laugh = h.Q04laugh, Q05worry = h.Q05worry, Q06cheerful = h.Q06cheerful,
        Q07relaxed = h.Q07relaxed, Q08slowed = h.Q08slowed, Q09butterflies = h.Q09butterflies,
        Q10appearence = h.Q10appearence, Q11restless = h.Q11restless, Q12enjoyment = h.Q12enjoyment,
        Q13panic = h.Q13panic, Q14goodbook = h.Q14goodbook,
        ScoreAnxiety = h.ScoreAnxiety, ResultAnxiety = h.ResultAnxiety,
        ScoreDepression = h.ScoreDepression, ResultDepression = h.ResultDepression,
        IsCountable = h.IsCountable
    };

    private static PappHaqDto MapHaq(BbPappPatientHaq h) => new()
    {
        PappHaqId = h.PappHaqId, PappFupId = h.PappFupId,
        Missingdata = h.Missingdata, Missingdatadetails = h.Missingdatadetails,
        Dressself = h.Dressself, Shampoo = h.Shampoo, Standchair = h.Standchair, Bed = h.Bed,
        Cutmeat = h.Cutmeat, Liftglass = h.Liftglass, Openmilk = h.Openmilk,
        Walkflat = h.Walkflat, Climbsteps = h.Climbsteps,
        Washdry = h.Washdry, Bath = h.Bath, Toilet = h.Toilet,
        Reachabove = h.Reachabove, Bend = h.Bend,
        Cardoor = h.Cardoor, Openjar = h.Openjar, Turntap = h.Turntap,
        Shop = h.Shop, Getincar = h.Getincar, Housework = h.Housework,
        Dressgroom = h.Dressgroom, Rising = h.Rising, Eating = h.Eating, Walking = h.Walking,
        Hygiene = h.Hygiene, Reach = h.Reach, Gripping = h.Gripping, Errands = h.Errands,
        Haqscore = h.Haqscore
    };

    // ── SAPASI helpers ────────────────────────────────────────────────────────

    /// <summary>
    /// SAPASI total = Σ RegionScore where
    ///   RegionScore = (Erythema + Induration + Desquamation) × Coverage × RegionWeight
    ///
    /// Weights: Head=0.1, Trunk=0.3, UpperLimbs=0.2, LowerLimbs=0.4
    /// Any null field in a region is treated as 0 (patient left it blank).
    /// </summary>
    private static float ComputeSapasiScore(PappSapasiSubmitDto dto)
    {
        static float RegionScore(int? coverage, int? e, int? i, int? d, float weight)
        {
            var severity = (e ?? 0) + (i ?? 0) + (d ?? 0);
            return severity * (coverage ?? 0) * weight;
        }

        return RegionScore(dto.HeadCoverage,       dto.HeadErythema,       dto.HeadInduration,       dto.HeadDesquamation,       0.1f)
             + RegionScore(dto.TrunkCoverage,      dto.TrunkErythema,      dto.TrunkInduration,      dto.TrunkDesquamation,      0.3f)
             + RegionScore(dto.UpperLimbsCoverage, dto.UpperLimbsErythema, dto.UpperLimbsInduration, dto.UpperLimbsDesquamation, 0.2f)
             + RegionScore(dto.LowerLimbsCoverage, dto.LowerLimbsErythema, dto.LowerLimbsInduration, dto.LowerLimbsDesquamation, 0.4f);
    }

    private static BbPappPatientSapasi BuildSapasiEntity(PappSapasiSubmitDto dto, int pappFupId, float score) => new()
    {
        PappFupId            = pappFupId,
        // Store coverage bands in the HeadArea / TrunkArea / … fields (float, 0–4)
        HeadArea             = dto.SkipForm ? null : dto.HeadCoverage,
        HeadErythema         = dto.SkipForm ? null : dto.HeadErythema,
        HeadInduration       = dto.SkipForm ? null : dto.HeadInduration,
        HeadDesquamation     = dto.SkipForm ? null : dto.HeadDesquamation,
        TrunkArea            = dto.SkipForm ? null : dto.TrunkCoverage,
        TrunkErythema        = dto.SkipForm ? null : dto.TrunkErythema,
        TrunkInduration      = dto.SkipForm ? null : dto.TrunkInduration,
        TrunkDesquamation    = dto.SkipForm ? null : dto.TrunkDesquamation,
        UpperLimbsArea       = dto.SkipForm ? null : dto.UpperLimbsCoverage,
        UpperLimbsErythema   = dto.SkipForm ? null : dto.UpperLimbsErythema,
        UpperLimbsInduration = dto.SkipForm ? null : dto.UpperLimbsInduration,
        UpperLimbsDesquamation = dto.SkipForm ? null : dto.UpperLimbsDesquamation,
        LowerLimbsArea       = dto.SkipForm ? null : dto.LowerLimbsCoverage,
        LowerLimbsErythema   = dto.SkipForm ? null : dto.LowerLimbsErythema,
        LowerLimbsInduration = dto.SkipForm ? null : dto.LowerLimbsInduration,
        LowerLimbsDesquamation = dto.SkipForm ? null : dto.LowerLimbsDesquamation,
        SapasiScore          = score,
        DateScored           = DateTime.UtcNow,
        DataStatus           = 0,
        Createdbyid = 0, Createdbyname = "PatientPortal", Createddate = DateTime.UtcNow,
        Lastupdatedbyid = 0, Lastupdatedbyname = "PatientPortal", Lastupdateddate = DateTime.UtcNow
    };

    private static void UpdateSapasiFromDto(BbPappPatientSapasi existing, PappSapasiSubmitDto dto, float score)
    {
        existing.HeadArea             = dto.SkipForm ? null : dto.HeadCoverage;
        existing.HeadErythema         = dto.SkipForm ? null : dto.HeadErythema;
        existing.HeadInduration       = dto.SkipForm ? null : dto.HeadInduration;
        existing.HeadDesquamation     = dto.SkipForm ? null : dto.HeadDesquamation;
        existing.TrunkArea            = dto.SkipForm ? null : dto.TrunkCoverage;
        existing.TrunkErythema        = dto.SkipForm ? null : dto.TrunkErythema;
        existing.TrunkInduration      = dto.SkipForm ? null : dto.TrunkInduration;
        existing.TrunkDesquamation    = dto.SkipForm ? null : dto.TrunkDesquamation;
        existing.UpperLimbsArea       = dto.SkipForm ? null : dto.UpperLimbsCoverage;
        existing.UpperLimbsErythema   = dto.SkipForm ? null : dto.UpperLimbsErythema;
        existing.UpperLimbsInduration = dto.SkipForm ? null : dto.UpperLimbsInduration;
        existing.UpperLimbsDesquamation = dto.SkipForm ? null : dto.UpperLimbsDesquamation;
        existing.LowerLimbsArea       = dto.SkipForm ? null : dto.LowerLimbsCoverage;
        existing.LowerLimbsErythema   = dto.SkipForm ? null : dto.LowerLimbsErythema;
        existing.LowerLimbsInduration = dto.SkipForm ? null : dto.LowerLimbsInduration;
        existing.LowerLimbsDesquamation = dto.SkipForm ? null : dto.LowerLimbsDesquamation;
        existing.SapasiScore          = score;
        existing.Lastupdateddate      = DateTime.UtcNow;
    }

    private static PappSapasiDto MapSapasi(BbPappPatientSapasi s) => new()
    {
        PappSapasiId           = s.PappSapasiId,
        PappFupId              = s.PappFupId,
        HeadCoverage           = (int?)s.HeadArea,
        HeadErythema           = s.HeadErythema,
        HeadInduration         = s.HeadInduration,
        HeadDesquamation       = s.HeadDesquamation,
        TrunkCoverage          = (int?)s.TrunkArea,
        TrunkErythema          = s.TrunkErythema,
        TrunkInduration        = s.TrunkInduration,
        TrunkDesquamation      = s.TrunkDesquamation,
        UpperLimbsCoverage     = (int?)s.UpperLimbsArea,
        UpperLimbsErythema     = s.UpperLimbsErythema,
        UpperLimbsInduration   = s.UpperLimbsInduration,
        UpperLimbsDesquamation = s.UpperLimbsDesquamation,
        LowerLimbsCoverage     = (int?)s.LowerLimbsArea,
        LowerLimbsErythema     = s.LowerLimbsErythema,
        LowerLimbsInduration   = s.LowerLimbsInduration,
        LowerLimbsDesquamation = s.LowerLimbsDesquamation,
        SapasiScore            = s.SapasiScore,
        DateScored             = s.DateScored
    };
}
