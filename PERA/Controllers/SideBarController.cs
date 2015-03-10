using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PERA.Controllers
{
    public class SideBarController : Controller
    {
        // GET: SideBar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Overview()
        {
            return View();
        }

        public ActionResult SingleGarage()
        {
            return View();
        }

    }
}