# ADR-017 – Architecture Discussion: Separate Patient Database vs Shared DB

> **Document ID:** ADR-017  
> **Type:** Architecture Decision Record  
> **Status:** Decided — Option B: Separate Patient Database with API integration, effective immediately  
> **Date:** 2026-03-28  
> **Last Updated:** 2026-03-28 (v2 — decision confirmed, open questions resolved)  
> **Author:** BADBIR development team (Patient App + Clinician System — same team)  
> **Audience:** All developers; future maintainers

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

## 2. Key Facts Established From the Real Database and Team Clarifications

Before analysing options, these observations from `badbir_synthetic.db` and subsequent team discussion are on record:

| Observation | Implication | Status |
|---|---|---|
| `bbPatient` already has `Portal_IsRegistered` and `Portal_DateRegistered` columns | The Clinician DB was already modified for portal integration — precedent set | ✅ Confirmed |
| `bbPatientQuestionnaireSourcelkp` has entry `2 = Patient (via Portal)` | The shared DB already has a code for patient-entered data — no schema change needed | ✅ Confirmed |
| `bbPatientDLQI`, `bbPatientLifestyle`, `bbPatientCage`, `bbPatientPASIScores` exist in shared DB | These four forms can be promoted to the live Clinician DB | ✅ Confirmed |
| **HADS, HAQ, EuroQol DO have live tables in the production Clinician DB** | All forms will be promoted. They were absent from the synthetic DB only because the synthetic dataset was a partial extract, not because the tables don't exist. No data is collected that is not required by ethics. | ✅ Resolved — OQ-07 closed |
| `createdbyname` in all shared tables is `VARCHAR(100)` | Can safely be set to `"PatientPortal"` with `createdbyid = 0`; patient's name never goes in audit columns. This is the current practice and continues unchanged. | ✅ Confirmed — OQ-10 closed |
| `bbPatientStatuslkp` has `statusid = 6 = "Registered awaiting consent form"` | The correct holding-state status already exists in the shared lookup | ✅ Confirmed |
| Both Patient App and Clinician System are developed by the same small team | Clinician System API endpoint changes can be coordinated and delivered immediately without cross-team negotiation delays | ✅ Confirmed — OQ-08 closed |

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
│   │  bbPappPatientEuroqol    │     │  bbPatientHADS                   │ │
│   │  bbPappPatientSapasi     │     │  bbPatientHAQ                    │ │
│   │  bbPappPatientPgaScore   │     │  bbPatientEuroQol                │ │
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
│                                │                         │  bbPatientHADS                     │
│                                │                         │  bbPatientHAQ                      │
│                                │                         │  bbPatientEuroQol                  │
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

| Criterion | Option A | **Option B (DECIDED)** |
|---|---|---|
| Complexity to implement | Low | Medium-High |
| Identity data isolation | ❌ Same DB | ✅ **Separate DB** |
| Research snapshot purity | ❌ Papp drafts in same DB | ✅ **Only confirmed data in Clinician DB** |
| Resilience (one app down) | Partial | **Better — HTTP retries, circuit breakers** |
| Docker deployment | ✅ Same DB service | ✅ **Two DB services, or same service different DBs** |
| .NET version independence | N/A | ✅ **Fully independent stacks** |
| Cross-system transaction safety | ✅ Single DB atomic | ⚠️ Need eventual-consistency pattern or 2PC |
| Requires Clinician System API changes | 1 endpoint (promote) | 3–4 new endpoints |
| HADS/HAQ/EuroQol destination | Promoted to live via same-DB proc | ✅ **Promoted via API call → live Clinician DB** |
| Risk to existing live system | Low | Medium — mitigated by same team ownership |
| Team coordination overhead | N/A | ✅ **None — same team** |

---

## 6. The "Pure Research Snapshot" Argument — Decisive

This is the strongest argument for Option B, and it is now decisive.

With Option B, the Clinician DB snapshot will only ever contain:
- Data entered by clinicians directly
- Data promoted from the Patient Portal **after clinician approval** (`qSourceID = 2`)

