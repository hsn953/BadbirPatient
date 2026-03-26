# DSD-001 – Database Schema Design
## BADBIR Patient Application

> **Document ID:** DSD-001  
> **Version:** 0.1 (Draft)  
> **Status:** In Review  
> **Last Updated:** 2026-03-25

---

## 1. Overview

The BADBIR Patient Application connects to an **existing SQL Server 2022 database**. This document:
1. Maps the legacy schema (derived from `legacy_reference/old_xamarin_app/bbPatientApp/NSwagClient/NSwagGenerated.cs`) to the new data model.
2. Documents the new tables that need to be added for the new system.
3. Documents the encryption strategy for PII fields.

> ⚠️ The actual SQL Server table names must be confirmed once the database DDL is shared. This document uses inferred names derived from the legacy code.

---

## 2. Legacy Schema Observations

### 2.1 Core Patient Identifier

The legacy system uses an integer `chid` (Clinical History ID) as the primary patient identifier. The new system must:
- Retain `chid` as a foreign key into the existing patient table.
- Map it to the new `ApplicationUser.Id` (GUID string) via the `Patients` table.

### 2.2 Follow-Up Tracking

The legacy `pappFupId` (Patient App Follow-Up ID) appears on every form submission DTO. This links a form submission to a specific follow-up period. The table `BbPappPatientCohortTracking` (found in the legacy web portal component layer) manages this.

### 2.3 Audit Fields (Legacy Pattern)

Every legacy form DTO carries:
- `createdbyid` / `createdbyname` / `createddate`
- `lastupdatedbyid` / `lastupdatedbyname` / `lastupdateddate`
- `formStatus` (int: 0=NotStarted, 1=Completed, 2=Skipped — inferred)

The new system preserves this pattern, replacing the legacy integer user IDs with the Identity user GUID.

---

## 3. Existing Tables (Inferred from Legacy Code)

> These tables already exist in the database. No modifications should be made without DBA approval.

### 3.1 Patient-Related

| Table | Key Columns | Notes |
|---|---|---|
| `bb_patient` or similar | `chid` (PK), `nhs_no`, `chi`, `dob`, `initials` | Core patient demographics |
| `BbPappPatientCohortTracking` | `papp_fup_id` (PK), `chid` (FK), `fup_number`, `fup_date`, `next_visit_date` | Follow-up scheduling |
| `BbLoginLog` | `login_log_id`, `chid`, `login_date`, `ip_address` | Legacy audit log |

### 3.2 PRO Form Tables (Existing)

| Table | Key Columns | Legacy Model | Notes |
|---|---|---|---|
| `bb_papp_patient_euroqol` | `form_id`, `chid`, `papp_fup_id`, `mobility`, `selfcare`, `usualacts`, `paindisc`, `anxdepr`, `comphealth`, `howyoufeel` | `PatientEuroQol` | EQ-5D-3L confirmed |
| `bb_papp_patient_had` | `form_id`, `chid`, `papp_fup_id`, `q01`–`q14` | `PatientHad` | All 14 items confirmed |
| `bb_papp_patient_haq` | `form_id`, `chid`, `papp_fup_id`, 30+ question fields | `PatientHaq` | All 8 categories confirmed |
| `bb_papp_patient_dlqi` | `form_id`, `chid`, `papp_fup_id`, `itchsore_score`...`total_score` | `PatientDlqi` | See FORM-004 for legacy reversal |
| `bb_papp_patient_cage` | `form_id`, `chid`, `papp_fup_id`, `cutdown`, `annoyed`, `guilty`, `earlymorning` | `PatientCage` | |
| `bb_papp_patient_pgascore` | `form_id`, `chid`, `papp_fup_id`, `pgascore` | `PatientPgascore` | Values 1–5 (Clear–Severe) |
| `bb_papp_patient_lifestyle` | `form_id`, `chid`, `papp_fup_id`, `currentlysmoke`, `drinkalcohol`, `ever_smoked`, `avg_cigs_per_day`, `age_started_smoking`, `age_stopped_smoking`, `currently_smoke`, `current_cigs_per_day`, `drink_alcohol`, `units_per_week`, `birth_town`, `birth_country`, `ethnicity`, `work_status`, `outdoor_occupation`, `lived_tropical` | `PatientLifestyle` | Baseline only |
| `bb_papp_patient_med_problem_fup` | `form_id`, `chid`, `papp_fup_id`, `hospital_admissions`, `new_drugs`, `new_referrals`, `occupation`, `work_status`, `currently_smoke`, `current_cigs_per_day`, `drink_alcohol`, `units_per_week` | `PatientMedProblemFup` | FUP only |

