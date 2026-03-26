# System Design – BADBIR Patient Application

> **Status:** Superseded — see detailed documents below  
> **Last Updated:** 2026-03-25  
> **Note:** This file is the original high-level overview. Detailed specifications have been split into dedicated documents:

## Documentation Index

| Document | Path | Description |
|---|---|---|
| **SAD-001** | `docs/architecture/SAD-001_System_Architecture_Design.md` | Full system architecture, project structure, auth flows |
| **SED-001** | `docs/architecture/SED-001_System_Environment_Design.md` | IIS / Docker / AWS deployment, dev setup, CI/CD |
| **DSD-001** | `docs/architecture/DSD-001_Database_Schema_Design.md` | Database schema, new tables, PII encryption |
| **URD-001** | `docs/requirements/URD-001_User_Requirements.md` | User stories and acceptance criteria |
| **SRS-001** | `docs/requirements/SRS-001_Software_Requirements_Specification.md` | Functional & non-functional requirements |
| **API-001** | `docs/api/API-001_Endpoint_Specification.md` | All API endpoints, request/response contracts |
| **FORM-001** | `docs/forms/FORM-001_EuroQol_EQ5D.md` | EuroQol EQ-5D form specification |
| **FORM-002** | `docs/forms/FORM-002_HAQ.md` | HAQ form specification (all 8 categories) |
| **FORM-003** | `docs/forms/FORM-003_HADS.md` | HADS anxiety & depression scale |
| **FORM-004** | `docs/forms/FORM-004_DLQI.md` | DLQI dermatology quality of life |
| **FORM-005** | `docs/forms/FORM-005_CAGE.md` | CAGE alcohol screening |
| **FORM-006** | `docs/forms/FORM-006_PGA.md` | Patient Global Assessment |
| **FORM-007** | `docs/forms/FORM-007_SAPASI.md` | SAPASI — in v1; UX design decision pending |
| **FORM-008** | `docs/forms/FORM-008_Lifestyle_MedProbs.md` | Lifestyle & Medical Problems forms (Baseline vs FUP split) |
| **FORM-009** | `docs/forms/FORM-009_Eligibility_Screener.md` | Eligibility Screener (Pathway A registration) |
| **TST-001** | `docs/testing/TST-001_Testing_Strategy.md` | SQLite testing strategy, test cases, infrastructure |
| **INT-001** | `docs/requirements/INT-001_Clinician_System_Integration.md` | Clinician System integration requirements (consent inbox, data promotion, shared schema) |
| **MOCK-001** | `docs/mockups/index.html` | UI mockup screens — Registration (10), Baseline (17), Follow-Up (11) |
| **DECISION-LOG** | `docs/progress/DECISION-LOG.md` | All architectural decisions with rationale |

---

---

## Table of Contents

