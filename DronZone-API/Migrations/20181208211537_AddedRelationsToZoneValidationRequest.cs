using Microsoft.EntityFrameworkCore.Migrations;

namespace DronZone_API.Migrations
{
    public partial class AddedRelationsToZoneValidationRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TargetZoneId",
                table: "ZoneValidationRequests",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResponsiblePersonId",
                table: "ZoneValidationRequests",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequesterId",
                table: "ZoneValidationRequests",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZoneValidationRequests_RequesterId",
                table: "ZoneValidationRequests",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneValidationRequests_ResponsiblePersonId",
                table: "ZoneValidationRequests",
                column: "ResponsiblePersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneValidationRequests_TargetZoneId",
                table: "ZoneValidationRequests",
                column: "TargetZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneValidationRequests_People_RequesterId",
                table: "ZoneValidationRequests",
                column: "RequesterId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneValidationRequests_People_ResponsiblePersonId",
                table: "ZoneValidationRequests",
                column: "ResponsiblePersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneValidationRequests_Zones_TargetZoneId",
                table: "ZoneValidationRequests",
                column: "TargetZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZoneValidationRequests_People_RequesterId",
                table: "ZoneValidationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneValidationRequests_People_ResponsiblePersonId",
                table: "ZoneValidationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneValidationRequests_Zones_TargetZoneId",
                table: "ZoneValidationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ZoneValidationRequests_RequesterId",
                table: "ZoneValidationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ZoneValidationRequests_ResponsiblePersonId",
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

            migrationBuilder.AlterColumn<string>(
                name: "ResponsiblePersonId",
                table: "ZoneValidationRequests",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequesterId",
                table: "ZoneValidationRequests",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
