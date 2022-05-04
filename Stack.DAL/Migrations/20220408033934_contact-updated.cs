using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class contactupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ConfigurationType",
                table: "Pools",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Pool_Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Pool_Admins",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinalized",
                table: "Contacts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Pool_Users");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Pool_Admins");

            migrationBuilder.DropColumn(
                name: "IsFinalized",
                table: "Contacts");

            migrationBuilder.AlterColumn<string>(
                name: "ConfigurationType",
                table: "Pools",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
