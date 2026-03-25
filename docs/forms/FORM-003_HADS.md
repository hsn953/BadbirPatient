# FORM-003 – HADS Form Specification
## Hospital Anxiety and Depression Scale
## BADBIR Patient Application

> **Document ID:** FORM-003  
> **Version:** 0.1 (Draft)  
> **Last Updated:** 2026-03-25

---

## 1. Overview

| Attribute | Value |
|---|---|
| Form Name | Hospital Anxiety and Depression Scale (HADS) |
| Legacy API endpoint | `POST /Dashboard/SaveHADS` |
| Legacy DTO | `PatientHad` |
| Conditional | No — always shown |
| Estimated Duration | 3–5 minutes |
| Scoring | Anxiety subscale (A): sum of 7 items (0–21); Depression subscale (D): sum of 7 items (0–21) |

---

## 2. Form Questions

The HADS contains **14 items** — 7 for Anxiety (odd-numbered items) and 7 for Depression (even-numbered items), presented interleaved. Each item has 4 response options scored **0–3**.

> ⚠️ **Important:** The scoring direction alternates. Some items score 0=Often → 3=Never; others score 0=Never → 3=Often. The direction is item-specific. See Section 4 for details.

### 2.1 Full Item List

| # | Legacy Field | Subscale | Question Stem | Options (value in brackets) |
|---|---|---|---|---|
| 1 | `q01tense` | **Anxiety** | I feel tense or wound up | Most of the time [3] / A lot of the time [2] / From time to time, occasionally [1] / Not at all [0] |
| 2 | `q02enjoy` | **Depression** | I still enjoy the things I used to enjoy | Definitely as much [0] / Not quite so much [1] / Only a little [2] / Hardly at all [3] |
| 3 | `q03frightened` | **Anxiety** | I get a sort of frightened feeling as if something awful is about to happen | Very definitely and quite badly [3] / Yes, but not too badly [2] / A little, but it doesn't worry me [1] / Not at all [0] |
| 4 | `q04laugh` | **Depression** | I can laugh and see the funny side of things | As much as I always could [0] / Not quite so much now [1] / Definitely not so much now [2] / Not at all [3] |
| 5 | `q05worry` | **Anxiety** | Worrying thoughts go through my mind | A great deal of the time [3] / A lot of the time [2] / From time to time but not too often [1] / Only occasionally [0] |
| 6 | `q06cheerful` | **Depression** | I feel cheerful | Not at all [3] / Not often [2] / Sometimes [1] / Most of the time [0] |
| 7 | `q07relaxed` | **Anxiety** | I can sit at ease and feel relaxed | Definitely [0] / Usually [1] / Not often [2] / Not at all [3] |
| 8 | `q08slowed` | **Depression** | I feel as if I am slowed down | Nearly all the time [3] / Very often [2] / Sometimes [1] / Not at all [0] |
| 9 | `q09butterflies` | **Anxiety** | I get a sort of frightened feeling like butterflies in the stomach | Not at all [0] / Occasionally [1] / Quite often [2] / Very often [3] |
| 10 | `q10appearence` | **Depression** | I have lost interest in my appearance | Definitely [3] / I don't take as much care as I should [2] / I may not take quite as much care [1] / I take just as much care as ever [0] |
| 11 | `q11restless` | **Anxiety** | I feel restless as if I have to be on the move | Very much indeed [3] / Quite a lot [2] / Not very much [1] / Not at all [0] |
| 12 | `q12lookforward` | **Depression** | I look forward with enjoyment to things | As much as I ever did [0] / Rather less than I used to [1] / Definitely less than I used to [2] / Hardly at all [3] |
| 13 | `q13panic` | **Anxiety** | I get sudden feelings of panic | Very often indeed [3] / Quite often [2] / Not very often [1] / Not at all [0] |
| 14 | `q14enjoybook` | **Depression** | I can enjoy a good book, or radio or TV programme | Often [0] / Sometimes [1] / Not often [2] / Very seldom [3] |

> **Note on legacy schema:** The legacy `PatientHad` DTO only shows Q1–Q10 (`q01`–`q10`). Items Q11–Q14 may exist in the database under different field names, or Q11–Q14 may not have been implemented in the legacy Xamarin app. **This must be confirmed before implementing.** See OQ-02 in URD-001.

---

## 3. Scoring

### 3.1 Subscale Totals

```
Anxiety Score (A)    = q01 + q03 + q05 + q07 + q09 + q11 + q13
Depression Score (D) = q02 + q04 + q06 + q08 + q10 + q12 + q14

Each subscale: 0–21
```

### 3.2 Interpretation Bands

| Score | Interpretation |
|---|---|
| 0–7 | Normal (non-case) |
| 8–10 | Borderline abnormal (borderline case) |
| 11–21 | Abnormal (clinical case) |

This applies independently to both the Anxiety and Depression subscales.

### 3.3 Server-Side Calculation

The API **calculates and stores** both `AnxietyScore` and `DepressionScore` from the individual item responses. The client does not need to compute these.

---

## 4. Validation Rules

- All 14 items (or however many are confirmed in scope) must be answered.
- Each item must be in the range 0–3 inclusive.
- No partial submissions accepted.

---

## 5. Legacy Data Contract

```json
{
  "formId": 0,
  "chid": 12345,
  "pappFupId": 67,
  "q01tense": 1,
  "q02enjoy": 0,
  "q03frightened": 1,
  "q04laugh": 0,
  "q05worry": 2,
  "q06cheerful": 1,
  "q07relaxed": 1,
  "q08slowed": 0,
  "q09butterflies": 0,
  "q10appearence": 1,
  "createdbyname": "Patient via API",
  "formStatus": 1
}
```

---

## 6. New API Contract (Proposed)

**POST** `/api/forms/hads`

```json
{
  "pappFupId": 67,
  "q01": 1, "q02": 0, "q03": 1, "q04": 0,
  "q05": 2, "q06": 1, "q07": 1, "q08": 0,
  "q09": 0, "q10": 1, "q11": 1, "q12": 0,
  "q13": 0, "q14": 1
}
```

Response `201 Created`:
```json
{
  "submissionId": 1002,
  "anxietyScore": 6,
  "depressionScore": 3,
  "anxietyBand": "Normal",
  "depressionBand": "Normal",
  "submittedAt": "2024-01-15T10:35:00Z"
}
```

---

## 7. UX Requirements

- Items must be presented **one at a time** (single item per screen) or in small groups (e.g., 2–3 at a time) to reduce cognitive load.
- Options must be clearly labelled with full text — no numbers or cryptic codes shown to the patient.
- A progress bar ("Question 4 of 14") must be shown throughout.
- On completion, show the subscale scores with their interpretation bands (Normal/Borderline/Abnormal) — patients find this feedback motivating.
- The form must not allow the patient to proceed to the next item without answering the current one.

---

## 8. Open Questions

| # | Question |
|---|---|
| OQ-02a | Are Q11–Q14 present in the database? Are they implemented in the legacy system? |
| OQ-02b | Is the full licensed HADS question text available? (HADS is copyright Snaith & Zigmond, 1983) |
| OQ-02c | Should HADS results be shown to the patient immediately, or only surfaced to clinicians? |
