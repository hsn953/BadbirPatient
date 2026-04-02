using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BADBIR.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialPatientDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ClinicianPatientId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitTracking",
                columns: table => new
                {
                    VisitId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClinicianPatientId = table.Column<int>(type: "INTEGER", nullable: true),
                    PotentialFupCode = table.Column<int>(type: "INTEGER", nullable: false),
                    ImportedFupId = table.Column<int>(type: "INTEGER", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateEntered = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Comments = table.Column<string>(type: "TEXT", nullable: true),
                    VisitStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    DataStatus = table.Column<byte>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitTracking", x => x.VisitId);
                    table.ForeignKey(
                        name: "FK_VisitTracking_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CageSubmissions",
                columns: table => new
                {
                    CageId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VisitId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cutdown = table.Column<bool>(type: "INTEGER", nullable: true),
                    Annoyed = table.Column<bool>(type: "INTEGER", nullable: true),
                    Guilty = table.Column<bool>(type: "INTEGER", nullable: true),
                    Earlymorning = table.Column<bool>(type: "INTEGER", nullable: true),
                    DateCompleted = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DataStatus = table.Column<byte>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CageSubmissions", x => x.CageId);
                    table.ForeignKey(
                        name: "FK_CageSubmissions_VisitTracking_VisitId",
                        column: x => x.VisitId,
                        principalTable: "VisitTracking",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DlqiSubmissions",
                columns: table => new
                {
                    DlqiId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VisitId = table.Column<int>(type: "INTEGER", nullable: false),
                    Diagnosis = table.Column<string>(type: "TEXT", nullable: true),
                    ItchsoreScore = table.Column<int>(type: "INTEGER", nullable: true),
                    EmbscScore = table.Column<int>(type: "INTEGER", nullable: true),
                    ShophgScore = table.Column<int>(type: "INTEGER", nullable: true),
                    ClothesScore = table.Column<int>(type: "INTEGER", nullable: true),
                    SocleisScore = table.Column<int>(type: "INTEGER", nullable: true),
                    SportScore = table.Column<int>(type: "INTEGER", nullable: true),
                    WorkstudScore = table.Column<int>(type: "INTEGER", nullable: true),
                    WorkstudnoScore = table.Column<int>(type: "INTEGER", nullable: true),
                    PartcrfScore = table.Column<int>(type: "INTEGER", nullable: true),
                    SexdifScore = table.Column<int>(type: "INTEGER", nullable: true),
                    TreatmentScore = table.Column<int>(type: "INTEGER", nullable: true),
                    TotalScore = table.Column<int>(type: "INTEGER", nullable: true),
                    DateCompleted = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SkipBreakup = table.Column<int>(type: "INTEGER", nullable: false),
                    DataStatus = table.Column<byte>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DlqiSubmissions", x => x.DlqiId);
                    table.ForeignKey(
                        name: "FK_DlqiSubmissions_VisitTracking_VisitId",
                        column: x => x.VisitId,
                        principalTable: "VisitTracking",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EuroqolSubmissions",
                columns: table => new
                {
                    EuroqolId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VisitId = table.Column<int>(type: "INTEGER", nullable: false),
                    Mobility = table.Column<int>(type: "INTEGER", nullable: true),
                    Selfcare = table.Column<int>(type: "INTEGER", nullable: true),
                    Usualacts = table.Column<int>(type: "INTEGER", nullable: true),
                    Paindisc = table.Column<int>(type: "INTEGER", nullable: true),
                    Anxdepr = table.Column<int>(type: "INTEGER", nullable: true),
                    Comphealth = table.Column<int>(type: "INTEGER", nullable: true),
                    Howyoufeel = table.Column<int>(type: "INTEGER", nullable: true),
                    DateCompleted = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DataStatus = table.Column<byte>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EuroqolSubmissions", x => x.EuroqolId);
                    table.ForeignKey(
                        name: "FK_EuroqolSubmissions_VisitTracking_VisitId",
                        column: x => x.VisitId,
                        principalTable: "VisitTracking",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HadsSubmissions",
                columns: table => new
                {
                    HadsId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VisitId = table.Column<int>(type: "INTEGER", nullable: false),
                    Q01tense = table.Column<int>(type: "INTEGER", nullable: true),
                    Q03frightened = table.Column<int>(type: "INTEGER", nullable: true),
                    Q05worry = table.Column<int>(type: "INTEGER", nullable: true),
                    Q07relaxed = table.Column<int>(type: "INTEGER", nullable: true),
                    Q09butterflies = table.Column<int>(type: "INTEGER", nullable: true),
                    Q11restless = table.Column<int>(type: "INTEGER", nullable: true),
                    Q13panic = table.Column<int>(type: "INTEGER", nullable: true),
                    Q02enjoy = table.Column<int>(type: "INTEGER", nullable: true),
                    Q04laugh = table.Column<int>(type: "INTEGER", nullable: true),
                    Q06cheerful = table.Column<int>(type: "INTEGER", nullable: true),
                    Q08slowed = table.Column<int>(type: "INTEGER", nullable: true),
                    Q10appearence = table.Column<int>(type: "INTEGER", nullable: true),
                    Q12enjoyment = table.Column<int>(type: "INTEGER", nullable: true),
                    Q14goodbook = table.Column<int>(type: "INTEGER", nullable: true),
                    ScoreAnxiety = table.Column<int>(type: "INTEGER", nullable: true),
                    ResultAnxiety = table.Column<int>(type: "INTEGER", nullable: true),
                    ScoreDepression = table.Column<int>(type: "INTEGER", nullable: true),
                    ResultDepression = table.Column<int>(type: "INTEGER", nullable: true),
                    DateScored = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsCountable = table.Column<bool>(type: "INTEGER", nullable: false),
                    DataStatus = table.Column<byte>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HadsSubmissions", x => x.HadsId);
                    table.ForeignKey(
                        name: "FK_HadsSubmissions_VisitTracking_VisitId",
                        column: x => x.VisitId,
                        principalTable: "VisitTracking",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HaqSubmissions",
                columns: table => new
                {
                    HaqId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VisitId = table.Column<int>(type: "INTEGER", nullable: false),
                    Missingdata = table.Column<bool>(type: "INTEGER", nullable: true),
                    Missingdatadetails = table.Column<string>(type: "TEXT", nullable: true),
                    Dressself = table.Column<int>(type: "INTEGER", nullable: true),
                    Shampoo = table.Column<int>(type: "INTEGER", nullable: true),
                    Standchair = table.Column<int>(type: "INTEGER", nullable: true),
                    Bed = table.Column<int>(type: "INTEGER", nullable: true),
                    Cutmeat = table.Column<int>(type: "INTEGER", nullable: true),
                    Liftglass = table.Column<int>(type: "INTEGER", nullable: true),
                    Openmilk = table.Column<int>(type: "INTEGER", nullable: true),
                    Walkflat = table.Column<int>(type: "INTEGER", nullable: true),
                    Climbsteps = table.Column<int>(type: "INTEGER", nullable: true),
                    Washdry = table.Column<int>(type: "INTEGER", nullable: true),
                    Bath = table.Column<int>(type: "INTEGER", nullable: true),
                    Toilet = table.Column<int>(type: "INTEGER", nullable: true),
                    Reachabove = table.Column<int>(type: "INTEGER", nullable: true),
                    Bend = table.Column<int>(type: "INTEGER", nullable: true),
                    Cardoor = table.Column<int>(type: "INTEGER", nullable: true),
                    Openjar = table.Column<int>(type: "INTEGER", nullable: true),
                    Turntap = table.Column<int>(type: "INTEGER", nullable: true),
                    Shop = table.Column<int>(type: "INTEGER", nullable: true),
                    Getincar = table.Column<int>(type: "INTEGER", nullable: true),
                    Housework = table.Column<int>(type: "INTEGER", nullable: true),
                    Cane = table.Column<int>(type: "INTEGER", nullable: true),
                    Crutches = table.Column<int>(type: "INTEGER", nullable: true),
                    Walker = table.Column<int>(type: "INTEGER", nullable: true),
                    Wheelchair = table.Column<int>(type: "INTEGER", nullable: true),
                    Specialutensils = table.Column<int>(type: "INTEGER", nullable: true),
                    Specialchair = table.Column<int>(type: "INTEGER", nullable: true),
                    Dressing = table.Column<int>(type: "INTEGER", nullable: true),
                    Dressingdetails = table.Column<string>(type: "TEXT", nullable: true),
                    Loolift = table.Column<int>(type: "INTEGER", nullable: true),
                    Bathseat = table.Column<int>(type: "INTEGER", nullable: true),
                    Bathrail = table.Column<int>(type: "INTEGER", nullable: true),
                    Longreach = table.Column<int>(type: "INTEGER", nullable: true),
                    Jaropener = table.Column<int>(type: "INTEGER", nullable: true),
                    Deviceother = table.Column<string>(type: "TEXT", nullable: true),
                    Dressgroom = table.Column<int>(type: "INTEGER", nullable: true),
                    Rising = table.Column<int>(type: "INTEGER", nullable: true),
                    Eating = table.Column<int>(type: "INTEGER", nullable: true),
                    Walking = table.Column<int>(type: "INTEGER", nullable: true),
                    Hygiene = table.Column<int>(type: "INTEGER", nullable: true),
                    Reach = table.Column<int>(type: "INTEGER", nullable: true),
                    Gripping = table.Column<int>(type: "INTEGER", nullable: true),
                    Errands = table.Column<int>(type: "INTEGER", nullable: true),
                    Totalscore = table.Column<int>(type: "INTEGER", nullable: true),
                    Haqscore = table.Column<double>(type: "REAL", nullable: true),
                    DateScored = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DataStatus = table.Column<byte>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HaqSubmissions", x => x.HaqId);
                    table.ForeignKey(
                        name: "FK_HaqSubmissions_VisitTracking_VisitId",
                        column: x => x.VisitId,
                        principalTable: "VisitTracking",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LifestyleSubmissions",
                columns: table => new
                {
                    LifestyleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VisitId = table.Column<int>(type: "INTEGER", nullable: false),
                    Birthtown = table.Column<string>(type: "TEXT", nullable: true),
                    Birthcountry = table.Column<string>(type: "TEXT", nullable: true),
                    Workstatusid = table.Column<int>(type: "INTEGER", nullable: true),
                    Occupation = table.Column<string>(type: "TEXT", nullable: true),
                    Ethnicityid = table.Column<int>(type: "INTEGER", nullable: true),
                    Otherethnicity = table.Column<string>(type: "TEXT", nullable: true),
                    Outdooroccupation = table.Column<bool>(type: "INTEGER", nullable: true),
                    Livetropical = table.Column<bool>(type: "INTEGER", nullable: true),
                    Eversmoked = table.Column<bool>(type: "INTEGER", nullable: true),
                    Eversmokednumbercigsperday = table.Column<int>(type: "INTEGER", nullable: true),
                    Agestart = table.Column<int>(type: "INTEGER", nullable: true),
                    Agestop = table.Column<int>(type: "INTEGER", nullable: true),
                    Currentlysmoke = table.Column<bool>(type: "INTEGER", nullable: true),
                    Currentlysmokenumbercigsperday = table.Column<int>(type: "INTEGER", nullable: true),
                    Drnkbeeravg = table.Column<int>(type: "INTEGER", nullable: true),
                    Drnkwineavg = table.Column<int>(type: "INTEGER", nullable: true),
                    Drnkspiritsavg = table.Column<int>(type: "INTEGER", nullable: true),
                    Drinkalcohol = table.Column<bool>(type: "INTEGER", nullable: true),
                    Drnkunitsavg = table.Column<int>(type: "INTEGER", nullable: true),
                    Admittedtohospital = table.Column<int>(type: "INTEGER", nullable: true),
                    Newdrugs = table.Column<int>(type: "INTEGER", nullable: true),
                    Newclinics = table.Column<int>(type: "INTEGER", nullable: true),
                    Systolic = table.Column<float>(type: "REAL", nullable: true),
                    Diastolic = table.Column<float>(type: "REAL", nullable: true),
                    Height = table.Column<float>(type: "REAL", nullable: true),
                    Weight = table.Column<float>(type: "REAL", nullable: true),
                    Waist = table.Column<float>(type: "REAL", nullable: true),
                    WeightMissing = table.Column<bool>(type: "INTEGER", nullable: false),
                    WaistMissing = table.Column<bool>(type: "INTEGER", nullable: false),
                    SmokingMissing = table.Column<bool>(type: "INTEGER", nullable: false),
                    DrinkingMissing = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCompleted = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DataStatus = table.Column<byte>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifestyleSubmissions", x => x.LifestyleId);
                    table.ForeignKey(
                        name: "FK_LifestyleSubmissions_VisitTracking_VisitId",
                        column: x => x.VisitId,
                        principalTable: "VisitTracking",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PgaSubmissions",
                columns: table => new
                {
                    PgaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VisitId = table.Column<int>(type: "INTEGER", nullable: false),
                    Pgascore = table.Column<int>(type: "INTEGER", nullable: true),
                    DateScored = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DataStatus = table.Column<byte>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PgaSubmissions", x => x.PgaId);
                    table.ForeignKey(
                        name: "FK_PgaSubmissions_VisitTracking_VisitId",
                        column: x => x.VisitId,
                        principalTable: "VisitTracking",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SapasiSubmissions",
                columns: table => new
                {
                    SapasiId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VisitId = table.Column<int>(type: "INTEGER", nullable: false),
                    HeadCoverage = table.Column<int>(type: "INTEGER", nullable: true),
                    HeadErythema = table.Column<int>(type: "INTEGER", nullable: true),
                    HeadInduration = table.Column<int>(type: "INTEGER", nullable: true),
                    HeadDesquamation = table.Column<int>(type: "INTEGER", nullable: true),
                    TrunkCoverage = table.Column<int>(type: "INTEGER", nullable: true),
                    TrunkErythema = table.Column<int>(type: "INTEGER", nullable: true),
                    TrunkInduration = table.Column<int>(type: "INTEGER", nullable: true),
                    TrunkDesquamation = table.Column<int>(type: "INTEGER", nullable: true),
                    UpperLimbsCoverage = table.Column<int>(type: "INTEGER", nullable: true),
                    UpperLimbsErythema = table.Column<int>(type: "INTEGER", nullable: true),
                    UpperLimbsInduration = table.Column<int>(type: "INTEGER", nullable: true),
                    UpperLimbsDesquamation = table.Column<int>(type: "INTEGER", nullable: true),
                    LowerLimbsCoverage = table.Column<int>(type: "INTEGER", nullable: true),
                    LowerLimbsErythema = table.Column<int>(type: "INTEGER", nullable: true),
                    LowerLimbsInduration = table.Column<int>(type: "INTEGER", nullable: true),
                    LowerLimbsDesquamation = table.Column<int>(type: "INTEGER", nullable: true),
                    SapasiScore = table.Column<float>(type: "REAL", nullable: true),
                    DateScored = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DataStatus = table.Column<byte>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SapasiSubmissions", x => x.SapasiId);
                    table.ForeignKey(
                        name: "FK_SapasiSubmissions_VisitTracking_VisitId",
                        column: x => x.VisitId,
                        principalTable: "VisitTracking",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CageSubmissions_VisitId",
                table: "CageSubmissions",
                column: "VisitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DlqiSubmissions_VisitId",
                table: "DlqiSubmissions",
                column: "VisitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EuroqolSubmissions_VisitId",
                table: "EuroqolSubmissions",
                column: "VisitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HadsSubmissions_VisitId",
                table: "HadsSubmissions",
                column: "VisitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HaqSubmissions_VisitId",
                table: "HaqSubmissions",
                column: "VisitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LifestyleSubmissions_VisitId",
                table: "LifestyleSubmissions",
                column: "VisitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PgaSubmissions_VisitId",
                table: "PgaSubmissions",
                column: "VisitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SapasiSubmissions_VisitId",
                table: "SapasiSubmissions",
                column: "VisitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisitTracking_UserId",
                table: "VisitTracking",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CageSubmissions");

            migrationBuilder.DropTable(
                name: "DlqiSubmissions");

            migrationBuilder.DropTable(
                name: "EuroqolSubmissions");

            migrationBuilder.DropTable(
                name: "HadsSubmissions");

            migrationBuilder.DropTable(
                name: "HaqSubmissions");

            migrationBuilder.DropTable(
                name: "LifestyleSubmissions");

            migrationBuilder.DropTable(
                name: "PgaSubmissions");

            migrationBuilder.DropTable(
                name: "SapasiSubmissions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "VisitTracking");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
