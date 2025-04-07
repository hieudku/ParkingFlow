using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ParkingFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitializeModelsWithSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkingSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlotCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsVacant = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSlots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParkingSlotId = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_ParkingSlots_ParkingSlotId",
                        column: x => x.ParkingSlotId,
                        principalTable: "ParkingSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ParkingSlots",
                columns: new[] { "Id", "IsVacant", "Location", "SlotCode" },
                values: new object[,]
                {
                    { 1, true, "Kensington Avenue", "K1" },
                    { 2, true, "Kensington Avenue", "K2" },
                    { 3, true, "Kensington Avenue", "K3" },
                    { 4, true, "Kensington Avenue", "K4" },
                    { 5, true, "Kensington Avenue", "K5" },
                    { 6, true, "Kensington Avenue", "K6" },
                    { 7, true, "Kensington Avenue", "K7" },
                    { 8, true, "Kensington Avenue", "K8" },
                    { 9, true, "Kensington Avenue", "K9" },
                    { 10, true, "Kensington Avenue", "K10" },
                    { 11, true, "Kensington Avenue", "K11" },
                    { 12, true, "Kensington Avenue", "K12" },
                    { 13, true, "Huia Street", "H1" },
                    { 14, true, "Huia Street", "H2" },
                    { 15, true, "Huia Street", "H3" },
                    { 16, true, "Huia Street", "H4" },
                    { 17, true, "Huia Street", "H5" },
                    { 18, true, "Huia Street", "H6" },
                    { 19, true, "Huia Street", "H7" },
                    { 20, true, "Huia Street", "H8" },
                    { 21, true, "Huia Street", "H9" },
                    { 22, true, "Huia Street", "H10" },
                    { 23, true, "Huia Street", "H11" },
                    { 24, true, "Huia Street", "H12" },
                    { 25, true, "Udy Street", "U1" },
                    { 26, true, "Udy Street", "U2" },
                    { 27, true, "Udy Street", "U3" },
                    { 28, true, "Udy Street", "U4" },
                    { 29, true, "Udy Street", "U5" },
                    { 30, true, "Udy Street", "U6" },
                    { 31, true, "Udy Street", "U7" },
                    { 32, true, "Udy Street", "U8" },
                    { 33, true, "Udy Street", "U9" },
                    { 34, true, "Udy Street", "U10" },
                    { 35, true, "Udy Street", "U11" },
                    { 36, true, "Udy Street", "U12" },
                    { 37, true, "Atiawa Street", "A1" },
                    { 38, true, "Atiawa Street", "A2" },
                    { 39, true, "Atiawa Street", "A3" },
                    { 40, true, "Atiawa Street", "A4" },
                    { 41, true, "Atiawa Street", "A5" },
                    { 42, true, "Atiawa Street", "A6" },
                    { 43, true, "Atiawa Street", "A7" },
                    { 44, true, "Atiawa Street", "A8" },
                    { 45, true, "Atiawa Street", "A9" },
                    { 46, true, "Atiawa Street", "A10" },
                    { 47, true, "Atiawa Street", "A11" },
                    { 48, true, "Atiawa Street", "A12" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ParkingSlotId",
                table: "Bookings",
                column: "ParkingSlotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "ParkingSlots");
        }
    }
}
