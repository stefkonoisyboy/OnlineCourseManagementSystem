namespace OnlineCourseManagementSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedLectureToExam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Exams",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "LectureId",
                table: "Exams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_LectureId",
                table: "Exams",
                column: "LectureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Lectures_LectureId",
                table: "Exams",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Lectures_LectureId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_LectureId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "LectureId",
                table: "Exams");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
