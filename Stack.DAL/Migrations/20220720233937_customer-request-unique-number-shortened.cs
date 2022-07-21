using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class customerrequestuniquenumbershortened : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UniqueNumber",
                table: "CustomerRequests",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UniqueNumber",
                table: "CustomerRequests",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
