using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class poolconfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Pools",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfigurationType",
                table: "Pools",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Pools");

            migrationBuilder.DropColumn(
                name: "ConfigurationType",
                table: "Pools");
        }
    }
}
