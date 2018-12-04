using Microsoft.EntityFrameworkCore.Migrations;

namespace DronZone_API.Migrations
{
    public partial class RelationsBetweenZoneAndValidationRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZoneValidationRequests_Zones_TargetZoneId",
                table: "ZoneValidationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ZoneValidationRequests_TargetZoneId",
                table: "ZoneValidationRequests");

            migrationBuilder.AlterColumn<string>(
                name: "TargetZoneId",
                table: "ZoneValidationRequests",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZoneId",
                table: "ZoneValidationRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZoneValidationRequests_ZoneId",
                table: "ZoneValidationRequests",
                column: "ZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneValidationRequests_Zones_ZoneId",
                table: "ZoneValidationRequests",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZoneValidationRequests_Zones_ZoneId",
                table: "ZoneValidationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ZoneValidationRequests_ZoneId",
                table: "ZoneValidationRequests");

            migrationBuilder.DropColumn(
                name: "ZoneId",
                table: "ZoneValidationRequests");

            migrationBuilder.AlterColumn<string>(
                name: "TargetZoneId",
                table: "ZoneValidationRequests",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZoneValidationRequests_TargetZoneId",
                table: "ZoneValidationRequests",
                column: "TargetZoneId",
                unique: true,
                filter: "[TargetZoneId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneValidationRequests_Zones_TargetZoneId",
                table: "ZoneValidationRequests",
                column: "TargetZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
