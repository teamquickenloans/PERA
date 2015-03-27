using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
//using System.Web.Http;
using PERA.Models;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Web.Mvc;

namespace PERA.Controllers
{
    public class DiscrepanciesController : Controller
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
        public ActionResult Post(ActiveParkerReport report)
        {

            NameValueCollection nvc = Request.Form;
            if (!string.IsNullOrEmpty(nvc["garageID"]))
            {
                var garageID = nvc["garageID"];
                Trace.WriteLine(garageID);
            }
            Trace.WriteLine("hello@!");
            Trace.WriteLine(report.MonthYear);


            var InvoiceReport = db.InvoiceActiveParkerReports.Where(
                x => x.MonthYear.Month == report.MonthYear.Month 
                  && x.MonthYear.Year == report.MonthYear.Year 
                  && x.GarageID == report.GarageID)
                .First(); //.ToList();
            var QLReport = db.QLActiveParkerReports.Where(
                x => x.MonthYear.Month == report.MonthYear.Month
                  && x.MonthYear.Year == report.MonthYear.Year
                  && x.GarageID == report.GarageID)
                .First(); //.ToList();

            var InvoiceReports = db.InvoiceActiveParkerReports.Where(
                x => x.MonthYear.Month == report.MonthYear.Month
                  && x.MonthYear.Year == report.MonthYear.Year)
                  .ToList();

            IdentifyDuplicates(InvoiceReport, QLReport, InvoiceReports);

            

            return View();
            
        }

        public void IdentifyDuplicates(InvoiceActiveParkerReport InvoiceReport, QLActiveParkerReport QLReport, 
            List<InvoiceActiveParkerReport> InvoiceReports)
        {
            Dictionary<ParkerReportTeamMember, QLTeamMember> Matches
                = new Dictionary<ParkerReportTeamMember, QLTeamMember>();

            foreach (ParkerReportTeamMember invoiceTM in InvoiceAPR.TeamMembers)
            {
                foreach (QLTeamMember qlTM in QLAPR.TeamMembers)
                {

                    if (invoiceTM.FirstName == qlTM.FirstName
                        && invoiceTM.LastName == qlTM.LastName)
                        Matches[invoiceTM] = qlTM;

                }
            }
        }
    }
}
