using System;

namespace ParkingFlow.Models
{
    public class PendingBooking
    {
        public int ParkingSlotId { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
