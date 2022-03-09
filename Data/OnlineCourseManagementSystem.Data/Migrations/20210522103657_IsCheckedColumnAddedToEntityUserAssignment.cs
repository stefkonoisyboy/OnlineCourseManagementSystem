namespace OnlineCourseManagementSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class IsCheckedColumnAddedToEntityUserAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "UserAssignments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "UserAssignments");
        }
    }
}
