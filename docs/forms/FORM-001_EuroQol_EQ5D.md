# FORM-001 – EuroQol EQ-5D Form Specification
## BADBIR Patient Application

> **Document ID:** FORM-001  
> **Version:** 0.2  
> **Status:** Updated — confirmed from actual paper forms (Baseline p.9–10, FUP p.9–10)  
> **Last Updated:** 2026-03-26

---

## 1. Overview

| Attribute | Value |
|---|---|
| Form Name | EuroQol EQ-5D (Generic Health Utility Index) |
| Paper title (Baseline) | "Generic Health Utility Index - Patient Baseline EuroQol" |
| Paper title (FUP) | "Generic Health Utility Index – EuroQol" |
| Legacy API endpoint | `POST /Dashboard/SaveEuroqol` |
| Legacy DTO | `PatientEuroQol` |
| Conditional | No — always shown at Baseline and every Follow-up |
| Estimated Duration | 2–3 minutes |
| Form appears at | Baseline p.9–10; FUP p.9–10 |

---

## 2. Version Confirmed: EQ-5D-3L ✅

The paper forms confirm **EQ-5D-3L** (3 levels per dimension). Each dimension has exactly 3 answer options. OQ-01 from the previous session is now **closed**. The existing legacy database columns are correct.

---

## 3. Form Questions (Exact text from paper forms)

**Preamble:** *"For each of the five activities below please indicate which statements best describe your own health state today."*

### 3.1 EQ-5D Dimensions

Each question instruction: *(Please tick one box)*

| # | Dimension | Level 1 (DB=1) | Level 2 (DB=2) | Level 3 (DB=3) |
|---|---|---|---|---|
| 1 | **MOBILITY** | I have no problems in walking | I have some problems in walking | I am confined to bed |
| 2 | **SELF CARE** | I have no problems with self care | I have some problems washing or dressing | I am unable to wash or dress |
| 3 | **USUAL ACTIVITIES** | I have no problems performing my usual activities *(e.g. work, study, housework, family/leisure activities)* | I have some problems performing my usual activities | I am unable to perform my usual activities |
| 4 | **PAIN/DISCOMFORT** | I have no pain or discomfort | I have moderate pain or discomfort | I have extreme pain or discomfort |
| 5 | **ANXIETY/DEPRESSION** | I am not anxious or depressed | I am moderately anxious or depressed | I am extremely anxious or depressed |

### 3.2 Comparative Health (Q6 — present on both Baseline and FUP)

*"Compared with my general level of health over the past 12 months, my health state today is: (Please tick one box)"*

| Option | DB value |
|---|---|
| Better | 1 |
| Much the same | 2 |
| Worse | 3 |

> **Note:** This question appears on the same page as the 5 dimensions in both Baseline and FUP paper forms.

### 3.3 EQ VAS — Visual Analogue Scale (Q7)

**Exact text from paper form:**  
*"We would like you to indicate on this scale how good or bad is your health today, in your opinion. Please do this by drawing a line from the box below to whichever point on the scale indicates how good or bad your current state is."*  
*"How do you feel today?"*

| Property | Value |
|---|---|
| Scale range | 0 – 100 (integer) |
| Top anchor | Best Imaginable Health State (100) |
| Bottom anchor | Worst Imaginable Health State (0) |
| Orientation | **Vertical** (per EuroQol specification) |
| Legacy field | `howyoufeel` |

> **Note:** The VAS appears on the reverse/second page in the paper form (Baseline p.10, FUP p.10), immediately before the Smoking section in FUP.

---

## 4. Scoring

### 4.1 EQ-5D-3L Health State Profile

The five dimension values form a 5-digit profile (e.g., `11111` = perfect health). This profile is stored in the DB.

### 4.2 EQ-5D Utility Index

A utility index (−0.594 to 1.000) is derived using the **England EQ-5D-3L value set** (Dolan 1997). This is a lookup-based calculation.

**The API computes and stores the index value.** The legacy system stored only raw dimension values; the new system adds `IndexValue`.

> ⚠️ The Dolan value set coefficients must be implemented under the EuroQol Foundation licence.

### 4.3 Validation

- All 5 dimensions + VAS + Comparative Health must be answered before submission.
- VAS: 0–100 integer, enforced server-side.

---

## 5. Legacy ↔ New Field Mapping

| Legacy field | New DTO field | Type | Notes |
|---|---|---|---|
| `mobility` | `Mobility` | int (1–3) | |
| `selfcare` | `SelfCare` | int (1–3) | |
| `usualacts` | `UsualActivities` | int (1–3) | |
| `paindisc` | `PainDiscomfort` | int (1–3) | |
| `anxdepr` | `AnxietyDepression` | int (1–3) | |
| `howyoufeel` | `VasScore` | int (0–100) | |
| `comphealth` | `ComparativeHealth` | int (1–3) | 1=Better, 2=Same, 3=Worse |
| *(not in legacy)* | `IndexValue` | decimal | Computed from value set |

---

## 6. API Contract

**POST** `/api/forms/euroqol`  
**Auth:** ****** required

Request:
```json
{
  "pappFupId": 67,
  "mobility": 1,
  "selfCare": 1,
  "usualActivities": 2,
  "painDiscomfort": 1,
  "anxietyDepression": 2,
  "vasScore": 75,
  "comparativeHealth": 1
}
```

Response `201 Created`:
```json
{
  "submissionId": 1001,
  "indexValue": 0.727,
  "submittedAt": "2024-01-15T10:30:00Z"
}
```

---

## 7. UX Requirements

- Present the 5 dimensions on one screen, followed by a separate screen for the VAS.
- VAS must be a **vertical slider** with numerical display of current value.
- Comparative Health appears after the VAS on the same screen.
- All 7 questions required before the Submit button activates.
- Minimum touch target: 44×44px for radio buttons and slider handle.
