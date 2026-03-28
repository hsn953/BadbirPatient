# ADR-017 – Architecture Discussion: Separate Patient Database vs Shared DB

> **Document ID:** ADR-017  
> **Type:** Architecture Discussion Record (pre-decision)  
> **Status:** Under Discussion — awaiting stakeholder decision  
> **Date:** 2026-03-28  
> **Author:** BADBIR Patient App development team  
> **Audience:** Project leads (clinical + technical)

---

## 1. What Prompted This Discussion

The legacy BADBIR Patient App connects to the same SQL Server database as the Clinician System. Patient-submitted data goes into holding tables (`bb_papp_*`). A SQL Server Agent job running every 5 minutes copies validated rows to the live tables (`bbPatientDLQI`, `bbPatientLifestyle`, etc.) when conditions are met. A separate stored procedure (with elevated database permissions) handles patient identity verification by decrypting PII fields to match the submitted identity against the clinical record.

The user raised several important architectural concerns:

1. The 5-minute SQL Agent job is archaic, hard to test, has no retry, and can fail silently.
2. Should the two systems communicate via **API calls** instead of direct DB sharing?
3. Should **identity management** (ASP.NET Core Identity, AspNetUsers) live in a **dedicated Patient database** rather than the shared clinical DB?
4. Would a **fully separate Patient DB** allow the research snapshot of the clinician DB to remain "pure" at all times?
5. How does this hold up in a **Docker / containerised** hosting model?
6. What is the right **development sequence**: API → Web → Android → iOS?
7. What are the **mobile app release standards** (app store compliance)?

This document gives a full analysis of each topic and closes with a clear recommendation.

---

## 2. Key Facts Established From the Real Database

Before analysing options, these observations from `badbir_synthetic.db` are critical:

| Observation | Implication |
|---|---|
| `bbPatient` already has `Portal_IsRegistered` and `Portal_DateRegistered` columns | The Clinician DB was already modified for portal integration — precedent set |
| `bbPatientQuestionnaireSourcelkp` has entry `2 = Patient (via Portal)` | The shared DB already has a code for patient-entered data — no schema change needed |
| `bbPatientDLQI`, `bbPatientLifestyle`, `bbPatientCage`, `bbPatientPASIScores` exist in shared DB | These four forms can be promoted to the live shared DB |
| **HADS, HAQ, EuroQol have no live tables in the shared DB** | These forms currently have nowhere to go after promotion — the papp holding tables may be the only home they ever need |
| `createdbyname` in all shared tables is `VARCHAR(100)` | Can safely be set to `"PatientPortal"` with `createdbyid = 0`; patient's name never goes in audit columns |
| `bbPatientStatuslkp` has `statusid = 6 = "Registered awaiting consent form"` | The correct holding-state status already exists in the shared lookup |

---

## 3. Current Architecture (Baseline)

```
┌─────────────────────────────────────────────────────────────────────────┐
│                    Single SQL Server database                            │
│                                                                          │
│   ┌──────────────────────────┐     ┌──────────────────────────────────┐ │
│   │   Papp / Portal Zone     │     │     Live Clinical Zone           │ │
│   │                          │     │                                  │ │
│   │  AspNetUsers (Identity)  │     │  bbPatient                       │ │
│   │  bbPappPatientCohort...  │     │  bbPatientCohortHistory          │ │
│   │  bbPappPatientDLQI       │     │  bbPatientCohortTracking         │ │
│   │  bbPappPatientLifestyle  │─────▶ bbPatientDLQI                   │ │
│   │  bbPappPatientCage       │ 5min│  bbPatientLifestyle              │ │
│   │  bbPappPatientHad        │ job │  bbPatientCage                   │ │
│   │  bbPappPatientHaq        │     │  bbPatientPASIScores             │ │
│   │  bbPappPatientEuroqol    │     │                                  │ │
│   │  bbPappPatientSapasi     │     │  (HADS/HAQ/EuroQol: no table)    │ │
│   │  bbPappPatientPgaScore   │     │                                  │ │
│   └──────────────────────────┘     └──────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────────────┘
         ▲                                       ▲
         │                                       │
   BADBIR.Api (.NET 10)               Clinician System (.NET 6/8)
```

**Problems with this baseline:**
- SQL Agent job: not testable, fails silently, up to 5 min latency, no application-layer audit trail
- Identity tables (AspNetUsers, email addresses, phone numbers) sitting in the same DB as clinical data — different access and retention requirements
- The stored-procedure identity match requires elevated DB permissions granted to the Patient API's DB login — a security smell
- A researcher's snapshot of the DB includes both validated clinical data AND unvalidated patient-portal drafts — research data is not pure
- Both applications couple to the same connection string; if one app causes DB saturation the other is affected

