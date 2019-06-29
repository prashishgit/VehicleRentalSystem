using PagedList;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Project.Models.ViewModel;
using System.Threading;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        VehicleRentalDBEntities _db = new VehicleRentalDBEntities();
        [AllowAnonymous]
        public ActionResult Index()
        {
            var user = _db.tblBanners.ToList();
            return View(user);
        }
       
        [AllowAnonymous]

        public PartialViewResult Vehicle()
        {

            var vehicle = _db.tblItems.OrderBy(r => Guid.NewGuid()).Take(8);
            
            return PartialView("_Vehicle", vehicle);
        }
        public PartialViewResult NewArrival()
        {

            var vehicle = _db.tblItems.OrderBy(r => Guid.NewGuid()).ToList();
            var vehicleAvailable = vehicle.Where(u => u.VehicleStatus == "Available").Take(4);

            return PartialView("_NewArrival", vehicleAvailable);
        }
       
        [AllowAnonymous]
        public ActionResult Testimony()
        {
            int i = 0;
          
            List<TestimonyViewModel> list = new List<TestimonyViewModel>();
            var items = _db.tblTestimonies.OrderBy(r => Guid.NewGuid()).Take(3);
          
            foreach (var item in items)
            {
                list.Add(new TestimonyViewModel()
                {

                    UserId = item.UserId,
                    UserName = item.tblUser.UserName,
                    UserPhoto = item.tblUser.Photo,
                    TestimonyDescription = item.TestimonyDescription,
                    Location = item.Location,
                   
                });
                i++;
            }
          
            return PartialView("_Testimony", list);
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
       
        [Authorize(Roles = "Admin")]
        public ActionResult AdminIndex()
        {
            return View();
        }
        public ActionResult UserCount()
        {
            ViewBag.UserNumber = _db.tblUsers.Count();
            return PartialView("_UserCount");
        }
        public ActionResult BookingPending()
        {
            ViewBag.PendingBooking = _db.tblBookings.Where(u => u.BookingStatus == "Pending" || u.BookingStatus == "Confirm").Count();
            return PartialView("_BookingPending");
        }
        public ActionResult TotalAmount()
        {
           var total = 0;
           var booking = _db.tblBookings.ToList();
            foreach (var item in booking)
            {
                total = Convert.ToInt32(total + item.AmountPaid);
            }
            @ViewBag.Total = total;
            return PartialView("_TotalAmount");
        }


        [Authorize(Roles = "User")]
        public ActionResult UserIndex()
        {
            return View();
        }
        public ActionResult UserProfileCard()
        {
            var userId = Convert.ToInt32(Session["UserId"]);
            var userDetails = _db.tblUsers.Where(u => u.UserId == userId).FirstOrDefault();
            var userRole = _db.tblRoles.Where(u => u.RoleId == userDetails.RoleId).FirstOrDefault();
          
            ViewBag.UserName = userDetails.UserName;
            ViewBag.Email = userDetails.Email;
            ViewBag.FullName = userDetails.FullName;
            ViewBag.Role = userRole.RoleName;
            ViewBag.Photo = userDetails.Photo;
            return PartialView("_UserProfileCard");
        }
        public ActionResult UserBookingList()
        {
            var userId = Convert.ToInt32(Session["UserId"]);
            var userBooking = _db.tblBookings.Where(u => u.UserId == userId).ToList();
            
            List<BookingViewModel> list = new List<BookingViewModel>();
          
            int i = 0;
            foreach (var item in userBooking)
            {
                
                list.Add(new BookingViewModel()
                {
                    SN = i + 1,
                    BookingId = item.BookingId,
                    BookingStatus = item.BookingStatus,
                    PickUpDate = item.PickUpDate,
                    DropOffDate = item.DropOffDate,
                    VehiclePhoto = item.tblItem.VehiclePhoto,
                    TotalAmount = item.TotalAmount,
                    AmountPaid = item.AmountPaid,
                   


                }) ;
                i++;
            }
                return PartialView("_UserBookingList", list);

        }
        [HttpGet]
        public ActionResult Cancel(int id)
        {
            var bookingDetails = _db.tblBookings.Where(u => u.BookingId == id).FirstOrDefault();
            bookingDetails.BookingStatus = "Cancelled";
            bookingDetails.tblItem.VehicleStatus = "Available";
            _db.SaveChanges();
            
            return View("UserIndex");

        }
        [HttpGet]
        public ActionResult TestimonyCreate()
        {
           
            var userId = Convert.ToInt32(Session["UserId"]);
            var testimony = _db.tblTestimonies.Where(u => u.UserId == userId).FirstOrDefault();
            TestimonyViewModel tvm = new TestimonyViewModel();
            
            if(testimony != null)
            {
                tvm.TestimonyDescription = testimony.TestimonyDescription;
            }
            else
            {
                tvm.TestimonyDescription = "";
            }
            return PartialView("_TestimonyCreate", tvm);

        }
        [HttpPost]
        public ActionResult TestimonyCreate(TestimonyViewModel tvm)
        {
           
            var userId = Convert.ToInt32(Session["UserId"]);
            tblTestimony tb = new tblTestimony();
            tb.UserId = userId;
            tb.TestimonyDescription = tvm.TestimonyDescription;
            tb.Location = "Kathmandu, Nepal";
            _db.tblTestimonies.Add(tb);
            _db.SaveChanges();
            return View();

        }
      
        [HttpPost]
        public ActionResult TestimonyEdit(TestimonyViewModel tvm)
        {
            tblTestimony tb = new tblTestimony();
            tb.TestimonyId = tvm.TestimonyId;
            tb.TestimonyDescription = tvm.TestimonyDescription;
            _db.tblTestimonies.Add(tb);
            _db.SaveChanges();
            return PartialView("_TestimonyCreate");

        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Shop(int? page, int? id)
        {
           
            if(id == null)
            {
                var vehicle = _db.tblItems.ToList().ToPagedList(page ?? 1, 9);

                return View(vehicle);
            }
            else
            {
                var vehicle = _db.tblItems.Where(u => u.tblCategory.VehicleCategoryId == id).ToList().ToPagedList(page ?? 1, 9);
                return View(vehicle);
            }

          
        }
        public static int x = 8;
        public ActionResult SeeMore()
        {
            if (x <= 12)
            {
                x = x + 5;
                var lst = _db.tblItems.Take(x).ToList();
                Thread.Sleep(2000);
                return PartialView("_SeeMore", lst);
            }
            else
            {
                x = 8;
                var vehicle = _db.tblItems.ToList();
                Thread.Sleep(2000);
                return RedirectToAction("Shop");
            }
          
        }

        public ActionResult CategoryList()
        {
            return PartialView("_CategoryList", _db.tblCategories.ToList());
        }
        public ActionResult DateRange()
        {
            return PartialView("_DateRange");
        }
        [HttpPost]
        public ActionResult DateRange(ItemViewModel ivm)
        {
            int i = 0;
            List<BookingViewModel> list = new List<BookingViewModel>();
            var items = _db.tblBookings.ToList();

            foreach (var item in items)
            {
                list.Add(new BookingViewModel()
                {

                    PickUpDate = item.PickUpDate,
                    DropOffDate = item.DropOffDate,


                });
                i++;
                var vehicle = _db.tblItems.Where(u => ivm.Start != Convert.ToDateTime(item.PickUpDate) && ivm.End != Convert.ToDateTime(item.DropOffDate)).ToList();
            }
           
                return PartialView("_DateRange");
        }
    }
}