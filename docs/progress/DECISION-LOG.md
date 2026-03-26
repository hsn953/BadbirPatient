# DECISION-LOG – Architectural & Design Decisions
## BADBIR Patient Application

> **Document ID:** DECISION-LOG  
> **Last Updated:** 2026-03-25

All significant architectural, design, and technology decisions are recorded here with rationale. This enables future developers to understand *why* choices were made and what alternatives were considered.

---

## Decision Format

Each entry includes:
- **Date** — when the decision was made
- **Status** — Decided / Under Discussion / Superseded
- **Context** — why a decision was needed
- **Decision** — what was decided
- **Alternatives Considered** — what else was evaluated
- **Consequences** — what this means going forward

---

## ADR-001: .NET 10 as the Target Framework

**Date:** 2026-03-25  
**Status:** Decided  

**Context:** The legacy system used .NET 4.5, .NET 6, and Xamarin (pre-.NET MAUI). All were end-of-life or approaching it. A unified modern target framework was required.

**Decision:** All projects target **net10.0** (.NET 10, current LTS-candidate).

**Alternatives Considered:**
- .NET 8 (current LTS) — rejected: client requirement explicitly states .NET 10.
- .NET 9 — rejected: not an LTS release.

**Consequences:** net10.0-android / net10.0-ios target monikers used for MAUI. All packages must support net10.0.

---

## ADR-002: No Swashbuckle — Native Microsoft.AspNetCore.OpenApi

**Date:** 2026-03-25  
**Status:** Decided  

**Context:** The legacy API used Swashbuckle. The master requirements document explicitly prohibits it.

**Decision:** Use native `Microsoft.AspNetCore.OpenApi` 10.0.x. OpenAPI JSON served at `/openapi/v1.json` in Development.

**Alternatives Considered:**
- Swashbuckle — explicitly prohibited by requirements.
- NSwag — also third-party, not allowed.

**Consequences:** Native OpenAPI in .NET 10 has fewer customisation points than Swashbuckle. UI tooling (Scalar, Swagger UI) can still be mounted if needed, but documentation generation is native.

---

## ADR-003: JWT Bearer with ASP.NET Core Identity (OIDC-Compatible)

**Date:** 2026-03-25  
**Status:** Decided  

**Context:** The legacy API used custom JWT generation. The master requirements call for JWT Bearer tokens, OIDC-compatible architecture, and a future NHS Login integration path.

**Decision:** 
- `AddIdentityCore<ApplicationUser>().AddApiEndpoints()` for Identity management.
- `AddJwtBearer()` for token validation.
- `MapIdentityApi<ApplicationUser>()` for built-in Identity endpoints.
- Custom `AuthController` for registration pathways and account recovery.

**Alternatives Considered:**
- Full IdentityServer / Duende — over-engineered for current scope.
- Third-party OAuth server — unnecessary complexity at this stage.

**Consequences:** The `MapIdentityApi` built-in endpoints issue opaque tokens; additional claims (roles, SecurityStamp) require a custom token service. Future NHS Login integration adds an external OIDC provider without changing the JWT Bearer validation pipeline.

---

## ADR-004: EF Core 10 Database-First

**Date:** 2026-03-25  
**Status:** Decided  

**Context:** An existing SQL Server 2022 production database is in use by the Clinician System and cannot be destabilised by code-first migrations.

**Decision:** Use **EF Core 10 Database-First** (`dotnet ef dbcontext scaffold`). All schema changes are applied via versioned SQL scripts, not EF migrations.

**Alternatives Considered:**
- Code-First with migrations — rejected: would conflict with the existing production DB schema managed by the Clinician System's separate team.
- Raw ADO.NET / Dapper — rejected: EF Core provides LINQ, change tracking, and testability benefits.

**Consequences:** Scaffolded entity files must not be edited directly — customisations go in `IEntityTypeConfiguration<T>` classes. The scaffolding command must be re-run when the DB schema changes.

---

## ADR-005: Razor Class Library for Shared UI (BADBIR.UI.Components)

**Date:** 2026-03-25  
**Status:** Decided  

**Context:** The legacy system had completely separate UIs for web and mobile. Code duplication was a major maintenance problem.

**Decision:** ~95% of UI lives in `BADBIR.UI.Components` (Razor Class Library). `BADBIR.Web` and `BADBIR.Mobile` are thin hosts.

