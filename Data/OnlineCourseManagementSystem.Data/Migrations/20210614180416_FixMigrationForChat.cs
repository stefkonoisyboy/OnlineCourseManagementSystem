namespace OnlineCourseManagementSystem.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class FixMigrationForChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Messages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "MessageEmojis",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MessageEmojis",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Emojis",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Emojis",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "ChatUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ChatUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Chats",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Chats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_IsDeleted",
                table: "Messages",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_MessageEmojis_IsDeleted",
                table: "MessageEmojis",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Emojis_IsDeleted",
                table: "Emojis",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUsers_IsDeleted",
                table: "ChatUsers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_IsDeleted",
                table: "Chats",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Messages_IsDeleted",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_MessageEmojis_IsDeleted",
                table: "MessageEmojis");

            migrationBuilder.DropIndex(
                name: "IX_Emojis_IsDeleted",
                table: "Emojis");

            migrationBuilder.DropIndex(
                name: "IX_ChatUsers_IsDeleted",
                table: "ChatUsers");

            migrationBuilder.DropIndex(
                name: "IX_Chats_IsDeleted",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "MessageEmojis");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MessageEmojis");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Emojis");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Emojis");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "ChatUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ChatUsers");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Chats");
        }
    }
}
