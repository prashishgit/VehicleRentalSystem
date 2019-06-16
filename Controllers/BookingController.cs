using PayPal.Api;
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
       

        [Authorize]
        [HttpGet]
        public ActionResult Create(BookingViewModel bvmm)
        {

           
           
         
            BookingViewModel bvm = new BookingViewModel();

            var client = User.Identity.Name;
            var user = _db.tblUsers.Where(u => u.UserName == client).FirstOrDefault();
            var userId = user.UserId;
            var bookingUserId = _db.tblBookings.Where(u => u.UserId == userId).FirstOrDefault();
            if (bookingUserId != null)
            {
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
                bvm.CitizenshipPhoto = bookingUserId.CitizenshipPhoto;
            }else
            {
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
            }
            
            
         
           

            return View(bvm);
        }
        [Authorize]
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
                if (fup.FileName != "")
                {
                    tb.CitizenshipPhoto = fup.FileName;
                    fup.SaveAs(Server.MapPath("~/images/CitizenshipPhoto/" + fup.FileName));
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

                  
                }
                else
                {
                    BookingViewModel bvmm = new BookingViewModel();
                    bvmm.PickUpDate = bvm.PickUpDate;
                    bvmm.DropOffDate = bvm.DropOffDate;
                    bvmm.VehicleId = bvm.VehicleId;
                    bvmm.VehiclePhoto = bvm.VehiclePhoto;
                    bvmm.VehicleTitle = bvm.VehicleTitle;
                    bvmm.VehiclePrice = bvm.VehiclePrice;
                    int total = Convert.ToInt32(bvm.VehiclePrice);
                    DateTime pickupday = Convert.ToDateTime(bvm.PickUpDate);
                    DateTime dropofday = Convert.ToDateTime(bvm.DropOffDate);
                    var days = (dropofday - pickupday).Days;
                    bvmm.TotalAmount = total * days;
                    bvmm.Days = days;
                    return RedirectToAction("Create", "Booking", bvmm);
                }

            }
            return RedirectToAction("Index", "Home");



        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
           
            var booking = _db.tblBookings.Where(b => b.BookingId == id).FirstOrDefault();
            if (booking.BookingStatus != "Checked Out")
            {
                BookingViewModel bvm = new BookingViewModel();
                bvm.BookingId = booking.BookingId;
                bvm.TotalAmount = booking.TotalAmount;
                bvm.AmountPaid = booking.AmountPaid;
                bvm.BookingStatus = booking.BookingStatus;
                bvm.AmountLeft = Convert.ToInt32(bvm.TotalAmount - booking.AmountPaid);

                return View(bvm);
            }
            else
            {
                 BookingViewModel bvm = new BookingViewModel();
                bvm.BookingId = booking.BookingId;
                bvm.TotalAmount = booking.TotalAmount;
                bvm.AmountPaid = booking.AmountPaid;
                bvm.BookingStatus = booking.BookingStatus;
                bvm.AmountLeft = Convert.ToInt32(bvm.TotalAmount - booking.AmountPaid);

                return RedirectToAction("IndexBooking", "Booking");
            }
           
        }
        
        [HttpPost]
        public ActionResult Edit(BookingViewModel bvmm)
        {
            
            var booking = _db.tblBookings.Where(b => b.BookingId == bvmm.BookingId).FirstOrDefault();
            
            booking.TotalAmount = bvmm.TotalAmount;
            booking.AmountPaid = booking.AmountPaid + bvmm.Payment;
            if (booking.TotalAmount >= booking.AmountPaid)
            {
                if (booking.TotalAmount != booking.AmountPaid && booking.AmountPaid != 0)
                {
                    booking.BookingStatus = "Confirm";
                }
                else if(bvmm.AmountPaid == 0)
                {
                    booking.BookingStatus = "Pending";
                }
                else 
                {
                    booking.BookingStatus = "Checked Out";
                }
                _db.SaveChanges();
            }
            else
            {
                return RedirectToAction("Edit", "Booking");
            }
           
          
          
            return RedirectToAction("Edit","Booking");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var booking = _db.tblBookings.Where(b => b.BookingId == id).FirstOrDefault();
            var users = _db.tblUsers.Where(u => u.UserId == booking.UserId).FirstOrDefault();
            var vehicle = _db.tblItems.Where(u => u.VehicleId == booking.VehicleId).FirstOrDefault();
            BookingViewModel bvm = new BookingViewModel();
            bvm.BookingId = booking.BookingId;
            bvm.TotalAmount = booking.TotalAmount;
            bvm.UserName = users.UserName;
            bvm.VehicleTitle = vehicle.VehicleTitle;
            bvm.PickUpDate = booking.PickUpDate;
            
            bvm.DropOffDate = booking.DropOffDate;
            DateTime pickupday = Convert.ToDateTime(bvm.PickUpDate);
            DateTime dropofday = Convert.ToDateTime(bvm.DropOffDate);
            var days = (dropofday - pickupday).Days;
            bvm.Days = days;
            bvm.VehiclePrice = vehicle.VehiclePrice;
            bvm.AmountPaid = booking.AmountPaid;
            bvm.AmountLeft = Convert.ToInt32(booking.TotalAmount - booking.AmountPaid);
           

            return View(bvm);
        }



        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext
            APIContext apiContext = PaypalConfiguration.GetAPIContext();

            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal
                //Payer Id will be returned when payment proceeds or click to pay
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class

                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                                "/Booking/PaymentWithPayPal?";

                    //here we are generating guid for storing the paymentID received in session
                    //which will be used in the payment execution

                    var guid = Convert.ToString((new Random()).Next(100000));

                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment

                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

                    //get links returned from paypal in response to Create function call

                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
                else
                {

                    // This function exectues after receving all parameters for the payment

                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    //If executed payment failed then we will show payment failure message to user
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        ViewBag.Error = "Approved";
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }

            //on successful payment, show success page to user.
            return View("SuccessView");
        }

        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {

            //create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            //Adding Item Details like name, currency, price etc
            itemList.items.Add(new Item()
            {
                name = "Item Name comes here",
                currency = "USD",
                price = "1",
                quantity = "1",
                sku = "sku"
            });

            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            // Adding Tax, shipping and Subtotal details
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = "1"
            };

            //Final amount with details
            var amount = new Amount()
            {
                currency = "USD",
                total = "3", // Total must be equal to sum of tax, shipping and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();
            // Adding description about the transaction
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = "your generated invoice number", //Generate an Invoice No
                amount = amount,
                item_list = itemList
            });


            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }
    }
}