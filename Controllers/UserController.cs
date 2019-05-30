using Project.Models;
using Project.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class UserController : Controller
    {
        // GET: User


        VehicleRentalDBEntities _db = new VehicleRentalDBEntities();
        // GET: Banner
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<UserViewModel> list = new List<UserViewModel>();
            var users = _db.tblUsers.ToList();
            foreach (var item in users)
            {
                list.Add(new UserViewModel()
                {
                    UserId = item.UserId,
                    UserName = item.UserName,
                    Password = item.Password,

                    FullName = item.FullName,
                    Email = item.Email,
                    CitizenshipNumber = item.CitizenshipNumber
                });
            }
            
            return View(list);
        }
        [HttpGet]
        
        public ActionResult GetAllUser()
        {
            
            return View("_GetAllUser", _db.tblUsers.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.RoleName = _db.tblRoles.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserViewModel uvm)
        {
            var users = _db.tblUsers.Where(b => b.UserName == uvm.UserName || b.Email == uvm.Email).FirstOrDefault();
            if(users != null)
            {
                tblUser tb = new tblUser();
                tb.RoleId = uvm.RoleId;
                tb.UserName = uvm.UserName;
                tb.Password = uvm.Password;

                tb.FullName = uvm.FullName;
                tb.Email = uvm.Email;
                tb.CitizenshipNumber = uvm.CitizenshipNumber;

                _db.tblUsers.Add(tb);
                _db.SaveChanges();
                int latestUserId = tb.UserId;
                tblUserRole userRole = new tblUserRole();
                userRole.UserId = latestUserId;
                userRole.RoleId = tb.RoleId;
                _db.tblUserRoles.Add(userRole);
                _db.SaveChanges();
            }

           
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var users = _db.tblUsers.Where(b => b.UserId == id).FirstOrDefault();
            UserViewModel bvm = new UserViewModel();
            bvm.UserId = users.UserId;
            bvm.UserName = users.UserName;
            bvm.Password = users.Password;
          
            bvm.FullName = users.FullName;
            bvm.Email = users.Email;
            bvm.CitizenshipNumber = users.CitizenshipNumber;
            return View(bvm);
        }
        [HttpPost]
        public ActionResult Edit(UserViewModel uvm)
        {
            var users = _db.tblUsers.Where(b => b.UserId == uvm.UserId).FirstOrDefault();

            
            users.UserName = uvm.UserName;
            users.Password = uvm.Password;
            
            users.FullName = uvm.FullName;
            users.Email = uvm.Email;
            users.CitizenshipNumber = users.CitizenshipNumber;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Details(int id)
        {

            var users = _db.tblUsers.Where(b => b.UserId == id).FirstOrDefault();
            UserViewModel uvm = new UserViewModel();
            uvm.UserId = users.UserId;
            uvm.UserName = users.UserName;
            uvm.Password = users.Password;
            
            uvm.FullName = users.FullName;
            uvm.Email = users.Email;
            uvm.CitizenshipNumber = users.CitizenshipNumber;

            return View(uvm);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var user = _db.tblUsers.Where(b => b.UserId == id).FirstOrDefault();
            UserViewModel uvm = new UserViewModel();
            uvm.UserId = user.UserId;

            return View(uvm);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete_Post(UserViewModel uvm)
        {
            tblUser tb = new tblUser();
            UserViewModel bvm = new UserViewModel();
            tb.UserId = bvm.UserId;
            _db.tblUsers.Remove(tb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Retrive()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Retrive(UserViewModel uvm, MailModel objModelMail)
        {

            tblUser tb = _db.tblUsers.Where(e => e.Email == uvm.Email).FirstOrDefault();
            if(tb != null)
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
               
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");


        }
        [HttpGet]
        public ActionResult CreateUser()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(UserViewModel uvm)
        {
            var users = _db.tblUsers.Where(b => b.UserName == uvm.UserName || b.Email == uvm.Email).FirstOrDefault();
            if (users == null)
            {
                tblUser tb = new tblUser();
                tb.RoleId = 2;
                tb.UserName = uvm.UserName;
                tb.Password = uvm.Password;

                tb.FullName = uvm.FullName;
                tb.Email = uvm.Email;
                tb.CitizenshipNumber = uvm.CitizenshipNumber;

                _db.tblUsers.Add(tb);
                _db.SaveChanges();
                int latestUserId = tb.UserId;
                tblUserRole userRole = new tblUserRole();
                userRole.UserId = latestUserId;
                userRole.RoleId = 2;
                _db.tblUserRoles.Add(userRole);
                _db.SaveChanges();
            }
            else
            {
                ViewBag.Message ="Username is already Taken";
            }
          
            return RedirectToAction("Index");
        }

    }
}