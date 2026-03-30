-- =============================================================================
-- BADBIR Patient Application – SQL Server 2022 Schema
-- =============================================================================
-- This script defines the full schema for the BADBIR Patient Application DB.
-- It is the authoritative reference used for EF Core Database-First scaffolding.
--
-- Conventions:
--   • All PK columns are INT IDENTITY(1,1) named <Table>Id
--   • All FK columns are named <ReferencedTable>Id
--   • Audit columns: CreatedAt, UpdatedAt on every major table
--   • Soft-delete: IsDeleted BIT (where appropriate)
-- =============================================================================

USE BADBIR;
GO

-- =============================================================================
-- 1. ASP.NET Core Identity tables (required for MapIdentityApi<ApplicationUser>)
-- =============================================================================

CREATE TABLE [dbo].[AspNetRoles] (
    [Id]               NVARCHAR(450)  NOT NULL,
    [Name]             NVARCHAR(256)  NULL,
    [NormalizedName]   NVARCHAR(256)  NULL,
    [ConcurrencyStamp] NVARCHAR(MAX)  NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR(450)  NOT NULL,
    [UserName]             NVARCHAR(256)  NULL,
    [NormalizedUserName]   NVARCHAR(256)  NULL,
    [Email]                NVARCHAR(256)  NULL,
    [NormalizedEmail]      NVARCHAR(256)  NULL,
    [EmailConfirmed]       BIT            NOT NULL DEFAULT 0,
    [PasswordHash]         NVARCHAR(MAX)  NULL,
    [SecurityStamp]        NVARCHAR(MAX)  NULL,
    [ConcurrencyStamp]     NVARCHAR(MAX)  NULL,
    [PhoneNumber]          NVARCHAR(MAX)  NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL DEFAULT 0,
    [TwoFactorEnabled]     BIT            NOT NULL DEFAULT 0,
    [LockoutEnd]           DATETIMEOFFSET NULL,
    [LockoutEnabled]       BIT            NOT NULL DEFAULT 0,
    [AccessFailedCount]    INT            NOT NULL DEFAULT 0,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR(450) NOT NULL,
    [RoleId] NVARCHAR(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles]([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id]         INT            IDENTITY(1,1) NOT NULL,
    [UserId]     NVARCHAR(450)  NOT NULL,
    [ClaimType]  NVARCHAR(MAX)  NULL,
    [ClaimValue] NVARCHAR(MAX)  NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers]([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [dbo].[AspNetRoleClaims] (
    [Id]         INT            IDENTITY(1,1) NOT NULL,
    [RoleId]     NVARCHAR(450)  NOT NULL,
    [ClaimType]  NVARCHAR(MAX)  NULL,
    [ClaimValue] NVARCHAR(MAX)  NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles]([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [dbo].[AspNetUserTokens] (
    [UserId]        NVARCHAR(450) NOT NULL,
    [LoginProvider] NVARCHAR(450) NOT NULL,
    [Name]          NVARCHAR(450) NOT NULL,
    [Value]         NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers]([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider]       NVARCHAR(450) NOT NULL,
    [ProviderKey]         NVARCHAR(450) NOT NULL,
    [ProviderDisplayName] NVARCHAR(MAX) NULL,
    [UserId]              NVARCHAR(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers]([Id]) ON DELETE CASCADE
);
GO

-- =============================================================================
-- 2. Core domain tables
-- =============================================================================

-- ---------------------------------------------------------------------------
-- Patients
-- One Patient corresponds to one AspNetUser (1-to-1 via UserId FK).
-- ---------------------------------------------------------------------------
CREATE TABLE [dbo].[Patients] (
    [PatientId]   INT            IDENTITY(1,1) NOT NULL,
    [UserId]      NVARCHAR(450)  NOT NULL,                      -- FK → AspNetUsers
    [NhsNumber]   NVARCHAR(20)   NOT NULL,
    [FirstName]   NVARCHAR(100)  NOT NULL,
    [LastName]    NVARCHAR(100)  NOT NULL,
    [DateOfBirth] DATE           NOT NULL,
    [Gender]      NVARCHAR(20)   NULL,                          -- 'Male','Female','Other','PreferNotToSay'
    [Ethnicity]   NVARCHAR(100)  NULL,
    [CreatedAt]   DATETIME2      NOT NULL DEFAULT SYSUTCDATETIME(),
    [UpdatedAt]   DATETIME2      NOT NULL DEFAULT SYSUTCDATETIME(),
    [IsDeleted]   BIT            NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Patients]          PRIMARY KEY ([PatientId]),
    CONSTRAINT [UQ_Patients_NhsNumber] UNIQUE ([NhsNumber]),
    CONSTRAINT [UQ_Patients_UserId]    UNIQUE ([UserId]),
    CONSTRAINT [FK_Patients_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers]([Id])
);
GO

-- ---------------------------------------------------------------------------
-- Dermatology-specific: Diagnosis
-- Links a patient to one or more biologic treatment episodes.
-- ---------------------------------------------------------------------------
CREATE TABLE [dbo].[PatientDiagnoses] (
    [DiagnosisId]  INT           IDENTITY(1,1) NOT NULL,
    [PatientId]    INT           NOT NULL,
    [DiagnosisCode] NVARCHAR(20) NOT NULL,   -- e.g. ICD-10 code
    [DiagnosisName] NVARCHAR(200) NOT NULL,  -- e.g. 'Plaque Psoriasis'
    [DiagnosedDate] DATE         NULL,
    [IsActive]     BIT           NOT NULL DEFAULT 1,
    [CreatedAt]    DATETIME2     NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT [PK_PatientDiagnoses] PRIMARY KEY ([DiagnosisId]),
    CONSTRAINT [FK_PatientDiagnoses_Patients] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients]([PatientId])
);
GO

-- =============================================================================
-- 3. Patient-Reported Outcome (PRO) Forms
-- =============================================================================

-- ---------------------------------------------------------------------------
-- EuroQol EQ-5D-5L
-- Each row = one complete EQ-5D-5L submission by a patient.
-- Dimensions are scored 1 (no problems) to 5 (extreme problems).
-- VAS is a 0–100 visual analogue scale.
-- ---------------------------------------------------------------------------
CREATE TABLE [dbo].[EuroQolSubmissions] (
    [SubmissionId]         INT       IDENTITY(1,1) NOT NULL,
    [PatientId]            INT       NOT NULL,
    [SubmittedAt]          DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

    -- EQ-5D-5L dimensions (1=No problems, 5=Extreme problems)
    [Mobility]             TINYINT   NOT NULL,   -- 1–5
    [SelfCare]             TINYINT   NOT NULL,   -- 1–5
    [UsualActivities]      TINYINT   NOT NULL,   -- 1–5
    [PainDiscomfort]       TINYINT   NOT NULL,   -- 1–5
    [AnxietyDepression]    TINYINT   NOT NULL,   -- 1–5

    -- EQ VAS (0–100)
    [VasScore]             TINYINT   NOT NULL,   -- 0–100

    -- Computed index value (populated by API from value set)
    [IndexValue]           DECIMAL(5,4) NULL,

    [Notes]                NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_EuroQolSubmissions] PRIMARY KEY ([SubmissionId]),
    CONSTRAINT [FK_EuroQolSubmissions_Patients] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients]([PatientId]),
    CONSTRAINT [CK_EuroQol_Mobility]          CHECK ([Mobility]          BETWEEN 1 AND 5),
    CONSTRAINT [CK_EuroQol_SelfCare]          CHECK ([SelfCare]          BETWEEN 1 AND 5),
    CONSTRAINT [CK_EuroQol_UsualActivities]   CHECK ([UsualActivities]   BETWEEN 1 AND 5),
    CONSTRAINT [CK_EuroQol_PainDiscomfort]    CHECK ([PainDiscomfort]    BETWEEN 1 AND 5),
    CONSTRAINT [CK_EuroQol_AnxietyDepression] CHECK ([AnxietyDepression] BETWEEN 1 AND 5),
    CONSTRAINT [CK_EuroQol_Vas]               CHECK ([VasScore]          BETWEEN 0 AND 100)
);
GO

-- ---------------------------------------------------------------------------
-- HAQ – Health Assessment Questionnaire (HAQ-DI)
-- 8 functional categories, each with 2–3 questions scored 0–3.
-- Stored as category-level difficulty scores (max per category used for HAQ-DI).
-- ---------------------------------------------------------------------------
CREATE TABLE [dbo].[HaqSubmissions] (
    [SubmissionId]            INT          IDENTITY(1,1) NOT NULL,
    [PatientId]               INT          NOT NULL,
    [SubmittedAt]             DATETIME2    NOT NULL DEFAULT SYSUTCDATETIME(),

    -- Category scores: 0=Without any difficulty, 1=With some difficulty, 
    --                  2=With much difficulty, 3=Unable to do
    [Dressing]                TINYINT      NOT NULL,   -- 0–3
    [Arising]                 TINYINT      NOT NULL,   -- 0–3
    [Eating]                  TINYINT      NOT NULL,   -- 0–3
    [Walking]                 TINYINT      NOT NULL,   -- 0–3
    [Hygiene]                 TINYINT      NOT NULL,   -- 0–3
    [Reach]                   TINYINT      NOT NULL,   -- 0–3
    [Grip]                    TINYINT      NOT NULL,   -- 0–3
    [Activities]              TINYINT      NOT NULL,   -- 0–3

    -- HAQ-DI = mean of 8 category scores (0–3, stored as DECIMAL)
    [HaqDiScore]              DECIMAL(4,3) NOT NULL,

    -- AIDS (Aids/Devices) and help adjustments flags
    [UsesDressingAids]        BIT          NOT NULL DEFAULT 0,
    [UsesArisingAids]         BIT          NOT NULL DEFAULT 0,
    [UsesEatingAids]          BIT          NOT NULL DEFAULT 0,
    [UsesWalkingAids]         BIT          NOT NULL DEFAULT 0,
    [UsesHygieneAids]         BIT          NOT NULL DEFAULT 0,
    [UsesReachAids]           BIT          NOT NULL DEFAULT 0,
    [UsesGripAids]            BIT          NOT NULL DEFAULT 0,
    [UsesActivitiesAids]      BIT          NOT NULL DEFAULT 0,

    [Notes]                   NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_HaqSubmissions] PRIMARY KEY ([SubmissionId]),
    CONSTRAINT [FK_HaqSubmissions_Patients] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients]([PatientId]),
    CONSTRAINT [CK_Haq_Dressing]   CHECK ([Dressing]   BETWEEN 0 AND 3),
    CONSTRAINT [CK_Haq_Arising]    CHECK ([Arising]    BETWEEN 0 AND 3),
    CONSTRAINT [CK_Haq_Eating]     CHECK ([Eating]     BETWEEN 0 AND 3),
    CONSTRAINT [CK_Haq_Walking]    CHECK ([Walking]    BETWEEN 0 AND 3),
    CONSTRAINT [CK_Haq_Hygiene]    CHECK ([Hygiene]    BETWEEN 0 AND 3),
    CONSTRAINT [CK_Haq_Reach]      CHECK ([Reach]      BETWEEN 0 AND 3),
    CONSTRAINT [CK_Haq_Grip]       CHECK ([Grip]       BETWEEN 0 AND 3),
    CONSTRAINT [CK_Haq_Activities] CHECK ([Activities] BETWEEN 0 AND 3)
);
GO

