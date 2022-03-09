namespace OnlineCourseManagementSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedIsArchivedToMessageQA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "MessageQAs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "MessageQAs");
        }
    }
}
