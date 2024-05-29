using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Linde.Persistence.Coaching.Migrations
{
    public partial class ModifyUserPlant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASOC_ROLE_PLANT",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "ASOC_USERS_PLANTS",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isOperator = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASOC_USERS_PLANTS", x => new { x.UserId, x.PlantId });
                    table.ForeignKey(
                        name: "FK_ASOC_USERS_PLANTS_CAT_PLANT_PlantId",
                        column: x => x.PlantId,
                        principalSchema: "dbo",
                        principalTable: "CAT_PLANT",
                        principalColumn: "PlantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ASOC_USERS_PLANTS_CAT_USER_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "CAT_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ASOC_USERS_PLANTS_PlantId",
                schema: "dbo",
                table: "ASOC_USERS_PLANTS",
                column: "PlantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASOC_USERS_PLANTS",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "ASOC_ROLE_PLANT",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
        }
    }
}
