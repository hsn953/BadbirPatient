# MDS-001 – University of Manchester MDS Release Form
## BADBIR Patient Application — .NET MAUI (Android + iOS)

> **Document ID:** MDS-001  
> **Version:** 0.1  
> **Status:** Draft — pre-submission, items flagged ⚠️ need completing before submission  
> **Last Updated:** 2026-03-30  
> **Based on:** UoM MDS Release Standard Form (v1.0)  
> **Reference (old form):** `docs/requirements/Release 01 Form Android release1 PEB 090523.docx` (Xamarin Android, submitted March 2023)

---

## About This Document

The University of Manchester Mobile Development Service (MDS) **Deployment tier** requires a Release Form to be submitted before deploying an app to Google Play and the Apple App Store. The MDS will review the submission twice (included in the £250 base fee; £125 per additional review thereafter).

This document is the updated Release Form for the **new .NET MAUI rebuild** of the BADBIR Patient app. It covers both the **Android update** and the **new iOS submission**.

### What Changed vs the 2023 Xamarin Submission

| Category | Old (Xamarin, March 2023) | New (MAUI, this submission) | Significant? |
|---|---|---|---|
| Framework | Xamarin.Forms | **.NET MAUI / .NET 10** | ✅ Yes — complete rewrite |
| Platforms | Android only | **Android + iOS** | ✅ Yes — iOS is new |
| Bundle ID / Package | `Org.badbir.patient` | **`org.badbir.patientapp`** | ✅ Yes — corrected to match legacy App Store entry |
| Android update type | New app | **Update to existing** (same package name on Play Store) | ✅ Yes |
| iOS submission | Not submitted | **New submission** | ✅ Yes |
| Android version | 1.0.0 / Build 1 | **2.0.0 / Build 200** | ✅ Yes |
| Min Android SDK | API 23 (Android 6.0) | **API 29 (Android 10)** | ✅ Yes — raised for MAUI support |
| Target Android | API 33 (Android 13) | **API 34 (Android 14)** | Minor |
| Min iOS version | Not applicable | **iOS 16** | N/A |
| Libraries | Xamarin, Plugin.Fingerprint, Syncfusion Xamarin | **Microsoft.Maui (built-in), no Syncfusion** | ✅ Yes |
| Biometrics API | Plugin.Fingerprint | **Microsoft.Maui.Authentication (built-in)** | Minor — same intent |
| Architecture | Shared DB (legacy) | **Separate Patient DB + API integration** | ✅ Yes — improved security posture |
| Auth tokens | JWT in SecureStorage | **JWT in SecureStorage** (same) | No change |
| GitHub URL | `github.com/hsn953/bbPatientApp` | **`github.com/hsn953/BadbirPatient`** | Yes — new repo |

> **Important note on bundle ID:** The 2023 form showed `Org.badbir.patient` (incorrect casing and missing "app" suffix). The live Play Store entry and the legacy Xamarin app both used `org.badbir.patientapp`. **This document uses the correct value throughout.**

---

## 1. App Information

| Field | Value |
|---|---|
| App Type | Research |
| Data Management Plan # | Not Applicable |
| Ethics Submission Reference | IRAS: 32990 |
| Information Governance Risk Review # | IGRR: 1092 |
| App Name | BADBIR Patient |
| Developer Contact | Hassan Ali |
| Customer / Product Owner | BADBIR |
| **Apple Bundle ID** | **`org.badbir.patientapp`** |
| **Android Package Name** | **`org.badbir.patientapp`** |
| **Android Version Number** | **2.0.0** |
| **Android Build Number** | **200** |
| **iOS Version Number** | **2.0.0** |
| **iOS Build Number** | **200** |
| New App or Update to Existing | **Update (Android)** / **New (iOS)** |
| Validity Period | Not applicable — live for the duration of the BADBIR study, currently expected until 2028+ |

> **Version rationale:** The old app was 1.x (Xamarin). This is a complete rewrite — version 2.0.0 signals a breaking change to existing users. `versionCode` 200 is monotonically above any 1.x build codes. The same version number is used for both platforms for consistency in store listings and communications.

