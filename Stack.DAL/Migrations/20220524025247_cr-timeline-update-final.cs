using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class crtimelineupdatefinal : Migration
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
                    Status = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ColorCode = table.Column<string>(nullable: true),
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
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    SystemAuthorizations = table.Column<string>(nullable: true),
                    NameAR = table.Column<string>(nullable: true),
                    DescriptionEN = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true),
                    HasParent = table.Column<bool>(nullable: false),
                    ParentRoleID = table.Column<string>(nullable: true)
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
                    Status = table.Column<int>(nullable: false),
                    SystemAuthorizations = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorizationSections",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAR = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizationSections", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEN = table.Column<string>(nullable: true),
                    TitleAR = table.Column<string>(nullable: true),
                    DescriptionEN = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContactStatuses",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EN = table.Column<string>(nullable: true),
                    AR = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CRPhases",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAR = table.Column<string>(nullable: true),
                    TitleEN = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRPhases", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CRTimelines",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAR = table.Column<string>(nullable: true),
                    TitleEN = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRTimelines", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LAttributes",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LabelAR = table.Column<string>(nullable: true),
                    LabelEN = table.Column<string>(nullable: true),
                    IsPredefined = table.Column<bool>(nullable: false),
                    ParentAttributeID = table.Column<long>(nullable: true),
                    ParentInputID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LAttributes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LAttributes_LAttributes_ParentAttributeID",
                        column: x => x.ParentAttributeID,
                        principalTable: "LAttributes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LabelAR = table.Column<string>(nullable: true),
                    LabelEN = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NameEN = table.Column<string>(nullable: true),
                    NameAR = table.Column<string>(nullable: true),
                    DescriptionEN = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true),
                    LocationType = table.Column<int>(nullable: false),
                    Latitude = table.Column<float>(nullable: false),
                    Longitude = table.Column<float>(nullable: false),
                    ParentLocationID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Locations_Locations_ParentLocationID",
                        column: x => x.ParentLocationID,
                        principalTable: "Locations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                    ConfigurationType = table.Column<int>(nullable: false),
                    Capacity = table.Column<int>(nullable: true),
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
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProspectStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SystemConfiguration",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LockDuration = table.Column<double>(nullable: false),
                    AutomaticUnassignmentDuration = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfiguration", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAR = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    HasDecisionalQuestions = table.Column<bool>(nullable: false),
                    HasCreateInterest = table.Column<bool>(nullable: false),
                    HasCreateRequest = table.Column<bool>(nullable: false),
                    HasCreateResale = table.Column<bool>(nullable: false),
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
                    FullNameEN = table.Column<string>(nullable: true),
                    FullNameAR = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    LeadSourceType = table.Column<string>(nullable: true),
                    LeadSourceName = table.Column<string>(nullable: true),
                    PrimaryPhoneNumber = table.Column<string>(nullable: true),
                    PoolID = table.Column<long>(nullable: false),
                    ChannelID = table.Column<int>(nullable: true),
                    LSTID = table.Column<int>(nullable: true),
                    LSNID = table.Column<int>(nullable: true),
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
                name: "PoolConnectionIDs",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 256, nullable: false),
                    PoolID = table.Column<long>(nullable: false),
                    RecordID = table.Column<long>(nullable: false),
                    RecordType = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoolConnectionIDs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PoolConnectionIDs_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SectionAuthorizations",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAR = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    AuthorizationSectionID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionAuthorizations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SectionAuthorizations_AuthorizationSections_AuthorizationSectionID",
                        column: x => x.AuthorizationSectionID,
                        principalTable: "AuthorizationSections",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeadSourceTypes",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEN = table.Column<string>(nullable: true),
                    TitleAR = table.Column<string>(nullable: true),
                    DescriptionEN = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    ChannelID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadSourceTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LeadSourceTypes_Channels_ChannelID",
                        column: x => x.ChannelID,
                        principalTable: "Channels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CRPhaseInputs",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhaseID = table.Column<long>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    TitleAR = table.Column<string>(nullable: true),
                    TitleEN = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRPhaseInputs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CRPhaseInputs_CRPhases_PhaseID",
                        column: x => x.PhaseID,
                        principalTable: "CRPhases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CRTimeline_Phases",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhaseID = table.Column<long>(nullable: false),
                    ParentPhaseID = table.Column<long>(nullable: true),
                    TimelineID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRTimeline_Phases", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CRTimeline_Phases_CRPhases_PhaseID",
                        column: x => x.PhaseID,
                        principalTable: "CRPhases",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CRTimeline_Phases_CRTimelines_TimelineID",
                        column: x => x.TimelineID,
                        principalTable: "CRTimelines",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CustomerRequestTypes",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEN = table.Column<string>(nullable: true),
                    NameAR = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    TimelineID = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    InputsTemplate = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRequestTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerRequestTypes_CRTimelines_TimelineID",
                        column: x => x.TimelineID,
                        principalTable: "CRTimelines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LSections",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LabelEN = table.Column<string>(nullable: true),
                    LabelAR = table.Column<string>(nullable: true),
                    LevelID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LSections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LSections_Levels_LevelID",
                        column: x => x.LevelID,
                        principalTable: "Levels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location_Pools",
                columns: table => new
                {
                    LocationID = table.Column<long>(nullable: false),
                    PoolID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location_Pools", x => new { x.LocationID, x.PoolID });
                    table.ForeignKey(
                        name: "FK_Location_Pools_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Location_Pools_Pools_PoolID",
                        column: x => x.PoolID,
                        principalTable: "Pools",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Pool_Users",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    PoolID = table.Column<long>(nullable: false),
                    Capacity = table.Column<int>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    JoinDate = table.Column<DateTime>(nullable: false)
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
                name: "PoolRequests",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoolID = table.Column<long>(nullable: false),
                    Requestee_PoolID = table.Column<long>(nullable: true),
                    RecordID = table.Column<long>(nullable: true),
                    RecordType = table.Column<int>(nullable: true),
                    RecordStatusID = table.Column<long>(nullable: true),
                    RequesteeID = table.Column<string>(nullable: true),
                    RequestType = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    AppliedActionDate = table.Column<DateTime>(nullable: false),
                    RequestDate = table.Column<DateTime>(nullable: false),
                    DescriptionEN = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoolRequests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PoolRequests_Pools_PoolID",
                        column: x => x.PoolID,
                        principalTable: "Pools",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                    Order = table.Column<int>(nullable: false),
                    IsDecisional = table.Column<bool>(nullable: false),
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
                    FullNameEN = table.Column<string>(nullable: true),
                    FullNameAR = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    PrimaryPhoneNumber = table.Column<string>(nullable: true),
                    PoolID = table.Column<long>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    ChannelID = table.Column<int>(nullable: true),
                    LSTID = table.Column<int>(nullable: true),
                    LSNID = table.Column<int>(nullable: true),
                    AssignedUserID = table.Column<string>(nullable: true),
                    StatusID = table.Column<long>(nullable: true),
                    IsFinalized = table.Column<bool>(nullable: false),
                    IsFresh = table.Column<bool>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false),
                    ForceUnlock_JobID = table.Column<string>(nullable: true),
                    CapacityCalculated = table.Column<bool>(nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Contacts_ContactStatuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "ContactStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer_Tags",
                columns: table => new
                {
                    CustomerID = table.Column<long>(nullable: false),
                    TagID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer_Tags", x => new { x.CustomerID, x.TagID });
                    table.ForeignKey(
                        name: "FK_Customer_Tags_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Customer_Tags_Tags_TagID",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CustomerComments",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(nullable: true),
                    CustomerID = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerComments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerComments_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
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
                    CustomerID = table.Column<long>(nullable: false),
                    ActiveStageID = table.Column<long>(nullable: false),
                    ActiveStageType = table.Column<int>(nullable: false),
                    PoolID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
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
                name: "LInterests",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DescriptionEN = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true),
                    IsSeparate = table.Column<bool>(nullable: false),
                    LevelID = table.Column<long>(nullable: false),
                    OwnerID = table.Column<long>(nullable: true),
                    LocationID = table.Column<long>(nullable: true),
                    ParentLInterestID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LInterests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LInterests_Levels_LevelID",
                        column: x => x.LevelID,
                        principalTable: "Levels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LInterests_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LInterests_Customers_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeadSourceNames",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEN = table.Column<string>(nullable: true),
                    TitleAR = table.Column<string>(nullable: true),
                    DescriptionEN = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    LeadSourceTypeID = table.Column<long>(nullable: false),
                    ChannelID = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadSourceNames", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LeadSourceNames_LeadSourceTypes_ChannelID",
                        column: x => x.ChannelID,
                        principalTable: "LeadSourceTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeadSourceNames_LeadSourceTypes_LeadSourceTypeID",
                        column: x => x.LeadSourceTypeID,
                        principalTable: "LeadSourceTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CRPhaseInputOptions",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InputID = table.Column<long>(nullable: false),
                    TitleEN = table.Column<string>(nullable: true),
                    TitleAR = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRPhaseInputOptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CRPhaseInputOptions_CRPhaseInputs_InputID",
                        column: x => x.InputID,
                        principalTable: "CRPhaseInputs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CR_Timeline_Phases",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<long>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    TimelinePhaseID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Timeline_Phases", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CR_Timeline_Phases_CRTimeline_Phases_TimelinePhaseID",
                        column: x => x.TimelinePhaseID,
                        principalTable: "CRTimeline_Phases",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Inputs",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LabelAR = table.Column<string>(nullable: true),
                    LabelEN = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    PredefinedInputType = table.Column<int>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    IsRequired = table.Column<bool>(nullable: false),
                    SectionID = table.Column<long>(nullable: false),
                    AttributeID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inputs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inputs_LAttributes_AttributeID",
                        column: x => x.AttributeID,
                        principalTable: "LAttributes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inputs_LSections_SectionID",
                        column: x => x.SectionID,
                        principalTable: "LSections",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SectionQuestionOptions",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValueAR = table.Column<string>(nullable: true),
                    ValueEN = table.Column<string>(nullable: true),
                    RoutesTo = table.Column<string>(nullable: true),
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
                name: "Contact_Favorites",
                columns: table => new
                {
                    ContactID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact_Favorites", x => new { x.ContactID, x.UserID });
                    table.ForeignKey(
                        name: "FK_Contact_Favorites_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Contact_Favorites_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contact_Tags",
                columns: table => new
                {
                    ContactID = table.Column<long>(nullable: false),
                    TagID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact_Tags", x => new { x.ContactID, x.TagID });
                    table.ForeignKey(
                        name: "FK_Contact_Tags_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Contact_Tags_Tags_TagID",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ContactComments",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(nullable: true),
                    ContactID = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactComments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContactComments_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactPhoneNumbers",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(nullable: true),
                    ContactID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPhoneNumbers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContactPhoneNumbers_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerRequests",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    DealID = table.Column<long>(nullable: true),
                    ContactID = table.Column<long>(nullable: true),
                    TimelineID = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    RequestTypeID = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRequests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerRequests_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerRequests_Deals_DealID",
                        column: x => x.DealID,
                        principalTable: "Deals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerRequests_CustomerRequestTypes_RequestTypeID",
                        column: x => x.RequestTypeID,
                        principalTable: "CustomerRequestTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoneDeals",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedUserID = table.Column<string>(nullable: true),
                    DealID = table.Column<long>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoneDeals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DoneDeals_AspNetUsers_AssignedUserID",
                        column: x => x.AssignedUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoneDeals_Deals_DealID",
                        column: x => x.DealID,
                        principalTable: "Deals",
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
                    State = table.Column<int>(nullable: false),
                    StatusID = table.Column<long>(nullable: true),
                    IsFresh = table.Column<bool>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false),
                    ForceUnlock_JobID = table.Column<string>(nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Opportunities",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedUserID = table.Column<string>(nullable: true),
                    DealID = table.Column<long>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    StatusID = table.Column<long>(nullable: true),
                    IsFresh = table.Column<bool>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false),
                    ForceUnlock_JobID = table.Column<string>(nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcessFlows",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    IsComplete = table.Column<bool>(nullable: false),
                    CustomerID = table.Column<long>(nullable: true),
                    DealID = table.Column<long>(nullable: true),
                    ContactID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessFlows", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProcessFlows_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessFlows_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                    State = table.Column<int>(nullable: false),
                    StatusID = table.Column<long>(nullable: true),
                    PoolID = table.Column<long>(nullable: false),
                    IsFresh = table.Column<bool>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false),
                    ForceUnlock_JobID = table.Column<string>(nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LInterestInputs",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Attachment = table.Column<string>(nullable: true),
                    SelectedAttributeID = table.Column<long>(nullable: true),
                    InputID = table.Column<long>(nullable: false),
                    LInterestID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LInterestInputs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LInterestInputs_LInterests_LInterestID",
                        column: x => x.LInterestID,
                        principalTable: "LInterests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CR_Timelines",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimelineID = table.Column<long>(nullable: false),
                    RequestID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Timelines", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CR_Timelines_CustomerRequests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "CustomerRequests",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CR_Timelines_CRTimelines_TimelineID",
                        column: x => x.TimelineID,
                        principalTable: "CRTimelines",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "DoneDeal_Favorites",
                columns: table => new
                {
                    RecordID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoneDeal_Favorites", x => new { x.RecordID, x.UserID });
                    table.ForeignKey(
                        name: "FK_DoneDeal_Favorites_DoneDeals_RecordID",
                        column: x => x.RecordID,
                        principalTable: "DoneDeals",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_DoneDeal_Favorites_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lead_Favorites",
                columns: table => new
                {
                    RecordID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lead_Favorites", x => new { x.RecordID, x.UserID });
                    table.ForeignKey(
                        name: "FK_Lead_Favorites_Leads_RecordID",
                        column: x => x.RecordID,
                        principalTable: "Leads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Lead_Favorites_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Opportunity_Favorites",
                columns: table => new
                {
                    RecordID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunity_Favorites", x => new { x.RecordID, x.UserID });
                    table.ForeignKey(
                        name: "FK_Opportunity_Favorites_Opportunities_RecordID",
                        column: x => x.RecordID,
                        principalTable: "Opportunities",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Opportunity_Favorites_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
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
                    SubmissionDate = table.Column<DateTime>(nullable: true),
                    IsSubmitted = table.Column<bool>(nullable: false),
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
                        name: "FK_Activities_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activities_ProcessFlows_ProcessFlowID",
                        column: x => x.ProcessFlowID,
                        principalTable: "ProcessFlows",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prospect_Favorites",
                columns: table => new
                {
                    RecordID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prospect_Favorites", x => new { x.RecordID, x.UserID });
                    table.ForeignKey(
                        name: "FK_Prospect_Favorites_Prospects_RecordID",
                        column: x => x.RecordID,
                        principalTable: "Prospects",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Prospect_Favorites_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ActivitySections",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsSubmitted = table.Column<bool>(nullable: false),
                    ActivityID = table.Column<long>(nullable: true),
                    SectionID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivitySections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActivitySections_Activities_ActivityID",
                        column: x => x.ActivityID,
                        principalTable: "Activities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ActivitySections_Sections_SectionID",
                        column: x => x.SectionID,
                        principalTable: "Sections",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SubmissionDetails",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentStage = table.Column<string>(nullable: true),
                    NewStage = table.Column<string>(nullable: true),
                    CurrentStatus = table.Column<long>(nullable: true),
                    NewStatus = table.Column<long>(nullable: true),
                    IsStatusChanged = table.Column<bool>(nullable: false),
                    IsStageChanged = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    SubmissionDate = table.Column<DateTime>(nullable: true),
                    ScheduledActivityDate = table.Column<DateTime>(nullable: true),
                    ScheduledActivityID = table.Column<long>(nullable: true),
                    ActivityID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubmissionDetails_Activities_ActivityID",
                        column: x => x.ActivityID,
                        principalTable: "Activities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubmissionDetails_ActivityTypes_ScheduledActivityID",
                        column: x => x.ScheduledActivityID,
                        principalTable: "ActivityTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SectionQuestionAnswers",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: true),
                    DateValue = table.Column<DateTime>(nullable: false),
                    ActivitySectionID = table.Column<long>(nullable: true),
                    QuestionID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionQuestionAnswers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SectionQuestionAnswers_ActivitySections_ActivitySectionID",
                        column: x => x.ActivitySectionID,
                        principalTable: "ActivitySections",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SectionQuestionAnswers_SectionQuestions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "SectionQuestions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SelectedOptions",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionQuestionOptionID = table.Column<long>(nullable: true),
                    SectionQuestionAnswerID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedOptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SelectedOptions_SectionQuestionAnswers_SectionQuestionAnswerID",
                        column: x => x.SectionQuestionAnswerID,
                        principalTable: "SectionQuestionAnswers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SelectedOptions_SectionQuestionOptions_SectionQuestionOptionID",
                        column: x => x.SectionQuestionOptionID,
                        principalTable: "SectionQuestionOptions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ActivityTypeID",
                table: "Activities",
                column: "ActivityTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CreatedBy",
                table: "Activities",
                column: "CreatedBy");

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
                name: "IX_Contact_Favorites_UserID",
                table: "Contact_Favorites",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Tags_TagID",
                table: "Contact_Tags",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactComments_ContactID",
                table: "ContactComments",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPhoneNumbers_ContactID",
                table: "ContactPhoneNumbers",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_AssignedUserID",
                table: "Contacts",
                column: "AssignedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CustomerID",
                table: "Contacts",
                column: "CustomerID",
                unique: true,
                filter: "[CustomerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PoolID",
                table: "Contacts",
                column: "PoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_StatusID",
                table: "Contacts",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Timeline_Phases_TimelinePhaseID",
                table: "CR_Timeline_Phases",
                column: "TimelinePhaseID");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Timelines_RequestID",
                table: "CR_Timelines",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Timelines_TimelineID",
                table: "CR_Timelines",
                column: "TimelineID");

            migrationBuilder.CreateIndex(
                name: "IX_CRPhaseInputOptions_InputID",
                table: "CRPhaseInputOptions",
                column: "InputID");

            migrationBuilder.CreateIndex(
                name: "IX_CRPhaseInputs_PhaseID",
                table: "CRPhaseInputs",
                column: "PhaseID");

            migrationBuilder.CreateIndex(
                name: "IX_CRTimeline_Phases_PhaseID",
                table: "CRTimeline_Phases",
                column: "PhaseID");

            migrationBuilder.CreateIndex(
                name: "IX_CRTimeline_Phases_TimelineID",
                table: "CRTimeline_Phases",
                column: "TimelineID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Tags_TagID",
                table: "Customer_Tags",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerComments_CustomerID",
                table: "CustomerComments",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPhoneNumber_CustomerID",
                table: "CustomerPhoneNumber",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRequests_ContactID",
                table: "CustomerRequests",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRequests_DealID",
                table: "CustomerRequests",
                column: "DealID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRequests_RequestTypeID",
                table: "CustomerRequests",
                column: "RequestTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRequestTypes_TimelineID",
                table: "CustomerRequestTypes",
                column: "TimelineID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AssignedUserID",
                table: "Customers",
                column: "AssignedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_CustomerID",
                table: "Deals",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_DoneDeal_Favorites_UserID",
                table: "DoneDeal_Favorites",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_DoneDeals_AssignedUserID",
                table: "DoneDeals",
                column: "AssignedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DoneDeals_DealID",
                table: "DoneDeals",
                column: "DealID");

            migrationBuilder.CreateIndex(
                name: "IX_Inputs_AttributeID",
                table: "Inputs",
                column: "AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_Inputs_SectionID",
                table: "Inputs",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_LAttributes_ParentAttributeID",
                table: "LAttributes",
                column: "ParentAttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_Favorites_UserID",
                table: "Lead_Favorites",
                column: "UserID");

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
                name: "IX_LeadSourceNames_ChannelID",
                table: "LeadSourceNames",
                column: "ChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadSourceNames_LeadSourceTypeID",
                table: "LeadSourceNames",
                column: "LeadSourceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadSourceTypes_ChannelID",
                table: "LeadSourceTypes",
                column: "ChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_LInterestInputs_LInterestID",
                table: "LInterestInputs",
                column: "LInterestID");

            migrationBuilder.CreateIndex(
                name: "IX_LInterests_LevelID",
                table: "LInterests",
                column: "LevelID");

            migrationBuilder.CreateIndex(
                name: "IX_LInterests_LocationID",
                table: "LInterests",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_LInterests_OwnerID",
                table: "LInterests",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Pools_PoolID",
                table: "Location_Pools",
                column: "PoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ParentLocationID",
                table: "Locations",
                column: "ParentLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_LSections_LevelID",
                table: "LSections",
                column: "LevelID");

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
                name: "IX_Opportunity_Favorites_UserID",
                table: "Opportunity_Favorites",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Pool_Users_UserID",
                table: "Pool_Users",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PoolConnectionIDs_UserID",
                table: "PoolConnectionIDs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PoolRequests_PoolID",
                table: "PoolRequests",
                column: "PoolID");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessFlows_ContactID",
                table: "ProcessFlows",
                column: "ContactID",
                unique: true);

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
                name: "IX_Prospect_Favorites_UserID",
                table: "Prospect_Favorites",
                column: "UserID");

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
                name: "IX_SectionAuthorizations_AuthorizationSectionID",
                table: "SectionAuthorizations",
                column: "AuthorizationSectionID");

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

            migrationBuilder.CreateIndex(
                name: "IX_SelectedOptions_SectionQuestionAnswerID",
                table: "SelectedOptions",
                column: "SectionQuestionAnswerID",
                unique: true,
                filter: "[SectionQuestionAnswerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedOptions_SectionQuestionOptionID",
                table: "SelectedOptions",
                column: "SectionQuestionOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionDetails_ActivityID",
                table: "SubmissionDetails",
                column: "ActivityID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionDetails_ScheduledActivityID",
                table: "SubmissionDetails",
                column: "ScheduledActivityID");
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
                name: "Contact_Favorites");

            migrationBuilder.DropTable(
                name: "Contact_Tags");

            migrationBuilder.DropTable(
                name: "ContactComments");

            migrationBuilder.DropTable(
                name: "ContactPhoneNumbers");

            migrationBuilder.DropTable(
                name: "CR_Timeline_Phases");

            migrationBuilder.DropTable(
                name: "CR_Timelines");

            migrationBuilder.DropTable(
                name: "CRPhaseInputOptions");

            migrationBuilder.DropTable(
                name: "Customer_Tags");

            migrationBuilder.DropTable(
                name: "CustomerComments");

            migrationBuilder.DropTable(
                name: "CustomerPhoneNumber");

            migrationBuilder.DropTable(
                name: "DoneDeal_Favorites");

            migrationBuilder.DropTable(
                name: "Inputs");

            migrationBuilder.DropTable(
                name: "Lead_Favorites");

            migrationBuilder.DropTable(
                name: "LeadSourceNames");

            migrationBuilder.DropTable(
                name: "LInterestInputs");

            migrationBuilder.DropTable(
                name: "Location_Pools");

            migrationBuilder.DropTable(
                name: "Opportunity_Favorites");

            migrationBuilder.DropTable(
                name: "Pool_Users");

            migrationBuilder.DropTable(
                name: "PoolConnectionIDs");

            migrationBuilder.DropTable(
                name: "PoolRequests");

            migrationBuilder.DropTable(
                name: "Prospect_Favorites");

            migrationBuilder.DropTable(
                name: "SectionAuthorizations");

            migrationBuilder.DropTable(
                name: "SelectedOptions");

            migrationBuilder.DropTable(
                name: "SubmissionDetails");

            migrationBuilder.DropTable(
                name: "SystemConfiguration");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CRTimeline_Phases");

            migrationBuilder.DropTable(
                name: "CustomerRequests");

            migrationBuilder.DropTable(
                name: "CRPhaseInputs");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "DoneDeals");

            migrationBuilder.DropTable(
                name: "LAttributes");

            migrationBuilder.DropTable(
                name: "LSections");

            migrationBuilder.DropTable(
                name: "Leads");

            migrationBuilder.DropTable(
                name: "LeadSourceTypes");

            migrationBuilder.DropTable(
                name: "LInterests");

            migrationBuilder.DropTable(
                name: "Opportunities");

            migrationBuilder.DropTable(
                name: "Prospects");

            migrationBuilder.DropTable(
                name: "AuthorizationSections");

            migrationBuilder.DropTable(
                name: "SectionQuestionAnswers");

            migrationBuilder.DropTable(
                name: "SectionQuestionOptions");

            migrationBuilder.DropTable(
                name: "CustomerRequestTypes");

            migrationBuilder.DropTable(
                name: "CRPhases");

            migrationBuilder.DropTable(
                name: "LeadStatuses");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "OpportunityStatuses");

            migrationBuilder.DropTable(
                name: "ProspectStatuses");

            migrationBuilder.DropTable(
                name: "ActivitySections");

            migrationBuilder.DropTable(
                name: "SectionQuestions");

            migrationBuilder.DropTable(
                name: "CRTimelines");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "ProcessFlows");

            migrationBuilder.DropTable(
                name: "ActivityTypes");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Deals");

            migrationBuilder.DropTable(
                name: "Pools");

            migrationBuilder.DropTable(
                name: "ContactStatuses");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
