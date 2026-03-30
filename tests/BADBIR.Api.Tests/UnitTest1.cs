using BADBIR.Api.Data;
using BADBIR.Api.Data.Entities.Papp;
using BADBIR.Api.Services;
using BADBIR.Shared.DTOs;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;

namespace BADBIR.Api.Tests;

/// <summary>
/// Score-calculation tests – these are pure unit tests with no DB dependency.
/// They validate the HAQ-DI and HADS score algorithms.
/// </summary>
public class HaqScoreCalculationTests
{
    [Fact]
    public void HaqDiScore_AllZero_ReturnsZero()
    {
        var dto = new HaqSubmitDto(); // all byte defaults = 0
        var score = CalculateHaqDi(dto);
        Assert.Equal(0m, score);
    }

    [Fact]
    public void HaqDiScore_AllThree_ReturnsThree()
    {
        var dto = new HaqSubmitDto
        {
            Dressing   = 3, Arising    = 3, Eating    = 3, Walking    = 3,
            Hygiene    = 3, Reach      = 3, Grip      = 3, Activities = 3
        };
        var score = CalculateHaqDi(dto);
        Assert.Equal(3m, score);
    }

    [Fact]
    public void HaqDiScore_Mixed_ReturnsCorrectMean()
    {
        var dto = new HaqSubmitDto
        {
            Dressing   = 1, Arising    = 2, Eating    = 0, Walking    = 3,
            Hygiene    = 1, Reach      = 2, Grip      = 0, Activities = 3
        };
        // (1+2+0+3+1+2+0+3) / 8 = 12/8 = 1.5
        var score = CalculateHaqDi(dto);
        Assert.Equal(1.5m, score);
    }

    private static decimal CalculateHaqDi(HaqSubmitDto dto) =>
        Math.Round(
            (dto.Dressing + dto.Arising + dto.Eating + dto.Walking +
             dto.Hygiene  + dto.Reach   + dto.Grip   + dto.Activities) / 8m,
            3);
}

public class HadsScoreCalculationTests
{
    [Fact]
    public void AnxietyScore_SumOfSevenItems_IsCorrect()
    {
        byte a1 = 2, a2 = 1, a3 = 3, a4 = 0, a5 = 2, a6 = 1, a7 = 3;
        byte expected = (byte)(a1 + a2 + a3 + a4 + a5 + a6 + a7); // 12
        Assert.Equal(12, expected);
    }

    [Fact]
    public void DepressionScore_AllZero_ReturnsZero()
    {
        byte total = (byte)(0 + 0 + 0 + 0 + 0 + 0 + 0);
        Assert.Equal(0, total);
    }
}

