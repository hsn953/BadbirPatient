# FORM-002 – HAQ Form Specification
## Health Assessment Questionnaire (HAQ-DI)
## BADBIR Patient Application

> **Document ID:** FORM-002  
> **Version:** 0.2  
> **Status:** Updated — confirmed from actual paper forms (Baseline p.11–12, FUP p.15–16)  
> **Last Updated:** 2026-03-26

---

## 1. Overview

| Attribute | Value |
|---|---|
| Form Name | Health Assessment Questionnaire – Disability Index (HAQ-DI) |
| Legacy API endpoint | `POST /Dashboard/SaveHAQ` |
| Legacy DTO | `PatientHaq` |
| **Conditional** | **YES — Only shown if patient has a rheumatologist's diagnosis of inflammatory arthritis** |
| Estimated Duration | 8–12 minutes |
| Paper title | "HEALTH ASSESSMENT QUESTIONNAIRE (HAQ)" |
| Form appears at | Baseline p.11–12; FUP p.15–16 |

---

## 2. Conditional Logic

**Trigger:** `PatientDiagnoses` includes a rheumatologist's diagnosis of inflammatory arthritis (including psoriatic arthritis).

- The clinician baseline form (p.5) states: *"(2) Only if patient has a rheumatologist's diagnosis of inflammatory arthritis"* for HAQ.
- The patient baseline registration checklist (p.1) states: *"HAQ (only if patient has diagnosis of IA)"*.
- If the condition is not met, HAQ is **omitted silently** from the form sequence — it does NOT appear as "Skipped" to the patient.
- The API checks this condition when building the dashboard form list.

---

## 3. Form Structure

**Scale instruction (exact text):**  
*"Please tick the one response which best describes your usual abilities over the past week"*

**Answer options (same for all 20 individual questions):**

| Option | DB value |
|---|---|
| Without ANY difficulty | 0 |
| With SOME difficulty | 1 |
| With MUCH difficulty | 2 |
| UNABLE to do | 3 |

---

## 4. HAQ Categories and Questions (Exact text from paper forms)

### Category 1: DRESSING AND GROOMING

*"Are you able to:"*

| Code | Question |
|---|---|
| `dressself` | a. Dress yourself, including tying shoelaces and doing buttons? |
| `shampoo` | b. Shampoo your hair? |

Category score field: `dressgroom`

**Aids applicable to this category:**
- Devices used for dressing (button hooks, zipper pull, shoe horn) — `dressing` flag

---

### Category 2: RISING

*"Are you able to:"*

| Code | Question |
|---|---|
| `standchair` | a. Stand up from an armless straight chair? |
| `bed` | b. Get in and out of bed? |

Category score field: `rising`

**Aids applicable to this category:**
- Special or built-up chair (A) — `specialchair` flag

---

### Category 3: EATING

*"Are you able to:"*

| Code | Question |
|---|---|
| `cutmeat` | a. Cut your meat? |
| `liftglass` | b. Lift a full cup or glass to your mouth? |
| `openmilk` | c. Open a new carton of milk (or soap powder)? |

Category score field: `eating`

**Aids applicable to this category:**
- Built-up or special utensils (E) — `specialutensils` flag

---

### Category 4: WALKING

*"Are you able to:"*

| Code | Question |
|---|---|
| `walkflat` | a. Walk outdoors on flat ground? |
| `climbsteps` | b. Climb up five steps? |

Category score field: `walking`

**Aids applicable to this category (Walking):**
- Cane (W) — `cane` flag
- Crutches (W) — `crutches` flag
- Walking frame (W) — `walker` flag
- Wheelchair (W) — `wheelchair` flag

---

### Category 5: HYGIENE

*"Are you able to:"*

| Code | Question |
|---|---|
| `washdry` | a. Wash and dry your entire body? |
| `bath` | b. Take a bath? |
| `toilet` | c. Get on and off the toilet? |

**Aids applicable to this category:**
- Raised toilet seat (H) — `loolift` flag
- Bath seat (H) — `bathseat` flag
- Bath rail (H) — `bathrail` flag

---

### Category 6: REACH

*"Are you able to:"*

| Code | Question |
|---|---|
| `reachabove` | a. Reach and get down a 5 lb object (e.g. a bag of potatoes) from just above your head? |
| `bend` | b. Bend down to pick up clothing off the floor? |

**Aids applicable to this category:**
- Long handled appliances for reach (R)

---

### Category 7: GRIP

*"Are you able to:"*

