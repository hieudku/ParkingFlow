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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParkingSlotId,BookingDate,StartTime,EndTime")] Bookings bookings)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                bookings.UserId = _userManager.GetUserId(User)!; // Set the UserId to the logged-in user's ID
            }
            ModelState.Remove(nameof(bookings.UserId));
            if (ModelState.IsValid)
            {
                // Mark slot as occupied
                var parkingSlot = await _db.ParkingSlots.FindAsync(bookings.ParkingSlotId);
                if (parkingSlot != null)
                {
                    parkingSlot.IsVacant = false;
                    _db.ParkingSlots.Update(parkingSlot);
                }
                _db.Add(bookings);
                await _db.SaveChangesAsync();
                return RedirectToAction(
                    "Index", "ParkingSlots",
                    new
                    {
                        bookingDate = bookings.BookingDate.ToString("yyyy-MM-dd"),
                        startTime = bookings.StartTime.ToString(@"hh\:mm"),
                        endTime = bookings.EndTime.ToString(@"hh\:mm")
                    });
            }
            ViewData["ParkingSlotId"] = new SelectList(_db.ParkingSlots, "Id", "SlotCode", bookings.ParkingSlotId);
            return View(bookings);
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