---

## 2. Code Base

### 2.1 Coding Standards

| Field | Value |
|---|---|
| Describe how your app meets the required coding standards | The app is developed in **.NET 10 (.NET MAUI)** following Microsoft's .NET coding guidelines. C# nullable reference types are enabled project-wide. All secrets and credentials are stored in environment variables — never in source code. Repository is private on GitHub with branch protection on `main`. All public branches are scanned via GitHub secret scanning. API input validation is applied at the controller layer. PII fields in the database are encrypted at rest using AES-256-CBC (see ADR-015 in the repository). |
| Where does the app display its version and build number | On the **login screen (bottom)** and the **About screen** in the form `v2.0.0 (Build 200)`. |

### 2.2 Version Control

| Field | Value |
|---|---|
| Describe your version control system | GitHub private repository. Feature branches merged to `main` via pull requests with at least one reviewer. Tags used for release versions (e.g. `v2.0.0-android`). |
| Describe how public branches have been screened for secrets | The repository is **private**. GitHub secret scanning is enabled. Environment variables are used for all secrets. Connection strings and JWT signing keys are in environment variables, never in `appsettings.json` committed to the repo. |
| GitHub URL | `https://github.com/hsn953/BadbirPatient` |
| Distribution Branch Name | `release/2.0.0-android` (Android) / `release/2.0.0-ios` (iOS) |

### 2.3 Encryption and Obfuscation

| Field | Value |
|---|---|
| Describe how your app uses encryption | **In transit:** All API calls use HTTPS (TLS 1.2+). The MAUI `HttpClient` enforces TLS. No HTTP allowed in production. **At rest on device:** The JWT refresh token is stored in `Microsoft.Maui.Storage.SecureStorage` (backed by Android Keystore / iOS Keychain). No other patient data is stored on device. **At rest in database:** Patient PII fields (`forenames`, `surname`, `title`, `NHS number`, `CHI number`) are encrypted in the Patient API server using AES-256-CBC before being written to the database. Encryption is server-side only — the mobile app handles no raw PII fields. |
| Describe obfuscation determination | The application does not require obfuscation. No reverse-engineering risk exists beyond normal HTTPS interception (which TLS prevents). The app logic is not security-sensitive — authentication is token-based server-side. |

### 2.4 Toolchains, Supported Architectures and Dependencies

| Field | Value |
|---|---|
| **Required frameworks / SDKs / libraries** | **.NET 10 MAUI** (Android workload, iOS workload) · `Microsoft.Maui.*` (built-in biometrics, storage, networking) · `Microsoft.AspNetCore.Components.WebView.Maui` (Blazor hybrid) · `Microsoft.Extensions.Http` (typed HTTP client) · `System.Text.Json` (built-in serialisation) · `Microsoft.Maui.Authentication` (biometrics — replaces Plugin.Fingerprint) |
| **Target platforms and versions** | Android 14 (API 34) · iOS 17 |
| **Minimum platform versions** | **Android 10 (API 29)** · **iOS 16** |
| Target architectures | ARM64 (primary); x86_64 for emulators |
| Supported idioms | Phone (portrait-locked); tablet layout deferred |
| Supported orientations | **Portrait only — locked** |
| Packaging format | **AAB** (Android App Bundle) for Play Store · **IPA** for App Store |
| Distribution model | Store (Google Play + Apple App Store) |
| **Permissions required and why** | `android.permission.INTERNET` — required for all API calls · `android.permission.USE_BIOMETRIC` / `android.permission.USE_FINGERPRINT` — for biometric login unlock · `NSFaceIDUsageDescription` (iOS Info.plist) — "BADBIR uses Face ID to unlock access to your saved login credentials." |

---

## 3. Code Signing Certificates and Key Stores

