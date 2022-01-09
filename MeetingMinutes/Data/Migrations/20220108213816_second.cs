using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingMinutes.Data.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeetingParticipants",
                columns: table => new
                {
                    MeetingParticipantsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingParticipants", x => x.MeetingParticipantsID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetingParticipants");
        }
    }
}