-- ---------------------------------------------------------------------------
-- HADS – Hospital Anxiety and Depression Scale
-- 14 items: 7 Anxiety (odd items) + 7 Depression (even items), each 0–3.
-- ---------------------------------------------------------------------------
CREATE TABLE [dbo].[HadsSubmissions] (
    [SubmissionId]  INT          IDENTITY(1,1) NOT NULL,
    [PatientId]     INT          NOT NULL,
    [SubmittedAt]   DATETIME2    NOT NULL DEFAULT SYSUTCDATETIME(),

    -- Anxiety items (A1–A7): 0–3 each, total 0–21
    [A1]            TINYINT      NOT NULL,
    [A2]            TINYINT      NOT NULL,
    [A3]            TINYINT      NOT NULL,
    [A4]            TINYINT      NOT NULL,
    [A5]            TINYINT      NOT NULL,
    [A6]            TINYINT      NOT NULL,
    [A7]            TINYINT      NOT NULL,

    -- Depression items (D1–D7): 0–3 each, total 0–21
    [D1]            TINYINT      NOT NULL,
    [D2]            TINYINT      NOT NULL,
    [D3]            TINYINT      NOT NULL,
    [D4]            TINYINT      NOT NULL,
    [D5]            TINYINT      NOT NULL,
    [D6]            TINYINT      NOT NULL,
    [D7]            TINYINT      NOT NULL,

    -- Computed subscale totals
    [AnxietyScore]     TINYINT   NOT NULL,  -- 0–21  (0-7 normal, 8-10 borderline, 11-21 abnormal)
    [DepressionScore]  TINYINT   NOT NULL,  -- 0–21

    [Notes]         NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_HadsSubmissions] PRIMARY KEY ([SubmissionId]),
    CONSTRAINT [FK_HadsSubmissions_Patients] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients]([PatientId]),
    CONSTRAINT [CK_Hads_A1] CHECK ([A1] BETWEEN 0 AND 3),
    CONSTRAINT [CK_Hads_A2] CHECK ([A2] BETWEEN 0 AND 3),
    CONSTRAINT [CK_Hads_A3] CHECK ([A3] BETWEEN 0 AND 3),
    CONSTRAINT [CK_Hads_A4] CHECK ([A4] BETWEEN 0 AND 3),
    CONSTRAINT [CK_Hads_A5] CHECK ([A5] BETWEEN 0 AND 3),
    CONSTRAINT [CK_Hads_A6] CHECK ([A6] BETWEEN 0 AND 3),
    CONSTRAINT [CK_Hads_A7] CHECK ([A7] BETWEEN 0 AND 3),
    CONSTRAINT [CK_Hads_D1] CHECK ([D1] BETWEEN 0 AND 3),
    CONSTRAINT [CK_Hads_D2] CHECK ([D2] BETWEEN 0 AND 3),
    CONSTRAINT [CK_Hads_D3] CHECK ([D3] BETWEEN 0 AND 3),
    CONSTRAINT [CK_Hads_D4] CHECK ([D4] BETWEEN 0 AND 3),
    CONSTRAINT [CK_Hads_D5] CHECK ([D5] BETWEEN 0 AND 3),
    CONSTRAINT [CK_Hads_D6] CHECK ([D6] BETWEEN 0 AND 3),
    CONSTRAINT [CK_Hads_D7] CHECK ([D7] BETWEEN 0 AND 3),
    CONSTRAINT [CK_Hads_AnxietyScore]    CHECK ([AnxietyScore]    BETWEEN 0 AND 21),
    CONSTRAINT [CK_Hads_DepressionScore] CHECK ([DepressionScore] BETWEEN 0 AND 21)
);
GO

