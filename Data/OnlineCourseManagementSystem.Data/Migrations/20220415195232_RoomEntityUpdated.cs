using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCourseManagementSystem.Data.Migrations
{
    public partial class RoomEntityUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Rooms",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCreator",
                table: "RoomParticipants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CreatorId",
                table: "Rooms",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_AspNetUsers_CreatorId",
                table: "Rooms",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_AspNetUsers_CreatorId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_CreatorId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "IsCreator",
                table: "RoomParticipants");
        }
    }
}
