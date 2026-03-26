# SRS-001 – Software Requirements Specification
## BADBIR Patient Application

> **Document ID:** SRS-001  
> **Version:** 0.1 (Draft)  
> **Status:** In Review  
> **Derived From:** URD-001, masterrequirements.md, legacy code analysis  
> **Last Updated:** 2026-03-25

---

## 1. Introduction

### 1.1 Purpose
This SRS defines the functional and non-functional software requirements for the BADBIR Patient Application. It is the authoritative reference for development and testing.

### 1.2 Scope
The system replaces three isolated legacy applications:
- `legacy_reference/old_web_portal_net45` — .NET 4.5 / NetTiers web portal
- `legacy_reference/old_api_decompiled` — Decompiled .NET 6 API (`https://patient.badbir.org/api`)
- `legacy_reference/old_xamarin_app` — Xamarin Android/iOS app

The new system is a unified mono-repo delivering:
- `BADBIR.Api` — .NET 10 REST API (backend)
- `BADBIR.Web` — Blazor Web App (browser access)
- `BADBIR.Mobile` — .NET MAUI Blazor Hybrid (Android/iOS native app)
- `BADBIR.UI.Components` — Shared Razor Class Library (~95% of UI)
- `BADBIR.Shared` — Shared DTOs, enums, constants

### 1.3 Definitions & Abbreviations

| Term | Definition |
|---|---|
| BADBIR | British Association of Dermatologists Biologics and Immunosuppressants Register |
| CHID | Clinical History ID — the legacy patient identifier in the BADBIR database |
| PappFupId | Patient App Follow-Up ID — identifies which follow-up period a form submission belongs to |
| PRO | Patient-Reported Outcome — a questionnaire completed by the patient |
| Holding State | A patient account registered but not yet clinician-confirmed |
| SSO | Single Sign-On |
| OIDC | OpenID Connect |
| PII | Personally Identifiable Information |
| Follow-up | A scheduled data collection period (Baseline=Month 0, FU1=Month 6, ...) |

---

## 2. System Context & Constraints

### 2.1 System Architecture
See `docs/architecture/SAD-001_System_Architecture_Design.md` for the full architecture.

### 2.2 Key Technical Constraints

| ID | Constraint |
|---|---|
| TC-01 | All projects must target .NET 10 |
| TC-02 | No Swashbuckle/third-party Swagger — use native `Microsoft.AspNetCore.OpenApi` |
| TC-03 | Authentication uses JWT Bearer tokens via `AddJwtBearer` and `MapIdentityApi` |
| TC-04 | EF Core 10 Database-First — entities are scaffolded from the existing SQL Server 2022 DB |
| TC-05 | UI components must be built in `BADBIR.UI.Components` (RCL), not in the web/mobile hosts |
| TC-06 | Configuration must be environment-agnostic (IIS Phase 1 → Docker/AWS Phase 2) |
| TC-07 | PII fields must be AES-encrypted before storage; same algorithm as Clinician System |
| TC-08 | Mobile offline storage uses SQLite (via Microsoft.EntityFrameworkCore.Sqlite) |

---

## 3. Functional Requirements

### 3.1 Authentication & Identity

#### FR-AUTH-01: Email-Based Identity
- The system uses the patient's **email address** as the primary login identifier.
- No separate arbitrary user IDs are presented to users.
- Internally, the system uses ASP.NET Core Identity (`ApplicationUser`) with `Email` as the username.

#### FR-AUTH-02: JWT Token Issuance
- On successful login, the API returns a short-lived **Access Token** (configurable, default 60 min) and a long-lived **Refresh Token** (configurable, default 30 days).
- Tokens are signed with a symmetric key configured in `appsettings.json` (`Jwt:Key`).
- Token claims must include: `sub` (userId), `email`, `roles`, `jti` (JWT ID for revocation).

#### FR-AUTH-03: Token Revocation
- All tokens for a user must be invalidatable on account recovery.
- The system maintains a revocation list (or uses a rotating `SecurityStamp` via ASP.NET Identity).
- Every token validation must check that the `SecurityStamp` claim matches the current value in the DB.

#### FR-AUTH-04: Biometric Login (Mobile)
- The MAUI mobile app stores the refresh token in the device's secure enclave (via `Microsoft.Maui.Storage.SecureStorage`).
- Biometric authentication unlocks access to the stored token; the token is then sent to the API.
- Biometric data is never transmitted to the server.

#### FR-AUTH-05: OIDC-Ready Architecture
- The `AddAuthentication` configuration must use OIDC-compatible patterns.
- External provider support (NHS Login) must be addable without architectural changes.
- The UI must be able to display multiple login options without redesign.

---

### 3.2 Registration Workflows

