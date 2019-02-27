using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd_zadatak.Migrations
{
    public partial class DeviceModelChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Devices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Devices");
        }
    }
}
