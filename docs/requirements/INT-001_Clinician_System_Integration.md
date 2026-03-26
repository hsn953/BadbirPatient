# INT-001 – Clinician System Integration Requirements
## BADBIR Patient Application ↔ Clinician System

> **Document ID:** INT-001  
> **Version:** 0.1 (Draft)  
> **Status:** In Review  
> **Last Updated:** 2026-03-26  
> **Audience:** BADBIR Clinician System development team AND Patient App development team  

---

## 1. Purpose & Scope

This document specifies the integration requirements that the **BADBIR Clinician System** must implement to work seamlessly with the new **BADBIR Patient Application**. It defines the API contracts, data flows, consent workflow, and shared database conventions that both systems must agree on.

> **Why this document exists:** The two systems share a single SQL Server database but are developed and deployed independently. This document is the integration contract between the two teams. Any changes to the shared schema or API surface defined here require agreement from both teams before implementation.

---

## 2. Architecture Overview

### 2.1 System Boundary

```
┌──────────────────────────┐         Shared SQL Server DB         ┌────────────────────────────────────┐
│   BADBIR Patient App     │ ──────────────────────────────────── │   BADBIR Clinician System          │
│                          │                                       │                                    │
│  BADBIR.Api (.NET 10)    │          papp_* tables               │  BadbirWebApp (.NET 6/8)            │
│  BADBIR.Web (Blazor)     │ ─── writes patient form data ──────► │  Clinical data entry               │
│  BADBIR.Mobile (MAUI)    │                                       │  Clinician dashboard               │
│                          │◄── reads lookup tables (drugs, ────── │  Patient management                │
│                          │    workstatus, patientstatus etc.)    │  Consent approval inbox            │
└──────────────────────────┘                                       └────────────────────────────────────┘
```

### 2.2 Integration Approach

Both systems share the same SQL Server database. The Patient App writes to **holding tables** (prefixed `bb_papp_`). The Clinician System:
1. Reads patient registrations from the holding area
2. Confirms patient eligibility, diagnosis, and drug assignment
3. Triggers data promotion from holding tables to live tables
4. Manages the follow-up schedule (assigning `papp_fup_id` values)

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

When a patient completes their baseline forms, the Patient API must:
1. Set `PatientHolding.FormStatus = FormsComplete`
2. Insert a row into `PendingClinicianActions` (new table — see §7)
3. Send a notification email to the clinician team contact for this patient's clinic

The Clinician System must then pick up this notification via one of:
- **Option A (Recommended):** Polling the `PendingClinicianActions` table, or
- **Option B:** Receiving a webhook POST from the Patient API to a Clinician System endpoint

See ADR-014 for the data promotion mechanism recommendation.

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

### 5.2 Recommended New Approach

See **ADR-014** in the DECISION-LOG for the full decision record. The recommended approach is:

**Option A (v1) — API-Triggered Promotion:** Replace the scheduled job with a direct API call. When the clinician clicks "Confirm Patient" in the Clinician System, it calls the Patient API:

```
POST /api/admin/patients/{chid}/promote
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

**Option B (v2 — recommended long-term):** Remove the physical separation between holding and live tables. Add a `DataStatus` column to each form table: `0 = Pending, 1 = Active, 2 = Rejected`. The Patient App writes with `DataStatus = 0`. The clinician approval action updates to `DataStatus = 1`. No data copying required.

### 5.3 Shared Table: PendingClinicianActions (New)

A new table is required to track which clinician actions are pending:

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

The following tables are managed by the Clinician System and read (not written to) by the Patient App. They must be populated before the Patient App goes live.

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

API calls between the Clinician System and Patient API use a dedicated service account JWT token:

| Property | Value |
|---|---|
| Token type | JWT Bearer |
| Scope / claim | `role: ClinicianSystem` |
| Token lifetime | 24 hours (non-interactive service) |
| Token endpoint | `POST /api/auth/service-token` |
| Secret | Shared symmetric key stored in environment variables |

> **Security note:** The Clinician System must NOT use patient-facing JWT tokens for service-to-service calls. A dedicated service account with the `ClinicianSystem` role bypasses patient-facing rate limits and uses a separate audit trail.

### 8.2 IP Allowlisting

In Phase 1 (IIS), the `/api/admin/*` and `/api/service/*` endpoints are additionally protected by IP allowlisting at the IIS/firewall level — only the Clinician System server's IP is permitted. This is documented in SED-001.

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

## 10. Clinician System Requirements Summary

This section lists the formal integration requirements that the Clinician System team must implement:

| ID | Requirement | Priority | Notes |
|---|---|---|---|
| INT-01 | Patient registration inbox showing holding-state patients | Must Have | New feature |
| INT-02 | New registration notification (email or in-system) | Must Have | Triggered by Patient API |
| INT-03 | 14-day countdown display per pending patient | Must Have | Legal obligation |
| INT-04 | Patient detail view showing submitted baseline forms | Must Have | For review before confirmation |
| INT-05 | Consent confirmation workflow with mandatory fields (C-01 to C-08) | Must Have | Gate for promotion |
| INT-06 | `POST /api/admin/patients/{chid}/promote` call on confirmation | Must Have | Replaces SQL Agent job |
| INT-07 | Drug and cohort assignment at confirmation | Must Have | Feeds live table |
| INT-08 | Clinician PASI + DLQI recording at baseline (separate from patient SAPASI) | Must Have | Separate columns |
| INT-09 | Expired record handling (14-day expiry) | Must Have | GDPR data minimisation |
| INT-10 | Reject workflow with reason code | Must Have | Patient notified on rejection |
| INT-11 | Populate `PendingClinicianActions` table entries | Must Have | Shared coordination table |
| INT-12 | Read-only view of patient follow-up history from Patient App | Should Have | Via `GET /api/patients/{chid}/followups` |
| INT-13 | Follow-up scheduling (`BbPappPatientCohortTracking` inserts) | Must Have | Existing responsibility |
| INT-14 | Shared lookup table maintenance (drugs, workstatus, etc.) | Must Have | Pre-existing responsibility |
| INT-15 | Service account JWT for API calls | Must Have | Security boundary |
| INT-16 | NHS Login integration (Phase 2 only) | Could Have | Long-term |

---

## 11. Open Integration Questions

| # | Question | Impact |
|---|---|---|
| IQ-01 | Will the Clinician System call the Patient API, or will the Patient API webhook the Clinician System? | Architecture decision |
| IQ-02 | Which team owns `PendingClinicianActions` — schema creation and maintenance? | Shared schema governance |
| IQ-03 | How will service account credentials be provisioned and rotated? | Security operations |
| IQ-04 | Is there an existing clinic assignment mechanism in the Clinician System for self-registered patients? | Registration UX |
| IQ-05 | Should patients be able to see their own submitted form responses in the Patient App? | UX requirement |
| IQ-06 | How does the Clinician System handle patients who change clinics between follow-ups? | Data integrity |
| IQ-07 | What is the notification channel preference — email vs. in-app push? | Infrastructure dependency |
