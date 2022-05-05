using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class updateHierarchyAndInterests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LInterest_InterestAttributes");

            migrationBuilder.DropTable(
                name: "LInterest_LInterestInputs");

            migrationBuilder.DropTable(
                name: "InterestAttributes");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "LInterests");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "LInterests");

            migrationBuilder.DropColumn(
                name: "LabelAR",
                table: "LInterestInputs");

            migrationBuilder.DropColumn(
                name: "LabelEN",
                table: "LInterestInputs");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "LInterestInputs");

            migrationBuilder.AddColumn<long>(
                name: "LevelID",
                table: "LInterests",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "AttributeID",
                table: "LInterestInputs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "InputField",
                table: "LInterestInputs",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InputID",
                table: "LInterestInputs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "InputType",
                table: "LInterestInputs",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LInterestID",
                table: "LInterestInputs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LAttributes",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LabelAR = table.Column<string>(nullable: true),
                    LabelEN = table.Column<string>(nullable: true),
                    ParentAttributeID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LAttributes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LAttributes_LAttributes_ParentAttributeID",
                        column: x => x.ParentAttributeID,
                        principalTable: "LAttributes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LabelAR = table.Column<string>(nullable: true),
                    LabelEN = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LSections",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LabelEN = table.Column<string>(nullable: true),
                    LabelAR = table.Column<string>(nullable: true),
                    LevelID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LSections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LSections_Levels_LevelID",
                        column: x => x.LevelID,
                        principalTable: "Levels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inputs",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LabelAR = table.Column<string>(nullable: true),
                    LabelEN = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    IsRequired = table.Column<bool>(nullable: false),
                    IsLevelDependent = table.Column<bool>(nullable: false),
                    MaxValue = table.Column<int>(nullable: false),
                    MinValue = table.Column<int>(nullable: false),
                    SectionID = table.Column<long>(nullable: false),
                    AttributeID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inputs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inputs_LAttributes_AttributeID",
                        column: x => x.AttributeID,
                        principalTable: "LAttributes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inputs_LSections_SectionID",
                        column: x => x.SectionID,
                        principalTable: "LSections",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LInterests_LevelID",
                table: "LInterests",
                column: "LevelID");

            migrationBuilder.CreateIndex(
                name: "IX_LInterestInputs_InputID",
                table: "LInterestInputs",
                column: "InputID");

            migrationBuilder.CreateIndex(
                name: "IX_LInterestInputs_LInterestID",
                table: "LInterestInputs",
                column: "LInterestID");

            migrationBuilder.CreateIndex(
                name: "IX_Inputs_AttributeID",
                table: "Inputs",
                column: "AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_Inputs_SectionID",
                table: "Inputs",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_LAttributes_ParentAttributeID",
                table: "LAttributes",
                column: "ParentAttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_LSections_LevelID",
                table: "LSections",
                column: "LevelID");

            migrationBuilder.AddForeignKey(
                name: "FK_LInterestInputs_Inputs_InputID",
                table: "LInterestInputs",
                column: "InputID",
                principalTable: "Inputs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LInterestInputs_LInterests_LInterestID",
                table: "LInterestInputs",
                column: "LInterestID",
                principalTable: "LInterests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LInterests_Levels_LevelID",
                table: "LInterests",
                column: "LevelID",
                principalTable: "Levels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LInterestInputs_Inputs_InputID",
                table: "LInterestInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_LInterestInputs_LInterests_LInterestID",
                table: "LInterestInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_LInterests_Levels_LevelID",
                table: "LInterests");

            migrationBuilder.DropTable(
                name: "Inputs");

            migrationBuilder.DropTable(
                name: "LAttributes");

            migrationBuilder.DropTable(
                name: "LSections");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_LInterests_LevelID",
                table: "LInterests");

            migrationBuilder.DropIndex(
                name: "IX_LInterestInputs_InputID",
                table: "LInterestInputs");

            migrationBuilder.DropIndex(
                name: "IX_LInterestInputs_LInterestID",
                table: "LInterestInputs");

            migrationBuilder.DropColumn(
                name: "LevelID",
                table: "LInterests");

            migrationBuilder.DropColumn(
                name: "AttributeID",
                table: "LInterestInputs");

            migrationBuilder.DropColumn(
                name: "InputField",
                table: "LInterestInputs");

            migrationBuilder.DropColumn(
                name: "InputID",
                table: "LInterestInputs");

            migrationBuilder.DropColumn(
                name: "InputType",
                table: "LInterestInputs");

            migrationBuilder.DropColumn(
                name: "LInterestID",
                table: "LInterestInputs");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "LInterests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "LInterests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LabelAR",
                table: "LInterestInputs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LabelEN",
                table: "LInterestInputs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "LInterestInputs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "InterestAttributes",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LabelAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LabelEN = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestAttributes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LInterest_LInterestInputs",
                columns: table => new
                {
                    LInterestID = table.Column<long>(type: "bigint", nullable: false),
                    LInterestInputID = table.Column<long>(type: "bigint", nullable: false),
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LInterest_LInterestInputs", x => new { x.LInterestID, x.LInterestInputID });
                    table.ForeignKey(
                        name: "FK_LInterest_LInterestInputs_LInterests_LInterestID",
                        column: x => x.LInterestID,
                        principalTable: "LInterests",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LInterest_LInterestInputs_LInterestInputs_LInterestInputID",
                        column: x => x.LInterestInputID,
                        principalTable: "LInterestInputs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LInterest_InterestAttributes",
                columns: table => new
                {
                    LInterestID = table.Column<long>(type: "bigint", nullable: false),
                    InterestAttributeID = table.Column<long>(type: "bigint", nullable: false),
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LInterest_InterestAttributes", x => new { x.LInterestID, x.InterestAttributeID });
                    table.ForeignKey(
                        name: "FK_LInterest_InterestAttributes_InterestAttributes_InterestAttributeID",
                        column: x => x.InterestAttributeID,
                        principalTable: "InterestAttributes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LInterest_InterestAttributes_LInterests_LInterestID",
                        column: x => x.LInterestID,
                        principalTable: "LInterests",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LInterest_InterestAttributes_InterestAttributeID",
                table: "LInterest_InterestAttributes",
                column: "InterestAttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_LInterest_LInterestInputs_LInterestInputID",
                table: "LInterest_LInterestInputs",
                column: "LInterestInputID");
        }
    }
}
