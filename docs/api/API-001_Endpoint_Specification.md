# API-001 – API Endpoint Specification
## BADBIR Patient Application

> **Document ID:** API-001  
> **Version:** 0.1 (Draft)  
> **Status:** In Review  
> **Base URL:** `https://api.badbir.org` (production) / `https://localhost:7000` (development)  
> **OpenAPI JSON:** `GET /openapi/v1.json` (Development only)  
> **Last Updated:** 2026-03-25

---

## 1. Authentication

All endpoints except registration and login require:
```
Authorization: Bearer <access_token>
```

Tokens are obtained via the Identity endpoints at `api/auth/*`.

---

## 2. Identity Endpoints (Built-in — `MapIdentityApi`)

These endpoints are provided automatically by `MapIdentityApi<ApplicationUser>()`.

| Method | Path | Description | Auth |
|---|---|---|---|
| POST | `/api/auth/register` | Create new user account | None |
| POST | `/api/auth/login` | Login with email + password; returns access + refresh tokens | None |
| POST | `/api/auth/refresh` | Exchange a refresh token for a new access token | Refresh token |
| POST | `/api/auth/logout` | Revoke current session | Bearer |
| GET | `/api/auth/confirmEmail` | Confirm email address via link | None |
| POST | `/api/auth/resendConfirmationEmail` | Re-send email confirmation | None |
| GET/POST | `/api/auth/manage/info` | Get/update authenticated user info | Bearer |
| POST | `/api/auth/manage/2fa` | Enable/disable two-factor authentication | Bearer |

### Login Request / Response

**POST** `/api/auth/login`

Request:
```json
{ "email": "patient@example.com", "password": "SecurePass123!" }
```

Response `200 OK`:
```json
{
  "tokenType": "Bearer",
  "accessToken": "eyJhbGciOiJIUzI1NiIs...",
  "expiresIn": 3600,
  "refreshToken": "CfDJ8NiIon..."
}
```

---

## 3. Custom Authentication Endpoints

### 3.1 Account Recovery

**POST** `/api/auth/recover`

Used when a patient has forgotten their email or password.

Request:
```json
{
  "dateOfBirth": "1985-03-15",
  "initials": "AB",
  "identifierType": "NhsNumber",
  "identifierValue": "1234567890",
  "newEmail": "newemail@example.com",
  "newPassword": "NewSecurePass123!"
}
```

Response `200 OK`:
```json
{
  "tokenType": "Bearer",
  "accessToken": "eyJhbGciOiJIUzI1NiIs...",
  "expiresIn": 3600,
  "refreshToken": "CfDJ8NiIon..."
}
```

Response `404 Not Found` if identity verification fails (generic message — no specifics about which field failed).

---

## 4. Registration Endpoints

### 4.1 Pathway A — Patient Self-Registration: Step 1 (Identity Verification)

**POST** `/api/registration/verify-identity`

Verifies the patient exists in the BADBIR database using the 3-point check.

Request:
```json
{
  "dateOfBirth": "1985-03-15",
  "initials": "AB",
  "nhsNumber": "1234567890",
  "chiNumber": null,
  "badbirStudyNo": null,
  "centreId": 5
}
```

Response `200 OK`:
```json
{ "holdingPassCode": "ABC123XY", "holdingId": 42 }
```

Response `404 Not Found` if no match (generic).

### 4.2 Pathway A — Step 2 (Set Email + Verify)

**POST** `/api/registration/set-email`

Request:
```json
{
  "holdingId": 42,
  "holdingPassCode": "ABC123XY",
  "emailAddress": "patient@example.com"
}
```

Response `200 OK`: Email verification sent; `{ "emailVerificationSent": true }`

### 4.3 Pathway A — Step 3 (Set Password + Complete)

**POST** `/api/registration/complete`

Request:
```json
{
  "holdingId": 42,
  "holdingPassCode": "ABC123XY",
  "emailVerificationCode": "6-digit-code",
  "password": "SecurePass123!"
}
```

Response `201 Created`:
```json
{
  "tokenType": "Bearer",
  "accessToken": "eyJhbGciOiJIUzI1NiIs...",
  "refreshToken": "CfDJ8NiIon..."
}
```

### 4.4 Pathway B — Clinician Invite Validation

**GET** `/api/registration/invite/{token}`

Called when a patient clicks the clinician-sent invite link.

Response `200 OK`:
```json
{
  "holdingId": 43,
  "prefillFirstName": "John",
  "prefillLastName": "Doe",
  "prefillDob": "1985-03-15",
  "centreId": 5
}
```

---

## 5. Patient Endpoints

### 5.1 Get My Profile

**GET** `/api/patients/me`  
**Auth:** Bearer

Response `200 OK`:
```json
{
  "patientId": 42,
  "firstName": "John",
  "lastName": "Doe",
  "dateOfBirth": "1985-03-15",
  "nhsNumber": "1234567890",
  "gender": "Male",
  "ethnicity": "British",
  "nextVisitDate": "2024-07-20"
}
```

### 5.2 Update Next Visit Date

**PATCH** `/api/patients/me/next-visit`  
**Auth:** Bearer

Request:
```json
{ "nextVisitDate": "2024-07-20" }
```

