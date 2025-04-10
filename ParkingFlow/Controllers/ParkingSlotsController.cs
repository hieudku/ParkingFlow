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
            ViewBag.CurrentDateTime = DateTime.Now;
            var parkingSlots = _db.ParkingSlots
                .Include(p => p.Bookings)
                .ToList();
            if (bookingDate.HasValue && startTime.HasValue && endTime.HasValue)
            {
                foreach (var slot in parkingSlots)
                {
                    bool hasConflict = slot.Bookings.Any(b =>
                        b.BookingDate.Date == bookingDate.Value.Date &&
                        (
                            (startTime >= b.StartTime && startTime < b.EndTime) ||
                            (endTime > b.StartTime && endTime <= b.EndTime) ||
                            (startTime <= b.StartTime && endTime >= b.EndTime)
                        ));
                    slot.IsVacant = !hasConflict;
                }
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
                _db.ParkingSlots.Add(parkingSlot);
                _db.SaveChanges();
                return RedirectToAction("Index");
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
                TempData["error"] = "Failed to edit category";
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