> **EuroQol version confirmed:** EQ-5D-3L (3 levels). The 7 existing columns are correct. OQ-01 is closed.  
> **SAPASI table:** No existing DB table — `SapasiSubmissions` is a new table created for v1 (see §4.5). SAPASI is in v1 scope; the UX interaction design is TBD (see ADR-010).  
> **Actual table names:** Column and table names above are inferred from legacy code. Exact names will be confirmed from the SQLite DB file provided by the client.

---

## 4. New Tables Required

The following tables do not exist in the legacy system and must be added.

### 4.1 `AspNetUsers` and ASP.NET Core Identity Tables

Standard Identity tables (`AspNetUsers`, `AspNetRoles`, `AspNetUserRoles`, etc.) are required for the new authentication system. These are auto-created by EF Core migrations if the database does not already have them.

Custom extension: `ApplicationUser` adds:
- `PatientChid` (INT, nullable) — links Identity user to legacy `chid`

### 4.2 `PatientHoldingAccounts`

```sql
CREATE TABLE [dbo].[PatientHoldingAccounts] (
    [HoldingId]          INT            IDENTITY(1,1) NOT NULL,
    [UserId]             NVARCHAR(450)  NOT NULL,          -- FK → AspNetUsers
    [NhsNumberHash]      NVARCHAR(64)   NULL,              -- HMAC-SHA256 for lookup
    [NhsNumberEncrypted] NVARCHAR(MAX)  NULL,              -- AES encrypted
    [ChiNumberHash]      NVARCHAR(64)   NULL,
    [ChiNumberEncrypted] NVARCHAR(MAX)  NULL,
    [BadbirStudyNo]      NVARCHAR(20)   NULL,
    [InitialsEncrypted]  NVARCHAR(MAX)  NOT NULL,
    [DobEncrypted]       NVARCHAR(MAX)  NOT NULL,
    [CentreId]           INT            NOT NULL,          -- FK → clinical centres lkp
    [RegistrationPathway] TINYINT       NOT NULL,          -- 0=QR/Self, 1=ClinicianInvite
    [CreatedAt]          DATETIME2      NOT NULL DEFAULT SYSUTCDATETIME(),
    [ExpiresAt]          DATETIME2      NOT NULL,          -- CreatedAt + 14 days
    [EmailVerified]      BIT            NOT NULL DEFAULT 0,
    CONSTRAINT [PK_PatientHoldingAccounts] PRIMARY KEY ([HoldingId]),
    CONSTRAINT [FK_PatientHolding_AspNetUsers] FOREIGN KEY ([UserId])
        REFERENCES [dbo].[AspNetUsers]([Id]) ON DELETE CASCADE
);
```

### 4.3 `ConsentRecords`

```sql
CREATE TABLE [dbo].[ConsentRecords] (
    [ConsentId]          INT            IDENTITY(1,1) NOT NULL,
    [UserId]             NVARCHAR(450)  NOT NULL,
    [ConsentFormVersion] NVARCHAR(20)   NOT NULL,
    [ConsentedAt]        DATETIME2      NOT NULL DEFAULT SYSUTCDATETIME(),
    [IpAddress]          NVARCHAR(45)   NULL,
    [UserAgent]          NVARCHAR(500)  NULL,
    [IsWithdrawn]        BIT            NOT NULL DEFAULT 0,
    [WithdrawnAt]        DATETIME2      NULL,
    CONSTRAINT [PK_ConsentRecords] PRIMARY KEY ([ConsentId])
    -- No FK cascade on UserId — consent must be retained even if user is deleted (GDPR note)
);
```

### 4.4 `SecurityAuditLog`

```sql
CREATE TABLE [dbo].[SecurityAuditLog] (
    [LogId]       BIGINT         IDENTITY(1,1) NOT NULL,
    [EventType]   NVARCHAR(50)   NOT NULL,  -- 'Login', 'Logout', 'Recovery', 'TokenRevoke', etc.
    [UserId]      NVARCHAR(450)  NULL,       -- NULL for failed logins (user unknown)
    [Timestamp]   DATETIME2      NOT NULL DEFAULT SYSUTCDATETIME(),
    [IpAddress]   NVARCHAR(45)   NULL,
    [UserAgent]   NVARCHAR(500)  NULL,
    [Details]     NVARCHAR(MAX)  NULL,       -- JSON: additional context
    [Success]     BIT            NOT NULL DEFAULT 1,
    CONSTRAINT [PK_SecurityAuditLog] PRIMARY KEY ([LogId])
);
CREATE INDEX [IX_SecurityAuditLog_UserId]    ON [dbo].[SecurityAuditLog] ([UserId]);
CREATE INDEX [IX_SecurityAuditLog_EventType] ON [dbo].[SecurityAuditLog] ([EventType]);
CREATE INDEX [IX_SecurityAuditLog_Timestamp] ON [dbo].[SecurityAuditLog] ([Timestamp]);
```

