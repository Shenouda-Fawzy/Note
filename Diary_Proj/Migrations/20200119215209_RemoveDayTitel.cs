using Microsoft.EntityFrameworkCore.Migrations;

namespace Diary_Proj.Migrations
{
    public partial class RemoveDayTitel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimaryDayTitle",
                table: "DayNotes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrimaryDayTitle",
                table: "DayNotes",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true);
        }
    }
}