| Field | Value |
|---|---|
| Additional entitlements required (iOS only) | None beyond standard distribution entitlements. |
| Describe keystore / signing certificate storage practices | **Android:** The upload keystore for Google Play App Signing is stored in a university-managed secure credential store (not in the repository). Google Play App Signing manages the final signing key. **iOS:** The distribution certificate and provisioning profile are stored in the Apple Developer Console under the University of Manchester team account. The MDS team will manage the iOS signing certificates for the deployment tier submission. |

---

## 4. Testing

### 4.1 Testing Procedures

| Field | Value |
|---|---|
| Describe testing procedures and automated testing | **Unit tests:** xUnit test project (`tests/BADBIR.Api.Tests/`) with SQLite in-memory database — runs on every pull request via GitHub Actions CI. **Integration tests:** API tested via `WebApplicationFactory<Program>`. All form submission flows, authentication, and patient promotion endpoints are covered. **Manual device testing:** See device matrix below. |

### 4.2 Device Test Matrix

| OS | Device/Config | Device Type | Portrait ✓ | Landscape |
|---|---|---|---|---|
| Android 10 (API 29) | Emulator (min spec) | Emulator | ✓ | Not supported (locked) |
| Android 14 (API 34) | Emulator (target spec) | Emulator | ✓ | Not supported (locked) |
| Android 13 | Samsung Galaxy S10 | Physical | ✓ | Not supported (locked) |
| Android 14 | Google Pixel 8 (or similar) | Physical | ✓ | Not supported (locked) |
| iOS 16 | iOS Simulator (min spec) | Simulator | ✓ | Not supported (locked) |
| iOS 17 | iOS Simulator (target spec) | Simulator | ✓ | Not supported (locked) |
| iOS 17 | iPhone 14 Pro (or similar) | Physical | ✓ | Not supported (locked) |

### 4.3 Test Credentials

| Field | Value |
|---|---|
| Test username | ⚠️ To be provided at submission time |
| Test password | ⚠️ To be provided at submission time |
| Instructions for tester | The app requires an account in the BADBIR staging environment. The MDS review team will use a test patient account. Forms can only be submitted once per follow-up window — if testing requires multiple submissions, contact the developer to reset the test account. The app connects to the **staging API** (`https://staging-api.badbir.org`) during the review period. |

---

## 5. Documentation

### 5.1 Technical Implementation

| Section | Location |
|---|---|
| App Overview | `docs/requirements/URD-001_User_Requirements.md` |
| Requirements | `docs/requirements/SRS-001_Software_Requirements_Specification.md` |
| UI Implementation | `docs/mockups/index.html` (HTML mockup screens — Registration, Baseline, Follow-Up) |
| Feature Implementation | `docs/api/API-001_Endpoint_Specification.md` |
| Supporting Infrastructure | `docs/architecture/SAD-001_System_Architecture_Design.md`, `docs/architecture/SED-001_System_Environment_Design.md` |

### 5.2 Data Protection (Privacy Policy)

> ⚠️ **Action required before submission:** The privacy policy must be accessible from within the app (via the About/Settings menu) and hosted at a live public URL. Confirm which URL will be used for the MAUI app.

| Field | Value |
|---|---|
| How is the privacy policy accessed from within the app | Via the **About** screen in the navigation drawer / settings menu. A direct link opens the policy in the device browser. |
| Privacy Policy URL | ⚠️ To be confirmed — options: `https://www.badbir.org/privacy` or a University of Manchester hosted URL. The old Xamarin app used `http://documents.manchester.ac.uk/display.aspx?DocID=37095` — this should be reviewed and updated for the new app as it may be outdated. |

**Draft privacy policy key points (for MDS review):**
- Data controller: University of Aberdeen / BADBIR (British Society for Rheumatology Biologics Register)
- Data collected: Name, date of birth, NHS/CHI number, patient-reported clinical questionnaire responses (DLQI, HADS, HAQ, EuroQol, SAPASI, PGA, CAGE, Lifestyle)
- Data storage: Encrypted in transit (HTTPS/TLS) and at rest (AES-256-CBC for PII fields). Stored in a University-hosted SQL Server database. Hosted on University of Manchester infrastructure in Phase 1.
- Data retention: For the lifetime of the BADBIR study (currently to 2028+)
- Legal basis: Participant consent (obtained in writing by the clinical team); legitimate interests for clinical research under UK GDPR Article 9(2)(j)
- Data sharing: Data is not sold or shared with third parties. Anonymised aggregate data is used for research publications.
- Patient rights: Right to access, rectify, erase (subject to research exemptions under UK GDPR)
- Contact: Data Protection Officer, University of Aberdeen

