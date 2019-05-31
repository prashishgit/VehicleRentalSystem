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
        [HttpGet]
        public ActionResult Create(BookingViewModel bvmm)
        {
            BookingViewModel bvm = new BookingViewModel();

            bvm.PickUpDate = bvmm.PickUpDate;
            bvm.DropOffDate = bvmm.DropOffDate;
            bvm.VehicleId = bvmm.VehicleId;
            bvm.VehiclePhoto = bvmm.VehiclePhoto;
            bvm.VehicleTitle = bvmm.VehicleTitle;
            int total = Convert.ToInt32(bvmm.VehiclePrice);
            bvm.TotalAmount = total * 3;


            return View(bvm);
        }

        [HttpPost]
        public ActionResult Create_Post(BookingViewModel bvm)
        {
            tblBooking tb = new tblBooking();

            tb.VehicleId = bvm.VehicleId;
            tb.UserId = Convert.ToInt32(@Session["UserId"]);

            tb.PickUpDate = bvm.PickUpDate;
            tb.DropOffDate = bvm.DropOffDate;
            tb.TotalAmount = bvm.TotalAmount;
           
            tb.BookingStatus = "Pending";
            HttpPostedFileBase fup = Request.Files["CitizenshipPhoto"];
            if (fup != null)
            {
                tb.CitizenshipPhoto = fup.FileName;
                fup.SaveAs(Server.MapPath("~/images/CitizenshipPhoto/" + fup.FileName));
            }
            _db.tblBookings.Add(tb);
            _db.SaveChanges();
            return RedirectToAction("Shop");


        }
    }
}