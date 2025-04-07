using ParkingFlow.Models;
using System.ComponentModel.DataAnnotations;

namespace ParkingFlow.Models
{
    public class ParkingSlots
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Slot Code is required")]
        [StringLength(10, ErrorMessage = "Slot Code cannot exceed 10 characters")]
        [Display(Name = "Slot Code")]
        public string? SlotCode { get; set; } 

        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters")]
        [Display(Name = "Location (Street)")]
        public string? Location { get; set; }

        [Display(Name = "Is Vacant?")]
        public bool IsVacant { get; set; } = true;

        // Navigation property
        public ICollection<Bookings> Bookings { get; set; } = new List<Bookings>();
    }
}
