# INT-001 – Clinician System Integration Requirements
## BADBIR Patient Application ↔ Clinician System

> **Document ID:** INT-001  
> **Version:** 0.2  
> **Status:** Updated — reflects Option B (separate Patient DB, API integration) decision  
> **Last Updated:** 2026-03-28  
> **Audience:** BADBIR development team (Patient App + Clinician System — same team)

> **Architecture Decision:** See ADR-017 for the full rationale. Option B (separate Patient DB + inter-system API) is the decided architecture. Both applications are developed by the same team. The Clinician System integration endpoints listed in this document are to be built alongside the Patient App.

---

## 1. Purpose & Scope

This document specifies the integration requirements for the **BADBIR Patient Application ↔ Clinician System** boundary. It defines the API contracts, data flows, consent workflow, QR code integration, and private endpoint security for both systems.

> **Why this document exists:** The Patient App and the Clinician System each own their own SQL Server database. They communicate exclusively via authenticated API calls — there is no shared database connection. This document is the integration contract. Both systems are developed by the same team, so both sides of every endpoint are specified here together.

---

## 2. Architecture Overview

### 2.1 System Boundary

```
+----------------------------+     HTTPS (private network)     +------------------------------------+
|   BADBIR Patient App       |<------------------------------>|   BADBIR Clinician System          |
|                            |                                 |                                    |
|  BADBIR.Api (.NET 10)      |  POST /internal/verify-identity|  BadbirWebApp (.NET 6/8 or later)  |
|  BADBIR.Web (Blazor)       |  GET  /internal/fup-schedule   |  Clinical data entry               |
|  BADBIR.Mobile (MAUI)      |  POST /internal/promote        |  Clinician dashboard               |
|                            |  POST/GET /internal/qr         |  Patient management                |
|  Patient DB (BadbirPatient)|                                 |  Consent approval inbox            |
|  - AspNetUsers (Identity)  |                                 |  Clinician DB (BADBIR)             |
|  - bbPappPatient* tables   |                                 |  - bbPatient, bbPatientCohort*     |
|  - QR code tokens          |                                 |  - bbPatientDLQI, HADS, HAQ etc.   |
|  - PendingClinicianActions |                                 |  - All lookup tables               |
+----------------------------+                                 +------------------------------------+
```

### 2.2 Integration Approach

**There is no shared database connection.** Each application owns its own SQL Server database. They communicate via mutually-authenticated HTTPS API calls on a private network (Docker internal network on-premise; VPN/mTLS in cloud deployment).

The **Patient App** (owns `BadbirPatient` DB):
1. Writes patient-submitted forms to its own holding tables (`bbPappPatient*`)
2. Calls the Clinician System to **verify patient identity** during registration
3. Calls the Clinician System to **retrieve follow-up schedules**
4. Exposes **promotion endpoints** for the Clinician System to call after approving a patient

The **Clinician System** (owns `BADBIR` DB):
1. Maintains the **patient inbox** — holding-state patients awaiting clinician review
2. Calls the Patient App to **promote** approved data to the live Clinician DB
3. Calls the Patient App to **reject** a patient registration with a reason code
4. **Generates and validates QR codes** for clinic use
5. Manages the follow-up schedule and assigns `FupId` values

---

## 2a. Clinician System API Endpoints (New — to be built)

These endpoints are implemented in the **Clinician System** and called by the **Patient App**. All endpoints are on the private internal route and require a service-account JWT.

### 2a.1 Patient Identity Verification

Replaces the legacy stored procedure with elevated DB permissions.

```
POST /api/internal/patients/verify-identity
Authorization: Bearer <patient-app-service-token>
Content-Type: application/json

{
  "encryptedSurname": "<AES-CBC Base64>",
  "encryptedForenames": "<AES-CBC Base64>",
  "dateOfBirth": "1985-04-12",
  "encryptedNhsNumber": "<AES-CBC Base64>",    // pnhs
  "encryptedChiNumber": "<AES-CBC Base64>"     // phrn — null if not applicable
}

Response 200 OK (match found):
{
  "matched": true,
  "patientId": 12345,
  "chid": 67890,
  "statusId": 6,
  "statusText": "Registered awaiting consent form"
}

Response 404 Not Found (no match):
{
  "matched": false
}
```

> **Security note:** The Clinician System performs the decryption internally. It NEVER returns PII fields back to the Patient App. Only `patientId`, `chid`, and status are returned.

