using System.Diagnostics;
using LoginFormASPcore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DinkToPdf.Contracts;
using DinkToPdf;
using LoginFormASPcore.Helpers;
//using LoginFormASPcore.Helpers.YourProjectNamespace.Helpers;
using LoginFormASPcore.Helpers; // For RenderViewAsync

namespace LoginFormASPcore.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IConverter _converter;

        public HomeController(MyDbContext context, IConverter converter)
        {
            _context = context;
            _converter = converter;
        }



        public async Task<IActionResult> DownloadTicket(int id)
        {
            var booking = await _context.EventBookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            var htmlContent = await this.RenderViewAsync("Invoice", booking, true);

            var pdf = _converter.Convert(new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10, Bottom = 10 }
                },
                Objects = {
            new ObjectSettings
            {
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8" }
            }
        }
            });

            return File(pdf, "application/pdf", "Ticket.pdf");
        }



        // Index action
        public IActionResult Index()
        {
            return View();
        }

        // Login page
        public IActionResult Login()
        {
            return View();
        }

        // Handle login form submission
        [HttpPost]
        public IActionResult Login(UserTbl user)
        {
            var myUser = _context.UserTbls
                .FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);

            if (myUser != null)
            {
                // Create session
                HttpContext.Session.SetString("UserSession", myUser.Email);
                return RedirectToAction("BookEvent");
            }

            // Show error message on login failure
            ViewBag.Message = "Invalid email or password. Please try again.";
            return View();
        }

        public IActionResult Logout()
        {
            // Check if the user session exists
            var userSession = HttpContext.Session.GetString("UserSession");
            if (userSession != null)
            {
                // Remove the user session
                HttpContext.Session.Remove("UserSession");
            }

            // Redirect the user to the Login page after logout
            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        // BookEvent page (GET)
        public IActionResult BookEvent()
        {
            var userSession = HttpContext.Session.GetString("UserSession");
            if (string.IsNullOrEmpty(userSession))
            {
                //TempData["Error"] = "Session expired. Please log in again.";
                return RedirectToAction("Login");
            }

            return View();
        }

        // Handle event booking (POST)
        [HttpPost]
        public async Task<IActionResult> BookEvent(EventBooking booking)
        {
            var userSession = HttpContext.Session.GetString("UserSession");
            if (string.IsNullOrEmpty(userSession))
            {
                TempData["Error"] = "Session expired. Please log in again.";
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                booking.BookedBy = userSession;
                await _context.EventBookings.AddAsync(booking);
                await _context.SaveChangesAsync();

                // Redirect to a view to show the ticket
                return RedirectToAction("Ticket", new { id = booking.Id });
            }

            TempData["Error"] = "Failed to book event. Please try again.";
            return View();
        }
        public async Task<IActionResult> Ticket(int id)
        {
            var booking = await _context.EventBookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }





        [HttpPost]
        public async Task<IActionResult> Register(UserTbl user)
        {
            if (ModelState.IsValid)
            {
                // Check if a user with the same email (or username) already exists
                var existingUser = await _context.UserTbls.FirstOrDefaultAsync(u => u.Email == user.Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "This email is already registered.");
                    return View(user);
                }

                // Proceed with registration
                await _context.UserTbls.AddAsync(user);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Registered Successfully";
                return RedirectToAction("Login");
            }
            return View(user);
        }
    }
}