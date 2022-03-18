using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class updatedcontactphonenumbers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactPhoneNumber_Contacts_ContactID",
                table: "ContactPhoneNumber");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactPhoneNumber",
                table: "ContactPhoneNumber");

            migrationBuilder.RenameTable(
                name: "ContactPhoneNumber",
                newName: "ContactPhoneNumbers");

            migrationBuilder.RenameIndex(
                name: "IX_ContactPhoneNumber_ContactID",
                table: "ContactPhoneNumbers",
                newName: "IX_ContactPhoneNumbers_ContactID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactPhoneNumbers",
                table: "ContactPhoneNumbers",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPhoneNumbers_Contacts_ContactID",
                table: "ContactPhoneNumbers",
                column: "ContactID",
                principalTable: "Contacts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactPhoneNumbers_Contacts_ContactID",
                table: "ContactPhoneNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactPhoneNumbers",
                table: "ContactPhoneNumbers");

            migrationBuilder.RenameTable(
                name: "ContactPhoneNumbers",
                newName: "ContactPhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_ContactPhoneNumbers_ContactID",
                table: "ContactPhoneNumber",
                newName: "IX_ContactPhoneNumber_ContactID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactPhoneNumber",
                table: "ContactPhoneNumber",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPhoneNumber_Contacts_ContactID",
                table: "ContactPhoneNumber",
                column: "ContactID",
                principalTable: "Contacts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
