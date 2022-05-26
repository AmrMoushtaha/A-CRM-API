using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class requesttypeupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "CustomerRequests");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CustomerRequestTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatorName",
                table: "CustomerRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CustomerRequests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CustomerRequestTypes");

            migrationBuilder.DropColumn(
                name: "CreatorName",
                table: "CustomerRequests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CustomerRequests");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "CustomerRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
