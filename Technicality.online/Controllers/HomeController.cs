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

namespace Technicality.online.Controllers
{
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
            RecaptchaSiteVerifyResponse deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<RecaptchaSiteVerifyResponse>(responseString);
            if (deserialized.score >= .5)
            {
                ViewData.Model = "For more information, contact Jeff at <a href='mailto: jeff @jefftrotman.com'>jeff@jefftrotman.com</a>.";
            }
            else
            {
                ViewData.Model = "<a href='./Showroom/Recaptcha'>ReCAPTCHA</a> thinks you are a bot, so email address not displayed.";
            }
            return PartialView("_VerifiedRecaptcha");
        }

        public IActionResult Sitemap()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
            sb.AppendLine("<url><loc>https://technicality.online</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Home/Privacy</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Products</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Products/Circlebox</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Products/RcmChannels</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Products/BallparkPilgrims</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Products/GoogleAnalyticsWordPressPlugin</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Products/PitchCounter</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Products/AtlasKegScanner</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Showroom</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Showroom/AspNet</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Showroom/AwsAzureCloud</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Showroom/CreditCardProcessing</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Showroom/DevOps</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Showroom/EmailBlasts</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Showroom/GoogleAnalytics</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Showroom/Https</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Showroom/IntegratedLogins</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Showroom/IntegratedApplications</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Showroom/Mobile</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Showroom/Recaptcha</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("<url><loc>https://technicality.online/Showroom/SqlServer</loc><lastmod>2019-09-14</lastmod></url>");
            sb.AppendLine("</urlset>");

            return Content(sb.ToString(), "text/xml", System.Text.Encoding.UTF8);
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
