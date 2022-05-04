using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class contactcapacitycheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "activeStageID",
                table: "Deals",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "activeStageType",
                table: "Deals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ContactID",
                table: "Customers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeadSourceName",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeadSourceType",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryPhoneNumber",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CapacityCalculated",
                table: "Contacts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DoneDeals",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedUserID = table.Column<string>(nullable: true),
                    DealID = table.Column<long>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoneDeals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DoneDeals_AspNetUsers_AssignedUserID",
                        column: x => x.AssignedUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoneDeals_Deals_DealID",
                        column: x => x.DealID,
                        principalTable: "Deals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ContactID",
                table: "Customers",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_DoneDeals_AssignedUserID",
                table: "DoneDeals",
                column: "AssignedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DoneDeals_DealID",
                table: "DoneDeals",
                column: "DealID");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Contacts_ContactID",
                table: "Customers",
                column: "ContactID",
                principalTable: "Contacts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Contacts_ContactID",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "DoneDeals");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ContactID",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "activeStageID",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "activeStageType",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ContactID",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LeadSourceName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LeadSourceType",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PrimaryPhoneNumber",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CapacityCalculated",
                table: "Contacts");
        }
    }
}