#### FR-REG-00: Eligibility Screener (Pathway A Only)

Before any demographics or consent forms are shown, Pathway A patients must pass a 3-question screener:

1. **Q1 — Diagnosis:** "Have you been diagnosed with psoriasis by a dermatologist?" → Must be YES.
2. **Q2 — Treatment Window:** "Have you started a new prescribed pill, injection, or light therapy (systemic treatment) for your psoriasis in the last 6 months?" → Must be YES.
3. **Q3 — Age:** "Are you currently 16 years of age or older?" → Must be YES.

- If Q1 or Q2 = NO → display rejection message; halt registration; no data stored.
- If Q3 = NO → display paediatric deferral message; halt registration; no data stored.
- Screener answers are NOT persisted to the database.
- Pathway B (clinician-initiated invite) bypasses the screener entirely.
- See FORM-009 for exact question text and messages.

#### FR-REG-01: Patient Identity Verification
- Patients are verified against the BADBIR database using exactly **three data points**:
  1. Date of Birth
  2. Initials (first initial + last initial)
  3. One of: NHS Number, CHI Number, or BADBIR Study Number
- Verification logic runs server-side against the existing database.

#### FR-REG-02: Pathway A — Patient Self-Registration
1. Patient navigates to public URL or scans centre-specific QR code.
2. Patient selects their clinical centre.
3. Patient reviews eligibility criteria and confirms.
4. Patient submits demographics (name, DOB, NHS/CHI/BADBIR number, email, password).
5. Server creates an account in the **Holding Table** (not in the main patient table).
6. Server sends a verification email.
7. Patient verifies email, then can begin baseline questionnaires.
8. A 14-day expiry timer starts.
9. If not clinician-confirmed within 14 days → account + all data permanently deleted.

#### FR-REG-03: Pathway B — Clinician-Initiated Invite
1. Clinician creates invite in Clinician System → API sends invitation email.
2. Patient clicks link → directed to registration page with demographics pre-populated.
3. Patient reviews demographics, completes consent, sets password.
4. Account is immediately active (no 14-day holding state).

#### FR-REG-04: Electronic Consent Record
- Every consent submission must store: `PatientId`, `ConsentFormVersion`, `ConsentTimestamp` (UTC), `IPAddress`, `DeviceType`.
- Consent form version must be configurable to support future re-consent workflows.

#### FR-REG-05: 14-Day Expiry (Holding Accounts)
- A background job (hosted service) runs daily.
- Any holding account older than 14 days is hard-deleted, including all submitted form data.
- The deletion must be logged in the audit trail.

#### FR-REG-06: Account Recovery
1. User initiates "Forgot Password / Email" flow.
2. Server verifies identity via FR-REG-01.
3. User sets new email + password.
4. Server calls `UserManager.UpdateSecurityStampAsync()` to invalidate all existing tokens.
5. A new token is issued and the user is logged in.
6. Event is written to the audit log.

---

### 3.3 Dashboard & Form Sequencing

#### FR-DASH-01: Follow-Up Period Determination
- On login, the API determines the patient's current follow-up number based on their consent date and follow-up schedule.
- Follow-up periods: Baseline (Month 0), FU1–FU6 (Months 6–36), FU7+ (Months 48+, annual).
- The API returns the follow-up number and the list of assigned forms via the dashboard endpoint.

#### FR-DASH-02: Form Assignment Logic

> **Note: SAPASI UX design (body map vs sliders) is TBD — will be finalised at development sprint. Data model and API are fixed. See FORM-007.**

Form sequences mirror the actual paper form order:

**Baseline Sequence:**

| Sequence | Form | Conditional? |
|---|---|---|
| 1 | Demographics / Lifestyle (includes PGA at end) | No |
| 2 | CAGE | Yes — only if DrinkAlcohol = true |
| 3 | DLQI | No |
| 4 | EuroQol (5D + VAS) | No |
| 5 | HAQ | Yes — only if IA diagnosis |
| 6 | HADS | No |
| 7 | SAPASI | No |

**Follow-Up Sequence (FU 1–6 and FU 7+):**

| Sequence | Form | Conditional? |
|---|---|---|
| 1 | PGA | No |
| 2 | Medical Problems Update (incl. occupation) | No |
| 3 | DLQI | No |
| 4 | EuroQol (5D + VAS) | No |
| 5 | Smoking Update | No |
| 6 | Drinking Update | No |
| 7 | CAGE | Yes — only if DrinkAlcohol = true |
| 8 | HADS | No |
| 9 | HAQ | Yes — only if IA diagnosis |
| 10 | SAPASI | No |

