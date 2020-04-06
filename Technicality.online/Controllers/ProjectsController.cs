using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Technicality.online.Controllers
{
    [RequireHttps]
    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Circlebox()
        {
            return View();
        }

        public IActionResult RcmChannels()
        {
            return View();
        }
        public IActionResult BallparkPilgrims()
        {
            return View();
        }


        public IActionResult GoogleAnalyticsWordPressPlugin()
        {
            return View();
        }

        public IActionResult PitchCounter()
        {
            return View();
        }

        public IActionResult AtlasKegScanner()
        {
            return View();
        }

    }
}