---

## 4. Option A — Single DB, API-Triggered Promotion (What We Are Building Now)

Replace the SQL Agent job with `POST /api/admin/patients/{id}/promote` called by the Clinician System. Everything else stays as is.

**What it fixes:**
- ✅ Eliminates the SQL Agent job
- ✅ Transactional, retryable, audit-trailed promotion
- ✅ No new infrastructure required

**What it does NOT fix:**
- ❌ Identity data (email, phone) still in clinical DB
- ❌ Unvalidated papp data still in same DB as live clinical data (researcher snapshots include it)
- ❌ Still requires Clinician System to call Patient App API with credentials — coupling remains
- ❌ Still requires Patient App to read `bbPatient` directly for patient matching (encrypted stored-proc dependency)

**Verdict:** Good short-term fix, best risk-to-reward for immediate delivery. Still leaves structural debt.

---

## 5. Option B — Two Databases, API Integration Between Systems

Split into two SQL Server databases. The systems communicate via mutually-authenticated API calls rather than sharing a DB.

```
┌────────────────────────────────┐       HTTPS (mTLS)      ┌────────────────────────────────────┐
│       Patient DB               │◄──────────────────────►│        Clinician DB                │
│                                │                         │                                    │
│  AspNetUsers (Identity)        │   POST /verify-patient  │  bbPatient (PII, encrypted)        │
│  bbPappPatientCohortTracking   │   POST /promote         │  bbPatientCohortHistory            │
│  bbPappPatient{Form}           │   GET /fup-schedule     │  bbPatientCohortTracking           │
│  PatientPortalConfig           │                         │  bbPatientDLQI                     │
│                                │                         │  bbPatientLifestyle                │
│  (Code-First, free to evolve)  │                         │  bbPatientCage                     │
│                                │                         │  bbPatientPASIScores               │
│                                │                         │  (Forms with no live table: HADS,  │
│                                │                         │   HAQ, EuroQol stay in Patient DB) │
└────────────────────────────────┘                         └────────────────────────────────────┘
         ▲                                                           ▲
         │                                                           │
   BADBIR.Api (.NET 10)                                    Clinician System
   (owns Patient DB entirely)                              (owns Clinician DB)
```

### 5.1 How Patient Identity Verification Works in Option B

The current stored-procedure decrypts patient PII to match against submitted identity. In Option B:

1. Patient submits: date of birth, initials, NHS/CHI number (encrypted the same way) via the Patient App
2. Patient App calls `POST /api/clinician-system/verify-patient-identity` (a new Clinician System endpoint) with the encrypted values and a service-account bearer token
3. Clinician System decrypts the fields and returns `{ matched: true, patientId: 12345 }` (or 404) — it never sends PII back
4. No elevated DB permissions needed on the Patient DB side

This is cleaner than a stored proc with elevated permissions.

### 5.2 How Promotion Works in Option B

Two variants:

**Variant B1 (Clinician System pulls):** Clinician System polls or receives a webhook notification, fetches papp data from Patient DB via `GET /api/patient-forms/{patientId}/pending`, and writes directly to Clinician DB. The Patient DB only needs to receive a final "approved / rejected" callback.

**Variant B2 (Patient App pushes on signal):** Clinician System calls `POST /api/admin/patients/{id}/promote` with a `fupId`. The Patient App API calls the Clinician System API to push the form data. Both sides update atomically.

### 5.3 Assessment

| Criterion | Option A | Option B |
|---|---|---|
| Complexity to implement | Low | Medium-High |
| Identity data isolation | ❌ Same DB | ✅ Separate DB |
| Research snapshot purity | ❌ Papp drafts in same DB | ✅ Only confirmed data in Clinician DB |
| Resilience (one app down) | Partial | Better — HTTP retries, circuit breakers |
| Docker deployment | ✅ Same DB service | ✅ Two DB services, or same service different DBs |
| .NET version independence | N/A | ✅ Fully independent stacks |
| Cross-system transaction safety | ✅ Single DB atomic | ⚠️ Need eventual-consistency pattern or 2PC |
| Requires Clinician System API changes | 1 endpoint (promote) | 3–4 new endpoints |
| Data forms without live table (HADS/HAQ/EuroQol) | Stay in papp | Stay in Patient DB — never promoted to Clinician DB (this may be fine) |
| Risk to existing live system | Low | Medium (new Clinician API surface) |

