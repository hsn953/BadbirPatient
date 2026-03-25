# SAD-001 – System Architecture Design
## BADBIR Patient Application

> **Document ID:** SAD-001  
> **Version:** 0.1 (Draft)  
> **Status:** In Review  
> **Last Updated:** 2026-03-25

---

## 1. Architecture Overview

The BADBIR Patient Application is a **.NET 10 Mono-repo** that replaces three isolated legacy systems with a single, unified codebase.

```
┌──────────────────────────────────────────────────────────────────────┐
│                        BADBIR.PatientApp.slnx                        │
│                                                                      │
│  ┌─────────────┐   ┌────────────────────────────┐                   │
│  │  BADBIR.Web │   │      BADBIR.Mobile          │                   │
│  │ Blazor Web  │   │   .NET MAUI Blazor Hybrid   │                   │
│  │    App      │   │   (Android / iOS)           │                   │
│  └──────┬──────┘   └────────────┬───────────────┘                   │
│         │                       │                                    │
│         └──────────┬────────────┘                                   │
│                    │ references                                      │
│         ┌──────────▼────────────┐                                   │
│         │  BADBIR.UI.Components │                                   │
│         │  Razor Class Library  │  ← 95% of all Blazor UI           │
│         │  (Pages, Layout,      │                                   │
│         │   Forms, Components)  │                                   │
│         └──────────┬────────────┘                                   │
│                    │ references                                      │
│         ┌──────────▼────────────┐                                   │
│         │    BADBIR.Shared      │                                   │
│         │  DTOs · Enums · Consts│                                   │
│         └───────────────────────┘                                   │
│                                                                      │
│         ┌───────────────────────┐                                   │
│         │     BADBIR.Api        │  references BADBIR.Shared         │
│         │   .NET 10 Web API     │                                   │
│         │  ┌─────────────────┐  │                                   │
│         │  │   Controllers   │  │                                   │
│         │  │   EF Core 10    │  │                                   │
│         │  │   Identity      │  │                                   │
│         │  │   JWT Bearer    │  │                                   │
│         │  │   OpenAPI       │  │                                   │
│         │  └────────┬────────┘  │                                   │
│         └───────────┼───────────┘                                   │
│                     │                                                │
│         ┌───────────▼───────────┐                                   │
│         │  SQL Server 2022      │                                   │
│         │  (Existing Database)  │                                   │
│         └───────────────────────┘                                   │
│                                                                      │
│         ┌───────────────────────┐                                   │
│         │  BADBIR.Api.Tests     │                                   │
│         │  xUnit + Mvc.Testing  │                                   │
│         └───────────────────────┘                                   │
└──────────────────────────────────────────────────────────────────────┘
```

---

## 2. Project Structure

### 2.1 Solution Projects

| Project | Type | TFM | Purpose |
|---|---|---|---|
| `BADBIR.Shared` | Class Library | net10.0 | DTOs, Enums, Constants — shared by all projects |
| `BADBIR.Api` | ASP.NET Core Web API | net10.0 | REST API, EF Core, Identity, JWT |
| `BADBIR.UI.Components` | Razor Class Library | net10.0 | All shared Blazor UI components and pages |
| `BADBIR.Web` | Blazor Web App | net10.0 | Browser-facing SPA/hybrid app |
| `BADBIR.Mobile` | MAUI Blazor Hybrid | net10.0-android/ios | Native Android/iOS app |
| `BADBIR.Api.Tests` | xUnit Test Project | net10.0 | API unit and integration tests |

### 2.2 Project Reference Graph

```
BADBIR.Shared
    ↑ referenced by
    ├── BADBIR.Api
    ├── BADBIR.UI.Components
    │       ↑
    │       ├── BADBIR.Web
    │       └── BADBIR.Mobile

BADBIR.Api
    ↑
    └── BADBIR.Api.Tests
```

---

## 3. BADBIR.Api — Internal Architecture

### 3.1 Folder Structure

