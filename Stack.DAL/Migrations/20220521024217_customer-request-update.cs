using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class customerrequestupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CRPhases",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAR = table.Column<string>(nullable: true),
                    TitleEN = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRPhases", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CRPhaseTimelines",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRPhaseTimelines", x => x.ID);
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
                    TitleEN = table.Column<string>(nullable: true)
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
                    PhaseID = table.Column<long>(nullable: false),
                    TimelineID = table.Column<long>(nullable: false),
                    ParentPhaseID = table.Column<long>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRTimeline_Phases", x => new { x.PhaseID, x.TimelineID });
                    table.ForeignKey(
                        name: "FK_CRTimeline_Phases_CRPhases_PhaseID",
                        column: x => x.PhaseID,
                        principalTable: "CRPhases",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CRTimeline_Phases_CRPhaseTimelines_TimelineID",
                        column: x => x.TimelineID,
                        principalTable: "CRPhaseTimelines",
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
                    InputsTemplate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRequestTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerRequestTypes_CRPhaseTimelines_TimelineID",
                        column: x => x.TimelineID,
                        principalTable: "CRPhaseTimelines",
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
                    TitleAR = table.Column<string>(nullable: true)
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
                name: "CustomerRequests",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    DealID = table.Column<long>(nullable: false),
                    ContactID = table.Column<long>(nullable: false),
                    TimelineID = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    RequestTypeID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRequests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerRequests_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerRequests_Deals_DealID",
                        column: x => x.DealID,
                        principalTable: "Deals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerRequests_CustomerRequestTypes_RequestTypeID",
                        column: x => x.RequestTypeID,
                        principalTable: "CustomerRequestTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerRequests_CRPhaseTimelines_TimelineID",
                        column: x => x.TimelineID,
                        principalTable: "CRPhaseTimelines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerRequestInputs",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEN = table.Column<string>(nullable: true),
                    TitleAR = table.Column<string>(nullable: true),
                    RequestID = table.Column<long>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRequestInputs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerRequestInputs_CustomerRequests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "CustomerRequests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerRequestInputOptions",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEN = table.Column<string>(nullable: true),
                    TitleAR = table.Column<string>(nullable: true),
                    InputID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRequestInputOptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerRequestInputOptions_CustomerRequestInputs_InputID",
                        column: x => x.InputID,
                        principalTable: "CustomerRequestInputs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CRPhaseInputOptions_InputID",
                table: "CRPhaseInputOptions",
                column: "InputID");

            migrationBuilder.CreateIndex(
                name: "IX_CRPhaseInputs_PhaseID",
                table: "CRPhaseInputs",
                column: "PhaseID");

            migrationBuilder.CreateIndex(
                name: "IX_CRTimeline_Phases_TimelineID",
                table: "CRTimeline_Phases",
                column: "TimelineID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRequestInputOptions_InputID",
                table: "CustomerRequestInputOptions",
                column: "InputID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRequestInputs_RequestID",
                table: "CustomerRequestInputs",
                column: "RequestID");

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
                name: "IX_CustomerRequests_TimelineID",
                table: "CustomerRequests",
                column: "TimelineID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRequestTypes_TimelineID",
                table: "CustomerRequestTypes",
                column: "TimelineID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CRPhaseInputOptions");

            migrationBuilder.DropTable(
                name: "CRTimeline_Phases");

            migrationBuilder.DropTable(
                name: "CustomerRequestInputOptions");

            migrationBuilder.DropTable(
                name: "CRPhaseInputs");

            migrationBuilder.DropTable(
                name: "CustomerRequestInputs");

            migrationBuilder.DropTable(
                name: "CRPhases");

            migrationBuilder.DropTable(
                name: "CustomerRequests");

            migrationBuilder.DropTable(
                name: "CustomerRequestTypes");

            migrationBuilder.DropTable(
                name: "CRPhaseTimelines");
        }
    }
}
