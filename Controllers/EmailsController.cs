using Project.Models;
using Project.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class EmailsController : Controller
    {
        // GET: Email
        VehicleRentalDBEntities _db = new VehicleRentalDBEntities();
        public ActionResult Emails()
        {
            ViewBag.EmailNumber = _db.tblContacts.Count();
            int i = 0;
            List<ContactViewModel> list = new List<ContactViewModel>();
            var contacts = _db.tblContacts.ToList();
            foreach (var item in contacts)
            {
                list.Add(new ContactViewModel()
                {

                    ContactId = item.ContactId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Subject = item.Subject,
                    Email = item.Email,
                    Message = item.Message
                });
                i++;
            }
            return PartialView("_Emails", list);
        }
    }
}