using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class UpdatedModelStructureTooMuch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drones_DroneCharacteristicsSet_CharacteristicsId",
                table: "Drones");

            migrationBuilder.DropForeignKey(
                name: "FK_RegisteredZones_ZoneSettingsSet_SettingsId",
                table: "RegisteredZones");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneSettingsSet_People_OwnerId",
                table: "ZoneSettingsSet");

            migrationBuilder.DropTable(
                name: "DroneCharacteristicsSet");

            migrationBuilder.DropIndex(
                name: "IX_ZoneSettingsSet_OwnerId",
                table: "ZoneSettingsSet");

            migrationBuilder.DropIndex(
                name: "IX_RegisteredZones_SettingsId",
                table: "RegisteredZones");

            migrationBuilder.DropIndex(
                name: "IX_Drones_CharacteristicsId",
                table: "Drones");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ZoneSettingsSet");

            migrationBuilder.DropColumn(
                name: "SettingsId",
                table: "RegisteredZones");

            migrationBuilder.DropColumn(
                name: "CharacteristicsId",
                table: "Drones");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ZoneSettingsSet",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ZoneId",
                table: "ZoneSettingsSet",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "RegisteredZones",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "People",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "MapRectangles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "AttachedDateTime",
                table: "Drones",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Drones",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Drones",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "MaxAvailableWeigth",
                table: "Drones",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MaxSpeed",
                table: "Drones",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Drones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Weigth",
                table: "Drones",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "DroneId",
                table: "DronePositionSnapshots",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "DronePositionSnapshots",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "DroneFilters",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_ZoneSettingsSet_ZoneId",
                table: "ZoneSettingsSet",
                column: "ZoneId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drones_Code",
                table: "Drones",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DronePositionSnapshots_DroneId",
                table: "DronePositionSnapshots",
                column: "DroneId");

            migrationBuilder.AddForeignKey(
                name: "FK_DronePositionSnapshots_Drones_DroneId",
                table: "DronePositionSnapshots",
                column: "DroneId",
                principalTable: "Drones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneSettingsSet_RegisteredZones_ZoneId",
                table: "ZoneSettingsSet",
                column: "ZoneId",
                principalTable: "RegisteredZones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DronePositionSnapshots_Drones_DroneId",
                table: "DronePositionSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneSettingsSet_RegisteredZones_ZoneId",
                table: "ZoneSettingsSet");

            migrationBuilder.DropIndex(
                name: "IX_ZoneSettingsSet_ZoneId",
                table: "ZoneSettingsSet");

            migrationBuilder.DropIndex(
                name: "IX_Drones_Code",
                table: "Drones");

            migrationBuilder.DropIndex(
                name: "IX_DronePositionSnapshots_DroneId",
                table: "DronePositionSnapshots");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ZoneSettingsSet");

            migrationBuilder.DropColumn(
                name: "ZoneId",
                table: "ZoneSettingsSet");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "RegisteredZones");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "MapRectangles");

            migrationBuilder.DropColumn(
                name: "AttachedDateTime",
                table: "Drones");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Drones");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Drones");

            migrationBuilder.DropColumn(
                name: "MaxAvailableWeigth",
                table: "Drones");

            migrationBuilder.DropColumn(
                name: "MaxSpeed",
                table: "Drones");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Drones");

            migrationBuilder.DropColumn(
                name: "Weigth",
                table: "Drones");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "DronePositionSnapshots");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "DroneFilters");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "ZoneSettingsSet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SettingsId",
                table: "RegisteredZones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CharacteristicsId",
                table: "Drones",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DroneId",
                table: "DronePositionSnapshots",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DroneCharacteristicsSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaxAvailableWeigth = table.Column<double>(nullable: false),
                    MaxSpeed = table.Column<double>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Weigth = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DroneCharacteristicsSet", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZoneSettingsSet_OwnerId",
                table: "ZoneSettingsSet",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredZones_SettingsId",
                table: "RegisteredZones",
                column: "SettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Drones_CharacteristicsId",
                table: "Drones",
                column: "CharacteristicsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drones_DroneCharacteristicsSet_CharacteristicsId",
                table: "Drones",
                column: "CharacteristicsId",
                principalTable: "DroneCharacteristicsSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegisteredZones_ZoneSettingsSet_SettingsId",
                table: "RegisteredZones",
                column: "SettingsId",
                principalTable: "ZoneSettingsSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneSettingsSet_People_OwnerId",
                table: "ZoneSettingsSet",
                column: "OwnerId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
