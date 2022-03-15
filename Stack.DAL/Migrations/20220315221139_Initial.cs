using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityTypes",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAR = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTypes", x => x.ID);
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
                    FirstName = table.Column<string>(type: "varchar(70)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(70)", nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterestAttributes",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestAttributes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LeadStatuses",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EN = table.Column<string>(nullable: true),
                    AR = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    LeadID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LOneInterestInputs",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOneInterestInputs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LOneInterests",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionEN = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOneInterests", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LThreeInterestInputs",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LThreeInterestInputs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LThreeInterests",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionEN = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true),
                    isStandalone = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LThreeInterests", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LTwoInterestInputs",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LTwoInterestInputs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityStatuses",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EN = table.Column<string>(nullable: true),
                    AR = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    OpportunityID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pools",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEN = table.Column<string>(nullable: true),
                    NameAR = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true),
                    DescriptionEN = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pools", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProspectStatuses",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EN = table.Column<string>(nullable: true),
                    AR = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    ProspectID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProspectStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    DescriptionEN = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAR = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    RoutesTo = table.Column<long>(nullable: false),
                    ActivityTypeID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sections_ActivityTypes_ActivityTypeID",
                        column: x => x.ActivityTypeID,
                        principalTable: "ActivityTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedUserID = table.Column<string>(nullable: true),
                    FirstNameEN = table.Column<string>(nullable: true),
                    LastNameEN = table.Column<string>(nullable: true),
                    FirstNameAR = table.Column<string>(nullable: true),
                    LastNameAR = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_AssignedUserID",
                        column: x => x.AssignedUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LOneInterest_InterestAttributes",
                columns: table => new
                {
                    LOneInterestID = table.Column<long>(nullable: false),
                    InterestAttributeID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOneInterest_InterestAttributes", x => new { x.LOneInterestID, x.InterestAttributeID });
                    table.ForeignKey(
                        name: "FK_LOneInterest_InterestAttributes_InterestAttributes_LOneInterestID",
                        column: x => x.LOneInterestID,
                        principalTable: "InterestAttributes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LOneInterest_InterestAttributes_LOneInterests_LOneInterestID",
                        column: x => x.LOneInterestID,
                        principalTable: "LOneInterests",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LOneInterest_LOneInterestInput",
                columns: table => new
                {
                    LOneInterestID = table.Column<long>(nullable: false),
                    LOneInterestInputID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOneInterest_LOneInterestInput", x => new { x.LOneInterestID, x.LOneInterestInputID });
                    table.ForeignKey(
                        name: "FK_LOneInterest_LOneInterestInput_LOneInterests_LOneInterestID",
                        column: x => x.LOneInterestID,
                        principalTable: "LOneInterests",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LOneInterest_LOneInterestInput_LOneInterestInputs_LOneInterestInputID",
                        column: x => x.LOneInterestInputID,
                        principalTable: "LOneInterestInputs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LTwoInterests",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionEN = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LOneInterestID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LTwoInterests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LTwoInterests_LOneInterests_LOneInterestID",
                        column: x => x.LOneInterestID,
                        principalTable: "LOneInterests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LThreeInterest_InterestAttributes",
                columns: table => new
                {
                    LThreeInterestID = table.Column<long>(nullable: false),
                    InterestAttributeID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LThreeInterest_InterestAttributes", x => new { x.LThreeInterestID, x.InterestAttributeID });
                    table.ForeignKey(
                        name: "FK_LThreeInterest_InterestAttributes_InterestAttributes_LThreeInterestID",
                        column: x => x.LThreeInterestID,
                        principalTable: "InterestAttributes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LThreeInterest_InterestAttributes_LThreeInterests_LThreeInterestID",
                        column: x => x.LThreeInterestID,
                        principalTable: "LThreeInterests",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LThreeInterest_LThreeInterestInputs",
                columns: table => new
                {
                    LThreeInterestID = table.Column<long>(nullable: false),
                    LThreeInterestInputID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LThreeInterest_LThreeInterestInputs", x => new { x.LThreeInterestID, x.LThreeInterestInputID });
                    table.ForeignKey(
                        name: "FK_LThreeInterest_LThreeInterestInputs_LThreeInterests_LThreeInterestID",
                        column: x => x.LThreeInterestID,
                        principalTable: "LThreeInterests",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LThreeInterest_LThreeInterestInputs_LThreeInterestInputs_LThreeInterestInputID",
                        column: x => x.LThreeInterestInputID,
                        principalTable: "LThreeInterestInputs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Pool_Admins",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    PoolID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pool_Admins", x => new { x.PoolID, x.UserID });
                    table.ForeignKey(
                        name: "FK_Pool_Admins_Pools_PoolID",
                        column: x => x.PoolID,
                        principalTable: "Pools",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Pool_Admins_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pool_Users",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    PoolID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pool_Users", x => new { x.PoolID, x.UserID });
                    table.ForeignKey(
                        name: "FK_Pool_Users_Pools_PoolID",
                        column: x => x.PoolID,
                        principalTable: "Pools",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Pool_Users_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    DescriptionEN = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RegionID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Areas_Regions_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Regions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SectionQuestions",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(nullable: true),
                    DescriptionEN = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    isRequired = table.Column<bool>(nullable: false),
                    SectionID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionQuestions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SectionQuestions_Sections_SectionID",
                        column: x => x.SectionID,
                        principalTable: "Sections",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoolID = table.Column<long>(nullable: false),
                    AssignedUserID = table.Column<string>(nullable: true),
                    FirstNameEN = table.Column<string>(nullable: true),
                    LastNameEN = table.Column<string>(nullable: true),
                    FirstNameAR = table.Column<string>(nullable: true),
                    LastNameAR = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CustomerID = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contacts_AspNetUsers_AssignedUserID",
                        column: x => x.AssignedUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contacts_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contacts_Pools_PoolID",
                        column: x => x.PoolID,
                        principalTable: "Pools",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPhoneNumber",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(nullable: true),
                    CustomerID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPhoneNumber", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerPhoneNumber_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deals",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Deals_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LTwoInterest_InterestAttributes",
                columns: table => new
                {
                    LTwoInterestID = table.Column<long>(nullable: false),
                    InterestAttributeID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LTwoInterest_InterestAttributes", x => new { x.LTwoInterestID, x.InterestAttributeID });
                    table.ForeignKey(
                        name: "FK_LTwoInterest_InterestAttributes_InterestAttributes_LTwoInterestID",
                        column: x => x.LTwoInterestID,
                        principalTable: "InterestAttributes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LTwoInterest_InterestAttributes_LTwoInterests_LTwoInterestID",
                        column: x => x.LTwoInterestID,
                        principalTable: "LTwoInterests",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LTwoInterest_LTwoInterestInputs",
                columns: table => new
                {
                    LTwoInterestID = table.Column<long>(nullable: false),
                    LTwoInterestInputID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LTwoInterest_LTwoInterestInputs", x => new { x.LTwoInterestID, x.LTwoInterestInputID });
                    table.ForeignKey(
                        name: "FK_LTwoInterest_LTwoInterestInputs_LTwoInterests_LTwoInterestID",
                        column: x => x.LTwoInterestID,
                        principalTable: "LTwoInterests",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LTwoInterest_LTwoInterestInputs_LTwoInterestInputs_LTwoInterestInputID",
                        column: x => x.LTwoInterestInputID,
                        principalTable: "LTwoInterestInputs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Area_LOneInterests",
                columns: table => new
                {
                    AreaID = table.Column<long>(nullable: false),
                    LOneInterestID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area_LOneInterests", x => new { x.AreaID, x.LOneInterestID });
                    table.ForeignKey(
                        name: "FK_Area_LOneInterests_Areas_AreaID",
                        column: x => x.AreaID,
                        principalTable: "Areas",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Area_LOneInterests_LOneInterests_LOneInterestID",
                        column: x => x.LOneInterestID,
                        principalTable: "LOneInterests",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Area_Pools",
                columns: table => new
                {
                    AreaID = table.Column<long>(nullable: false),
                    PoolID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area_Pools", x => new { x.AreaID, x.PoolID });
                    table.ForeignKey(
                        name: "FK_Area_Pools_Areas_AreaID",
                        column: x => x.AreaID,
                        principalTable: "Areas",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Area_Pools_Pools_PoolID",
                        column: x => x.PoolID,
                        principalTable: "Pools",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SectionQuestionOptions",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValueAR = table.Column<string>(nullable: true),
                    ValueEN = table.Column<string>(nullable: true),
                    RoutesTo = table.Column<long>(nullable: true),
                    QuestionID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionQuestionOptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SectionQuestionOptions_SectionQuestions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "SectionQuestions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactPhoneNumber",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(nullable: true),
                    ContactID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPhoneNumber", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContactPhoneNumber_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedUserID = table.Column<string>(nullable: true),
                    DealID = table.Column<long>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    StatusID = table.Column<long>(nullable: false),
                    isJunked = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Leads_AspNetUsers_AssignedUserID",
                        column: x => x.AssignedUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leads_Deals_DealID",
                        column: x => x.DealID,
                        principalTable: "Deals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leads_LeadStatuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "LeadStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Opportunities",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedUserID = table.Column<string>(nullable: true),
                    DealID = table.Column<long>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    StatusID = table.Column<long>(nullable: false),
                    isJunked = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Opportunities_AspNetUsers_AssignedUserID",
                        column: x => x.AssignedUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunities_Deals_DealID",
                        column: x => x.DealID,
                        principalTable: "Deals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Opportunities_OpportunityStatuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "OpportunityStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessFlows",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<long>(nullable: false),
                    DealID = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessFlows", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProcessFlows_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessFlows_Deals_DealID",
                        column: x => x.DealID,
                        principalTable: "Deals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prospects",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedUserID = table.Column<string>(nullable: true),
                    DealID = table.Column<long>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    StatusID = table.Column<long>(nullable: false),
                    isJunked = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prospects", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Prospects_AspNetUsers_AssignedUserID",
                        column: x => x.AssignedUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prospects_Deals_DealID",
                        column: x => x.DealID,
                        principalTable: "Deals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prospects_ProspectStatuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "ProspectStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessFlowID = table.Column<long>(nullable: false),
                    ActivityTypeID = table.Column<long>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Activities_ActivityTypes_ActivityTypeID",
                        column: x => x.ActivityTypeID,
                        principalTable: "ActivityTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activities_ProcessFlows_ProcessFlowID",
                        column: x => x.ProcessFlowID,
                        principalTable: "ProcessFlows",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivitySections",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityID = table.Column<long>(nullable: false),
                    SectionID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivitySections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActivitySections_Activities_ActivityID",
                        column: x => x.ActivityID,
                        principalTable: "Activities",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ActivitySections_Sections_SectionID",
                        column: x => x.SectionID,
                        principalTable: "Sections",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SectionQuestionAnswers",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionID = table.Column<long>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    ActivitySectionID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionQuestionAnswers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SectionQuestionAnswers_ActivitySections_ActivitySectionID",
                        column: x => x.ActivitySectionID,
                        principalTable: "ActivitySections",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SectionQuestionAnswers_SectionQuestions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "SectionQuestions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ActivityTypeID",
                table: "Activities",
                column: "ActivityTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ProcessFlowID",
                table: "Activities",
                column: "ProcessFlowID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitySections_ActivityID",
                table: "ActivitySections",
                column: "ActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitySections_SectionID",
                table: "ActivitySections",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Area_LOneInterests_LOneInterestID",
                table: "Area_LOneInterests",
                column: "LOneInterestID");

            migrationBuilder.CreateIndex(
                name: "IX_Area_Pools_PoolID",
                table: "Area_Pools",
                column: "PoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_RegionID",
                table: "Areas",
                column: "RegionID");

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
                name: "IX_ContactPhoneNumber_ContactID",
                table: "ContactPhoneNumber",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_AssignedUserID",
                table: "Contacts",
                column: "AssignedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CustomerID",
                table: "Contacts",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PoolID",
                table: "Contacts",
                column: "PoolID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPhoneNumber_CustomerID",
                table: "CustomerPhoneNumber",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AssignedUserID",
                table: "Customers",
                column: "AssignedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_CustomerID",
                table: "Deals",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_AssignedUserID",
                table: "Leads",
                column: "AssignedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_DealID",
                table: "Leads",
                column: "DealID");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_StatusID",
                table: "Leads",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_LOneInterest_LOneInterestInput_LOneInterestInputID",
                table: "LOneInterest_LOneInterestInput",
                column: "LOneInterestInputID");

            migrationBuilder.CreateIndex(
                name: "IX_LThreeInterest_LThreeInterestInputs_LThreeInterestInputID",
                table: "LThreeInterest_LThreeInterestInputs",
                column: "LThreeInterestInputID");

            migrationBuilder.CreateIndex(
                name: "IX_LTwoInterest_LTwoInterestInputs_LTwoInterestInputID",
                table: "LTwoInterest_LTwoInterestInputs",
                column: "LTwoInterestInputID");

            migrationBuilder.CreateIndex(
                name: "IX_LTwoInterests_LOneInterestID",
                table: "LTwoInterests",
                column: "LOneInterestID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_AssignedUserID",
                table: "Opportunities",
                column: "AssignedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_DealID",
                table: "Opportunities",
                column: "DealID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_StatusID",
                table: "Opportunities",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Pool_Admins_UserID",
                table: "Pool_Admins",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Pool_Users_UserID",
                table: "Pool_Users",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessFlows_CustomerID",
                table: "ProcessFlows",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessFlows_DealID",
                table: "ProcessFlows",
                column: "DealID",
                unique: true,
                filter: "[DealID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_AssignedUserID",
                table: "Prospects",
                column: "AssignedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_DealID",
                table: "Prospects",
                column: "DealID");

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_StatusID",
                table: "Prospects",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_SectionQuestionAnswers_ActivitySectionID",
                table: "SectionQuestionAnswers",
                column: "ActivitySectionID");

            migrationBuilder.CreateIndex(
                name: "IX_SectionQuestionAnswers_QuestionID",
                table: "SectionQuestionAnswers",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_SectionQuestionOptions_QuestionID",
                table: "SectionQuestionOptions",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_SectionQuestions_SectionID",
                table: "SectionQuestions",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ActivityTypeID",
                table: "Sections",
                column: "ActivityTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Area_LOneInterests");

            migrationBuilder.DropTable(
                name: "Area_Pools");

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
                name: "ContactPhoneNumber");

            migrationBuilder.DropTable(
                name: "CustomerPhoneNumber");

            migrationBuilder.DropTable(
                name: "Leads");

            migrationBuilder.DropTable(
                name: "LOneInterest_InterestAttributes");

            migrationBuilder.DropTable(
                name: "LOneInterest_LOneInterestInput");

            migrationBuilder.DropTable(
                name: "LThreeInterest_InterestAttributes");

            migrationBuilder.DropTable(
                name: "LThreeInterest_LThreeInterestInputs");

            migrationBuilder.DropTable(
                name: "LTwoInterest_InterestAttributes");

            migrationBuilder.DropTable(
                name: "LTwoInterest_LTwoInterestInputs");

            migrationBuilder.DropTable(
                name: "Opportunities");

            migrationBuilder.DropTable(
                name: "Pool_Admins");

            migrationBuilder.DropTable(
                name: "Pool_Users");

            migrationBuilder.DropTable(
                name: "Prospects");

            migrationBuilder.DropTable(
                name: "SectionQuestionAnswers");

            migrationBuilder.DropTable(
                name: "SectionQuestionOptions");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "LeadStatuses");

            migrationBuilder.DropTable(
                name: "LOneInterestInputs");

            migrationBuilder.DropTable(
                name: "LThreeInterests");

            migrationBuilder.DropTable(
                name: "LThreeInterestInputs");

            migrationBuilder.DropTable(
                name: "InterestAttributes");

            migrationBuilder.DropTable(
                name: "LTwoInterests");

            migrationBuilder.DropTable(
                name: "LTwoInterestInputs");

            migrationBuilder.DropTable(
                name: "OpportunityStatuses");

            migrationBuilder.DropTable(
                name: "ProspectStatuses");

            migrationBuilder.DropTable(
                name: "ActivitySections");

            migrationBuilder.DropTable(
                name: "SectionQuestions");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Pools");

            migrationBuilder.DropTable(
                name: "LOneInterests");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "ProcessFlows");

            migrationBuilder.DropTable(
                name: "ActivityTypes");

            migrationBuilder.DropTable(
                name: "Deals");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
