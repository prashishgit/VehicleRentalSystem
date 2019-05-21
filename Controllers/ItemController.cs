using Project.Models;
using Project.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        VehicleRentalDBEntities _db = new VehicleRentalDBEntities();
        // GET: Banner
        public ActionResult Index()
        {
            List<ItemViewModel> list = new List<ItemViewModel>();
            var items = _db.tblItems.ToList();
            foreach (var item in items)
            {
                list.Add(new ItemViewModel()
                {
                    ItemId = item.ItemId,
                    ItemTitle = item.ItemTitle,
                    Description = item.Description,
                    ItemPrice = item.ItemPrice,
                    ItemStatus = item.ItemStatus,
                    ItemPhoto = item.ItemPhoto
                });
            }
            return View(list);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ItemViewModel itm)
        {
            tblItem tb = new tblItem();

            tb.ItemTitle = itm.ItemTitle;
            tb.ItemPrice = itm.ItemPrice;
            tb.ItemStatus = itm.ItemStatus;
            tb.Description = itm.Description;
            HttpPostedFileBase fup = Request.Files["ItemPhoto"];
            if (fup != null)
            {
                tb.ItemPhoto = fup.FileName;
                fup.SaveAs(Server.MapPath("~/images/Vehicle/" + fup.FileName));
            }
            _db.tblItems.Add(tb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var vehicle = _db.tblItems.Where(b => b.ItemId == id).FirstOrDefault();
            ItemViewModel ivm = new ItemViewModel();
            ivm.ItemId = vehicle.ItemId;
            ivm.ItemTitle = vehicle.ItemTitle;
            ivm.Description = vehicle.Description;
            ivm.ItemPrice = vehicle.ItemPrice;
            ivm.ItemStatus = vehicle.ItemStatus;
            ivm.ItemPhoto = vehicle.ItemPhoto;
            return View(ivm);
        
       
        }
        [HttpPost]
        public ActionResult Edit(ItemViewModel ivm)
        {
            var vehicle = _db.tblItems.Where(b => b.ItemId == ivm.ItemId).FirstOrDefault();
           

            vehicle.ItemTitle = ivm.ItemTitle;
            vehicle.Description = ivm.Description;
            vehicle.ItemStatus = ivm.ItemStatus;
            vehicle.ItemPrice = ivm.ItemPrice;
            HttpPostedFileBase fup = Request.Files["ItemPhoto"];
            if (fup != null)
            {
                if (fup.FileName != null)
                {
                    System.IO.File.Delete(Server.MapPath("~/images/Vehicle" + ivm.ItemPhoto));
                    vehicle.ItemPhoto = fup.FileName;
                    fup.SaveAs(Server.MapPath("~/images/Vehicle/" + fup.FileName));
                }

            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {

            var banners = _db.tblBanners.Where(b => b.BannerId == id).FirstOrDefault();
            BannerViewModel bvm = new BannerViewModel();
            bvm.Title = banners.Title;
            bvm.Description = banners.Description;
            bvm.HeadingOne = banners.HeadingOne;
            bvm.HeadingTwo = banners.HeadingTwo;
            bvm.Photo = banners.Photo;

            return View(bvm);
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

    }
}