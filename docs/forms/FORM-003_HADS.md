# FORM-003 – HADS Form Specification
## Hospital Anxiety and Depression Scale
## BADBIR Patient Application

> **Document ID:** FORM-003  
> **Version:** 0.2  
> **Status:** Updated — all 14 items confirmed from actual paper forms (Baseline p.13–14, FUP p.12–13)  
> **Last Updated:** 2026-03-26

---

## 1. Overview

| Attribute | Value |
|---|---|
| Form Name | Hospital Anxiety and Depression Scale (HADS) |
| Legacy API endpoint | `POST /Dashboard/SaveHADS` |
| Legacy DTO | `PatientHad` |
| Conditional | No — always shown at Baseline and every Follow-up |
| Estimated Duration | 3–5 minutes |
| Copyright | © R.P. Snaith and A.S. Zigmond, 1983, 1992, 1994 |
| Form appears at | Baseline p.13–14; FUP p.12–13 |

---

## 2. Open Question Resolved: Q11–Q14 Are Present ✅

OQ-02a is now closed. All 14 HADS items are confirmed present in the paper forms (pages 13–14 of Baseline, pages 12–13 of FUP). Q11–Q14 must be implemented.

**Note on legacy DTO:** The legacy `PatientHad` NSwag-generated DTO showed only Q1–Q10 (`q01tense` through `q10appearence`). Q11–Q14 must be added to the new entity/DTO. The database may already store them — confirm with DB DDL (DB-01).

---

## 3. Preamble (Exact text from paper)

*"Read each item and circle the reply which comes closest to how you have been feeling in the past week. Don't take too long over your replies: your immediate reaction to each item will probably be more accurate than a long thought out response."*

---

## 4. All 14 Questions with Exact Options and Scoring

**Scoring direction:** Options are listed left-to-right on the paper form. The left option is NOT always the highest score — the direction alternates between Anxiety and Depression items.

| # | Legacy Field | Subscale | Question | Option A | Option B | Option C | Option D |
|---|---|---|---|---|---|---|---|
| 1 | `q01tense` | **A** | I feel tense or wound up: | Most of the time **[3]** | A lot of the time **[2]** | Time to time, occasionally **[1]** | Not at all **[0]** |
| 2 | `q02enjoy` | **D** | I still enjoy the things I used to enjoy: | Definitely as much **[0]** | Not quite as much **[1]** | Only a little **[2]** | Hardly at all **[3]** |
| 3 | `q03frightened` | **A** | I get a sort of frightened feeling as if something awful is about to happen: | Very definitely and quite badly **[3]** | Yes, but not too badly **[2]** | A little, but it doesn't worry me **[1]** | Not at all **[0]** |
| 4 | `q04laugh` | **D** | I can laugh and see the funny side of things: | As much as I always could **[0]** | Not quite so much now **[1]** | Definitely not so much now **[2]** | Not at all **[3]** |
| 5 | `q05worry` | **A** | Worrying thoughts go through my mind: | A great deal of the time **[3]** | A lot of the time **[2]** | From time to time but not too often **[1]** | Only occasionally **[0]** |
| 6 | `q06cheerful` | **D** | I feel cheerful: | Not at all **[3]** | Not often **[2]** | Sometimes **[1]** | Most of the time **[0]** |
| 7 | `q07relaxed` | **A** | I can sit at ease and feel relaxed: | Definitely **[0]** | Usually **[1]** | Not often **[2]** | Not at all **[3]** |
| 8 | `q08slowed` | **D** | I feel as if I am slowed down: | Nearly all the time **[3]** | Very often **[2]** | Sometimes **[1]** | Not at all **[0]** |
| 9 | `q09butterflies` | **A** | I get a sort of frightened feeling like 'butterflies' in my stomach: | Not at all **[0]** | Occasionally **[1]** | Quite often **[2]** | Very often **[3]** |
| 10 | `q10appearence` | **D** | I have lost interest in my appearance: | Definitely **[3]** | I don't take so much care as I should **[2]** | I may not take quite as much care **[1]** | I take just as much care as ever **[0]** |
| 11 | `q11restless` | **A** | I feel restless as if I have to be on the move: | Very much indeed **[3]** | Quite a lot **[2]** | Not very much **[1]** | Not at all **[0]** |
| 12 | `q12lookforward` | **D** | I look forward with enjoyment to things: | As much as ever I did **[0]** | Rather less than I used to **[1]** | Definitely less than I used to **[2]** | Hardly at all **[3]** |
| 13 | `q13panic` | **A** | I get sudden feelings of panic: | Very often indeed **[3]** | Quite often **[2]** | Not very often **[1]** | Not at all **[0]** |
| 14 | `q14enjoybook` | **D** | I can enjoy a good book or radio or TV programme: | Often **[0]** | Sometimes **[1]** | Not often **[2]** | Very seldom **[3]** |

