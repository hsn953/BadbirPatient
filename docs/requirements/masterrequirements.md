# BADBIR Patient Application - Master Requirements Specification

## 1. Project Overview

The BADBIR Patient Application is a modernization of a legacy patient portal. The goal is to transition from a fragmented architecture (older .NET 4.5 web portal, separate .NET 6 API, and Xamarin mobile app) into a unified, modern cross-platform solution.

* **GitHub Repository:** https://github.com/hsn953/BadbirPatient/

* **Legacy Live Portal:** https://patient.badbir.org/

* **Primary Function:** A data-collection tool focused on complex medical questionnaires (EuroQol, HAQ, HADS) with future capabilities for patient data visualization (charts/graphs).

## 2. Architectural Blueprint & Tech Stack

GitHub Copilot must adhere strictly to this architecture when generating code:

* **Target Framework:** **.NET 10** across all projects.

* **Paradigm:** .NET Mono-repo.

* **Backend:** .NET 10 Web API.

* **Frontend:** .NET MAUI Blazor Hybrid.

* **UI Sharing:** 95% of the UI (Blazor components, forms, layouts) lives in a shared Razor Class Library (`BADBIR.UI.Components`), consumed by both the .NET MAUI app and the Blazor Web App.

* **Database:** Existing SQL Server 2022. The API will use **Entity Framework Core 10 (Database-First)** to scaffold entities from the existing schema.

* **API Design & Security:** RESTful endpoints secured via built-in .NET 10 Identity endpoints and **JWT Bearer tokens**. The authentication architecture must be built using standard OpenID Connect (OIDC) patterns to allow future external providers.

* **API Documentation:** Must use native .NET 10 `Microsoft.AspNetCore.OpenApi` (Do NOT use Swashbuckle/third-party Swagger).

## 3. Hosting & Deployment Environments

* **Phase 1 (Current):** On-premise University Hosting. Standalone IIS deployment on Windows Server 2022.

* **Phase 2 (Future):** AWS Cloud. The application (API and Web) will be containerized (Docker) and deployed to AWS cloud services.

* *Requirement:* All configuration, logging, and file-handling logic must be environment-agnostic to ensure a seamless transition to Docker/AWS later.

## 4. Explicit Directives for AI (GitHub Copilot)

When asked to implement a feature from this document:

1. **Check Legacy First:** Always search the `legacy_reference/` folders (`old_web_portal_net45`, `old_api_decompiled`, `old_xamarin_app`) to understand how it was done previously, specifically for form validation rules, data types, and scoring algorithms.

2. **Modernize:** Do not copy legacy architectural patterns (like NetTiers or bloated stored procedure calls). Translate the *business logic* into modern C# 10 and EF Core LINQ.

3. **Share UI:** Always build UI components in the `BADBIR.UI.Components` shared library, using a modern component library (e.g., MudBlazor).

## 5. User Requirements & Roles

* **Patient:** Needs to receive email invitations, provide electronic consent, log in seamlessly (supporting device biometrics), view assigned questionnaires, submit forms securely (both online and offline), and view historical form data.

## 6. Use Cases & Expected Behaviors

* **Use Case 1 (Registration, Electronic Consent & Frictionless Auth Flow):**

  * **Identifier:** The system will use the patient's **Email Address** as the primary username/identifier. No separate, arbitrary "User IDs" will be required.

  * **Pathway A: Patient-Initiated Registration (On-Site/QR Code)**

    * **Trigger:** Patient scans a center-specific QR code or navigates to the public signup page, manually selecting their clinical center from a list.

    * **Action:** Patient reviews eligibility criteria, fills out a demographics sheet, and completes the electronic consent form.

    * **Holding State:** The patient is immediately allowed to fill out baseline questionnaires. However, their account and data are placed in an "Unconfirmed / Holding" table.

    * **Clinician Action:** The submitted consent appears in an inbox on the *Clinician System*. The clinician must review, match the record to an existing/new patient, and confirm the consent.

    * **Data Expiry (14-Day Rule):** If the clinician does not confirm the patient within 14 days, the patient's account and all submitted data will be automatically and permanently deleted.

  * **Pathway B: Clinician-Initiated Registration (Email Invite)**

    * **Trigger:** The clinician enters the patient's initial demographics into the *Clinician System* and triggers an email invite.

    * **Action:** The patient clicks the email link, is taken to the signup page, and reviews their pre-filled demographics.

    * **Confirmation:** The patient confirms the details, completes the electronic consent, and proceeds immediately to baseline questionnaires.

  * **Data Entry Boundaries (Clinician vs. Patient):**

    * *Clinician Only:* Patient drugs, diagnosis confirmation, comorbidities (at baseline), and adverse events (at follow-ups).

    * *Patient Only/Shared:* Patients enter demographics, consent, and questionnaires. This data is surfaced to the clinician system, where the clinician can review, accept, or override the patient's form data.

  * **Account Verification/Matching:** Patients are verified against the database using three data points: **Date of Birth** + **Initials** + **1 of 3 IDs (BADBIR ID, NHS Number, or CHI Number)**.

  * **Login & Biometrics:** Once registered, patients can log in via email and password. On mobile devices, the MAUI app must support native biometric authentication (FaceID/Fingerprint) and a "save password" feature for seamless re-entry.

  * **Frictionless Account Recovery ("Re-Register to Reset"):**

    * If a patient forgets their password or email, there are no "secret questions" or complex reset links.

    * They simply undergo the Registration/Verification process again (providing DOB + Initials + NHS/CHI/BADBIR ID).

    * Upon successful match, the system allows them to provide a new email and password.

    * The system immediately invalidates any older JWT tokens, applies the new credentials, and logs them in with a new long-lived session token.

  * **Future Integration (NHS Login / Single Sign-On):**

    * *Scope:* Not part of the initial release, but the architecture must not block it.

    * *Requirement:* The UI and underlying authentication layer must support co-existing dual-login methods (Standard Email/Password alongside an external OpenID Connect provider like NHS Digital).

  * **Audit Logging:** Every registration, re-registration, token invalidation, and login event MUST be heavily audited and logged in the database to track user access patterns securely.

