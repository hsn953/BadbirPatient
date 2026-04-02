# BADBIR Patient App — Database Setup Guide

## Overview

The BADBIR Patient Application uses its own standalone SQL Server database named **`BadbirPatient`**.
This database is **completely independent** of the Clinician System's BADBIR database. There is no
shared table access between the two systems; all inter-system communication happens through
the API integration layer (`IClinicianSystemClient` and the `/api/internal/*` routes).

---

## Database tables

The following tables are created by EF Core migrations:

| Table | Description |
|---|---|
| `AspNetUsers` | Patient portal accounts (ASP.NET Core Identity) |
| `AspNetRoles` | Roles (Patient, Administrator, InternalService) |
| `AspNetUserRoles` | User–role mappings |
| `AspNetUserClaims` / `AspNetUserLogins` / `AspNetUserTokens` | Identity infrastructure |
| `VisitTracking` | One row per portal visit session (baseline or follow-up) |
| `DlqiSubmissions` | DLQI form answers |
| `LifestyleSubmissions` | Lifestyle / demographics form answers |
| `CageSubmissions` | CAGE alcohol screening answers |
| `EuroqolSubmissions` | EuroQol EQ-5D-3L form answers |
| `HadsSubmissions` | HADS anxiety & depression answers |
| `HaqSubmissions` | HAQ-DI disability index answers |
| `PgaSubmissions` | Patient Global Assessment scores |
| `SapasiSubmissions` | SAPASI self-assessed PASI scores |
| `__EFMigrationsHistory` | EF Core migration tracking |

---

## Option A — SQLite (development / local testing)

SQLite is used automatically when `ConnectionStrings:BadbirDbSqlite` is set in
`appsettings.Development.json`. The file is stored at `src/BADBIR.Api/data/BadbirPatient.db`
(relative to the project directory when running via `dotnet run`).

### First-time setup

1. Ensure you have the EF Core CLI tools installed:
   ```
   dotnet tool install --global dotnet-ef
   ```

2. From the `src/BADBIR.Api/` directory, run:
   ```
   dotnet ef database update
   ```
   This creates `data/BadbirPatient.db` and applies all migrations.

3. Run the API:
   ```
   dotnet run
   ```
   The API will connect to `data/BadbirPatient.db` automatically in Development mode.

### Resetting the SQLite database

Simply delete the file and re-run `dotnet ef database update`:
```
del src\BADBIR.Api\data\BadbirPatient.db    # Windows
rm  src/BADBIR.Api/data/BadbirPatient.db    # macOS / Linux
dotnet ef database update
```

---

## Option B — SQL Server (staging / production)

SQL Server is used when `ConnectionStrings:BadbirDbSqlite` is **absent** or when you
explicitly want to test against SQL Server locally.

### Creating the SQL Server database

1. Open **SQL Server Management Studio (SSMS)** and connect to your server.

2. Create a new empty database named `BadbirPatient` (or `BadbirPatient_Dev` for your
   local development instance):
   ```sql
   CREATE DATABASE BadbirPatient;
   ```

3. Update `appsettings.Development.json` (or `appsettings.json` for production):
   ```json
   "ConnectionStrings": {
     "BadbirDb": "Server=YOUR_SERVER_NAME;Database=BadbirPatient;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```
   Replace `YOUR_SERVER_NAME` with your SQL Server instance name (e.g. `localhost`,
   `.\SQLEXPRESS`, or a named instance like `DESKTOP-ABC\SQLEXPRESS`).

4. **Remove or comment out** `BadbirDbSqlite` from `appsettings.Development.json` so the
   app switches to SQL Server:
   ```json
   "ConnectionStrings": {
     "BadbirDb": "Server=...;Database=BadbirPatient;...",
     // "BadbirDbSqlite": "Data Source=data/BadbirPatient.db"   ← comment out
   }
   ```

5. Run migrations from the `src/BADBIR.Api/` directory:
   ```
   dotnet ef database update
   ```
   EF Core will detect the SQL Server connection string and create all tables in the
   `BadbirPatient` database.

6. Verify in SSMS — you should see all the tables listed in the table above under
   `BadbirPatient > Tables`.

---

## Switching between SQLite and SQL Server

The selection logic is in `Program.cs`:

```csharp
var sqliteConnStr = builder.Configuration.GetConnectionString("BadbirDbSqlite");

if (!string.IsNullOrEmpty(sqliteConnStr))
    builder.Services.AddDbContext<BadbirDbContext>(o => o.UseSqlite(sqliteConnStr));
else
    builder.Services.AddDbContext<BadbirDbContext>(o => o.UseSqlServer(sqlServerConnStr));
```

| To use | Set in `appsettings.Development.json` |
|---|---|
| **SQLite** | Include `"BadbirDbSqlite": "Data Source=data/BadbirPatient.db"` |
| **SQL Server** | Remove / comment out `BadbirDbSqlite` |

> **Tip:** You can also override via environment variables or `dotnet run --launch-profile`.

---

## Adding future migrations

When you add new entity properties or tables:

1. Update the entity class.
2. Run:
   ```
   dotnet ef migrations add <DescriptiveName>
   ```
3. Inspect the generated migration in `src/BADBIR.Api/Migrations/`.
4. Apply:
   ```
   dotnet ef database update
   ```

Both SQLite and SQL Server will apply the same migration files. EF Core automatically
generates compatible SQL for both providers.

---

## Connection string examples

### Windows Authentication (local SQL Server Express)
```json
"BadbirDb": "Server=.\\SQLEXPRESS;Database=BadbirPatient;Trusted_Connection=True;TrustServerCertificate=True;"
```

### SQL Server with username/password
```json
"BadbirDb": "Server=myserver.database.windows.net;Database=BadbirPatient;User Id=sa;Password=YourStrongPassword!;TrustServerCertificate=True;"
```

### Azure SQL Database
```json
"BadbirDb": "Server=tcp:myserver.database.windows.net,1433;Database=BadbirPatient;Authentication=Active Directory Default;TrustServerCertificate=False;Encrypt=True;"
```

---

## Notes for production deployment

- **Never** set `BadbirDbSqlite` in production. SQLite is for development only.
- The `Jwt:Key`, `EncryptionServiceConfig:Password`, and database password must all be
  provided via secure environment variables or a secrets manager — not in `appsettings.json`.
- Set `SignIn.RequireConfirmedEmail = true` in `Program.cs` before go-live.
