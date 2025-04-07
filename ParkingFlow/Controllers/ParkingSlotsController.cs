﻿using Microsoft.AspNetCore.Mvc;
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
    }
}