### 4.5 `SapasiSubmissions` (New Form — In v1 Scope, Legacy Has None)

The SAPASI DB table is included in v1. The UX design decision (body map interaction) is deferred to the development sprint — the schema is independent of that choice.

> **Note on column structure:** Each region stores Coverage (0–4) and three separate severity components: Redness, Thickness, and Scaliness (each 0–4). These are stored separately to preserve the individual ratings, not just the combined severity sum.

```sql
CREATE TABLE [dbo].[SapasiSubmissions] (
    [SubmissionId]    INT          IDENTITY(1,1) NOT NULL,
    [Chid]            INT          NOT NULL,
    [PappFupId]       INT          NULL,
    [SubmittedAt]     DATETIME2    NOT NULL DEFAULT SYSUTCDATETIME(),

    -- Head region (body surface weight: 0.10)
    [HeadCoverage]    TINYINT      NOT NULL DEFAULT 0,  -- 0=None 1=<10% 2=10-30% 3=30-50% 4=>50%
    [HeadRedness]     TINYINT      NOT NULL DEFAULT 0,  -- 0–4
    [HeadThickness]   TINYINT      NOT NULL DEFAULT 0,  -- 0–4
    [HeadScaliness]   TINYINT      NOT NULL DEFAULT 0,  -- 0–4

    -- Upper Limbs (body surface weight: 0.20)
    [UlCoverage]      TINYINT      NOT NULL DEFAULT 0,
    [UlRedness]       TINYINT      NOT NULL DEFAULT 0,
    [UlThickness]     TINYINT      NOT NULL DEFAULT 0,
    [UlScaliness]     TINYINT      NOT NULL DEFAULT 0,

    -- Trunk (body surface weight: 0.30)
    [TrunkCoverage]   TINYINT      NOT NULL DEFAULT 0,
    [TrunkRedness]    TINYINT      NOT NULL DEFAULT 0,
    [TrunkThickness]  TINYINT      NOT NULL DEFAULT 0,
    [TrunkScaliness]  TINYINT      NOT NULL DEFAULT 0,

    -- Lower Limbs (body surface weight: 0.40)
    [LlCoverage]      TINYINT      NOT NULL DEFAULT 0,
    [LlRedness]       TINYINT      NOT NULL DEFAULT 0,
    [LlThickness]     TINYINT      NOT NULL DEFAULT 0,
    [LlScaliness]     TINYINT      NOT NULL DEFAULT 0,

    -- Computed total score (server-side; stored for historical tracking)
    [TotalScore]      DECIMAL(5,2) NULL,

    [FormStatus]      TINYINT      NOT NULL DEFAULT 0,
    [CreatedById]     INT          NOT NULL DEFAULT 0,
    [CreatedByName]   NVARCHAR(100) NULL,
    [CreatedDate]     DATETIME2    NOT NULL DEFAULT SYSUTCDATETIME(),
    [LastUpdatedById] INT          NOT NULL DEFAULT 0,
    [LastUpdatedByName] NVARCHAR(100) NULL,
    [LastUpdatedDate] DATETIME2    NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT [PK_SapasiSubmissions] PRIMARY KEY ([SubmissionId])
);
```

> **SAPASI Score Formula:** `TotalScore = Σ (RegionWeight × Coverage × (Redness + Thickness + Scaliness))` across all 4 regions. Max possible = 72. See FORM-007 for full formula with examples.

### 4.6 `PushTokens`

```sql
CREATE TABLE [dbo].[PushTokens] (
    [TokenId]     INT            IDENTITY(1,1) NOT NULL,
    [UserId]      NVARCHAR(450)  NOT NULL,
    [Platform]    NVARCHAR(10)   NOT NULL,  -- 'Android' or 'iOS'
    [Token]       NVARCHAR(500)  NOT NULL,
    [RegisteredAt] DATETIME2     NOT NULL DEFAULT SYSUTCDATETIME(),
    [IsActive]    BIT            NOT NULL DEFAULT 1,
    CONSTRAINT [PK_PushTokens] PRIMARY KEY ([TokenId]),
    CONSTRAINT [FK_PushTokens_Users] FOREIGN KEY ([UserId])
        REFERENCES [dbo].[AspNetUsers]([Id]) ON DELETE CASCADE
);
```