```
src/BADBIR.Api/
├── Controllers/
│   ├── PatientsController.cs         GET /api/patients/{id}, /me
│   ├── FormsController.cs            POST/GET per form type
│   ├── AuthController.cs             Custom auth flows (recovery, re-register)
│   ├── RegistrationController.cs     Both registration pathways
│   └── NotificationsController.cs    Patient notification preferences
├── Data/
│   ├── BadbirDbContext.cs            EF Core DbContext (IdentityDbContext)
│   ├── Entities/                     Scaffolded + custom EF entities
│   └── Configuration/               IEntityTypeConfiguration<T> per entity
├── Services/
│   ├── ITokenService.cs              JWT generation + revocation
│   ├── IEncryptionService.cs         AES PII encryption/decryption
│   ├── INotificationService.cs       Email + push dispatch
│   └── IFormSubmissionService.cs     Form business logic
├── BackgroundJobs/
│   ├── HoldingAccountExpiryJob.cs    14-day deletion background service
│   └── NotificationSchedulerJob.cs   Daily visit-date-based notification job
├── Mapping/
│   └── EntityToDtoMappingExtensions.cs
├── Program.cs
└── appsettings.json / appsettings.Development.json
```

### 3.2 Authentication Flow

```
Client                              API                              DB
  │                                  │                               │
  │  POST /api/auth/login            │                               │
  │ ─────────────────────────────►   │                               │
  │  { email, password }             │  UserManager.CheckPasswordAsync
  │                                  │ ────────────────────────────► │
  │                                  │ ◄──────────────────────────── │
  │                                  │  PasswordOk + SecurityStamp   │
  │                                  │                               │
  │                                  │  Generate JWT (sub, email,    │
  │                                  │  roles, jti, securityStamp)   │
  │                                  │  Generate RefreshToken        │
  │                                  │                               │
  │  200 { accessToken, refreshToken}│                               │
  │ ◄─────────────────────────────── │                               │
  │                                  │                               │
  │  GET /api/patients/me            │                               │
  │  Authorization: Bearer <token>   │                               │
  │ ─────────────────────────────►   │                               │
  │                                  │  Validate JWT signature       │
  │                                  │  Check SecurityStamp claim    │
  │                                  │ ────────────────────────────► │
  │  200 { patient profile }         │                               │
  │ ◄─────────────────────────────── │                               │
```

### 3.3 Registration Pathway A (Self-Registration)

```
Patient          Public API              DB          Clinician System
  │                  │                   │                  │
  │ POST /register   │                   │                  │
  │ ──────────────►  │                   │                  │
  │                  │  Verify identity  │                  │
  │                  │ ───────────────►  │                  │
  │                  │ ◄──────────────── │                  │
  │                  │  Insert HoldingPatient              │
  │                  │ ───────────────►  │                  │
  │  201 { holdingId}│                   │                  │
  │ ◄──────────────  │                   │                  │
  │                  │  Send verify email│                  │
  │                  │──────────────────────────────────►   │
  │ (email arrives)  │                   │                  │
  │ POST /verify-email│                  │                  │
  │ ──────────────►  │                   │                  │
  │                  │  Mark email verified                 │
  │  Patient can now submit forms (in holding state)        │
  │                  │                   │  Clinician confirms│
  │                  │                   │ ◄────────────────  │
  │                  │  Move to Patients │                  │
  │                  │ ───────────────►  │                  │
```

---

## 4. BADBIR.UI.Components — Component Architecture

### 4.1 Blazor Hybrid Sharing Model

The `BADBIR.UI.Components` Razor Class Library is the **single source of truth for all UI**. Both the Blazor Web App and MAUI app are thin hosts.

```
BADBIR.UI.Components/
├── Layout/
│   ├── MainLayout.razor           Shell layout (sidebar + top bar)
│   └── NavMenu.razor              Shared navigation menu
├── Pages/
│   ├── Home.razor                 Landing / welcome page
│   ├── Dashboard.razor            Follow-up dashboard with form list
│   ├── Login.razor                Email/password login form
│   ├── Register/
│   │   ├── RegisterStep1.razor    Identity verification
│   │   ├── RegisterStep2.razor    Email + password setup
│   │   └── RegisterStep3.razor    Consent & confirmation
│   └── Forms/
│       ├── EuroQolForm.razor      EQ-5D form component
│       ├── HaqForm.razor          HAQ form component
│       ├── HadsForm.razor         HADS form component
│       ├── DlqiForm.razor         DLQI form component
│       ├── CageForm.razor         CAGE form component
│       ├── SapasiForm.razor       SAPASI form (new, interactive body map)
│       ├── PgaForm.razor          PGA score form
│       └── LifestyleForm.razor    Lifestyle + Medical problems
├── Services/
│   └── ServiceCollectionExtensions.cs   AddBADBIRUIComponents()
└── _Imports.razor
```

### 4.2 Service Registration Pattern

Both host applications call `AddBADBIRUIComponents(apiBaseUrl)`:

