using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class phaseinputanswerupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CRPhaseInputAnswer_CRPhaseInputs_InputID",
                table: "CRPhaseInputAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_CRPhaseInputAnswer_CR_Timeline_Phases_RequestPhaseID",
                table: "CRPhaseInputAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CRPhaseInputAnswer",
                table: "CRPhaseInputAnswer");

            migrationBuilder.RenameTable(
                name: "CRPhaseInputAnswer",
                newName: "CRPhaseInputAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_CRPhaseInputAnswer_RequestPhaseID",
                table: "CRPhaseInputAnswers",
                newName: "IX_CRPhaseInputAnswers_RequestPhaseID");

            migrationBuilder.RenameIndex(
                name: "IX_CRPhaseInputAnswer_InputID",
                table: "CRPhaseInputAnswers",
                newName: "IX_CRPhaseInputAnswers_InputID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CRPhaseInputAnswers",
                table: "CRPhaseInputAnswers",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CRPhaseInputAnswers_CRPhaseInputs_InputID",
                table: "CRPhaseInputAnswers",
                column: "InputID",
                principalTable: "CRPhaseInputs",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CRPhaseInputAnswers_CR_Timeline_Phases_RequestPhaseID",
                table: "CRPhaseInputAnswers",
                column: "RequestPhaseID",
                principalTable: "CR_Timeline_Phases",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CRPhaseInputAnswers_CRPhaseInputs_InputID",
                table: "CRPhaseInputAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_CRPhaseInputAnswers_CR_Timeline_Phases_RequestPhaseID",
                table: "CRPhaseInputAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CRPhaseInputAnswers",
                table: "CRPhaseInputAnswers");

            migrationBuilder.RenameTable(
                name: "CRPhaseInputAnswers",
                newName: "CRPhaseInputAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_CRPhaseInputAnswers_RequestPhaseID",
                table: "CRPhaseInputAnswer",
                newName: "IX_CRPhaseInputAnswer_RequestPhaseID");

            migrationBuilder.RenameIndex(
                name: "IX_CRPhaseInputAnswers_InputID",
                table: "CRPhaseInputAnswer",
                newName: "IX_CRPhaseInputAnswer_InputID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CRPhaseInputAnswer",
                table: "CRPhaseInputAnswer",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CRPhaseInputAnswer_CRPhaseInputs_InputID",
                table: "CRPhaseInputAnswer",
                column: "InputID",
                principalTable: "CRPhaseInputs",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CRPhaseInputAnswer_CR_Timeline_Phases_RequestPhaseID",
                table: "CRPhaseInputAnswer",
                column: "RequestPhaseID",
                principalTable: "CR_Timeline_Phases",
                principalColumn: "ID");
        }
    }
}
