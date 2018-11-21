using Microsoft.EntityFrameworkCore.Migrations;

namespace DronZone_API.Migrations
{
    public partial class MapRectangleRenamings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "West",
                table: "MapRectangles",
                newName: "TopLeftLongitude");

            migrationBuilder.RenameColumn(
                name: "South",
                table: "MapRectangles",
                newName: "TopLeftLatitude");

            migrationBuilder.RenameColumn(
                name: "North",
                table: "MapRectangles",
                newName: "BottomRightLongitude");

            migrationBuilder.RenameColumn(
                name: "East",
                table: "MapRectangles",
                newName: "BottomRightLatitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TopLeftLongitude",
                table: "MapRectangles",
                newName: "West");

            migrationBuilder.RenameColumn(
                name: "TopLeftLatitude",
                table: "MapRectangles",
                newName: "South");

            migrationBuilder.RenameColumn(
                name: "BottomRightLongitude",
                table: "MapRectangles",
                newName: "North");

            migrationBuilder.RenameColumn(
                name: "BottomRightLatitude",
                table: "MapRectangles",
                newName: "East");
        }
    }
}
