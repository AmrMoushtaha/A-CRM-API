using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class modelbuilderupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SectionQuestionAnswers_ActivitySections_ActivitySectionID",
                table: "SectionQuestionAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionQuestionAnswers_SectionQuestions_QuestionID",
                table: "SectionQuestionAnswers");

            migrationBuilder.AddForeignKey(
                name: "FK_SectionQuestionAnswers_ActivitySections_ActivitySectionID",
                table: "SectionQuestionAnswers",
                column: "ActivitySectionID",
                principalTable: "ActivitySections",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionQuestionAnswers_SectionQuestions_QuestionID",
                table: "SectionQuestionAnswers",
                column: "QuestionID",
                principalTable: "SectionQuestions",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SectionQuestionAnswers_ActivitySections_ActivitySectionID",
                table: "SectionQuestionAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionQuestionAnswers_SectionQuestions_QuestionID",
                table: "SectionQuestionAnswers");

            migrationBuilder.AddForeignKey(
                name: "FK_SectionQuestionAnswers_ActivitySections_ActivitySectionID",
                table: "SectionQuestionAnswers",
                column: "ActivitySectionID",
                principalTable: "ActivitySections",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionQuestionAnswers_SectionQuestions_QuestionID",
                table: "SectionQuestionAnswers",
                column: "QuestionID",
                principalTable: "SectionQuestions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
