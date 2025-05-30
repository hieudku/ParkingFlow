using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkingFlow.Data;
using ParkingFlow.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ParkingFlow.Helpers;

namespace ParkingFlow.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<BookingsController> _logger;

        public BookingsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, ILogger<BookingsController> logger)
        {
            _db = context;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (User.IsInRole("Admin")) // Admin can view all bookings
            {
                var allBookings = await _db.Bookings
                    .Include(b => b.ParkingSlot)
                    .OrderByDescending(b => b.BookingDate)
                    .ToListAsync();

                return View(allBookings);
            }
            var userBookings = await _db.Bookings
                .Include(b => b.ParkingSlot)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();

            return View(userBookings);
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _db.Bookings
                .Include(b => b.ParkingSlot)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // GET: Bookings/Create
        public IActionResult Create(int? slotId)
        {
            ViewData["ParkingSlotId"] = new SelectList(_db.ParkingSlots, "Id", "SlotCode", slotId);
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ParkingSlotId,BookingDate,StartTime,EndTime")] Bookings bookings)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                bookings.UserId = _userManager.GetUserId(User)!;
            }

            ModelState.Remove(nameof(bookings.UserId));
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetObject("PendingBooking", bookings);
                return RedirectToAction("Review");
            }

            TempData["Error"] = "Failed to process booking. Please check the details.";
            ViewData["ParkingSlotId"] = new SelectList(_db.ParkingSlots, "Id", "SlotCode", bookings.ParkingSlotId);
            return View(bookings);
        }

        // GET: Bookings/Review
        public IActionResult Review()
        {
            var pendingBooking = HttpContext.Session.GetObject<Bookings>("PendingBooking");
            if (pendingBooking == null)
            {
                TempData["Error"] = "No pending booking found.";
                return RedirectToAction(nameof(Create));
            }

            ViewData["ParkingSlot"] = _db.ParkingSlots.FirstOrDefault(s => s.Id == pendingBooking.ParkingSlotId);
            return View(pendingBooking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelPending()
        {
            HttpContext.Session.Remove("PendingBooking");
            TempData["Success"] = "Pending booking has been cancelled.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmBooking()
        {
            var pendingBooking = HttpContext.Session.GetObject<Bookings>("PendingBooking");

            if (pendingBooking == null)
            {
                TempData["Error"] = "No pending booking found to confirm.";
                return RedirectToAction(nameof(Create));
            }

            // Set UserId again just in case
            pendingBooking.UserId = _userManager.GetUserId(User)!;

            // Mark slot as occupied
            var parkingSlot = await _db.ParkingSlots.FindAsync(pendingBooking.ParkingSlotId);
            if (parkingSlot != null)
            {
                parkingSlot.IsVacant = false;
                _db.ParkingSlots.Update(parkingSlot);
            }

            _db.Bookings.Add(pendingBooking);
            await _db.SaveChangesAsync();

            HttpContext.Session.Remove("PendingBooking"); // clear session

            TempData["Success"] = "Booking confirmed successfully!";
            return RedirectToAction("Index", "ParkingSlots");
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _db.Bookings.FindAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }
            ViewData["ParkingSlotId"] = new SelectList(_db.ParkingSlots, "Id", "SlotCode", bookings.ParkingSlotId);
            return View(bookings);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ParkingSlotId,BookingDate,StartTime,EndTime")] Bookings bookings)
        {
            if (id != bookings.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["Success"] = "Booking updated successfully!";
                    _db.Update(bookings);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingsExists(bookings.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Failed to update booking. Please check the details and try again.";
            ViewData["ParkingSlotId"] = new SelectList(_db.ParkingSlots, "Id", "SlotCode", bookings.ParkingSlotId);
            return View(bookings);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _db.Bookings
                .Include(b => b.ParkingSlot)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookings = await _db.Bookings.FindAsync(id);
            if (bookings != null)
            {
                TempData["Success"] = "Booking deleted successfully!";
                _db.Bookings.Remove(bookings);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingsExists(int id)
        {
            return _db.Bookings.Any(e => e.Id == id);
        }
    }
}
