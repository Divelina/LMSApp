using Microsoft.EntityFrameworkCore.Migrations;

namespace LMSApp.Data.Migrations
{
    public partial class FacultyEnumInStudentsEducators : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacultyName",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FacultyName",
                table: "Educator",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacultyName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FacultyName",
                table: "Educator");
        }
    }
}
