using Microsoft.EntityFrameworkCore.Migrations;

namespace MoonlightGID.Migrations
{
    public partial class AlterBusinessJobstoaddservicechargeandbookingfee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Services",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "BookingFee",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "ServiceCharge",
                table: "Businesses");

            migrationBuilder.AddColumn<string>(
                name: "BookingFee",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceCharge",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Services_ServiceId",
                table: "Jobs",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Services_ServiceId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "BookingFee",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ServiceCharge",
                table: "Jobs");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Jobs",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "BookingFee",
                table: "Businesses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceCharge",
                table: "Businesses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Services",
                table: "Jobs",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
