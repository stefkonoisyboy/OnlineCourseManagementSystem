namespace OnlineCourseManagementSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedCourseIdToFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Files",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_FileId",
                table: "Courses",
                column: "FileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Files_FileId",
                table: "Courses",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Files_FileId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_FileId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Courses");
        }
    }
}
