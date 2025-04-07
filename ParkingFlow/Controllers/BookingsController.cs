using Microsoft.AspNetCore.Mvc;
using ParkingFlow.Data;
using ParkingFlow.Models;

namespace ParkingFlow.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookingsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Bookings> bookings = _db.Bookings.ToList();
            return View(bookings);
        }
    }
}