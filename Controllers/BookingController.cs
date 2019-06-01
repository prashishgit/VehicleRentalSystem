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
        public ActionResult Index()
        {
            int i = 0;
            List<BookingViewModel> list = new List<BookingViewModel>();
            var items = _db.tblBookings.ToList();
           
            foreach (var item in items)
            {


                list.Add(new BookingViewModel()
                {

                    SN = i + 1,
                    VehicleId = item.VehicleId,
                   
                    PickUpDate = item.PickUpDate,
                    DropOffDate = item.DropOffDate,
                    TotalAmount = item.TotalAmount,
                    AmountPaid = item.AmountPaid,
                    CitizenshipPhoto = item.CitizenshipPhoto,
                    BookingStatus = item.BookingStatus
                }) ;
                i++;
            }
            return View(list);
        }
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


    }
        
       
}