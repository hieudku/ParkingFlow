using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingFlow.Models
{
    public class StripePayment
    {
        public int Id { get; set; }

        [Required]
        public string StripePaymentId { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal AmountPaid { get; set; }

        [Required]
        public string Currency { get; set; } = "nzd";

        public string? ReceiptEmail { get; set; }

        public string? PaymentStatus { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public string? UserId { get; set; }
    }
}