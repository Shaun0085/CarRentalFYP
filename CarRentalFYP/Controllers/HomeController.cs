using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CarRentalFYP.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CarRentalFYP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CarRentalDatabaseContext _context;
        public HomeController(ILogger<HomeController> logger, CarRentalDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            var vehicles = _context.Vehicles.Include(v => v.VehicleCategory).ToList();
            return View(vehicles);
        }
        public IActionResult Booking(int? vehicleId)
        {
            if (vehicleId != null)
            {
                var vehicle = _context.Vehicles
                    .Include(v => v.VehicleCategory)
                    .Include(v => v.VehicleTransmission)
                    .Include(v => v.VehicleFuel)
                    .FirstOrDefault(v => v.VehicleId == vehicleId);
                if (vehicle == null)
                {
                    return NotFound();
                }
                var drivingRanges = _context.Locations.Select(l => new { l.LocationId, l.Name, l.Fee }).ToList();
                ViewBag.DrivingRanges = drivingRanges;
                return View(vehicle);
            }
            return View();
        }

        [HttpPost]
        public IActionResult SaveBooking(Booking model, int drivingRange)
        {
            if (ModelState.IsValid)
            {
                var booking = new Booking
                {
                    CustomerName = model.CustomerName,
                    Icnumber = model.Icnumber,
                    CustomerPhoneNumber = model.CustomerPhoneNumber,
                    Email = model.Email,
                    PickUpLocation = model.PickUpLocation,
                    PickUpDate = model.PickUpDate,
                    PickUpTime = model.PickUpTime,
                    DropOffLocation = model.DropOffLocation,
                    DropOffDate = model.DropOffDate,
                    DropOffTime = model.DropOffTime,
                    VehicleId = model.VehicleId,
                    BabySeat = model.BabySeat == "on" ? "Yes" : "No",
                    UsbCable = model.UsbCable == "on" ? "Yes" : "No",
                    PhoneHolder = model.PhoneHolder == "on" ? "Yes" : "No",
                    LocationId = drivingRange,
                    BookingStatusId = 1,
                    Amount = model.Amount,
                    PaymentType = model.PaymentType

                };
                _context.Bookings.Add(booking);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Booking", model);
        }
        public IActionResult CarList(string category)
        {
            IQueryable<Vehicle> vehiclesQuery = _context.Vehicles
                .Include(v => v.VehicleCategory)
                .Include(v => v.VehicleTransmission)
                .Include(v => v.VehicleFuel);

            if (!string.IsNullOrEmpty(category))
            {
                vehiclesQuery = vehiclesQuery.Where(v => v.VehicleCategory.Name.ToLower() == category.ToLower());
            }
            var vehicles = vehiclesQuery.ToList();
            return View(vehicles);
        }
        public IActionResult TNC()
        {
            return View();
        }
        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Checkout(Booking model)
        {
            var vehicleId = model.VehicleId;
            var customerName = model.CustomerName;
            var icNumber = model.Icnumber;
            var email = model.Email;
            var phoneNumber = model.CustomerPhoneNumber;
            var pickUpDate = model.PickUpDate;
            var pickUpTime = model.PickUpTime;
            var pickUpLocation = model.PickUpLocation;
            var dropOffDate = model.DropOffDate;
            var dropOffTime = model.DropOffTime;
            var dropOffLocation = model.DropOffLocation;
            var babySeat = model.BabySeat;
            var phoneHolder = model.PhoneHolder;
            var usbCable = model.UsbCable;

            var drivingRange = Request.Form["DrivingRange"];

            var vehicle = _context.Vehicles.FirstOrDefault(v => v.VehicleId == vehicleId);
            if (vehicle == null)
            {
                return NotFound(); 
            }

            int numberOfDays = 0;
            if (pickUpDate.HasValue && dropOffDate.HasValue)
            {
                numberOfDays = (dropOffDate.Value.Date - pickUpDate.Value.Date).Days + 1;
            }

            var dailyRentalPrice = vehicle.Price ?? 0; 
            var totalRentalPrice = dailyRentalPrice * numberOfDays;
            var totalDeposit = vehicle.Deposit ?? 0; 
            var drivingRangeFee = 0;                                  

            var drivingRangeId = _context.Locations.FirstOrDefault(l => l.LocationId == Convert.ToInt32(drivingRange));
            if (drivingRangeId != null)
            {
                drivingRangeFee = drivingRangeId.Fee ?? 0; 
            }

            var totalPayment = totalRentalPrice + totalDeposit + drivingRangeFee;
            var totalDrivingRangeFee = drivingRangeFee;

            ViewBag.VehicleId = vehicleId;
            ViewBag.CustomerName = customerName;
            ViewBag.ICNumber = icNumber;
            ViewBag.Email = email;
            ViewBag.PhoneNumber = phoneNumber;
            ViewBag.PickUpDate = pickUpDate;
            ViewBag.PickUpTime = pickUpTime;
            ViewBag.PickUpLocation = pickUpLocation;
            ViewBag.DropOffDate = dropOffDate;
            ViewBag.DropOffTime = dropOffTime;
            ViewBag.DropOffLocation = dropOffLocation;
            ViewBag.DrivingRange = drivingRange;
            ViewBag.BabySeat = babySeat;
            ViewBag.PhoneHolder = phoneHolder;
            ViewBag.USBCable = usbCable;
            ViewBag.TotalRentalPrice = totalRentalPrice;
            ViewBag.TotalDeposit = totalDeposit;
            ViewBag.TotalDrivingRangeFee = totalDrivingRangeFee;
            ViewBag.TotalPayment = totalPayment;

            if (vehicleId != null)
            {
                var Bookingvehicle = _context.Vehicles
                    .Include(v => v.VehicleCategory)
                    .Include(v => v.VehicleFuel)
                    .FirstOrDefault(v => v.VehicleId == vehicleId);

                if (Bookingvehicle == null)
                {
                    return NotFound();
                }

                return View(Bookingvehicle);
            }

            return View();
        }
    }
}