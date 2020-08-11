using Project.Models;
using Project.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class CategoryController : Controller
    {
        VehicleRentalDBEntities db = new VehicleRentalDBEntities();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            using (VehicleRentalDBEntities db = new VehicleRentalDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<CategoryViewModel> lst = new List<CategoryViewModel>();
                var catList = db.tblCategories.ToList();
                foreach (var item in catList)
                {
                    lst.Add(new CategoryViewModel() { VehicleCategoryId = item.VehicleCategoryId, CategoryName = item.CategoryName });
                }
                return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Add(int id = 0)
        {
            if (id == 0)
                return View(new tblCategory());
            else
            {
                using (VehicleRentalDBEntities db = new VehicleRentalDBEntities())
                {
                    return View(db.tblCategories.Where(x => x.VehicleCategoryId == id).FirstOrDefault<tblCategory>());
                }
            }
        }

        [HttpPost]
        public ActionResult Add(tblCategory emp)
        {
            using (VehicleRentalDBEntities db = new VehicleRentalDBEntities())
            {
                if (emp.VehicleCategoryId == 0)
                {
                    db.tblCategories.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }



        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (VehicleRentalDBEntities db = new VehicleRentalDBEntities())
            {
                tblCategory sm = db.tblCategories.Where(x => x.VehicleCategoryId == id).FirstOrDefault();
                db.tblCategories.Remove(sm);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}