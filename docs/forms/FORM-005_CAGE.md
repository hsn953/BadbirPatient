# FORM-005 – CAGE Form Specification
## CAGE Alcohol Screening Questionnaire
## BADBIR Patient Application

> **Document ID:** FORM-005  
> **Version:** 0.1 (Draft)  
> **Last Updated:** 2026-03-25

---

## 1. Overview

| Attribute | Value |
|---|---|
| Form Name | CAGE Alcohol Screening |
| Legacy API endpoint | `POST /Dashboard/SaveCage` |
| Legacy DTO | `PatientCage` |
| **Conditional** | **YES — Only shown if patient confirmed they drink alcohol in the Lifestyle form** |
| Estimated Duration | 1 minute |
| Scoring | 0–4; score ≥ 2 indicates possible problem drinking |

---

## 2. Conditional Logic

- The CAGE form is **only presented** if `PatientLifestyle.drinkalcohol == true`.
- The Lifestyle form must be completed before CAGE appears in the sequence.
- If the patient indicates they do not drink, the CAGE form is silently bypassed (not shown as skipped).

---

## 3. Form Questions

The CAGE consists of **4 yes/no questions**. Each "Yes" answer scores 1 point.

CAGE is a mnemonic:

| # | Legacy Field | Letter | Question |
|---|---|---|---|
| 1 | `cutdown` | **C** | Have you ever felt you should **Cut down** on your drinking? |
| 2 | `annoyed` | **A** | Have people **Annoyed** you by criticising your drinking? |
| 3 | `guilty` | **G** | Have you ever felt bad or **Guilty** about your drinking? |
| 4 | `earlymorning` | **E** | Have you ever had a drink first thing in the morning to steady your nerves or get rid of a hangover (**Eye-opener**)? |

**Answer type:** Boolean Yes/No for each question.

---

## 4. Scoring

```
Score = count of "Yes" answers (each Yes = 1 point)
Range: 0–4

Interpretation:
  0–1: Low risk
  2+:  Clinically significant concern — may indicate problem drinking
```

The score is computed server-side. The interpretation is not shown to the patient (it is a clinical indicator for the clinician).

---

## 5. Legacy Data Contract

```json
{
  "formId": 0,
  "chid": 12345,
  "pappFupId": 67,
  "cutdown": false,
  "annoyed": false,
  "guilty": true,
  "earlymorning": false,
  "dateCompleted": "2024-01-15T10:40:00Z",
  "createdbyname": "Patient via API",
  "formStatus": 1
}
```

---

## 6. New API Contract (Proposed)

**POST** `/api/forms/cage`

```json
{
  "pappFupId": 67,
  "cutdown": false,
  "annoyed": false,
  "guilty": true,
  "eyeOpener": false
}
```

Response `201 Created`:
```json
{
  "submissionId": 1003,
  "patientId": 42,
  "cageScore": 1,
  "submittedAt": "2024-01-15T10:40:00Z"
}
```

---

## 7. UX Requirements

- Questions should be presented on a **single screen** (4 items is short).
- Yes/No toggle or checkbox per question.
- Clear heading explaining the context: *"The following questions are about your alcohol use."*
- On submit, no score or interpretation is shown to the patient.
