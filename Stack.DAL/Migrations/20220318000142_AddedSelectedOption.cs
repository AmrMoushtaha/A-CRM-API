using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class AddedSelectedOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitSection",
                table: "Sections",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "RoutesTo",
                table: "SectionQuestionOptions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ActivityTypes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SelectedOptions",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionQuestionOptionID = table.Column<long>(nullable: true),
                    SectionQuestionAnswerID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedOptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SelectedOptions_SectionQuestionAnswers_SectionQuestionAnswerID",
                        column: x => x.SectionQuestionAnswerID,
                        principalTable: "SectionQuestionAnswers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SelectedOptions_SectionQuestionOptions_SectionQuestionOptionID",
                        column: x => x.SectionQuestionOptionID,
                        principalTable: "SectionQuestionOptions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelectedOptions_SectionQuestionAnswerID",
                table: "SelectedOptions",
                column: "SectionQuestionAnswerID",
                unique: true,
                filter: "[SectionQuestionAnswerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedOptions_SectionQuestionOptionID",
                table: "SelectedOptions",
                column: "SectionQuestionOptionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectedOptions");

            migrationBuilder.DropColumn(
                name: "IsSubmitSection",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ActivityTypes");

            migrationBuilder.AlterColumn<string>(
                name: "RoutesTo",
                table: "SectionQuestionOptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long));
        }
    }
}
