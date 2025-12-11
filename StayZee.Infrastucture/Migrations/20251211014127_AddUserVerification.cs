using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayZee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserVerification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Bookings_BookingStatuses_BookingStatusId",
            //    table: "Bookings");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Bookings_Customers_CustomerId",
            //    table: "Bookings");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Bookings_Homes_HomeId",
            //    table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_PaymentStatuses_PaymentStatusId",
                table: "Bookings");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VerificationExpiresAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            ////migrationBuilder.CreateTable(
            ////    name: "BookingSharedCustomers",
            ////    columns: table => new
            ////    {
            ////        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            ////        BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            ////        CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            ////    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_BookingSharedCustomers", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_BookingSharedCustomers_Bookings_BookingId",
            //            column: x => x.BookingId,
            //            principalTable: "Bookings",
            //            principalColumn: "BookingId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_BookingSharedCustomers_Customers_CustomerId",
            //            column: x => x.CustomerId,
            //            principalTable: "Customers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_BookingSharedCustomers_BookingId_CustomerId",
            //    table: "BookingSharedCustomers",
            //    columns: new[] { "BookingId", "CustomerId" },
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_BookingSharedCustomers_CustomerId",
            //    table: "BookingSharedCustomers",
            //    column: "CustomerId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Bookings_BookingStatuses_BookingStatusId",
            //    table: "Bookings",
            //    column: "BookingStatusId",
            //    principalTable: "BookingStatuses",
            //    principalColumn: "BookingStatusId",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Bookings_Customers_CustomerId",
            //    table: "Bookings",
            //    column: "CustomerId",
            //    principalTable: "Customers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Bookings_Homes_HomeId",
            //    table: "Bookings",
            //    column: "HomeId",
            //    principalTable: "Homes",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Bookings_PaymentStatuses_PaymentStatusId",
            //    table: "Bookings",
            //    column: "PaymentStatusId",
            //    principalTable: "PaymentStatuses",
            //    principalColumn: "PaymentStatusId",
            //    onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_BookingStatuses_BookingStatusId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_CustomerId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Homes_HomeId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_PaymentStatuses_PaymentStatusId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "BookingSharedCustomers");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VerificationExpiresAt",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_BookingStatuses_BookingStatusId",
                table: "Bookings",
                column: "BookingStatusId",
                principalTable: "BookingStatuses",
                principalColumn: "BookingStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_CustomerId",
                table: "Bookings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Homes_HomeId",
                table: "Bookings",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_PaymentStatuses_PaymentStatusId",
                table: "Bookings",
                column: "PaymentStatusId",
                principalTable: "PaymentStatuses",
                principalColumn: "PaymentStatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
