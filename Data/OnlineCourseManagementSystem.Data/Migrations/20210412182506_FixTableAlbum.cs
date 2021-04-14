namespace OnlineCourseManagementSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class FixColumnAlbum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_AspNetUsers_UserId",
                table: "Album");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Album_AlbumId",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Album",
                table: "Album");

            migrationBuilder.RenameTable(
                name: "Album",
                newName: "Albums");

            migrationBuilder.RenameIndex(
                name: "IX_Album_UserId",
                table: "Albums",
                newName: "IX_Albums_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Album_IsDeleted",
                table: "Albums",
                newName: "IX_Albums_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Albums",
                table: "Albums",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_AspNetUsers_UserId",
                table: "Albums",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Albums_AlbumId",
                table: "Files",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_AspNetUsers_UserId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Albums_AlbumId",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Albums",
                table: "Albums");

            migrationBuilder.RenameTable(
                name: "Albums",
                newName: "Album");

            migrationBuilder.RenameIndex(
                name: "IX_Albums_UserId",
                table: "Album",
                newName: "IX_Album_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Albums_IsDeleted",
                table: "Album",
                newName: "IX_Album_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Album",
                table: "Album",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_AspNetUsers_UserId",
                table: "Album",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Album_AlbumId",
                table: "Files",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