### 5.3 Accessibility Statement

> ⚠️ **Action required before submission:** A WCAG 2.1 AA compliance statement must be written and accessible from within the app.

| Field | Value |
|---|---|
| How is the accessibility statement accessed from within the app | Via the **About** screen. |
| Draft accessibility statement | BADBIR Patient is committed to meeting WCAG 2.1 Level AA accessibility standards. The app uses standard MAUI/Blazor accessibility features: semantic labels for all interactive controls, sufficient colour contrast ratios (minimum 4.5:1 for normal text), scalable font sizes that respect the device's accessibility text size settings, and screen reader (TalkBack on Android, VoiceOver on iOS) compatible markup. Known limitations at v2.0.0 launch: the SAPASI body map component (graphical psoriasis area selection) has limited screen reader support — a text-based fallback is available. Accessibility issues can be reported to `badbir@abdn.ac.uk`. The University of Manchester is committed to making its mobile applications accessible in accordance with the Public Sector Bodies (Websites and Mobile Applications) Accessibility Regulations 2018. |

### 5.4 End User Licence Agreement (EULA)

> ⚠️ **Action required before submission:** A EULA must be written, reviewed by University legal, and accessible from within the app.

| Field | Value |
|---|---|
| How is the EULA accessed from within the app | Via the **About** screen and presented on first launch (must be accepted before the app can be used). |
| Draft EULA key points | This application is provided by the University of Aberdeen and the British Society for Rheumatology Biologics Register (BADBIR) for the exclusive use of enrolled BADBIR study participants. The application may only be used to submit patient-reported outcome questionnaires as part of participation in the BADBIR study. Participants must have provided signed consent to the study before using this application. Data entered is used solely for research purposes as described in the participant information sheet. The application is not a substitute for medical advice. In an emergency, contact your GP or call 999. By accepting this agreement you confirm that you are a registered BADBIR study participant. |

### 5.5 Analytics

| Field | Value |
|---|---|
| Analytics services / integrations used | **None.** No analytics SDK is included. |
| Events captured | N/A |
| Data linkage to identifiable information | N/A |
| Data usage / sharing | N/A |

---

## 6. University Supporting Processes

| Party | Details of Consultation |
|---|---|
| **Information Governance & ITS Security** | IGRR 1092 obtained for the existing BADBIR portal. The new app continues under the same data flows. ⚠️ Confirm with IG whether a new IGRR is required for the MAUI rebuild given the architectural change to a separate Patient DB. |
| **Ethical Approval** | IRAS 32990 — existing ethics approval covers patient-reported outcome collection via digital means. ⚠️ Confirm with the ethics committee whether the app rebuild requires an amendment. |
| **Medical Device Certification** | ⚠️ **Requires review.** The app collects and displays clinical outcome scores (DLQI, HADS, HAQ, EuroQol). Under UK MDR 2002 and MHRA guidance for Software as a Medical Device (SaMD), software used to inform clinical decisions may require certification. **Recommendation:** Seek MHRA guidance / legal advice before iOS App Store submission, as Apple's health data policies also touch on this. The app does NOT diagnose, treat, or directly drive clinical decisions — it is a data collection tool — but this should be confirmed formally. |
| **Intellectual Property & Legal** | Clinical instruments: DLQI (Cardiff University licence held by BADBIR), HADS (GL Assessment — pay-per-completed-form agreement in place), EQ-5D-3L (EuroQol licence — non-commercial research arrangement), HAQ (free for academic research), CAGE and PGA (public domain). All licences confirmed in DECISION-LOG (ADR-016). ⚠️ University legal to review EULA before submission. |
| **Branding and Colours** | App does not use NHS Identity branding. BADBIR logo and colours are used. No University of Manchester branding required (app is BADBIR-branded). ⚠️ Confirm NHS logo usage policy if any NHS-branded elements are added. |
| **Export Controls** | The app does not contain dual-use technology or encryption algorithms beyond standard commercial SSL/TLS and AES. No export control classification required. |

