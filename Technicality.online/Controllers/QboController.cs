using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Technicality.online.Controllers
{
    public class QboController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EULA()
        {
            return View();
        }

        public IActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}