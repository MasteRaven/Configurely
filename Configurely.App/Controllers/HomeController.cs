using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Configurely.App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Configurely - Configuration Made Easy";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Luis Gomes Contacts";

            return View();
        }
    }
}