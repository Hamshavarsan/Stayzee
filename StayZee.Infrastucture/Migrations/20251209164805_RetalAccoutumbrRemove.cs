using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayZee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RetalAccoutumbrRemove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Rentals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "Rentals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
