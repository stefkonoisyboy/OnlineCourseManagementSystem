namespace OnlineCourseManagementSystem.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ManyToManyCoursesLecturers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseLecturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    LecturerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLecturers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseLecturers_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseLecturers_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseLecturers_CourseId",
                table: "CourseLecturers",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseLecturers_IsDeleted",
                table: "CourseLecturers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CourseLecturers_LecturerId",
                table: "CourseLecturers",
                column: "LecturerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseLecturers");
        }
    }
}