#### FR-DASH-03: Conditional Form Logic
- **HAQ**: Only displayed if the patient has a rheumatologist's confirmed diagnosis of inflammatory arthritis (including psoriatic arthritis). Source: Baseline clinical form and patient checklist.
- **CAGE**: Only displayed if the patient answered "Yes" to the current-follow-up drinking question (Lifestyle form at Baseline; Drinking section in FUP).
- Conditional checks are performed by the API; the client renders what it is told to.
- If a conditional form does not apply, it does NOT appear as "Skipped" — it is simply absent from the sequence.

#### FR-DASH-04: Form State Tracking
Each form submission record must carry a `FormStatus` field:
- `0` = Not Started
- `1` = Completed
- `2` = Skipped
- `3` = In Progress *(reserved; not persisted — see FR-FORM-03)*

#### FR-DASH-05: Next Visit Date
- Defaults to 6 months (for FU 1–6) or 12 months (for FU 7+) from last submission.
- Patient can update from the app.
- Clinician can override from the Clinician System.
- Stored as `NextVisitDate` on the patient's follow-up tracking record.

---

### 3.4 Form Submission

#### FR-FORM-01: Form Locking
- Once a form is submitted (`FormStatus = 1`) for a follow-up period, it cannot be re-submitted.
- The API must reject any re-submission for the same `PatientId` + `PappFupId` + `FormType`.

#### FR-FORM-02: No Partial Saves
- The API only accepts complete form submissions.
- No draft/partial records are stored in the main form tables.
- The mobile app may store partial state in a local session object but MUST NOT persist it to the API until the form is fully complete.

#### FR-FORM-03: Navigation-Away Warning
- Enforced by the UI (Blazor `NavigationManager` interception).
- If a user navigates away with unsaved form answers, a confirmation dialog is shown.
- Confirming resets the form's in-memory state.

#### FR-FORM-04: Form Skipping
- The API accepts a `SkipReason` (optional string) with a skip request.
- A skip records `FormStatus = 2` for the given `PatientId` + `PappFupId` + `FormType`.
- Skips require a separate API call (not bundled with form submission).

#### FR-FORM-05: Daily Submission Limit
- A patient can submit a given follow-up's form sequence only once per calendar day.
- The API enforces this by checking the `DateCompleted` field against UTC today.

---

### 3.5 Offline Support (Mobile)

#### FR-OFFLINE-01: Local SQLite Cache
- The MAUI app maintains a local SQLite database.
- Pending form submissions are stored in a `PendingSubmissions` table with columns: `LocalId`, `FormType`, `PappFupId`, `JsonPayload`, `CreatedAt`, `RetryCount`.

#### FR-OFFLINE-02: Sync on Reconnect
- A background service monitors network connectivity (`IConnectivity` in MAUI).
- On reconnect, pending submissions are sent to the API in sequence.
- Successful submissions are removed from the local SQLite table.
- Failed submissions increment `RetryCount`; after 5 failures, the user is notified.

#### FR-OFFLINE-03: Conflict Resolution
- If the API returns `409 Conflict` (duplicate submission), the local record is removed without error.

---

### 3.6 Notifications

#### FR-NOTIF-01: Email Notifications
- Sent via an integrated mail service (initially configurable SMTP; Phase 2: AWS SES).
- Templates stored in the database (managed by the Admin Communication Hub).
- Trigger events: Registration, Reminder (build-up), Day-Of, Overdue, Account Recovery.

#### FR-NOTIF-02: Push Notifications (Mobile)
- Delivered via a push notification service (e.g., Firebase Cloud Messaging / APNs).
- The MAUI app registers a push token on login; the token is stored against the patient record.
- Push triggers mirror email triggers.

#### FR-NOTIF-03: Notification Scheduling
- A background job evaluates `NextVisitDate` for all active patients daily.
- Configurable day-offsets define when build-up, day-of, and overdue reminders fire.
- Default schedule (configurable):
  - Build-up: 14 days before, 7 days before, 3 days before
  - Day-of: On the `NextVisitDate`
  - Overdue: +7 days, +14 days, +28 days after `NextVisitDate` (if not submitted)

#### FR-NOTIF-04: Patient Preferences
- `NotifyReminders` (bool): Toggle clinical form reminders.
- `NotifyInfoComms` (bool): Toggle informational/newsletter communications.
- These are stored on the patient record and checked before each notification is sent.

---

### 3.7 Security & Data Protection

#### FR-SEC-01: PII Encryption at Rest
The following fields must be AES-encrypted before database insertion and decrypted on read:
- `Patient.FirstName`
- `Patient.LastName` / Initials
- `Patient.NhsNumber`
- `Patient.ChiNumber`
- `Patient.BirthPlace`
- `Patient.Occupation`

The AES algorithm and key management will match the Clinician System's implementation (to be shared by the client). See `docs/architecture/DSD-001_Database_Schema_Design.md`.

