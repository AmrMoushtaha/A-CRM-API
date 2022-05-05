using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class updateInputAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLevelDependent",
                table: "Inputs");

            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "Inputs");

            migrationBuilder.DropColumn(
                name: "MinValue",
                table: "Inputs");

            migrationBuilder.AddColumn<bool>(
                name: "IsPredefined",
                table: "LAttributes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPredefined",
                table: "LAttributes");

            migrationBuilder.AddColumn<bool>(
                name: "IsLevelDependent",
                table: "Inputs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxValue",
                table: "Inputs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinValue",
                table: "Inputs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
