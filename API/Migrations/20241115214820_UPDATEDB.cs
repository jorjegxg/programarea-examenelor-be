using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class UPDATEDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabHolders_Professors_ProfessorProfID",
                table: "LabHolders");

            migrationBuilder.DropIndex(
                name: "IX_LabHolders_ProfessorProfID",
                table: "LabHolders");

            migrationBuilder.DropColumn(
                name: "ProfessorProfID",
                table: "LabHolders");

            migrationBuilder.CreateIndex(
                name: "IX_LabHolders_ProfID",
                table: "LabHolders",
                column: "ProfID");

            migrationBuilder.AddForeignKey(
                name: "FK_LabHolders_Professors_ProfID",
                table: "LabHolders",
                column: "ProfID",
                principalTable: "Professors",
                principalColumn: "ProfID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabHolders_Professors_ProfID",
                table: "LabHolders");

            migrationBuilder.DropIndex(
                name: "IX_LabHolders_ProfID",
                table: "LabHolders");

            migrationBuilder.AddColumn<int>(
                name: "ProfessorProfID",
                table: "LabHolders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabHolders_ProfessorProfID",
                table: "LabHolders",
                column: "ProfessorProfID");

            migrationBuilder.AddForeignKey(
                name: "FK_LabHolders_Professors_ProfessorProfID",
                table: "LabHolders",
                column: "ProfessorProfID",
                principalTable: "Professors",
                principalColumn: "ProfID");
        }
    }
}