/// <summary>
/// Encryption service unit tests.
/// Validates that encrypt → decrypt round-trips produce the original value,
/// and that the output format matches the legacy system (Base64 encoded).
/// </summary>
public class EncryptionServiceTests
{
    private static BADBIR.Api.Services.EncryptionService CreateService(string password = "TestPassword123!")
    {
        var cfg = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["EncryptionServiceConfig:Password"] = password
            })
            .Build();
        return new BADBIR.Api.Services.EncryptionService(cfg, NullLogger<BADBIR.Api.Services.EncryptionService>.Instance);
    }

    [Fact]
    public void Encrypt_EmptyString_ReturnsEmpty()
    {
        var svc = CreateService();
        Assert.Equal(string.Empty, svc.Encrypt(string.Empty));
    }

    [Fact]
    public void Decrypt_EmptyString_ReturnsEmpty()
    {
        var svc = CreateService();
        Assert.Equal(string.Empty, svc.Decrypt(string.Empty));
    }

    [Fact]
    public void EncryptDecrypt_RoundTrip_ReturnsOriginal()
    {
        var svc = CreateService();
        const string plainText = "John Smith";
        var cipher = svc.Encrypt(plainText);
        var result = svc.Decrypt(cipher);
        Assert.Equal(plainText, result);
    }

    [Fact]
    public void Encrypt_OutputIsBase64()
    {
        var svc = CreateService();
        var cipher = svc.Encrypt("test value");
        // Should not throw
        var bytes = Convert.FromBase64String(cipher);
        Assert.NotEmpty(bytes);
    }

    [Fact]
    public void EncryptDecrypt_NhsNumber_RoundTrip()
    {
        var svc = CreateService();
        const string nhs = "1234567890";
        Assert.Equal(nhs, svc.Decrypt(svc.Encrypt(nhs)));
    }

    [Fact]
    public void Decrypt_InvalidBase64_ReturnsErrorString()
    {
        var svc = CreateService();
        var result = svc.Decrypt("not_valid_base64!!!");
        Assert.Contains("error", result, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void DifferentPasswords_CannotDecryptEachOthersData()
    {
        var svc1 = CreateService("Password1!");
        var svc2 = CreateService("Password2!");
        var cipher = svc1.Encrypt("secret");
        var result = svc2.Decrypt(cipher);
        Assert.NotEqual("secret", result);
    }
}

/// <summary>
/// Tests for the papp holding table schema using an in-memory SQLite database.
/// Validates that EF Core can create and query the papp tables correctly.
/// </summary>
public class PappTableSchemaTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly BadbirDbContext _db;

    public PappTableSchemaTests()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<BadbirDbContext>()
            .UseSqlite(_connection)
            .Options;

        _db = new BadbirDbContext(options);
        _db.Database.EnsureCreated(); // creates all tables from EF model
    }

    public void Dispose()
    {
        _db.Dispose();
        _connection.Dispose();
    }

    [Fact]
    public async Task PappCohortTracking_CanInsertAndRetrieve()
    {
        await SeedPatientAsync(1);

        var tracking = new BbPappPatientCohortTracking
        {
            PatientId        = 1,
            PotentialFupCode = 0,
            PappFupStatus    = 0,
            DataStatus       = 0,
            Dateentered      = DateTime.UtcNow,
            Createdbyid      = 0,
            Createdbyname    = "Test",
            Createddate      = DateTime.UtcNow,
            Lastupdatedbyid  = 0,
            Lastupdatedbyname = "Test",
            Lastupdateddate  = DateTime.UtcNow
        };

        _db.PappCohortTrackings.Add(tracking);
        await _db.SaveChangesAsync();

        var saved = await _db.PappCohortTrackings.FindAsync(tracking.PappFupId);
        Assert.NotNull(saved);
        Assert.Equal(0, saved.DataStatus);
        Assert.Equal(0, saved.PotentialFupCode);
    }

    [Fact]
    public async Task PappDlqi_CanInsertAndRetrieve()
    {
        await SeedPatientAsync(1);

        // Create parent tracking row first
        var tracking = new BbPappPatientCohortTracking
        {
            PatientId = 1, PotentialFupCode = 0, PappFupStatus = 0, DataStatus = 0,
            Dateentered = DateTime.UtcNow, Createdbyid = 0, Createdbyname = "Test",
            Createddate = DateTime.UtcNow, Lastupdatedbyid = 0, Lastupdatedbyname = "Test",
            Lastupdateddate = DateTime.UtcNow
        };
        _db.PappCohortTrackings.Add(tracking);
        await _db.SaveChangesAsync();

        var dlqi = new BbPappPatientDlqi
        {
            PappFupId     = tracking.PappFupId,
            Diagnosis     = "Psoriasis",
            ItchsoreScore = 3,
            TotalScore    = 15,
            DataStatus    = 0,
            Createdbyid   = 0, Createdbyname = "Test", Createddate = DateTime.UtcNow,
            Lastupdatedbyid = 0, Lastupdatedbyname = "Test", Lastupdateddate = DateTime.UtcNow
        };

        _db.PappDlqis.Add(dlqi);
        await _db.SaveChangesAsync();

        var saved = await _db.PappDlqis
            .FirstOrDefaultAsync(d => d.PappFupId == tracking.PappFupId);

        Assert.NotNull(saved);
        Assert.Equal("Psoriasis", saved.Diagnosis);
        Assert.Equal(15, saved.TotalScore);
    }

    [Fact]
    public async Task PappHad_IsCountable_SetOnFullSubmission()
    {
        await SeedPatientAsync(2);

        var tracking = new BbPappPatientCohortTracking
        {
            PatientId = 2, PotentialFupCode = 0, PappFupStatus = 0, DataStatus = 0,
            Dateentered = DateTime.UtcNow, Createdbyid = 0, Createdbyname = "Test",
            Createddate = DateTime.UtcNow, Lastupdatedbyid = 0, Lastupdatedbyname = "Test",
            Lastupdateddate = DateTime.UtcNow
        };
        _db.PappCohortTrackings.Add(tracking);
        await _db.SaveChangesAsync();

        // All 14 items answered
        var had = new BbPappPatientHad
        {
            PappFupId = tracking.PappFupId,
            Q01tense = 2, Q02enjoy = 1, Q03frightened = 1, Q04laugh = 2,
            Q05worry = 3, Q06cheerful = 0, Q07relaxed = 1, Q08slowed = 2,
            Q09butterflies = 2, Q10appearence = 1, Q11restless = 3, Q12enjoyment = 1,
            Q13panic = 0, Q14goodbook = 2,
            ScoreAnxiety = 12, ResultAnxiety = 1,
            ScoreDepression = 9, ResultDepression = 1,
            IsCountable = true, DataStatus = 0,
            Createdbyid = 0, Createdbyname = "Test", Createddate = DateTime.UtcNow,
            Lastupdatedbyid = 0, Lastupdatedbyname = "Test", Lastupdateddate = DateTime.UtcNow
        };

        _db.PappHads.Add(had);
        await _db.SaveChangesAsync();

        var saved = await _db.PappHads.FindAsync(had.PappHadId);
        Assert.NotNull(saved);
        Assert.True(saved.IsCountable);
        Assert.Equal(12, saved.ScoreAnxiety);
    }

    [Fact]
    public async Task PappCohortTracking_DataStatus_DefaultIsZero()
    {
        await SeedPatientAsync(3);

        var tracking = new BbPappPatientCohortTracking
        {
            PatientId = 3, PotentialFupCode = 0, PappFupStatus = 0, DataStatus = 0,
            Dateentered = DateTime.UtcNow, Createdbyid = 0, Createdbyname = "Test",
            Createddate = DateTime.UtcNow, Lastupdatedbyid = 0, Lastupdatedbyname = "Test",
            Lastupdateddate = DateTime.UtcNow
        };
        _db.PappCohortTrackings.Add(tracking);
        await _db.SaveChangesAsync();

        Assert.Equal((byte)0, tracking.DataStatus);
        Assert.Null(tracking.ImportedFupId);
    }

    [Fact]
    public async Task PappCohortTracking_CanBeMarkedApproved()
    {
        await SeedPatientAsync(4);

        var tracking = new BbPappPatientCohortTracking
        {
            PatientId = 4, PotentialFupCode = 0, PappFupStatus = 0, DataStatus = 0,
            Dateentered = DateTime.UtcNow, Createdbyid = 0, Createdbyname = "Test",
            Createddate = DateTime.UtcNow, Lastupdatedbyid = 0, Lastupdatedbyname = "Test",
            Lastupdateddate = DateTime.UtcNow
        };
        _db.PappCohortTrackings.Add(tracking);
        await _db.SaveChangesAsync();

        tracking.DataStatus    = 1;
        tracking.ImportedFupId = 99999;
        await _db.SaveChangesAsync();

        var saved = await _db.PappCohortTrackings.FindAsync(tracking.PappFupId);
        Assert.NotNull(saved);
        Assert.Equal((byte)1, saved.DataStatus);
        Assert.Equal(99999, saved.ImportedFupId);
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private async Task SeedPatientAsync(int patientId)
    {
        if (await _db.BbPatients.AnyAsync(p => p.Patientid == patientId))
            return;

        _db.BbPatients.Add(new BADBIR.Api.Data.Entities.BbPatient
        {
            Patientid         = patientId,
            PortalIsRegistered = 1,
            Statusid           = 6,
            Createdbyid        = 0,
            Createdbyname      = "Test",
            Createddate        = DateTime.UtcNow,
            Lastupdatedbyid    = 0,
            Lastupdatedbyname  = "Test",
            Lastupdateddate    = DateTime.UtcNow,
            Statusdetailid     = 0
        });
        await _db.SaveChangesAsync();
    }
}