---

## 6. Deeper Analysis: The "Pure Research Snapshot" Argument

This is the strongest argument FOR Option B. Researchers take DB snapshots for analyses. Right now, even with the 5-minute sync job, a snapshot taken while a patient's data is in the holding area will include that unvalidated, possibly incorrect data in the same database.

With Option B, the Clinician DB snapshot will only ever contain:
- Data entered by clinicians directly
- Data promoted from the Patient Portal after clinician approval (`qSourceID = 2`)

The Patient DB snapshot would contain drafts and works-in-progress — fine, since nobody analyses that.

**However:** The current synthetic DB shows that HADS, HAQ, and EuroQol have no live tables at all. This raises the possibility that the Clinician System never intends to receive those forms from the patient portal at all, or stores them differently (perhaps in a broader survey responses table not shown in the synthetic DB). This needs to be clarified before committing to either option.

---

## 7. The Audit Column Problem

All shared tables have `createdbyname VARCHAR(100)` and `createdbyid INTEGER`. Patient names are encrypted in `bbPatient`. If the Patient App uses the patient's username in audit columns, this creates a confidentiality breach (username → email address → identity).

**Correct approach (applies in ALL options):**
- `createdbyid = 0`
- `createdbyname = "PatientPortal"`
- The fact that it was the patient who entered the data is captured by `qSourceID = 2 (Patient via Portal)` in the form tables that support it

This is already correctly implemented in the current codebase.

---

## 8. Docker / Containerisation Considerations

In a Docker deployment (e.g., on a university VM running Docker Compose, or future AWS ECS):

```yaml
# Simplified compose structure
services:
  badbir-patient-api:
    image: badbir/patient-api:latest
    environment:
      - ConnectionStrings__PatientDb=Server=db;Database=BadbirPatient;...
      - ClinicianSystem__BaseUrl=https://clinician-api.badbir.org

  badbir-clinician-api:       # (existing, not ours)
    image: badbir/clinician-api:latest
    environment:
      - ConnectionStrings__ClinicianDb=Server=db;Database=BadbirClinician;...

  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    volumes:
      - sqldata:/var/opt/mssql
```

**With Option A (shared DB):** Both containers connect to the same database. If the SQL Server container restarts, both apps go down together. Scaling the Patient API horizontally is fine for reads but write contention with the Clinician System increases as patient load grows.

**With Option B (separate DBs on same SQL Server instance):** Two logical databases on one SQL Server container. Independent backup schedules. Patient DB can be cleared/reset without affecting clinical data. Each app still fails if the single SQL Server goes down — but they don't interfere with each other's data at rest.

**True separation (Option B, different instances):** Each app has its own SQL Server container. Maximum isolation. Cross-system calls go over HTTPS. No shared DB password. Different failure domains. This is the ideal Docker-native architecture. The cost is that the Patient API needs HTTP resilience (Polly retry, circuit breakers) for Clinician System calls.

**Recommendation for Docker:** Structure the code NOW to support Option B even if you deploy Option A initially. This means:
- `BadbirDbContext` for Patient App tables (code-first, fully owned)
- `ClinicianReadDbContext` for read-only access to shared bbPatient* tables (database-first, never migrated)
- `IClinicianSystemClient` interface with a `ClinicianSystemHttpClient` implementation and a future `ClinicianSystemDirectDbClient` fallback

The interface lets you swap between "call the API" and "query the DB" without changing business logic.

---

## 9. My Recommendation

### Short term (v1 — deliver working product now)

**Stay with Option A with clean separation in code.**

Rationale:
1. The Clinician System team needs to agree to new API endpoints. That conversation hasn't happened yet and will take time.
2. The promotion endpoint (`POST /api/admin/patients/{id}/promote`) is already designed and coded.
3. The Identity tables (AspNetUsers) should be in a **separate SQL Server database** from day one, even in v1. This costs almost nothing (different database name in the connection string, EF Core handles it) but separates patient identity data from clinical data cleanly. On the same SQL Server instance is fine — different logical databases.

### Medium term (v1.1 — after v1 is live)

**Migrate to Option B (separate Patient DB, API integration).**

The Clinician System will need:
- `POST /api/patient-portal/verify-identity` — encrypted patient identity match (replaces the stored proc)
- `GET /api/patient-portal/patients/{patientId}/fup-schedule` — follow-up windows visible in the Patient App
- `POST /api/patient-portal/patients/{patientId}/promote` — trigger data promotion (or Patient App calls this when clinician approves via Patient App UI)

