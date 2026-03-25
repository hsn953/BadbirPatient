# FORM-008 – Lifestyle & Medical Problems Specification
## BADBIR Patient Application

> **Document ID:** FORM-008  
> **Version:** 0.1 (Draft)  
> **Last Updated:** 2026-03-25

---

## 1. Overview

| Attribute | Value |
|---|---|
| Forms | Lifestyle Update + Medical Problems / Follow-Up Update |
| Legacy API endpoints | `POST /Dashboard/SaveLifestyle` / `POST /Dashboard/SaveMedProblem` |
| Legacy DTOs | `PatientLifestyle` / `PatientMedProblemFup` |
| Conditional | No — always shown. CAGE conditional on Lifestyle alcohol answer. |
| Estimated Duration | 3–5 minutes combined |

---

## 2. Lifestyle Form

### 2.1 Smoking Section

| Legacy Field | Question | Type |
|---|---|---|
| `smokingMissing` | (Internal flag — set if patient cannot/won't answer) | bool |
| `currentlysmoke` | Do you currently smoke? | bool (Yes/No) |
| `currentlysmokenumbercigsperday` | If yes: How many cigarettes per day on average? | int (0–200) |

**Conditional logic:** The "cigarettes per day" question is only shown if `currentlysmoke = true`.

### 2.2 Alcohol Section

| Legacy Field | Question | Type |
|---|---|---|
| `drinkingMissing` | (Internal flag — patient won't answer) | bool |
| `drinkalcohol` | Do you drink alcohol? | bool (Yes/No) |
| `drnkunitsavg` | If yes: How many units per week on average? | int (0–500) |

**Conditional logic:** 
- "Units per week" only shown if `drinkalcohol = true`.
- If `drinkalcohol = true`, the **CAGE form** is triggered in the form sequence.

### 2.3 Additional Baseline Fields (Month 0 Only)

At baseline, additional demographic/lifestyle fields are collected (to be confirmed from full DB schema):
- **Occupation** (encrypted PII — stored as `OccupationEncrypted`)
- **BMI or Weight/Height** (if collected in the patient app — confirm with client)
- **Ethnicity** (dropdown lookup)
- **Work Status** (lookup)

---

## 3. Medical Problems / Follow-Up Update Form

This form collects medical updates at each follow-up visit. At follow-ups, the patient reports on any **changes** since the last visit.

### 3.1 Confirmed Fields from Legacy

| Legacy Field | Description | Type |
|---|---|---|
| `occupation` | Patient's occupation (encrypted PII) | string |

> **Note:** The `PatientMedProblemFup` DTO showed only `occupation` in the visible portion of the NSwag client. The full list of fields in this form must be confirmed from the database DDL. This document will be updated once the full schema is available.

### 3.2 Expected Fields (Based on Requirements & Legacy Context)

From the master requirements (Section 8.3), patient-entered data at follow-ups includes:

| Category | Fields |
|---|---|
| Medical Updates | New diagnoses since last visit (checkbox list or free text) |
| Medical Updates | Any hospital admissions (Yes/No) |
| Medical Updates | Any new medications (Yes/No — details entered by clinician) |
| Medical Updates | Any pregnancies (if applicable, female patients only) |
| Next Visit Date | Patient's actual next clinic appointment date |

---

## 4. Psoriatic Arthritis Screening (Baseline Only)

At baseline, the patient is asked a screening question to determine whether the HAQ form should be shown:

**Question:** *"Have you been diagnosed with psoriatic arthritis or inflammatory arthritis?"*

- **Yes** → HAQ form is added to the form sequence for this follow-up.
- **No** → HAQ is omitted.

This response is stored on the patient's diagnosis record and applies to all future follow-ups (unless updated by the clinician).

---

## 5. Legacy Data Contract — Lifestyle

```json
{
  "formId": 0,
  "chid": 12345,
  "pappFupId": 67,
  "smokingMissing": false,
  "currentlysmoke": true,
  "currentlysmokenumbercigsperday": 10,
  "drinkingMissing": false,
  "drinkalcohol": true,
  "drnkunitsavg": 14,
  "dateCompleted": "2024-01-15T11:00:00Z",
  "createdbyname": "Patient via API",
  "formStatus": 1
}
```

---

## 6. New API Contracts (Proposed)

**POST** `/api/forms/lifestyle`

```json
{
  "pappFupId": 67,
  "currentlySmoke": true,
  "cigarettesPerDay": 10,
  "drinkAlcohol": true,
  "unitsPerWeek": 14,
  "occupation": "Teacher"
}
```

**POST** `/api/forms/medprobs`

```json
{
  "pappFupId": 67,
  "newDiagnoses": "Hypertension",
  "hadHospitalAdmission": false,
  "isPregnant": null,
  "nextVisitDate": "2024-07-20"
}
```

---

## 7. UX Requirements

- Smoking and alcohol sections should each have a clear section header.
- Use a toggle/switch for Yes/No answers, with conditional follow-up fields that appear smoothly when "Yes" is selected.
- The psoriatic arthritis screening question should appear **before** the DLQI/HAQ forms in the baseline sequence, as the response determines form inclusion.
- "Missing data" flags (`smokingMissing`, `drinkingMissing`) should be presented as "Prefer not to say" options rather than system flags.

---

## 8. Open Questions

| # | Question |
|---|---|
| OQ-LIFESTYLE-01 | What is the full list of fields in `bb_papp_patient_med_problem_fup`? (DDL needed) |
| OQ-LIFESTYLE-02 | Is BMI/weight collected in the patient app or only by the clinician? |
| OQ-LIFESTYLE-03 | Are ethnicity and work status collected at baseline via the patient app? |
| OQ-LIFESTYLE-04 | Should "occupation" be a free-text field or a structured lookup? |
| OQ-LIFESTYLE-05 | At follow-ups, is Medical Problems a single screen or a multi-section form? |
