using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayZee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BookingSharedCustomerTable_Fixed_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingSharedCustomers_Bookings_BookingId",
                table: "BookingSharedCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Rentals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Rentals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "RentalId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingSharedCustomers_Bookings_BookingId",
                table: "BookingSharedCustomers",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingSharedCustomers_Bookings_BookingId",
                table: "BookingSharedCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RentalId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bookings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingSharedCustomers_Bookings_BookingId",
                table: "BookingSharedCustomers",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
