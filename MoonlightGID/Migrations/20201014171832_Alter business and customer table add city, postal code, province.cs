using Microsoft.EntityFrameworkCore.Migrations;

namespace MoonlightGID.Migrations
{
    public partial class Alterbusinessandcustomertableaddcitypostalcodeprovince : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityAddress",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CompanyAddress",
                table: "Businesses");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Businesses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Businesses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Businesses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Businesses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Businesses");

            migrationBuilder.AddColumn<string>(
                name: "CityAddress",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyAddress",
                table: "Businesses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
