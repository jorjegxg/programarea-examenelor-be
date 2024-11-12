using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class InitialMigraiton : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    FacultyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.FacultyID);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                    table.ForeignKey(
                        name: "FK_Departments_Faculties_FacultyID",
                        column: x => x.FacultyID,
                        principalTable: "Faculties",
                        principalColumn: "FacultyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    SpecializationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.SpecializationID);
                    table.ForeignKey(
                        name: "FK_Specializations_Faculties_FacultyID",
                        column: x => x.FacultyID,
                        principalTable: "Faculties",
                        principalColumn: "FacultyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyID = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Faculties_FacultyID",
                        column: x => x.FacultyID,
                        principalTable: "Faculties",
                        principalColumn: "FacultyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_Rooms_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubSpecializations",
                columns: table => new
                {
                    SubSpecializationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecializationID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSpecializations", x => x.SubSpecializationID);
                    table.ForeignKey(
                        name: "FK_SubSpecializations_Specializations_SpecializationID",
                        column: x => x.SpecializationID,
                        principalTable: "Specializations",
                        principalColumn: "SpecializationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modifications",
                columns: table => new
                {
                    ModificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Table = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modifications", x => x.ModificationID);
                    table.ForeignKey(
                        name: "FK_Modifications_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    ProfID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.ProfID);
                    table.ForeignKey(
                        name: "FK_Professors_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID");
                    table.ForeignKey(
                        name: "FK_Professors_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Secretaries",
                columns: table => new
                {
                    SecretaryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secretaries", x => x.SecretaryID);
                    table.ForeignKey(
                        name: "FK_Secretaries_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecializationID = table.Column<int>(type: "int", nullable: false),
                    SubSpecializationID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupID);
                    table.ForeignKey(
                        name: "FK_Groups_Specializations_SpecializationID",
                        column: x => x.SpecializationID,
                        principalTable: "Specializations",
                        principalColumn: "SpecializationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Groups_SubSpecializations_SubSpecializationID",
                        column: x => x.SubSpecializationID,
                        principalTable: "SubSpecializations",
                        principalColumn: "SubSpecializationID");
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseHolderID = table.Column<int>(type: "int", nullable: false),
                    SpecializationID = table.Column<int>(type: "int", nullable: false),
                    SubSpecializationID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_Courses_Professors_CourseHolderID",
                        column: x => x.CourseHolderID,
                        principalTable: "Professors",
                        principalColumn: "ProfID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Specializations_SpecializationID",
                        column: x => x.SpecializationID,
                        principalTable: "Specializations",
                        principalColumn: "SpecializationID");
                    table.ForeignKey(
                        name: "FK_Courses_SubSpecializations_SubSpecializationID",
                        column: x => x.SubSpecializationID,
                        principalTable: "SubSpecializations",
                        principalColumn: "SubSpecializationID");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Students_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "ExamRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    AssistantID = table.Column<int>(type: "int", nullable: false),
                    SessionID = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeStart = table.Column<TimeSpan>(type: "time", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamRequests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_ExamRequests_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamRequests_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "GroupID");
                    table.ForeignKey(
                        name: "FK_ExamRequests_Professors_AssistantID",
                        column: x => x.AssistantID,
                        principalTable: "Professors",
                        principalColumn: "ProfID");
                    table.ForeignKey(
                        name: "FK_ExamRequests_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID");
                    table.ForeignKey(
                        name: "FK_ExamRequests_Sessions_SessionID",
                        column: x => x.SessionID,
                        principalTable: "Sessions",
                        principalColumn: "SessionID");
                });

            migrationBuilder.CreateTable(
                name: "LabHolders",
                columns: table => new
                {
                    LabId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfessorProfID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabHolders", x => x.LabId);
                    table.ForeignKey(
                        name: "FK_LabHolders_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabHolders_Professors_ProfessorProfID",
                        column: x => x.ProfessorProfID,
                        principalTable: "Professors",
                        principalColumn: "ProfID");
                });

            migrationBuilder.CreateTable(
                name: "roomRequesteds",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExamRequestRequestID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roomRequesteds", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_roomRequesteds_ExamRequests_ExamRequestRequestID",
                        column: x => x.ExamRequestRequestID,
                        principalTable: "ExamRequests",
                        principalColumn: "RequestID");
                    table.ForeignKey(
                        name: "FK_roomRequesteds_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseHolderID",
                table: "Courses",
                column: "CourseHolderID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SpecializationID",
                table: "Courses",
                column: "SpecializationID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SubSpecializationID",
                table: "Courses",
                column: "SubSpecializationID");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FacultyID",
                table: "Departments",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamRequests_AssistantID",
                table: "ExamRequests",
                column: "AssistantID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamRequests_CourseID",
                table: "ExamRequests",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamRequests_GroupID",
                table: "ExamRequests",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamRequests_RoomID",
                table: "ExamRequests",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamRequests_SessionID",
                table: "ExamRequests",
                column: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_SpecializationID",
                table: "Groups",
                column: "SpecializationID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_SubSpecializationID",
                table: "Groups",
                column: "SubSpecializationID");

            migrationBuilder.CreateIndex(
                name: "IX_LabHolders_CourseID",
                table: "LabHolders",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_LabHolders_ProfessorProfID",
                table: "LabHolders",
                column: "ProfessorProfID");

            migrationBuilder.CreateIndex(
                name: "IX_Modifications_UserID",
                table: "Modifications",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_DepartmentID",
                table: "Professors",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_UserID",
                table: "Professors",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_roomRequesteds_ExamRequestRequestID",
                table: "roomRequesteds",
                column: "ExamRequestRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_roomRequesteds_RoomID",
                table: "roomRequesteds",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_DepartmentID",
                table: "Rooms",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Secretaries_UserID",
                table: "Secretaries",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_FacultyID",
                table: "Specializations",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupID",
                table: "Students",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserID",
                table: "Students",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_SubSpecializations_SpecializationID",
                table: "SubSpecializations",
                column: "SpecializationID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FacultyID",
                table: "Users",
                column: "FacultyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabHolders");

            migrationBuilder.DropTable(
                name: "Modifications");

            migrationBuilder.DropTable(
                name: "roomRequesteds");

            migrationBuilder.DropTable(
                name: "Secretaries");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "ExamRequests");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Professors");

            migrationBuilder.DropTable(
                name: "SubSpecializations");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
