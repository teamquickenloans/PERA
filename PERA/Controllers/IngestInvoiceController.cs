using Excel;
using Newtonsoft.Json;
using PERA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

        public string AddFile(string form)
        {
            var invoice = Request["invoice"];
            Invoice newInvoice = JsonConvert.DeserializeObject<Invoice>(invoice);
            System.Diagnostics.Debug.WriteLine("Garage ID");
            System.Diagnostics.Debug.WriteLine(newInvoice.GarageID);

            return "";
          
        }
    }
}