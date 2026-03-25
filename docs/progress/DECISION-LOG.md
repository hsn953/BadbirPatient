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
