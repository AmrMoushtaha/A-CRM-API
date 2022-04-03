using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class updateInterset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LevelType",
                table: "LInterests");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "LInterests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "LInterests");

            migrationBuilder.AddColumn<int>(
                name: "LevelType",
                table: "LInterests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
