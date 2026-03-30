-- =============================================================================
-- BADBIR Patient App – Papp Holding Tables DDL
-- =============================================================================
-- These tables store patient-submitted PRO form data in a holding area until
-- a clinician promotes the data to the live shared tables (bbPatient*).
--
-- Run this script ONCE on any new environment before starting the API.
-- The script uses IF NOT EXISTS / IF OBJECT_ID guards so it is safe to re-run.
--
-- DataStatus values (applies to all papp tables):
--   0 = Holding  (awaiting clinician review)
--   1 = Approved (data promoted to live tables, ImportedFupId set)
--   2 = Rejected (clinician rejected registration)
-- =============================================================================

-- ---------------------------------------------------------------------------
-- bbPappPatientCohortTracking
-- One row per patient portal visit (baseline = PotentialFupCode 0).
-- PatientId FK → bbPatient.patientid
-- ImportedFupId → bbPatientCohortTracking.FupId (set after promotion)
-- ---------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS bbPappPatientCohortTracking (
    PappFupId         INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
    PatientId         INTEGER  NOT NULL,
    PotentialFupCode  INTEGER  NOT NULL DEFAULT 0,
    ImportedFupId     INTEGER  NULL,
    VisitDate         DATETIME NULL,
    Dateentered       DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    Comments          TEXT     NULL,
    PappFupStatus     INTEGER  NOT NULL DEFAULT 0,
    DataStatus        TINYINT  NOT NULL DEFAULT 0,
    createdbyid       INTEGER  NOT NULL DEFAULT 0,
    createdbyname     VARCHAR(100) NOT NULL DEFAULT 'PatientPortal',
    createddate       DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    lastupdatedbyid   INTEGER  NOT NULL DEFAULT 0,
    lastupdatedbyname VARCHAR(100) NOT NULL DEFAULT 'PatientPortal',
    lastupdateddate   DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    FOREIGN KEY (PatientId) REFERENCES bbPatient (patientid)
);

CREATE INDEX IF NOT EXISTS idx_pappct_patientid_status
    ON bbPappPatientCohortTracking (PatientId, DataStatus);

-- ---------------------------------------------------------------------------
-- bbPappPatientDlqi
-- Mirrors bbPatientDLQI column-for-column (GL Assessments licence).
-- ---------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS bbPappPatientDlqi (
    PappDlqiId        INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
    PappFupId         INTEGER  NOT NULL,
    diagnosis         VARCHAR(50) NULL,
    itchsore_score    INTEGER  NULL,
    embsc_score       INTEGER  NULL,
    shophg_score      INTEGER  NULL,
    clothes_score     INTEGER  NULL,
    socleis_score     INTEGER  NULL,
    sport_score       INTEGER  NULL,
    workstud_score    INTEGER  NULL,
    workstudno_score  INTEGER  NULL,
    partcrf_score     INTEGER  NULL,
    sexdif_score      INTEGER  NULL,
    treatment_score   INTEGER  NULL,
    total_score       INTEGER  NULL,
    datecomp          DATETIME NULL,
    skipBreakup       INTEGER  NOT NULL DEFAULT 0,
    DataStatus        TINYINT  NOT NULL DEFAULT 0,
    createdbyid       INTEGER  NOT NULL DEFAULT 0,
    createdbyname     VARCHAR(100) NOT NULL DEFAULT 'PatientPortal',
    createddate       DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    lastupdatedbyid   INTEGER  NOT NULL DEFAULT 0,
    lastupdatedbyname VARCHAR(100) NOT NULL DEFAULT 'PatientPortal',
    lastupdateddate   DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    FOREIGN KEY (PappFupId) REFERENCES bbPappPatientCohortTracking (PappFupId)
);

