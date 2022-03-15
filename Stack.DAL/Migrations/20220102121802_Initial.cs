using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "varchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    NameAR = table.Column<string>(type: "varchar(70)", nullable: true),
                    NameEN = table.Column<string>(type: "varchar(70)", nullable: true),
                    Gender = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "varchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCodes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    Code = table.Column<string>(type: "varchar(20)", nullable: true),
                    Currency = table.Column<string>(type: "varchar(3)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCodes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Condition",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "varchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    Level = table.Column<string>(type: "varchar(10)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condition", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeGroup",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(255)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeGroup", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MaterialGroup",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    Code = table.Column<string>(type: "varchar(20)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialGroup", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MaterialType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    Code = table.Column<string>(type: "varchar(20)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "varchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RFQTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "varchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFQTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Taxes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "varchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    Percentage = table.Column<float>(nullable: false),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UOM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(type: "varchar(10)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UOM", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAR = table.Column<string>(type: "varchar(255)", nullable: true),
                    NameEN = table.Column<string>(type: "varchar(255)", nullable: true),
                    SystemUsername = table.Column<string>(type: "varchar(255)", nullable: true),
                    TaxRegistrationNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
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
                name: "CostCenters",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: true),
                    CompanyCodeID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CostCenters_CompanyCodes_CompanyCodeID",
                        column: x => x.CompanyCodeID,
                        principalTable: "CompanyCodes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstCurrency = table.Column<string>(type: "varchar(3)", nullable: true),
                    SecondCurrency = table.Column<string>(type: "varchar(3)", nullable: true),
                    Rate = table.Column<float>(nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: true),
                    CompanyCodeID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_CompanyCodes_CompanyCodeID",
                        column: x => x.CompanyCodeID,
                        principalTable: "CompanyCodes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrgUnits",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ManagerId = table.Column<long>(nullable: false),
                    IsPurchasing = table.Column<bool>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    CompanyCodeID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgUnits", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrgUnits_CompanyCodes_CompanyCodeID",
                        column: x => x.CompanyCodeID,
                        principalTable: "CompanyCodes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plant",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    Code = table.Column<string>(type: "varchar(20)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    CompanyCodeID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Plant_CompanyCodes_CompanyCodeID",
                        column: x => x.CompanyCodeID,
                        principalTable: "CompanyCodes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfitCenters",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: true),
                    CompanyCodeID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfitCenters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProfitCenters_CompanyCodes_CompanyCodeID",
                        column: x => x.CompanyCodeID,
                        principalTable: "CompanyCodes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSubGroup",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(255)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    EmployeeGroupID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSubGroup", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeeSubGroup_EmployeeGroup_EmployeeGroupID",
                        column: x => x.EmployeeGroupID,
                        principalTable: "EmployeeGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GLAccounts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    ControllingArea = table.Column<string>(type: "varchar(200)", nullable: true),
                    Currency = table.Column<string>(type: "varchar(3)", nullable: true),
                    Type = table.Column<string>(type: "varchar(30)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    MaterialGroupID = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GLAccounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GLAccounts_MaterialGroup_MaterialGroupID",
                        column: x => x.MaterialGroupID,
                        principalTable: "MaterialGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    Code = table.Column<string>(type: "varchar(20)", nullable: true),
                    EAN = table.Column<string>(type: "varchar(100)", nullable: true),
                    PriceIndicator = table.Column<string>(type: "varchar(1)", nullable: true),
                    Stock = table.Column<long>(nullable: false),
                    MovingAverage = table.Column<float>(nullable: false),
                    StandardPrice = table.Column<float>(nullable: false),
                    MinimumOrderAmount = table.Column<float>(nullable: false),
                    SafetyStock = table.Column<float>(nullable: false),
                    MaxStock = table.Column<float>(nullable: false),
                    ReOrderPoint = table.Column<float>(nullable: false),
                    LeadTime = table.Column<float>(nullable: false),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    MaterialTypeID = table.Column<int>(nullable: false),
                    MaterialGroupID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Material_MaterialGroup_MaterialGroupID",
                        column: x => x.MaterialGroupID,
                        principalTable: "MaterialGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Material_MaterialType_MaterialTypeID",
                        column: x => x.MaterialTypeID,
                        principalTable: "MaterialType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRS",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "varchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    Currency = table.Column<string>(type: "varchar(3)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    PRTypeID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRS_PRTypes_PRTypeID",
                        column: x => x.PRTypeID,
                        principalTable: "PRTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RFQS",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "varchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    Currency = table.Column<string>(type: "varchar(3)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    RFQTypeID = table.Column<int>(nullable: false),
                    VendorID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFQS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RFQS_RFQTypes_RFQTypeID",
                        column: x => x.RFQTypeID,
                        principalTable: "RFQTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RFQS_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendor_Bank",
                columns: table => new
                {
                    VendorID = table.Column<long>(nullable: false),
                    BankID = table.Column<int>(nullable: false),
                    IBAN = table.Column<string>(type: "varchar(100)", nullable: true),
                    AccountID = table.Column<string>(type: "varchar(10)", nullable: true),
                    SwiftCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor_Bank", x => new { x.VendorID, x.BankID });
                    table.ForeignKey(
                        name: "FK_Vendor_Bank_Banks_BankID",
                        column: x => x.BankID,
                        principalTable: "Banks",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Vendor_Bank_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Vendor_CompanyCode",
                columns: table => new
                {
                    VendorID = table.Column<long>(nullable: false),
                    CompanyCodeID = table.Column<int>(nullable: false),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor_CompanyCode", x => new { x.VendorID, x.CompanyCodeID });
                    table.ForeignKey(
                        name: "FK_Vendor_CompanyCode_CompanyCodes_CompanyCodeID",
                        column: x => x.CompanyCodeID,
                        principalTable: "CompanyCodes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Vendor_CompanyCode_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Vendor_Tax",
                columns: table => new
                {
                    VendorID = table.Column<long>(nullable: false),
                    TaxID = table.Column<int>(nullable: false),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor_Tax", x => new { x.VendorID, x.TaxID });
                    table.ForeignKey(
                        name: "FK_Vendor_Tax_Taxes_TaxID",
                        column: x => x.TaxID,
                        principalTable: "Taxes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Vendor_Tax_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "VendorAddresses",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "varchar(70)", nullable: true),
                    Country = table.Column<string>(type: "varchar(70)", nullable: true),
                    Governorate = table.Column<string>(type: "varchar(70)", nullable: true),
                    District = table.Column<string>(type: "varchar(70)", nullable: true),
                    BuildingNumber = table.Column<string>(type: "varchar(70)", nullable: true),
                    OtherInfo = table.Column<string>(type: "varchar(255)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    VendorID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorAddresses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VendorAddresses_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorEmails",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    VendorID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorEmails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VendorEmails_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorPhoneNumbers",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "varchar(16)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    VendorID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorPhoneNumbers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VendorPhoneNumbers_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HiringDate = table.Column<DateTime>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    ManagerID = table.Column<long>(nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    ApplicationUserID = table.Column<string>(nullable: true),
                    CostCenterID = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_CostCenters_CostCenterID",
                        column: x => x.CostCenterID,
                        principalTable: "CostCenters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    OrgUnitID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Positions_OrgUnits_OrgUnitID",
                        column: x => x.OrgUnitID,
                        principalTable: "OrgUnits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchasingGroups",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    Code = table.Column<string>(type: "varchar(5)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: true),
                    PlantID = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasingGroups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PurchasingGroups_Plant_PlantID",
                        column: x => x.PlantID,
                        principalTable: "Plant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StorageLocation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    Code = table.Column<string>(type: "varchar(20)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    PlantID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageLocation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StorageLocation_Plant_PlantID",
                        column: x => x.PlantID,
                        principalTable: "Plant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Material_UOM",
                columns: table => new
                {
                    MaterialID = table.Column<long>(nullable: false),
                    UOMID = table.Column<int>(nullable: false),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material_UOM", x => new { x.MaterialID, x.UOMID });
                    table.ForeignKey(
                        name: "FK_Material_UOM_Material_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Material",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Material_UOM_UOM_UOMID",
                        column: x => x.UOMID,
                        principalTable: "UOM",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Plant_Material",
                columns: table => new
                {
                    MaterialID = table.Column<long>(nullable: false),
                    PlantID = table.Column<int>(nullable: false),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant_Material", x => new { x.PlantID, x.MaterialID });
                    table.ForeignKey(
                        name: "FK_Plant_Material_Material_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Material",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Plant_Material_Plant_PlantID",
                        column: x => x.PlantID,
                        principalTable: "Plant",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PRMainItems",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialID = table.Column<long>(nullable: false),
                    PlantID = table.Column<int>(nullable: false),
                    UOMID = table.Column<int>(nullable: false),
                    MaterialGroupID = table.Column<int>(nullable: false),
                    PurchasingGroupID = table.Column<int>(nullable: false),
                    PurchasingGroupDescriptionAR = table.Column<string>(type: "varchar(200)", nullable: true),
                    PurchasingGroupDescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    MaterialDescriptionAR = table.Column<string>(type: "varchar(200)", nullable: true),
                    MaterialDescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    MaterialGroupDescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    MaterialGroupDescriptionAR = table.Column<string>(type: "varchar(200)", nullable: true),
                    PlantDescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    PlantDescriptionAR = table.Column<string>(type: "varchar(200)", nullable: true),
                    UOMAbbreviation = table.Column<string>(type: "varchar(10)", nullable: true),
                    Currency = table.Column<string>(type: "varchar(3)", nullable: true),
                    TypeIndicator = table.Column<string>(type: "varchar(15)", nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    LineItemNumber = table.Column<int>(nullable: false),
                    ValuationPrice = table.Column<float>(nullable: false),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    PRID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRMainItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRMainItems_PRS_PRID",
                        column: x => x.PRID,
                        principalTable: "PRS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RFQMainItems",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialID = table.Column<long>(nullable: false),
                    PlantID = table.Column<int>(nullable: false),
                    UOMID = table.Column<int>(nullable: false),
                    MaterialGroupID = table.Column<int>(nullable: false),
                    PurchasingGroupID = table.Column<int>(nullable: false),
                    PurchasingGroupDescriptionAR = table.Column<string>(type: "varchar(200)", nullable: true),
                    PurchasingGroupDescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    MaterialDescriptionAR = table.Column<string>(type: "varchar(200)", nullable: true),
                    MaterialDescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    MaterialGroupDescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    MaterialGroupDescriptionAR = table.Column<string>(type: "varchar(200)", nullable: true),
                    PlantDescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    PlantDescriptionAR = table.Column<string>(type: "varchar(200)", nullable: true),
                    UOMAbbreviation = table.Column<string>(type: "varchar(10)", nullable: true),
                    Currency = table.Column<string>(type: "varchar(3)", nullable: true),
                    TypeIndicator = table.Column<string>(type: "varchar(15)", nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    LineItemNumber = table.Column<int>(nullable: false),
                    ValuationPrice = table.Column<float>(nullable: false),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    RFQID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFQMainItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RFQMainItems_RFQS_RFQID",
                        column: x => x.RFQID,
                        principalTable: "RFQS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee_Action",
                columns: table => new
                {
                    EmployeeID = table.Column<long>(nullable: false),
                    ActionID = table.Column<int>(nullable: false),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_Action", x => new { x.EmployeeID, x.ActionID });
                    table.ForeignKey(
                        name: "FK_Employee_Action_Actions_ActionID",
                        column: x => x.ActionID,
                        principalTable: "Actions",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Employee_Action_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Employee_PhoneNumbers",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    Number = table.Column<string>(type: "varchar(70)", nullable: true),
                    EmployeeID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_PhoneNumbers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employee_PhoneNumbers_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee_SubGroup",
                columns: table => new
                {
                    EmployeeID = table.Column<long>(nullable: false),
                    SubGroupID = table.Column<int>(nullable: false),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_SubGroup", x => new { x.EmployeeID, x.SubGroupID });
                    table.ForeignKey(
                        name: "FK_Employee_SubGroup_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Employee_SubGroup_EmployeeSubGroup_SubGroupID",
                        column: x => x.SubGroupID,
                        principalTable: "EmployeeSubGroup",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAddresses",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(70)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(70)", nullable: true),
                    Governorate = table.Column<string>(type: "nvarchar(70)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(70)", nullable: true),
                    BuildingNumber = table.Column<string>(type: "nvarchar(70)", nullable: true),
                    OtherInfo = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    EmployeeID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAddresses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeeAddresses_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee_Position",
                columns: table => new
                {
                    EmployeeID = table.Column<long>(nullable: false),
                    PositionID = table.Column<int>(nullable: false),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Percentage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_Position", x => new { x.EmployeeID, x.PositionID });
                    table.ForeignKey(
                        name: "FK_Employee_Position_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Employee_Position_Positions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "Positions",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PositionAssignment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<long>(nullable: false),
                    EmployeeNameAR = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    EmployeeNameEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    PositionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionAssignment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PositionAssignment_Positions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "Positions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee_PurchasingGroup",
                columns: table => new
                {
                    EmployeeID = table.Column<long>(nullable: false),
                    PurchasingGroupID = table.Column<int>(nullable: false),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_PurchasingGroup", x => new { x.EmployeeID, x.PurchasingGroupID });
                    table.ForeignKey(
                        name: "FK_Employee_PurchasingGroup_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Employee_PurchasingGroup_PurchasingGroups_PurchasingGroupID",
                        column: x => x.PurchasingGroupID,
                        principalTable: "PurchasingGroups",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PRMainItem_Condition",
                columns: table => new
                {
                    PRMainItemID = table.Column<long>(nullable: false),
                    ConditionID = table.Column<int>(nullable: false),
                    Per = table.Column<string>(type: "varchar(10)", nullable: true),
                    Currency = table.Column<string>(type: "varchar(3)", nullable: true),
                    TotalValue = table.Column<float>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRMainItem_Condition", x => new { x.ConditionID, x.PRMainItemID });
                    table.ForeignKey(
                        name: "FK_PRMainItem_Condition_Condition_ConditionID",
                        column: x => x.ConditionID,
                        principalTable: "Condition",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PRMainItem_Condition_PRMainItems_PRMainItemID",
                        column: x => x.PRMainItemID,
                        principalTable: "PRMainItems",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PRSubItem",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceID = table.Column<long>(nullable: false),
                    UOMID = table.Column<int>(nullable: false),
                    ServiceDescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    ServiceDescriptionAR = table.Column<string>(type: "varchar(200)", nullable: true),
                    UOMAbbreviation = table.Column<string>(type: "varchar(10)", nullable: true),
                    Currency = table.Column<string>(type: "varchar(3)", nullable: true),
                    LineItemNumber = table.Column<int>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    Quantity = table.Column<float>(nullable: false),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    PRMainItemID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRSubItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRSubItem_PRMainItems_PRMainItemID",
                        column: x => x.PRMainItemID,
                        principalTable: "PRMainItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RFQMainItem_Condition",
                columns: table => new
                {
                    RFQMainItemID = table.Column<long>(nullable: false),
                    ConditionID = table.Column<int>(nullable: false),
                    Per = table.Column<string>(type: "varchar(10)", nullable: true),
                    Currency = table.Column<string>(type: "varchar(3)", nullable: true),
                    TotalValue = table.Column<float>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFQMainItem_Condition", x => new { x.RFQMainItemID, x.ConditionID });
                    table.ForeignKey(
                        name: "FK_RFQMainItem_Condition_Condition_ConditionID",
                        column: x => x.ConditionID,
                        principalTable: "Condition",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RFQMainItem_Condition_RFQMainItems_RFQMainItemID",
                        column: x => x.RFQMainItemID,
                        principalTable: "RFQMainItems",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "RFQSubItems",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceID = table.Column<long>(nullable: false),
                    UOMID = table.Column<int>(nullable: false),
                    ServiceDescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    ServiceDescriptionAR = table.Column<string>(type: "varchar(200)", nullable: true),
                    UOMAbbreviation = table.Column<string>(type: "varchar(10)", nullable: true),
                    Currency = table.Column<string>(type: "varchar(3)", nullable: true),
                    LineItemNumber = table.Column<int>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    Quantity = table.Column<float>(nullable: false),
                    Created_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Creation_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(type: "varchar(255)", nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: false),
                    RFQMainItemID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFQSubItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RFQSubItems_RFQMainItems_RFQMainItemID",
                        column: x => x.RFQMainItemID,
                        principalTable: "RFQMainItems",
                        principalColumn: "ID",
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
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenters_CompanyCodeID",
                table: "CostCenters",
                column: "CompanyCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Action_ActionID",
                table: "Employee_Action",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PhoneNumbers_EmployeeID",
                table: "Employee_PhoneNumbers",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Position_PositionID",
                table: "Employee_Position",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PurchasingGroup_PurchasingGroupID",
                table: "Employee_PurchasingGroup",
                column: "PurchasingGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_SubGroup_SubGroupID",
                table: "Employee_SubGroup",
                column: "SubGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAddresses_EmployeeID",
                table: "EmployeeAddresses",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ApplicationUserID",
                table: "Employees",
                column: "ApplicationUserID",
                unique: true,
                filter: "[ApplicationUserID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CostCenterID",
                table: "Employees",
                column: "CostCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSubGroup_EmployeeGroupID",
                table: "EmployeeSubGroup",
                column: "EmployeeGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_CompanyCodeID",
                table: "ExchangeRates",
                column: "CompanyCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_GLAccounts_MaterialGroupID",
                table: "GLAccounts",
                column: "MaterialGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Material_MaterialGroupID",
                table: "Material",
                column: "MaterialGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Material_MaterialTypeID",
                table: "Material",
                column: "MaterialTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Material_UOM_UOMID",
                table: "Material_UOM",
                column: "UOMID");

            migrationBuilder.CreateIndex(
                name: "IX_OrgUnits_CompanyCodeID",
                table: "OrgUnits",
                column: "CompanyCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Plant_CompanyCodeID",
                table: "Plant",
                column: "CompanyCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Plant_Material_MaterialID",
                table: "Plant_Material",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_PositionAssignment_PositionID",
                table: "PositionAssignment",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_OrgUnitID",
                table: "Positions",
                column: "OrgUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_PRMainItem_Condition_PRMainItemID",
                table: "PRMainItem_Condition",
                column: "PRMainItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PRMainItems_PRID",
                table: "PRMainItems",
                column: "PRID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfitCenters_CompanyCodeID",
                table: "ProfitCenters",
                column: "CompanyCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_PRS_PRTypeID",
                table: "PRS",
                column: "PRTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PRSubItem_PRMainItemID",
                table: "PRSubItem",
                column: "PRMainItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasingGroups_PlantID",
                table: "PurchasingGroups",
                column: "PlantID");

            migrationBuilder.CreateIndex(
                name: "IX_RFQMainItem_Condition_ConditionID",
                table: "RFQMainItem_Condition",
                column: "ConditionID");

            migrationBuilder.CreateIndex(
                name: "IX_RFQMainItems_RFQID",
                table: "RFQMainItems",
                column: "RFQID");

            migrationBuilder.CreateIndex(
                name: "IX_RFQS_RFQTypeID",
                table: "RFQS",
                column: "RFQTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_RFQS_VendorID",
                table: "RFQS",
                column: "VendorID");

            migrationBuilder.CreateIndex(
                name: "IX_RFQSubItems_RFQMainItemID",
                table: "RFQSubItems",
                column: "RFQMainItemID");

            migrationBuilder.CreateIndex(
                name: "IX_StorageLocation_PlantID",
                table: "StorageLocation",
                column: "PlantID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_Bank_BankID",
                table: "Vendor_Bank",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CompanyCode_CompanyCodeID",
                table: "Vendor_CompanyCode",
                column: "CompanyCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_Tax_TaxID",
                table: "Vendor_Tax",
                column: "TaxID");

            migrationBuilder.CreateIndex(
                name: "IX_VendorAddresses_VendorID",
                table: "VendorAddresses",
                column: "VendorID");

            migrationBuilder.CreateIndex(
                name: "IX_VendorEmails_VendorID",
                table: "VendorEmails",
                column: "VendorID");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPhoneNumbers_VendorID",
                table: "VendorPhoneNumbers",
                column: "VendorID");
        }

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
                name: "Employee_Action");

            migrationBuilder.DropTable(
                name: "Employee_PhoneNumbers");

            migrationBuilder.DropTable(
                name: "Employee_Position");

            migrationBuilder.DropTable(
                name: "Employee_PurchasingGroup");

            migrationBuilder.DropTable(
                name: "Employee_SubGroup");

            migrationBuilder.DropTable(
                name: "EmployeeAddresses");

            migrationBuilder.DropTable(
                name: "ExchangeRates");

            migrationBuilder.DropTable(
                name: "GLAccounts");

            migrationBuilder.DropTable(
                name: "Material_UOM");

            migrationBuilder.DropTable(
                name: "Plant_Material");

            migrationBuilder.DropTable(
                name: "PositionAssignment");

            migrationBuilder.DropTable(
                name: "PRMainItem_Condition");

            migrationBuilder.DropTable(
                name: "ProfitCenters");

            migrationBuilder.DropTable(
                name: "PRSubItem");

            migrationBuilder.DropTable(
                name: "RFQMainItem_Condition");

            migrationBuilder.DropTable(
                name: "RFQSubItems");

            migrationBuilder.DropTable(
                name: "StorageLocation");

            migrationBuilder.DropTable(
                name: "Vendor_Bank");

            migrationBuilder.DropTable(
                name: "Vendor_CompanyCode");

            migrationBuilder.DropTable(
                name: "Vendor_Tax");

            migrationBuilder.DropTable(
                name: "VendorAddresses");

            migrationBuilder.DropTable(
                name: "VendorEmails");

            migrationBuilder.DropTable(
                name: "VendorPhoneNumbers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "PurchasingGroups");

            migrationBuilder.DropTable(
                name: "EmployeeSubGroup");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "UOM");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "PRMainItems");

            migrationBuilder.DropTable(
                name: "Condition");

            migrationBuilder.DropTable(
                name: "RFQMainItems");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Taxes");

            migrationBuilder.DropTable(
                name: "Plant");

            migrationBuilder.DropTable(
                name: "EmployeeGroup");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CostCenters");

            migrationBuilder.DropTable(
                name: "MaterialGroup");

            migrationBuilder.DropTable(
                name: "MaterialType");

            migrationBuilder.DropTable(
                name: "OrgUnits");

            migrationBuilder.DropTable(
                name: "PRS");

            migrationBuilder.DropTable(
                name: "RFQS");

            migrationBuilder.DropTable(
                name: "CompanyCodes");

            migrationBuilder.DropTable(
                name: "PRTypes");

            migrationBuilder.DropTable(
                name: "RFQTypes");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
