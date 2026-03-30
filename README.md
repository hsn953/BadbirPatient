# BADBIR Patient Application

> **BADBIR** – British Association of Dermatologists Biologics and Immunosuppressants Register

A modern, unified **Mono-repo** for the BADBIR Patient Portal, built on **.NET 8** and the **Blazor Hybrid** model.

---

## Table of Contents

1. [Overview](#overview)
2. [Architecture](#architecture)
   - [Solution Structure](#solution-structure)
   - [Project Descriptions](#project-descriptions)
   - [Blazor Hybrid UI Sharing Model](#blazor-hybrid-ui-sharing-model)
3. [The `legacy_reference` Folder](#the-legacy_reference-folder)
4. [Technology Stack](#technology-stack)
5. [Getting Started](#getting-started)
   - [Prerequisites](#prerequisites)
   - [Build & Run](#build--run)
6. [Project Reference Map](#project-reference-map)
7. [Roadmap](#roadmap)

---

## Overview

We are modernising a fragmented legacy patient portal into a single, maintainable solution. The legacy system consisted of three isolated projects (a .NET 4.5 web portal, a Xamarin Android app, and a decompiled .NET 6 API) that shared no code and maintained separate data contracts.

The new system consolidates everything into a single repository where:

- **One Razor Class Library** (BADBIR.UI.Components) holds ~95 % of all UI code.
- **One API project** (BADBIR.Api) serves both web and mobile clients.
- **One shared library** (BADBIR.Shared) owns all DTOs, enums, and constants.

---

## Architecture

### Solution Structure

```
BADBIR.PatientApp.slnx          ← .NET solution file (XML format)
│
├── docs/
│   ├── requirements/           ← Functional specs (EuroQol, HAQ, HADS forms)
│   ├── architecture/           ← System design, DB schemas, EF Core strategy
│   └── progress/               ← Sprint notes and developer logs
│
├── legacy_reference/           ← READ-ONLY reference material (see below)
│   ├── old_web_portal_net45/
│   ├── old_api_decompiled/
│   ├── old_xamarin_app/
│   └── database_schema/
│
├── src/
│   ├── BADBIR.Shared/          ← DTOs, Enums, Constants (net8.0 Class Library)
│   ├── BADBIR.Api/             ← .NET 8 Web API (EF Core, OpenAPI, Identity)
│   ├── BADBIR.UI.Components/   ← Razor Class Library – shared Blazor UI
│   ├── BADBIR.Web/             ← Blazor Web App (browser)
│   └── BADBIR.Mobile/         ← .NET MAUI Blazor Hybrid (Android / iOS)
│
└── tests/
    └── BADBIR.Api.Tests/       ← xUnit tests for BADBIR.Api
```

### Project Descriptions

| Project | Type | Target | Purpose |
|---|---|---|---|
| `BADBIR.Shared` | Class Library | `net8.0` | DTOs, enums, constants shared by all projects |
| `BADBIR.Api` | Web API | `net8.0` | REST API with EF Core (DB-First), OpenAPI docs, .NET Identity endpoints |
| `BADBIR.UI.Components` | Razor Class Library | `net8.0` | All Blazor components, pages, layouts – consumed by Web and Mobile |
| `BADBIR.Web` | Blazor Web App | `net8.0` | Browser-facing web application |
| `BADBIR.Mobile` | MAUI Blazor Hybrid | `net8.0-android`/`ios` | Native mobile shell hosting the shared Blazor UI |
| `BADBIR.Api.Tests` | xUnit Test Project | `net8.0` | Unit and integration tests for the API layer |

### Blazor Hybrid UI Sharing Model

```
┌─────────────────────────────────────────┐
│         BADBIR.UI.Components (RCL)      │
│  Pages/, Layout/, Components/           │
│  (Razor + C# – platform-agnostic)       │
└────────────────┬────────────────────────┘
                 │  referenced by
        ┌────────┴────────┐
        ▼                 ▼
┌──────────────┐  ┌────────────────────┐
│ BADBIR.Web   │  │  BADBIR.Mobile     │
│ Blazor Web   │  │  MAUI Blazor       │
│ App (browser)│  │  Hybrid (Android/  │
│              │  │  iOS native shell) │
└──────────────┘  └────────────────────┘
        │                 │
        └────────┬────────┘
                 ▼
       ┌─────────────────┐
       │   BADBIR.Api    │
       │  .NET 8 Web API │
       │  (EF Core +     │
       │   .NET Identity)│
       └────────┬────────┘
                │
       ┌────────▼────────┐
       │  SQL Server 2022│
       │  (existing DB)  │
       └─────────────────┘
```

**How the Hybrid model works:**

1. `BADBIR.UI.Components` is a standard Razor Class Library. It has no platform-specific code.
2. `BADBIR.Web` hosts the RCL inside a standard ASP.NET Core Blazor Web App – rendered in the browser via WebAssembly or Server rendering modes.
3. `BADBIR.Mobile` is a .NET MAUI project. It hosts a `BlazorWebView` control, which loads the same Razor components from the RCL inside a native app WebView. The mobile app is a thin native shell with platform-specific lifecycle code only.
4. Both apps share the same service registration entry point via `AddBADBIRUIComponents()` in `BADBIR.UI.Components/Services/ServiceCollectionExtensions.cs`.

---

## The `legacy_reference` Folder

> ⚠️ **IMPORTANT – READ-ONLY CONTEXT ONLY**

The `legacy_reference/` directory is **not part of the build**. It holds copies of the old systems strictly for reference:

| Sub-folder | Contents | Usage |
|---|---|---|
| `old_web_portal_net45/` | .NET 4.5 / NetTiers web portal | Reference for original form logic (EuroQol, HAQ, HADS implementations) and data-access patterns |
| `old_api_decompiled/` | Decompiled .NET 6 API DLLs | **Treat as read-only.** Use only to understand old JSON payloads, endpoint routes, and data contracts. Do not copy its architecture. |
| `old_xamarin_app/` | Xamarin Android mobile app | Reference for client-side HTTP call patterns and legacy UX flows |
| `database_schema/` | SQL Server scripts and table structures | Required for EF Core DB-First scaffolding |

**Never import or reference code from `legacy_reference/` into the `src/` projects.**

---

## Technology Stack

| Concern | Technology |
|---|---|
| Backend framework | .NET 8 Web API |
| ORM / DB access | Entity Framework Core 8 (Database-First) |
| Database | SQL Server 2022 |
| Authentication | ASP.NET Core Identity (built-in endpoints) |
| API documentation | Built-in OpenAPI (.NET 8 `Microsoft.AspNetCore.OpenApi`) |
| Web frontend | Blazor Web App (.NET 8) |
| Mobile frontend | .NET MAUI Blazor Hybrid (Android & iOS) |
| Shared UI layer | Razor Class Library (RCL) |
| Shared DTOs/models | .NET 8 Class Library |
| Testing | xUnit + Microsoft.AspNetCore.Mvc.Testing |

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (or newer)
- [.NET MAUI workload](https://learn.microsoft.com/en-us/dotnet/maui/get-started/installation) (for mobile builds):
  ```bash
  dotnet workload install maui
  ```
- SQL Server 2022 (local or remote instance)
- Visual Studio 2022 17.8+ or JetBrains Rider 2023.3+

### Build & Run

```bash
# Restore all packages
dotnet restore BADBIR.PatientApp.slnx

# Build all projects (excluding Mobile – requires MAUI workload)
dotnet build src/BADBIR.Shared
dotnet build src/BADBIR.Api
dotnet build src/BADBIR.UI.Components
dotnet build src/BADBIR.Web

# Run the API
dotnet run --project src/BADBIR.Api

# Run the web frontend (separate terminal)
dotnet run --project src/BADBIR.Web

# Run tests
dotnet test tests/BADBIR.Api.Tests

# Build the mobile project (requires MAUI workload + Android/iOS SDK)
dotnet build src/BADBIR.Mobile -f net8.0-android
```

---

## Project Reference Map

```
BADBIR.Shared
    ↑
    ├── BADBIR.Api
    ├── BADBIR.UI.Components
    │       ↑
    │       ├── BADBIR.Web
    │       └── BADBIR.Mobile
    └── (transitive via above)

BADBIR.Api
    ↑
    └── BADBIR.Api.Tests
```

---

## Roadmap

- [ ] Scaffold EF Core models from the existing SQL Server 2022 database
- [ ] Implement .NET Identity authentication endpoints
- [ ] Build EuroQol (EQ-5D-5L) form component in `BADBIR.UI.Components`
- [ ] Build HAQ form component
- [ ] Build HADS form component
- [ ] Wire up API client in `BADBIR.UI.Components` using `HttpClient`
- [ ] Add integration tests for all API endpoints
- [ ] Configure CI/CD pipeline (GitHub Actions)
- [ ] Configure Android and iOS MAUI build targets
