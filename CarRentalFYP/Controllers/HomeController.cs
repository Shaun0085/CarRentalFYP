using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CarRentalFYP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Razorpay.Api;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

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
        public IActionResult Index(string orderId)
        {
            var vehicles = _context.Vehicles.Include(v => v.VehicleCategory).ToList();

            if (!string.IsNullOrEmpty(orderId))
            {
                SendBookingConfirmationEmail(orderId);
            }
            return View(vehicles);
        }


        public class BookingViewModel
        {
            public Vehicle Vehicle { get; set; }
            public List<LocationViewModel> DrivingRanges { get; set; }
        }
        public class LocationViewModel
        {
            public int LocationId { get; set; }
            public string Name { get; set; }
            public int? Fee { get; set; }
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
                var drivingRanges = _context.Locations
                    .Select(l => new LocationViewModel { LocationId = l.LocationId, Name = l.Name, Fee = l.Fee })
                    .ToList();
                var viewModel = new BookingViewModel
                {
                    Vehicle = vehicle,
                    DrivingRanges = drivingRanges
                };

                return View(viewModel);
            }
            return View();
        }

        private async Task SendEmailAsync(string email, string customerName, string phoneNumber,
            string pickUpLocation, DateTime? pickUpDate, TimeSpan? pickUpTime, string dropOffLocation,
            DateTime? dropOffDate, TimeSpan? dropOffTime, int? amount, string paymentType)
        {
            var formattedPickUpDate = pickUpDate?.ToShortDateString();
            var formattedDropOffDate = dropOffDate?.ToShortDateString();
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("everydaytransport@everydaytransportss.com", "Everyday Transport");
            var to = new EmailAddress(email);
            var templateId = "d-7dbf91def98f4c89b2a15ec19668a922";
            var dynamicTemplateData = new
            {
                customer_name = customerName,
                phone_number = phoneNumber,
                pickup_location = pickUpLocation,
                pickup_date = formattedPickUpDate,
                pickup_time = pickUpTime,
                dropoff_location = dropOffLocation,
                dropoff_date = formattedDropOffDate,
                dropoff_time = dropOffTime,
                payment_amount = amount,
                payment_type = paymentType
            };
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, dynamicTemplateData);
            var response = await client.SendEmailAsync(msg);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Email has been sent successfully");
            }
        }

        [HttpPost]
        private async Task SendBookingConfirmationEmail(string orderId)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.OrderId == orderId);
            if (booking != null)
            {
                await SendEmailAsync(booking.Email, booking.CustomerName, booking.CustomerPhoneNumber,
                    booking.PickUpLocation, booking.PickUpDate, booking.PickUpTime,
                    booking.DropOffLocation, booking.DropOffDate, booking.DropOffTime,
                    booking.Amount, booking.PaymentType);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveBooking(Booking model, Billing billing, int drivingRange)
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
                    BabySeat = model.BabySeat == "Yes" ? "Yes" : "No",
                    UsbCable = model.UsbCable == "Yes" ? "Yes" : "No",
                    PhoneHolder = model.PhoneHolder == "Yes" ? "Yes" : "No",
                    LocationId = drivingRange,
                    BookingStatusId = 1,
                    Amount = model.Amount,
                    PaymentType = model.PaymentType
                };
                var newBilling = new Billing
                {
                    FirstName = billing.FirstName,
                    LastName = billing.LastName,
                    PhoneNumber = billing.PhoneNumber,
                    Address = billing.Address,
                    City = billing.City,
                    PostCode = billing.PostCode,
                    Country = billing.Country,
                    State = billing.State
                };
                _context.Billings.Add(newBilling);
                _context.SaveChanges();

                booking.BillingId = newBilling.BillingId;

                _context.Bookings.Add(booking);
                _context.SaveChanges();

                await SendEmailAsync(model.Email, model.CustomerName, model.CustomerPhoneNumber, model.PickUpLocation,
                    model.PickUpDate, model.PickUpTime, model.DropOffLocation, model.DropOffDate, model.DropOffTime,
                    model.Amount, model.PaymentType);
                return RedirectToAction("Index", new { showBookingSuccessModal = true });
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

        public ActionResult Checkout(Booking model, int DrivingRange)
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

            var drivingRange = DrivingRange;
            var drivingRangeName = "Unknown";
            var totalDeliveryFee = 0;


            var location = _context.Locations.FirstOrDefault(l => l.LocationId == drivingRange);
            if (location != null)
            {
                drivingRangeName = location.Name;
            }
            else
            {
                drivingRangeName = "Unknown";
            }

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

            if(pickUpLocation == "Hotel")
            {
                totalDeliveryFee += 15;
            }

            if (dropOffLocation == "Hotel")
            {
                totalDeliveryFee += 15;
            }

            var dailyRentalPrice = vehicle.Price ?? 0; 
            var totalRentalPrice = dailyRentalPrice * numberOfDays;
            var totalDeposit = vehicle.Deposit ?? 0; 
            var drivingRangeFee = 0;                                  

            var drivingRangeId = _context.Locations.FirstOrDefault(l => l.LocationId == drivingRange);
            if (drivingRangeId != null)
            {
                drivingRangeFee = drivingRangeId.Fee ?? 0; 
            }

            var totalPayment = totalRentalPrice + totalDeposit + drivingRangeFee + totalDeliveryFee;
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
            ViewBag.DrivingRangeName = drivingRangeName;
            ViewBag.DrivingRange = drivingRange;
            ViewBag.BabySeat = babySeat;
            ViewBag.PhoneHolder = phoneHolder;
            ViewBag.USBCable = usbCable;
            ViewBag.TotalRentalPrice = totalRentalPrice;
            ViewBag.TotalDeposit = totalDeposit;
            ViewBag.TotalDrivingRangeFee = totalDrivingRangeFee;
            ViewBag.TotalDeliveryFee = totalDeliveryFee;
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

        public IActionResult InitiateOrder(Booking model, Billing billing, int DrivingRange)
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
            var paymentType = model.PaymentType;

            var drivingRange = DrivingRange;
            var drivingRangeName = "Unknown";

            var location = _context.Locations.FirstOrDefault(l => l.LocationId == drivingRange);
            if (location != null)
            {
                drivingRangeName = location.Name;
            }
            else
            {
                drivingRangeName = "Unknown";
            }

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

            var drivingRangeId = _context.Locations.FirstOrDefault(l => l.LocationId == drivingRange);
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
            ViewBag.DrivingRangeName = drivingRangeName;
            ViewBag.DrivingRange = drivingRange;
            ViewBag.BabySeat = babySeat;
            ViewBag.PhoneHolder = phoneHolder;
            ViewBag.USBCable = usbCable;
            ViewBag.TotalRentalPrice = totalRentalPrice;
            ViewBag.TotalDeposit = totalDeposit;
            ViewBag.TotalDrivingRangeFee = totalDrivingRangeFee;
            ViewBag.TotalPayment = totalPayment;
            ViewBag.PaymentType = paymentType;

            string key = "rzp_test_h74mk0wiZDO5OY";
            string secret = "D2LQFshr5q1UIGqa4vNEbUfG";
            RazorpayClient client = new RazorpayClient(key, secret);

            // Create order
            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", Convert.ToDecimal(totalPayment)*100); 
            input.Add("currency", "MYR");
            input.Add("receipt", "12121"); 

            try
            {
                Order order = client.Order.Create(input);

                string orderId = order["id"].ToString();

                ViewBag.OrderId = orderId;

                if (ModelState.IsValid)
                {
                    // Save the booking details into the database
                    var booking = new Booking
                    {
                        CustomerName = ViewBag.CustomerName,
                        Icnumber = ViewBag.Icnumber,
                        CustomerPhoneNumber = ViewBag.PhoneNumber,
                        Email = ViewBag.Email,
                        PickUpLocation = ViewBag.PickUpLocation,
                        PickUpDate = ViewBag.PickUpDate,
                        PickUpTime = ViewBag.PickUpTime,
                        DropOffLocation = ViewBag.DropOffLocation,
                        DropOffDate = ViewBag.DropOffDate,
                        DropOffTime = ViewBag.DropOffTime,
                        VehicleId = ViewBag.VehicleId,
                        BabySeat = ViewBag.BabySeat == "Yes" ? "Yes" : "No",
                        UsbCable = ViewBag.UsbCable == "Yes" ? "Yes" : "No",
                        PhoneHolder = ViewBag.PhoneHolder == "Yes" ? "Yes" : "No",
                        LocationId = DrivingRange,
                        BookingStatusId = 1,
                        Amount = ViewBag.TotalPayment,
                        PaymentType = ViewBag.PaymentType,
                        OrderId = ViewBag.OrderId
                    };
                    var newBilling = new Billing
                    {
                        FirstName = billing.FirstName,
                        LastName = billing.LastName,
                        PhoneNumber = billing.PhoneNumber,
                        Address = billing.Address,
                        City = billing.City,
                        PostCode = billing.PostCode,
                        Country = billing.Country,
                        State = billing.State
                    };
                    _context.Billings.Add(newBilling);
                    _context.SaveChanges();

                    booking.BillingId = newBilling.BillingId;

                    _context.Bookings.Add(booking);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                ViewBag.ErrorMessage = "Error creating order: " + ex.Message;
            }
            return RedirectToAction("Payment", "Home", new
            {
                orderId = ViewBag.OrderId,
                amount = ViewBag.Amount,
                customerName = ViewBag.CustomerName,
                email = ViewBag.Email
            });
        }
        public IActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteFailedBooking(string orderId)
        {
            if (!string.IsNullOrEmpty(orderId))
            {
                try
                {
                    var bookingToDelete = _context.Bookings.Include(b => b.Billing)
                                        .FirstOrDefault(b => b.OrderId == orderId);
                    if (bookingToDelete != null)
                    {
                        if (bookingToDelete.Billing != null)
                        {
                            _context.Billings.RemoveRange(bookingToDelete.Billing);
                        }
                        _context.Bookings.Remove(bookingToDelete);
                        _context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return NotFound("Booking with orderId " + orderId + " not found.");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "An error occurred while deleting the booking: " + ex.Message);
                }
            }
            else
            {
                return BadRequest("Invalid orderId provided.");
            }
        }
    }
}