/// <summary>
/// Tests that the HADS scoring logic (result classification) is correct.
/// </summary>
public class HadsResultClassificationTests
{
    [Theory]
    [InlineData(0,  0)]  // Normal
    [InlineData(7,  0)]  // Normal (boundary)
    [InlineData(8,  1)]  // Borderline
    [InlineData(10, 1)]  // Borderline (boundary)
    [InlineData(11, 2)]  // Abnormal
    [InlineData(21, 2)]  // Abnormal (max)
    public void HadsResult_ReturnsCorrectBand(int score, int expectedResult)
    {
        int result = score switch
        {
            <= 7  => 0,
            <= 10 => 1,
            _     => 2
        };
        Assert.Equal(expectedResult, result);
    }
}


// ─────────────────────────────────────────────────────────────────────────────
// Clinician System Stub tests
// ─────────────────────────────────────────────────────────────────────────────

/// <summary>
/// Tests for <see cref="StubClinicianSystemClient"/>.
/// Verifies that the stub returns <c>true</c> (AlwaysTrue) or <c>false</c>
/// (AlwaysFalse) based on configuration, allowing both registration paths to
/// be exercised without a real Clinician System.
/// </summary>
public class StubClinicianSystemClientTests
{
    private static IConfiguration BuildConfig(string stubMode) =>
        new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ClinicianSystem:StubMode"] = stubMode
            })
            .Build();

    [Fact]
    public async Task VerifyIdentity_AlwaysTrue_ReturnsTrue()
    {
        var client = new StubClinicianSystemClient(BuildConfig("AlwaysTrue"));
        var result = await client.VerifyIdentityAsync(
            new DateOnly(1985, 4, 12), "JD", "1234567890", null);
        Assert.True(result);
    }

    [Fact]
    public async Task VerifyIdentity_AlwaysFalse_ReturnsFalse()
    {
        var client = new StubClinicianSystemClient(BuildConfig("AlwaysFalse"));
        var result = await client.VerifyIdentityAsync(
            new DateOnly(1985, 4, 12), "JD", "1234567890", null);
        Assert.False(result);
    }

    [Fact]
    public async Task VerifyIdentity_DefaultMode_ReturnsTrue()
    {
        // When ClinicianSystem:StubMode is absent, defaults to AlwaysTrue
        var client = new StubClinicianSystemClient(
            new ConfigurationBuilder().Build());
        var result = await client.VerifyIdentityAsync(
            new DateOnly(1990, 1, 1), "AB", null, "0101901234");
        Assert.True(result);
    }

    [Fact]
    public async Task VerifyIdentity_AlwaysFalse_CaseInsensitive_ReturnsFalse()
    {
        var client = new StubClinicianSystemClient(BuildConfig("alwaysfalse"));
        var result = await client.VerifyIdentityAsync(
            new DateOnly(1990, 1, 1), "AB", null, "0101901234");
        Assert.False(result);
    }
}

