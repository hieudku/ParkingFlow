using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingsSeedSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDate", "EndTime", "ParkingSlotId", "StartTime", "UserId" },
                values: new object[] { 1, new DateTime(2025, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 0, 0, 0), 1, new TimeSpan(0, 9, 0, 0, 0), "test-user" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
