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
        [HttpGet]
        public ActionResult Retrive()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Retrive(RetriveViewModel l, MailModel objModelMail)
        {
           
            tblUser tb = _db.tblUsers.Where(e => e.Email == l.Email).FirstOrDefault();
            if (tb != null)
            {
                
                if (ModelState.IsValid)
                {
                    //https://www.google.com/settings/security/lesssecureapps
                    //Make Access for less secure apps=true

                    string from = "vehiclerentalsystem09@gmail.com";
                    objModelMail.To = tb.Email;
                    using (MailMessage mail = new MailMessage(from, objModelMail.To))
                    {
                        try
                        {
                            mail.Subject = "Your Password";
                            mail.Body = tb.Password;

                            mail.IsBodyHtml = false;
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "smtp.gmail.com";
                            smtp.EnableSsl = true;
                            NetworkCredential networkCredential = new NetworkCredential(from, "159753159753p");
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = networkCredential;
                            smtp.Port = 587;
                            smtp.Send(mail);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            ViewBag.Message = "Sent";
                        }

                    }

                }

            }
            else
            {

                return View();
            }

            //return RedirectToAction("Index", "Home");
            return View();
        }
    }
}