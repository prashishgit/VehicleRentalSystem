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
using PagedList;
using PagedList.Mvc;
namespace Project.Controllers
{
    public class BookingController : Controller
    {
        public string strCart = "Cart";
        // GET: Booking
        VehicleRentalDBEntities _db = new VehicleRentalDBEntities();
        [HttpGet]
        public ActionResult IndexBooking(string search, int? page)
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
                    CitizenshipPhoto = users.CitizenshipPhoto,
                    PickUpDate = item.PickUpDate,
                    DropOffDate = item.DropOffDate,
                    TotalAmount = item.TotalAmount,
                    AmountPaid = item.AmountPaid,
                    BookingStatus = item.BookingStatus
                });
                i++;
            }
            return View(list.ToPagedList(page ?? 1, 10));
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
            if (user.CitizenshipPhoto != null)
            {
                bvm.UserId = userId;
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
                bvm.CitizenshipPhoto = user.CitizenshipPhoto;
            }
            else
            {
                bvm.UserId = userId;
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



        public ActionResult Create_Post(MailModel objModelMail)
        {
            List<Cart> lstCart = (List<Cart>)Session[strCart];
            foreach (var item in lstCart)
            {
                tblBooking tb = new tblBooking();
                var vehicle = _db.tblItems.Where(u => u.VehicleId == item.Vehicle.VehicleId).FirstOrDefault();
                tb.VehicleId = vehicle.VehicleId;
                tb.UserId = Convert.ToInt32(@Session["UserId"]);

                tb.PickUpDate = item.Vehicle.PickUpDate;
                tb.DropOffDate = item.Vehicle.DropOffDate;
                tb.TotalAmount = item.Vehicle.TotalAmount;
                tb.AmountPaid = item.Vehicle.AmountPaid;
                vehicle.VehicleStatus = "Booked";
                tb.BookingStatus = "Pending";
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
                    vehicle.VehicleStatus = "Booked";
                }
                else
                {

                    return RedirectToAction("PaymentWithPaypal", "Home");
                }
            }
            Session.Remove(strCart);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            var booking = _db.tblBookings.Where(b => b.BookingId == id).FirstOrDefault();
            var vehicle = _db.tblItems.Where(b => b.VehicleId == booking.VehicleId).FirstOrDefault();
            if (booking.BookingStatus != "Checked Out")
            {
                BookingViewModel bvm = new BookingViewModel();
                bvm.BookingId = booking.BookingId;
                bvm.VehicleId = vehicle.VehicleId;
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
                bvm.VehicleId = vehicle.VehicleId;
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
            var vehicle = _db.tblItems.Where(u => u.VehicleId == bvmm.VehicleId).FirstOrDefault();
            booking.TotalAmount = bvmm.TotalAmount;
            booking.AmountPaid = booking.AmountPaid + bvmm.Payment;
            if (booking.TotalAmount >= booking.AmountPaid)
            {
                if (booking.TotalAmount != booking.AmountPaid && booking.AmountPaid != 0)
                {
                    ViewBag.BookingConfirm = "Booking Confirmed";
                    booking.BookingStatus = "Confirm";
                }
                else if (bvmm.AmountPaid == 0)
                {
                    booking.BookingStatus = "Pending";
                }
                else
                {
                   
                    booking.BookingStatus = "Checked Out";
                    vehicle.VehicleStatus = "Available";

                }

                _db.SaveChanges();
            }
            else
            {
                ViewBag.FailedBooking = "Booking Confirmed Failed";
                return View();
            }


          
            return View();
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



        [HttpPost]
        public ActionResult OrderNow(BookingViewModel bvm)
        {


            var id = _db.tblItems.Where(x => x.VehicleId == bvm.VehicleId).FirstOrDefault();

            if (bvm == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session[strCart] == null)
            {
                List<Cart> lstCart = new List<Cart>
                {
                    new Cart(bvm, 1)
                };
                Session[strCart] = lstCart;
            }
            else
            {
                List<Cart> lstCart = (List<Cart>)Session[strCart];
                int check = IsExistingCheck(id.VehicleId);
                if (check == -1)
                {

                    lstCart.Add(new Cart(bvm, 1));
                }
                else
                {
                    lstCart[check].Quantity = 1;
                }

                Session[strCart] = lstCart;
               
            }
            var tb = _db.tblUsers.Where(x => x.UserId == bvm.UserId).FirstOrDefault();

            HttpPostedFileBase fup = Request.Files["CitizenshipPhoto"];
            if (fup != null)
            {
                if (fup.FileName != "")
                {
                    tb.CitizenshipPhoto = fup.FileName;
                    fup.SaveAs(Server.MapPath("~/images/CitizenshipPhoto/" + fup.FileName));
                    _db.SaveChanges();
                }
            }
            return View("Cart");
        }

        private int IsExistingCheck(int? id)
        {
            List<Cart> lstCart = (List<Cart>)Session[strCart];
            for (int i = 0; i < lstCart.Count; i++)
            {
                if (lstCart[i].Vehicle.VehicleId == id) return i;
            }
            return -1;
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
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Booking/PaymentWithPayPal?";

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
                ViewBag.Error = ex.Message;
                return View("SuccessView");
            }

            //on successful payment, show success page to user.
            return RedirectToAction("Create_Post", "Booking");
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
            List<Cart> listBookings = (List<Cart>)Session[strCart];

            foreach (var item in listBookings)
            {
                itemList.items.Add(new Item()
                {
                    name = item.Vehicle.VehicleTitle,
                    currency = "USD",
                    price = (Convert.ToInt32(item.Vehicle.VehiclePrice) * item.Vehicle.Days).ToString(),
                    quantity = item.Quantity.ToString(),
                    sku = "sku"
                });
            }

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


                subtotal = listBookings.Sum(x => Convert.ToInt32(x.Vehicle.AmountPaid) * x.Quantity).ToString()

            };

            //Final amount with details
            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToString(details.fee) + Convert.ToDouble(details.subtotal)).ToString(),
                details = details
            };

            var transactionList = new List<Transaction>();
            // Adding description about the transaction
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = Convert.ToString(new Random().Next(100000)), //Generate an Invoice No
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