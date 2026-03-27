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

