using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class UpdatedUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NameAR",
                table: "AspNetUsers",
                type: "nvarchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NameAR",
                table: "AspNetUsers",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldNullable: true);
        }
    }
}