---

## 7. Maintenance Arrangements

| Field | Value |
|---|---|
| Describe maintenance arrangements | In-house maintenance by BADBIR-employed software development staff at the University of Aberdeen. Bug fixes and minor releases handled by the development team. MDS contacted for each new store release. |
| Plan for app availability | The app is expected to be available for the lifetime of the BADBIR study (currently 2028, with possible extension). App will be retired from the stores at study closure and replaced with a data export notification to remaining participants. |

---

## 8. Artwork Checklist

> ⚠️ All artwork must be created before submission. Send as a zipped archive.

| Store | Asset | Size / Aspect Ratio | Status |
|---|---|---|---|
| Google Play | App icon | 512×512px (PNG, no alpha) | ⚠️ To produce |
| Google Play | Feature graphic | 1024×500px | ⚠️ To produce |
| Google Play | Phone screenshots (portrait) | Min 2, aspect 9:16 or 2:1 | ⚠️ To produce from staging app |
| Google Play | 7-inch tablet screenshots | Min 2 | ⚠️ To produce |
| Google Play | 10-inch tablet screenshots | Min 2 | ⚠️ To produce |
| Apple App Store | App icon | 1024×1024px (PNG, no transparency) | ⚠️ To produce |
| Apple App Store | iPhone 6.7" screenshots (iPhone 15 Pro Max) | Min 3, 100% scale | ⚠️ To produce |
| Apple App Store | iPhone 6.5" screenshots (iPhone 14 Plus) | Min 3, 100% scale | ⚠️ To produce |
| Apple App Store | iPad Pro 12.9" 6th gen screenshots | Min 3, 100% scale | ⚠️ To produce |

> **Tip:** Screenshots can be produced from the iOS Simulator and Android Emulator once the app UI is complete. The HTML mockup screens in `docs/mockups/` can be used as reference for what the screenshots should show.

---

## 9. Store Listing

| Field | Value |
|---|---|
| **Subtitle / Tagline** (App Store — 30 chars max) | `BADBIR Patient Portal` |
| **Short Description** (Google Play — 80 chars max) | `Submit your BADBIR study questionnaires securely from your phone.` |
| **Promotional Text** (App Store — 170 chars max) | `BADBIR Patient makes it easy to submit your regular study questionnaires from your phone or tablet. Secure, private, and available whenever you need it.` |
| **Release Notes** (for v2.0.0) | `Version 2.0 is a complete rebuild of the BADBIR Patient app using modern .NET MAUI technology. All your forms and follow-up schedule are available in the new app. If you had an account in the previous app, please re-register using the same details — your study record is preserved. Contact badbir@abdn.ac.uk if you need help migrating.` |
| **Long Description** (4000 chars max) | BADBIR Patient is the official app for participants in the British Association of Dermatologists Biologic and Immunomodulators Register (BADBIR). BADBIR is a UK-wide registry studying the long-term safety and effectiveness of treatments for severe psoriasis. If you are taking part in the BADBIR study, this app lets you: • Complete your regular follow-up questionnaires (DLQI, EuroQol, HADS, HAQ, SAPASI, PGA, CAGE and lifestyle questions) securely from your smartphone. • See your upcoming questionnaire schedule. • Track your submissions. Your data is encrypted in transit and at rest. The app connects securely to the BADBIR research database. Your questionnaire responses contribute to important research about psoriasis treatments. Thank you for taking part. |
| **Keywords** (Google Play / App Store — min 4) | `BADBIR, psoriasis, clinical trial, research, questionnaire, rheumatology, dermatology, patient portal` |
| **Desired Store Category** | Medical (both stores) |
| Promo video URL | None |
| **Support email** | `badbir@abdn.ac.uk` |
| **Support URL** | `https://www.badbir.org/Contact/` |
| **App website** | `https://patient.badbir.org` |
| **Privacy policy URL** | ⚠️ To be confirmed before submission |
| Advertised outside stores? | Yes — BADBIR website, newsletters, direct links |
| Countries available | No restriction (worldwide availability) |
| Contains / uses encryption | Yes — HTTPS/TLS for all communications; JWT stored in SecureStorage (device OS encryption); patient PII encrypted with AES-256 server-side before database storage. |
| Content restricting use by age groups | No |
| How long available | Indefinitely (until study closure) |

