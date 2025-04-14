using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotionBack.Migrations
{
    /// <inheritdoc />
    public partial class FixTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Label",
                table: "ListContents");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ListContents");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "JustPageContents");

            migrationBuilder.DropColumn(
                name: "Untitled",
                table: "CalendarContents");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "ListContents",
                newName: "Color");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tables",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Pages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Pages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Lists",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "GalleryContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "GalleryContents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GalleryContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "GalleryContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Galleries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Calendars",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDt",
                table: "Calendars",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "CalendarContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Boards",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Lists");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "GalleryContents");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "GalleryContents");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "GalleryContents");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "GalleryContents");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "DeleteDt",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "CalendarContents");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Boards");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "ListContents",
                newName: "Text");

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "ListContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ListContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "JustPageContents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Untitled",
                table: "CalendarContents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