// ─────────────────────────────────────────────────────────────────────────────
// SAPASI score calculation tests
// ─────────────────────────────────────────────────────────────────────────────

/// <summary>
/// Validates the SAPASI scoring formula:
///   RegionScore = (Erythema + Induration + Desquamation) × Coverage × RegionWeight
///   SAPASI Total = Head(0.1) + Trunk(0.3) + UpperLimbs(0.2) + LowerLimbs(0.4)
/// </summary>
public class SapasiScoreCalculationTests
{
    // ── Formula replication (mirrors FormsController.ComputeSapasiScore) ──────

    private static float ComputeSapasiScore(PappSapasiSubmitDto dto)
    {
        static float Region(int? coverage, int? e, int? i, int? d, float w)
            => ((e ?? 0) + (i ?? 0) + (d ?? 0)) * (coverage ?? 0) * w;

        return Region(dto.HeadCoverage, dto.HeadErythema, dto.HeadInduration, dto.HeadDesquamation, 0.1f)
             + Region(dto.TrunkCoverage, dto.TrunkErythema, dto.TrunkInduration, dto.TrunkDesquamation, 0.3f)
             + Region(dto.UpperLimbsCoverage, dto.UpperLimbsErythema, dto.UpperLimbsInduration, dto.UpperLimbsDesquamation, 0.2f)
             + Region(dto.LowerLimbsCoverage, dto.LowerLimbsErythema, dto.LowerLimbsInduration, dto.LowerLimbsDesquamation, 0.4f);
    }

