using Project.Models;
using Project.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        VehicleRentalDBEntities _db = new VehicleRentalDBEntities();
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            
            return View();
        }
        [Authorize(Roles = "User")]
        [HttpPost]
       public ActionResult Create(BookingViewModel bvm)
        {
            tblBooking tb = new tblBooking();

            tb.VehicleId = bvm.VehicleId;
            tb.UserId = bvm.UserId;
          
            tb.BookingDate = bvm.BookingDate;
            tb.DueDate = bvm.DueDate;
            tb.AmountPaid = bvm.AmountPaid;
            tb.TotalAmount = bvm.TotalAmount;
            HttpPostedFileBase fup = Request.Files["CitizenshipPhoto"];
            if (fup != null)
            {
                tb.CitizenshipPhoto = fup.FileName;
                fup.SaveAs(Server.MapPath("~/images/Citizenship/" + fup.FileName));
            }
            _db.tblBookings.Add(tb);
            _db.SaveChanges();
            return RedirectToAction("Shop");
           
        }
    }
}