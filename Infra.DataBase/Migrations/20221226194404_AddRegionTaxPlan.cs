using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddRegionTaxPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Minutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegionTax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginRegionId = table.Column<int>(type: "int", nullable: false),
                    DestinyRegionId = table.Column<int>(type: "int", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionTax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegionTax_Region_DestinyRegionId",
                        column: x => x.DestinyRegionId,
                        principalTable: "Region",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegionTax_Region_OriginRegionId",
                        column: x => x.OriginRegionId,
                        principalTable: "Region",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegionTax_DestinyRegionId",
                table: "RegionTax",
                column: "DestinyRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionTax_OriginRegionId",
                table: "RegionTax",
                column: "OriginRegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plan");

            migrationBuilder.DropTable(
                name: "RegionTax");

            migrationBuilder.DropTable(
                name: "Region");
        }
    }
}
