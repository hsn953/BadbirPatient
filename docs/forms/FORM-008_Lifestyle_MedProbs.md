# FORM-008 – Lifestyle & Medical Problems Form Specification
## BADBIR Patient Application

> **Document ID:** FORM-008  
> **Version:** 0.2  
> **Status:** Major update — confirmed from actual paper forms  
> **Last Updated:** 2026-03-26

---

## 1. Overview

This document covers two distinct form contexts:
- **Baseline Lifestyle / Demographics Form** (Baseline p.6) — collected once at registration
- **FUP Medical Problems & Lifestyle Update** (FUP p.6) — collected at every follow-up

Both share common elements (occupation, alcohol/smoking) but differ significantly in scope.

---

## 2. BASELINE Lifestyle / Demographics Form

### 2.1 Overview

| Attribute | Value |
|---|---|
| Paper title | "BADBIR PATIENT BASELINE QUESTIONNAIRE" |
| Form appears at | Baseline p.6 |
| Also includes | PGA score (at bottom of same page) — see FORM-006 |
| API endpoint | `POST /api/forms/lifestyle/baseline` |

### 2.2 Questions (Exact text from paper)

**Section: Personal Details (collected once at Baseline only)**

| Field | Question / Label | Type | Options |
|---|---|---|---|
| `BirthTown` | "Where were you born? Town:" | Text | Free text |
| `BirthCountry` | "Where were you born? Country:" | Text | Free text |
| `Occupation` | "What is your occupation?" | Text | Free text (encrypted PII) |
| `WorkStatus` | "Please tick the one box which best describes you:" | Enum | Working full-time / Working part-time / Working full-time in the home / Student / Unemployed but seeking work / Not working due to ill health/disability / Retired |
| `Ethnicity` | "Which of these ethnic groups do you belong to?" | Enum | White / Indian / Pakistani / Bangladeshi / Chinese / Black-African / Black-Caribbean / Black-British / Black-other / Other (specify) |
| `EthnicityOther` | "Other Please specify" | Text | Free text (only if Ethnicity = Other) |
| `OutdoorOccupation` | "Do you have an occupation or hobby which is mainly outdoors?" | bool | Yes / No |
| `LivedTropical` | "Have you ever lived in a tropical/subtropical (hot/sunny climate) country?" | bool | Yes / No |

**Section: Smoking History (Baseline — full history)**

| Field | Question | Type |
|---|---|---|
| `EverSmoked` | "Have you EVER smoked more than one cigarette a day?" | bool (Yes/No) |
| `AvgCigsPerDay` | "If you have ever smoked, what was the average number of cigarettes /day?" | int |
| `AgeStartedSmoking` | "Age started smoking (years)" | int |
| `AgeStoppedSmoking` | "Age stopped smoking (years)" | int? |
| `CurrentlySmoking` | "Do you CURRENTLY smoke more than one cigarette a day?" | bool (Yes/No) |
| `CurrentCigsPerDay` | "If YES, how many cigarettes do you smoke each day?" | int? |

**Section: Alcohol**

| Field | Question | Type |
|---|---|---|
| `DrinkAlcohol` | "Do you drink alcohol?" | bool (Yes/No) |
| `UnitsPerWeek` | "If yes, how many units do you drink in an average week?" | int? |

> **Units guidance shown on paper form:**
> - A pint of ordinary beer/lager (4%) = 2.3 units
> - A pint of strong lager = 3 units
> - A standard (175ml) glass of wine = 2 units
> - A large (250ml) glass of wine = 3 units
> - A small (25ml) glass of spirits = 1 unit
> - A 275ml bottled alcopop = 1.5 units

**Section: PGA** — See FORM-006. Collected at bottom of same page: *"How would you currently rate your psoriasis? Please choose one."*

---

## 3. FUP Medical Problems & Lifestyle Update Form

### 3.1 Overview

| Attribute | Value |
|---|---|
| Paper title (internal) | "Patient Follow-Up Questionnaire" |
| Form appears at | FUP p.6 |
| API endpoint | `POST /api/forms/fup-update` |

### 3.2 PGA (Top of FUP page — collected first)

Collected at the top of FUP p.6 before Medical Problems. See FORM-006.  
*"How would you currently rate your psoriasis? Please choose one."* — Severe / Moderate / Mild / Almost clear / Clear

### 3.3 Medical Problems Questions

**Exact text from paper:**

| # | Field | Question | Options |
|---|---|---|---|
| 1 | `HospitalAdmissions` | "How many times have you been **ADMITTED to hospital** since your last dermatology clinic visit?" | None / One / Two / More than two |
| 2 | `NewDrugs` | "How many **NEW DRUGS** have you been prescribed since your last dermatology clinic visit? (By your GP or the hospital)" | None / One / Two / More than two |
| 3 | `NewReferrals` | "How many **NEW hospital clinics** have you been REFERRED to since your last dermatology clinic visit?" | None / One / Two / More than two |

