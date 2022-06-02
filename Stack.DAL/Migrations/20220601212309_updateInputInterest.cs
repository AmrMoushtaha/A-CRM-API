using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class updateInputInterest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AttributeID",
                table: "LInterestInputs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InputType",
                table: "LInterestInputs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PredefinedInputType",
                table: "LInterestInputs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttributeID",
                table: "LInterestInputs");

            migrationBuilder.DropColumn(
                name: "InputType",
                table: "LInterestInputs");

            migrationBuilder.DropColumn(
                name: "PredefinedInputType",
                table: "LInterestInputs");
        }
    }
}
