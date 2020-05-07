using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NotesKeeper.DataAccess.EntityFramework.Migrations.UserDb
{
    public partial class UpdatedDaysAndEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Days_DayId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_DayId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventLastDay",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventStartDay",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "Events");

            migrationBuilder.AddColumn<bool>(
                name: "AllDay",
                table: "Events",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundColor",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EventLastTime",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EventStartTime",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "EventDay",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EventId = table.Column<Guid>(nullable: false),
                    DayId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventDay_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventDay_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventDay_DayId",
                table: "EventDay",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_EventDay_EventId",
                table: "EventDay",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventDay");

            migrationBuilder.DropColumn(
                name: "AllDay",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "BackgroundColor",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventLastTime",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventStartTime",
                table: "Events");

            migrationBuilder.AddColumn<Guid>(
                name: "DayId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EventLastDay",
                table: "Events",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EventStartDay",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Frequency",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Events_DayId",
                table: "Events",
                column: "DayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Days_DayId",
                table: "Events",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
