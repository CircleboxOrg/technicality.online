using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Technicality.online.Controllers
{
    [RequireHttps]
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AspNet()
        {
            return View();
        }

        public IActionResult AwsAzureCloud()
        {
            return View();
        }

        public IActionResult CreditCardProcessing()
        {
            return View();
        }

        public IActionResult DevOps()
        {
            return View();
        }

        public IActionResult EmailBlasts()
        {
            // redirect to Notifications
            return new RedirectToActionResult("Notifications", "Showroom", null);
            
        }

        public IActionResult GoogleAnalytics()
        {
            return View();
        }

        public IActionResult Https()
        {
            return View();
        }

        public IActionResult IntegratedLogins()
        {
            return View();
        }

        public IActionResult IntegratingApplications()
        {
            return View();
        }

        public IActionResult Mobile()
        {
            return View();
        }

        public IActionResult Notifications()
        {
            return View();
        }

        public IActionResult Recaptcha()
        {
            return View();
        }

        public IActionResult SqlServer()
        {
            return View();
        }
    }
}