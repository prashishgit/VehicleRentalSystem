using Project.Models;
using Project.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project.Controllers
{
    public class AccountController : Controller
    {
        VehicleRentalDBEntities _db = new VehicleRentalDBEntities();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel l, string ReturnUrl = "")
        {
            using (_db)
            {
                var users = _db.tblUsers.Where(u => u.UserName == l.UserName && u.Password == l.Password).FirstOrDefault();
                if (users != null)
                {
                    Session.Add("fullname", users.FullName);
                    Session["Email"] = users.Email;
                    
                    Session["UserId"] = users.UserId;
                    Session["Photo"] = users.Photo;
                    FormsAuthentication.SetAuthCookie(l.UserName, l.RememberMe);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User");
                }
            }

            return View();
        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
      
    }
}