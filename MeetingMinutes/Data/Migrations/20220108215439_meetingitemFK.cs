using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingMinutes.Data.Migrations
{
    public partial class meetingitemFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeetingItem",
                columns: table => new
                {
                    MeetingItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Meetingid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingItem", x => x.MeetingItemID);
                    table.ForeignKey(
                        name: "FK_MeetingItem_Meetings_Meetingid",
                        column: x => x.Meetingid,
                        principalTable: "Meetings",
                        principalColumn: "MeetingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingItem_Meetingid",
                table: "MeetingItem",
                column: "Meetingid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetingItem");
        }
    }
}
