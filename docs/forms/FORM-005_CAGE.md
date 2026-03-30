# FORM-005 – CAGE Form Specification
## CAGE Alcohol Screening Questionnaire
## BADBIR Patient Application

> **Document ID:** FORM-005  
> **Version:** 0.2  
> **Status:** Updated — confirmed from actual paper forms (Baseline p.8, FUP p.11)  
> **Last Updated:** 2026-03-26

---

## 1. Overview

| Attribute | Value |
|---|---|
| Form Name | CAGE Alcohol Screening |
| Paper title | "CAGE Questionnaire (Cut down, Annoyed, Guilty, Early morning)" |
| Legacy API endpoint | `POST /Dashboard/SaveCage` |
| Legacy DTO | `PatientCage` |
| **Conditional** | **YES — Only shown if patient confirms they drink alcohol** |
| Estimated Duration | 1 minute |
| Form appears at | Baseline p.8 (separate page); FUP p.11 (embedded in Drinking section) |

---

## 2. Conditional Logic

**Trigger:** `drinkalcohol == true` (patient answered "Yes" to "Do you drink alcohol?")

- **Baseline:** The Lifestyle form (demographics page) asks "Do you drink alcohol? Yes / No". If Yes → CAGE is presented as a separate form page (Baseline p.8). The CAGE page header shows "Alcohol Intake:" confirming this link.
- **FUP:** The Drinking section (FUP p.11) asks "Do you drink alcohol? Yes / No". *"If no, you have now completed the questionnaire."* CAGE questions 1–4 follow immediately on the same page if the patient answers Yes.
- If the patient answers No → CAGE is **silently bypassed** (not shown as Skipped).

> **Design decision:** Although the FUP embeds CAGE within the drinking section, the app treats CAGE as a distinct form with its own `FormStatus` record. CAGE is presented immediately after the Lifestyle/Drinking form if the trigger condition is met.

---

## 3. Form Questions (Exact text from paper forms)

**Paper header (Baseline):** *"CAGE Questionnaire (Cut down, Annoyed, Guilty, Early morning)"*  
**Contextual header:** *"Alcohol Intake:"*

CAGE is a mnemonic:

| # | Legacy field | Letter | Question (exact) | Options |
|---|---|---|---|---|
| 1 | `cutdown` | **C** | Have you ever felt you should **cut down** on your drinking? | Yes / No |
| 2 | `annoyed` | **A** | Have people **annoyed** you by criticising your drinking? | Yes / No |
| 3 | `guilty` | **G** | Have you ever felt **bad or guilty** about your drinking? | Yes / No |
| 4 | `earlymorning` | **E** | Have you ever had a drink first thing in the morning (as an "**eye opener**") to steady your nerves or to get rid of a hangover? | Yes / No |

---

## 4. Scoring

| Answer | Score |
|---|---|
| Yes | 1 |
| No | 0 |

```
CAGE Score = count of "Yes" answers
Range: 0–4

Clinical threshold: score ≥ 2 → clinically significant concern
```

The score and clinical threshold are surfaced to clinicians only — **not shown to the patient**.

---

## 5. Legacy Data Contract

```json
{
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

## 6. API Contract

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
  "cageScore": 1,
  "submittedAt": "2024-01-15T10:40:00Z"
}
```

---

## 7. UX Requirements

- Four Yes/No toggles on a single screen (form is short).
- Clear heading: *"The following questions are about your alcohol use."*
- No score displayed to the patient.
- If the patient changes the drinking answer from Yes to No in the Lifestyle form after CAGE was already triggered, the CAGE form is cancelled.