-- ---------------------------------------------------------------------------
-- bbPappPatientLifestyle
-- Mirrors bbPatientLifestyle. Birthtown/Birthcountry are AES-encrypted.
-- ---------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS bbPappPatientLifestyle (
    PappLifestyleId                  INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
    PappFupId                        INTEGER  NOT NULL,
    birthtown                        VARCHAR(256) NULL,
    birthcountry                     VARCHAR(256) NULL,
    workstatusid                     INTEGER  NULL,
    occupation                       VARCHAR(50)  NULL,
    ethnicityid                      INTEGER  NULL,
    otherethnicity                   VARCHAR(50)  NULL,
    outdooroccupation                BOOLEAN  NULL,
    livetropical                     BOOLEAN  NULL,
    eversmoked                       BOOLEAN  NULL,
    eversmokednumbercigsperday       INTEGER  NULL,
    agestart                         INTEGER  NULL,
    agestop                          INTEGER  NULL,
    currentlysmoke                   BOOLEAN  NULL,
    currentlysmokenumbercigsperday   INTEGER  NULL,
    drnkbeeravg                      INTEGER  NULL,
    drnkwineavg                      INTEGER  NULL,
    drnkspiritsavg                   INTEGER  NULL,
    drinkalcohol                     BOOLEAN  NULL,
    drnkunitsavg                     INTEGER  NULL,
    admittedtohospital               INTEGER  NULL,
    newdrugs                         INTEGER  NULL,
    newclinics                       INTEGER  NULL,
    systolic                         FLOAT    NULL,
    diastolic                        FLOAT    NULL,
    height                           FLOAT    NULL,
    weight                           FLOAT    NULL,
    waist                            FLOAT    NULL,
    weightMissing                    BOOLEAN  NOT NULL DEFAULT 0,
    waistMissing                     BOOLEAN  NOT NULL DEFAULT 0,
    smokingMissing                   BOOLEAN  NOT NULL DEFAULT 0,
    drinkingMissing                  BOOLEAN  NOT NULL DEFAULT 0,
    DateCompleted                    DATETIME NULL,
    DataStatus                       TINYINT  NOT NULL DEFAULT 0,
    createdbyid                      INTEGER  NOT NULL DEFAULT 0,
    createdbyname                    VARCHAR(100) NOT NULL DEFAULT 'PatientPortal',
    createddate                      DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    lastupdatedbyid                  INTEGER  NOT NULL DEFAULT 0,
    lastupdatedbyname                VARCHAR(100) NOT NULL DEFAULT 'PatientPortal',
    lastupdateddate                  DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    FOREIGN KEY (PappFupId) REFERENCES bbPappPatientCohortTracking (PappFupId)
);

-- ---------------------------------------------------------------------------
-- bbPappPatientCage
-- Mirrors bbPatientCage.
-- ---------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS bbPappPatientCage (
    PappCageId        INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
    PappFupId         INTEGER  NOT NULL,
    cutdown           BOOLEAN  NULL,
    annoyed           BOOLEAN  NULL,
    guilty            BOOLEAN  NULL,
    earlymorning      BOOLEAN  NULL,
    datecomp          DATETIME NULL,
    DataStatus        TINYINT  NOT NULL DEFAULT 0,
    createdbyid       INTEGER  NOT NULL DEFAULT 0,
    createdbyname     VARCHAR(100) NOT NULL DEFAULT 'PatientPortal',
    createddate       DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    lastupdatedbyid   INTEGER  NOT NULL DEFAULT 0,
    lastupdatedbyname VARCHAR(100) NOT NULL DEFAULT 'PatientPortal',
    lastupdateddate   DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    FOREIGN KEY (PappFupId) REFERENCES bbPappPatientCohortTracking (PappFupId)
);

-- ---------------------------------------------------------------------------
-- bbPappPatientEuroqol
-- EQ-5D-3L (3-level version per existing live table convention).
-- ---------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS bbPappPatientEuroqol (
    PappEuroqolId     INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
    PappFupId         INTEGER  NOT NULL,
    mobility          INTEGER  NULL,
    selfcare          INTEGER  NULL,
    usualacts         INTEGER  NULL,
    paindisc          INTEGER  NULL,
    anxdepr           INTEGER  NULL,
    comphealth        INTEGER  NULL,
    howyoufeel        INTEGER  NULL,
    DateCompleted     DATETIME NULL,
    DataStatus        TINYINT  NOT NULL DEFAULT 0,
    createdbyid       INTEGER  NOT NULL DEFAULT 0,
    createdbyname     VARCHAR(100) NOT NULL DEFAULT 'PatientPortal',
    createddate       DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    lastupdatedbyid   INTEGER  NOT NULL DEFAULT 0,
    lastupdatedbyname VARCHAR(100) NOT NULL DEFAULT 'PatientPortal',
    lastupdateddate   DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    FOREIGN KEY (PappFupId) REFERENCES bbPappPatientCohortTracking (PappFupId)
);

