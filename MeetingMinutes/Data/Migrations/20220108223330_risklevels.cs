using  System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingMinutes.Data.Migrations
{
    public partial class risklevels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedTo",
                table: "MeetingItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ChangeRequested",
                table: "MeetingItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "MeetingItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MeetingItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileAttachment",
                table: "MeetingItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "MeetingItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "MeetingItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestedBy",
                table: "MeetingItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RiskLevelid",
                table: "MeetingItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "VisibleInMinutes",
                table: "MeetingItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "RiskLevel",
                columns: table => new
                {
                    RiskLevelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskLevel", x => x.RiskLevelID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingItem_RiskLevelid",
                table: "MeetingItem",
                column: "RiskLevelid");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingItem_RiskLevel_RiskLevelid",
                table: "MeetingItem",
                column: "RiskLevelid",
                principalTable: "RiskLevel",
                principalColumn: "RiskLevelID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingItem_RiskLevel_RiskLevelid",
                table: "MeetingItem");

            migrationBuilder.DropTable(
                name: "RiskLevel");

            migrationBuilder.DropIndex(
                name: "IX_MeetingItem_RiskLevelid",
                table: "MeetingItem");

            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "MeetingItem");

            migrationBuilder.DropColumn(
                name: "ChangeRequested",
                table: "MeetingItem");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "MeetingItem");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "MeetingItem");

            migrationBuilder.DropColumn(
                name: "FileAttachment",
                table: "MeetingItem");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "MeetingItem");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "MeetingItem");

            migrationBuilder.DropColumn(
                name: "RequestedBy",
                table: "MeetingItem");

            migrationBuilder.DropColumn(
                name: "RiskLevelid",
                table: "MeetingItem");

            migrationBuilder.DropColumn(
                name: "VisibleInMinutes",
                table: "MeetingItem");
        }
    }
}
