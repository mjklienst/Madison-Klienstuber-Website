using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

namespace KlienstuberWebsite.Controllers
{
    public class HomeController : Controller
    {
        public static bool isSent = false;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            if (isSent == true)
            {
                ViewBag.SentMessage = "Your Message Sent Successfully!";
            }
            return View();
        }

        public ActionResult Portfolio()
        {
            ViewBag.Message = "Your portfolio page.";

            return View();
        }

        public ActionResult NotFound()
        {
            ViewBag.Message = "Error Page!";

            return View();
        }

        public ActionResult SendMail()
        {
            //Always send to this email:
            string mainEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["mainEmail"];
            string recipient = mainEmail; //My main email
            string subject = Request["subject"]; //Get subject from html
            string body = "From: " + Request["email"] + ". Message: " + Request["body"]; //Get body from html

            //Set up connections from incoming email (my backup email account I will "send" emails from, and send to my main email, above)
            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.SmtpPort = 587;
            WebMail.SmtpUseDefaultCredentials = true;
            WebMail.EnableSsl = true;
            //Get my backup email user and pass from app secrets file
            string email = System.Web.Configuration.WebConfigurationManager.AppSettings["email"];
            WebMail.UserName = email;
            string pass = System.Web.Configuration.WebConfigurationManager.AppSettings["emailPass"];
            WebMail.Password = pass;

            //Send email from backup email to my active one
            WebMail.Send(to: recipient, subject: subject, body: body, isBodyHtml: true);

            isSent = true;
            ViewBag.MessageSent = true;
            ViewBag.SentMessage = "Sent!";
            return RedirectToAction("Contact");
        }

    }
}