using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class AdaugareShortName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Faculties",
                newName: "ShortName");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentID",
                table: "Professors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "LongName",
                table: "Faculties",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongName",
                table: "Faculties");

            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "Faculties",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentID",
                table: "Professors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
