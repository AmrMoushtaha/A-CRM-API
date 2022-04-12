using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class updatedactivitysubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStageChanged",
                table: "SubmissionDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledActivityDate",
                table: "SubmissionDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStageChanged",
                table: "SubmissionDetails");

            migrationBuilder.DropColumn(
                name: "ScheduledActivityDate",
                table: "SubmissionDetails");
        }
    }
}
