# FORM-006 – PGA Form Specification
## Patient's Global Assessment
## BADBIR Patient Application

> **Document ID:** FORM-006  
> **Version:** 0.2  
> **Status:** Updated — confirmed from actual paper forms (Baseline p.6 bottom, FUP p.6 top)  
> **Last Updated:** 2026-03-26

---

## 1. Overview

| Attribute | Value |
|---|---|
| Form Name | Patient's Global Assessment (PGA) |
| Legacy API endpoint | `POST /Dashboard/SavePGAScore` |
| Legacy DTO | `PatientPgascore` |
| Conditional | No — always shown at Baseline and every Follow-up |
| Estimated Duration | < 1 minute |
| Form appears at | Baseline: bottom of Demographics page (p.6); FUP: top of Follow-Up page (p.6) |

---

## 2. ⚠️ IMPORTANT: Scale Correction

The legacy DTO stored `pgascore` as `INT?` (previously assumed to be 0–10). The **actual paper form** uses a **5-level categorical scale**, not a numeric 0–10 scale.

**Exact question text (from paper):**  
*"How would you currently rate your psoriasis? Please choose one."*

| Option | Stored integer value |
|---|---|
| Severe | 5 |
| Moderate | 4 |
| Mild | 3 |
| Almost clear | 2 |
| Clear | 1 |

> **Note:** The legacy integer mapping (1–5) is inferred from clinical convention (lower = better). This must be confirmed against the actual DB DDL (DB-01). Do not assume 0-indexed.

### 2.1 Difference from Clinician PGA

The clinician-completed PGA (on the baseline clinical form, p.2) has **6 levels**: Severe / Moderate to severe / Moderate / Mild / Almost clear / Clear.

The **patient PGA has only 5 levels** (no "Moderate to severe"). These are separate data items stored in separate columns.

---

## 3. Placement in Form Sequence

- **Baseline:** PGA is at the bottom of the Patient Baseline Questionnaire (demographics page). It is collected as part of the Lifestyle/Demographics form, not as a separate form. 
  - Design choice: Treat PGA as a standalone micro-form that immediately follows demographics submission, OR include it as the last question on the Lifestyle/Demographics form.
- **FUP:** PGA is the *first item* on the Follow-Up patient form (FUP p.6), before Medical Problems.
  - In the app, PGA is treated as a standalone form presented first in the FUP sequence.

---

## 4. Legacy Data Contract

```json
{
  "chid": 12345,
  "pappFupId": 67,
  "pgascore": 3,
  "createdbyname": "Patient via API",
  "formStatus": 1
}
```

---

## 5. API Contract

**POST** `/api/forms/pga`

```json
{
  "pappFupId": 67,
  "pgaScore": 3
}
```

Accepted values: 1 (Clear), 2 (Almost clear), 3 (Mild), 4 (Moderate), 5 (Severe).

Response `201 Created`:
```json
{
  "submissionId": 1004,
  "pgaScore": 3,
  "pgaLabel": "Mild",
  "submittedAt": "2024-01-15T10:45:00Z"
}
```

---

## 6. UX Requirements

- Display as a **single-question screen** with large radio buttons or a segmented control.
- Options ordered from best to worst: Clear → Almost clear → Mild → Moderate → Severe (or reverse — confirm with UX).
- Label each option clearly. No numeric values shown to the patient.
- On Baseline: appears at the end of the Lifestyle/Demographics form. 
- On FUP: appears as the first screen in the FUP form sequence.
