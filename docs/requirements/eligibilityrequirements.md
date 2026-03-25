# BADBIR Patient Registration - Eligibility & Triage Rules

## 1. Purpose
This document outlines the clinical eligibility criteria for the BADBIR study and defines how the Patient Application should implement these checks during the self-registration flow (Pathway A).

## 2. The Clinical Domain Rules (For Backend Context)
GitHub Copilot must understand these underlying clinical rules, even though the patient will not be asked to validate all of them directly.

* **The 6-Month Rule (Crucial):** All patients must have started, or switched to, an eligible systemic, biologic, or small molecule therapy **within the last 6 months** of consenting.
* **Paediatric Patients (<16 years at consent):** * Eligible if starting *any* systemic treatment for psoriasis.
  * Exempt from all minimum PASI and DLQI score requirements.
* **Biologic Cohort:** * No minimum PASI or DLQI score required for eligibility (though scores must be recorded by the clinician).
* **Small Molecule Cohort (e.g., Apremilast, Dimethyl Fumarate, Deucravacitinib):** * Must be biologic-naïve (except for specific routing with Deucravacitinib).
  * No minimum PASI or DLQI score required.
* **Conventional Cohort (e.g., Methotrexate, Ciclosporin):**
  * Must be biologic-naïve.
  * **Strict Thresholds:** Must have a PASI score of ≥ 10 and a DLQI score of ≥ 11 at the start of therapy.
  * *Exception:* No minimum scores are required if the patient is switching directly between conventional therapies with a gap of less than 3 months.

## 3. The Patient-Facing Screener (App Implementation)
Because patients will not know their PASI scores or cohort designations, the Patient Application must implement a simplified, user-friendly "Eligibility Screener" at the very beginning of the registration flow. 

**UI Flow & Questions:**
Before a patient can fill out the demographic or consent forms, they must pass this screener.

1. **Question 1: Diagnosis**
   * *Text:* "Have you been diagnosed with psoriasis by a dermatologist?"
   * *Logic:* Must be YES to proceed.
2. **Question 2: Treatment Window**
   * *Text:* "Have you started a new prescribed pill, injection, or light therapy (systemic treatment) for your psoriasis in the last 6 months?"
   * *Logic:* Must be YES to proceed.
3. **Question 3: Age & Consent**
   * *Text:* "Are you currently 16 years of age or older?"
   * *Logic:* If YES, proceed to standard adult registration. If NO, display a message stating: *"Currently, patients under 16 must be registered directly by their clinical team. Please speak to your dermatologist at your next appointment."* (Based on the Phase 1 adult-only scope).

## 4. Rejection & Deferral Logic
* If a patient answers **NO** to Question 1 or 2, the application must gracefully halt the registration process.
* **User Message:** *"Based on your answers, you may not be eligible to join BADBIR at this exact time, or your clinical team may need to register you directly. Please speak to your dermatologist or dermatology nurse at your next appointment to confirm your eligibility."*

## 5. Clinician Validation Boundary
* **Important Architectural Rule:** The Patient Application **will not** attempt to calculate PASI or DLQI thresholds during registration to determine cohort placement. 
* The Patient App will simply ensure the patient passes the basic 6-month treatment window. The strict cohort assignment (Biologic vs. Conventional) and score validation (PASI ≥ 10) is the sole responsibility of the clinician using the separate Clinician System when they approve the patient's holding record within the 14-day window.