### 4.7 `NotificationLog`

```sql
CREATE TABLE [dbo].[NotificationLog] (
    [LogId]         BIGINT         IDENTITY(1,1) NOT NULL,
    [UserId]        NVARCHAR(450)  NOT NULL,
    [Channel]       NVARCHAR(10)   NOT NULL,   -- 'Email' or 'Push'
    [TriggerEvent]  NVARCHAR(50)   NOT NULL,   -- 'Reminder', 'DayOf', 'Overdue'
    [SentAt]        DATETIME2      NOT NULL DEFAULT SYSUTCDATETIME(),
    [Success]       BIT            NOT NULL DEFAULT 1,
    [ErrorMessage]  NVARCHAR(MAX)  NULL,
    CONSTRAINT [PK_NotificationLog] PRIMARY KEY ([LogId])
);
```

---

## 5. PII Encryption Strategy

### 5.1 Encrypted Columns

| Table | Column | Algorithm | Notes |
|---|---|---|---|
| `Patients` | `NhsNumber` | AES-256 | + HMAC hash stored separately |
| `Patients` | `ChiNumber` | AES-256 | + HMAC hash |
| `Patients` | `FirstName` | AES-256 | — |
| `Patients` | `LastName` | AES-256 | — |
| `Patients` | `BirthPlace` | AES-256 | — |
| `Patients` | `Occupation` | AES-256 | — |
| `PatientHoldingAccounts` | `NhsNumberEncrypted` | AES-256 | + hash column |
| `PatientHoldingAccounts` | `InitialsEncrypted` | AES-256 | — |
| `PatientHoldingAccounts` | `DobEncrypted` | AES-256 | — |

### 5.2 HMAC Strategy for Deterministic Lookups

To support identity verification (DOB + Initials + NHS/CHI lookup) and future NHS Login matching without decrypting all rows:

```
NhsNumber = "1234567890"
NhsNumberHash  = HMAC-SHA256(key=HMAC_KEY, data="1234567890")
NhsNumberEncrypted = AES-256-CBC(key=AES_KEY, data="1234567890")
```

Lookup query:
```sql
SELECT * FROM Patients 
WHERE NhsNumberHash = @inputHash
```

The HMAC key must be different from the AES key and stored separately.

### 5.3 Key Management

| Phase | Key Storage |
|---|---|
| Development | `dotnet user-secrets` |
| Phase 1 (IIS) | Windows environment variables on the server |
| Phase 2 (AWS) | AWS Secrets Manager (injected at container startup) |

---

## 6. Entity Relationship Overview

```
AspNetUsers (Identity)
    │  1:1
    ▼
Patients
    │  1:N
    ├── PatientDiagnoses
    ├── BbPappPatientCohortTracking (follow-ups)
    │       │  1:N
    │       ├── EuroQolSubmissions
    │       ├── HadsSubmissions
    │       ├── HaqSubmissions
    │       ├── DlqiSubmissions
    │       ├── CageSubmissions
    │       ├── SapasiSubmissions       (NEW)
    │       ├── PgaSubmissions
    │       └── LifestyleSubmissions
    ├── ConsentRecords
    ├── PushTokens                      (NEW)
    └── NotificationLog                 (NEW)

PatientHoldingAccounts → AspNetUsers (soft link, deleted on confirmation)
SecurityAuditLog       → UserId (no FK — must survive user deletion)
```

---

## 7. Open Database Questions

| # | Question | Impact |
|---|---|---|
| DB-01 | What are the exact table and column names in the production database? (DDL script needed) | EF Core scaffolding accuracy |
| DB-02 | Does the existing DB already have Identity tables (AspNetUsers etc.)? | Migration strategy |
| DB-03 | Is `chid` an auto-increment INT or a GUID? | FK mapping strategy |
| DB-04 | What is the encryption algorithm used by the Clinician System? AES-256-CBC? Key size? IV handling? | Must match exactly |
| DB-05 | Are there stored procedures for any of the form saves that must be retained? | EF Core `FromSqlRaw` vs. LINQ |
| DB-06 | Is the `PatientHoldingAccounts` table new or does a similar table already exist? | Avoid duplicate table creation |
| DB-07 | What is the SAPASI score formula used in production (if any prior implementation exists)? | Score calculation accuracy |
