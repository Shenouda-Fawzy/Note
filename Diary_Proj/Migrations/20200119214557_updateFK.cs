using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Diary_Proj.Migrations
{
    public partial class updateFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_DayNotes_DayNoteDateTime",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_DayNoteDateTime",
                table: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DayNotes",
                table: "DayNotes");

            migrationBuilder.DropColumn(
                name: "DayNoteDateTime",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "DayNotes");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Notes",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "DayNotes",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DayNotes",
                table: "DayNotes",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_Date",
                table: "Notes",
                column: "Date");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_DayNotes_Date",
                table: "Notes",
                column: "Date",
                principalTable: "DayNotes",
                principalColumn: "Date",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_DayNotes_Date",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_Date",
                table: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DayNotes",
                table: "DayNotes");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "DayNotes");

            migrationBuilder.AddColumn<DateTime>(
                name: "DayNoteDateTime",
                table: "Notes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "DayNotes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DayNotes",
                table: "DayNotes",
                column: "DateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_DayNoteDateTime",
                table: "Notes",
                column: "DayNoteDateTime");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_DayNotes_DayNoteDateTime",
                table: "Notes",
                column: "DayNoteDateTime",
                principalTable: "DayNotes",
                principalColumn: "DateTime",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
