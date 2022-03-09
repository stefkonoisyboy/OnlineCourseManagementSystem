namespace OnlineCourseManagementSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedCreatorToExams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Exams",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CreatorId",
                table: "Exams",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_AspNetUsers_CreatorId",
                table: "Exams",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_AspNetUsers_CreatorId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_CreatorId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Exams");
        }
    }
}
