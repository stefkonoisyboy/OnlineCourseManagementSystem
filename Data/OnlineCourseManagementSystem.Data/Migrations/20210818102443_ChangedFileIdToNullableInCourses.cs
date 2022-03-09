namespace OnlineCourseManagementSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangedFileIdToNullableInCourses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Courses_FileId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_FileId",
                table: "Courses",
                column: "FileId",
                unique: true,
                filter: "[FileId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Courses_FileId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_FileId",
                table: "Courses",
                column: "FileId",
                unique: true);
        }
    }
}
