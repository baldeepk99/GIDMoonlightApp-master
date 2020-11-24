using Microsoft.EntityFrameworkCore.Migrations;

namespace MoonlightGID.Migrations
{
    public partial class AlterBusinesstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChatLink",
                table: "Businesses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatLink",
                table: "Businesses");
        }
    }
}
