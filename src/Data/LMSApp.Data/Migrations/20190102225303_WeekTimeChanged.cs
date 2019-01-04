using Microsoft.EntityFrameworkCore.Migrations;

namespace LMSApp.Data.Migrations
{
    public partial class WeekTimeChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StartHour",
                table: "WeekTimes",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "EndHour",
                table: "WeekTimes",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StartHour",
                table: "WeekTimes",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "EndHour",
                table: "WeekTimes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