**Alternatives Considered:**
- Separate Blazor components per host — rejected: defeats the purpose of MAUI Blazor Hybrid.
- MVVM shared library — not applicable for Blazor architecture.

**Consequences:** All form components, pages, and layouts must be developed in the RCL. Host-specific features (biometrics, push notifications, SecureStorage) are abstracted behind interfaces injected from each host.

---

## ADR-006: AES-256 Encryption for PII at the API Layer

**Date:** 2026-03-25  
**Status:** Decided (algorithm details pending client confirmation)  

**Context:** NHS Number, name, DOB-related fields are PII that must be encrypted at rest under GDPR. The encryption algorithm must match the Clinician System.

**Decision:** 
- AES-256-CBC encryption at the API service layer (before EF Core persistence).
- HMAC-SHA256 hash stored alongside NHS/CHI Number for deterministic lookups.
- Two separate keys: AES key (for encryption) and HMAC key (for hashing).
- Keys stored in environment variables / AWS Secrets Manager (never in source code).

**Alternatives Considered:**
- SQL Server Always Encrypted — rejected: adds DB-level dependency, complicates portability.
- ASP.NET Core Data Protection API — rejected: must match the existing Clinician System algorithm exactly.

**Consequences:** The actual AES key and IV strategy must be confirmed with the Clinician System team before implementing. A code stub `IEncryptionService` is created, to be filled in when the algorithm is confirmed.

---

## ADR-007: SQLite for Mobile Offline Storage

**Date:** 2026-03-25  
**Status:** Decided  

**Context:** Hospital environments have poor connectivity. The MAUI app must support offline form submission with automatic sync.

**Decision:** Use **SQLite via EF Core** (`Microsoft.EntityFrameworkCore.Sqlite`) for local pending submission storage on the mobile device.

**Alternatives Considered:**
- Preferences/SecureStorage — too limited for structured form data.
- LiteDB — not EF-based; adds a second ORM.
- Manual JSON files — no ACID guarantees, harder to query.

**Consequences:** The offline DB uses a separate `OfflineDbContext`. The sync service reads `PendingSubmissions` and posts to the API. A strategy for conflict resolution (409 Conflict = discard local copy) must be implemented.

---

## ADR-008: Phase 1 IIS, Phase 2 Docker/AWS — Configuration Portability

**Date:** 2026-03-25  
**Status:** Decided  

**Context:** Initial deployment is on-premise (IIS/Windows Server 2022). Future deployment is AWS (Docker containers). No code changes should be required for the Phase 2 migration.

**Decision:** All environment-specific values (connection strings, keys, origins) are read from `IConfiguration` (environment variables). File storage and email are abstracted behind interfaces with swappable implementations.

**Consequences:** Requires `IEmailService` (SMTP → SES) and `IFileStorageService` (local → S3) abstractions from day one, even if only the Phase 1 implementations are built initially.

---

## Pending Decisions

| ID | Decision Needed | Owner | Deadline |
|---|---|---|---|
| PD-01 | EuroQol version: EQ-5D-3L vs EQ-5D-5L | Client | Before EuroQol implementation sprint |
| PD-02 | Encryption algorithm specifics (key size, IV mode) from Clinician System | Client / DBA | Before any PII data storage |
| PD-03 | SAPASI UX approach: shaded slider vs. interactive body map | Client / UX | Before SAPASI sprint |
| PD-04 | Full database DDL (CREATE TABLE scripts) | DBA | Before scaffolding sprint |
| PD-05 | Notification service provider: FCM / AWS SNS / other | Client | Before notification sprint |
| PD-06 | HADS Q11–Q14 inclusion confirmation | Client | Before HADS implementation sprint |
| PD-07 | NHS Login integration timeline and OIDC provider specs | Client / NHS Digital | Long-term planning |

---

## ADR-009: EQ-5D-3L Confirmed — OQ-01 Closed

**Date:** 2026-03-26  
**Status:** Decided  

**Context:** OQ-01 asked whether the new system should use EQ-5D-3L or EQ-5D-5L. The actual paper forms (Baseline p.9, FUP p.9) confirm exactly 3 levels per dimension.

**Decision:** Implement **EQ-5D-3L**. Existing legacy DB columns (`mobility`, `selfcare`, `usualacts`, `paindisc`, `anxdepr`) are correct. No schema change required for the EuroQol dimensions.

