using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class customerstagedynamicstatusnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactStatuses_StatusID",
                table: "Contacts");

            migrationBuilder.AlterColumn<long>(
                name: "StatusID",
                table: "Contacts",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactStatuses_StatusID",
                table: "Contacts",
                column: "StatusID",
                principalTable: "ContactStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactStatuses_StatusID",
                table: "Contacts");

            migrationBuilder.AlterColumn<long>(
                name: "StatusID",
                table: "Contacts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactStatuses_StatusID",
                table: "Contacts",
                column: "StatusID",
                principalTable: "ContactStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
