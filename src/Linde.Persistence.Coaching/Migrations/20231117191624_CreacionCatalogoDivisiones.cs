using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Linde.Persistence.Coaching.Migrations
{
    public partial class CreacionCatalogoDivisiones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "CAT_DIVISIONS",
               schema: "dbo",
               columns: table => new
               {
                   DivisionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                   Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                   Visible = table.Column<bool>(type: "bit", nullable: false),
                   CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                   ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                   CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                   ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_CAT_DIVISION", x => x.DivisionId);
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
