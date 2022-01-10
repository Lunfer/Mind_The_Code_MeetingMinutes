using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingMinutes.Migrations
{
    public partial class fks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MeetingId",
                table: "MeetingParticipant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MeetingParticipant",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingParticipant_MeetingId",
                table: "MeetingParticipant",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingParticipant_UserId",
                table: "MeetingParticipant",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingParticipant_AspNetUsers_UserId",
                table: "MeetingParticipant",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingParticipant_Meeting_MeetingId",
                table: "MeetingParticipant",
                column: "MeetingId",
                principalTable: "Meeting",
                principalColumn: "MeetingID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingParticipant_AspNetUsers_UserId",
                table: "MeetingParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingParticipant_Meeting_MeetingId",
                table: "MeetingParticipant");

            migrationBuilder.DropIndex(
                name: "IX_MeetingParticipant_MeetingId",
                table: "MeetingParticipant");

            migrationBuilder.DropIndex(
                name: "IX_MeetingParticipant_UserId",
                table: "MeetingParticipant");

            migrationBuilder.DropColumn(
                name: "MeetingId",
                table: "MeetingParticipant");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MeetingParticipant");
        }
    }
}
