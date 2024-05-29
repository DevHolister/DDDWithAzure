using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Linde.Persistence.Coaching.Migrations
{
    public partial class MenuMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessType",
                schema: "dbo",
                table: "CAT_USER",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TBL_MENUS",
                schema: "dbo",
                columns: table => new
                {
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContainsChildren = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_MENUS", x => x.MenuId);
                    table.ForeignKey(
                        name: "FK_TBL_MENUS_CAT_PERMISSION_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "dbo",
                        principalTable: "CAT_PERMISSION",
                        principalColumn: "PermissionId");
                    table.ForeignKey(
                        name: "FK_TBL_MENUS_TBL_MENUS_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "dbo",
                        principalTable: "TBL_MENUS",
                        principalColumn: "MenuId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_MENUS_ParentId",
                schema: "dbo",
                table: "TBL_MENUS",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_MENUS_PermissionId",
                schema: "dbo",
                table: "TBL_MENUS",
                column: "PermissionId",
                unique: true,
                filter: "[PermissionId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_MENUS",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "AccessType",
                schema: "dbo",
                table: "CAT_USER");
        }
    }
}
