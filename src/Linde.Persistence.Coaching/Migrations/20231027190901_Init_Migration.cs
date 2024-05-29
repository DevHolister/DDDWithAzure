using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Linde.Persistence.Coaching.Migrations
{
    public partial class Init_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "CAT_COUNTRY",
                schema: "dbo",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OriginalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_COUNTRY", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "CAT_PERMISSION",
                schema: "dbo",
                columns: table => new
                {
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Actions = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_PERMISSION", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "CAT_ROLE",
                schema: "dbo",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ROLE", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "CAT_USER",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstSurname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecondSurname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lockout = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_USER", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ASOC_ROLE_PERMISSION",
                schema: "dbo",
                columns: table => new
                {
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASOC_ROLE_PERMISSION", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_ASOC_ROLE_PERMISSION_CAT_PERMISSION_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "dbo",
                        principalTable: "CAT_PERMISSION",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ASOC_ROLE_PERMISSION_CAT_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "CAT_ROLE",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ASOC_USER_COUNTRY",
                schema: "dbo",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASOC_USER_COUNTRY", x => new { x.UserId, x.CountryId });
                    table.ForeignKey(
                        name: "FK_ASOC_USER_COUNTRY_CAT_COUNTRY_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "dbo",
                        principalTable: "CAT_COUNTRY",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ASOC_USER_COUNTRY_CAT_USER_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "CAT_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ASOC_USER_ROLE",
                schema: "dbo",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASOC_USER_ROLE", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ASOC_USER_ROLE_CAT_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "CAT_ROLE",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ASOC_USER_ROLE_CAT_USER_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "CAT_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ASOC_ROLE_PERMISSION_PermissionId",
                schema: "dbo",
                table: "ASOC_ROLE_PERMISSION",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ASOC_USER_COUNTRY_CountryId",
                schema: "dbo",
                table: "ASOC_USER_COUNTRY",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ASOC_USER_ROLE_RoleId",
                schema: "dbo",
                table: "ASOC_USER_ROLE",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_USER_UserName",
                schema: "dbo",
                table: "CAT_USER",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASOC_ROLE_PERMISSION",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ASOC_USER_COUNTRY",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ASOC_USER_ROLE",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CAT_PERMISSION",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CAT_COUNTRY",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CAT_ROLE",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CAT_USER",
                schema: "dbo");
        }
    }
}