* **Use Case 2 (Form Interruption & Progression):**

  * **Sequential Navigation:** Forms must be presented in a consistent sequence that mimics the physical booklet experience.

  * **Completed Forms:** Once a form within a sequence is completed and submitted, it is locked for that session. Users cannot navigate back to refill it.

  * **No Partial Saves:** If a user navigates away from the app or hits the "Home" button mid-form, they must be warned that progress on *that specific form* will be lost and reset. Partial forms are not saved.

  * **Resuming:** If a sequence is broken, upon returning, the user resumes exactly where they left off (the next uncompleted form in the sequence).

  * **Intentional Skipping:** To prevent survey abandonment (e.g., getting stuck on a long form), users can choose to skip a form.

    * Skipping requires explicit confirmation (e.g., "Are you sure you want to skip this form?").

    * Skipping must be done individually per form (no "Skip All" button).

    * Skipped forms are marked as skipped for that period and the user is moved to the next form.

  * **Completion:** Once all required forms in the sequence are submitted (or intentionally skipped), the user is presented with a "Thank You" screen confirming completion.

* **Use Case 3 (Offline Data Capture & Reconnection - Native Mobile Only):**

  * **Context:** Hospital environments often have poor cellular signals and restricted Wi-Fi.

  * **Requirement:** The **native MAUI mobile applications (Android/iOS)** must support a "Store and Forward" offline mechanism. *Note: The Blazor Web App does not require offline support, as it requires an active connection to load.*

  * **Behavior:** If a patient is filling out a form on the mobile app and loses connectivity, the system must locally cache the form submissions (e.g., using a local SQLite database).

  * **Syncing:** Once the mobile device detects an active internet connection, the system should automatically sync the pending form submissions to the API in the background.

## 7. Core Features & Medical Forms

### 7.1 The HAQ Form (Health Assessment Questionnaire)

* **Objective:** Collect patient mobility and lifestyle capability data.

* **Conditional Trigger:** Only required if the patient indicates a diagnosis of psoriatic/inflammatory arthritis. If not, the form is entirely skipped.

* **UX Requirement:** Because HAQ is long, the UI must divide it into logical sections/pages and display an estimated duration to complete. Users frequently skip this, so the individual skip confirmation must be frictionless but explicit.

### 7.2 The HADS Form (Hospital Anxiety and Depression Scale)

* **Objective:** Assess patient mental well-being.

### 7.3 The EuroQol Form

* **Objective:** Standardized measure of health status.

* **UX Requirement:** Must adhere to strict visual specifications (to be provided separately) to maintain its validated scoring integrity.

### 7.4 SAPASI (Self-Administered Psoriasis Area and Severity Index)

* **Requirement:** This is a new implementation for the application and must be built from scratch.

* **UX Requirement:** Requires specific UI techniques (e.g., interactive body maps or sliders) to assist the user in calculating the score.

* **Data Storage:** The database must store both the individual component parts of the assessment as well as the total calculated score.

### 7.5 The CAGE Form (Alcohol Screening)

* **Conditional Trigger:** Only required if the patient has confirmed they consume alcohol. If they indicate they do not drink, the CAGE form is bypassed entirely.

## 8. Detailed Data Collection Boundary & Schedule

This section outlines precisely what data is collected, mapping it to either the Clinician (out of scope for the Patient App) or the Patient (in scope for the Patient App).

### 8.1 Security & Encryption at Rest (Overarching Rule)

* **Encrypted Fields:** Sensitive PII must be encrypted at rest: **Patient Name/Initials, NHS Number, CHI Number, Birth Place, Occupation.** This encryption will be done using a predefined service/algorithm which is in use in the clinician application and will be shared as code later on.

