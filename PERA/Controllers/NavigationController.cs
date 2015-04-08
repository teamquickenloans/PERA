using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PERA.Controllers
{
    public class NavigationController : Controller
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

        public ActionResult Garage()
        {
            return View();
        }
        public ActionResult TopBar()
        {
            return View();
        }
        public ActionResult Expense()
        {
            return View();
        }
        public ActionResult MapGarage()
        {
            return View();
        }
        public ActionResult MapTopBar()
        {
            return View();
        }

    }
}