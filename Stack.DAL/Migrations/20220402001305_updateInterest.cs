using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class updateInterest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Area_LOneInterests");

            migrationBuilder.DropTable(
                name: "Area_Pools");

            migrationBuilder.DropTable(
                name: "LOneInterest_InterestAttributes");

            migrationBuilder.DropTable(
                name: "LOneInterest_LOneInterestInput");

            migrationBuilder.DropTable(
                name: "LThreeInterest_InterestAttributes");

            migrationBuilder.DropTable(
                name: "LThreeInterest_LThreeInterestInputs");

            migrationBuilder.DropTable(
                name: "LTwoInterest_InterestAttributes");

            migrationBuilder.DropTable(
                name: "LTwoInterest_LTwoInterestInputs");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "LOneInterestInputs");

            migrationBuilder.DropTable(
                name: "LThreeInterests");

            migrationBuilder.DropTable(
                name: "LThreeInterestInputs");

            migrationBuilder.DropTable(
                name: "LTwoInterests");

            migrationBuilder.DropTable(
                name: "LTwoInterestInputs");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "LOneInterests");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "InterestAttributes");

            migrationBuilder.AddColumn<string>(
                name: "LabelAR",
                table: "InterestAttributes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LabelEN",
                table: "InterestAttributes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LInterestInputs",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    LabelAR = table.Column<string>(nullable: true),
                    LabelEN = table.Column<string>(nullable: true),
                    Attachment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LInterestInputs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEN = table.Column<string>(nullable: true),
                    NameAR = table.Column<string>(nullable: true),
                    DescriptionEN = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true),
                    LocationType = table.Column<int>(nullable: false),
                    Latitude = table.Column<float>(nullable: false),
                    Longitude = table.Column<float>(nullable: false),
                    ParentLocationID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Locations_Locations_ParentLocationID",
                        column: x => x.ParentLocationID,
                        principalTable: "Locations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LInterests",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionEN = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true),
                    IsSeparate = table.Column<bool>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    LevelType = table.Column<int>(nullable: false),
                    OwnerID = table.Column<long>(nullable: true),
                    LocationID = table.Column<long>(nullable: false),
                    ParentLInterestID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LInterests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LInterests_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LInterests_Customers_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LInterests_LInterests_ParentLInterestID",
                        column: x => x.ParentLInterestID,
                        principalTable: "LInterests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Location_Pools",
                columns: table => new
                {
                    LocationID = table.Column<long>(nullable: false),
                    PoolID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location_Pools", x => new { x.LocationID, x.PoolID });
                    table.ForeignKey(
                        name: "FK_Location_Pools_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Location_Pools_Pools_PoolID",
                        column: x => x.PoolID,
                        principalTable: "Pools",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LInterest_InterestAttributes",
                columns: table => new
                {
                    LInterestID = table.Column<long>(nullable: false),
                    InterestAttributeID = table.Column<long>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "LInterest_LInterestInputs",
                columns: table => new
                {
                    LInterestID = table.Column<long>(nullable: false),
                    LInterestInputID = table.Column<long>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_LInterest_InterestAttributes_InterestAttributeID",
                table: "LInterest_InterestAttributes",
                column: "InterestAttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_LInterest_LInterestInputs_LInterestInputID",
                table: "LInterest_LInterestInputs",
                column: "LInterestInputID");

            migrationBuilder.CreateIndex(
                name: "IX_LInterests_LocationID",
                table: "LInterests",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_LInterests_OwnerID",
                table: "LInterests",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_LInterests_ParentLInterestID",
                table: "LInterests",
                column: "ParentLInterestID");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Pools_PoolID",
                table: "Location_Pools",
                column: "PoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ParentLocationID",
                table: "Locations",
                column: "ParentLocationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LInterest_InterestAttributes");

            migrationBuilder.DropTable(
                name: "LInterest_LInterestInputs");

            migrationBuilder.DropTable(
                name: "Location_Pools");

            migrationBuilder.DropTable(
                name: "LInterests");

            migrationBuilder.DropTable(
                name: "LInterestInputs");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropColumn(
                name: "LabelAR",
                table: "InterestAttributes");

            migrationBuilder.DropColumn(
                name: "LabelEN",
                table: "InterestAttributes");

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "InterestAttributes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LOneInterestInputs",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOneInterestInputs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LOneInterests",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOneInterests", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LThreeInterestInputs",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LThreeInterestInputs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LThreeInterests",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isStandalone = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LThreeInterests", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LTwoInterestInputs",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LTwoInterestInputs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LOneInterest_InterestAttributes",
                columns: table => new
                {
                    LOneInterestID = table.Column<long>(type: "bigint", nullable: false),
                    InterestAttributeID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOneInterest_InterestAttributes", x => new { x.LOneInterestID, x.InterestAttributeID });
                    table.ForeignKey(
                        name: "FK_LOneInterest_InterestAttributes_InterestAttributes_LOneInterestID",
                        column: x => x.LOneInterestID,
                        principalTable: "InterestAttributes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LOneInterest_InterestAttributes_LOneInterests_LOneInterestID",
                        column: x => x.LOneInterestID,
                        principalTable: "LOneInterests",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LOneInterest_LOneInterestInput",
                columns: table => new
                {
                    LOneInterestID = table.Column<long>(type: "bigint", nullable: false),
                    LOneInterestInputID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOneInterest_LOneInterestInput", x => new { x.LOneInterestID, x.LOneInterestInputID });
                    table.ForeignKey(
                        name: "FK_LOneInterest_LOneInterestInput_LOneInterests_LOneInterestID",
                        column: x => x.LOneInterestID,
                        principalTable: "LOneInterests",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LOneInterest_LOneInterestInput_LOneInterestInputs_LOneInterestInputID",
                        column: x => x.LOneInterestInputID,
                        principalTable: "LOneInterestInputs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LTwoInterests",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LOneInterestID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LTwoInterests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LTwoInterests_LOneInterests_LOneInterestID",
                        column: x => x.LOneInterestID,
                        principalTable: "LOneInterests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LThreeInterest_InterestAttributes",
                columns: table => new
                {
                    LThreeInterestID = table.Column<long>(type: "bigint", nullable: false),
                    InterestAttributeID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LThreeInterest_InterestAttributes", x => new { x.LThreeInterestID, x.InterestAttributeID });
                    table.ForeignKey(
                        name: "FK_LThreeInterest_InterestAttributes_InterestAttributes_LThreeInterestID",
                        column: x => x.LThreeInterestID,
                        principalTable: "InterestAttributes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LThreeInterest_InterestAttributes_LThreeInterests_LThreeInterestID",
                        column: x => x.LThreeInterestID,
                        principalTable: "LThreeInterests",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LThreeInterest_LThreeInterestInputs",
                columns: table => new
                {
                    LThreeInterestID = table.Column<long>(type: "bigint", nullable: false),
                    LThreeInterestInputID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LThreeInterest_LThreeInterestInputs", x => new { x.LThreeInterestID, x.LThreeInterestInputID });
                    table.ForeignKey(
                        name: "FK_LThreeInterest_LThreeInterestInputs_LThreeInterests_LThreeInterestID",
                        column: x => x.LThreeInterestID,
                        principalTable: "LThreeInterests",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LThreeInterest_LThreeInterestInputs_LThreeInterestInputs_LThreeInterestInputID",
                        column: x => x.LThreeInterestInputID,
                        principalTable: "LThreeInterestInputs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Areas_Regions_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Regions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LTwoInterest_InterestAttributes",
                columns: table => new
                {
                    LTwoInterestID = table.Column<long>(type: "bigint", nullable: false),
                    InterestAttributeID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LTwoInterest_InterestAttributes", x => new { x.LTwoInterestID, x.InterestAttributeID });
                    table.ForeignKey(
                        name: "FK_LTwoInterest_InterestAttributes_InterestAttributes_LTwoInterestID",
                        column: x => x.LTwoInterestID,
                        principalTable: "InterestAttributes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LTwoInterest_InterestAttributes_LTwoInterests_LTwoInterestID",
                        column: x => x.LTwoInterestID,
                        principalTable: "LTwoInterests",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LTwoInterest_LTwoInterestInputs",
                columns: table => new
                {
                    LTwoInterestID = table.Column<long>(type: "bigint", nullable: false),
                    LTwoInterestInputID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LTwoInterest_LTwoInterestInputs", x => new { x.LTwoInterestID, x.LTwoInterestInputID });
                    table.ForeignKey(
                        name: "FK_LTwoInterest_LTwoInterestInputs_LTwoInterests_LTwoInterestID",
                        column: x => x.LTwoInterestID,
                        principalTable: "LTwoInterests",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LTwoInterest_LTwoInterestInputs_LTwoInterestInputs_LTwoInterestInputID",
                        column: x => x.LTwoInterestInputID,
                        principalTable: "LTwoInterestInputs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Area_LOneInterests",
                columns: table => new
                {
                    AreaID = table.Column<long>(type: "bigint", nullable: false),
                    LOneInterestID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area_LOneInterests", x => new { x.AreaID, x.LOneInterestID });
                    table.ForeignKey(
                        name: "FK_Area_LOneInterests_Areas_AreaID",
                        column: x => x.AreaID,
                        principalTable: "Areas",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Area_LOneInterests_LOneInterests_LOneInterestID",
                        column: x => x.LOneInterestID,
                        principalTable: "LOneInterests",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Area_Pools",
                columns: table => new
                {
                    AreaID = table.Column<long>(type: "bigint", nullable: false),
                    PoolID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area_Pools", x => new { x.AreaID, x.PoolID });
                    table.ForeignKey(
                        name: "FK_Area_Pools_Areas_AreaID",
                        column: x => x.AreaID,
                        principalTable: "Areas",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Area_Pools_Pools_PoolID",
                        column: x => x.PoolID,
                        principalTable: "Pools",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Area_LOneInterests_LOneInterestID",
                table: "Area_LOneInterests",
                column: "LOneInterestID");

            migrationBuilder.CreateIndex(
                name: "IX_Area_Pools_PoolID",
                table: "Area_Pools",
                column: "PoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_RegionID",
                table: "Areas",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_LOneInterest_LOneInterestInput_LOneInterestInputID",
                table: "LOneInterest_LOneInterestInput",
                column: "LOneInterestInputID");

            migrationBuilder.CreateIndex(
                name: "IX_LThreeInterest_LThreeInterestInputs_LThreeInterestInputID",
                table: "LThreeInterest_LThreeInterestInputs",
                column: "LThreeInterestInputID");

            migrationBuilder.CreateIndex(
                name: "IX_LTwoInterest_LTwoInterestInputs_LTwoInterestInputID",
                table: "LTwoInterest_LTwoInterestInputs",
                column: "LTwoInterestInputID");

            migrationBuilder.CreateIndex(
                name: "IX_LTwoInterests_LOneInterestID",
                table: "LTwoInterests",
                column: "LOneInterestID");
        }
    }
}
