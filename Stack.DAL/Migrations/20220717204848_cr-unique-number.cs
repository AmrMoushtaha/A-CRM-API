using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class cruniquenumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UniqueNumber",
                table: "CustomerRequests",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueNumber",
                table: "CustomerRequests");
        }
    }
}
