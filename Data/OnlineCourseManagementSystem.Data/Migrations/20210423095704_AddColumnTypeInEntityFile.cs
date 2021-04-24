using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCourseManagementSystem.Data.Migrations
{
    public partial class AddColumnTypeInEntityFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Files",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Files");
        }
    }
}
