namespace OnlineCourseManagementSystem.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class SubscribersEntityUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmedDate",
                table: "Subscribers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsConfirmed",
                table: "Subscribers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmedDate",
                table: "Subscribers");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Subscribers");
        }
    }
}
