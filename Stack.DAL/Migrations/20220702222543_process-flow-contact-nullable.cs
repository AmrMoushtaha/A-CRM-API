using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class processflowcontactnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessFlows_Contacts_ContactID",
                table: "ProcessFlows");

            migrationBuilder.DropIndex(
                name: "IX_ProcessFlows_ContactID",
                table: "ProcessFlows");

            migrationBuilder.AlterColumn<long>(
                name: "ContactID",
                table: "ProcessFlows",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessFlows_ContactID",
                table: "ProcessFlows",
                column: "ContactID",
                unique: true,
                filter: "[ContactID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessFlows_Contacts_ContactID",
                table: "ProcessFlows",
                column: "ContactID",
                principalTable: "Contacts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessFlows_Contacts_ContactID",
                table: "ProcessFlows");

            migrationBuilder.DropIndex(
                name: "IX_ProcessFlows_ContactID",
                table: "ProcessFlows");

            migrationBuilder.AlterColumn<long>(
                name: "ContactID",
                table: "ProcessFlows",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessFlows_ContactID",
                table: "ProcessFlows",
                column: "ContactID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessFlows_Contacts_ContactID",
                table: "ProcessFlows",
                column: "ContactID",
                principalTable: "Contacts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
