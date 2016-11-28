using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTrash.Models;

namespace iTrash.Controllers
{
    public class RouteController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Route
        public ActionResult CreateRoute()
        {
            RouteViewModel model = new RouteViewModel(db);
            return View(model);
        }
    }
}