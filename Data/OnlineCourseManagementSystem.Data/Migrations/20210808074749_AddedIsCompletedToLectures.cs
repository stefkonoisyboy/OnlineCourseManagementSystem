using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCourseManagementSystem.Data.Migrations
{
    public partial class AddedIsCompletedToLectures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Lectures",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Lectures");
        }
    }
}