-- ---------------------------------------------------------------------------
-- bbPappPatientHad
-- HADS – 14 items (Q01–Q14). IsCountable triggers GL Assessments invoice.
-- ---------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS bbPappPatientHad (
    PappHadId         INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
    PappFupId         INTEGER  NOT NULL,
    q01tense          INTEGER  NULL,
    q02enjoy          INTEGER  NULL,
    q03frightened     INTEGER  NULL,
    q04laugh          INTEGER  NULL,
    q05worry          INTEGER  NULL,
    q06cheerful       INTEGER  NULL,
    q07relaxed        INTEGER  NULL,
    q08slowed         INTEGER  NULL,
    q09butterflies    INTEGER  NULL,
    q10appearence     INTEGER  NULL,
    q11restless       INTEGER  NULL,
    q12enjoyment      INTEGER  NULL,
    q13panic          INTEGER  NULL,
    q14goodbook       INTEGER  NULL,
    ScoreAnxiety      INTEGER  NULL,
    ResultAnxiety     INTEGER  NULL,
    ScoreDepression   INTEGER  NULL,
    ResultDepression  INTEGER  NULL,
    DateScored        DATETIME NULL,
    IsCountable       BOOLEAN  NOT NULL DEFAULT 0,
    DataStatus        TINYINT  NOT NULL DEFAULT 0,
    createdbyid       INTEGER  NOT NULL DEFAULT 0,
    createdbyname     VARCHAR(100) NOT NULL DEFAULT 'PatientPortal',
    createddate       DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    lastupdatedbyid   INTEGER  NOT NULL DEFAULT 0,
    lastupdatedbyname VARCHAR(100) NOT NULL DEFAULT 'PatientPortal',
    lastupdateddate   DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    FOREIGN KEY (PappFupId) REFERENCES bbPappPatientCohortTracking (PappFupId)
);

-- ---------------------------------------------------------------------------
-- bbPappPatientHaq
-- HAQ-DI – individual item responses + computed category scores.
-- ---------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS bbPappPatientHaq (
    PappHaqId         INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
    PappFupId         INTEGER  NOT NULL,
    missingdata       BOOLEAN  NULL,
    missingdatadetails TEXT     NULL,
    -- Category 1: Dressing & Grooming
    dressself         INTEGER  NULL,
    shampoo           INTEGER  NULL,
    -- Category 2: Arising
    standchair        INTEGER  NULL,
    bed               INTEGER  NULL,
    -- Category 3: Eating
    cutmeat           INTEGER  NULL,
    liftglass         INTEGER  NULL,
    openmilk          INTEGER  NULL,
    -- Category 4: Walking
    walkflat          INTEGER  NULL,
    climbsteps        INTEGER  NULL,
    -- Category 5: Hygiene
    washdry           INTEGER  NULL,
    bath              INTEGER  NULL,
    toilet            INTEGER  NULL,
    -- Category 6: Reach
    reachabove        INTEGER  NULL,
    bend              INTEGER  NULL,
    -- Category 7: Grip
    cardoor           INTEGER  NULL,
    openjar           INTEGER  NULL,
    turntap           INTEGER  NULL,
    -- Category 8: Activities
    shop              INTEGER  NULL,
    getincar          INTEGER  NULL,
    housework         INTEGER  NULL,
    -- Aids & Devices
    cane              INTEGER  NULL,
    crutches          INTEGER  NULL,
    walker            INTEGER  NULL,
    wheelchair        INTEGER  NULL,
    specialutensils   INTEGER  NULL,
    specialchair      INTEGER  NULL,
    dressing          INTEGER  NULL,
    dressingdetails   TEXT     NULL,
    loolift           INTEGER  NULL,
    bathseat          INTEGER  NULL,
    bathrail          INTEGER  NULL,
    longreach         INTEGER  NULL,
    jaropener         INTEGER  NULL,
    deviceother       TEXT     NULL,
    -- Computed category scores
    dressgroom        INTEGER  NULL,
    rising            INTEGER  NULL,
    eating            INTEGER  NULL,
    walking           INTEGER  NULL,
    hygiene           INTEGER  NULL,
    reach             INTEGER  NULL,
    gripping          INTEGER  NULL,
    errands           INTEGER  NULL,
    totalscore        INTEGER  NULL,
    haqscore          REAL     NULL,
    DateScored        DATETIME NULL,
    DataStatus        TINYINT  NOT NULL DEFAULT 0,
    createdbyid       INTEGER  NOT NULL DEFAULT 0,
    createdbyname     VARCHAR(100) NOT NULL DEFAULT 'PatientPortal',
    createddate       DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    lastupdatedbyid   INTEGER  NOT NULL DEFAULT 0,
    lastupdatedbyname VARCHAR(100) NOT NULL DEFAULT 'PatientPortal',
    lastupdateddate   DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    FOREIGN KEY (PappFupId) REFERENCES bbPappPatientCohortTracking (PappFupId)
);

