using Project.Models;
using Project.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        VehicleRentalDBEntities _db = new VehicleRentalDBEntities();
        // GET: Banner
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            int i = 0;
            List<ItemViewModel> list = new List<ItemViewModel>();
            var items = _db.tblItems.ToList();
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
                }) ;
                i++;
            }
            return View(list);
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
            tb.VehicleStatus = itm.VehicleStatus;
            tb.Description = itm.Description;
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
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var vehicle = _db.tblItems.Where(b => b.VehicleId == id).FirstOrDefault();
            ItemViewModel ivm = new ItemViewModel();
            ivm.VehicleId = vehicle.VehicleId;
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
       
        [HttpGet]
        public ActionResult DetailsClient(int id)
        {
            var banners = _db.tblItems.Where(b => b.VehicleId == id).FirstOrDefault();
           
            ItemViewModel bvm = new ItemViewModel();
            bvm.VehicleId = id;
            bvm.VehicleTitle = banners.VehicleTitle;
            bvm.Description = banners.Description;
            bvm.VehicleStatus = banners.VehicleStatus;
            bvm.VehiclePrice = banners.VehiclePrice;
            bvm.VehiclePhoto = banners.VehiclePhoto;
           
            
            return View(bvm);
           
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
            var banners = _db.tblBanners.Where(b => b.BannerId == id).FirstOrDefault();
            BannerViewModel bvm = new BannerViewModel();
            bvm.BannerId = banners.BannerId;

            return View(bvm);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete_Post(BannerViewModel bvmm)
        {
            tblBanner tb = new tblBanner();
            BannerViewModel bvm = new BannerViewModel();
            tb.BannerId = bvm.BannerId;
            _db.tblBanners.Remove(tb);
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