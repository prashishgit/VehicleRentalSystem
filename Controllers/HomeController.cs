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
        public ActionResult Vehicle()
        {
            return PartialView("_Vehicle", _db.tblItems.OrderBy(r => Guid.NewGuid()).Take(8));
        }
       
        [AllowAnonymous]
        public ActionResult Testimony()
        {
            return PartialView("_Testimony");
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
        public ActionResult TestimonyCreate()
        {
            
            return PartialView("_TestimonyCreate");

        }
        [HttpPost]
        public ActionResult TestimonyCreate(TestimonyViewModel tvm)
        {
            var userId = Convert.ToInt32(Session["UserId"]);
            tblTestimony tb = new tblTestimony();
            tb.UserId = userId;
            tb.TestimonyDescription = tvm.TestimonyDescription;
            _db.tblTestimonies.Add(tb);
            _db.SaveChanges();
            return PartialView("Index");

        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Shop(int? page, int id)
        {
           
            if(id == 0)
            {
                var vehicle = _db.tblItems.ToList().ToPagedList(page ?? 1, 9);

                return View(vehicle);
            }
            else
            {
                var vehicle = _db.tblItems.Where(u => u.tblCategory.VehicleCategoryId == id).ToList();
                return View(vehicle);
            }

          
        }
        public ActionResult CategoryList()
        {
            return PartialView("_CategoryList", _db.tblCategories.ToList());
        }
    }
}