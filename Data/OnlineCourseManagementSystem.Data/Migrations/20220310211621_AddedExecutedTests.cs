using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCourseManagementSystem.Data.Migrations
{
    public partial class AddedExecutedTests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExecutedTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestInput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserOutput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpectedOutput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmissionId = table.Column<int>(type: "int", nullable: false),
                    HasPassed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutedTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutedTests_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExecutedTests_IsDeleted",
                table: "ExecutedTests",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutedTests_SubmissionId",
                table: "ExecutedTests",
                column: "SubmissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExecutedTests");
        }
    }
}
