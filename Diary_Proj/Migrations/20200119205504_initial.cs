using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Diary_Proj.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayNotes",
                columns: table => new
                {
                    DateTime = table.Column<DateTime>(nullable: false),
                    PrimaryDayTitle = table.Column<string>(maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayNotes", x => x.DateTime);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 70, nullable: true),
                    Text = table.Column<string>(nullable: true),
                    StartAt = table.Column<DateTime>(nullable: false),
                    Pic = table.Column<byte[]>(type: "VarBinary(max)", nullable: true),
                    DayNoteDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notes_DayNotes_DayNoteDateTime",
                        column: x => x.DayNoteDateTime,
                        principalTable: "DayNotes",
                        principalColumn: "DateTime",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_DayNoteDateTime",
                table: "Notes",
                column: "DayNoteDateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "DayNotes");
        }
    }
}