These are modest, well-scoped endpoints. The conversation with the Clinician System team should happen during v1 development so v1.1 is ready to go.

### Long term (v2 — cloud deployment)

**Full microservice separation.** Patient service (own DB, own identity provider), Clinician service (own DB), forms data promoted via an event bus (Azure Service Bus or equivalent). Research snapshots are point-in-time exports from the Clinician DB only.

---

## 10. Important Finding: Missing Live Tables for HADS, HAQ, EuroQol

The `badbir_synthetic.db` database does **not contain live tables** for HADS, HAQ, or EuroQol submissions. This means one of:

1. Those tables exist in the production Clinician DB but were not included in the synthetic dataset (most likely)
2. The Clinician System never stores these forms from the Patient Portal — they remain as patient-held records only
3. They exist in a broader survey/questionnaire table not shown in the schema provided

**This needs to be clarified.** If interpretation (1) is correct, the live table schemas should be obtained from the client and matching entity classes need to be added. If interpretation (2) is correct, the papp holding tables for HADS, HAQ, and EuroQol are the final destination and the Patient DB becomes the authoritative source for those forms.

---

## 11. Development Sequence Recommendation

### 11.1 API First — Absolutely Correct

Building the API first is unambiguously the right choice for this project. Reasons:

1. **All three clients** (Web, Android, iOS) consume the same API — get it right once
2. **OpenAPI spec** is auto-generated from the API and becomes the contract for mobile teams
3. **Testing** at the API layer is comprehensive and fast (no UI)
4. **The Clinician System integration** can begin immediately once API endpoints are published
5. The API can be deployed to staging while the Web and Mobile apps are still in development — the Clinician System team can begin integration testing in parallel

### 11.2 Web App Second — Also Correct

Blazor Web App sits in the same solution and shares the `BADBIR.UI.Components` RCL with Mobile. Building the web app forces you to design the components properly — you discover UX issues earlier and cheaper than on mobile.

Additionally, web is significantly easier to test, debug, and iterate. University staff and researchers are on desktops. Getting them onto the new web portal early generates real-world feedback before the mobile apps are released.

### 11.3 Android Before iOS — Correct

- Larger BADBIR patient demographic likely to use Android
- No Apple Developer account / Mac required for initial development (reduces toolchain risk)
- App distribution in development is simpler (APK sideloading vs TestFlight)
- Google Play review is faster than App Store review
- MAUI Android debugging is more straightforward on CI

### 11.4 iOS Last

Apple's review process is slower and more restrictive. The App Store requires a paid Apple Developer Program membership for the University. First submission will take longer due to Apple health data policy review. Completing Android first means the app is battle-tested before the higher-scrutiny iOS review.

**Estimated sequence:**
```
Month 1-2:   BADBIR.Api (all endpoints, auth, forms, admin/promote)
Month 2-3:   BADBIR.Web (Blazor - registration, dashboard, all forms)
Month 3:     Integration testing with Clinician System (staging)
Month 4:     BADBIR.Mobile (Android) — reuses BADBIR.UI.Components
Month 5:     Google Play internal testing → open beta → release
Month 5-6:   BADBIR.Mobile (iOS) — largely the same Blazor code
Month 6:     App Store submission
```

---

## 12. Mobile App Release Standards (University App Store Distribution)

Based on the old Xamarin app and standard university app distribution requirements:

### 12.1 Package Identity (must be consistent forever once published)
```
Bundle ID / App ID: org.badbir.patientapp
```
The old Xamarin app used `org.badbir.patientapp`. This should be carried forward identically.

### 12.2 Versioning (on screen + in store)

The old app showed version on the Login screen:
```csharp
// LoginViewModel.cs (Xamarin, reference)
public string VersionText { get; } = $"App Version: v{VersionTracking.CurrentVersion} (Build {VersionTracking.CurrentBuild})";
```
This pattern must be replicated in MAUI:
- Display `v{Major}.{Minor}.{Patch} (Build {BuildNumber})` on login screen and About screen
- Version follows semantic versioning: `MAJOR.MINOR.PATCH` (e.g., `2.0.0`)
- Android: `versionCode` = monotonically increasing integer (e.g., `200`, `201`, ...)
- iOS: `CFBundleShortVersionString` = display version, `CFBundleVersion` = build number

### 12.3 Required Screens / Policies (App Store and University Requirements)

These screens are mandatory before first submission:

