using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class ActivityModelUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SectionQuestionAnswers_SectionQuestions_QuestionID",
                table: "SectionQuestionAnswers");

            migrationBuilder.DropColumn(
                name: "RoutesTo",
                table: "Sections");

            migrationBuilder.AddColumn<bool>(
                name: "HasDecisionalQuestions",
                table: "Sections",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Sections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDecisional",
                table: "SectionQuestions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "SectionQuestions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "RoutesTo",
                table: "SectionQuestionOptions",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "QuestionID",
                table: "SectionQuestionAnswers",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "ProcessFlows",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "ProcessFlows",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ActivityTypes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "ActivitySections",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "ActivitySections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "ActivitySections",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "Activities",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubtmissionDate",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionQuestionAnswers_SectionQuestions_QuestionID",
                table: "SectionQuestionAnswers",
                column: "QuestionID",
                principalTable: "SectionQuestions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SectionQuestionAnswers_SectionQuestions_QuestionID",
                table: "SectionQuestionAnswers");

            migrationBuilder.DropColumn(
                name: "HasDecisionalQuestions",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "IsDecisional",
                table: "SectionQuestions");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "SectionQuestions");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "ProcessFlows");

            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "ProcessFlows");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ActivityTypes");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "ActivitySections");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "ActivitySections");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "ActivitySections");

            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "SubtmissionDate",
                table: "Activities");

            migrationBuilder.AddColumn<long>(
                name: "RoutesTo",
                table: "Sections",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "RoutesTo",
                table: "SectionQuestionOptions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "QuestionID",
                table: "SectionQuestionAnswers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionQuestionAnswers_SectionQuestions_QuestionID",
                table: "SectionQuestionAnswers",
                column: "QuestionID",
                principalTable: "SectionQuestions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
