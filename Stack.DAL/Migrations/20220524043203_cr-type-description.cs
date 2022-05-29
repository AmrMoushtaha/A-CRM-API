using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class crtypedescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InputsTemplate",
                table: "CustomerRequestTypes");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAR",
                table: "CustomerRequestTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEN",
                table: "CustomerRequestTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionAR",
                table: "CustomerRequestTypes");

            migrationBuilder.DropColumn(
                name: "DescriptionEN",
                table: "CustomerRequestTypes");

            migrationBuilder.AddColumn<string>(
                name: "InputsTemplate",
                table: "CustomerRequestTypes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