### 2a.2 Follow-Up Schedule

```
GET /api/internal/patients/{chid}/fup-schedule
Authorization: Bearer <patient-app-service-token>

Response 200 OK:
{
  "chid": 67890,
  "followUps": [
    {
      "fupId": 1,
      "fupNumber": 0,
      "dueDate": "2024-01-10",
      "editWindowFrom": "2023-12-29",
      "editWindowTo": "2024-02-21",
      "fupStatus": 0,
      "formsRequired": ["lifestyle", "dlqi", "euroqol", "hads", "haq", "sapasi", "pga", "cage"]
    },
    {
      "fupId": 2,
      "fupNumber": 1,
      "dueDate": "2024-07-10",
      "editWindowFrom": "2024-06-29",
      "editWindowTo": "2024-08-21",
      "fupStatus": 0,
      "formsRequired": ["pga", "dlqi", "euroqol", "hads", "haq", "sapasi", "cage"]
    }
  ]
}
```

### 2a.3 QR Code — Clinician Generates for Patient

```
POST /api/internal/patients/{patientId}/portal-qr
Authorization: Bearer <patient-app-service-token>

Response 200 OK:
{
  "qrToken": "<signed JWT>",
  "qrUrl": "https://patient.badbir.org/qr/<token>",
  "expiresAt": "2024-01-11T11:00:00Z"
}
```

The Patient App (not the Clinician System) generates this token — the Clinician System calls the Patient App to get it. See Section 2b for the full QR flow.

---

## 2b. Patient App API Endpoints (New — inter-service, private)

These endpoints are implemented in the **Patient App** and called by the **Clinician System**. All require service-account JWT.

### 2b.1 Promote Patient Data

```
POST /api/internal/patients/{chid}/promote
Authorization: Bearer <clinician-system-service-token>
Content-Type: application/json

{
  "clinicianId": 42,
  "cohort": "Biologic",
  "drugId": 15,
  "drugStartDate": "2024-01-10",
  "consentDate": "2024-01-10",
  "consentType": "InPerson",
  "pasi": 14.2,
  "dlqi": 15,
  "fupId": 1
}

Response 200 OK:
{
  "chid": 67890,
  "promotedAt": "2024-01-10T11:30:00Z",
  "formsPromoted": ["lifestyle", "dlqi", "euroqol", "hads", "haq", "sapasi", "pga", "cage"],
  "patientStatus": "Active"
}
```

### 2b.2 Reject Patient Registration

```
POST /api/internal/patients/{chid}/reject
Authorization: Bearer <clinician-system-service-token>
Content-Type: application/json

{
  "clinicianId": 42,
  "reasonCode": "NotEligible",
  "reasonText": "Psoriasis diagnosis not confirmed"
}

Response 200 OK:
{
  "chid": 67890,
  "rejectedAt": "2024-01-10T11:30:00Z",
  "patientNotified": true
}
```

### 2b.3 Get Pending Registrations (for Clinician System inbox)

```
GET /api/internal/registrations/pending?clinicId={clinicId}
Authorization: Bearer <clinician-system-service-token>

Response 200 OK:
{
  "pending": [
    {
      "chid": 67890,
      "submittedAt": "2024-01-08T09:00:00Z",
      "expiresAt": "2024-01-22T09:00:00Z",
      "daysRemaining": 14,
      "formsSubmitted": ["lifestyle", "dlqi", "euroqol"],
      "formsOutstanding": ["hads", "haq", "sapasi", "pga"],
      "clinicId": 5
    }
  ]
}
```

### 2b.4 Generate Portal QR Code (for Clinician to Show Patient)

```
POST /api/internal/qr/generate
Authorization: Bearer <clinician-system-service-token>
Content-Type: application/json

{
  "patientId": 12345,
  "purpose": "registration",    // or "link-account"
  "expiryHours": 24
}

Response 200 OK:
{
  "token": "<signed JWT>",
  "qrUrl": "https://patient.badbir.org/qr/<token>",
  "expiresAt": "2024-01-11T11:00:00Z"
}
```

### 2b.5 Validate Patient-Presented QR Code (Patient shows phone to clinician)