The Patient DB snapshot contains drafts, works-in-progress, and unconfirmed identity data — none of which researchers should analyse. This remains completely isolated.

**All forms (DLQI, Lifestyle, CAGE, PASIScores, HADS, HAQ, EuroQol) have corresponding live tables in the Clinician DB.** (The absence of HADS/HAQ/EuroQol in the synthetic dataset was a partial extract — those tables exist in production.) No data is collected that is not required by ethics approval. After clinician approval, all patient-submitted forms are promoted to the Clinician DB via the API-based promotion endpoint. The research snapshot stays pure.

---

## 7. The Audit Column Problem — Resolved

All shared tables have `createdbyname VARCHAR(100)` and `createdbyid INTEGER`. Patient names are encrypted in `bbPatient`. Using a patient's username in audit columns would create a confidentiality breach (username → email address → identity).

**Agreed approach (confirmed — applies to all shared tables written by the Patient App):**
- `createdbyid = 0`
- `createdbyname = "PatientPortal"`
- The fact that it was the patient who entered the data is captured by `qSourceID = 2 (Patient via Portal)` in the form tables that support it

This is the same convention already in use in the legacy system and continues unchanged. No further information is needed in audit fields when filled by a patient. **OQ-10 is closed.**

---

## 7a. Private Inter-Service Endpoints

The Patient App API and Clinician System API must communicate over private endpoints that are **not publicly accessible**. Several layers of protection are appropriate and can be combined:

### 7a.1 Docker Compose / Internal Network (University On-Premise)

Within a Docker Compose deployment, services on the same `docker network` can reach each other by service name without routing traffic through the internet or the public port mappings:

```yaml
services:
  badbir-patient-api:
    networks:
      - internal
      - public

  badbir-clinician-api:
    networks:
      - internal     # only reachable internally
      # NOT on public network

networks:
  internal:
    internal: true   # no external routing
  public:
    driver: bridge
```

With this configuration, `http://badbir-clinician-api/api/internal/...` is only reachable from within the Docker network. External internet traffic cannot reach it.

### 7a.2 Service Account JWT + Private Route Prefix

All inter-service calls use a **service-account JWT** (not a patient-facing token):
- The Clinician System holds a long-lived service JWT signed by the Patient App's Identity server
- The Patient App holds a long-lived service JWT signed by the Clinician System
- These tokens carry `role: "InternalService"` (or `"ClinicianSystem"`)
- Endpoints under `/api/internal/` or `/api/service/` require this role — they return `401` to any patient-facing token

### 7a.3 Shared API Key (Simple Fallback)

For simplicity during early development (before full JWT service accounts), a shared API key in a custom header is acceptable:

```
X-Internal-Api-Key: <secret-from-environment>
```

Both sides validate this key. The key is stored in environment variables / Docker secrets — never in source code.

### 7a.4 IP Allowlisting (Defence in Depth)

As an additional layer on IIS or nginx, the `/api/internal/*` route can be restricted to the IP address of the Clinician System server. This is a last-resort measure — the primary authentication is still the service JWT / API key.

### 7a.5 mTLS (Future / Cloud)

When deployed to AWS or Azure, mutual TLS (mTLS) client certificates provide the strongest inter-service guarantee. This is deferred to cloud deployment but the `IClinicianSystemClient` interface is designed to accommodate it.

---

## 7b. QR Code Integration

Both the Patient App and the Clinician System rely on a QR code facility. This enables the following flows:

### 7b.1 Clinician-Generated QR → Patient Scans to Register

1. Clinician navigates to a patient record in the Clinician System and clicks "Generate Patient Portal QR"
2. The Clinician System calls `POST /api/internal/patients/{patientId}/portal-qr` on the Patient API
3. The Patient API returns a **signed, time-limited token** encoded as a QR code (or returns a URL which the Clinician System renders as QR)
4. The clinician shows the QR code on-screen or prints it for the patient
5. The patient scans the QR code with the Patient App (or browser)
6. The Patient App decodes the token, pre-fills the patient's study ID / clinic, and navigates to the registration or link-account flow
7. The token is single-use and expires after 24 hours