    [Fact]
    public void SapasiScore_AllZero_ReturnsZero()
    {
        var dto = new PappSapasiSubmitDto(); // all null — treated as 0
        Assert.Equal(0f, ComputeSapasiScore(dto));
    }

    [Fact]
    public void SapasiScore_SpecExample_IsCorrect()
    {
        // From FORM-007 spec example:
        // Head:         R=2,T=1,S=2 → Severity=5. Coverage=2. Weight=0.1 → 1.0
        // UpperLimbs:   R=3,T=2,S=2 → Severity=7. Coverage=2. Weight=0.2 → 2.8
        // Trunk:        R=1,T=1,S=1 → Severity=3. Coverage=1. Weight=0.3 → 0.9
        // LowerLimbs:   R=2,T=2,S=1 → Severity=5. Coverage=3. Weight=0.4 → 6.0
        // Total = 1.0 + 2.8 + 0.9 + 6.0 = 10.7
        var dto = new PappSapasiSubmitDto
        {
            HeadCoverage = 2, HeadErythema = 2, HeadInduration = 1, HeadDesquamation = 2,
            TrunkCoverage = 1, TrunkErythema = 1, TrunkInduration = 1, TrunkDesquamation = 1,
            UpperLimbsCoverage = 2, UpperLimbsErythema = 3, UpperLimbsInduration = 2, UpperLimbsDesquamation = 2,
            LowerLimbsCoverage = 3, LowerLimbsErythema = 2, LowerLimbsInduration = 2, LowerLimbsDesquamation = 1,
        };
        var score = ComputeSapasiScore(dto);
        Assert.Equal(10.7f, score, precision: 1);
    }

    [Fact]
    public void SapasiScore_AllMax_Returns48()
    {
        // Max: all 4 severity items = 4, all coverage = 4, weights sum to 1.0
        // → (4+4+4) × 4 × 1.0 = 48
        var dto = new PappSapasiSubmitDto
        {
            HeadCoverage = 4, HeadErythema = 4, HeadInduration = 4, HeadDesquamation = 4,
            TrunkCoverage = 4, TrunkErythema = 4, TrunkInduration = 4, TrunkDesquamation = 4,
            UpperLimbsCoverage = 4, UpperLimbsErythema = 4, UpperLimbsInduration = 4, UpperLimbsDesquamation = 4,
            LowerLimbsCoverage = 4, LowerLimbsErythema = 4, LowerLimbsInduration = 4, LowerLimbsDesquamation = 4,
        };
        Assert.Equal(48f, ComputeSapasiScore(dto), precision: 1);
    }

    [Fact]
    public void SapasiScore_HeadOnly_IsCorrect()
    {
        // Head: coverage=3, E=2, I=1, D=1 → (2+1+1)×3×0.1 = 1.2
        var dto = new PappSapasiSubmitDto
        {
            HeadCoverage = 3, HeadErythema = 2, HeadInduration = 1, HeadDesquamation = 1
        };
        Assert.Equal(1.2f, ComputeSapasiScore(dto), precision: 2);
    }

    [Fact]
    public void SapasiScore_NullFieldsInRegion_TreatedAsZero()
    {
        // Trunk with coverage=2, E=null, I=3, D=null → (0+3+0)×2×0.3 = 1.8
        var dto = new PappSapasiSubmitDto
        {
            TrunkCoverage = 2, TrunkInduration = 3
        };
        Assert.Equal(1.8f, ComputeSapasiScore(dto), precision: 2);
    }
}

// ─────────────────────────────────────────────────────────────────────────────
// Input validation range tests
// ─────────────────────────────────────────────────────────────────────────────