---

## 10. Content Rating Questionnaire

### 10.1 App Category
- [x] **Utility, Productivity, Communication or Other** — The app is a data collection utility for clinical research participants.
- *(Medical could also apply — check with MDS which category best suits the health data declaration on Apple)*

### 10.2 App Content
| Question | Answer |
|---|---|
| Does the app contain violent material? | No |
| Does the app contain sexual material or nudity? | No |
| Does the app contain potentially offensive language? | No |
| Does the app contain references to illegal drugs? | No |
| Does the app promote age-restricted items? | No |
| Does the app allow users to interact with other users? | No |
| Does the app share the user's current physical location? | No |
| Does the app allow users to purchase digital goods? | No |
| Does the app contain Nazi symbols or unconstitutional content? | No |
| Is the app a web browser or search engine? | No |

### 10.3 Target Age Groups
| Age Group | Included |
|---|---|
| 5 and under | No |
| 6–8 | No |
| 9–12 | No |
| 13–15 | No |
| 16–17 | Yes (BADBIR recruits patients aged 16+) |
| 18 and over | Yes |
| Could the store listing unintentionally appeal to children? | No |

---

## 11. Declaration

> **By submitting this form to MDS, the developer confirms that the form has been completed accurately to the best of their knowledge and assumes responsibility for the software and its compliance with relevant legislation.**

| Party | Name | Position | Date |
|---|---|---|---|
| Developer Signatory | Hassan Ali | Software Development Team Leader for BADBIR | ⚠️ To be signed at submission time |

---

## 12. Submission Checklist

Before contacting MDS to begin the review:

- [ ] App is feature-complete and running on the staging API
- [ ] All test cases in TST-001 pass on CI
- [ ] Privacy policy URL live and accessible from within the app
- [ ] Accessibility statement complete and accessible from within the app
- [ ] EULA reviewed by University legal, accepted on first launch
- [ ] Medical Device / SaMD assessment completed (see Section 6)
- [ ] New IGRR confirmed or existing IGRR confirmed to cover MAUI rebuild (see Section 6)
- [ ] Ethics amendment confirmed not required (see Section 6)
- [ ] All store artwork produced (see Section 8)
- [ ] Store listing copy approved by BADBIR lead (see Section 9)
- [ ] Test staging account provided to MDS review team
- [ ] Android AAB built in Release mode and signed with upload keystore
- [ ] iOS IPA built in Release mode with distribution certificate
- [ ] Form signed (Section 11)

---

## 13. MDS Contact & Process

**Service:** Deployment tier (£250 for two reviews, £125 per additional review)

**Contact form:** Complete the "Application Support form" at: `https://research-it.manchester.ac.uk/services/mobile-development-service-mds/`

**Process:**
1. Complete this form
2. Submit to MDS via the Application Support form
3. MDS will review and may contact you with questions
4. Address any issues raised, resubmit for second review (included in £250)
5. MDS publishes app to Google Play (under University store account) and App Store (under University Apple Developer account)

> **Note on iOS:** The University Apple Developer account requires the University to be the registered developer. The MDS team handles the App Store Connect account. Ensure the bundle ID `org.badbir.patientapp` is registered under the University's team before submission — MDS will advise on this as part of their review process.