### 5.3 Get My Notification Preferences

**GET** `/api/patients/me/preferences`  
**Auth:** Bearer

Response:
```json
{
  "notifyReminders": true,
  "notifyInfoComms": false
}
```

### 5.4 Update Notification Preferences

**PUT** `/api/patients/me/preferences`  
**Auth:** Bearer

Request:
```json
{
  "notifyReminders": true,
  "notifyInfoComms": false
}
```

---

## 6. Dashboard Endpoints

### 6.1 Get Patient Dashboard

**GET** `/api/dashboard`  
**Auth:** Bearer

Returns the current follow-up state and form list for the authenticated patient.

Response `200 OK`:
```json
{
  "patientId": 42,
  "followUpNumber": 2,
  "followUpDueDate": "2024-07-20",
  "nextVisitDate": "2024-07-20",
  "greeting": "Hello John",
  "forms": [
    { "formType": "Lifestyle",  "status": "Completed",   "sequence": 1 },
    { "formType": "DLQI",       "status": "NotStarted",  "sequence": 2 },
    { "formType": "EuroQol",    "status": "NotStarted",  "sequence": 3 },
    { "formType": "HADS",       "status": "NotStarted",  "sequence": 4 },
    { "formType": "CAGE",       "status": "NotStarted",  "sequence": 5 },
    { "formType": "SAPASI",     "status": "NotStarted",  "sequence": 6 },
    { "formType": "PGA",        "status": "NotStarted",  "sequence": 7 }
  ]
}
```

Note: `HAQ` only appears in `forms` if the patient has a relevant arthritis diagnosis.

---

## 7. Form Submission Endpoints

For all form endpoints:
- `GET` returns all submissions for the authenticated patient (for history view)
- `POST` creates a new submission for the current follow-up
- Re-submission for the same `pappFupId` returns `409 Conflict`

| Method | Path | Form |
|---|---|---|
| GET / POST | `/api/forms/lifestyle` | Lifestyle |
| GET / POST | `/api/forms/medprobs` | Medical Problems |
| GET / POST | `/api/forms/dlqi` | DLQI |
| GET / POST | `/api/forms/euroqol` | EuroQol |
| GET / POST | `/api/forms/hads` | HADS |
| GET / POST | `/api/forms/cage` | CAGE |
| GET / POST | `/api/forms/sapasi` | SAPASI |
| GET / POST | `/api/forms/pga` | PGA |
| GET / POST | `/api/forms/haq` | HAQ |
| POST | `/api/forms/{formType}/skip` | Skip a specific form |

### Skip Request

**POST** `/api/forms/haq/skip`  
**Auth:** Bearer

Request:
```json
{ "pappFupId": 67, "skipReason": "Too long today" }
```

Response `200 OK`:
```json
{ "formType": "HAQ", "status": "Skipped" }
```

---

## 8. Push Notification Endpoints

### 8.1 Register Push Token

**POST** `/api/notifications/push-token`  
**Auth:** Bearer

Request:
```json
{
  "platform": "Android",
  "token": "fcm-device-token-here"
}
```

### 8.2 Remove Push Token

**DELETE** `/api/notifications/push-token`  
**Auth:** Bearer  
*(Called on logout from mobile app)*

---

## 9. Error Responses

All error responses follow the RFC 9110 `ProblemDetails` format:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "Bad Request",
  "status": 400,
  "detail": "The 'pappFupId' field is required.",
  "traceId": "00-abc123-def456-00"
}
```

| Status | Meaning |
|---|---|
| 400 | Validation error — check `detail` field |
| 401 | Missing or invalid bearer token |
| 403 | Authenticated but not authorised (e.g., accessing another patient's data) |
| 404 | Resource not found |
| 409 | Conflict — duplicate form submission for same follow-up |
| 422 | Business rule violation (e.g., form sequence out of order) |
| 500 | Internal server error — logged server-side, generic message to client |

---

## 10. Legacy API Mapping

| Legacy Endpoint | New Endpoint |
|---|---|
| `POST /Auth2/Login` | `POST /api/auth/login` |
| `POST /Auth2/registerstep1` | `POST /api/registration/verify-identity` |
| `POST /Auth2/registerstep2` | `POST /api/registration/set-email` |
| `POST /Auth2/registerstep3` | `POST /api/registration/complete` |
| `GET /Dashboard/GetPatientAppDashboardAuthenticated` | `GET /api/dashboard` |
| `POST /Dashboard/SaveDLQI` | `POST /api/forms/dlqi` |
| `POST /Dashboard/SavePGAScore` | `POST /api/forms/pga` |
| `POST /Dashboard/SaveEuroqol` | `POST /api/forms/euroqol` |
| `POST /Dashboard/SaveMedProblem` | `POST /api/forms/medprobs` |
| `POST /Dashboard/SaveLifestyle` | `POST /api/forms/lifestyle` |
| `POST /Dashboard/SaveCage` | `POST /api/forms/cage` |
| `POST /Dashboard/SaveHAQ` | `POST /api/forms/haq` |
| `POST /Dashboard/SaveHADS` | `POST /api/forms/hads` |
| *(none)* | `POST /api/forms/sapasi` **(new)** |
