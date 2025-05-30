using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToStripePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "StripePayments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StripePayments");

            migrationBuilder.DropColumn(
                name: "StripePaymentIntentId",
                table: "StripePayments");

            migrationBuilder.RenameColumn(
                name: "StripeSessionId",
                table: "StripePayments",
                newName: "StripePaymentId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "StripePayments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "StripePayments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptEmail",
                table: "StripePayments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "StripePayments");

            migrationBuilder.DropColumn(
                name: "ReceiptEmail",
                table: "StripePayments");

            migrationBuilder.RenameColumn(
                name: "StripePaymentId",
                table: "StripePayments",
                newName: "StripeSessionId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "StripePayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "StripePayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "StripePayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StripePaymentIntentId",
                table: "StripePayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
