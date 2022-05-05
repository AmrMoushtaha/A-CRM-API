using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class contactmodelupdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Contacts_ContactID",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ContactID",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_CustomerID",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ContactID",
                table: "Customers");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CustomerID",
                table: "Contacts",
                column: "CustomerID",
                unique: true,
                filter: "[CustomerID] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contacts_CustomerID",
                table: "Contacts");

            migrationBuilder.AddColumn<long>(
                name: "ContactID",
                table: "Customers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ContactID",
                table: "Customers",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CustomerID",
                table: "Contacts",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Contacts_ContactID",
                table: "Customers",
                column: "ContactID",
                principalTable: "Contacts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
