using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPersonalPage.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string name, string message, string email)
        {
            new EmailService().SendAsync(new IdentityMessage { Destination = ConfigurationManager.AppSettings["ContactEmail"], Subject = "Webpage Message from " + name + " (" + email + ")", Body = message });

            return View();
        }
    }
}