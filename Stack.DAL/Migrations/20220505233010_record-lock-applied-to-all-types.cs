using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class recordlockappliedtoalltypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ForceUnlock_JobID",
                table: "Prospects",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Prospects",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ForceUnlock_JobID",
                table: "Opportunities",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Opportunities",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ForceUnlock_JobID",
                table: "Leads",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Leads",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForceUnlock_JobID",
                table: "Prospects");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Prospects");

            migrationBuilder.DropColumn(
                name: "ForceUnlock_JobID",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "ForceUnlock_JobID",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Leads");
        }
    }
}
