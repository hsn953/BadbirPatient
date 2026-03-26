# FORM-007 – SAPASI Form Specification
## Self-Administered PASI
## BADBIR Patient Application

> **Document ID:** FORM-007  
> **Version:** 0.2  
> **Status:** In scope for v1 — UX design decision pending (to be finalised at development sprint)  
> **Last Updated:** 2026-03-26

---

## 1. Overview

| Attribute | Value |
|---|---|
| Form Name | Self-Administered PASI (SAPASI) |
| Conditional | No — shown at Baseline and every Follow-up |
| Estimated Duration | 3–5 minutes |
| Legacy equivalent | None — **new form with no legacy history** |
| DB table | New — `SapasiSubmissions` (see DSD-001 §4.5) |
| API endpoint | `POST /api/forms/sapasi` (new — no legacy endpoint) |

### Why SAPASI?

The clinical PASI (Psoriasis Area and Severity Index) is calculated by the clinician during a face-to-face visit. SAPASI allows the **patient** to self-report an equivalent score between visits, making it possible to track psoriasis severity at every follow-up without requiring a clinic appointment.

---

## 2. ⚠️ UX Design Decision: PENDING

**The scoring algorithm and data model are fixed (see below). The open question is how to present the body diagram to the patient.**

| Option | Description | Pros | Cons |
|---|---|---|---|
| **Option A — Shaded silhouette + sliders** | Show a static body diagram split into 4 regions. For each region, patient uses sliders for: % coverage (0–4 bands) and severity (Redness + Thickness + Scaliness, each 0–4). | Simple, accessible, works on all screen sizes, no complex UI component | Less intuitive than tapping; patients may not relate to static diagram |
| **Option B — Interactive tappable body map** | Full-colour interactive body silhouette. Patient taps/shades affected areas to indicate coverage, then rates severity per region. | Intuitive and visual; closely matches the concept | Complex UI; accessibility concerns; harder to implement on small screens |

**Decision process:** This will be finalised at the sprint planning for the SAPASI form, with input from clinical users.

> **Until the UX decision is made:** Implement the data model and API fully (both options store the same 4-region data). The UI component is the only variable.

---

## 3. SAPASI Scoring Model

SAPASI is based on the Fleischer et al. (1994) validated instrument. It has **4 body regions**, each scored independently.

### 3.1 Body Regions

| Region | Abbreviation | Body Surface Weight |
|---|---|---|
| Head | H | 0.1 |
| Upper Limbs | U | 0.2 |
| Trunk | T | 0.3 |
| Lower Limbs | L | 0.4 |

> Weights sum to 1.0

### 3.2 Per-Region Inputs

For each region, the patient provides:

| Input | Label | Scale |
|---|---|---|
| `Coverage` | How much of the region is affected? | 0 = None, 1 = < 10%, 2 = 10–30%, 3 = 30–50%, 4 = > 50% |
| `Redness` | Redness/Erythema | 0 = None, 1 = Slight, 2 = Moderate, 3 = Marked, 4 = Very marked |
| `Thickness` | Thickness/Induration | 0 = None, 1 = Slight, 2 = Moderate, 3 = Marked, 4 = Very marked |
| `Scaliness` | Scaliness/Desquamation | 0 = None, 1 = Slight, 2 = Moderate, 3 = Marked, 4 = Very marked |

### 3.3 Scoring Formula

```
SeverityScore(region) = Redness + Thickness + Scaliness   (0–12)

RegionScore(region) = SeverityScore(region) × Coverage × RegionWeight

SAPASI Total = Sum of RegionScore across all 4 regions

Maximum possible SAPASI score = 72
```

Example:
```
Head:         Redness=2, Thickness=1, Scaliness=2 → Severity=5. Coverage=2. Weight=0.1 → 5×2×0.1 = 1.0
Upper Limbs:  Redness=3, Thickness=2, Scaliness=2 → Severity=7. Coverage=2. Weight=0.2 → 7×2×0.2 = 2.8
Trunk:        Redness=1, Thickness=1, Scaliness=1 → Severity=3. Coverage=1. Weight=0.3 → 3×1×0.3 = 0.9
Lower Limbs:  Redness=2, Thickness=2, Scaliness=1 → Severity=5. Coverage=3. Weight=0.4 → 5×3×0.4 = 6.0
SAPASI Total = 1.0 + 2.8 + 0.9 + 6.0 = 10.7
```

### 3.4 Interpretation

| Score | Severity |
|---|---|
| 0–10 | Mild |
| 10–20 | Moderate |
| 20+ | Severe |

---

## 4. Database Table

See DSD-001 §4.5 for the `SapasiSubmissions` DDL.

Columns: `SubmissionId`, `Chid`, `PappFupId`, `SubmittedAt`, `HeadCoverage`, `HeadRedness`, `HeadThickness`, `HeadScaliness`, `UlCoverage`, `UlRedness`, `UlThickness`, `UlScaliness`, `TrunkCoverage`, `TrunkRedness`, `TrunkThickness`, `TrunkScaliness`, `LlCoverage`, `LlRedness`, `LlThickness`, `LlScaliness`, `TotalScore`, `FormStatus`.

---

## 5. API Contract

**POST** `/api/forms/sapasi`  
**Auth:** Bearer token required

```json
{
  "pappFupId": 67,
  "head":        { "coverage": 2, "redness": 2, "thickness": 1, "scaliness": 2 },
  "upperLimbs":  { "coverage": 2, "redness": 3, "thickness": 2, "scaliness": 2 },
  "trunk":       { "coverage": 1, "redness": 1, "thickness": 1, "scaliness": 1 },
  "lowerLimbs":  { "coverage": 3, "redness": 2, "thickness": 2, "scaliness": 1 }
}
```

Response `201 Created`:
```json
{
  "submissionId": 1005,
  "totalScore": 10.7,
  "severityBand": "Moderate",
  "submittedAt": "2024-01-15T11:00:00Z"
}
```

**Validation:**
- All coverage and severity values must be 0–4.
- All 4 regions required (no partial submission).
- `totalScore` is computed server-side; do not accept from client.

---

## 6. UX Requirements (Known Constraints)

Regardless of which interaction model (Option A or B) is chosen:

- All 4 regions must be completed before submission is enabled.
- Each region must show its 4 inputs (coverage + redness + thickness + scaliness).
- A running total score should be displayed as the patient fills in each region.
- The final score and severity band are shown on the completion screen.
- Accessibility: must work for patients with limited dexterity (minimum 44×44px touch targets; do not require precise drag operations on small areas).
- The form should include brief instructions explaining what each rating means (e.g., a legend for the coverage percentages).
