using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NotesKeeper.DataAccess.EntityFramework.Migrations.UserDb
{
    public partial class UserContextExtendedWithDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DayId",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Days_DayId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropIndex(
                name: "IX_Events_DayId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "Events");
        }
    }
}
