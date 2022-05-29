using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class interestlinkwcr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "InterestID",
                table: "CustomerRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeIndex",
                table: "CustomerRequests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterestID",
                table: "CustomerRequests");

            migrationBuilder.DropColumn(
                name: "TypeIndex",
                table: "CustomerRequests");
        }
    }
}
