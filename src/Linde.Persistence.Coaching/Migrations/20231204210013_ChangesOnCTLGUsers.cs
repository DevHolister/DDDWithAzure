using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Linde.Persistence.Coaching.Migrations
{
    public partial class ChangesOnCTLGUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstSurname",
                schema: "dbo",
                table: "CAT_USER");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "dbo",
                table: "CAT_USER");

            migrationBuilder.DropColumn(
                name: "SecondSurname",
                schema: "dbo",
                table: "CAT_USER");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNumber",
                schema: "dbo",
                table: "CAT_USER",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "dbo",
                table: "CAT_USER",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TypeUser",
                schema: "dbo",
                table: "CAT_USER",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZoneId",
                schema: "dbo",
                table: "CAT_USER",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeNumber",
                schema: "dbo",
                table: "CAT_USER");

            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "dbo",
                table: "CAT_USER");

            migrationBuilder.DropColumn(
                name: "TypeUser",
                schema: "dbo",
                table: "CAT_USER");

            migrationBuilder.DropColumn(
                name: "ZoneId",
                schema: "dbo",
                table: "CAT_USER");

            migrationBuilder.AddColumn<string>(
                name: "FirstSurname",
                schema: "dbo",
                table: "CAT_USER",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "CAT_USER",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondSurname",
                schema: "dbo",
                table: "CAT_USER",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
