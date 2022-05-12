using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class channelsicon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "LeadSourceTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "LeadSourceNames",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Channels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "LeadSourceTypes");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "LeadSourceNames");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Channels");
        }
    }
}
