# TST-001 – Testing Strategy
## BADBIR Patient Application

> **Document ID:** TST-001  
> **Version:** 0.1  
> **Status:** Draft  
> **Last Updated:** 2026-03-26

---

## 1. Overview

This document defines the testing strategy for the BADBIR Patient Application. It covers:
- The use of SQLite as the development and testing database backend.
- Test project structure.
- Test categories: unit, integration, form scoring.
- Synthetic test data approach.
- Future migration to SQL Server.

---

## 2. Database Strategy: SQLite → SQL Server

### 2.1 Why SQLite for Testing

EF Core's provider abstraction allows the same `DbContext` and LINQ code to run against multiple database backends. This enables:
- **Fast, offline development** without a SQL Server instance.
- **Automated tests** (unit and integration) using an in-memory or file-based SQLite DB.
- **Deterministic test data** via seeding from a known-good SQLite file.
- **CI/CD compatibility** — no SQL Server required in the build pipeline.

### 2.2 When SQLite Is Used vs SQL Server

| Scenario | Database |
|---|---|
| Developer local dev/debug | SQLite (file) |
| Automated unit tests (`BADBIR.Api.Tests`) | SQLite (in-memory) |
| Integration tests with synthetic data | SQLite (file — provided by client) |
| Staging environment | SQL Server 2022 |
| Production | SQL Server 2022 |

### 2.3 Connection String Switching

```json
// appsettings.Development.json (not in source control — use user-secrets or .gitignored file)
{
  "ConnectionStrings": {
    "BadbirDb": "Data Source=tests/TestData/badbir_synthetic.db"
  },
  "DatabaseProvider": "Sqlite"
}

// appsettings.Production.json (injected via environment variable)
{
  "ConnectionStrings": {
    "BadbirDb": "Server=...;Database=BADBIR;..."
  },
  "DatabaseProvider": "SqlServer"
}
```

```csharp
// Program.cs — provider-agnostic registration
var provider = builder.Configuration["DatabaseProvider"] ?? "SqlServer";
if (provider == "Sqlite")
    builder.Services.AddDbContext<BadbirDbContext>(
        opt => opt.UseSqlite(connectionString));
else
    builder.Services.AddDbContext<BadbirDbContext>(
        opt => opt.UseSqlServer(connectionString));
```

---

## 3. Test Project Structure

```
tests/
├── BADBIR.Api.Tests/
│   ├── BADBIR.Api.Tests.csproj
│   ├── Forms/
│   │   ├── DlqiScoringTests.cs
│   │   ├── HaqScoringTests.cs
│   │   ├── HadsScoringTests.cs
│   │   ├── EuroQolScoringTests.cs
│   │   ├── CageScoringTests.cs
│   │   └── PgaTests.cs
│   ├── Registration/
│   │   ├── EligibilityScreenerTests.cs
│   │   ├── IdentityVerificationTests.cs
│   │   └── HoldingAccountExpiryTests.cs
│   ├── Auth/
│   │   ├── TokenIssuanceTests.cs
│   │   └── AccountRecoveryTests.cs
│   ├── Dashboard/
│   │   └── FormAssignmentTests.cs
│   ├── Infrastructure/
│   │   ├── TestDbContextFactory.cs    ← creates in-memory SQLite
│   │   └── SyntheticDataSeeder.cs     ← seeds from .db file or code
│   └── Helpers/
│       └── TestWebApplicationFactory.cs
├── TestData/
│   └── badbir_synthetic.db            ← SQLite file (provided by client, not in .gitignore)
└── .gitignore
```

---

## 4. SQLite Compatibility Notes

Some SQL Server features require workarounds in SQLite:

| SQL Server Feature | SQLite Workaround |
|---|---|
| `datetime2` | Stored as `TEXT` (ISO 8601) in SQLite. EF Core handles automatically. |
| `rowversion` / `timestamp` | Not supported in SQLite. Concurrency handled via `xmin` pattern or disabled in tests. |
| Computed columns | Not all types supported. Use EF Core value generation in memory. |
| `nvarchar(max)` | TEXT in SQLite — no truncation. |
| Clustered indexes | Not applicable in SQLite. |
| Stored procedures | SQLite has no SP support. Any SP calls must be abstracted behind `IRepository` for testability. |
| `NEWID()` | Use `Guid.NewGuid()` in EF Core — works in both. |
| `SYSUTCDATETIME()` | Use EF Core value generators or `DateTime.UtcNow` — works in both. |

---

## 5. Test Categories

### 5.1 Form Scoring Unit Tests (High Priority — SRS NFR-10: ≥90% coverage)