This flow removes the manual entry of Study ID or CHI number during patient self-registration when the clinician is present — improving accuracy and reducing friction.

### 7b.2 Patient QR → Clinician Scans to Verify

1. A logged-in patient opens the Patient App and requests their "My QR Code"
2. The Patient App calls `GET /api/patients/me/portal-qr` to get a display token
3. The patient holds up their phone; the clinician scans it with the Clinician System
4. The Clinician System calls `POST /api/internal/verify-portal-qr` on the Patient API to resolve the token to a patient record
5. The Clinician System uses this to quickly pull up the correct patient record without typing

### 7b.3 QR Token Security Requirements

- Tokens must be signed (HMAC-SHA256 or JWT with `purpose: "portal-qr"` claim)
- Tokens expire: 24 hours for clinician-generated, 10 minutes for patient-display
- Tokens are single-use where initiating a registration flow (patient-registration QRs) — redeemable only once
- QR content is a URL: `https://patient.badbir.org/qr/{token}` — so a camera app with no Patient App installed can still open the web version

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

**Recommendation for Docker (now decided — Option B from day one):** Structure the code with full separation:
- `PatientDbContext` (code-first) → `BadbirPatient` database
- `IClinicianSystemClient` interface → implemented as `ClinicianSystemHttpClient` (HTTP calls, no direct DB)
- Public Docker ports expose only patient-facing and public API routes
- Internal Docker network handles all `/api/internal/*` traffic between services

---

## 9. Decision

### Option B — Separate Patient Database with API Integration — Effective Immediately

**This is the decided architecture.** It is not a future migration — it is the target for the current development sprint.

Rationale:
1. **Same-team advantage**: The Patient App and Clinician System are developed by the same small team with the same team lead. There is no cross-team coordination overhead for new Clinician System endpoints. This removes the main risk that previously argued for "Option A first".
2. **Research snapshot purity**: All seven forms (DLQI, Lifestyle, CAGE, PASI, HADS, HAQ, EuroQol) have live tables in the Clinician DB. With Option B, the Clinician DB snapshot is always authoritative and always clean — patient-submitted drafts never appear in it.
3. **Identity isolation is correct from day one**: ASP.NET Core Identity tables (AspNetUsers, email, phone numbers) should never be in the same database as validated clinical research data. Separating them now costs nothing and avoids a migration later.
4. **Docker-ready**: The architecture aligns naturally with containerised deployment — two services, two databases, internal network for service-to-service calls. No shared DB passwords, no shared DB connection.
5. **Eventual consistency is manageable**: The forms are not real-time financial transactions. Appropriate retry logic (Polly) and idempotent promotion endpoints handle the distributed transaction problem adequately for this use case.

### What Happens Now

1. **Patient DB (`BadbirPatient`)** — new, code-first, fully owned by Patient App
   - ASP.NET Core Identity (AspNetUsers etc.)
   - All papp holding tables (bbPappPatient*)
   - PatientPortalConfig and app-specific tables
   - QR code tokens table
   - `PendingClinicianActions` coordination table

2. **Clinician DB (existing, `BADBIR`)** — read by Patient App via API only, never by direct EF Core connection
   - All live form tables (bbPatientDLQI, bbPatientHADS etc.)
   - bbPatient, bbPatientCohortHistory, bbPatientCohortTracking
   - All lookup tables

3. **Inter-service communication** (private endpoints, service-account JWT, internal Docker network):
   - Patient App → Clinician System: `POST /api/internal/patients/verify-identity`
   - Patient App → Clinician System: `GET /api/internal/patients/{patientId}/fup-schedule`
   - Clinician System → Patient App: `POST /api/internal/patients/{patientId}/promote`
   - Clinician System → Patient App: `POST /api/internal/patients/{patientId}/reject`
   - Clinician System ↔ Patient App: QR code generation and validation endpoints

See **INT-001** for the complete Clinician System integration requirements and API contracts.