* **Implementation:** The .NET 10 API will handle encryption (before database insertion) and decryption (before returning to the UI) using a custom configured AES algorithm.

* ***Architectural Note for Future NHS Login:*** Because the system will eventually need to match incoming NHS Login tokens against the database, the encryption strategy for the NHS Number column must allow for secure, deterministic lookups (e.g., using deterministic encryption or utilizing a secure, salted hash alongside the encrypted value).

### 8.2 Baseline Data Breakdown (Month 0)

**A. Clinician-Entered Data (Out of Scope for Patient App)**

* Psoriasis Details, Baseline Severity, Treatments, Comorbidities, Skin Details, Laboratory & Measurements.

**B. Patient-Entered Data (In Scope for Patient App)**

* Demographics & Lifestyle (encrypted fields), Psoriatic Arthritis Screening, Questionnaires (DLQI, EuroQol, HADS, CAGE, HAQ, SAPASI, PGA).

### 8.3 Follow-up 1 to 6 Data Breakdown (Months 6, 12, 18, 24, 30, 36)

**A. Clinician-Entered Data (Out of Scope for Patient App)**

* Treatment Updates, Adverse Events, Laboratory & Measurements.

**B. Patient-Entered Data (In Scope for Patient App)**

* Medical Updates, Lifestyle Updates, Questionnaires.

* **Logistics (Next Visit Date):** The system defaults the next visit to exactly 6 months. However, the patient can input/confirm their *actual* next anticipated clinic visit date (often derived from NHS letters). **Note:** The clinician can also override and update this exact date via the Clinician System.

### 8.4 Follow-up 7+ Data Breakdown (Month 48, 60, 72, 84, etc.)

**A. Clinician-Entered Data (Out of Scope for Patient App)**

* Treatment changes, Adverse events, PASI/PGA, Weight/Waist (No lab blood values required).

**B. Patient-Entered Data (In Scope for Patient App)**

* Frequency Change (Annual), Medical Updates, Lifestyle Updates, Questionnaires.

### 8.5 Paediatric & Age-Specific Workflows

* **Current Iteration Scope:** The primary focus for this initial development phase is exclusively on **adult patients (16+ years old)**.

* **The "Re-consent" Workflow:** Paediatric patients DO NOT automatically become adults in the system upon turning 16. The system must prompt them to "Re-consent as an Adult".

## 9. Patient Engagement & Retention Strategy (High Uptake)

To ensure high patient uptake and continuous data entry, the application is designed around a "low friction, high reward" philosophy:

* **Seamless Re-entry:** Passwords are saved, sessions are long-lived, and biometric logins are highly encouraged.

* **The "Data Drop-Off" Model:** Patients are never blocked from providing data (within the 1x/day limit). The app should feel like a simple drop-off point rather than a rigid medical gateway.

* **Future Incentives & Value Adds:** Support for informational tips, targeted offers, and gamification elements (pending ethical approval).

## 10. Communications & Notifications Strategy

To maximize data collection around the critical window of the patient's actual clinic visit, the system requires a robust, dual-channel communication engine.

### 10.1 Notification Channels

* **Email:** Delivered via an integrated mail service (e.g., AWS SES plugins).

* **Push Notifications:** Delivered directly to the native iOS/Android MAUI applications via a dedicated push subscription service.

### 10.2 Triggers & Scheduling Logic

The core driver for notifications is the **Next Visit Date** (which defaults to the 6-month/annual interval but can be explicitly set by the patient based on their NHS appointment letters, or overridden by the clinician).

* **The Build-Up:** Reminders sent a set number of days *prior* to the next visit date to prepare the patient.

* **Day-Of:** A primary notification sent on the exact date of the clinic visit, requesting the patient to fill out the forms (as this data best correlates with the clinician's in-clinic PASI/PGA measurements).

* **Overdue (ASAP):** If the visit date passes and data has not been submitted, follow-up "ASAP" reminders are triggered to salvage the research data window.

### 10.3 Patient Preferences & Opt-Outs (In-App)

Patients must have granular control over their communications within the Patient App's settings/profile area:

* **Clinical Reminders:** Ability to toggle push/email notifications for form reminders.

* **Informative Comms:** A separate, explicit opt-in/opt-out toggle for newsletters, study updates, and educational psoriasis content.

### 10.4 Admin Communication Hub (Out of Scope for Patient App)

* **Separation of Concerns:** The management of these notifications will **not** be built into the native Patient App.

* **Requirement:** A separate "Communication Hub" module will be built into the backend/Admin portal.

* **Capabilities:** This hub will allow study administrators to:

  * Customize email and push notification text/templates.

  * Schedule periodic/automated notification rules.

  * Schedule and send global broadcast notifications (e.g., "Merry Christmas from the BADBIR team" or widespread study updates)
