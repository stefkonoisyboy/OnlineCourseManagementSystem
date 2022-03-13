namespace OnlineCourseManagementSystem.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class DeletedEntityIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Icons_IconId",
                table: "Chats");

            migrationBuilder.DropTable(
                name: "Icons");

            migrationBuilder.DropIndex(
                name: "IX_Chats_IconId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "IconId",
                table: "Chats");

            migrationBuilder.AddColumn<string>(
                name: "IconRemoteUrl",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconRemoteUrl",
                table: "Chats");

            migrationBuilder.AddColumn<int>(
                name: "IconId",
                table: "Chats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Icons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chats_IconId",
                table: "Chats",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_Icons_IsDeleted",
                table: "Icons",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Icons_IconId",
                table: "Chats",
                column: "IconId",
                principalTable: "Icons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
