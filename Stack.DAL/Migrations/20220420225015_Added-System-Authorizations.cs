using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class AddedSystemAuthorizations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SystemAuthorizations",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemAuthorizations",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuthorizationSections",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAR = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizationSections", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SectionAuthorizations",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAR = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    AuthorizationSectionID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionAuthorizations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SectionAuthorizations_AuthorizationSections_AuthorizationSectionID",
                        column: x => x.AuthorizationSectionID,
                        principalTable: "AuthorizationSections",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SectionAuthorizations_AuthorizationSectionID",
                table: "SectionAuthorizations",
                column: "AuthorizationSectionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SectionAuthorizations");

            migrationBuilder.DropTable(
                name: "AuthorizationSections");

            migrationBuilder.DropColumn(
                name: "SystemAuthorizations",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SystemAuthorizations",
                table: "AspNetRoles");
        }
    }
}