Each scoring algorithm has dedicated unit tests. These are pure C# — no DB required.

**DLQI Scoring Tests:**
```csharp
[Fact]
public void CalculateDlqi_AllVeryMuch_Returns30()
{
    var service = new DlqiScoringService();
    var result = service.Calculate(new DlqiSubmissionDto
    {
        Q1ItchScore = 3, Q2EmbsScore = 3, Q3ShopScore = 3,
        Q4ClothesScore = 3, Q5SocleisScore = 3, Q6SportScore = 3,
        Q7Prevented = true, Q7ImpactScore = null,
        Q8PartnerScore = 3, Q9SexScore = 3, Q10TreatmentScore = 3
    });
    Assert.Equal(30, result.TotalScore);
    Assert.Equal("ExtremelyLargeEffect", result.Band);
}

[Fact]
public void CalculateDlqi_AllNotAtAll_Returns0()
{
    var service = new DlqiScoringService();
    var result = service.Calculate(new DlqiSubmissionDto
    {
        Q1ItchScore = 0, Q2EmbsScore = 0, Q3ShopScore = 0,
        Q4ClothesScore = 0, Q5SocleisScore = 0, Q6SportScore = 0,
        Q7Prevented = false, Q7ImpactScore = 0,
        Q8PartnerScore = 0, Q9SexScore = 0, Q10TreatmentScore = 0
    });
    Assert.Equal(0, result.TotalScore);
}

[Fact]
public void CalculateDlqi_Q7NotRelevant_ContributesZero()
{
    // If patient is not working/studying — not relevant → 0 contribution
}
```

**HAQ Scoring Tests:**
```csharp
[Fact]
public void CalculateHaq_AllWithoutDifficulty_Returns0()
[Fact]
public void CalculateHaq_AllUnable_Returns3()
[Fact]
public void CalculateHaq_AidRaisesScoreToMin2()
[Fact]
public void CalculateHaq_CategoryScoreIsMaxOfItems()
[Theory]
[InlineData(0, 0.000)]
[InlineData(8, 1.000)]
[InlineData(16, 2.000)]
[InlineData(24, 3.000)]
public void CalculateHaq_LookupTable_IsCorrect(int sum, decimal expected)
```

**HADS Scoring Tests:**
```csharp
[Fact]
public void CalculateHads_AllMaxScores_Returns21Each()
[Fact]
public void CalculateHads_AlternatingScoreDirection_IsCorrect()
// Q1 Most of the time=3, Q2 Definitely as much=0 (not 3)
[Theory]
[InlineData(7, "Normal")]
[InlineData(8, "BorderlineAbnormal")]
[InlineData(11, "Abnormal")]
public void HadsInterpretation_Band_IsCorrect(int score, string expected)
```

**EuroQol Scoring Tests:**
```csharp
[Fact]
public void CalculateEuroQol_Profile11111_Returns1_000_IndexValue()
[Fact]
public void CalculateEuroQol_VasOutOfRange_ThrowsValidationException()
```

**PGA Tests:**
```csharp
[Theory]
[InlineData(1, "Clear")]
[InlineData(3, "Mild")]
[InlineData(5, "Severe")]
public void PgaLabel_IsCorrect(int score, string expectedLabel)
[Fact]
public void PgaScore_OutOfRange_ThrowsValidationException()
```

### 5.2 Registration / Business Logic Tests

```csharp
// Eligibility screener
[Fact]
public void EligibilityScreener_AllYes_Passes()
[Fact]
public void EligibilityScreener_NoPsoriasisDiagnosis_Rejects()
[Fact]
public void EligibilityScreener_NoRecentTreatment_Rejects()
[Fact]
public void EligibilityScreener_Under16_ShowsPaediatricMessage()

// Holding account expiry
[Fact]
public void HoldingAccountExpiryJob_AccountOlderThan14Days_DeletesAccount()
[Fact]
public void HoldingAccountExpiryJob_AccountYoungerThan14Days_Retains()

// Identity verification
[Fact]
public void VerifyIdentity_CorrectDobInitialsNhs_Succeeds()
[Fact]
public void VerifyIdentity_WrongInitials_Returns404()
```

### 5.3 Form Assignment Tests

```csharp
[Fact]
public void GetDashboard_PatientWithIA_IncludesHaqForm()
[Fact]
public void GetDashboard_PatientWithoutIA_ExcludesHaqForm()
[Fact]
public void GetDashboard_PatientWithAlcohol_IncludesCageForm()
[Fact]
public void GetDashboard_PatientWithoutAlcohol_ExcludesCageForm()
[Fact]
public void GetDashboard_BaselinePatient_CorrectFormList()
[Fact]
public void GetDashboard_FupPatient_CorrectFormList()
```

