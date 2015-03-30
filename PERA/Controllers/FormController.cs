using PERA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PERA.Controllers
{
    public class FormController : Controller
    {
        // GET: InvoiceUpload
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Invoice()
        {
            return View();
        }
        public ActionResult ParkerReport()
        {
            return View();
        }

        public ActionResult Discrepancies()
        {
            return View();
        }

        public ActionResult EditGarage()
        {
            return View();
        }
    }
}