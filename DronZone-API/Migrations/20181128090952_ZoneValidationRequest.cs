﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DronZone_API.Migrations
{
    public partial class ZoneValidationRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapRectangles_RegisteredZones_ZoneId",
                table: "MapRectangles");

            migrationBuilder.DropForeignKey(
                name: "FK_RegisteredZones_People_PersonId",
                table: "RegisteredZones");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneSettingsSet_RegisteredZones_ZoneId",
                table: "ZoneSettingsSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegisteredZones",
                table: "RegisteredZones");

            migrationBuilder.RenameTable(
                name: "RegisteredZones",
                newName: "Zones");

            migrationBuilder.RenameIndex(
                name: "IX_RegisteredZones_PersonId",
                table: "Zones",
                newName: "IX_Zones_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zones",
                table: "Zones",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ZoneValidationRequests",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    AdministratorId = table.Column<string>(nullable: true),
                    ZoneId = table.Column<string>(nullable: false),
                    RequestType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneValidationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZoneValidationRequests_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZoneValidationRequests_ZoneId",
                table: "ZoneValidationRequests",
                column: "ZoneId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MapRectangles_Zones_ZoneId",
                table: "MapRectangles",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zones_People_PersonId",
                table: "Zones",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneSettingsSet_Zones_ZoneId",
                table: "ZoneSettingsSet",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapRectangles_Zones_ZoneId",
                table: "MapRectangles");

            migrationBuilder.DropForeignKey(
                name: "FK_Zones_People_PersonId",
                table: "Zones");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneSettingsSet_Zones_ZoneId",
                table: "ZoneSettingsSet");

            migrationBuilder.DropTable(
                name: "ZoneValidationRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zones",
                table: "Zones");

            migrationBuilder.RenameTable(
                name: "Zones",
                newName: "RegisteredZones");

            migrationBuilder.RenameIndex(
                name: "IX_Zones_PersonId",
                table: "RegisteredZones",
                newName: "IX_RegisteredZones_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegisteredZones",
                table: "RegisteredZones",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MapRectangles_RegisteredZones_ZoneId",
                table: "MapRectangles",
                column: "ZoneId",
                principalTable: "RegisteredZones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegisteredZones_People_PersonId",
                table: "RegisteredZones",
                column: "PersonId",
                principalTable: "People",
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
    }
}
