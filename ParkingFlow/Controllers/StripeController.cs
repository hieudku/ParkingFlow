using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingFlow.Data;
using ParkingFlow.Helpers;
using ParkingFlow.Models;
using Stripe.Checkout;

namespace ParkingFlow.Controllers
{
    public class StripeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public StripeController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult CreateCheckoutSession(decimal amount)
        {
            var domain = $"{Request.Scheme}://{Request.Host}";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(amount * 100),
                            Currency = "nzd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Parking Slot Booking",
                            },
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = domain + "/Stripe/Success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "/Stripe/Cancel"
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Redirect(session.Url);
        }

        [HttpGet]
        public async Task<IActionResult> Success(string session_id)
        {
            var service = new SessionService();
            var stripeSession = await service.GetAsync(session_id);
            var userId = _userManager.GetUserId(User);

            // Save Stripe Payment
            var payment = new StripePayment
            {
                StripePaymentId = stripeSession.PaymentIntentId,
                AmountPaid = stripeSession.AmountTotal.GetValueOrDefault() / 100M,
                Currency = stripeSession.Currency,
                ReceiptEmail = stripeSession.CustomerDetails?.Email ?? "Unknown",
                PaymentStatus = stripeSession.PaymentStatus,
                PaymentDate = DateTime.UtcNow,
                UserId = userId
            };

            _db.StripePayments.Add(payment);

            // Finalize Booking from session
            var pendingBooking = HttpContext.Session.GetObject<Bookings>("PendingBooking");
            if (pendingBooking != null)
            {
                pendingBooking.UserId = userId;
                _db.Bookings.Add(pendingBooking);

                var slot = await _db.ParkingSlots.FirstOrDefaultAsync(s => s.Id == pendingBooking.ParkingSlotId);
                if (slot != null)
                {
                    slot.IsVacant = false;
                    _db.ParkingSlots.Update(slot);
                }

                HttpContext.Session.Remove("PendingBooking");
            }

            await _db.SaveChangesAsync();

            TempData["Success"] = "Payment and booking confirmed!";
            return RedirectToAction("Index", "Bookings");
        }

        public IActionResult Cancel()
        {
            TempData["Error"] = "Payment was canceled.";
            return RedirectToAction("Review", "Bookings");
        }
    }
}
