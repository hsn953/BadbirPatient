# FORM-002 – HAQ Form Specification
## Health Assessment Questionnaire (HAQ-DI)
## BADBIR Patient Application

> **Document ID:** FORM-002  
> **Version:** 0.1 (Draft)  
> **Last Updated:** 2026-03-25

---

## 1. Overview

| Attribute | Value |
|---|---|
| Form Name | Health Assessment Questionnaire – Disability Index (HAQ-DI) |
| Legacy API endpoint | `POST /Dashboard/SaveHAQ` |
| Legacy DTO | `PatientHaq` |
| **Conditional** | **YES — Only shown if patient has psoriatic or inflammatory arthritis diagnosis** |
| Estimated Duration | 8–12 minutes (long form — must be sectioned) |
| Scoring | HAQ-DI = mean of 8 category scores (0–3); displayed to patient after submission |

---

## 2. Conditional Logic

- The HAQ form is **only presented** if `PatientDiagnoses` includes a psoriatic arthritis or inflammatory arthritis code.
- The API checks this condition when building the dashboard form list.
- If the condition is not met, the form is silently omitted from the sequence (not shown as "skipped").
- **UX Note:** Because HAQ is long and frequently skipped, the skip confirmation must be prominently available from the start of the form, not just at the end.

---

## 3. Form Structure

The HAQ is divided into **8 functional categories**. Within each category there are 2–3 individual questions. The category score is the **maximum** of the individual question scores (per HAQ-DI standard method).

### 3.1 Scoring Scale (per question)

| Score | Meaning |
|---|---|
| 0 | Without any difficulty |
| 1 | With some difficulty |
| 2 | With much difficulty |
| 3 | Unable to do |

If a patient uses an **aid or device** for a category, the minimum score for that category is raised to **2** (per HAQ-DI rules).

---

### 3.2 Category 1: Dressing and Grooming

| # | Legacy Field | Question |
|---|---|---|
| 1.1 | `dressself` | Are you able to... **Dress yourself**, including tying shoelaces and doing buttons? |
| 1.2 | `shampoo` | Are you able to... **Shampoo your hair**? |

Category score = max(1.1, 1.2), adjusted for aids.

**Aids/devices for this category:** `dressing` (bool flag), `dressingdetails` (text)

---

### 3.3 Category 2: Arising

| # | Legacy Field | Question |
|---|---|---|
| 2.1 | `standchair` | Are you able to... **Stand up from a straight chair**? |
| 2.2 | `bed` | Are you able to... **Get in and out of bed**? |

Category score = max(2.1, 2.2).

**Section category score:** `dressgroom` (Dressing & Grooming combined score), `rising` (Arising score)

---

### 3.4 Category 3: Eating

| # | Legacy Field | Question |
|---|---|---|
| 3.1 | `cutmeat` | Are you able to... **Cut your meat**? |
| 3.2 | `liftglass` | Are you able to... **Lift a full cup or glass to your mouth**? |
| 3.3 | `openmilk` | Are you able to... **Open a new milk carton**? |

Category score = max(3.1, 3.2, 3.3).  
Legacy field: `eating`

**Aids:** `specialutensils` (bool flag)

---

### 3.5 Category 4: Walking

| # | Legacy Field | Question |
|---|---|---|
| 4.1 | `walkflat` | Are you able to... **Walk outdoors on flat ground**? |
| 4.2 | `climbsteps` | Are you able to... **Climb up 5 steps**? |

Category score = max(4.1, 4.2).  
Legacy field: `walking`

**Aids:** `cane`, `crutches`, `walker`, `wheelchair` (separate bool flags for each)

---

### 3.6 Category 5: Hygiene

| # | Legacy Field | Question |
|---|---|---|
| 5.1 | `washdry` | Are you able to... **Wash and dry your body**? |
| 5.2 | `bath` | Are you able to... **Take a tub bath**? |
| 5.3 | `toilet` | Are you able to... **Get on and off the toilet**? |

Category score = max(5.1, 5.2, 5.3).

**Aids:** `loolift`, `bathseat`, `bathrail` (bool flags)

---

### 3.7 Category 6: Reach

| # | Legacy Field | Question |
|---|---|---|
| 6.1 | `reachabove` | Are you able to... **Reach and get down a 5-pound object (such as a bag of sugar) from above your head**? |
| 6.2 | `bend` | Are you able to... **Bend down to pick up clothing from the floor**? |

Category score = max(6.1, 6.2).

---

### 3.8 Category 7: Grip

| # | Legacy Field | Question |
|---|---|---|
| 7.1 | `cardoor` | Are you able to... **Open car doors**? |
| 7.2 | `openjar` | Are you able to... **Open jars that have been previously opened**? |
| 7.3 | `turntap` | Are you able to... **Turn taps on and off**? |

Category score = max(7.1, 7.2, 7.3).

---

### 3.9 Category 8: Activities

| # | Legacy Field | Question |
|---|---|---|
| 8.1 | `shop` | Are you able to... **Run errands and shop**? |
| 8.2 | `getincar` | Are you able to... **Get in and out of a car**? |
| 8.3 | `housework` | Are you able to... **Do chores such as vacuuming and gardening**? |

Category score = max(8.1, 8.2, 8.3).

---

## 4. HAQ-DI Score Calculation

```
HAQ-DI = mean(Category1, Category2, Category3, Category4, Category5, Category6, Category7, Category8)
Range: 0.00 – 3.00

Interpretation:
  0.00 – 0.25: Mild disability
  0.26 – 0.50: Mild-to-moderate
  0.51 – 1.00: Moderate
  1.01 – 2.00: Moderate-to-severe
  2.01 – 3.00: Severe disability
```

**Aid adjustment:** If a patient uses an aid for any question within a category, the category score minimum becomes 2 (if the unaided score was 0 or 1).

---

## 5. Missing Data

Legacy field `missingdata` (bool) + `missingdatadetails` (string). If the patient cannot complete a question, the clinician can flag it as missing. The new system should retain this but it is a clinician-facing annotation, not patient-facing.

---

## 6. Legacy Data Contract

```json
{
  "formId": 0,
  "chid": 12345,
  "pappFupId": 67,
  "missingdata": false,
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

## 7. New API Contract (Proposed)

**POST** `/api/forms/haq`

```json
{
  "pappFupId": 67,
  "q_dressself": 1, "q_shampoo": 0,
  "q_standchair": 1, "q_bed": 1,
  "q_cutmeat": 0, "q_liftglass": 0, "q_openmilk": 1,
  "q_walkflat": 0, "q_climbsteps": 1,
  "q_washdry": 0, "q_bath": 0, "q_toilet": 0,
  "q_reachabove": 1, "q_bend": 1,
  "q_cardoor": 0, "q_openjar": 1, "q_turntap": 0,
  "q_shop": 0, "q_getincar": 0, "q_housework": 1,
  "aid_cane": false, "aid_crutches": false, "aid_walker": false, "aid_wheelchair": false,
  "aid_specialUtensils": false, "aid_specialChair": false,
  "aid_dressing": false, "aid_loolift": false, "aid_bathseat": false, "aid_bathrail": false
}
```

Response `201 Created` includes computed `haqDiScore`.

---

## 8. UX Requirements

- The 8 categories must be presented as **separate sections or pages** with a progress indicator.
- Display an estimated completion time (e.g., "About 10 minutes") at the start.
- Aids/devices questions should appear at the **end** of each relevant category section.
- The skip confirmation must be easily accessible throughout — not only at the end.
- On completion, display the calculated HAQ-DI score with a brief interpretation band (mild/moderate/severe).
