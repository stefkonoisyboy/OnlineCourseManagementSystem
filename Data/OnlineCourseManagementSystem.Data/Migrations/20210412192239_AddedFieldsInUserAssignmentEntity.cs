namespace OnlineCourseManagementSystem.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedFieldsInUserAssignmentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seen",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "Points",
                table: "Assignments",
                newName: "PossiblePoints");

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "UserAssignments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Seen",
                table: "UserAssignments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "TurnedOn",
                table: "UserAssignments",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "UserAssignments");

            migrationBuilder.DropColumn(
                name: "Seen",
                table: "UserAssignments");

            migrationBuilder.DropColumn(
                name: "TurnedOn",
                table: "UserAssignments");

            migrationBuilder.RenameColumn(
                name: "PossiblePoints",
                table: "Assignments",
                newName: "Points");

            migrationBuilder.AddColumn<bool>(
                name: "Seen",
                table: "Assignments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
