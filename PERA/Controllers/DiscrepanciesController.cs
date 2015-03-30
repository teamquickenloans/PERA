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

            Trace.WriteLine(report.MonthYear.Month);

            Trace.WriteLine(report.MonthYear.Year);

            Trace.WriteLine(report.GarageID);


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
            Dictionary<string, string> Matches
                = new Dictionary<string, string>();

            Trace.WriteLine(InvoiceReport.TeamMembers.First());

            foreach (ParkerReportTeamMember invoiceTM in InvoiceReport.TeamMembers)
            {
                Trace.WriteLine("invoice loop");
                Trace.WriteLine(QLReport.TeamMembers.Count);
                /*foreach (QLTeamMember qlTM in QLReport.TeamMembers)
                {
                    Trace.WriteLine("ql loop");
                    Trace.WriteLine("invoiceTM.FirstName" + invoiceTM.FirstName);
                    Trace.WriteLine("qlTM.FirstName" + qlTM.FirstName);
                    Trace.WriteLine("invoiceTM.LastName" + invoiceTM.LastName);
                    Trace.WriteLine("qlTM.LastName" + qlTM.LastName);
                    if (invoiceTM.FirstName == qlTM.FirstName
                        && invoiceTM.LastName == qlTM.LastName)
                    {
                        if(Matches.ContainsKey(invoiceTM.FirstName + invoiceTM.LastName))
                        {
                            Trace.WriteLine("duplicate");
                        }
                        else
                        {
                            Trace.WriteLine("new");
                            Matches[invoiceTM.FirstName + invoiceTM.LastName] = qlTM.FirstName + qlTM.LastName;
                        }
                    }

                }*/
            }
        }
    }
}