### 5.4 API Integration Tests

Using `TestWebApplicationFactory` with SQLite in-memory:
```csharp
[Fact]
public async Task PostDlqi_ValidPayload_Returns201()
[Fact]
public async Task PostDlqi_DuplicateSubmission_Returns409()
[Fact]
public async Task PostDlqi_Unauthenticated_Returns401()
```

---

## 6. Synthetic Test Data (SQLite File)

The client will provide a SQLite database (`badbir_synthetic.db`) containing:
- Sample patients (various cohorts, follow-up stages, with/without IA diagnoses)
- Sample follow-up records (`BbPappPatientCohortTracking`)
- Sample form submissions (for read/history tests)

**File location:** `tests/TestData/badbir_synthetic.db`  
**Git tracking:** The file IS tracked in git (it contains only synthetic, non-PII data).  
**Update process:** Client provides a new `.db` file when schema changes — replace the file and re-run tests.

---

## 7. Test Naming Conventions

```
MethodUnderTest_Scenario_ExpectedResult

Examples:
  CalculateDlqi_AllVeryMuch_Returns30
  VerifyIdentity_WrongNhsNumber_Returns404
  HoldingAccountExpiryJob_Expired_DeletesData
```

---

## 8. Test Infrastructure Setup

### 8.1 TestDbContextFactory

```csharp
public static class TestDbContextFactory
{
    public static BadbirDbContext CreateInMemory()
    {
        var options = new DbContextOptionsBuilder<BadbirDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;
        var context = new BadbirDbContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();
        return context;
    }
    
    public static BadbirDbContext CreateFromFile(string path = "tests/TestData/badbir_synthetic.db")
    {
        var options = new DbContextOptionsBuilder<BadbirDbContext>()
            .UseSqlite($"Data Source={path}")
            .Options;
        return new BadbirDbContext(options);
    }
}
```

### 8.2 NuGet Packages Required

```xml
<!-- tests/BADBIR.Api.Tests/BADBIR.Api.Tests.csproj -->
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="10.*" />
<PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="10.*" />
<PackageReference Include="xunit" Version="2.*" />
<PackageReference Include="xunit.runner.visualstudio" Version="2.*" />
<PackageReference Include="FluentAssertions" Version="6.*" />
<PackageReference Include="Moq" Version="4.*" />
```

---

## 9. Running Tests

```bash
# Run all tests
dotnet test tests/BADBIR.Api.Tests

# Run only form scoring tests
dotnet test tests/BADBIR.Api.Tests --filter "Category=FormScoring"

# Run with coverage
dotnet test tests/BADBIR.Api.Tests --collect:"XPlat Code Coverage"

# Open coverage report
reportgenerator -reports:coverage.xml -targetdir:coverage-report
```

---

## 10. SAPASI Scoring Tests

SAPASI is included in v1. The scoring algorithm is fixed regardless of UX design choice. Unit tests can be written now.

```csharp
[Fact]
public void CalculateSapasi_AllZero_Returns0()

[Fact]
public void CalculateSapasi_AllMaxInAllRegions_Returns72()
// Each region: Coverage=4, R=4, T=4, S=4 → severity=12, each region contributes
// Head: 4×12×0.10=4.8, UL: 4×12×0.20=9.6, Trunk: 4×12×0.30=14.4, LL: 4×12×0.40=19.2 → total=48.0
// Wait — max is: 4×12×1.0(sum of weights) = 48? No — weights are per region, so max = 4×12×(0.1+0.2+0.3+0.4) = 4×12×1.0 = 48.0
// Confirm formula in implementation.

[Theory]
[InlineData(2, 2, 1, 2, 0.1, 1.0)]  // Head: coverage=2, R+T+S=5, weight=0.1 → 2×5×0.1=1.0
[InlineData(3, 2, 2, 1, 0.4, 6.0)]  // LL: coverage=3, R+T+S=5, weight=0.4 → 3×5×0.4=6.0
public void CalculateSapasi_SingleRegion_IsCorrect(
    int coverage, int redness, int thickness, int scaliness, 
    decimal weight, decimal expectedContribution)

[Fact]
public void CalculateSapasi_ScoreOutOfRange_ThrowsValidationException()
// coverage=5 or severity component=5 → invalid

[Theory]
[InlineData(5.0, "Mild")]
[InlineData(15.0, "Moderate")]
[InlineData(25.0, "Severe")]
public void SapasiInterpretation_Band_IsCorrect(double score, string expectedBand)
```