```
POST /api/internal/qr/validate
Authorization: Bearer <clinician-system-service-token>
Content-Type: application/json

{
  "token": "<signed JWT from patient's screen>"
}

Response 200 OK:
{
  "valid": true,
  "patientUserId": "abc123",
  "displayStudyNumber": "B00123",
  "expiresAt": "2024-01-10T11:10:00Z"
}

Response 400 Bad Request:
{
  "valid": false,
  "reason": "Expired"   // or "AlreadyUsed", "InvalidSignature"
}
```

---

## 3. Patient Registration & Consent Flow

### 3.1 Pathway A — Patient Self-Registration (Primary)

The patient registers via the Patient App and enters a **holding state** pending clinician confirmation.

```
PATIENT APP                    SHARED DB                    CLINICIAN SYSTEM
     │                             │                               │
     │  Patient completes          │                               │
     │  eligibility screener       │                               │
     │  + identity verification    │                               │
     │  + account creation         │                               │
     │                             │                               │
     │──── INSERT PatientHolding ──►                               │
     │      + baseline forms ─────►│                               │
     │      (bb_papp_* tables)     │                               │
     │                             │                               │
     │                             │   ◄──── Clinician logs in     │
     │                             │         and sees INBOX        │
     │                             │         (new registrations    │
     │                             │          awaiting review)     │
     │                             │                               │
     │  Patient waits              │   Clinician opens record:     │
     │  (holding state)            │   • Verifies identity (DOB,   │
     │                             │     initials, NHS/CHI)        │
     │                             │   • Confirms diagnosis        │
     │                             │   • Assigns drug & cohort     │
     │                             │   • Confirms consent given    │
     │                             │     in person                 │
     │                             │                               │
     │                             │──── PROMOTE: copies papp_* ──►│
     │                             │      rows to live tables      │
     │                             │      sets patient status      │
     │                             │      = Active                 │
     │                             │                               │
     │◄─── Status update ──────────│                               │
     │     "Account confirmed"     │                               │
     │                             │                               │
     │  Patient app unlocked:      │                               │
     │  can now see dashboard      │                               │
     │  and follow-up schedule     │                               │
```

### 3.2 Pathway B — Clinician-Registered Patient

For patients who cannot self-register (e.g., elderly, no smartphone, under 16 with guardian), the clinician creates the record directly in the Clinician System. This pathway bypasses the Patient App holding tables entirely.

The Patient App should allow these patients to log in and see their follow-up schedule without going through the holding state.

### 3.3 The 14-Day Confirmation Window

| State | Trigger | Timeout |
|---|---|---|
| `Holding` | Patient self-registration submitted | — |
| `Holding - Email Verified` | Patient clicks email link | — |
| `Holding - Forms Submitted` | Patient completes baseline forms | 14-day clock starts |
| `Active` | Clinician confirms in Clinician System | — |
| `Expired` | 14-day window passes without clinician confirmation | Patient record deleted from holding tables |

> **Important:** The 14-day window starts when the patient submits their baseline questionnaires, not when they register. This ensures the patient has a complete record for the clinician to review.

### 3.4 What the Clinician Must Confirm Before Promotion

Before promoting a patient from holding to active, the clinician must confirm all of the following in the Clinician System:

| # | Required Confirmation | Notes |
|---|---|---|
| C-01 | Patient identity verified (DOB, name, NHS/CHI matches) | Checked against patient demographic records |
| C-02 | Psoriasis diagnosis confirmed | Dermatologist diagnosis required |
| C-03 | Treatment started within 6 months of consent date | The 6-month rule |
| C-04 | Drug name and cohort assigned | Biologic / Small Molecule / Conventional |
| C-05 | Patient is biologic-naïve (if Conventional or Small Molecule cohort) | Per eligibility rules |
| C-06 | PASI and DLQI thresholds met (if Conventional cohort only) | PASI ≥ 10, DLQI ≥ 11 |
| C-07 | In-person consent taken and signed | Paper consent form filed, or scanned to record |
| C-08 | GP notification sent | Informing GP of trial participation |

---

## 4. Clinician Consent Inbox

### 4.1 Requirements for the Clinician System

The Clinician System must implement an **inbox / task list** for each clinician/nurse showing patients awaiting confirmation. This must include:

| # | Requirement | Details |
|---|---|---|
| CI-01 | Inbox view | List of patients in holding state assigned to this clinic |
| CI-02 | New registration notification | Email or in-app notification when a new patient registers from their clinic |
| CI-03 | Days-remaining indicator | Show how many days remain before the 14-day window expires |
| CI-04 | Patient detail view | Show submitted baseline form data for review |
| CI-05 | Consent confirmation action | Button/form to mark consent as confirmed in person |
| CI-06 | Drug assignment | Select drug from lookup table (`bb_drug` or equivalent) |
| CI-07 | Cohort assignment | Assign to Biologic / Small Molecule / Conventional |
| CI-08 | PASI / DLQI recording | Required at baseline by clinician (separate from patient-reported SAPASI) |
| CI-09 | Reject / decline action | Reject with reason (e.g., "not eligible", "patient withdrew") |
| CI-10 | Expired record cleanup | Automatically flag and optionally delete expired holding records |

### 4.2 Notification Trigger

When a patient completes their baseline forms, the Patient API:
1. Sets the internal `PatientHolding.FormStatus = FormsComplete`
2. Inserts a row into the `PendingClinicianActions` table in the Patient DB
3. Sends a notification email to the clinician team contact for this patient's clinic
4. **Calls a Clinician System webhook endpoint** (`POST /api/internal/webhooks/patient-registration-ready`) to push a notification in real time

The Clinician System endpoint for receiving this notification:
```
POST /api/internal/webhooks/patient-registration-ready
Authorization: Bearer <patient-app-service-token>

{
  "chid": 67890,
  "submittedAt": "2024-01-08T09:00:00Z",
  "expiresAt": "2024-01-22T09:00:00Z",
  "clinicId": 5,
  "formsSubmitted": ["lifestyle", "dlqi", "euroqol", "hads", "haq", "sapasi", "pga", "cage"]
}

Response 202 Accepted
```

If the Clinician System is unavailable, the Patient App retries with exponential backoff. The Clinician System can also poll `GET /api/internal/registrations/pending` as a fallback (see Section 2b.3).

---

## 5. Data Promotion Mechanism (Holding → Live Tables)

### 5.1 Legacy Approach (Current — to be retired)

The legacy system used a **5-minute SQL Agent scheduled job** that scanned the `bb_papp_*` holding tables and copied confirmed records into the live tables. 

**Problems with this approach:**
- Up to 5 minutes latency between clinician confirmation and data being "live"
- No error handling or retry mechanism
- Silent failures — the job could fail without any notification
- Difficult to audit which rows were promoted when and by whom
- Cannot be tested without a running SQL Agent

### 5.2 Decided Approach — API-Triggered Promotion

See **ADR-017** for the full architectural decision. The SQL Agent job is replaced by a direct API call. When the clinician clicks "Confirm Patient" in the Clinician System, it calls the Patient App's internal promote endpoint:

```
POST /api/internal/patients/{chid}/promote
Authorization: Bearer <clinician-system-service-token>

{
  "clinicianId": 42,
  "cohort": "Biologic",
  "drugId": 15,
  "drugStartDate": "2024-01-10",
  "consentDate": "2024-01-10",
  "consentType": "InPerson",
  "pasi": 14.2,
  "dlqi": 15
}
```

Response `200 OK`:
```json
{
  "chid": 12345,
  "promotedAt": "2024-01-10T11:30:00Z",
  "formsPromoted": ["lifestyle", "dlqi", "euroqol", "hads", "haq", "sapasi", "pga", "cage"],
  "patientStatus": "Active"
}
```

This call:
1. Validates the promotion data
2. Copies `bb_papp_*` rows to live tables in a **single database transaction**
3. Updates patient status to `Active`
4. Creates a `PromotionAuditLog` record
5. Sends confirmation notification to the patient via the push notification / email service
6. Returns success or a structured error

**Note on DataStatus columns:** The papp holding tables already have a `DataStatus` column (`0=Holding, 1=Approved, 2=Rejected`). These tables live in the Patient DB. On promotion, form data is pushed to the corresponding live table in the Clinician DB via API. The holding table row's DataStatus is then updated to `1=Approved`. This gives a complete audit trail in the Patient DB of what was submitted and when it was accepted.

### 5.3 PendingClinicianActions (Patient DB — new)

This table lives in the Patient DB (`BadbirPatient`), not in the Clinician DB. It tracks which actions are pending and is read by the Clinician System via the `GET /api/internal/registrations/pending` endpoint. The Patient App owns the schema.

