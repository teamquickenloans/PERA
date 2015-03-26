using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PERA.Models;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PERA.Controllers
{
    public class DiscrepanciesController : ApiController
    {
        private PERAContext db = new PERAContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }

        [HttpPost]
        public async Task Post()
        {
            dynamic obj = await Request.Content.ReadAsAsync<JObject>();
            DateTime r = obj.MonthYear;
            var InvoiceAPRs = db.InvoiceActiveParkerReports.Where(x => x.MonthYear == r);

            
            
        }
    }
}
