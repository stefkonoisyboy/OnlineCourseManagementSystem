namespace OnlineCourseManagementSystem.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddColumnAlbum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Album",
                table: "Files");

            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "Files",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Seen",
                table: "Assignments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Album_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_AlbumId",
                table: "Files",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Album_IsDeleted",
                table: "Album",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Album_UserId",
                table: "Album",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Album_AlbumId",
                table: "Files",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Album_AlbumId",
                table: "Files");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropIndex(
                name: "IX_Files_AlbumId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Seen",
                table: "Assignments");

            migrationBuilder.AddColumn<string>(
                name: "Album",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
