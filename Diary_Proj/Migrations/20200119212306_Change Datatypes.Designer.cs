﻿// <auto-generated />
using System;
using Diary_Proj.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Diary_Proj.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200119212306_Change Datatypes")]
    partial class ChangeDatatypes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Diary_Proj.Models.DayNote", b =>
                {
                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PrimaryDayTitle")
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.HasKey("DateTime");

                    b.ToTable("DayNotes");
                });

            modelBuilder.Entity("Diary_Proj.Models.Note", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DayNoteDateTime")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Pic")
                        .HasColumnName("Pic")
                        .HasColumnType("VarBinary(max)");

                    b.Property<TimeSpan?>("StartAt")
                        .HasColumnType("time");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.HasKey("ID");

                    b.HasIndex("DayNoteDateTime");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("Diary_Proj.Models.Note", b =>
                {
                    b.HasOne("Diary_Proj.Models.DayNote", "DayNote")
                        .WithMany("Notes")
                        .HasForeignKey("DayNoteDateTime");
                });
#pragma warning restore 612, 618
        }
    }
}