```sql
CREATE TABLE [PendingClinicianActions] (
    [ActionId]       INT IDENTITY(1,1) NOT NULL,
    [Chid]           INT NOT NULL,
    [ActionType]     NVARCHAR(50) NOT NULL,   -- 'PatientRegistration', 'ConsentRequired', 'FormReview'
    [CreatedAt]      DATETIME2 NOT NULL,
    [DueAt]          DATETIME2 NOT NULL,       -- CreatedAt + 14 days for registration
    [AssignedClinic] INT NULL,                 -- FK to clinic lookup
    [Status]         NVARCHAR(20) NOT NULL DEFAULT 'Pending', -- Pending, Completed, Expired, Rejected
    [CompletedAt]    DATETIME2 NULL,
    [CompletedBy]    INT NULL,                 -- Clinician user ID
    CONSTRAINT [PK_PendingClinicianActions] PRIMARY KEY ([ActionId])
);
```

---

## 6. Follow-Up Scheduling

### 6.1 Responsibility Split

| Responsibility | Owner | Mechanism |
|---|---|---|
| Create `BbPappPatientCohortTracking` record (assign `papp_fup_id`) | Clinician System | At patient confirmation / follow-up scheduling |
| Notify patient that follow-up is due | Patient API | Scheduled notification service |
| Present follow-up form sequence | Patient App | Based on `papp_fup_id` from `BbPappPatientCohortTracking` |
| Mark follow-up as complete | Patient API | After all mandatory forms submitted |

### 6.2 Follow-Up Schedule (Standard)

| Visit Number | Month | Notes |
|---|---|---|
| Baseline (FUP 0) | Month 0 | At consent |
| FUP 1 | Month 6 | ± 6 weeks window |
| FUP 2 | Month 12 | ± 6 weeks window |
| FUP 3 | Month 18 | ± 6 weeks window |
| FUP 4 | Month 24 | ± 6 weeks window |
| FUP 5 | Month 36 | Long-term cohort only |
| FUP 6 | Month 48 | Long-term cohort only |

### 6.3 Follow-Up API (Required from Patient App)

The Patient App exposes the following read endpoint for the Clinician System dashboard to display follow-up completion status:

```
GET /api/patients/{chid}/followups
Authorization: Bearer <clinician-system-service-token>

Response 200:
[
  { "pappFupId": 1, "fupNumber": 0, "fupDate": "2024-01-10", "status": "Complete", "completedAt": "2024-01-15" },
  { "pappFupId": 2, "fupNumber": 1, "fupDate": "2024-07-10", "status": "Pending", "completedAt": null }
]
```

---

## 7. Shared Lookup Tables

The following tables are managed by the Clinician System. Under Option B, the Patient App no longer has a direct DB connection to the Clinician DB — lookup data is either:
1. **Exposed as read-only API endpoints** by the Clinician System (e.g., `GET /api/internal/lookups/drugs`), OR
2. **Cached as a local copy** in the Patient DB, refreshed periodically on a schedule

The Clinician System must provide these lookup values before the Patient App goes live.

| Table | Patient App usage |
|---|---|
| `bb_drug` / drug lookup | Display drug names, validate drug IDs |
| `bb_workstatus` / work status lookup | Populate dropdown in demographics form |
| `bb_patientstatus` | Display patient status labels |
| `bb_clinic` / clinic lookup | Associate patients with clinics |
| `bb_diagnosis` | Confirm diagnosis types (psoriasis, PsA) |
| `bb_ethnicity` | Dropdown options for ethnicity field |
| `bb_cohort` | Biologic / Small Molecule / Conventional labels |

> **Requirement for Clinician System team:** Provide a data dump of all lookup table contents as part of the synthetic SQLite test database (pending delivery).

---

## 8. Authentication & Service Account

### 8.1 Service-to-Service Authentication

All inter-system API calls use a dedicated service-account JWT, not patient-facing tokens:

