using Microsoft.EntityFrameworkCore.Migrations;

namespace LMSApp.Data.Migrations
{
    public partial class EnumValuesAdde : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Test",
                table: "Assignments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                table: "Assignments");
        }
    }
}
