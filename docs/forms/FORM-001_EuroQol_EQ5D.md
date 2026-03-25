# FORM-001 – EuroQol EQ-5D Form Specification
## BADBIR Patient Application

> **Document ID:** FORM-001  
> **Version:** 0.1 (Draft)  
> **Status:** Pending EuroQol version confirmation (see OQ-01)  
> **Last Updated:** 2026-03-25

---

## 1. Overview

| Attribute | Value |
|---|---|
| Form Name | EuroQol EQ-5D (Health-Related Quality of Life) |
| Legacy API endpoint | `POST /Dashboard/SaveEuroqol` |
| Legacy DTO | `PatientEuroQol` |
| Conditional | No — always shown |
| Estimated Duration | 2–3 minutes |
| Scoring | See Section 4 |

---

## 2. ⚠️ Version Ambiguity — Must Be Resolved

The legacy database stores **7 values**: `mobility`, `selfcare`, `usualacts`, `paindisc`, `anxdepr`, `comphealth`, `howyoufeel`. This is consistent with **EQ-5D-3L** (3-level, validated 2002) with an additional Comparative Health item.

The master requirements document (Section 7.3) mentions "EuroQol" without specifying version. **EQ-5D-5L** (5-level, validated 2011) uses a different scoring algorithm and licensed question text.

**Action Required (OQ-01):** Confirm with client whether the new system should use:
- (a) EQ-5D-3L — matching the existing database columns and legacy implementation
- (b) EQ-5D-5L — new, requires schema change (additional levels) and new value set

*This specification documents both variants. The code will implement the confirmed version.*

---

## 3. Form Questions

### 3.1 EQ-5D-3L Version (Legacy — 3 Levels per Dimension)

Each dimension uses radio buttons with exactly 3 options:

| # | Dimension | Level 1 | Level 2 | Level 3 |
|---|---|---|---|---|
| 1 | **MOBILITY** | I have no problems in walking about | I have some problems in walking about | I am confined to bed |
| 2 | **SELF-CARE** | I have no problems with self-care | I have some problems washing or dressing myself | I am unable to wash or dress myself |
| 3 | **USUAL ACTIVITIES** | I have no problems performing my usual activities | I have some problems performing my usual activities | I am unable to perform my usual activities |
| 4 | **PAIN / DISCOMFORT** | I have no pain or discomfort | I have moderate pain or discomfort | I have extreme pain or discomfort |
| 5 | **ANXIETY / DEPRESSION** | I am not anxious or depressed | I am moderately anxious or depressed | I am extremely anxious or depressed |

**Top label (shown on each question):**  
*"Please select the ONE box that best describes your health TODAY."*

| # | Additional Question | Options |
|---|---|---|
| 6 | **EQ VAS** — "We would like to know how good or bad your health is TODAY. On a scale of 0–100 where 100 = best health and 0 = worst health imaginable, what is your health today?" | Vertical slider 0–100 |
| 7 | **Comparative Health** — "Compared with my general level of health over the past 12 months, my health state today is:" | Better / Much the same / Worse |

### 3.2 EQ-5D-5L Version (If Confirmed — 5 Levels per Dimension)

Each dimension uses radio buttons with exactly 5 options. Example for Mobility:
- I have no problems walking about
- I have slight problems walking about
- I have moderate problems walking about
- I have severe problems walking about
- I am unable to walk about

*(Full licensed question text must be obtained from EuroQol Foundation — not reproduced here.)*

---

## 4. Scoring Algorithm

### 4.1 EQ-5D-3L Scoring

- A 5-digit health state profile is formed from the 5 dimension answers (e.g., `11111` = full health).
- A utility index value is calculated using an **English value set** (lookup table, not a formula).
- Range: −0.594 (worst) to 1.000 (full health).
- The VAS score (0–100) is recorded separately.
- The Comparative Health item (Q7) is stored but does not affect the index.

**Legacy implementation note:** The legacy system stored the raw dimension values (`mobility`, `selfcare`, etc.) and did not store the computed index value. **The new system should compute and store the index value** for data quality purposes.

### 4.2 Validation Rules

- All 5 dimension questions must be answered (no skipping within the form).
- VAS must be 0–100 (inclusive).
- Comparative Health must be answered.
- Only one answer per dimension (enforced by radio button UI).

---

## 5. Legacy Data Contract

```json
{
  "formId": 0,
  "chid": 12345,
  "pappFupId": 67,
  "mobility": 1,
  "selfcare": 1,
  "usualacts": 2,
  "paindisc": 1,
  "anxdepr": 2,
  "howyoufeel": 75,
  "comphealth": 1,
  "dateCompleted": "2024-01-15T10:30:00Z",
  "createdbyname": "Patient via API",
  "formStatus": 1
}
```

---

## 6. New API Contract (Proposed)

**POST** `/api/forms/euroqol`  
**Authorization:** Bearer token required

Request body:
```json
{
  "pappFupId": 67,
  "mobility": 1,
  "selfCare": 1,
  "usualActivities": 2,
  "painDiscomfort": 1,
  "anxietyDepression": 2,
  "vasScore": 75,
  "comparativeHealth": 1,
  "notes": null
}
```

Response `201 Created`:
```json
{
  "submissionId": 1001,
  "patientId": 42,
  "pappFupId": 67,
  "mobility": 1,
  "selfCare": 1,
  "usualActivities": 2,
  "painDiscomfort": 1,
  "anxietyDepression": 2,
  "vasScore": 75,
  "comparativeHealth": 1,
  "indexValue": 0.727,
  "submittedAt": "2024-01-15T10:30:00Z"
}
```

---

## 7. UX Requirements

- Each dimension must display on a **full-width card** with a clear heading.
- Radio buttons must be large and easy to tap on mobile (minimum touch target 44×44px).
- The form must adhere to EuroQol Foundation visual guidelines *(to be provided by client)*.
- The VAS slider must be **vertical** per the EuroQol specification, with 0 at the bottom and 100 at the top.
- The VAS must show the current selected value numerically as the user drags.
- Users must not be able to submit without answering all questions.

---

## 8. Implementation Notes

- Legacy field names use lowercase abbreviations (`anxdepr`, `paindisc`). The new C# model uses PascalCase (`AnxietyDepression`, `PainDiscomfort`).
- The `howyoufeel` legacy field = VAS score (0–100 slider value).
- The `comphealth` legacy field = Comparative Health question (1=Better, 2=Same, 3=Worse).
- EQ-5D index value calculation requires a published value set. For EQ-5D-3L (England), the Dolan (1997) value set is standard. An implementation must be obtained under the EuroQol licence.
