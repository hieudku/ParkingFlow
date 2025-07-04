﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingFlow.Data;
using ParkingFlow.Models;

namespace ParkingFlow.Controllers
{
    public class ParkingSlotsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ParkingSlotsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Filter parking slots based on booking date and time
        public IActionResult Index(DateTime? bookingDate, TimeSpan? startTime, TimeSpan? endTime)
        {
            var nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            var nzNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, nzTimeZone);

            ViewBag.CurrentDateTime = nzNow;

            bookingDate ??= nzNow.Date;
            startTime ??= nzNow.TimeOfDay;
            endTime ??= nzNow.AddMinutes(30).TimeOfDay;

            ViewBag.BookingDate = bookingDate;
            ViewBag.StartTime = startTime;
            ViewBag.EndTime = endTime;

            ViewBag.NZBookingDate = bookingDate.Value.ToString("yyyy-MM-dd");
            ViewBag.NZStartTime = startTime.Value.ToString(@"hh\:mm");
            ViewBag.NZEndTime = endTime.Value.ToString(@"hh\:mm");

            var parkingSlots = _db.ParkingSlots.Include(p => p.Bookings).ToList();

            foreach (var slot in parkingSlots)
            {
                slot.IsVacant = true;

                var conflict = slot.Bookings.Any(b =>
                    b.BookingDate.Date == bookingDate.Value.Date &&
                    (
                        (startTime >= b.StartTime && startTime < b.EndTime) ||
                        (endTime > b.StartTime && endTime <= b.EndTime) ||
                        (startTime <= b.StartTime && endTime >= b.EndTime)
                    ));

                if (conflict)
                    slot.IsVacant = false;
            }

            return View(parkingSlots);
        }


        // Create action method to add new parking slots
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ParkingSlots parkingSlot)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Parking Slot created successfully";
                _db.ParkingSlots.Add(parkingSlot);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if (!ModelState.IsValid)
            {
                TempData["error"] = "Failed to create parking slot";
            }
            return View(parkingSlot);
        }

        // Edit action method to update parking slot details
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            ParkingSlots? categoryFromDb = _db.ParkingSlots.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(ParkingSlots obj)
        {
            if (ModelState.IsValid)
            {
                _db.ParkingSlots.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Parking Slot updated successfully";
                return RedirectToAction("Index");
            }
            else if (!ModelState.IsValid)
            {
                TempData["error"] = "Failed to update Slot. Please check for errors";
            }
            return View();
        }

        // Delete action method to remove parking slots
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            ParkingSlots? categoryFromDb = _db.ParkingSlots.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeletePOST(int? id)
        {
            ParkingSlots? obj = _db.ParkingSlots.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.ParkingSlots.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Delete successful!";
            return RedirectToAction("Index");
        }
    }
}