-- =============================================================================
-- 4. Indexes
-- =============================================================================

CREATE INDEX [IX_Patients_UserId]         ON [dbo].[Patients] ([UserId]);
CREATE INDEX [IX_Patients_NhsNumber]      ON [dbo].[Patients] ([NhsNumber]);
CREATE INDEX [IX_PatientDiagnoses_PatId]  ON [dbo].[PatientDiagnoses] ([PatientId]);
CREATE INDEX [IX_EuroQol_PatientId]       ON [dbo].[EuroQolSubmissions] ([PatientId]);
CREATE INDEX [IX_EuroQol_SubmittedAt]     ON [dbo].[EuroQolSubmissions] ([SubmittedAt]);
CREATE INDEX [IX_Haq_PatientId]           ON [dbo].[HaqSubmissions] ([PatientId]);
CREATE INDEX [IX_Haq_SubmittedAt]         ON [dbo].[HaqSubmissions] ([SubmittedAt]);
CREATE INDEX [IX_Hads_PatientId]          ON [dbo].[HadsSubmissions] ([PatientId]);
CREATE INDEX [IX_Hads_SubmittedAt]        ON [dbo].[HadsSubmissions] ([SubmittedAt]);
GO

-- =============================================================================
-- 5. Seed: Default roles
-- =============================================================================

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
VALUES
    (NEWID(), 'Patient',       'PATIENT',       NEWID()),
    (NEWID(), 'Clinician',     'CLINICIAN',     NEWID()),
    (NEWID(), 'Administrator', 'ADMINISTRATOR', NEWID());
GO
