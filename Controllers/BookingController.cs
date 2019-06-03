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
    public class BookingController : Controller
    {
        // GET: Booking
        VehicleRentalDBEntities _db = new VehicleRentalDBEntities();
        [HttpGet]
        public ActionResult IndexBooking()
        {

            int i = 0;
            List<BookingViewModel> list = new List<BookingViewModel>();
            var items = _db.tblBookings.ToList();

            foreach (var item in items)
            {
                var users = _db.tblUsers.Where(u => u.UserId == item.UserId).FirstOrDefault();
                var vehicle = _db.tblItems.Where(u => u.VehicleId == item.VehicleId).FirstOrDefault();
                list.Add(new BookingViewModel()
                {
                    BookingId = item.BookingId,
                    SN = i + 1,
                    UserName = users.UserName,
                    VehicleTitle = vehicle.VehicleTitle,
                    VehiclePhoto = vehicle.VehiclePhoto,
                    PickUpDate = item.PickUpDate,
                    DropOffDate = item.DropOffDate,
                    TotalAmount = item.TotalAmount,
                    AmountPaid = item.AmountPaid,
                    CitizenshipPhoto = item.CitizenshipPhoto,
                    BookingStatus = item.BookingStatus
                });
                i++;
            }
            return View(list);
        }
        //public ActionResult ManageBooking()
        //{
        //    return View();
        //}
        //[HttpGet]
        //public JsonResult GetData()
        //{
        //    using (VehicleRentalDBEntities db = new VehicleRentalDBEntities())
        //    {
        //        db.Configuration.LazyLoadingEnabled = false;
        //        List<BookingViewModel> lstitem = new List<BookingViewModel>();
        //        var lst = db.tblBookings.ToList();
        //        foreach (var item in lst)
        //        {
        //            var users = _db.tblUsers.Where(u => u.UserId == item.UserId).FirstOrDefault();
        //            var vehicle = _db.tblItems.Where(u => u.VehicleId == item.VehicleId).FirstOrDefault();
        //            lstitem.Add(new BookingViewModel() { BookingId = item.BookingId, VehiclePhoto=vehicle.VehiclePhoto, UserName = users.UserName, VehicleTitle = vehicle.VehicleTitle, PickUpDate = item.PickUpDate, DropOffDate = item.DropOffDate, TotalAmount = item.TotalAmount, CitizenshipPhoto = item.CitizenshipPhoto, AmountPaid = item.AmountPaid, BookingStatus = item.BookingStatus });
        //        }
        //        return Json(new { data = lstitem }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //[HttpGet]
        //public ActionResult Edit(int id)
        //{

        //        using (VehicleRentalDBEntities db = new VehicleRentalDBEntities())
        //        {
        //            ViewBag.Action = "Edit Item";

        //            tblBooking item = db.tblBookings.Where(i => i.BookingId == id).FirstOrDefault();
        //            BookingViewModel itemvm = new BookingViewModel();


        //        itemvm.AmountPaid = item.AmountPaid;
        //        itemvm.TotalAmount = item.TotalAmount;
        //        itemvm.AmountLeft = Convert.ToInt32(item.TotalAmount - item.AmountPaid);





        //            return View(itemvm);
        //        }
        //    }


        [HttpGet]
        public ActionResult Create(BookingViewModel bvmm)
        {

           
           
         
            BookingViewModel bvm = new BookingViewModel();

            bvm.PickUpDate = bvmm.PickUpDate;
            bvm.DropOffDate = bvmm.DropOffDate;
            bvm.VehicleId = bvmm.VehicleId;
            bvm.VehiclePhoto = bvmm.VehiclePhoto;
            bvm.VehicleTitle = bvmm.VehicleTitle;
            bvm.VehiclePrice = bvmm.VehiclePrice;
            int total = Convert.ToInt32(bvmm.VehiclePrice);
            DateTime pickupday = Convert.ToDateTime(bvm.PickUpDate);
            DateTime dropofday = Convert.ToDateTime(bvm.DropOffDate);
            var days = (dropofday - pickupday).Days;
            bvm.TotalAmount = total * days;
            bvm.Days = days;
           

            return View(bvm);
        }

        [HttpPost]
        public ActionResult Create_Post(BookingViewModel bvm, MailModel objModelMail)
        {
            tblBooking tb = new tblBooking();

            tb.VehicleId = bvm.VehicleId;
            tb.UserId = Convert.ToInt32(@Session["UserId"]);

            tb.PickUpDate = bvm.PickUpDate;
            tb.DropOffDate = bvm.DropOffDate;
            tb.TotalAmount = bvm.TotalAmount;
            tb.AmountPaid = 0;
            tb.BookingStatus = "Pending";
            HttpPostedFileBase fup = Request.Files["CitizenshipPhoto"];
            if (fup != null)
            {
                tb.CitizenshipPhoto = fup.FileName;
                fup.SaveAs(Server.MapPath("~/images/CitizenshipPhoto/" + fup.FileName));
            }

            _db.tblBookings.Add(tb);
            _db.SaveChanges();
            var email = @Session["Email"].ToString();


            if (tb != null)
            {
                if (ModelState.IsValid)
                {
                    //https://www.google.com/settings/security/lesssecureapps
                    //Make Access for less secure apps=true

                    string from = "vehiclerentalsystem09@gmail.com";
                    objModelMail.To = email;
                    using (MailMessage mail = new MailMessage(from, objModelMail.To))
                    {
                        try
                        {
                            mail.Subject = "Booking Details";
                            mail.Body = "To Confirm you booking please visit our office for the partial payment of Total Amount:";

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
        public ActionResult Edit(int id)
        {
            var booking = _db.tblBookings.Where(b => b.BookingId == id).FirstOrDefault();
            BookingViewModel bvm = new BookingViewModel();
            bvm.BookingId = booking.BookingId;
            bvm.TotalAmount = booking.TotalAmount;
            bvm.AmountPaid = booking.AmountPaid;
            bvm.BookingStatus = booking.BookingStatus;
            bvm.AmountLeft = Convert.ToInt32(bvm.TotalAmount - booking.AmountPaid);

            return View(bvm);
        }
        [HttpPost]
        public ActionResult Edit(BookingViewModel bvmm)
        {

            var booking = _db.tblBookings.Where(b => b.BookingId == bvmm.BookingId).FirstOrDefault();
            
            booking.TotalAmount = bvmm.TotalAmount;
            booking.AmountPaid = bvmm.AmountPaid;
            if (booking.TotalAmount != booking.AmountPaid)
            {
                booking.BookingStatus = "Confirm";
            }else
            {
                booking.BookingStatus = "Checked Out";
            }
          
            _db.SaveChanges();
            return RedirectToAction("IndexBooking","Booking");
        }


    }
}