using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayZee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixBookingRelationshipsAndPendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_ord",
                table: "Rentals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "_ord",
                table: "Rentals",
                type: "int",
                nullable: true);
        }
    }
}
