# FORM-004 – DLQI Form Specification
## Dermatology Life Quality Index
## BADBIR Patient Application

> **Document ID:** FORM-004  
> **Version:** 0.2  
> **Status:** Updated — confirmed from actual paper forms (Baseline p.7, FUP p.7–8)  
> **Last Updated:** 2026-03-26

---

## 1. Overview

| Attribute | Value |
|---|---|
| Form Name | Dermatology Life Quality Index (DLQI) |
| Legacy API endpoint | `POST /Dashboard/SaveDLQI` |
| Legacy DTO | `PatientDlqi` |
| Conditional | No — always shown at Baseline and every Follow-up |
| Estimated Duration | 2–3 minutes |
| Copyright | © AY Finlay, GK Khan, April 1992. Must not be copied without permission. |
| Form appears at | Baseline p.7; FUP p.7–8 |

---

## 2. Copyright Note

The DLQI is a copyrighted instrument (© AY Finlay, GK Khan, April 1992, Cardiff University). The exact question text must be used under a valid licence held by BADBIR. The question text below is reproduced from the BADBIR paper form under the assumption that BADBIR holds the required licence.

---

## 3. Form Questions (Exact text from paper forms)

**Preamble:** *"The aim of this questionnaire is to measure how much your skin problem has affected your life OVER THE LAST WEEK. Please tick one box for each question."*

*(In FUP: "The aim of the next 10 questions is to measure how much your skin problem has affected your life over the last week. Please tick one box for each question.")*

### Questions and Answer Options

| # | Legacy Field | Question (exact) | Options |
|---|---|---|---|
| 1 | `itchsoreScore` | Over the last week, how **itchy, sore, painful or stinging** has your skin been? | Very much / A lot / A little / Not at all |
| 2 | `embscScore` | Over the last week, how **embarrassed or self conscious** have you been because of your skin? | Very much / A lot / A little / Not at all |
| 3 | `shophgScore` | Over the last week, how much has your skin **interfered with you going shopping or looking after your home or garden**? | Very much / A lot / A little / Not at all / **Not relevant** |
| 4 | `clothesScore` | Over the last week, how much has your skin **influenced the clothes you wear**? | Very much / A lot / A little / Not at all / **Not relevant** |
| 5 | `socleisScore` | Over the last week, how much has your skin **affected any social or leisure activities**? | Very much / A lot / A little / Not at all / **Not relevant** |
| 6 | `sportScore` | Over the last week, how much has your skin **made it difficult for you to do any sport**? | Very much / A lot / A little / Not at all / **Not relevant** |
| 7a | `workstudScore` | Over the last week, has your skin **prevented you from working or studying**? | Yes / No / ~~Not relevant~~ |
| 7b | `workstudnoScore` | *(Sub-question shown only if Q7a = No)* If "No", over the past week how much has your skin been **a problem at work or studying**? | A lot / A little / Not at all |
| 8 | `partcrfScore` | Over the last week, how much has your skin **created problems with your partner or any of your close friends or relatives**? | Very much / A lot / A little / Not at all / **Not relevant** |
| 9 | `sexdifScore` | Over the last week, how much has your skin **caused any sexual difficulties**? | Very much / A lot / A little / Not at all / **Not relevant** |
| 10 | `treatmentScore` | Over the last week, how much of a **problem has the treatment for your skin** been, for example by making your home messy, or by taking up time? | Very much / A lot / A little / Not at all / **Not relevant** |

> **Items 1–2:** No "Not relevant" option. Must answer Very much/A lot/A little/Not at all.  
> **Items 3–6, 8–10:** Include a "Not relevant" option.  
> **Item 7:** Three-part item — see Q7 Logic below.

---

## 4. Scoring Algorithm (Corrected from legacy code analysis)

### 4.1 Standard Items (1–6, 8–10)

| Answer | Score contribution |
|---|---|
| Very much | 3 |
| A lot | 2 |
| A little | 1 |
| Not at all | 0 |
| Not relevant | 0 |

> **Legacy note:** The legacy system stored 0=VeryMuch (reversed) and calculated `3 - storedValue`. The **new system stores the actual score value** (VeryMuch=3, ALot=2, ALittle=1, NotAtAll=0, NotRelevant=0) to simplify the logic.

### 4.2 Item 7 Logic (Work/Study)

| Q7a answer | Q7b answer | Score contribution |
|---|---|---|
| Yes (prevented) | — | **3** |
| No | A lot | **2** |
| No | A little | **1** |
| No | Not at all | **0** |
| Not relevant (not working/not studying) | — | **0** |

> **Legacy storage**: Q7a stored as: 5=prevented (Yes), 6=not prevented (No), other=not relevant. Q7b stored as: 0=NotAtAll, 1=ALittle, 2=ALot.  
> **New system**: Store `workStudyPrevented` (bool?) and `workStudyImpact` (int? 0–2) directly.

### 4.3 Total Score

```
TotalScore = Sum of contributions for items 1–10
Range: 0 (no effect) to 30 (extremely large effect on life)
```

### 4.4 Interpretation

| Score | Meaning |
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
  "chid": 12345,
  "pappFupId": 67,
  "diagnosis": "Psoriasis",
  "itchsoreScore": 2, "embscScore": 1, "shophgScore": 0,
  "clothesScore": 1, "socleisScore": 2, "sportScore": 0,
  "workstudScore": 5, "workstudnoScore": null,
  "partcrfScore": 1, "sexdifScore": 0, "treatmentScore": 1,
  "totalScore": 16,
  "createdbyname": "Patient via API",
  "formStatus": 1
}
```

> **Legacy reversal warning:** In the legacy system, `itchsoreScore` of 0 = Very much (not 0 = Not at all). The new system stores 3 = Very much. When reading legacy data for migration, apply the reversal.

---

## 6. API Contract

**POST** `/api/forms/dlqi`

```json
{
  "pappFupId": 67,
  "q1ItchScore": 3,
  "q2EmbsScore": 2,
  "q3ShopScore": 0,
  "q4ClothesScore": 1,
  "q5SocleisScore": 2,
  "q6SportScore": 0,
  "q7Prevented": true,
  "q7ImpactScore": null,
  "q8PartnerScore": 1,
  "q9SexScore": 0,
  "q10TreatmentScore": 1
}
```

Response `201 Created` includes server-computed `totalScore` and `interpretationBand`.

---

## 7. UX Requirements

- Items 1–6 on one screen; Items 7–10 on a second screen (mirroring paper pages).
- Item 7b (work/study impact) must only be visible if Q7a is answered "No".
- Show "Not relevant" for appropriate items — make it clearly distinguishable from "Not at all".
- On completion, display total score and interpretation band.
- Copyright notice must be shown: *"© AY Finlay, GK Khan, April 1992."*
