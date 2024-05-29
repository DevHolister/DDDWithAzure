using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Linde.Persistence.Coaching.Migrations
{
    public partial class OperetorsPlantsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_PLANT",
                schema: "dbo",
                columns: table => new
                {
                    PlantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Bu = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Division = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    State = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    City = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Municipality = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SuperintendentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlantManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_PLANT", x => x.PlantId);
                    table.ForeignKey(
                        name: "FK_CAT_PLANT_CAT_COUNTRY_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "dbo",
                        principalTable: "CAT_COUNTRY",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_PLANT_CAT_USER_PlantManagerId",
                        column: x => x.PlantManagerId,
                        principalSchema: "dbo",
                        principalTable: "CAT_USER",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_CAT_PLANT_CAT_USER_SuperintendentId",
                        column: x => x.SuperintendentId,
                        principalSchema: "dbo",
                        principalTable: "CAT_USER",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ASOC_ROLE_PLANT",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASOC_ROLE_PLANT", x => new { x.UserId, x.PlantId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ASOC_ROLE_PLANT_CAT_PLANT_PlantId",
                        column: x => x.PlantId,
                        principalSchema: "dbo",
                        principalTable: "CAT_PLANT",
                        principalColumn: "PlantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ASOC_ROLE_PLANT_CAT_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "CAT_ROLE",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ASOC_ROLE_PLANT_CAT_USER_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "CAT_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ASOC_ROLE_PLANT_PlantId",
                schema: "dbo",
                table: "ASOC_ROLE_PLANT",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ASOC_ROLE_PLANT_RoleId",
                schema: "dbo",
                table: "ASOC_ROLE_PLANT",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_PLANT_CountryId",
                schema: "dbo",
                table: "CAT_PLANT",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_PLANT_PlantManagerId",
                schema: "dbo",
                table: "CAT_PLANT",
                column: "PlantManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_PLANT_SuperintendentId",
                schema: "dbo",
                table: "CAT_PLANT",
                column: "SuperintendentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASOC_ROLE_PLANT",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CAT_PLANT",
                schema: "dbo");
        }
    }
}
