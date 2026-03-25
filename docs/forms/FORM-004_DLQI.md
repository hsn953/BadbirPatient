# FORM-004 – DLQI Form Specification
## Dermatology Life Quality Index
## BADBIR Patient Application

> **Document ID:** FORM-004  
> **Version:** 0.1 (Draft)  
> **Last Updated:** 2026-03-25

---

## 1. Overview

| Attribute | Value |
|---|---|
| Form Name | Dermatology Life Quality Index (DLQI) |
| Legacy API endpoint | `POST /Dashboard/SaveDLQI` |
| Legacy DTO | `PatientDlqi` |
| Conditional | No — always shown |
| Estimated Duration | 2–3 minutes |
| Scoring | Total score 0–30; lower = less impact on quality of life |
| Copyright | © Finlay AY & Khan GK, 1992. Licensed from Cardiff University. |

---

## 2. ⚠️ Copyright Notice

The DLQI is a copyrighted instrument. The exact question wording is licensed from Cardiff University. **The question text used in the live system must be used under a valid licence.** The question headings below are for development guidance only — final text must be supplied by the client under their existing licence.

---

## 3. Form Questions

The DLQI has **10 items** plus one sub-question (item 7b). Each item is scored 0–3 (or special values).

| # | Legacy Field | Question Theme | Score Options |
|---|---|---|---|
| 1 | `itchsoreScore` | Itch, soreness, pain, stinging | 0=Not at all / 1=A little / 2=A lot / 3=Very much |
| 2 | `embscScore` | Embarrassment or self-consciousness | 0=Not at all / 1=A little / 2=A lot / 3=Very much |
| 3 | `shophgScore` | Interfered with shopping, housework, gardening | 0=Not at all / 1=A little / 2=A lot / 3=Very much |
| 4 | `clothesScore` | Influenced choice of clothing | 0=Not at all / 1=A little / 2=A lot / 3=Very much |
| 5 | `socleisScore` | Social or leisure activities | 0=Not at all / 1=A little / 2=A lot / 3=Very much |
| 6 | `sportScore` | Prevented sport | 0=Not at all / 1=A little / 2=A lot / 3=Very much |
| 7a | `workstudScore` | Work or study | 0=Not at all / 1=A little / 2=A lot / **5=Not relevant** / **6=Yes, has affected work** |
| 7b | `workstudnoScore` | If work affected, how much? | 0=Not at all / 1=A little / 2=A lot (only shown if 7a = 6) |
| 8 | `partcrfScore` | Problems with partner, close friends, relatives | 0=Not at all / 1=A little / 2=A lot / 3=Very much |
| 9 | `sexdifScore` | Sexual difficulties | 0=Not at all / 1=A little / 2=A lot / 3=Very much |
| 10 | `treatmentScore` | Treatment a problem | 0=Not at all / 1=A little / 2=A lot / 3=Very much |

**Reference period:** "Over the last week..." appears before most items.

---

## 4. Scoring Algorithm

The DLQI scoring has a special case for item 7 (work/study):

```
For items 1–6, 8, 9, 10:
    score = 3 - storedValue   (stored as 0=VeryMuch → displayed 3; legacy reversal)

Wait — the legacy stores in reverse: stored 0 = score 3, stored 3 = score 0
So for items 1–6, 8, 9, 10:
    contribution = 3 - storedValue

For item 7 (Work/Study):
    if (workstudScore == 5):
        contribution = 3   (prevented from working = max score)
    else if (workstudScore == 6 && workstudnoScore has value):
        contribution = 3 - workstudnoScore
    else:
        contribution = 0

TotalScore = Sum of all contributions

Range: 0 (no effect) to 30 (extremely large effect on life)
```

**Interpretation bands:**
| Score | Interpretation |
|---|---|
| 0–1 | No effect on patient's life |
| 2–5 | Small effect |
| 6–10 | Moderate effect |
| 11–20 | Very large effect |
| 21–30 | Extremely large effect |

---

## 5. Legacy Data Contract

```json
{
  "formId": 0,
  "chid": 12345,
  "pappFupId": 67,
  "diagnosis": "Psoriasis",
  "itchsoreScore": 2,
  "embscScore": 1,
  "shophgScore": 0,
  "clothesScore": 1,
  "socleisScore": 2,
  "sportScore": 0,
  "workstudScore": 5,
  "workstudnoScore": null,
  "partcrfScore": 1,
  "sexdifScore": 0,
  "treatmentScore": 1,
  "totalScore": 16,
  "createdbyname": "Patient via API",
  "formStatus": 1
}
```

---

## 6. New API Contract (Proposed)

**POST** `/api/forms/dlqi`

```json
{
  "pappFupId": 67,
  "itchsoreScore": 2,
  "embscScore": 1,
  "shophgScore": 0,
  "clothesScore": 1,
  "socleisScore": 2,
  "sportScore": 0,
  "workstudScore": 5,
  "workstudnoScore": null,
  "partcrfScore": 1,
  "sexdifScore": 0,
  "treatmentScore": 1
}
```

Response `201 Created` includes server-computed `totalScore` and `interpretationBand`.

---

## 7. UX Requirements

- Display items in the standard DLQI order (1–10).
- Item 7b should only be visible if item 7a = "Yes, it has prevented me from working" (workstudScore = 6).
- The work/study item has unique logic — a "Not applicable" option must be clearly distinguishable.
- On completion, display the total score and its interpretation band.

---

## 8. Open Questions

| # | Question |
|---|---|
| OQ-DLQI-01 | Can the exact licensed DLQI question text be shared? |
| OQ-DLQI-02 | The legacy scoring reversal (stored 0 = display "Very much") — confirm this is the correct interpretation of the legacy `calculateTotal()` code. |
| OQ-DLQI-03 | Should `diagnosis` field (from legacy DTO) be retained? It appears to be a free text note. |
