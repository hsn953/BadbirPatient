# URD-001 – User Requirements Document
## BADBIR Patient Application

> **Document ID:** URD-001  
> **Version:** 0.1 (Draft)  
> **Status:** In Review  
> **Source:** `docs/requirements/masterrequirements.md` + legacy code analysis  
> **Last Updated:** 2026-03-25

---

## 1. Purpose & Scope

This document captures all requirements from the perspective of the **end user** (the patient). It is intentionally non-technical — it describes *what the user needs to do* and *how the system should behave*, not *how it is built*. It is the primary input into the Software Requirements Specification (SRS-001).

**In scope:** Patient-facing features of the BADBIR Patient Application (web + mobile).  
**Out of scope:** Clinician-facing features, Admin Communication Hub, backend infrastructure details.

---

## 2. User Personas

### 2.1 Primary User — Adult Patient

| Attribute | Description |
|---|---|
| Age range | 16 years and over |
| Technical ability | Low to moderate — must be designed for non-technical users |
| Context of use | At home, in clinic (often poor Wi-Fi), on both phones and browsers |
| Primary goal | Complete assigned questionnaires before/during a clinic visit |
| Key pain points | Forgetting passwords, losing form progress, long forms, poor connectivity |

### 2.2 Secondary User — Young Patient (Paediatric, under 16)

- Out of scope for initial release.
- System must prompt re-consent as adult when turning 16.

---

## 3. User Goals (Epics)

| ID | Goal | Priority |
|---|---|---|
| UG-01 | Register for the system and provide consent | Must Have |
| UG-02 | Log in securely with minimal friction | Must Have |
| UG-03 | View which questionnaires need to be completed | Must Have |
| UG-04 | Complete assigned questionnaires, even offline | Must Have |
| UG-05 | Skip a form without losing all progress | Must Have |
| UG-06 | Resume a partially completed sequence | Must Have |
| UG-07 | Recover account without email/password | Must Have |
| UG-08 | Receive timely reminders about upcoming clinic visits | Should Have |
| UG-09 | Control notification and communication preferences | Should Have |
| UG-10 | View historical form submissions | Could Have |
| UG-11 | Log in using device biometrics | Should Have |

---

## 4. User Stories

### 4.1 Registration & Consent

#### US-01: QR Code / Self-Initiated Registration

> **As a** new patient who has been given a QR code by my clinic,  
> **I want to** scan the code and complete a short sign-up form on my own device,  
> **So that** I can begin filling in questionnaires without waiting for clinic staff.

**Acceptance Criteria:**
- AC-01.1: The QR code directs me to a public signup page specific to my clinical centre.
- AC-01.2: I can see a list of clinical centres and select my own if I navigate directly.
- AC-01.3: I must review and accept the eligibility criteria before proceeding.
- AC-01.4: I can fill in a demographics form (name, DOB, NHS/CHI/BADBIR number).
- AC-01.5: I am placed in a "Holding / Pending Confirmation" state after submitting.
- AC-01.6: I can immediately access and fill in baseline questionnaires while in the holding state.
- AC-01.7: I am clearly informed that my data will be permanently deleted if my clinician does not confirm me within **14 days**.

#### US-02: Clinician-Initiated Registration (Email Invite)

> **As a** patient who has received an email invitation from my clinic,  
> **I want to** click the link and confirm my pre-filled details,  
> **So that** I can complete consent and start my questionnaires quickly.

**Acceptance Criteria:**
- AC-02.1: The email link takes me to a secure signup page with my demographics pre-populated.
- AC-02.2: I can review and confirm or correct my pre-filled details.
- AC-02.3: Once confirmed, I complete the electronic consent form.
- AC-02.4: After consenting, I proceed directly to my baseline questionnaires.
- AC-02.5: My account is active immediately (no 14-day waiting period for this pathway).

#### US-03: Electronic Consent

> **As a** new patient,  
> **I want to** review and digitally sign the research consent form,  
> **So that** I understand what I am agreeing to and can provide informed consent.

**Acceptance Criteria:**
- AC-03.1: The consent form is displayed clearly and in full before I can submit.
- AC-03.2: I must explicitly tick or select "I Agree" — pre-ticked boxes are not allowed.
- AC-03.3: A record of my consent (timestamp, IP, version of form) is stored in the database.

---

### 4.2 Authentication

#### US-04: Standard Email/Password Login

> **As a** registered patient,  
> **I want to** log in with my email and password,  
> **So that** I can access my questionnaires.

**Acceptance Criteria:**
- AC-04.1: I log in using my email address (not a user ID or username).
- AC-04.2: Sessions are long-lived (configurable, e.g. 30 days) to reduce friction.
- AC-04.3: I receive a clear error message if my credentials are wrong.
- AC-04.4: After 5 consecutive failed attempts, my account is temporarily locked.

