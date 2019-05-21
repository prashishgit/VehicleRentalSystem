using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return PartialView("_Vehicle", _db.tblItems.ToList());
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


       

        [Authorize(Roles = "User")]
        public ActionResult UserIndex()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Shop()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}