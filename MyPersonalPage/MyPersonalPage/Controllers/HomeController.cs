using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Configuration;

namespace MyPersonalPage.Controllers
{
    public class HomeController : Controller
    {
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

        public ActionResult JavaScriptDemo()
        {
            return View();
        }
    }
}