-- ---------------------------------------------------------------------------
-- bbPappPatientPgaScore
-- Patient-reported global assessment (maps to bbPatientPASIScores.psglobid).
-- ---------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS bbPappPatientPgaScore (
    PappPgaId         INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
    PappFupId         INTEGER  NOT NULL,
    pgascore          INTEGER  NULL,   -- 1=Clear … 5=Severe
    DateScored        DATETIME NULL,
    DataStatus        TINYINT  NOT NULL DEFAULT 0,
    createdbyid       INTEGER  NOT NULL DEFAULT 0,
    createdbyname     VARCHAR(100) NOT NULL DEFAULT 'PatientPortal',
    createddate       DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    lastupdatedbyid   INTEGER  NOT NULL DEFAULT 0,
    lastupdatedbyname VARCHAR(100) NOT NULL DEFAULT 'PatientPortal',
    lastupdateddate   DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    FOREIGN KEY (PappFupId) REFERENCES bbPappPatientCohortTracking (PappFupId)
);

-- ---------------------------------------------------------------------------
-- bbPappPatientSapasi
-- SAPASI (Self-Assessed PASI) – new table, no live equivalent yet.
-- Four body regions × (area %, erythema, induration, desquamation).
-- ---------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS bbPappPatientSapasi (
    PappSapasiId         INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
    PappFupId            INTEGER  NOT NULL,
    -- Head
    HeadArea             REAL     NULL,
    HeadErythema         INTEGER  NULL,
    HeadInduration       INTEGER  NULL,
    HeadDesquamation     INTEGER  NULL,
    -- Trunk
    TrunkArea            REAL     NULL,
    TrunkErythema        INTEGER  NULL,
    TrunkInduration      INTEGER  NULL,
    TrunkDesquamation    INTEGER  NULL,
    -- Upper Limbs
    UpperLimbsArea       REAL     NULL,
    UpperLimbsErythema   INTEGER  NULL,
    UpperLimbsInduration INTEGER  NULL,
    UpperLimbsDesquamation INTEGER NULL,
    -- Lower Limbs
    LowerLimbsArea       REAL     NULL,
    LowerLimbsErythema   INTEGER  NULL,
    LowerLimbsInduration INTEGER  NULL,
    LowerLimbsDesquamation INTEGER NULL,
    SapasiScore          REAL     NULL,
    DateScored           DATETIME NULL,
    DataStatus           TINYINT  NOT NULL DEFAULT 0,
    createdbyid          INTEGER  NOT NULL DEFAULT 0,
    createdbyname        VARCHAR(100) NOT NULL DEFAULT 'PatientPortal',
    createddate          DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    lastupdatedbyid      INTEGER  NOT NULL DEFAULT 0,
    lastupdatedbyname    VARCHAR(100) NOT NULL DEFAULT 'PatientPortal',
    lastupdateddate      DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    FOREIGN KEY (PappFupId) REFERENCES bbPappPatientCohortTracking (PappFupId)
);
