using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureEntityRelationshipBookingsAndSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_ParkingSlots_ParkingSlotId",
                table: "Bookings");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_ParkingSlots_ParkingSlotId",
                table: "Bookings",
                column: "ParkingSlotId",
                principalTable: "ParkingSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_ParkingSlots_ParkingSlotId",
                table: "Bookings");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_ParkingSlots_ParkingSlotId",
                table: "Bookings",
                column: "ParkingSlotId",
                principalTable: "ParkingSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
