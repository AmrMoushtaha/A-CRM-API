using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class phaseinputanswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CRPhaseInputAnswer",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InputID = table.Column<long>(nullable: false),
                    RequestPhaseID = table.Column<long>(nullable: false),
                    Answer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRPhaseInputAnswer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CRPhaseInputAnswer_CRPhaseInputs_InputID",
                        column: x => x.InputID,
                        principalTable: "CRPhaseInputs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CRPhaseInputAnswer_CR_Timeline_Phases_RequestPhaseID",
                        column: x => x.RequestPhaseID,
                        principalTable: "CR_Timeline_Phases",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CRPhaseInputAnswer_InputID",
                table: "CRPhaseInputAnswer",
                column: "InputID");

            migrationBuilder.CreateIndex(
                name: "IX_CRPhaseInputAnswer_RequestPhaseID",
                table: "CRPhaseInputAnswer",
                column: "RequestPhaseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CRPhaseInputAnswer");
        }
    }
}
