using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class updatedrolemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionAR",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEN",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasParent",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NameAR",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentRoleID",
                table: "AspNetRoles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionAR",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "DescriptionEN",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "HasParent",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "NameAR",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "ParentRoleID",
                table: "AspNetRoles");
        }
    }
}