#### US-05: Biometric Login (Mobile Only)

> **As a** patient using the BADBIR mobile app,  
> **I want to** log in using my phone's FaceID or fingerprint,  
> **So that** I don't have to remember my password every time.

**Acceptance Criteria:**
- AC-05.1: On first successful email/password login, the app offers to enable biometric login.
- AC-05.2: Once enabled, I can log in by authenticating with my device biometrics.
- AC-05.3: If biometric auth fails, I can fall back to email/password.
- AC-05.4: Biometric data is never sent to the server — it is used only to unlock a stored token.

#### US-06: Account Recovery ("Re-Register to Reset")

> **As a** patient who has forgotten my password or email address,  
> **I want to** verify my identity using my date of birth, initials, and NHS/CHI/BADBIR number,  
> **So that** I can set a new email and password without needing a recovery email.

**Acceptance Criteria:**
- AC-06.1: I can initiate the recovery process from the login screen.
- AC-06.2: The system verifies me using: **Date of Birth** + **Initials** + **one of: NHS Number, CHI Number, or BADBIR Study Number**.
- AC-06.3: Upon successful verification, I can provide a new email address and password.
- AC-06.4: All existing JWT tokens for my account are immediately invalidated.
- AC-06.5: I am automatically logged in with a new token after resetting.
- AC-06.6: The recovery event is fully logged in the audit trail.

---

### 4.3 Questionnaire Completion

#### US-07: View Assigned Questionnaires (Dashboard)

> **As a** registered patient,  
> **I want to** see which questionnaires have been assigned to me for this visit,  
> **So that** I know exactly what I need to complete.

**Acceptance Criteria:**
- AC-07.1: My dashboard shows a list of forms for the current follow-up period.
- AC-07.2: Each form shows its status: Not Started, In Progress (not applicable — see US-09), Completed, or Skipped.
- AC-07.3: The form sequence number and estimated duration are displayed.
- AC-07.4: Completed forms are locked and cannot be refilled.
- AC-07.5: Conditional forms (HAQ, CAGE) only appear if the trigger condition is met.

#### US-08: Sequential Form Navigation

> **As a** patient,  
> **I want to** complete forms in a consistent, booklet-like order,  
> **So that** the experience matches the paper questionnaire I might be familiar with.

**Acceptance Criteria:**
- AC-08.1: Forms are presented one at a time in a fixed sequence.
- AC-08.2: I cannot navigate to form N+1 before completing or skipping form N.
- AC-08.3: Once submitted, a form is locked and I cannot go back and edit it.
- AC-08.4: Upon completing all forms, I see a "Thank You / Submission Complete" screen.

#### US-09: Warn on Mid-Form Navigation Away

> **As a** patient filling in a form,  
> **I want to** be warned before navigating away,  
> **So that** I don't accidentally lose my in-progress answers.

**Acceptance Criteria:**
- AC-09.1: If I tap "Home" or navigate away mid-form, a confirmation dialog appears.
- AC-09.2: The dialog states that my current form answers will be lost.
- AC-09.3: If I confirm, the form is reset. If I cancel, I return to the form.
- AC-09.4: No partial form data is saved to the database.

#### US-10: Skip a Form

> **As a** patient who finds a form too long or not applicable,  
> **I want to** be able to skip it individually,  
> **So that** I don't abandon the entire session.

**Acceptance Criteria:**
- AC-10.1: A "Skip this form" option is visible on each form.
- AC-10.2: Tapping Skip shows a confirmation: *"Are you sure you want to skip [Form Name]?"*
- AC-10.3: There is no "Skip All" option.
- AC-10.4: A skipped form is marked as Skipped for this follow-up period.
- AC-10.5: I proceed to the next form after confirming the skip.

#### US-11: Resume an Interrupted Session

> **As a** patient who left the app partway through a form sequence,  
> **I want to** resume from where I left off,  
> **So that** I don't have to re-enter forms I've already completed.

**Acceptance Criteria:**
- AC-11.1: When I return to the app, the dashboard shows my progress (completed, skipped).
- AC-11.2: The "Continue" action takes me to the next uncompleted form in the sequence.
- AC-11.3: Already-completed forms in this follow-up remain locked.

---

### 4.4 Offline (Mobile Only)

#### US-12: Fill Forms Without Internet (Store and Forward)

> **As a** patient in a hospital clinic with poor Wi-Fi,  
> **I want to** complete my forms even without internet,  
> **So that** I don't miss the research window.

