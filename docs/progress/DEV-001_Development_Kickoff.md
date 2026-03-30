# DEV-001 – Development Kickoff Guide
## BADBIR Patient Application — Sprint 1

> **Document ID:** DEV-001  
> **Version:** 0.1  
> **Status:** Active  
> **Last Updated:** 2026-03-30  
> **Audience:** Lead developer and team

---

## 1. Context

Architecture is decided (Option B — separate Patient DB, API-first). Documentation is complete. This document answers:

1. **Where do we start?** The `.NET 10 API` project (`src/BADBIR.Api/`).
2. **How can I test as we go?** Swagger UI is live at `https://localhost:{port}/scalar/v1` from the first day.
3. **What is the development sequence?**

---

## 2. Development Sequence

```
Phase 1: BADBIR.Api  (Months 1–2)
    └── Patient DB schema + EF Core migrations
    └── ASP.NET Core Identity (auth endpoints)
    └── Form submission endpoints (all 9 forms)
    └── Admin/internal endpoints (promote, reject, QR)
    └── Clinician System integration stubs (IClinicianSystemClient)

Phase 2: BADBIR.Web  (Months 2–3)
    └── Blazor Server — Registration, Dashboard, Forms
    └── Integrates with BADBIR.Api over HTTPS

Phase 3: Integration testing with Clinician System  (Month 3)
    └── Staging environment
    └── Clinician System builds /api/internal/* endpoints in parallel

Phase 4: BADBIR.Mobile (Android)  (Month 4)
    └── .NET MAUI Blazor Hybrid — reuses BADBIR.UI.Components
    └── Biometric auth, push notifications, offline handling

Phase 5: Google Play internal testing → beta → release  (Month 5)

Phase 6: BADBIR.Mobile (iOS)  (Months 5–6)

Phase 7: App Store submission via UoM MDS  (Month 6)
```

> The **API is the critical path.** Web and Mobile consume it. Get the API right, and the other layers follow quickly.

---

## 3. Sprint 1 — What to Do First (API Foundation)

### 3.1 Prerequisites (One-Time Setup)

```bash
# Install .NET 10 SDK
# https://dotnet.microsoft.com/download/dotnet/10.0

# Verify
dotnet --version    # should show 10.x

# Install EF Core CLI tools
dotnet tool install --global dotnet-ef

# Clone the repo (already done on this machine)
cd /path/to/BadbirPatient

# Restore
dotnet restore
```

### 3.2 Run the API Immediately (Day 1)

The API project already exists at `src/BADBIR.Api/`. Run it:

```bash
cd src/BADBIR.Api
dotnet run
```

The API will start on `https://localhost:5001` (or whatever port is configured). Swagger / Scalar UI should be available at:
- `https://localhost:5001/scalar/v1` (Scalar UI, if configured)
- `https://localhost:5001/openapi/v1.json` (raw OpenAPI spec)

> If Scalar UI is not yet configured, add it to `Program.cs`:
> ```csharp
> builder.Services.AddOpenApi();
> // ...
> app.MapOpenApi();
> app.MapScalarApiReference();  // install Scalar.AspNetCore NuGet package
> ```

### 3.3 Set Up the Patient Database (SQLite for Dev)

The Patient DB (`BadbirPatient`) is owned by the Patient App and is managed **code-first** with EF Core migrations. In development, SQLite is used (no SQL Server installation required).

**Step 1: Verify `appsettings.Development.json`**

```json
{
  "ConnectionStrings": {
    "BadbirDbSqlite": "Data Source=badbir_patient_dev.db"
  }
}
```

**Step 2: Create the initial migration**

```bash
cd src/BADBIR.Api

dotnet ef migrations add InitialCreate \
  --project src/BADBIR.Api \
  --startup-project src/BADBIR.Api \
  --output-dir Data/Migrations

dotnet ef database update
```

