using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MeetupInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Meetup");

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "Meetup",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Groups",
                schema: "Meetup");
        }
    }
}
