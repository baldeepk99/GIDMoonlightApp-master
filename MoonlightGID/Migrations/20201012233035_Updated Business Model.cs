using Microsoft.EntityFrameworkCore.Migrations;

namespace MoonlightGID.Migrations
{
    public partial class UpdatedBusinessModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookingFee",
                table: "Businesses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Businesses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfficeHours",
                table: "Businesses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceCharge",
                table: "Businesses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkingDays",
                table: "Businesses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingFee",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "OfficeHours",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "ServiceCharge",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "WorkingDays",
                table: "Businesses");
        }
    }
}
