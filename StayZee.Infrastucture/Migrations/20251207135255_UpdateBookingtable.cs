using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayZee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SharedAt",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SharedEmails",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SharedAt",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "SharedEmails",
                table: "Bookings");
        }
    }
}