1. [High-Level Architecture](#high-level-architecture)
2. [EF Core Database-First Strategy](#ef-core-database-first-strategy)
3. [API Layer Design](#api-layer-design)
4. [Authentication & Authorisation](#authentication--authorisation)
5. [MAUI Blazor Hybrid UI Flow](#maui-blazor-hybrid-ui-flow)
6. [Patient-Reported Outcome (PRO) Forms](#patient-reported-outcome-pro-forms)
7. [Data Flow Diagrams](#data-flow-diagrams)
8. [Security Considerations](#security-considerations)
9. [Open Questions](#open-questions)

---

## High-Level Architecture

```
┌────────────────────────────────────────────────────────────────────┐
│                         Client Layer                               │
│                                                                    │
│  ┌──────────────────────┐    ┌─────────────────────────────────┐   │
│  │   BADBIR.Web         │    │   BADBIR.Mobile                 │   │
│  │   Blazor Web App     │    │   .NET MAUI Blazor Hybrid       │   │
│  │   (Browser)          │    │   (Android / iOS native shell)  │   │
│  └──────────┬───────────┘    └────────────────┬────────────────┘   │
│             │                                 │                    │
│             └──────────────┬──────────────────┘                   │
│                            │                                       │
│              ┌─────────────▼─────────────┐                        │
│              │  BADBIR.UI.Components     │                        │
│              │  Razor Class Library      │                        │
│              │  (Shared Blazor UI)       │                        │
│              └───────────────────────────┘                        │
└────────────────────────────────────────────────────────────────────┘
                             │ HTTPS / REST
┌────────────────────────────▼───────────────────────────────────────┐
│                         Backend Layer                              │
│                                                                    │
│              ┌───────────────────────────┐                        │
│              │   BADBIR.Api              │                        │
│              │   .NET 8 Web API          │                        │
│              │   - OpenAPI (built-in)    │                        │
│              │   - .NET Identity         │                        │
│              │   - EF Core (DB-First)    │                        │
│              └───────────────┬───────────┘                        │
└──────────────────────────────┼─────────────────────────────────────┘
                               │ ADO.NET / TDS
┌──────────────────────────────▼─────────────────────────────────────┐
│                         Data Layer                                 │
│                                                                    │
│              ┌───────────────────────────┐                        │
│              │   SQL Server 2022         │                        │
│              │   (Existing database)     │                        │
│              └───────────────────────────┘                        │
└────────────────────────────────────────────────────────────────────┘
```

---

## EF Core Database-First Strategy

### Rationale

The SQL Server 2022 database already exists and has been in production use with the legacy .NET 4.5 portal. We adopt **Database-First** to avoid data-migration risk and to preserve the existing table structures, constraints, and stored procedures.

### Scaffolding Workflow

1. Ensure the connection string is set in `src/BADBIR.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "BadbirDb": "Server=<server>;Database=BADBIR;Trusted_Connection=True;"
  }
}
```

2. Install EF Core tooling:

```bash
dotnet tool install --global dotnet-ef
dotnet add src/BADBIR.Api package Microsoft.EntityFrameworkCore.SqlServer
dotnet add src/BADBIR.Api package Microsoft.EntityFrameworkCore.Design
```

3. Scaffold models from the existing database:

```bash
dotnet ef dbcontext scaffold \
  "Name=ConnectionStrings:BadbirDb" \
  Microsoft.EntityFrameworkCore.SqlServer \
  --project src/BADBIR.Api \
  --output-dir Data/Entities \
  --context-dir Data \
  --context BadbirDbContext \
  --no-onconfiguring \
  --data-annotations
```

4. The scaffolded `BadbirDbContext` and entity classes live in `src/BADBIR.Api/Data/`.
5. **Do not modify scaffolded entity classes directly.** Use partial classes or EF Core fluent configuration in `BadbirDbContext.OnModelCreating()` for any customisation.
6. Re-scaffold (with `--force`) whenever the database schema changes.

### Key Design Rules

- Entity classes stay in `BADBIR.Api/Data/Entities/` – they are **internal to the API**.
- DTOs in `BADBIR.Shared` are what cross the HTTP boundary to clients.
- AutoMapper or manual mapping translates between entities and DTOs.

---

## API Layer Design

### Endpoint Groups (planned)

| Route prefix | Controller / Handler | Description |
|---|---|---|
| `POST /api/auth/login` | AuthController | Validate credentials, return JWT |
| `GET /api/patients/{id}` | PatientsController | Fetch patient profile |
| `GET /api/forms` | FormsController | List available PRO forms for a patient |
| `POST /api/forms/{type}` | FormsController | Submit a completed form |
| `GET /api/forms/history` | FormsController | Patient's submission history |

### OpenAPI

.NET 8 ships with built-in OpenAPI support via `Microsoft.AspNetCore.OpenApi`. The API will **not** use Swashbuckle – it uses the new built-in endpoint:

```csharp
// Program.cs
builder.Services.AddOpenApi();
app.MapOpenApi();          // serves /openapi/v1.json
```

A scalar or Swagger UI front-end can be added on top of this JSON endpoint if needed.

---

## Authentication & Authorisation

We use **ASP.NET Core Identity** with the built-in Identity API endpoints (introduced in .NET 8):

```csharp
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<BadbirDbContext>();

app.MapIdentityApi<IdentityUser>();
```

This provides `/register`, `/login`, `/refresh`, and `/manage` endpoints out of the box, secured with bearer tokens (JWT or cookie – configurable).

### Roles

| Role | Access |
|---|---|
| `Patient` | Own profile, submit / view own forms |
| `Clinician` | View patient summaries (read-only) |
| `Administrator` | Full access |

---

## MAUI Blazor Hybrid UI Flow

### Startup Flow (Mobile)

```
MauiProgram.CreateMauiApp()
    │
    ├── UseMauiApp<App>()
    ├── AddMauiBlazorWebView()
    ├── AddBADBIRUIComponents()    ← registers RCL services
    │
    └── App → MainPage
                └── BlazorWebView (HostPage = "wwwroot/index.html")
                        └── Routes.razor  (from BADBIR.UI.Components)
                                └── MainLayout, NavMenu, Pages...
```

### Key Files

| File | Project | Purpose |
|---|---|---|
| `MauiProgram.cs` | BADBIR.Mobile | App entry point, DI registration |
| `MainPage.xaml` | BADBIR.Mobile | Native MAUI page hosting `BlazorWebView` |
| `Resources/Raw/wwwroot/index.html` | BADBIR.Mobile | HTML host page for the WebView |
| `Components/Routes.razor` | BADBIR.Mobile | Router pointing at RCL assembly |
| `Layout/MainLayout.razor` | BADBIR.UI.Components | Shared shell layout |
| `Pages/` | BADBIR.UI.Components | All shared page components |

### Platform-Specific Considerations

- Android requires `INTERNET` permission in `AndroidManifest.xml`.
- iOS requires entitlements for network access.
- Platform-specific services (e.g. push notifications, file system) are registered in `BADBIR.Mobile/Platforms/` and injected via DI abstractions defined in `BADBIR.Shared`.

---

## Patient-Reported Outcome (PRO) Forms

Three clinical forms must be implemented as Blazor components in `BADBIR.UI.Components/Pages/`:

### EuroQol EQ-5D-5L

- **Dimensions:** Mobility, Self-Care, Usual Activities, Pain/Discomfort, Anxiety/Depression
- **Scale:** 5-level Likert per dimension (No problems → Extreme problems)
- **VAS:** 0–100 health state visual analogue scale
- **Reference:** `legacy_reference/old_web_portal_net45/` for original implementation

### HAQ (Health Assessment Questionnaire)

- **Sections:** 8 functional categories (Dressing, Arising, Eating, Walking, Hygiene, Reach, Grip, Activities)
- **Scale:** 0–3 per question (Without difficulty → Unable to do)
- **Score:** HAQ-DI disability index (0–3)

### HADS (Hospital Anxiety and Depression Scale)

- **Subscales:** Anxiety (7 items) and Depression (7 items)
- **Scale:** 0–3 per item
- **Interpretation thresholds:** 0–7 Normal, 8–10 Borderline, 11–21 Abnormal

Each form component will:
1. Render the questions from a model/configuration.
2. Capture answers in a `FormSubmissionDto`.
3. POST to `BADBIR.Api` via an injected `IFormService`.
4. Display confirmation on success.

---

## Data Flow Diagrams

### Form Submission Flow

```
Patient (Browser/Mobile)
    │
    │  Fill in form fields
    ▼
BADBIR.UI.Components – FormComponent.razor
    │
    │  Calls IFormService.SubmitAsync(FormSubmissionDto)
    ▼
HttpClient (configured with API base URL + Bearer token)
    │
    │  POST /api/forms/{type}
    ▼
BADBIR.Api – FormsController
    │
    │  Maps DTO → Entity
    ▼
BadbirDbContext (EF Core)
    │
    │  INSERT into FormSubmissions table
    ▼
SQL Server 2022
```

---

## Security Considerations

- All API endpoints require authentication (except `/api/auth/login`).
- HTTPS enforced in production (`UseHttpsRedirection()`).
- Patient data is scoped by `PatientId` – clinicians cannot submit forms on behalf of patients.
- Connection strings stored in environment variables / Azure Key Vault in production; never committed to source control.
- The `legacy_reference/` folder must **never** be deployed – it is excluded from all build outputs.

---

## Open Questions

| # | Question | Owner | Status |
|---|---|---|---|
| 1 | Does the existing DB schema use ASP.NET Identity tables, or do we need to add them? | DBA Team | Open |
| 2 | Should the API use JWT bearer tokens or cookie-based auth for the mobile client? | Architecture | Open |
| 3 | Are there existing stored procedures for form scoring that EF Core should call? | DBA Team | Open |
| 4 | What is the target minimum Android API level for MAUI? | Mobile Lead | Open |
| 5 | Should `BADBIR.Web` support server-side rendering, WebAssembly, or Auto mode? | Architecture | Open |
