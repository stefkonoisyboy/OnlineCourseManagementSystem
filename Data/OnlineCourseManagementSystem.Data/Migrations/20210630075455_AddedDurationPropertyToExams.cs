namespace OnlineCourseManagementSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedDurationPropertyToExams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Exams");
        }
    }
}
