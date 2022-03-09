namespace OnlineCourseManagementSystem.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedVideoRoomsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoRoomId",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VideoRooms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LectureId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoRooms_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VideoRooms_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoRooms_CreatorId",
                table: "VideoRooms",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoRooms_IsDeleted",
                table: "VideoRooms",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_VideoRooms_LectureId",
                table: "VideoRooms",
                column: "LectureId",
                unique: true,
                filter: "[LectureId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoRooms");

            migrationBuilder.DropColumn(
                name: "VideoRoomId",
                table: "Lectures");
        }
    }
}
