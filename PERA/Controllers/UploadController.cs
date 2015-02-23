using PERA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PERA.Controllers
{
    public class UploadController : Controller
    {
        // GET: InvoiceUpload
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InvoiceForm()
        {
            return View();
        }

        public ActionResult InvoiceFile()
        {
            return View();
        }
        /*
        [HttpPost]
        public async Task<bool> SaveInvoiceForm()
        {
            return true;
        }*/
    }
}