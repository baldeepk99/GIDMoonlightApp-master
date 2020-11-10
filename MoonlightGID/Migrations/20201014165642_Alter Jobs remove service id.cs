using Microsoft.EntityFrameworkCore.Migrations;

namespace MoonlightGID.Migrations
{
    public partial class AlterJobsremoveserviceid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Services_ServiceId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_ServiceId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "ServicesServiceId",
                table: "Jobs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ServicesServiceId",
                table: "Jobs",
                column: "ServicesServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Services_ServicesServiceId",
                table: "Jobs",
                column: "ServicesServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Services_ServicesServiceId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_ServicesServiceId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ServicesServiceId",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ServiceId",
                table: "Jobs",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Services_ServiceId",
                table: "Jobs",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
