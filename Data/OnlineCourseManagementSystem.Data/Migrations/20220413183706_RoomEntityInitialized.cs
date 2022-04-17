using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCourseManagementSystem.Data.Migrations
{
    public partial class RoomEntityInitialized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDisplayCameraAllowed = table.Column<bool>(type: "bit", nullable: false),
                    IsUnmutedMicAllowed = table.Column<bool>(type: "bit", nullable: false),
                    IsShareScreenAllowed = table.Column<bool>(type: "bit", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomParticipants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsGivenRightToDisplayCamera = table.Column<bool>(type: "bit", nullable: false),
                    IsGivenRightToUnmuteMic = table.Column<bool>(type: "bit", nullable: false),
                    IsGivenRightToShareScreen = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomParticipants_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomParticipants_IsDeleted",
                table: "RoomParticipants",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RoomParticipants_RoomId",
                table: "RoomParticipants",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_IsDeleted",
                table: "Rooms",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomParticipants");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
