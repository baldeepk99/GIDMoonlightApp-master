using Microsoft.EntityFrameworkCore.Migrations;

namespace MoonlightGID.Migrations
{
    public partial class AlterJobsremoveserviceidupdateservicetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Services",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Services",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Services_JobId",
                table: "Services",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Jobs_JobId",
                table: "Services",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Jobs_JobId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_JobId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Services");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Services",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<int>(
                name: "ServicesServiceId",
                table: "Jobs",
                type: "int",
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
    }
}
