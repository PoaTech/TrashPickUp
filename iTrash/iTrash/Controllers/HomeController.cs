using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iTrash.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "The origin story of TR@SH";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "We value your feedback more than your trash.";

            return View();
        }

        public ActionResult Pricing()
        {
            ViewBag.Message = "Pricing Options";

            return View();
        }
    }
}