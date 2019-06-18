using Project.Models;
using Project.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class TestimonyController : Controller
    {
        VehicleRentalDBEntities _db = new VehicleRentalDBEntities();
        // GET: Testimony
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(TestimonyViewModel tvm)
        {
            tblTestimony tb = new tblTestimony();

           // tb.TestimonyDescription = tvm.TestimonyDescription;
           //tb.
           // tb.VehicleStatus = itm.VehicleStatus;
           // tb.Description = itm.Description;
           // HttpPostedFileBase fup = Request.Files["VehiclePhoto"];
           // if (fup != null)
           // {
           //     tb.VehiclePhoto = fup.FileName;
           //     fup.SaveAs(Server.MapPath("~/images/Vehicle/" + fup.FileName));
           // }
           // _db.tblItems.Add(tb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}