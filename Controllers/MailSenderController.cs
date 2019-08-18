using Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class MailSenderController : Controller
    {
        // GET: MailSender
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Index(MailModel objModelMail, HttpPostedFileBase fileUploader)
        {
            if (ModelState.IsValid)
            {
                //https://www.google.com/settings/security/lesssecureapps
                //Make Access for less secure apps=true

                string from = "vehiclerentalsystem09@gmail.com";
                using (MailMessage mail = new MailMessage(from, objModelMail.To))
                {
                    try
                    {
                        mail.Subject = objModelMail.Subject;
                        mail.Body = objModelMail.Body;
                        if (fileUploader != null)
                        {
                            string fileName = Path.GetFileName(fileUploader.FileName);
                            mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                        }
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
                    return View(objModelMail);
                }

            }
            else
            {
                return View();
            }
        }
    
    }
}