# FORM-006 – PGA Form Specification
## Patient's Global Assessment
## BADBIR Patient Application

> **Document ID:** FORM-006  
> **Version:** 0.1 (Draft)  
> **Last Updated:** 2026-03-25

---

## 1. Overview

| Attribute | Value |
|---|---|
| Form Name | Patient's Global Assessment (PGA) |
| Legacy API endpoint | `POST /Dashboard/SavePGAScore` |
| Legacy DTO | `PatientPgascore` |
| Conditional | No — always shown |
| Estimated Duration | < 1 minute |
| Scoring | Single score: 0–10 (or 0–100 — see Section 3) |

---

## 2. Form Questions

The PGA consists of a **single question** using a visual analogue / numeric scale.

**Question:**  
*"On a scale from 0 to 10, how would you rate your psoriasis overall today?"*  
*(0 = completely clear, 10 = worst imaginable)*

| Legacy Field | Type | Range |
|---|---|---|
| `pgascore` | INT? | 0–10 (or possibly 0–100 — confirm with client) |

---

## 3. Scoring

There is no subscale calculation. The raw `pgascore` value **is** the score. It is used by clinicians to compare the patient's self-assessment with the clinician's objective PASI score.

> **Open question:** The legacy field is `INT?` — confirm whether it is a 0–10 scale (11 values) or a 0–100 VAS (101 values). This affects the slider control and database column definition.

---

## 4. Legacy Data Contract

```json
{
  "formId": 0,
  "chid": 12345,
  "pappFupId": 67,
  "pgascore": 5,
  "createdbyname": "Patient via API",
  "formStatus": 1
}
```

---

## 5. New API Contract (Proposed)

**POST** `/api/forms/pga`

```json
{
  "pappFupId": 67,
  "pgaScore": 5
}
```

Response `201 Created`:
```json
{
  "submissionId": 1004,
  "patientId": 42,
  "pgaScore": 5,
  "submittedAt": "2024-01-15T10:45:00Z"
}
```

---

## 6. UX Requirements

- The PGA must be presented as a **horizontal slider** with clear endpoint labels (0 = Completely Clear, 10 = Worst Imaginable).
- The current value must be displayed numerically as the user adjusts.
- Large, accessible touch targets for the slider handle.
- The form is very short — can appear on the same screen as a brief introductory text.
