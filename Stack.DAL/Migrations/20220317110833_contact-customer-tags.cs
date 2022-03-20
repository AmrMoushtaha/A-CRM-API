using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class contactcustomertags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Contact_Tags",
                columns: table => new
                {
                    ContactID = table.Column<long>(nullable: false),
                    TagID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact_Tags", x => new { x.ContactID, x.TagID });
                    table.ForeignKey(
                        name: "FK_Contact_Tags_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Contact_Tags_Tags_TagID",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Customer_Tags",
                columns: table => new
                {
                    CustomerID = table.Column<long>(nullable: false),
                    TagID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer_Tags", x => new { x.CustomerID, x.TagID });
                    table.ForeignKey(
                        name: "FK_Customer_Tags_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Customer_Tags_Tags_TagID",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Tags_TagID",
                table: "Contact_Tags",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Tags_TagID",
                table: "Customer_Tags",
                column: "TagID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact_Tags");

            migrationBuilder.DropTable(
                name: "Customer_Tags");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
