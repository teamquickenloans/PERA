using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PERA.Controllers
{
    public class IngestInvoiceController : Controller
    {
        public ActionResult One()
        {
            return View();
        }

        public ActionResult Two()
        {
            return View();
        }

        public ActionResult Three()
        {
            return View();
        }

        public string AddFile(string File)
        {
            Console.WriteLine(File);
            return File;
        }
    }
}