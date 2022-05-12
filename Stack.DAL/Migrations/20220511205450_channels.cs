using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class channels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChannelID",
                table: "LeadSourceTypes",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LeadSourceTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ChannelID",
                table: "LeadSourceNames",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LeadSourceNames",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEN = table.Column<string>(nullable: true),
                    TitleAR = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    DescriptionEN = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadSourceTypes_ChannelID",
                table: "LeadSourceTypes",
                column: "ChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadSourceNames_ChannelID",
                table: "LeadSourceNames",
                column: "ChannelID");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadSourceNames_LeadSourceTypes_ChannelID",
                table: "LeadSourceNames",
                column: "ChannelID",
                principalTable: "LeadSourceTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadSourceTypes_Channels_ChannelID",
                table: "LeadSourceTypes",
                column: "ChannelID",
                principalTable: "Channels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadSourceNames_LeadSourceTypes_ChannelID",
                table: "LeadSourceNames");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadSourceTypes_Channels_ChannelID",
                table: "LeadSourceTypes");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropIndex(
                name: "IX_LeadSourceTypes_ChannelID",
                table: "LeadSourceTypes");

            migrationBuilder.DropIndex(
                name: "IX_LeadSourceNames_ChannelID",
                table: "LeadSourceNames");

            migrationBuilder.DropColumn(
                name: "ChannelID",
                table: "LeadSourceTypes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LeadSourceTypes");

            migrationBuilder.DropColumn(
                name: "ChannelID",
                table: "LeadSourceNames");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LeadSourceNames");
        }
    }
}