| Code | Question |
|---|---|
| `cardoor` | a. Open car doors? |
| `openjar` | b. Open jars which have been previously opened? |
| `turntap` | c. Turn taps on and off? |

**Aids applicable to this category:**
- Jar opener (for jars previously opened) (G)

---

### Category 8: ACTIVITIES

*"Are you able to:"*

| Code | Question |
|---|---|
| `shop` | a. Run errands and shop? |
| `getincar` | b. Get in and out of a car? |
| `housework` | c. Do chores such as vacuuming, housework or light gardening? |

---

## 5. "Help from Another Person" Flags

The paper form asks patients to tick categories where they need help from another person:

| Paper label | Mapped to categories |
|---|---|
| Dressing and Grooming | Category 1 |
| Rising | Category 2 |
| Eating | Category 3 |
| Walking | Category 4 |
| Hygiene | Category 5 |
| Gripping and opening things | Category 7 |
| Reach | Category 6 |
| Errands and housework | Category 8 |

These are stored as boolean flags per category on the DTO.

---

## 6. HAQ-DI Score Calculation

### 6.1 Category Score

Each category score is the **maximum** of the individual question scores within that category, subject to the aid adjustment below.

```
CategoryScore = max(all individual question scores in category)
```

### 6.2 Aid Adjustment

If the patient uses an aid or device for any question within a category, the minimum category score for that category is raised to **2** (if the unaided score was 0 or 1).

```
if (anyAidUsedInCategory && categoryScore < 2):
    categoryScore = 2
```

### 6.3 HAQ-DI Formula

```
HAQ-DI = Sum(8 category scores) / 8
Range: 0.0 – 3.0
```

The paper form includes a lookup table directly on the scoring page (confirmed from OCR):

| Sum of 8 category scores | HAQ-DI |
|---|---|
| 0 | 0.000 |
| 1 | 0.125 |
| 2 | 0.250 |
| 3 | 0.375 |
| 4 | 0.500 |
| 5 | 0.625 |
| 6 | 0.750 |
| 7 | 0.875 |
| 8 | 1.000 |
| 9 | 1.125 |
| 10 | 1.250 |
| 11 | 1.375 |
| 12 | 1.500 |
| 13 | 1.625 |
| 14 | 1.750 |
| 15 | 1.875 |
| 16 | 2.000 |
| 17 | 2.125 |
| 18 | 2.250 |
| 19 | 2.375 |
| 20 | 2.500 |
| 21 | 2.625 |
| 22 | 2.750 |
| 23 | 2.875 |
| 24 | 3.000 |

### 6.4 Interpretation Bands

| HAQ-DI | Band |
|---|---|
| 0.00–0.25 | Mild disability |
| 0.26–0.50 | Mild-to-moderate |
| 0.51–1.00 | Moderate |
| 1.01–2.00 | Moderate-to-severe |
| 2.01–3.00 | Severe disability |

---

## 7. Legacy Data Contract

```json
{
  "chid": 12345,
  "pappFupId": 67,
  "dressself": 1, "shampoo": 0,
  "standchair": 1, "bed": 1,
  "cutmeat": 0, "liftglass": 0, "openmilk": 1,
  "walkflat": 0, "climbsteps": 1,
  "washdry": 0, "bath": 0, "toilet": 0,
  "reachabove": 1, "bend": 1,
  "cardoor": 0, "openjar": 1, "turntap": 0,
  "shop": 0, "getincar": 0, "housework": 1,
  "dressgroom": 1, "rising": 1, "eating": 1, "walking": 1,
  "cane": 0, "crutches": 0, "walker": 0, "wheelchair": 0,
  "specialutensils": 0, "specialchair": 0, "dressing": 0,
  "loolift": 0, "bathseat": 0, "bathrail": 0,
  "createdbyname": "Patient via API",
  "formStatus": 1
}
```

---

## 8. API Contract

**POST** `/api/forms/haq`  
**Auth:** ****** required

Response `201 Created` includes computed `haqDiScore` (decimal 0.000–3.000).

---

## 9. UX Requirements

- Present categories in 2 pages matching the paper form layout: Categories 1–4 on page 1, Categories 5–8 on page 2.
- Aids/devices checkboxes appear at the bottom of each page.
- "Help from another person" checkboxes appear at the bottom of each page.
- Display a progress indicator ("Page 1 of 2").
- On completion, show the computed HAQ-DI score and interpretation band.
- Skip confirmation must be accessible throughout (not just at the end).
