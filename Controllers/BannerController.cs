using Project.Models;
using Project.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class BannerController : Controller
    {
        VehicleRentalDBEntities _db = new VehicleRentalDBEntities();
        // GET: Banner
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            int i = 0;
            List<BannerViewModel> list = new List<BannerViewModel>();
            var banners = _db.tblBanners.ToList();
            foreach (var item in banners)
            {
                list.Add(new BannerViewModel()
                {
                    SN = i + 1,
                    BannerId = item.BannerId,
                    Title = item.Title,
                    Description = item.Description,
                    HeadingOne = item.HeadingOne,
                    HeadingTwo = item.HeadingTwo,
                    Photo = item.Photo

                }) ;
                i++;
            }
            return View(list);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(BannerViewModel bvm)
        {
            tblBanner tb = new tblBanner();

            tb.Title = bvm.Title;
            tb.HeadingOne = bvm.HeadingOne;
            tb.HeadingTwo = bvm.HeadingTwo;
            tb.Description = bvm.Description;
            HttpPostedFileBase fup = Request.Files["Photo"];
            if (fup != null)
            {
                tb.Photo = fup.FileName;
                fup.SaveAs(Server.MapPath("~/images/Banner/" +fup.FileName));
            }
            _db.tblBanners.Add(tb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var banners = _db.tblBanners.Where(b => b.BannerId == id).FirstOrDefault();
            BannerViewModel bvm = new BannerViewModel();
            bvm.BannerId = banners.BannerId;
            bvm.Title = banners.Title;
            bvm.Description = banners.Description;
            bvm.HeadingOne = banners.HeadingOne;
            bvm.HeadingTwo = banners.HeadingTwo;
            bvm.Photo = banners.Photo;
            return View(bvm);
        }
        [HttpPost]
        public ActionResult Edit(BannerViewModel bvmm)
        {
            var banners = _db.tblBanners.Where(b => b.BannerId == bvmm.BannerId).FirstOrDefault();
            BannerViewModel bvm = new BannerViewModel();

            banners.Title = bvmm.Title;
            banners.Description = bvmm.Description;
            banners.HeadingOne = bvmm.HeadingOne;
            banners.HeadingTwo = bvmm.HeadingTwo;
            HttpPostedFileBase fup = Request.Files["Photo"];
            if (fup != null)
            {
                if (fup.FileName != null)
                {
                    System.IO.File.Delete(Server.MapPath("~/images/Banner" + bvmm.Photo));
                    banners.Photo = fup.FileName;
                    fup.SaveAs(Server.MapPath("~/images/Banner/" + fup.FileName));
                }
               
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
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
        public ActionResult Delete_Post(int id)
        {
            tblBanner tb = _db.tblBanners.Where(u => u.BannerId == id).FirstOrDefault();
            _db.tblBanners.Remove(tb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}