# FORM-007 – SAPASI Form Specification
## Self-Administered Psoriasis Area and Severity Index
## BADBIR Patient Application

> **Document ID:** FORM-007  
> **Version:** 0.1 (Draft)  
> **Status:** New implementation — no legacy equivalent  
> **Last Updated:** 2026-03-25

---

## 1. Overview

| Attribute | Value |
|---|---|
| Form Name | Self-Administered Psoriasis Area and Severity Index (SAPASI) |
| Legacy API endpoint | **None** — this is a **new form with no legacy implementation** |
| Conditional | No — always shown |
| Estimated Duration | 3–5 minutes |
| Scoring | Composite score 0–72; higher = more severe psoriasis |

---

## 2. Background

The SAPASI is a patient self-administered adaptation of the clinician-scored PASI (Psoriasis Area and Severity Index). It is a validated tool (Fleischer et al., 1994; Feldman & Fleischer, 1996) that produces scores closely correlated with clinician PASI assessments.

The SAPASI divides the body into **4 regions** and asks the patient to rate:
1. **Area coverage** — using a shaded body diagram
2. **Redness, thickness, and scaliness** — using visual analogue scales (0–4)

---

## 3. Body Regions & Weights

| Region | Body Surface Area Weight |
|---|---|
| Head & neck | 10% (weight factor: 0.1) |
| Upper limbs (arms) | 20% (weight factor: 0.2) |
| Trunk (front + back) | 30% (weight factor: 0.3) |
| Lower limbs (legs) | 40% (weight factor: 0.4) |

---

## 4. Form Structure

### 4.1 For Each Body Region: Coverage (Area) Score

The patient is shown a **body silhouette diagram** for the region with a gradient fill slider or tappable area, and selects what proportion of the region is affected by psoriasis:

| Score | Coverage |
|---|---|
| 0 | 0% (no psoriasis in this area) |
| 1 | 1–9% |
| 2 | 10–29% |
| 3 | 30–49% |
| 4 | 50–69% |
| 5 | 70–89% |
| 6 | 90–100% |

*(Area score conversion for SAPASI: each coverage band converts to a numeric value in the SAPASI formula.)*

### 4.2 For Each Body Region: Severity Scores

The patient rates three severity descriptors using a **horizontal slider (0–4 each)**:

| Descriptor | DB Column | Scale |
|---|---|---|
| **Redness** (Erythema) | `{Region}Redness` | 0 (none) → 4 (very severe) |
| **Thickness** (Induration) | `{Region}Thickness` | 0 (none) → 4 (very severe) |
| **Scaliness** (Desquamation) | `{Region}Scaliness` | 0 (none) → 4 (very severe) |

> **Note:** The initial database schema stub (`DSD-001`) stored only a single `Coverage` and `Severity` column per region. This specification refines that to store Redness, Thickness, and Scaliness separately to maintain score integrity. The schema will need to be updated accordingly.

### 4.3 Proposed Database Columns

```sql
-- For each region prefix: Head, Ul (UpperLimbs), Trunk, Ll (LowerLimbs)
{Region}Coverage  TINYINT  NOT NULL DEFAULT 0,  -- 0–6
{Region}Redness   TINYINT  NOT NULL DEFAULT 0,  -- 0–4
{Region}Thickness TINYINT  NOT NULL DEFAULT 0,  -- 0–4
{Region}Scaliness TINYINT  NOT NULL DEFAULT 0,  -- 0–4
```

Total: 16 data columns + computed `TotalScore`.

---

## 5. SAPASI Score Calculation

```
For each region:
    AreaScore = Coverage score (converted to area index: 1=0.5, 2=1.5, 3=2.5, 4=3.5, 5=4.5, 6=5.5)
    SeveritySum = Redness + Thickness + Scaliness
    RegionScore = AreaScore × SeveritySum × WeightFactor

TotalSAPASI = Sum(RegionScore for all 4 regions)

Range: 0 (no psoriasis) to 72 (theoretical maximum)
```

**Example:**
```
Head: Coverage=2 (area=1.5), R=2, T=1, S=1 → 1.5 × 4 × 0.1 = 0.6
UL:   Coverage=1 (area=0.5), R=1, T=1, S=0 → 0.5 × 2 × 0.2 = 0.2
Trunk: Coverage=3 (area=2.5), R=2, T=2, S=2 → 2.5 × 6 × 0.3 = 4.5
LL:   Coverage=0 (area=0), all → 0
TotalSAPASI = 0.6 + 0.2 + 4.5 + 0 = 5.3
```

---

## 6. UX Requirements

This is the most complex form in the application from a UX perspective.

### 6.1 Body Diagram Interaction

**Option A (Recommended): Shaded Silhouette + Slider**
- Show a front + back body silhouette for each region.
- A slider fills the silhouette progressively (0% → 100%) with a psoriasis-coloured shading.
- The patient matches the fill level to their actual affected area.
- This is accessible, intuitive, and does not require complex drawing interaction.

**Option B (Advanced): Tappable Body Map**
- The patient taps affected areas on an interactive body map.
- The system calculates the percentage from tapped regions.
- This is more accurate but significantly more complex to implement.
- *Recommended for v2.*

### 6.2 Severity Sliders
- Each severity slider (Redness, Thickness, Scaliness) must have:
  - Visual anchor images or colour gradients (e.g., skin tone progression for redness)
  - Labels at each end: "None" and "Very Severe"
  - Large touch targets (48px+ height)
- Show all 3 severity sliders per region on one screen.

### 6.3 Region Navigation
- Use a **stepper/wizard pattern**: one region per step.
- Show a body overview map with completed regions highlighted.
- Allow the patient to go back and adjust a previous region before submitting.

### 6.4 If No Psoriasis in a Region
- If the patient sets coverage to 0, the severity sliders for that region are automatically set to 0 and greyed out.

---

## 7. New API Contract (Proposed)

**POST** `/api/forms/sapasi`

```json
{
  "pappFupId": 67,
  "headCoverage": 2, "headRedness": 2, "headThickness": 1, "headScaliness": 1,
  "ulCoverage": 1,   "ulRedness": 1,   "ulThickness": 1,   "ulScaliness": 0,
  "trunkCoverage": 3,"trunkRedness": 2,"trunkThickness": 2,"trunkScaliness": 2,
  "llCoverage": 0,   "llRedness": 0,   "llThickness": 0,   "llScaliness": 0
}
```

Response `201 Created`:
```json
{
  "submissionId": 1005,
  "patientId": 42,
  "totalScore": 5.30,
  "submittedAt": "2024-01-15T10:50:00Z"
}
```

---

## 8. Open Questions

| # | Question |
|---|---|
| OQ-SAPASI-01 | Does the client have a preferred UX model for the body diagram (shaded slider vs. tappable map)? |
| OQ-SAPASI-02 | What SAPASI scoring formula should be used? (Fleischer 1994, or a variant validated by BADBIR?) |
| OQ-SAPASI-03 | Should the SAPASI score be shown to the patient after submission? |
| OQ-SAPASI-04 | Should coverage use the 7-point scale (0–6) or a simplified 5-point scale? |
