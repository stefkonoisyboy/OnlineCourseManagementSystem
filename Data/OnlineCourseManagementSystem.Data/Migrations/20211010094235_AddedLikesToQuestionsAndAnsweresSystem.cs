namespace OnlineCourseManagementSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedLikesToQuestionsAndAnsweresSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessageQAId",
                table: "Likes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_MessageQAId",
                table: "Likes",
                column: "MessageQAId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_MessageQAs_MessageQAId",
                table: "Likes",
                column: "MessageQAId",
                principalTable: "MessageQAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_MessageQAs_MessageQAId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_MessageQAId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "MessageQAId",
                table: "Likes");
        }
    }
}
