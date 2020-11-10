using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoonlightGID.Migrations
{
    public partial class UpdatedServicetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Services");

            migrationBuilder.AlterColumn<string>(
                name: "DateOrder",
                table: "Services",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceLocation",
                table: "Services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeDescription",
                table: "Services",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceLocation",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "TimeDescription",
                table: "Services");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOrder",
                table: "Services",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
