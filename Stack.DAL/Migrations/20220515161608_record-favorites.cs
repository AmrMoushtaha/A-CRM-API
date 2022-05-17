using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class recordfavorites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contact_Favorites",
                columns: table => new
                {
                    ContactID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact_Favorites", x => new { x.ContactID, x.UserID });
                    table.ForeignKey(
                        name: "FK_Contact_Favorites_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Contact_Favorites_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DoneDeal_Favorites",
                columns: table => new
                {
                    RecordID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoneDeal_Favorites", x => new { x.RecordID, x.UserID });
                    table.ForeignKey(
                        name: "FK_DoneDeal_Favorites_DoneDeals_RecordID",
                        column: x => x.RecordID,
                        principalTable: "DoneDeals",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_DoneDeal_Favorites_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lead_Favorites",
                columns: table => new
                {
                    RecordID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lead_Favorites", x => new { x.RecordID, x.UserID });
                    table.ForeignKey(
                        name: "FK_Lead_Favorites_Leads_RecordID",
                        column: x => x.RecordID,
                        principalTable: "Leads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Lead_Favorites_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Opportunity_Favorites",
                columns: table => new
                {
                    RecordID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunity_Favorites", x => new { x.RecordID, x.UserID });
                    table.ForeignKey(
                        name: "FK_Opportunity_Favorites_Opportunities_RecordID",
                        column: x => x.RecordID,
                        principalTable: "Opportunities",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Opportunity_Favorites_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Prospect_Favorites",
                columns: table => new
                {
                    RecordID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prospect_Favorites", x => new { x.RecordID, x.UserID });
                    table.ForeignKey(
                        name: "FK_Prospect_Favorites_Prospects_RecordID",
                        column: x => x.RecordID,
                        principalTable: "Prospects",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Prospect_Favorites_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Favorites_UserID",
                table: "Contact_Favorites",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_DoneDeal_Favorites_UserID",
                table: "DoneDeal_Favorites",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_Favorites_UserID",
                table: "Lead_Favorites",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_Favorites_UserID",
                table: "Opportunity_Favorites",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Prospect_Favorites_UserID",
                table: "Prospect_Favorites",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact_Favorites");

            migrationBuilder.DropTable(
                name: "DoneDeal_Favorites");

            migrationBuilder.DropTable(
                name: "Lead_Favorites");

            migrationBuilder.DropTable(
                name: "Opportunity_Favorites");

            migrationBuilder.DropTable(
                name: "Prospect_Favorites");
        }
    }
}