**Subscale key: A = Anxiety items (odd: 1,3,5,7,9,11,13); D = Depression items (even: 2,4,6,8,10,12,14)**

---

## 5. Scoring (Confirmed from paper scoring page)

**Exact text from paper form scoring page:**
- *"Depression Items 2 4 6 8 10 12 14 — Score either 0, 1, 2, 3 so possible max of 21"*
- *"Anxiety Items 1 3 5 7 9 11 13 — Score either 0, 1, 2, 3 so possible max of 21"*

```
Anxiety Score (A)    = q01 + q03 + q05 + q07 + q09 + q11 + q13    Range: 0–21
Depression Score (D) = q02 + q04 + q06 + q08 + q10 + q12 + q14    Range: 0–21
```

### Interpretation Bands

| Score | Interpretation |
|---|---|
| 0–7 | Normal |
| 8–10 | Borderline abnormal |
| 11–21 | Abnormal (clinical case) |

Applies independently to both Anxiety and Depression subscales.

---

## 6. Key Implementation Notes

### 6.1 Scoring Direction

The options are NOT in a consistent high-to-low order. Each item's scoring must be hardcoded per the table above. Do NOT assume a simple linear mapping from position.

### 6.2 Legacy DB Field Names (Q11–Q14)

The legacy NSwag DTO showed only Q1–Q10. Q11–Q14 fields must be:
- Added to the new EF Core entity based on the actual DB DDL.
- Expected legacy field names (inferred): `q11restless`, `q12lookforward`, `q13panic`, `q14enjoybook`.

---

## 7. Legacy Data Contract

```json
{
  "chid": 12345,
  "pappFupId": 67,
  "q01tense": 1, "q02enjoy": 0, "q03frightened": 1, "q04laugh": 0,
  "q05worry": 2, "q06cheerful": 1, "q07relaxed": 1, "q08slowed": 0,
  "q09butterflies": 0, "q10appearence": 1,
  "q11restless": 1, "q12lookforward": 0, "q13panic": 0, "q14enjoybook": 1,
  "createdbyname": "Patient via API",
  "formStatus": 1
}
```

---

## 8. API Contract

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
  "anxietyBand": "Normal",
  "depressionScore": 3,
  "depressionBand": "Normal",
  "submittedAt": "2024-01-15T10:35:00Z"
}
```

---

## 9. UX Requirements

- Items presented one at a time or in pairs with clear numbering.
- Progress bar: "Question N of 14".
- Option labels must match exact text from the paper form.
- Options must NOT be reordered from the standard left-to-right presentation.
- On completion, display Anxiety and Depression subscale scores with interpretation bands.
- Copyright notice must be displayed: *"HADS copyright © R.P. Snaith and A.S. Zigmond, 1983, 1992, 1994."*

---

## 10. Licence Note — Pay-Per-Form (GL Assessments)

**IMPORTANT:** HADS is published by **GL Assessment** and is subject to a **pay-per-completed-form agreement** between GL Assessments and BADBIR.

| Requirement | Detail |
|---|---|
| Billing model | Per successfully completed and submitted HADS form |
| Agreement holder | BADBIR (registered charity) |
| Counter field | `IsCountable` flag in `HadsSubmissions` table — set to `1` only on full submission |
| What counts | Fully completed (all 14 items answered) and submitted forms only |
| What does NOT count | Abandoned/incomplete sessions, draft auto-saves, test submissions |
| Reporting | `GET /api/admin/reports/hads-submissions-count?from=&to=` — for monthly/quarterly finance reporting |

**Implementation rule:** The `HadsSubmissions.IsCountable` column must remain `0` until the form is fully completed and the patient clicks "Submit". If the session is abandoned, the incomplete row should be deleted or remain with `IsCountable = 0`. The submission count must be auditable.
