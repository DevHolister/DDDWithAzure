using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Linde.Persistence.Coaching.Migrations
{
    public partial class QuestionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_QUESTIONS",
                schema: "dbo",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    IsCritical = table.Column<bool>(type: "bit", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_QUESTIONS", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "ASOC_QUESTIONS_ACTIVITIES",
                schema: "dbo",
                columns: table => new
                {
                    CatQuestionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASOC_QUESTIONS_ACTIVITIES", x => new { x.CatQuestionsId, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_ASOC_QUESTIONS_ACTIVITIES_CAT_ACTIVITIES_ActivityId",
                        column: x => x.ActivityId,
                        principalSchema: "dbo",
                        principalTable: "CAT_ACTIVITIES",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ASOC_QUESTIONS_ACTIVITIES_CAT_QUESTIONS_CatQuestionsId",
                        column: x => x.CatQuestionsId,
                        principalSchema: "dbo",
                        principalTable: "CAT_QUESTIONS",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ASOC_QUESTIONS_ACTIVITIES_ActivityId",
                schema: "dbo",
                table: "ASOC_QUESTIONS_ACTIVITIES",
                column: "ActivityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASOC_QUESTIONS_ACTIVITIES",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CAT_QUESTIONS",
                schema: "dbo");
        }
    }
}