| Screen | Requirement | Source |
|---|---|---|
| **Privacy Policy** | Must be a live URL (not in-app only) | Apple App Store, Google Play |
| **Terms of Use** | Must be a live URL or full in-app page | University policy |
| **About / Information for Patients** | Explains what the app does and who to contact | Legacy app had this |
| **Version display** | Version visible on login and/or about screen | University policy + app stores |
| **Data controller statement** | Who controls your data (University of Aberdeen / BSR) | GDPR / NHS |
| **NHS logo usage** | Must comply with NHS Identity guidelines if NHS branding used | NHS Identity |
| **Accessibility statement** | WCAG 2.1 AA compliance statement | Public Sector Bodies Regulations |

### 12.4 Biometric Authentication

Old app used `USE_BIOMETRIC` (Android) and `NSFaceIDUsageDescription` (iOS). MAUI uses `Microsoft.Maui.Authentication` or `Plugin.Fingerprint`. The same permissions pattern carries forward. The Face ID usage description must be present in `Info.plist` to pass Apple review.

### 12.5 Health Data Classification

Apple will ask whether the app handles health data under their guidelines. DLQI, HADS, HAQ scores are patient-reported health data. Apple requires:
- A privacy-focused data declaration in App Store Connect
- No use of HealthKit unless explicitly needed (we do NOT use HealthKit — we use our own API)
- Clear description of what data is collected and why

### 12.6 Minimum OS Versions

Old app targeted:
- Android: `minSdkVersion 27` (Android 8.1 Oreo)
- iOS: `MinimumOSVersion 8.0`

For the new app, update to:
- Android: `minSdkVersion 29` (Android 10) — aligns with MAUI support matrix and covers 95%+ of active devices
- iOS: minimum iOS 16 — aligns with .NET MAUI support and Apple's own trend of dropping older versions

---

## 13. On the HTML Mockups

Yes, HTML mockup screens were generated for all three flows in a previous session:

| File | What it contains |
|---|---|
| `docs/mockups/index.html` | Overview and navigation between flows |
| `docs/mockups/01-registration.html` | 10 screens: eligibility screener → identity verification → account creation → holding state |
| `docs/mockups/02-baseline.html` | 17 screens: full baseline form sequence (Demographics, DLQI, CAGE, EuroQol, HAQ, HADS, SAPASI, PGA) |
| `docs/mockups/03-followup.html` | 11 screens: follow-up form sequence (PGA, MedProblems, DLQI, EuroQol, Smoking, Drinking, CAGE, HADS, HAQ, SAPASI) |

Open `docs/mockups/index.html` in any browser — they are self-contained with no external dependencies. Screenshots are also saved as PNG files alongside them.

---

## 14. Summary Table: Decision Points

| Decision | Short-term v1 | Long-term v2 |
|---|---|---|
| Identity DB | Separate logical DB (`BadbirPatient`) on same SQL Server | Keep separate — no change needed |
| Papp holding tables | Same DB as identity (Patient DB) | Keep in Patient DB |
| Promotion mechanism | API endpoint (replaces SQL Agent job) | API endpoint via inter-system HTTP call |
| Patient identity verification | Direct DB read via `ClinicianReadDbContext` OR stored proc | Clinician System `verify-identity` API endpoint |
| HADS/HAQ/EuroQol live destination | **Needs clarification from client** | TBD based on clarification |
| Docker DB structure | One SQL Server, two logical DBs | Two SQL Server instances (true separation) |
| Mobile app sequence | Android first, iOS second | N/A |
| Version display | On login screen and About screen | N/A |

---

## 15. Questions That Need Client Answers Before Proceeding

Before finalising architecture:

1. **OQ-07 (NEW): Do HADS, HAQ, and EuroQol have live tables in the production Clinician DB?** If yes, please provide their schemas. If no, the Patient DB becomes the only store for these forms — which is fine, but needs to be confirmed.

2. **OQ-08 (NEW): Is the Clinician System team willing to expose a `verify-patient-identity` API endpoint?** Or should we keep using the stored procedure / direct DB read for v1?

3. **OQ-09 (NEW): Should the Patient DB be a completely separate SQL Server database from day one,** or should we start with a separate logical database on the same instance and migrate later?

4. **OQ-10 (NEW): What is the agreed `createdbyid` value to use when the Patient App writes to shared tables?** We suggest `0` with `createdbyname = "PatientPortal"`. Confirm this is acceptable to the clinical data team.

---

*This document captures the architectural discussion. Once the above questions are answered and a direction is agreed, this ADR will be updated to "Decided" status and the relevant code changes (if any) will follow.*
