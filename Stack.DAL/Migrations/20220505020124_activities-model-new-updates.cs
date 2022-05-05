using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class activitiesmodelnewupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubtmissionDate",
                table: "Activities");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmissionDate",
                table: "Activities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmissionDate",
                table: "Activities");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubtmissionDate",
                table: "Activities",
                type: "datetime2",
                nullable: true);
        }
    }
}
