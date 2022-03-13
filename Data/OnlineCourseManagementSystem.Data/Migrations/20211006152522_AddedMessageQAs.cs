namespace OnlineCourseManagementSystem.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedMessageQAs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageQAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    IsHighlighted = table.Column<bool>(type: "bit", nullable: true),
                    IsStarred = table.Column<bool>(type: "bit", nullable: true),
                    IsAnswered = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageQAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageQAs_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageQAs_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageQAs_MessageQAs_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MessageQAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageQAs_ChannelId",
                table: "MessageQAs",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageQAs_CreatorId",
                table: "MessageQAs",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageQAs_IsDeleted",
                table: "MessageQAs",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_MessageQAs_ParentId",
                table: "MessageQAs",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageQAs");
        }
    }
}
