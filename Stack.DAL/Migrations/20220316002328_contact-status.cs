using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class contactstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Contacts");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryPhoneNumber",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StatusID",
                table: "Contacts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ContactStatus",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EN = table.Column<string>(nullable: true),
                    AR = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactStatus", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_StatusID",
                table: "Contacts",
                column: "StatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactStatus_StatusID",
                table: "Contacts",
                column: "StatusID",
                principalTable: "ContactStatus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactStatus_StatusID",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "ContactStatus");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_StatusID",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "PrimaryPhoneNumber",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "StatusID",
                table: "Contacts");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