#### FR-SEC-02: Deterministic Lookup for NHS Number
- To support future NHS Login matching (OQ-01 in URD-001), the `NhsNumber` column must support deterministic lookups.
- Strategy: Store a **HMAC-SHA256 hash** of the NHS Number alongside the AES-encrypted value. The hash is used for lookups; the encrypted value is used for display.

#### FR-SEC-03: Audit Logging
Every event in the following list must be written to a `SecurityAuditLog` table:
- User login (success + failure)
- Token refresh
- Account recovery / re-registration
- Token invalidation
- Registration (both pathways)
- Consent submission
- 14-day expiry deletion

Minimum audit record: `EventType`, `UserId`, `Timestamp` (UTC), `IPAddress`, `UserAgent`, `Details` (JSON).

#### FR-SEC-04: HTTPS Enforcement
- All API and web endpoints are HTTPS only.
- HSTS headers must be set.
- HTTP → HTTPS redirect enforced via `UseHttpsRedirection()`.

#### FR-SEC-05: CORS Policy
- The API's CORS policy (`AllowedOrigins` in config) must only allow the known web and mobile origins.
- Wildcard origins (`*`) are prohibited in production.

---

### 3.8 Data Retention & Deletion

#### FR-DATA-01: 14-Day Holding Account Deletion
- See FR-REG-05. Full cascade delete of holding account + associated form data.

#### FR-DATA-02: Patient Right to Erasure (GDPR)
- The system must support a full patient data deletion request.
- Deletion anonymises PII but retains anonymised research data (to be confirmed with DPO).
- Implementation deferred to future sprint; architecture must not block it.

---

## 4. Non-Functional Requirements

| ID | Requirement | Metric / Target |
|---|---|---|
| NFR-01 | API response time (95th percentile) | < 500 ms |
| NFR-02 | Dashboard load time on 4G | < 3 seconds |
| NFR-03 | System availability (production) | 99.5% uptime per month |
| NFR-04 | Maximum concurrent users (initial) | 500 patients |
| NFR-05 | Mobile app size (Android APK) | < 50 MB |
| NFR-06 | All logging is structured (Serilog or Microsoft.Extensions.Logging) | — |
| NFR-07 | No secrets in source code or appsettings (use environment variables / Key Vault) | — |
| NFR-08 | All database migrations are reversible | — |
| NFR-09 | Environment-agnostic config (IIS Phase 1 → Docker Phase 2 without code changes) | — |
| NFR-10 | Unit test coverage for scoring algorithms ≥ 90% | — |
| NFR-11 | WCAG 2.1 AA compliance for all patient-facing UI | — |

---

## 5. Interfaces

### 5.1 External Interfaces

| Interface | Direction | Protocol | Notes |
|---|---|---|---|
| SQL Server 2022 Database | API ↔ DB | TDS / EF Core | Existing DB; Database-First scaffolding |
| Email Service | API → External | SMTP (Phase 1) / AWS SES (Phase 2) | Configurable via `appsettings` |
| Push Notification Service | API → External | FCM / APNs | Token stored per device |
| NHS Login (Future) | Patient ↔ API | OIDC | Architecture must not block |

### 5.2 Internal Interfaces
- `BADBIR.Api` exposes RESTful JSON endpoints documented in `docs/api/API-001_Endpoint_Specification.md`.
- `BADBIR.UI.Components` communicates with the API via typed `HttpClient` wrappers injected through DI.
- `BADBIR.Web` and `BADBIR.Mobile` register shared services via `AddBADBIRUIComponents()`.

---

## 6. Requirements Traceability Matrix (Abbreviated)

| User Story | FR IDs | NFR IDs |
|---|---|---|
| US-01 QR Registration | FR-REG-02, FR-REG-04, FR-REG-05 | NFR-04 |
| US-02 Email Invite | FR-REG-03, FR-REG-04 | NFR-01 |
| US-04 Login | FR-AUTH-01, FR-AUTH-02, FR-AUTH-03 | NFR-01 |
| US-05 Biometric | FR-AUTH-04 | NFR-06 |
| US-06 Account Recovery | FR-AUTH-03, FR-REG-06, FR-SEC-03 | NFR-01 |
| US-07 Dashboard | FR-DASH-01, FR-DASH-02, FR-DASH-03 | NFR-02 |
| US-08–US-11 Forms | FR-FORM-01–05, FR-DASH-04 | NFR-10 |
| US-12 Offline | FR-OFFLINE-01, FR-OFFLINE-02, FR-OFFLINE-03 | NFR-05 |
| US-13–15 Notifications | FR-NOTIF-01–04 | NFR-03 |
| (all) Security | FR-SEC-01–05 | NFR-07, NFR-11 |
