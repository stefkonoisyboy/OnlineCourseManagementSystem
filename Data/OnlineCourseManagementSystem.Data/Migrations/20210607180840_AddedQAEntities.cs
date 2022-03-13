namespace OnlineCourseManagementSystem.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedQAEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AudienceCommentId",
                table: "Likes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AudienceComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    ChannelId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudienceComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AudienceComments_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AudienceComments_AudienceComments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AudienceComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AudienceComments_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_AudienceCommentId",
                table: "Likes",
                column: "AudienceCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_AudienceComments_AuthorId",
                table: "AudienceComments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_AudienceComments_ChannelId",
                table: "AudienceComments",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_AudienceComments_IsDeleted",
                table: "AudienceComments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_AudienceComments_ParentId",
                table: "AudienceComments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_IsDeleted",
                table: "Channels",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AudienceComments_AudienceCommentId",
                table: "Likes",
                column: "AudienceCommentId",
                principalTable: "AudienceComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AudienceComments_AudienceCommentId",
                table: "Likes");

            migrationBuilder.DropTable(
                name: "AudienceComments");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropIndex(
                name: "IX_Likes_AudienceCommentId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "AudienceCommentId",
                table: "Likes");
        }
    }
}
