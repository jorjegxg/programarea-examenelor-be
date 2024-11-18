using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class MODIFCAREROOMS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamRequests_Rooms_RoomID",
                table: "ExamRequests");

            migrationBuilder.DropIndex(
                name: "IX_ExamRequests_RoomID",
                table: "ExamRequests");

            migrationBuilder.DropColumn(
                name: "RoomID",
                table: "ExamRequests");

            migrationBuilder.CreateTable(
                name: "ExamRequestRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamRequestID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamRequestRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamRequestRooms_ExamRequests_ExamRequestID",
                        column: x => x.ExamRequestID,
                        principalTable: "ExamRequests",
                        principalColumn: "RequestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamRequestRooms_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamRequestRooms_ExamRequestID",
                table: "ExamRequestRooms",
                column: "ExamRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamRequestRooms_RoomID",
                table: "ExamRequestRooms",
                column: "RoomID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamRequestRooms");

            migrationBuilder.AddColumn<int>(
                name: "RoomID",
                table: "ExamRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExamRequests_RoomID",
                table: "ExamRequests",
                column: "RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamRequests_Rooms_RoomID",
                table: "ExamRequests",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "RoomID");
        }
    }
}
