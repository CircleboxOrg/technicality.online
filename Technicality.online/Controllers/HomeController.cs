using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Technicality.online.Models;
using System.Net;
using System.Text;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Technicality.online.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult MVPs()
        {
            return View();
        }

        public IActionResult FAQs()
        {
            return View();
        }

        public IActionResult Process()
        {
            return View();
        }

        public IActionResult VerifyRecaptcha(string token)
        {
            // validate reCAPTCHA result
            var request = (HttpWebRequest)WebRequest.Create("https://www.google.com/recaptcha/api/siteverify");
            var reCAPTCHAsecret = _configuration["reCAPTCHAsecret"];
            if (reCAPTCHAsecret == null)
            {
                ViewData.Model = "";
                return PartialView("_VerifiedRecaptcha");
            }

            var postData = "secret=" + reCAPTCHAsecret;
            postData += "&response=" + token;
            postData += "&remoteip=" + Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            RecaptchaSiteVerifyResponse deserialized = JsonSerializer.Deserialize<RecaptchaSiteVerifyResponse>(responseString);
            if (deserialized.score >= .5)
            {
                ViewData.Model = "For more information, contact Jeff at <a href='mailto: jeff @jefftrotman.com'>jeff@jefftrotman.com</a>.";
            }
            else
            {
                ViewData.Model = "<a href='./Services/Recaptcha'>ReCAPTCHA</a> thinks you are a bot, so email address not displayed.";
            }
            return PartialView("_VerifiedRecaptcha");
        }

        public IActionResult Sitemap()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
            sb.AppendLine("<url><loc>https://technicality.online</loc><lastmod>2020-06-10</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Home/MVPs</loc><lastmod>2020-06-10</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Home/FAQs</loc><lastmod>2020-06-10</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Home/About</loc><lastmod>2020-06-10</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Home/Privacy</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Projects</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Projects/Circlebox</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Projects/RcmChannels</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Projects/BallparkPilgrims</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Projects/GoogleAnalyticsWordPressPlugin</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Projects/PitchCounter</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Projects/AtlasKegScanner</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Toolbox</loc><lastmod>2020-06-10</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Toolbox/AspNet</loc><lastmod>2020-06-10</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Toolbox/AwsAzureCloud</loc><lastmod>2020-06-10</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Toolbox/CreditCardProcessing</loc><lastmod>2020-06-10</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Toolbox/DevOps</loc><lastmod>2020-06-10</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Toolbox/GoogleAnalytics</loc><lastmod>2020-06-10</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Toolbox/Https</loc><lastmod>2020-06-10</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Toolbox/IntegratedLogins</loc><lastmod>2020-06-10</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Toolbox/IntegratedApplications</loc><lastmod>2020-06-10</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Toolbox/Mobile</loc><lastmod>2020-06-10</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Toolbox/Notifications</loc><lastmod>2020-06-10</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Toolbox/Recaptcha</loc><lastmod>2020-06-10</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Toolbox/SqlServer</loc><lastmod>2020-06-10</lastmod></url>");
            sb.AppendLine("</urlset>");

            return Content(sb.ToString(), "text/xml", System.Text.Encoding.UTF8);
        }

        public IActionResult Consultation()
        {
            return Redirect("https://app.acuityscheduling.com/schedule.php?owner=13759965");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }

    public class RecaptchaSiteVerifyResponse
    {
        public bool success { get; set; }
        public double score { get; set; }
        public string action { get; set; }
        public string challenge_ts { get; set; }
        public string hostname { get; set; }
        public string errorcodes { get; set; }
    }
}
