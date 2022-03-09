namespace OnlineCourseManagementSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedCreatorToLectures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Lectures",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_CreatorId",
                table: "Lectures",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_AspNetUsers_CreatorId",
                table: "Lectures",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_AspNetUsers_CreatorId",
                table: "Lectures");

            migrationBuilder.DropIndex(
                name: "IX_Lectures_CreatorId",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Lectures");
        }
    }
}
