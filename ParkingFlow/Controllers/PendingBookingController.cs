using Microsoft.AspNetCore.Mvc;
using ParkingFlow.Models;
using System.Text.Json;

namespace ParkingFlow.Controllers
{
    public class PendingBookingController : Controller
    {
        private const string SessionKey = "PendingBooking";

        [HttpPost]
        public IActionResult Save(BookingInputModel input)
        {
            var pendingBooking = new PendingBooking
            {
                ParkingSlotId = input.ParkingSlotId,
                BookingDate = input.BookingDate,
                StartTime = input.StartTime,
                EndTime = input.EndTime
            };

            var serialized = JsonSerializer.Serialize(pendingBooking);
            HttpContext.Session.SetString(SessionKey, serialized);

            return RedirectToAction("Preview");
        }

        public IActionResult Preview()
        {
            var serialized = HttpContext.Session.GetString(SessionKey);
            if (serialized == null)
            {
                return RedirectToAction("Index", "ParkingSlots");
            }

            var pendingBooking = JsonSerializer.Deserialize<PendingBooking>(serialized);
            return View(pendingBooking);
        }
    }

    public class BookingInputModel
    {
        public int ParkingSlotId { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
