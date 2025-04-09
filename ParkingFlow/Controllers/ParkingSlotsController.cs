using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            List<ParkingSlots> parkingSlots = _db.ParkingSlots.ToList();
            return View(parkingSlots);
        }

        public IActionResult Create()
        {
            return View();
        }
        // Create action method to add new parking slots
        [HttpPost]
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
        // Delete
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