**Acceptance Criteria:**
- AC-12.1: The mobile app detects loss of connectivity and notifies me.
- AC-12.2: I can still complete and submit forms while offline.
- AC-12.3: Offline submissions are stored locally (SQLite) on my device.
- AC-12.4: Once connectivity is restored, the app automatically syncs pending submissions in the background.
- AC-12.5: I receive a confirmation when offline submissions have been successfully synced.
- AC-12.6: Offline submissions that fail to sync after reconnection are retried with exponential backoff.
- AC-12.7: *This feature applies only to the native MAUI mobile app. The web app requires a live connection.*

---

### 4.5 Notifications & Preferences

#### US-13: Receive Reminders About Upcoming Visits

> **As a** patient with an upcoming clinic visit,  
> **I want to** receive reminders to fill in my forms,  
> **So that** I complete them around the right time.

**Acceptance Criteria:**
- AC-13.1: Reminders are sent via email and/or push notification (based on my preferences).
- AC-13.2: Reminders are triggered based on my **Next Visit Date**.
- AC-13.3: I receive a build-up of reminders in the days before my visit.
- AC-13.4: I receive a day-of reminder on the actual visit date.
- AC-13.5: I receive overdue reminders if I haven't submitted after the visit date passes.

#### US-14: Set My Clinic Visit Date

> **As a** patient who has received an NHS appointment letter,  
> **I want to** enter my actual clinic date into the app,  
> **So that** reminders are sent at the right time.

**Acceptance Criteria:**
- AC-14.1: I can input or confirm my next visit date from within the app.
- AC-14.2: The system defaults to exactly 6 months from my last follow-up if I don't enter a date.
- AC-14.3: The clinician can also override this date from the Clinician System.

#### US-15: Manage Communication Preferences

> **As a** patient,  
> **I want to** control which notifications I receive,  
> **So that** I'm not spammed with unwanted communications.

**Acceptance Criteria:**
- AC-15.1: I can toggle **clinical form reminders** (push and/or email) on/off.
- AC-15.2: I can separately opt in/out of **informative communications** (newsletters, updates).
- AC-15.3: These preferences are saved to my profile and respected by the notification system.
- AC-15.4: Turning off clinical reminders does NOT affect my data collection responsibilities.

---

## 5. Non-Functional User Requirements

| ID | Requirement | Category |
|---|---|---|
| NFU-01 | The app must load the dashboard within 3 seconds on a 4G connection | Performance |
| NFU-02 | Forms must be usable on screen widths as small as 360px | Accessibility |
| NFU-03 | All form labels and questions must meet WCAG 2.1 AA contrast standards | Accessibility |
| NFU-04 | Patient-identifiable data must never be visible in URL parameters | Security |
| NFU-05 | Sessions remain active for at least 30 days without requiring re-login | Usability |
| NFU-06 | Biometric login must fall back gracefully if the device does not support it | Usability |
| NFU-07 | A patient can only submit a given form sequence once per calendar day | Business Rule |
| NFU-08 | All error messages must be in plain English with actionable guidance | Usability |

---

## 6. Out of Scope (Patient App)

The following are **explicitly out of scope** for the Patient Application:

- Clinician-entered data (drug prescriptions, PASI, adverse events, comorbidities).
- Admin Communication Hub (notification template management, scheduling).
- NHS Login / Single Sign-On integration (must not be blocked by architecture, but not implemented in v1).
- Paediatric workflows (re-consent on turning 16).
- Future gamification or incentive features.

---

## 7. Open Questions for the Client

| # | Question | Impact |
|---|---|---|
| OQ-01 | Is the EuroQol version EQ-5D-3L (as suggested by legacy 3-level answers) or EQ-5D-5L (as mentioned in the master requirements)? | Form field count, scoring algorithm, licensed question text |
| OQ-02 | What is the exact HADS question text and scoring direction? The legacy model stores q01–q14 as integers but the direction alternates. | Scoring algorithm accuracy |
| OQ-03 | Are the DLQI question texts licensed from the Cardiff University group? What licensed text can be used? | Legal compliance |
| OQ-04 | What is the exact SAPASI body-map UI spec? (score per body region × coverage × severity) | SAPASI form design |
| OQ-05 | Should the "1x per day" submission limit apply to individual forms or the entire sequence? | Business rule |
| OQ-06 | Is the encryption algorithm for PII already implemented in the Clinician System? When will the code be shared? | API encryption layer |
| OQ-07 | What should the 14-day expiry look like from the patient's perspective — are they notified? | UX for holding state |
| OQ-08 | Should the Lifestyle and Medical Problems sections be one page or two separate forms in the sequence? | Form navigation design |
| OQ-09 | What is the exact follow-up scheduling logic for Month 48+ patients vs Month 6–36 patients? | Dashboard / form assignment |
| OQ-10 | Is biometric auth required for web browser, or native mobile only? | Scope clarification |
