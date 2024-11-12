using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class REMOVVEDATA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Faculties_FacultyID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FacultyID",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_FacultyID",
                table: "Users",
                column: "FacultyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Faculties_FacultyID",
                table: "Users",
                column: "FacultyID",
                principalTable: "Faculties",
                principalColumn: "FacultyID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
