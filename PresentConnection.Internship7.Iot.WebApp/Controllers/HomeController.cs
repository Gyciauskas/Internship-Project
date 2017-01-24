using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PresentConnection.Internship7.Iot.ServiceModels;
using PresentConnection.Internship7.Iot.Services;
using PresentConnection.Internship7.Iot.WebApp.Models;
using ServiceStack.Mvc;

namespace PresentConnection.Internship7.Iot.WebApp.Controllers
{
    public class HomeController : ServiceStackController
    {
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

            return View();
        }

    }
}