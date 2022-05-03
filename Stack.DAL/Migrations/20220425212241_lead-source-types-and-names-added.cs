using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class leadsourcetypesandnamesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeadSourceTypes",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEN = table.Column<string>(nullable: true),
                    TitleAR = table.Column<string>(nullable: true),
                    DescriptionEN = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadSourceTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LeadSourceNames",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEN = table.Column<string>(nullable: true),
                    TitleAR = table.Column<string>(nullable: true),
                    DescriptionEN = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true),
                    LeadSourceTypeID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadSourceNames", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LeadSourceNames_LeadSourceTypes_LeadSourceTypeID",
                        column: x => x.LeadSourceTypeID,
                        principalTable: "LeadSourceTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadSourceNames_LeadSourceTypeID",
                table: "LeadSourceNames",
                column: "LeadSourceTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadSourceNames");

            migrationBuilder.DropTable(
                name: "LeadSourceTypes");
        }
    }
}
