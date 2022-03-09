namespace OnlineCourseManagementSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedCreatorToChannel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Channels",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Channels_CreatorId",
                table: "Channels",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_AspNetUsers_CreatorId",
                table: "Channels",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_AspNetUsers_CreatorId",
                table: "Channels");

            migrationBuilder.DropIndex(
                name: "IX_Channels_CreatorId",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Channels");
        }
    }
}
