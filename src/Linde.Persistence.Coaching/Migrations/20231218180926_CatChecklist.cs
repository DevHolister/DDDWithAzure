using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Linde.Persistence.Coaching.Migrations
{
    public partial class CatChecklist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_CHECKLISTS",
                schema: "dbo",
                columns: table => new
                {
                    ChecklistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_CHECKLISTS", x => x.ChecklistId);
                });

            migrationBuilder.CreateTable(
                name: "ASOC_CHECKLISTS_QUESTIONS",
                schema: "dbo",
                columns: table => new
                {
                    CatChecklistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASOC_CHECKLISTS_QUESTIONS", x => new { x.CatChecklistId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_ASOC_CHECKLISTS_QUESTIONS_CAT_CHECKLISTS_CatChecklistId",
                        column: x => x.CatChecklistId,
                        principalSchema: "dbo",
                        principalTable: "CAT_CHECKLISTS",
                        principalColumn: "ChecklistId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ASOC_CHECKLISTS_QUESTIONS_CAT_QUESTIONS_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "dbo",
                        principalTable: "CAT_QUESTIONS",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ASOC_CHECKLISTS_QUESTIONS_QuestionId",
                schema: "dbo",
                table: "ASOC_CHECKLISTS_QUESTIONS",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASOC_CHECKLISTS_QUESTIONS",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CAT_CHECKLISTS",
                schema: "dbo");
        }
    }
}