### Code Structure Required Right Now

```csharp
// Patient App owns this context entirely (Code-First)
class PatientDbContext : DbContext { ... }  // → BadbirPatient DB

// Patient App reads Clinician DB only via this interface — never directly
interface IClinicianSystemClient {
    Task<VerifyIdentityResult> VerifyPatientIdentityAsync(VerifyIdentityRequest request);
    Task<FupScheduleResult> GetFupScheduleAsync(int chid);
    Task PromotePatientDataAsync(int chid, PromoteRequest request);
}

// Implementation: HTTP calls to Clinician System API (production)
class ClinicianSystemHttpClient : IClinicianSystemClient { ... }
```

The `IClinicianSystemClient` interface ensures the code does not have a hard dependency on the HTTP implementation. In tests, it can be mocked. In the future, an alternative implementation (e.g., direct DB read for emergency fallback) can be swapped in without changing business logic.

---

## 10. Finding Resolved: All Forms Have Live Tables in the Clinician DB

The `badbir_synthetic.db` provided for development did **not** include tables for HADS, HAQ, or EuroQol. This was a partial extract of the production DB, not a complete representation.

**Confirmed by the project lead:** All forms collected by the Patient App have a corresponding live table in the Clinician DB. No data is collected that is not required by ethics approval. The live table schemas for HADS, HAQ, and EuroQol will be provided when the complete schema is shared. This does not block development — the papp holding tables are the source of truth until promotion occurs.

**OQ-07 is closed.**

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

| Decision | **Decided** |
|---|---|
| Architecture option | **Option B — Separate Patient DB + API integration, effective immediately** |
| Identity DB | **Separate SQL Server database (`BadbirPatient`)** — Patient App owns it entirely, code-first |
| Papp holding tables | **In Patient DB** (`BadbirPatient`) — never in Clinician DB |
| Promotion mechanism | **API endpoint** — Clinician System calls `POST /api/internal/patients/{id}/promote` on Patient App |
| Patient identity verification | **Clinician System `POST /api/internal/patients/verify-identity` endpoint** — replaces stored proc with elevated permissions |
| HADS/HAQ/EuroQol destination | **All forms promoted to live Clinician DB** — confirmed, all tables exist |
| Audit columns (createdbyid / createdbyname) | **`0 / "PatientPortal"`** — confirmed, matches existing practice |
| Docker DB structure | Two logical databases on same SQL Server instance (dev/v1), then separate SQL Server instances (v2/cloud) |
| Inter-service endpoint security | **Service-account JWT + internal Docker network + optional API key header** |
| QR code facility | **Both apps implement QR generation/validation** — Clinician System generates; Patient App validates and vice versa |
| Consent/patient inbox (Clinician System) | **Required** — holding-stage inbox for new patient registrations in both main pathways |
| Mobile app sequence | **Android first, iOS second** |
| Version display | **On login screen and About screen** — `v{Major}.{Minor}.{Patch} (Build N)` |
| Clinician System team coordination | **No overhead** — same team |

---

## 15. Open Questions — All Resolved

| OQ | Question | Resolution |
|---|---|---|
| OQ-07 | Do HADS, HAQ, EuroQol have live tables in the Clinician DB? | ✅ Yes — confirmed. Absent from synthetic DB was a partial extract. All ethics-approved forms have live tables. |
| OQ-08 | Is the Clinician System team willing to expose integration API endpoints? | ✅ Yes — same team, changes can be done right away. |
| OQ-09 | Separate SQL Server database from day one, or same instance different DB? | ✅ Separate logical database (`BadbirPatient`) on same SQL Server instance. True instance separation can come later with Docker. |
| OQ-10 | What `createdbyid` / `createdbyname` value when Patient App writes to shared tables? | ✅ `0` / `"PatientPortal"` — matches existing practice, confirmed to continue unchanged. |

No outstanding questions remain. Development can proceed on Option B immediately.

---

*This ADR is now in "Decided" status. v2 of this document reflects the confirmed direction as of 2026-03-28.*