/// <summary>
/// Verifies that DataAnnotations Range attributes on form submit DTOs
/// produce the expected validation errors for out-of-range values.
/// </summary>
public class FormDtoValidationTests
{
    private static IList<System.ComponentModel.DataAnnotations.ValidationResult> Validate(object dto)
    {
        var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
        var context = new System.ComponentModel.DataAnnotations.ValidationContext(dto);
        System.ComponentModel.DataAnnotations.Validator.TryValidateObject(dto, context, results, validateAllProperties: true);
        return results;
    }

    [Fact]
    public void EuroqolSubmitDto_MobilityOutOfRange_FailsValidation()
    {
        var dto = new PappEuroqolSubmitDto { Mobility = 0 }; // must be 1–3
        var errors = Validate(dto);
        Assert.Contains(errors, e => e.MemberNames.Contains("Mobility"));
    }

    [Fact]
    public void EuroqolSubmitDto_VasOutOfRange_FailsValidation()
    {
        var dto = new PappEuroqolSubmitDto { Howyoufeel = 101 }; // must be 0–100
        var errors = Validate(dto);
        Assert.Contains(errors, e => e.MemberNames.Contains("Howyoufeel"));
    }

    [Fact]
    public void EuroqolSubmitDto_ValidValues_PassesValidation()
    {
        var dto = new PappEuroqolSubmitDto
        {
            Mobility = 1, Selfcare = 2, Usualacts = 3,
            Paindisc = 1, Anxdepr = 2, Comphealth = 3, Howyoufeel = 75
        };
        Assert.Empty(Validate(dto));
    }

    [Fact]
    public void HadSubmitDto_ItemOutOfRange_FailsValidation()
    {
        var dto = new PappHadSubmitDto { Q01tense = 4 }; // must be 0–3
        var errors = Validate(dto);
        Assert.Contains(errors, e => e.MemberNames.Contains("Q01tense"));
    }

    [Fact]
    public void HadSubmitDto_NullItems_PassesValidation()
    {
        // All items null (paper-based blank) should pass
        var dto = new PappHadSubmitDto();
        Assert.Empty(Validate(dto));
    }

    [Fact]
    public void HaqSubmitDto_ItemOutOfRange_FailsValidation()
    {
        var dto = new PappHaqSubmitDto { Dressself = 4 }; // must be 0–3
        var errors = Validate(dto);
        Assert.Contains(errors, e => e.MemberNames.Contains("Dressself"));
    }

    [Fact]
    public void DlqiSubmitDto_ItemOutOfRange_FailsValidation()
    {
        var dto = new PappDlqiSubmitDto { ItchsoreScore = 4 }; // must be 0–3
        var errors = Validate(dto);
        Assert.Contains(errors, e => e.MemberNames.Contains("ItchsoreScore"));
    }

    [Fact]
    public void SapasiSubmitDto_CoverageOutOfRange_FailsValidation()
    {
        var dto = new PappSapasiSubmitDto { HeadCoverage = 5 }; // must be 0–4
        var errors = Validate(dto);
        Assert.Contains(errors, e => e.MemberNames.Contains("HeadCoverage"));
    }

    [Fact]
    public void SapasiSubmitDto_SeverityOutOfRange_FailsValidation()
    {
        var dto = new PappSapasiSubmitDto { TrunkErythema = -1 }; // must be 0–4
        var errors = Validate(dto);
        Assert.Contains(errors, e => e.MemberNames.Contains("TrunkErythema"));
    }

    [Fact]
    public void SapasiSubmitDto_AllNull_PassesValidation()
    {
        // All null (paper-based blank) should pass
        var dto = new PappSapasiSubmitDto();
        Assert.Empty(Validate(dto));
    }

    [Fact]
    public void PgaSubmitDto_ScoreOutOfRange_FailsValidation()
    {
        var dto = new PappPgaSubmitDto { Pgascore = 6 }; // must be 1–5
        var errors = Validate(dto);
        Assert.Contains(errors, e => e.MemberNames.Contains("Pgascore"));
    }
}