```csharp
// BADBIR.Web / Program.cs
builder.Services.AddBADBIRUIComponents(
    builder.Configuration["ApiBaseUrl"]!);

// BADBIR.Mobile / MauiProgram.cs
builder.Services.AddMauiBlazorWebView();
builder.Services.AddBADBIRUIComponents(
    builder.Configuration["ApiBaseUrl"]!);
```

Inside `AddBADBIRUIComponents`:
- Registers typed `HttpClient` with base address + JWT attachment handler
- Registers form state services
- Registers offline sync service (mobile: backed by SQLite; web: no-op)

---

## 5. Data Layer

### 5.1 EF Core Database-First Approach

The existing SQL Server 2022 database is scaffolded using:

```bash
dotnet ef dbcontext scaffold \
  "Name=ConnectionStrings:BadbirDb" \
  Microsoft.EntityFrameworkCore.SqlServer \
  --project src/BADBIR.Api \
  --output-dir Data/Entities \
  --context-dir Data \
  --context BadbirDbContext \
  --no-onconfiguring \
  --data-annotations \
  --force
```

Entity customizations (fluent API) go in `Data/Configuration/` — never directly in generated entity files.

### 5.2 Key Entity Groups

| Group | Tables | Notes |
|---|---|---|
| Identity | AspNetUsers, AspNetRoles, AspNetUserRoles, ... | Standard Identity tables |
| Patient Core | Patients, PatientHoldingAccounts, PatientDiagnoses | `Patients.NhsNumber` uses AES + HMAC |
| Consent & Audit | ConsentRecords, SecurityAuditLog | Immutable — append-only |
| Follow-Ups | PappPatientCohortTracking | Maps patient → follow-up periods |
| PRO Forms | BbPappPatientDlqi, EuroQolSubmissions, HaqSubmissions, HadsSubmissions, CageSubmissions, SapasiSubmissions, PgaSubmissions, LifestyleSubmissions | Per-form tables |
| Notifications | PushTokens, NotificationLog | Push device tokens + send history |

### 5.3 Legacy Field Mapping

The legacy API used `chid` (clinical history ID) as the core patient identifier. In the new system, `chid` maps to the BADBIR internal `PatientId` on the `Patients` table. The new API uses this internally; clients receive the Identity `userId` (GUID string).

---

## 6. Security Architecture

### 6.1 Authentication Layers

| Layer | Mechanism |
|---|---|
| API authentication | JWT Bearer (ASP.NET Core `AddJwtBearer`) |
| Identity management | ASP.NET Core Identity (`IdentityUser` extended as `ApplicationUser`) |
| Built-in Identity endpoints | `MapIdentityApi<ApplicationUser>` at `api/auth/*` |
| Token revocation | SecurityStamp validation on every request |
| Biometric (mobile) | Device SecureStorage + platform biometric prompt |

### 6.2 PII Encryption Pipeline

```
API receives patient data
    │
    ▼
EncryptionService.Encrypt(plaintext)  ← AES-256-CBC (key from Key Vault/env var)
    │
    ▼
Encrypted bytes → Base64 string stored in DB column
    │
    ▼ (on read)
EncryptionService.Decrypt(ciphertext) → plaintext returned in DTO
```

The `NhsNumber` column additionally stores `NhsNumberHash` (HMAC-SHA256 with separate key) for deterministic lookups without decrypting all rows.

---

## 7. Deployment Architecture

See `docs/architecture/SED-001_System_Environment_Design.md` for full deployment details.

### Summary

| Phase | Target | API Hosting | Web Hosting | Database |
|---|---|---|---|---|
| Phase 1 | On-Premise (University) | IIS / Windows Server 2022 | IIS / Windows Server 2022 | SQL Server 2022 (existing) |
| Phase 2 | AWS Cloud | ECS (Docker) | CloudFront + S3 or ECS | RDS SQL Server |

---

## 8. Key Architectural Decisions

See `docs/progress/DECISION-LOG.md` for the full decision record.

| Decision | Choice | Rationale |
|---|---|---|
| OpenAPI | Native `Microsoft.AspNetCore.OpenApi` | Per requirements — no Swashbuckle |
| Auth | JWT Bearer + ASP.NET Identity | Standard, OIDC-compatible, upgrade path to NHS Login |
| UI sharing | Razor Class Library | Enables code sharing between Web + MAUI without duplication |
| EF Core | Database-First | Existing production DB must not be broken by migration-first approach |
| Offline store | SQLite via EF Core | Consistent EF API for both server (SQL Server) and mobile (SQLite) |
| PII encryption | AES-256 at API layer | Must match Clinician System; transparent to DB layer |
