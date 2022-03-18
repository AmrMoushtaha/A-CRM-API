using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class contactdetailsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstNameAR",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "FirstNameEN",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "LastNameAR",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "LastNameEN",
                table: "Contacts");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullNameAR",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullNameEN",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeadSourceName",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeadSourceType",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "Contacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "FullNameAR",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "FullNameEN",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "LeadSourceName",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "LeadSourceType",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "Contacts");

            migrationBuilder.AddColumn<string>(
                name: "FirstNameAR",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameEN",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastNameAR",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastNameEN",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