This creates `badbir_patient_dev.db` in the project folder. This is a SQLite file — open it with [DB Browser for SQLite](https://sqlitebrowser.org/) or the VS Code SQLite extension to inspect the tables.

> **Important:** `badbir_patient_dev.db` is in `.gitignore` — never commit it.

### 3.4 What Tables Should Exist After the First Migration

The EF Core context (`PatientDbContext`) should create:

| Table | Description |
|---|---|
| `AspNetUsers` | ASP.NET Core Identity — patient accounts |
| `AspNetRoles` | Identity roles (Patient, InternalService) |
| `AspNetUserRoles` | Role assignments |
| `AspNetUserClaims` | User claims |
| `AspNetUserTokens` | Refresh tokens |
| `BbPappPatientCohortTracking` | Follow-up schedule tracking |
| `BbPappPatientDlqi` | DLQI form submissions (holding) |
| `BbPappPatientLifestyle` | Lifestyle & demographics (holding) |
| `BbPappPatientCage` | CAGE (holding) |
| `BbPappPatientHad` | HADS (holding) |
| `BbPappPatientHaq` | HAQ (holding) |
| `BbPappPatientEuroqol` | EuroQol (holding) |
| `BbPappPatientSapasi` | SAPASI (holding) |
| `BbPappPatientPgaScore` | PGA (holding) |
| `PendingClinicianActions` | Actions awaiting clinician review |
| `QrCodeTokens` | Signed QR tokens (single-use) |

### 3.5 First Testable Endpoint: `/api/health`

Add a health check endpoint — this is the first thing to verify the API is running and database is connected:

```csharp
// Program.cs
builder.Services.AddHealthChecks()
    .AddDbContextCheck<PatientDbContext>();

app.MapHealthChecks("/api/health");
```

Test it:
```bash
curl https://localhost:5001/api/health
# Expected: {"status":"Healthy"}
```

### 3.6 Second Milestone: Auth Endpoints

ASP.NET Core Identity API endpoints are registered with one call. After this milestone, a patient can register, login, and get a JWT token — all testable via Swagger UI.

```csharp
// Program.cs
builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PatientDbContext>();

app.MapIdentityApi<ApplicationUser>();
```

**Endpoints this adds automatically:**
- `POST /register` — create account with email + password
- `POST /login` — returns bearer token + refresh token
- `POST /refresh` — exchange refresh token for new bearer
- `POST /logout`
- `GET /manage/info` — current user info

**Test via Swagger UI:**
1. Open `https://localhost:5001/scalar/v1`
2. `POST /register` with `{ "email": "test@example.com", "password": "Test1234!" }`
3. `POST /login` — copy the `accessToken` from the response
4. Click "Authorize" in Swagger and paste the token
5. `GET /manage/info` — should return your email

> At this point you can test the full auth flow without any mobile app. This is the foundation everything else builds on.

---

## 4. How to Test Continuously as Development Progresses

### 4.1 Swagger UI (API Testing — Daily Use)

Every new endpoint appears immediately in Swagger UI. Use it to:
- Test request/response payloads
- Test auth (paste the bearer token in the Authorize dialog)
- Verify validation errors (try submitting bad data)

### 4.2 `.http` File (Quick Endpoint Tests)

The project already has `src/BADBIR.Api/BADBIR.Api.http`. Use this in VS Code (with the REST Client extension) or Rider to keep a collection of test requests that run with a single click:

```http
### Health check
GET https://localhost:5001/api/health

### Login
POST https://localhost:5001/login
Content-Type: application/json

{
  "email": "test@example.com",
  "password": "Test1234!"
}

### Submit DLQI (authenticated)
POST https://localhost:5001/api/forms/dlqi
Authorization: Bearer {{token}}
Content-Type: application/json

{
  "fupId": 1,
  "answers": [0, 1, 2, 0, 1, 2, 0, 1, 2, 0]
}
```

### 4.3 Automated Tests (CI — Every Push)

Tests live in `tests/BADBIR.Api.Tests/`. Every push to any branch triggers the GitHub Actions CI which runs all tests. Check the Actions tab at `https://github.com/hsn953/BadbirPatient/actions` to see test results.

Run tests locally:
```bash
dotnet test tests/BADBIR.Api.Tests/
```

### 4.4 Web App Testing (Phase 2 Onwards)

Once `BADBIR.Web` is running, open it in the browser (`https://localhost:5002` or similar). The full registration → baseline forms → dashboard flow is testable in the browser without any mobile device.

### 4.5 Mobile App Testing (Phase 4 Onwards)

- **Android:** Build and deploy to Android Emulator directly from Visual Studio / Rider with F5. No device needed.
- **iOS:** Build and deploy to iOS Simulator on macOS. Requires a Mac with Xcode installed.
- **Physical device (Android):** Enable Developer Options on the device, enable USB Debugging, plug in via USB — Visual Studio deploys directly.

---

## 5. Sprint 1 Checklist

### Week 1 — Foundation

- [ ] `dotnet run` in `src/BADBIR.Api/` — API starts, Swagger UI loads at `/scalar/v1`
- [ ] `PatientDbContext` set up code-first with all papp holding table entities
- [ ] `dotnet ef migrations add InitialCreate` — migrations created successfully
- [ ] `dotnet ef database update` — SQLite dev DB created, tables verified in DB Browser
- [ ] `/api/health` endpoint returns `{"status":"Healthy"}`
- [ ] ASP.NET Core Identity wired up — `/register` and `/login` work in Swagger UI
- [ ] `ApplicationUser` model created (extends `IdentityUser`) — add any BADBIR-specific fields (e.g. `Chid`, `RegistrationStatus`)
- [ ] GitHub Actions CI: `dotnet test` passes on push

### Week 2 — Registration Flow (API)

- [ ] `POST /api/patients/register` — patient self-registration (Step 1: eligibility screener answers)
- [ ] `POST /api/patients/verify-identity` — call to Clinician System (use stub/mock `IClinicianSystemClient`)
- [ ] Patient holding state created in `PendingClinicianActions`
- [ ] `GET /api/patients/me/status` — returns current registration status
- [ ] Unit tests for registration controller

### Week 3 — Baseline Forms (API)

- [ ] `POST /api/forms/lifestyle` — demographics + lifestyle form
- [ ] `POST /api/forms/pga` — Patient Global Assessment
- [ ] `POST /api/forms/dlqi` — DLQI form (10 questions, score calculated server-side)
- [ ] `POST /api/forms/euroqol` — EQ-5D-5L (5 dimensions + VAS)
- [ ] `POST /api/forms/hads` — HADS (7 anxiety + 7 depression)
- [ ] `POST /api/forms/haq` — HAQ (8 categories)
- [ ] `POST /api/forms/cage` — CAGE (4 questions, conditional)
- [ ] `POST /api/forms/sapasi` — SAPASI (psoriasis area + severity)
- [ ] Unit tests for each form controller (score calculation, validation)

### Week 4 — Admin / Internal Endpoints

- [ ] `POST /api/internal/patients/{chid}/promote` — clinician triggers promotion (stub response while Clinician System builds their side)
- [ ] `POST /api/internal/patients/{chid}/reject` — clinician rejects with reason
- [ ] `GET /api/internal/registrations/pending` — inbox feed
- [ ] `POST /api/internal/qr/generate` — signed QR token generation
- [ ] `POST /api/internal/qr/validate` — token validation
- [ ] Service JWT middleware on `/api/internal/*` routes
- [ ] Integration tests for all internal endpoints

---

## 6. Development Environment Setup Reference

### Required Tools

| Tool | Version | Purpose | Install |
|---|---|---|---|
| .NET SDK | 10.x | Build/run API, Web, Mobile | `https://dotnet.microsoft.com/download/dotnet/10.0` |
| dotnet-ef | Latest | EF Core migrations | `dotnet tool install --global dotnet-ef` |
| Visual Studio 2022 or JetBrains Rider | Latest | IDE | VS Community (free) or Rider |
| DB Browser for SQLite | Latest | Inspect dev SQLite DB | `https://sqlitebrowser.org/` |
| Git | 2.x+ | Version control | Pre-installed or `https://git-scm.com/` |
| VS Code (optional) | Latest | Lightweight editing, `.http` file testing | `https://code.visualstudio.com/` |

### For Mobile Development (when needed in Phase 4+)

| Tool | Platform | Purpose |
|---|---|---|
| Android SDK / Emulator | Windows/Mac/Linux | Android emulator testing |
| Xcode | macOS only | iOS simulator + device testing, IPA build |
| MAUI workloads | .NET SDK | `dotnet workload install maui` |

### Useful VS Code Extensions

- **REST Client** — run `.http` files for API testing
- **SQLite** — view `badbir_patient_dev.db` in the sidebar
- **C#** (ms-dotnettools.csharp) — C# language support
- **GitHub Actions** — view CI run status from the editor

---

## 7. Database — Two Environments Side by Side

During development, you will work with two databases:

| Database | Tech | What it is | How to run |
|---|---|---|---|
| `BadbirPatient` (Patient DB) | SQLite (dev) / SQL Server (staging) | **Your DB** — code-first, you own the schema | `dotnet ef database update` |
| `BADBIR` (Clinician DB) | SQL Server (staging only) | **Clinician System's DB** — you never touch this directly | Clinician System exposes it via `/api/internal/*` |

For **local development**, you will never need to connect to the real Clinician DB. All Clinician System calls go through the `IClinicianSystemClient` interface, which in development/test mode uses a stub:

```csharp
// Startup.cs or Program.cs
#if Development
builder.Services.AddSingleton<IClinicianSystemClient, StubClinicianSystemClient>();
#else
builder.Services.AddHttpClient<IClinicianSystemClient, ClinicianSystemHttpClient>(...);
#endif
```

The `StubClinicianSystemClient` returns hardcoded success responses, so registration, identity verification, and form promotion all work end-to-end in local dev without a live Clinician System.

---

## 8. Key Reference Documents

| What you need | Where to find it |
|---|---|
| All API endpoints planned | `docs/api/API-001_Endpoint_Specification.md` |
| Database schema (Patient DB tables) | `docs/architecture/DSD-001_Database_Schema_Design.md` |
| Form question specs | `docs/forms/FORM-001` through `FORM-009` |
| Integration API contracts (internal endpoints) | `docs/requirements/INT-001_Clinician_System_Integration.md` |
| Auth flow, role definitions | `docs/architecture/SAD-001_System_Architecture_Design.md` |
| Dev / staging / prod environments | `docs/architecture/SED-001_System_Environment_Design.md` |
| PII encryption algorithm | `docs/progress/DECISION-LOG.md` → ADR-015 |
| Testing strategy + test cases | `docs/testing/TST-001_Testing_Strategy.md` |
| Architecture decision rationale | `docs/architecture/ADR-017_Architecture_Discussion_Separate_Patient_DB.md` |
