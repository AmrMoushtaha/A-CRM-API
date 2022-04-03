using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class updateLinterest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ID",
                table: "LInterest_LInterestInputs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LInterest_LInterestInputs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ID",
                table: "LInterest_InterestAttributes",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LInterest_InterestAttributes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID",
                table: "LInterest_LInterestInputs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LInterest_LInterestInputs");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "LInterest_InterestAttributes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LInterest_InterestAttributes");
        }
    }
}