### 3.4 Occupation Update (FUP only)

| Field | Question | Type |
|---|---|---|
| `Occupation` | "What is your occupation? Please tick the one box which best describes you:" | Enum |

Options (same 7 as Baseline): Working full-time / Working part-time / Working full-time in the home / Student / Unemployed but seeking work / Not working due to ill health/disability / Retired

### 3.5 Smoking (FUP — current only, no history)

FUP p.10 — shown on the same page as the EuroQol VAS:

| Field | Question | Type |
|---|---|---|
| `CurrentlySmoking` | "Do you CURRENTLY smoke more than one cigarette a day?" | bool (Yes/No) |
| `CurrentCigsPerDay` | "If YES, how many cigarettes do you smoke each day?" | int? |

### 3.6 Alcohol / CAGE (FUP p.11)

The FUP alcohol section and CAGE are on the same page. The CAGE questions (FORM-005) appear immediately after the alcohol question when the patient answers Yes.

| Field | Question | Type |
|---|---|---|
| `DrinkAlcohol` | "Do you drink alcohol?" | bool (Yes/No) |
| `UnitsPerWeek` | "If yes, how many units do you drink in an average week?" | int? |

*"If no, you have now completed the questionnaire."* → CAGE skipped, patient signs and dates.

If Yes → CAGE questions 1–4 follow on the same page → see FORM-005.

---

## 4. Differences: Baseline vs FUP

| Field | Baseline | FUP |
|---|---|---|
| Birthplace (town/country) | ✅ Collected | ❌ Not collected |
| Full ethnicity | ✅ Collected | ❌ Not collected |
| Outdoor occupation | ✅ Collected | ❌ Not collected |
| Lived in tropics | ✅ Collected | ❌ Not collected |
| Full smoking history | ✅ Ever, age started/stopped, avg/day, currently | ❌ Current only |
| Work status | ✅ Collected | ✅ Updated |
| Occupation text | ✅ Collected | ✅ Updated |
| Alcohol + units | ✅ Collected | ✅ Updated |
| CAGE | ✅ Conditional (separate page) | ✅ Conditional (embedded in drink section) |
| Hospital admissions | ❌ Not collected | ✅ Count category |
| New drugs | ❌ Not collected | ✅ Count category |
| New referrals | ❌ Not collected | ✅ Count category |
| PGA | ✅ Bottom of same page | ✅ Top of page (separate first item) |

---

## 5. API Contracts

### 5.1 Baseline Lifestyle

**POST** `/api/forms/lifestyle/baseline`

```json
{
  "pappFupId": 1,
  "birthTown": "Edinburgh",
  "birthCountry": "UK",
  "occupation": "Teacher",
  "workStatus": "WorkingFullTime",
  "ethnicity": "White",
  "ethnicityOther": null,
  "outdoorOccupation": false,
  "livedTropical": false,
  "everSmoked": true,
  "avgCigsPerDay": 10,
  "ageStartedSmoking": 18,
  "ageStoppedSmoking": 30,
  "currentlySmoking": false,
  "currentCigsPerDay": null,
  "drinkAlcohol": true,
  "unitsPerWeek": 14,
  "pgaScore": 3
}
```

### 5.2 FUP Medical Update

**POST** `/api/forms/fup-update`

```json
{
  "pappFupId": 67,
  "pgaScore": 3,
  "hospitalAdmissions": "None",
  "newDrugs": "One",
  "newReferrals": "None",
  "occupation": "Teacher",
  "workStatus": "WorkingFullTime",
  "currentlySmoking": false,
  "currentCigsPerDay": null,
  "drinkAlcohol": true,
  "unitsPerWeek": 12
}
```

---

## 6. CAGE Trigger Integration

When `drinkAlcohol = true` is submitted in either the Baseline Lifestyle or FUP Medical Update form, the API response includes `"cageRequired": true`. The client then presents the CAGE form (FORM-005) as the next item in the sequence.

---

## 7. UX Requirements

- **Baseline:** Present as a single multi-section scrollable form or a stepped wizard. Includes personal details, smoking history, alcohol, and PGA as the last question.
- **FUP:** Present PGA first (standalone single-question screen), then Medical Problems (3 count questions), then Occupation update, then Smoking, then Alcohol/CAGE.
- The units guidance table should be shown alongside the units-per-week field.
- Smoking conditional questions (avg cigs, age started/stopped, currently, how many) should appear progressively based on prior answers.
