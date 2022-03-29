using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class customerrequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CRTypes",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAR = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CRSections",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAR = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    HasDecisionalQuestions = table.Column<bool>(nullable: false),
                    TypeID = table.Column<long>(nullable: false),
                    RequestTypeID = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRSections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CRSections_CRTypes_RequestTypeID",
                        column: x => x.RequestTypeID,
                        principalTable: "CRTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerRequests",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessFlowID = table.Column<long>(nullable: false),
                    TypeID = table.Column<long>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    SubtmissionDate = table.Column<DateTime>(nullable: true),
                    IsSubmitted = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRequests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerRequests_CRTypes_TypeID",
                        column: x => x.TypeID,
                        principalTable: "CRTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CRSectionQuestions",
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
                    table.PrimaryKey("PK_CRSectionQuestions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CRSectionQuestions_CRSections_SectionID",
                        column: x => x.SectionID,
                        principalTable: "CRSections",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CR_Sections",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsSubmitted = table.Column<bool>(nullable: false),
                    RequestID = table.Column<long>(nullable: false),
                    SectionID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Sections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CR_Sections_CustomerRequests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "CustomerRequests",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CR_Sections_CRSections_SectionID",
                        column: x => x.SectionID,
                        principalTable: "CRSections",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CRSubmissionDetails",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmissionDate = table.Column<DateTime>(nullable: true),
                    IsStatusChanged = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    RequestID = table.Column<long>(nullable: false),
                    CRTypeID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRSubmissionDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CRSubmissionDetails_CRTypes_CRTypeID",
                        column: x => x.CRTypeID,
                        principalTable: "CRTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CRSubmissionDetails_CustomerRequests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "CustomerRequests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CRSectionQuestionOptions",
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
                    table.PrimaryKey("PK_CRSectionQuestionOptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CRSectionQuestionOptions_CRSectionQuestions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "CRSectionQuestions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CRSectionQuestionAnswers",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: true),
                    RequestSectionID = table.Column<long>(nullable: true),
                    QuestionID = table.Column<long>(nullable: true),
                    CR_SectionID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRSectionQuestionAnswers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CRSectionQuestionAnswers_CR_Sections_CR_SectionID",
                        column: x => x.CR_SectionID,
                        principalTable: "CR_Sections",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CRSectionQuestionAnswers_CRSectionQuestions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "CRSectionQuestions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CRSectionQuestionAnswers_CRSections_RequestSectionID",
                        column: x => x.RequestSectionID,
                        principalTable: "CRSections",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CRSelectedOptions",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionQuestionOptionID = table.Column<long>(nullable: true),
                    SectionQuestionAnswerID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRSelectedOptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CRSelectedOptions_CRSectionQuestionAnswers_SectionQuestionAnswerID",
                        column: x => x.SectionQuestionAnswerID,
                        principalTable: "CRSectionQuestionAnswers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CRSelectedOptions_CRSectionQuestionOptions_SectionQuestionOptionID",
                        column: x => x.SectionQuestionOptionID,
                        principalTable: "CRSectionQuestionOptions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CR_Sections_RequestID",
                table: "CR_Sections",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Sections_SectionID",
                table: "CR_Sections",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_CRSectionQuestionAnswers_CR_SectionID",
                table: "CRSectionQuestionAnswers",
                column: "CR_SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_CRSectionQuestionAnswers_QuestionID",
                table: "CRSectionQuestionAnswers",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_CRSectionQuestionAnswers_RequestSectionID",
                table: "CRSectionQuestionAnswers",
                column: "RequestSectionID");

            migrationBuilder.CreateIndex(
                name: "IX_CRSectionQuestionOptions_QuestionID",
                table: "CRSectionQuestionOptions",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_CRSectionQuestions_SectionID",
                table: "CRSectionQuestions",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_CRSections_RequestTypeID",
                table: "CRSections",
                column: "RequestTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CRSelectedOptions_SectionQuestionAnswerID",
                table: "CRSelectedOptions",
                column: "SectionQuestionAnswerID",
                unique: true,
                filter: "[SectionQuestionAnswerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CRSelectedOptions_SectionQuestionOptionID",
                table: "CRSelectedOptions",
                column: "SectionQuestionOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_CRSubmissionDetails_CRTypeID",
                table: "CRSubmissionDetails",
                column: "CRTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CRSubmissionDetails_RequestID",
                table: "CRSubmissionDetails",
                column: "RequestID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRequests_TypeID",
                table: "CustomerRequests",
                column: "TypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CRSelectedOptions");

            migrationBuilder.DropTable(
                name: "CRSubmissionDetails");

            migrationBuilder.DropTable(
                name: "CRSectionQuestionAnswers");

            migrationBuilder.DropTable(
                name: "CRSectionQuestionOptions");

            migrationBuilder.DropTable(
                name: "CR_Sections");

            migrationBuilder.DropTable(
                name: "CRSectionQuestions");

            migrationBuilder.DropTable(
                name: "CustomerRequests");

            migrationBuilder.DropTable(
                name: "CRSections");

            migrationBuilder.DropTable(
                name: "CRTypes");
        }
    }
}
