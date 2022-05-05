using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class DealModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "activeStageType",
                table: "Deals",
                newName: "ActiveStageType");

            migrationBuilder.RenameColumn(
                name: "activeStageID",
                table: "Deals",
                newName: "ActiveStageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActiveStageType",
                table: "Deals",
                newName: "activeStageType");

            migrationBuilder.RenameColumn(
                name: "ActiveStageID",
                table: "Deals",
                newName: "activeStageID");
        }
    }
}
