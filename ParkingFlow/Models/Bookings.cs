using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingFlow.Models
{
    public class Bookings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public string UserId { get; set; } //the foreign key to AspNetUsers

        [Required]
        [Display(Name = "Parking Slot")]
        public int ParkingSlotId { get; set; }

        [ForeignKey("ParkingSlotId")]
        public ParkingSlots ParkingSlot { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }
    }
}