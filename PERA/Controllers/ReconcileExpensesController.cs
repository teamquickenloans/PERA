using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PERA.Controllers
{
    public class ReconcileExpensesController : Controller
    {
        // GET: ReconcileExpenses
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetectedIssues()
        {
            return View();
        }

        public ActionResult UploadHistory()
        {
            return View();
        }


    }
}