| Property | Value |
|---|---|
| Token type | JWT Bearer |
| Scope / claim | `role: InternalService` (or `role: ClinicianSystem` / `role: PatientApp` to distinguish direction) |
| Token lifetime | 24 hours (non-interactive) |
| Token endpoint | `POST /api/auth/service-token` (on each system's API) |
| Secret | Shared symmetric key stored in environment variables / Docker secrets — never in source code |

> **Security note:** Service tokens are never issued to patient-facing clients. They carry separate audit trail entries. Patient-facing endpoints (e.g. `/api/forms/*`, `/api/auth/*`) always reject `InternalService` tokens — and vice versa.

### 8.2 Private Internal Route Prefix

All inter-system endpoints are prefixed `/api/internal/`. These routes:
- Require `role: InternalService` claim — return `403` to any patient-facing or public token
- Are not documented in the public OpenAPI spec
- Should be excluded from public-facing rate limiting middleware
- In IIS, can be additionally protected by URL rewrite rules that block external IPs

### 8.3 IP Allowlisting (Defence in Depth)

In Phase 1 (IIS on-premise), the `/api/internal/*` routes are additionally protected at the IIS/firewall level — only the Clinician System server's IP is permitted to reach these routes on the Patient App, and vice versa. This is documented in SED-001.

### 8.4 Docker Internal Network (Phase 2)

In Docker deployment, both services are placed on a shared `internal` Docker network with `internal: true` (no external routing). The `/api/internal/*` routes are only reachable within this network. The public Docker port mappings expose only the patient-facing and public API routes.

```yaml
networks:
  internal:
    internal: true   # not routable from outside Docker
  public:
    driver: bridge

services:
  badbir-patient-api:
    networks: [internal, public]
  badbir-clinician-api:
    networks: [internal, public]
```

### 8.5 Shared API Key Header (Simple Fallback)

During early development, before full service-account JWT infrastructure is wired up, a shared API key in a custom header is acceptable:

```
X-Internal-Api-Key: <value from environment variable>
```

Both sides validate this header for `/api/internal/*` routes. The key is stored in environment variables. This must be replaced by JWT service accounts before staging deployment.

---

## 9. Encryption Interoperability

Both systems must use the **same encryption algorithm and key** to be able to read each other's encrypted PII fields. See ADR-015 for the confirmed algorithm specification.

**Summary:**
- Algorithm: AES (Rijndael) with `PasswordDeriveBytes` (PBKDF1)  
- Text encoding: Unicode (UTF-16 LE)  
- Output format: Base64 string  
- Key derivation: `PasswordDeriveBytes(password, salt)` where `salt = ASCII(password.Length.ToString())`  
- Key bytes: `.GetBytes(32)` for the AES key; `.GetBytes(16)` for the IV  

Both systems must share the **same password** stored in their respective configuration stores. This password must never be committed to source control.

Encrypted fields in shared tables:
- `bb_patient`: `title`, `forenames`, `surname`, `countryresidence`, `phrn` (CHI/H&C number), `pnhs` (NHS number)  
- `bb_papp_patient_lifestyle`: `birthtown`, `birthcountry`

---

## 9a. QR Code Integration

QR codes are a first-class feature relied on by both systems. They eliminate manual entry errors during patient registration and speed up clinic workflows.

### 9a.1 Clinician-Generated QR (most common flow)

1. Clinician navigates to a patient record in the Clinician System and clicks **"Generate Patient Portal QR"**
2. The Clinician System calls `POST /api/internal/qr/generate` on the Patient App (see Section 2b.4)
3. The Patient App returns a **signed, time-limited URL** (`https://patient.badbir.org/qr/{token}`)
4. The Clinician System displays this as a QR code on screen (or prints it)
5. The patient scans the QR code with the Patient App (or browser)
6. The Patient App verifies the token signature, extracts the `patientId` and `purpose`, and navigates the patient to the appropriate flow (registration, link-account, etc.)
7. The token is single-use and expires after 24 hours

This flow supports both main registration pathways:
- **Pathway A (self-registration):** Patient scans the QR code at clinic after completing the eligibility screener. The QR pre-fills their study ID / clinic and skips manual data entry.
- **Pathway B (clinician-registered):** Clinician registers the patient first; generates a QR so the existing patient can link their mobile app account to the record.

### 9a.2 Patient QR → Clinician Scans (identity at a glance)

1. A logged-in patient opens the Patient App → "Show My QR"
2. The Patient App generates a short-lived signed token (10 minutes) and displays it as QR
3. The clinician scans the QR with the Clinician System (or a camera that opens the Clinician System link)
4. The Clinician System calls `POST /api/internal/qr/validate` on the Patient App (see Section 2b.5)
5. The Patient App returns `{ valid: true, displayStudyNumber: "B00123" }`
6. The Clinician System immediately opens the matching patient record

### 9a.3 QR Token Security

| Property | Value |
|---|---|
| Format | Signed JWT with `purpose` claim |
| Signature | HMAC-SHA256 using the service-to-service shared key |
| Patient QR expiry | 10 minutes (display only) |
| Clinician-generated QR expiry | 24 hours |
| Registration QRs | Single-use (redeemed only once; second use returns 400) |
| QR content | URL: `https://patient.badbir.org/qr/{token}` (works with any camera app) |

---

## 10. Clinician System Requirements Summary

This section lists the formal integration requirements that the Clinician System team must implement:

| ID | Requirement | Priority | Notes |
|---|---|---|---|
| INT-01 | Patient registration inbox showing holding-state patients (polls `GET /api/internal/registrations/pending` or receives webhook) | Must Have | New feature |
| INT-02 | New registration notification — receive `POST /api/internal/webhooks/patient-registration-ready` from Patient App | Must Have | Real-time push |
| INT-03 | 14-day countdown display per pending patient | Must Have | Legal obligation |
| INT-04 | Patient detail view showing submitted baseline forms (fetched from Patient App via API) | Must Have | For review before confirmation |
| INT-05 | Consent confirmation workflow with mandatory fields (C-01 to C-08) | Must Have | Gate for promotion |
| INT-06 | Call `POST /api/internal/patients/{chid}/promote` on Patient App on confirmation | Must Have | Replaces SQL Agent job |
| INT-07 | Drug and cohort assignment at confirmation | Must Have | Feeds live Clinician DB table |
| INT-08 | Clinician PASI + DLQI recording at baseline (separate from patient SAPASI) | Must Have | Separate columns |
| INT-09 | Expired record handling (14-day expiry) — call `POST /api/internal/patients/{chid}/reject` with reason `Expired` | Must Have | GDPR data minimisation |
| INT-10 | Reject workflow — call `POST /api/internal/patients/{chid}/reject` with reason code | Must Have | Patient notified on rejection |
| INT-11 | Expose `POST /api/internal/patients/verify-identity` endpoint | Must Have | Replaces stored proc with elevated DB permissions |
| INT-12 | Expose `GET /api/internal/patients/{chid}/fup-schedule` endpoint | Must Have | Patient App reads follow-up schedule |
| INT-13 | Follow-up scheduling — write `BbPatientCohortTracking` entries in Clinician DB; notify Patient App | Must Have | Existing responsibility |
| INT-14 | Shared lookup table maintenance (drugs, workstatus, etc.) | Must Have | Pre-existing responsibility |
| INT-15 | Service-account JWT for all `/api/internal/*` calls | Must Have | Security boundary |
| INT-16 | QR code generation — call `POST /api/internal/qr/generate` on Patient App | Must Have | Both apps use QR codes |
| INT-17 | QR code validation — call `POST /api/internal/qr/validate` on Patient App when scanning patient phone | Must Have | Both apps use QR codes |
| INT-18 | Expose webhook `POST /api/internal/webhooks/patient-registration-ready` to receive notifications | Must Have | Push notification from Patient App |
| INT-19 | NHS Login integration (Phase 2 only) | Could Have | Long-term |

---

## 11. Open Integration Questions

| # | Question | Impact | Status |
|---|---|---|---|
| IQ-01 | Will the Clinician System call the Patient API, or will the Patient API webhook the Clinician System? | Architecture decision | ✅ **Both.** Patient App webhooks Clinician System when registration is ready. Clinician System calls Patient App to promote/reject. |
| IQ-02 | Which team owns `PendingClinicianActions` — schema creation and maintenance? | Shared schema governance | ✅ **Patient App owns it** — it lives in the Patient DB (`BadbirPatient`). Same team, no conflict. |
| IQ-03 | How will service account credentials be provisioned and rotated? | Security operations | Open — recommendation: environment variables / Docker secrets in v1; vault rotation in v2. |
| IQ-04 | Is there an existing clinic assignment mechanism in the Clinician System for self-registered patients? | Registration UX | Open — the Patient App's registration step should capture clinic ID or let the clinician assign it during the inbox review. |
| IQ-05 | Should patients be able to see their own submitted form responses in the Patient App? | UX requirement | Open — suggested: yes, read-only view of own submissions. Helps patients track progress. |
| IQ-06 | How does the Clinician System handle patients who change clinics between follow-ups? | Data integrity | Open — existing Clinician System logic governs this; Patient App follows the `centreidcurrent` from `BbPatientCohortTracking`. |
| IQ-07 | What is the notification channel preference — email vs. in-app push? | Infrastructure dependency | Open — v1: email. v2: push notifications via APNs/FCM. Both are planned. |
