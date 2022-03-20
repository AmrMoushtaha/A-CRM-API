using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class contactstatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactStatus_StatusID",
                table: "Contacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactStatus",
                table: "ContactStatus");

            migrationBuilder.RenameTable(
                name: "ContactStatus",
                newName: "ContactStatuses");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ContactStatuses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactStatuses",
                table: "ContactStatuses",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactStatuses_StatusID",
                table: "Contacts",
                column: "StatusID",
                principalTable: "ContactStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactStatuses_StatusID",
                table: "Contacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactStatuses",
                table: "ContactStatuses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ContactStatuses");

            migrationBuilder.RenameTable(
                name: "ContactStatuses",
                newName: "ContactStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactStatus",
                table: "ContactStatus",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactStatus_StatusID",
                table: "Contacts",
                column: "StatusID",
                principalTable: "ContactStatus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
