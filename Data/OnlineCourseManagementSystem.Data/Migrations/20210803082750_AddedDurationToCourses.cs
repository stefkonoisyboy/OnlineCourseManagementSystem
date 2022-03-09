namespace OnlineCourseManagementSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedDurationToCourses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecommendedDuration",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecommendedDuration",
                table: "Courses");
        }
    }
}
