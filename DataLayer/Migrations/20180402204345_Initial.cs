using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "DronePositionSnapshots",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Altitude = table.Column<string>(nullable: true),
                    DroneId = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    Radius = table.Column<double>(nullable: false),
                    When = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DronePositionSnapshots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MapZones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Altitude = table.Column<string>(nullable: true),
                    FigureType = table.Column<int>(nullable: false),
                    Longitude = table.Column<string>(nullable: true),
                    Size = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapZones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drones",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CharacteristicsId = table.Column<int>(nullable: true),
                    OwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drones_DroneCharacteristicsSet_CharacteristicsId",
                        column: x => x.CharacteristicsId,
                        principalTable: "DroneCharacteristicsSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Drones_People_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZoneSettingsSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneSettingsSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZoneSettingsSet_People_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredZones",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    MapZoneId = table.Column<int>(nullable: true),
                    OwnerId = table.Column<string>(nullable: true),
                    PersonId = table.Column<string>(nullable: true),
                    SettingsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredZones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegisteredZones_MapZones_MapZoneId",
                        column: x => x.MapZoneId,
                        principalTable: "MapZones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisteredZones_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisteredZones_ZoneSettingsSet_SettingsId",
                        column: x => x.SettingsId,
                        principalTable: "ZoneSettingsSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drones_CharacteristicsId",
                table: "Drones",
                column: "CharacteristicsId");

            migrationBuilder.CreateIndex(
                name: "IX_Drones_OwnerId",
                table: "Drones",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredZones_MapZoneId",
                table: "RegisteredZones",
                column: "MapZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredZones_PersonId",
                table: "RegisteredZones",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredZones_SettingsId",
                table: "RegisteredZones",
                column: "SettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneSettingsSet_OwnerId",
                table: "ZoneSettingsSet",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DronePositionSnapshots");

            migrationBuilder.DropTable(
                name: "Drones");

            migrationBuilder.DropTable(
                name: "RegisteredZones");

            migrationBuilder.DropTable(
                name: "DroneCharacteristicsSet");

            migrationBuilder.DropTable(
                name: "MapZones");

            migrationBuilder.DropTable(
                name: "ZoneSettingsSet");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
