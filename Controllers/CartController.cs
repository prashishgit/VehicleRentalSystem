using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class CartController : Controller
    {
        VehicleRentalDBEntities _db = new VehicleRentalDBEntities();
        private string strCart = "Cart";
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderNow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            if (Session[strCart] == null)
            {
                List<VehicleCart> lstCart = new List<VehicleCart>
                {
                    new VehicleCart(_db.tblItems.Find(id), 1)
                };
                Session[strCart] = lstCart;
                ViewBag.CartValue = lstCart.Count;
            }
            else
            {
                List<VehicleCart> lstCart = (List<VehicleCart>)Session[strCart];
                int check = IsExistingCheck(id);
                if (check == -1)
                {
                    lstCart.Add(new VehicleCart(_db.tblItems.Find(id), 1));
                }
                else
                {
                    ViewBag.CartMessage = "The Vehicle is already in your Cart";
                }
               
                Session[strCart] = lstCart;
                ViewBag.CartValue = lstCart.Count;
            }
            return View("Index");
        }
        private int  IsExistingCheck(int? id)
        {
            List<VehicleCart> lstCart = (List<VehicleCart>)Session[strCart];
            for (int i = 0; i < lstCart.Count; i++)
            {
                if (lstCart[i].Vehicle.VehicleId == id)
                {
                    return i;
                }
             
            }
            return -1;
        }
        public ActionResult Delete (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            int check = IsExistingCheck(id);
            List<VehicleCart> lstCart = (List<VehicleCart>)Session[strCart];
            lstCart.RemoveAt(check);
            ViewBag.CartValue = lstCart.Count;
            return View("Index");
        }
    }
}