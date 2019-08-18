using Project.Models;
using Project.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;


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
            ViewBag.UserNumber = _db.tblUsers.Count();
            foreach (var item in users)
            {
                list.Add(new UserViewModel()
                {
                    UserId = item.UserId,
                    UserName = item.UserName,
                    Password = item.Password,
                    RoleName = item.tblRole.RoleName,
                    FullName = item.FullName,
                    Email = item.Email,
                    Photo = item.Photo

                });
            }

            return View(list);
        }

        [HttpGet]

        public ActionResult GetAllUser()
        {

            return View("_GetAllUser", _db.tblUsers.ToList());
        }
        public JsonResult IsUserNameAvailable(string UserName)
         {
            return Json(!_db.tblUsers.Any(user => user.UserName == UserName), JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsEmailAvailable(string Email)
        {
            return Json(!_db.tblUsers.Any(user => user.Email == Email), JsonRequestBehavior.AllowGet);
        }
        public JsonResult EmailExists(string Email)
        {
            return Json(_db.tblUsers.Any(user => user.Email == Email), JsonRequestBehavior.AllowGet);
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

            tblUser tb = new tblUser();
            tb.RoleId = uvm.RoleId;
            tb.UserName = uvm.UserName;
            tb.Password = uvm.Password;

            tb.FullName = uvm.FullName;
            tb.Email = uvm.Email;
            HttpPostedFileBase fup = Request.Files["Photo"];
            if (fup != null)
            {
                if (fup.FileName != null)
                {
                    tb.Photo = fup.FileName;
                    fup.SaveAs(Server.MapPath("~/images/UserPhoto/" + fup.FileName));
                }

            }
            _db.tblUsers.Add(tb);
            _db.SaveChanges();
            int latestUserId = tb.UserId;
            tblUserRole userRole = new tblUserRole();
            userRole.UserId = latestUserId;
            userRole.RoleId = tb.RoleId;
            _db.tblUserRoles.Add(userRole);
            _db.SaveChanges();



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
            ViewBag.RoleName = _db.tblRoles.ToList();
            bvm.FullName = users.FullName;
            bvm.Email = users.Email;
            bvm.Photo = users.Photo;
            return View(bvm);
        }
        [HttpPost]
        public ActionResult Edit(UserViewModel uvm)
        {
            var users = _db.tblUsers.Where(b => b.UserId == uvm.UserId).FirstOrDefault();

            users.RoleId = uvm.RoleId;
            users.UserName = uvm.UserName;
            users.Password = uvm.Password;

            users.FullName = uvm.FullName;
            users.Email = uvm.Email;
            HttpPostedFileBase fup = Request.Files["Photo"];
            if (fup != null)
            {
                if (fup.FileName != null)
                {
                    System.IO.File.Delete(Server.MapPath("~/images/UserPhoto" + uvm.Photo));
                    users.Photo = fup.FileName;
                    fup.SaveAs(Server.MapPath("~/images/UserPhoto/" + fup.FileName));
                }

            }
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
            uvm.Photo = users.Photo;

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

        public ActionResult CreateUser()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(UserViewModel uvm)
        {

            tblUser tb = new tblUser();
            tb.RoleId = 2;
            tb.UserName = uvm.UserName;
            tb.Password = uvm.Password;

            tb.FullName = uvm.FullName;
            tb.Email = uvm.Email;
            HttpPostedFileBase fup = Request.Files["Photo"];
            if (fup != null)
            {
                if (fup.FileName != "")
                {
                    tb.Photo = fup.FileName;
                    fup.SaveAs(Server.MapPath("~/images/UserPhoto/" + fup.FileName));
                }
            }
            _db.tblUsers.Add(tb);
            _db.SaveChanges();
            int latestUserId = tb.UserId;
            tblUserRole userRole = new tblUserRole();
            userRole.UserId = latestUserId;
            userRole.RoleId = 2;
            _db.tblUserRoles.Add(userRole);
            _db.SaveChanges();
            ViewBag.Successfull = "User Created Successfully";


            return RedirectToAction("Index");
        }

    }
}