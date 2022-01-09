using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingMinutes.Data.Migrations
{
    public partial class modifydbcontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingItem_Meetings_Meetingid",
                table: "MeetingItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingItem_RiskLevel_RiskLevelid",
                table: "MeetingItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RiskLevel",
                table: "RiskLevel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetingItem",
                table: "MeetingItem");

            migrationBuilder.RenameTable(
                name: "RiskLevel",
                newName: "RiskLevels");

            migrationBuilder.RenameTable(
                name: "MeetingItem",
                newName: "MeetingItems");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingItem_RiskLevelid",
                table: "MeetingItems",
                newName: "IX_MeetingItems_RiskLevelid");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingItem_Meetingid",
                table: "MeetingItems",
                newName: "IX_MeetingItems_Meetingid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RiskLevels",
                table: "RiskLevels",
                column: "RiskLevelID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetingItems",
                table: "MeetingItems",
                column: "MeetingItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingItems_Meetings_Meetingid",
                table: "MeetingItems",
                column: "Meetingid",
                principalTable: "Meetings",
                principalColumn: "MeetingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingItems_RiskLevels_RiskLevelid",
                table: "MeetingItems",
                column: "RiskLevelid",
                principalTable: "RiskLevels",
                principalColumn: "RiskLevelID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingItems_Meetings_Meetingid",
                table: "MeetingItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingItems_RiskLevels_RiskLevelid",
                table: "MeetingItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RiskLevels",
                table: "RiskLevels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetingItems",
                table: "MeetingItems");

            migrationBuilder.RenameTable(
                name: "RiskLevels",
                newName: "RiskLevel");

            migrationBuilder.RenameTable(
                name: "MeetingItems",
                newName: "MeetingItem");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingItems_RiskLevelid",
                table: "MeetingItem",
                newName: "IX_MeetingItem_RiskLevelid");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingItems_Meetingid",
                table: "MeetingItem",
                newName: "IX_MeetingItem_Meetingid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RiskLevel",
                table: "RiskLevel",
                column: "RiskLevelID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetingItem",
                table: "MeetingItem",
                column: "MeetingItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingItem_Meetings_Meetingid",
                table: "MeetingItem",
                column: "Meetingid",
                principalTable: "Meetings",
                principalColumn: "MeetingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingItem_RiskLevel_RiskLevelid",
                table: "MeetingItem",
                column: "RiskLevelid",
                principalTable: "RiskLevel",
                principalColumn: "RiskLevelID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
