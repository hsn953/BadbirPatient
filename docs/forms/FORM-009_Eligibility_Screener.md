# FORM-009 – Eligibility Screener
## Patient Self-Registration — Pre-Qualification
## BADBIR Patient Application

> **Document ID:** FORM-009  
> **Version:** 0.1  
> **Status:** New — derived from eligibilityrequirements.md  
> **Last Updated:** 2026-03-26  
> **Source:** `docs/requirements/eligibilityrequirements.md`

---

## 1. Overview

| Attribute | Value |
|---|---|
| Form Name | Eligibility Screener |
| Applicable To | Self-registration Pathway A (patient-initiated via QR code or direct URL) |
| NOT applicable to | Pathway B (clinician-initiated invite — screener bypassed) |
| Placement | **Before** any demographic or consent form; first step of Pathway A |
| Estimated Duration | < 1 minute |

---

## 2. Purpose

Because patients will not know their PASI scores, cohort designation, or other clinical criteria, the patient app implements a simplified 3-question pre-screener. This screener:
- Prevents clearly ineligible patients from filling in registration forms unnecessarily.
- Confirms the patient is an **adult** (16+) for Phase 1 (adult-only scope).
- Does NOT determine cohort placement — that is the clinician's responsibility.

---

## 3. Screener Questions (Exact text from eligibilityrequirements.md)

Questions are presented sequentially. If any required answer is NO, the flow halts.

### Q1: Diagnosis

**Text:** *"Have you been diagnosed with psoriasis by a dermatologist?"*

| Answer | Action |
|---|---|
| **Yes** | Proceed to Q2 |
| **No** | → Show rejection message (Section 4), halt registration |

---

### Q2: Treatment Window

**Text:** *"Have you started a new prescribed pill, injection, or light therapy (systemic treatment) for your psoriasis in the last 6 months?"*

| Answer | Action |
|---|---|
| **Yes** | Proceed to Q3 |
| **No** | → Show rejection message (Section 4), halt registration |

---

### Q3: Age & Consent

**Text:** *"Are you currently 16 years of age or older?"*

| Answer | Action |
|---|---|
| **Yes** | → Proceed to standard adult registration (identity verification) |
| **No** | → Show paediatric deferral message (Section 5), halt registration |

---

## 4. Rejection Message (Q1 or Q2 = No)

**Exact text from eligibilityrequirements.md:**

> *"Based on your answers, you may not be eligible to join BADBIR at this exact time, or your clinical team may need to register you directly. Please speak to your dermatologist or dermatology nurse at your next appointment to confirm your eligibility."*

No further action. No data is stored.

---

## 5. Paediatric Deferral Message (Q3 = No)

**Exact text from eligibilityrequirements.md:**

> *"Currently, patients under 16 must be registered directly by their clinical team. Please speak to your dermatologist at your next appointment."*

No further action. No data is stored.

---

## 6. Clinical Background (For API Context — Not Shown to Patient)

The app does NOT implement these rules — they are enforced by the clinician during the 14-day holding account review.

| Cohort | Notes |
|---|---|
| **Biologic** | No minimum PASI/DLQI required |
| **Small molecule** | Must be biologic-naïve (except specific exceptions). No minimum scores required. |
| **Conventional** | Must be biologic-naïve AND have PASI ≥10 AND DLQI ≥11, UNLESS switching between conventional therapies within 3 months |
| **Paediatric (<16)** | Any systemic treatment for psoriasis. Exempt from PASI/DLQI requirements. Phase 1: clinician-registered only. |

> **Important architectural rule:** The Patient App will NOT calculate PASI or DLQI thresholds during registration. Cohort assignment is solely the clinician's responsibility in the Clinician System.

---

## 7. API Contract

The screener is a **UI-only flow** — no API call is made for Q1, Q2, Q3 answers. They are not persisted.

Only when all 3 answers are Yes does the patient proceed to the identity verification step:

**POST** `/api/registration/verify-identity` (see API-001 Section 4.1)

---

## 8. UX Requirements

- Each question displayed on its own full-screen card, one at a time.
- Large Yes/No buttons (minimum 44×44px touch targets).
- Progress indicator: "Step 1 of 3", "Step 2 of 3", "Step 3 of 3".
- Rejection and deferral messages displayed in a calm, non-alarmist style.
- Back navigation allowed between Q1→Q2→Q3 (before submission).
- No data is stored or transmitted until the patient passes all 3 questions.
