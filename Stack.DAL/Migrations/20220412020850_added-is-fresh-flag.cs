using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class addedisfreshflag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFresh",
                table: "Prospects",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFresh",
                table: "Opportunities",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFresh",
                table: "Leads",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFresh",
                table: "Contacts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFresh",
                table: "Prospects");

            migrationBuilder.DropColumn(
                name: "IsFresh",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "IsFresh",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "IsFresh",
                table: "Contacts");
        }
    }
}
