using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayZee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsBlocked", "NICOrPassport", "Name", "PasswordHash", "PhoneNumber", "Role", "Username" },
                values: new object[] { 1, "admin@example.com", false, "A1234567", "Admin", "$2a$11$oCV74rZcRLxtlb3VtbjvCOXnU9URHrwUMX9kFFxG7PVGW/bwT99qi", "1234567890", "Admin", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