**Consequences:** The Dolan (1997) England EQ-5D-3L value set coefficients must be obtained under the EuroQol licence.

---

## ADR-010: SAPASI UX Design Decision Deferred — Feature Is In v1 Scope

**Date:** 2026-03-26  
**Status:** Decided  

**Context:** Initial interpretation of client guidance was that SAPASI was deferred entirely from v1. Client clarified: *"Just to confirm when I say defer SAPASI, I mean defer the real implementation questions how to show graphically etc close to the time we are developing — we still want to include SAPASI in the first product."*

**Decision:** SAPASI **is in v1 scope**. The data model, DB table (`SapasiSubmissions`), API endpoint (`POST /api/forms/sapasi`), and scoring algorithm are all fixed and fully specified in FORM-007. The **only deferred decision** is the graphical UI interaction model (Option A: static silhouette + sliders vs. Option B: interactive tappable body map). This UX decision will be made at the SAPASI development sprint.

**Consequences:**
- FORM-007 is updated from "DEFERRED" to "In scope for v1 — UX design pending".
- `SapasiSubmissions` table remains in DSD-001 §4.5.
- `POST /api/forms/sapasi` remains in API-001.
- SAPASI is included in form sequences in SRS-001 FR-DASH-02.
- The API and scoring service can be built independently of the UI design decision.

---

## ADR-011: SQLite for Development and Testing

**Date:** 2026-03-26  
**Status:** Decided  

**Context:** The client proposes providing a SQLite database with synthetic test data for use in development and automated testing. This allows development to proceed without connection to the production SQL Server environment.

**Decision:** Use **EF Core with SQLite** for development/testing, switching to SQL Server for production. EF Core's provider abstraction makes this transparent — the same `DbContext` and LINQ queries work against both. See TST-001 for full testing strategy.

**Key points:**
- `BADBIR.Api.Tests` project will use an in-memory SQLite DB seeded from a test fixture.
- Client will provide a `.db` SQLite file with synthetic data — stored in `tests/TestData/`.
- Connection string switching: `"BadbirDb"` from `appsettings.Development.json` → SQLite path; production → SQL Server.
- EF Core SQLite provider: `Microsoft.EntityFrameworkCore.Sqlite`.

**Consequences:** Some SQL Server–specific features (e.g., `datetime2`, `rowversion`, full-text search, computed columns) require special handling in SQLite. Any such features are documented in TST-001.

---

## ADR-012: PGA Scale Correction (5-Level Categorical)

**Date:** 2026-03-26  
**Status:** Decided  

**Context:** The previous documentation assumed PGA was a 0–10 numeric scale. The actual paper form (Baseline p.6 bottom, FUP p.6 top) uses a 5-level categorical scale.

**Decision:** PGA is stored as integers 1–5: Clear=1, Almost clear=2, Mild=3, Moderate=4, Severe=5.

**Consequences:** The legacy `pgascore INT?` column must be confirmed to use this mapping. The API response includes `pgaLabel` for display purposes.

---

## ADR-013: HADS Q11–Q14 Confirmed as Required — OQ-02a Closed

**Date:** 2026-03-26  
**Status:** Decided  

**Context:** Previous documentation flagged uncertainty about whether Q11–Q14 were implemented in the legacy system. The actual paper forms confirm all 14 items.

**Decision:** All 14 HADS items are implemented. Q11–Q14 DB fields (`q11restless`, `q12lookforward`, `q13panic`, `q14enjoybook`) must be present in the entity. The legacy NSwag DTO only showed Q1–Q10 — this was a limitation of what was decompiled, not the actual DB schema.

**Pending decisions updated:**

| ID | Status | Resolution |
|---|---|---|
| PD-01 EuroQol version | ✅ CLOSED | EQ-5D-3L confirmed |
| PD-02 Encryption algorithm | 🔵 OPEN | Pending Clinician System code share |
| PD-03 SAPASI UX | ✅ CLOSED | Deferred to v2 |
| PD-04 Full DB DDL | 🔵 OPEN | SQLite DB forthcoming |
| PD-05 Notification service | 🔵 OPEN | Pending |
| PD-06 HADS Q11–Q14 | ✅ CLOSED | All 14 items confirmed |
| PD-07 NHS Login | 🔵 OPEN | Long-term |
