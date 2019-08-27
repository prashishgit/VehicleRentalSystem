using Project.Models;
using Project.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
namespace Project.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        VehicleRentalDBEntities _db = new VehicleRentalDBEntities();
        // GET: Banner
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string search, int? page)
        {
            int i = 0;
            List<ItemViewModel> list = new List<ItemViewModel>();
            var items = _db.tblItems.ToList();
            if (search != null)
            {
                foreach (var item in items)
                {
                    list.Add(new ItemViewModel()
                    {
                        SN = i + 1,
                        VehicleId = item.VehicleId,
                        VehicleTitle = item.VehicleTitle,
                        Description = item.Description,
                        VehiclePrice = item.VehiclePrice,
                        VehicleStatus = item.VehicleStatus,
                        VehiclePhoto = item.VehiclePhoto
                    });
                    i++;
                }
                return View(list.Where(x => x.VehicleTitle.Contains(search) || x.Description.Contains(search) || search == null).ToList().ToPagedList(page ?? 1, 10));
              
            }
            else
            {


              
                foreach (var item in items)
                {
                    list.Add(new ItemViewModel()
                    {
                        SN = i + 1,
                        VehicleId = item.VehicleId,
                        VehicleTitle = item.VehicleTitle,
                        Description = item.Description,
                        VehiclePrice = item.VehiclePrice,
                        VehicleStatus = item.VehicleStatus,
                        VehiclePhoto = item.VehiclePhoto
                    });
                    i++;
                }
                return View(list.ToPagedList(page ?? 1, 10));
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CategoryName = _db.tblCategories.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(ItemViewModel itm)
        {
            tblItem tb = new tblItem();

            tb.VehicleTitle = itm.VehicleTitle;
            tb.VehiclePrice = itm.VehiclePrice;
            tb.VehicleStatus = "Available";
            tb.Description = itm.Description;
            tb.VehicleCategoryId = itm.VehicleCategoryId;
            HttpPostedFileBase fup = Request.Files["VehiclePhoto"];
            if (fup != null)
            {
                tb.VehiclePhoto = fup.FileName;
                fup.SaveAs(Server.MapPath("~/images/Vehicle/" + fup.FileName));
            }
            _db.tblItems.Add(tb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult GetReservedDates(int id)
        {

            var dates = _db.tblBookings.ToList().Where(x => x.BookingStatus == "Pending" || x.BookingStatus == "Confirm");
            var bookedDates = dates.Where(x => x.VehicleId == id);

            return Json(new { bookedDates }, "text/x-json", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.CategoryName = _db.tblCategories.ToList();
            
            var vehicle = _db.tblItems.Where(b => b.VehicleId == id).FirstOrDefault();
            ItemViewModel ivm = new ItemViewModel();
            ivm.VehicleId = vehicle.VehicleId;
            ivm.VehicleCategoryId = vehicle.VehicleCategoryId;
            ivm.VehicleTitle = vehicle.VehicleTitle;
            ivm.Description = vehicle.Description;
            ivm.VehiclePrice = vehicle.VehiclePrice;
            ivm.VehicleStatus = vehicle.VehicleStatus;
            ivm.VehiclePhoto = vehicle.VehiclePhoto;
            return View(ivm);


        }
        [HttpPost]
        public ActionResult Edit(ItemViewModel ivm)
        {
            var vehicle = _db.tblItems.Where(b => b.VehicleId == ivm.VehicleId).FirstOrDefault();

            vehicle.VehicleCategoryId = ivm.VehicleCategoryId;
            vehicle.VehicleTitle = ivm.VehicleTitle;
            vehicle.Description = ivm.Description;
            vehicle.VehicleStatus = ivm.VehicleStatus;
            vehicle.VehiclePrice = ivm.VehiclePrice;
            HttpPostedFileBase fup = Request.Files["VehiclePhoto"];
            if (fup != null)
            {
                if (fup.FileName != null)
                {
                    System.IO.File.Delete(Server.MapPath("~/images/Vehicle" + ivm.VehiclePhoto));
                    vehicle.VehiclePhoto = fup.FileName;
                    fup.SaveAs(Server.MapPath("~/images/Vehicle/" + fup.FileName));
                }

            }
            _db.SaveChanges();
            ViewBag.EditSuccess = "Vehicle Editing is Complete";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {

            var banners = _db.tblItems.Where(b => b.VehicleId == id).FirstOrDefault();
            ItemViewModel bvm = new ItemViewModel();
            bvm.VehicleTitle = banners.VehicleTitle;
            bvm.Description = banners.Description;
            bvm.VehicleStatus = banners.VehicleStatus;
            bvm.VehiclePrice = banners.VehiclePrice;
            bvm.VehiclePhoto = banners.VehiclePhoto;

            return View(bvm);
        }
        //[HttpGet]
        //public ActionResult BookedDate(int id)
        //{
        //    var bookedDate = _db.tblBookings.Where()
        //    return View();
        //}
        [HttpGet]
        public ActionResult DetailsClient(int id)
        {

            var banners = _db.tblItems.Where(b => b.VehicleId == id).FirstOrDefault();
            if (banners == null)
            {
                return HttpNotFound();
            }
            var bookingDetails = _db.tblBookings.Where(x => x.VehicleId == id && (x.BookingStatus == "Confirm" || x.BookingStatus == "Pending")).FirstOrDefault();
            if (bookingDetails == null)
            {
                ItemViewModel bvm = new ItemViewModel();

                bvm.VehicleId = id;
                bvm.VehicleTitle = banners.VehicleTitle;
                bvm.Description = banners.Description;
                bvm.VehicleStatus = banners.VehicleStatus;
                bvm.VehiclePrice = banners.VehiclePrice;
                bvm.VehiclePhoto = banners.VehiclePhoto;

                ViewBag.ArticleId = id;
                var comments = _db.tblComments.Where(d => d.VehicleId == id).ToList();
                ViewBag.Comments = comments;
                var ratings = _db.tblComments.Where(d => d.VehicleId == id).ToList();
                if (ratings.Count() > 0)
                {
                    var ratingSum = ratings.Sum(d => d.Rating.Value);
                    ViewBag.RatingSum = ratingSum;
                    var ratingCount = ratings.Count();
                    ViewBag.RatingCount = ratingCount;
                }
                else
                {
                    ViewBag.RatingSum = 0;
                    ViewBag.RatingCount = 0;
                }

                //from database
                var disabledDates = new List<string> { "08/20/2019 00:01", "08/21/2019 00:01", "08/22/2019 00:01" };

                ViewBag.DisabledDates = disabledDates;

                return View(bvm);
            }
            else
            {
                ItemViewModel bvm = new ItemViewModel();

                bvm.VehicleId = id;
                bvm.VehicleTitle = banners.VehicleTitle;
                bvm.Description = banners.Description;
                bvm.VehicleStatus = banners.VehicleStatus;
                bvm.VehiclePrice = banners.VehiclePrice;
                bvm.VehiclePhoto = banners.VehiclePhoto;
                bvm.PickUpDate = Convert.ToDateTime(bookingDetails.PickUpDate);
                bvm.DropOffDate = Convert.ToDateTime(bookingDetails.DropOffDate);
                ViewBag.PickUpDate = bvm.PickUpDate.ToString("dddd, dd MMMM yyyy");
                ViewBag.DropOffDate = bvm.DropOffDate.ToString("dddd, dd MMMM yyyy");
                ViewBag.pt = bvm.PickUpDate.ToString("yyyy-MM-dd");
                ViewBag.dt = bvm.DropOffDate.ToString("yyyy-MM-dd");
                ViewBag.ArticleId = id;
                var comments = _db.tblComments.Where(d => d.VehicleId == id).ToList();
                ViewBag.Comments = comments;
                var ratings = _db.tblComments.Where(d => d.VehicleId == id).ToList();
                if (ratings.Count() > 0)
                {
                    var ratingSum = ratings.Sum(d => d.Rating.Value);
                    ViewBag.RatingSum = ratingSum;
                    var ratingCount = ratings.Count();
                    ViewBag.RatingCount = ratingCount;
                }
                else
                {
                    ViewBag.RatingSum = 0;
                    ViewBag.RatingCount = 0;
                }

                //from database
                var disabledDates = new List<string> { "08/15/2019 00:01", "08/16/2019 00:01", "08/17/2019 00:01" };

                ViewBag.DisabledDates = disabledDates;

                return View(bvm);
            }


        }

        [HttpPost]
        public ActionResult DetailsClient(BookingViewModel bvmm)
        {


            BookingViewModel bvm = new BookingViewModel();
         
            bvm.VehicleId = bvmm.VehicleId;
            bvm.PickUpDate = bvmm.PickUpDate;
            bvm.DropOffDate = bvmm.DropOffDate;
            bvm.VehiclePhoto = bvmm.VehiclePhoto;
            bvm.VehicleTitle = bvmm.VehicleTitle;
            bvm.VehiclePrice = bvmm.VehiclePrice;
            return RedirectToAction("Create", "Booking", bvm);
        }
       
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var vehicle = _db.tblItems.Where(b => b.VehicleId == id).FirstOrDefault();
            ItemViewModel ivm = new ItemViewModel();
            ivm.VehicleTitle = vehicle.VehicleTitle;
            ivm.Description = vehicle.Description;
            ivm.VehiclePrice = vehicle.VehiclePrice;
            ivm.CategoryName = vehicle.tblCategory.CategoryName.ToString();
            ivm.VehicleStatus = vehicle.VehicleStatus;
            ivm.VehiclePhoto = vehicle.VehiclePhoto;
            return View(ivm);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete_Post(int id)
        {
            tblItem tb = _db.tblItems.Where(u => u.VehicleId == id).FirstOrDefault();
            _db.tblItems.Remove(tb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Rate(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var article = _db.tblItems.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArticleId = id.Value;
            CommentViewModel cvm = new CommentViewModel();

            var comments = _db.tblComments.Where(d => d.VehicleId == id).ToList();
            ViewBag.Comments = comments;


            var ratings = _db.tblComments.Where(d => d.VehicleId == id).ToList();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.Rating.Value);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }

            return View(article);
        }


    }
}