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
        public JsonResult Post(ActiveParkerReport report)
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

            // invoice report for this garage only
            var InvoiceReportQ = db.InvoiceActiveParkerReports.Where(
                x => x.MonthYear.Month == report.MonthYear.Month
                  && x.MonthYear.Year == report.MonthYear.Year
                  && x.GarageID == report.GarageID);

            var InvoiceReport = InvoiceReportQ.First(); //.ToList();

            // QL report for this garage
            var QLReport = db.QLActiveParkerReports.Where(
                x => x.MonthYear.Month == report.MonthYear.Month
                  && x.MonthYear.Year == report.MonthYear.Year
                  && x.GarageID == report.GarageID)
                .First(); //.ToList();

            // parker reports for all other garages
            var InvoiceReports = db.InvoiceActiveParkerReports.Where(
                x => x.MonthYear.Month == report.MonthYear.Month
                  && x.MonthYear.Year == report.MonthYear.Year)
                  .ToList()
                .Except(InvoiceReportQ)
                  .ToList();

            foreach (var invoiceReport in InvoiceReports)
            {
                Trace.WriteLine("invoiceReport " + invoiceReport.GarageID);
            }

            var duplicates = IdentifyDuplicates(InvoiceReport, QLReport, InvoiceReports);

            return Json(duplicates, JsonRequestBehavior.AllowGet);
        }

        public List<ParkerReportTeamMember> IdentifyDuplicates(InvoiceActiveParkerReport InvoiceReport, QLActiveParkerReport QLReport, 
            List<InvoiceActiveParkerReport> InvoiceReports)
        {
            Dictionary<string, string> Matches = new Dictionary<string, string>();
            List<ParkerReportTeamMember> Duplicates = new List<ParkerReportTeamMember>();
            //team members in the QL report that were not in the invoice
            List<QLTeamMember> Missing = new List<QLTeamMember>();
            //team members in the invoice that were not in the QL report
            ICollection<ParkerReportTeamMember> Extra = InvoiceReport.TeamMembers;


            Trace.WriteLine(InvoiceReport.TeamMembers.First());

            foreach (QLTeamMember qlTM in QLReport.TeamMembers) //for each TM in the qlReport
            {
                //First find duplicates within this garage
                var InvoiceTeamMembers = db.ParkerReportTeamMembers.Where(             //grab the matching Invoice TM
                    x => x.InvoiceActiveParkerReportID == InvoiceReport.ID
                      && x.FirstName == qlTM.FirstName
                      && x.LastName == qlTM.LastName).ToList();
                Trace.WriteLine(qlTM.FirstName + " " + qlTM.LastName + QLReport.ID);

                if(qlTM.TokenID == null)
                {
                    var InvoiceTeamMember = db.ParkerReportTeamMembers.Where(             //grab the matching Invoice TM
                        x => x.InvoiceActiveParkerReportID == InvoiceReport.ID
                          && x.FirstName == qlTM.FirstName
                          && x.LastName == qlTM.LastName
                          && x.TokenID == qlTM.BadgeID).ToList();
                    /*switch (InvoiceTeamMember.Count)
                    {

                    }*/
                }
                else
                {
                    var InvoiceTeamMember = db.ParkerReportTeamMembers.Where(             //grab the matching Invoice TM
                        x => x.InvoiceActiveParkerReportID == InvoiceReport.ID
                          && x.FirstName == qlTM.FirstName
                          && x.LastName == qlTM.LastName
                          && x.TokenID == qlTM.BadgeID).ToList();
                }

                Trace.WriteLine(qlTM.FirstName + " " + qlTM.LastName + QLReport.ID);

                switch (InvoiceTeamMembers.Count)
                {
                    case 0:
                        //couldn't find the team member in the invoice list
                        var Match = db.ParkerReportTeamMembers.Where(             //grab the matching Invoice TM
                                    x => x.InvoiceActiveParkerReportID == InvoiceReport.ID
                                      && x.FirstName == qlTM.FirstName
                                      && x.BadgeID == qlTM.BadgeID).ToList();
                        if (Match.Count > 0)

                            //potential match, different last name same badgeID
                        Missing.Add(qlTM);
                        continue;
                    case 1:
                        ParkerReportTeamMember PRTM = InvoiceTeamMembers.First(); //
                        if (PRTM.BadgeID != null)
                        { if (PRTM.BadgeID == qlTM.BadgeID) { 
                            //report this error as well
                            } 
                        }
                        Extra.Remove(PRTM);
                        break;
                    default: //more than two matches
                        Duplicates.Add(qlTM);
                        Trace.WriteLine("Duplicate");
                        foreach (ParkerReportTeamMember tm in InvoiceTeamMembers) {
                            Duplicates.Add(tm);
                        }
                        break;
                }
                // Next find duplicates within the garage network
                foreach (InvoiceActiveParkerReport invoiceReport in InvoiceReports)
                {
                    Trace.WriteLine(invoiceReport.GarageID);
                    // team members within another garage's InvoiceActiveParkerReport for the same month
                    var InvoiceTMs = db.ParkerReportTeamMembers.Where(
                        x => x.InvoiceActiveParkerReportID == invoiceReport.ID
                          && x.FirstName == qlTM.FirstName
                          && x.LastName == qlTM.LastName).ToList();
                    switch (InvoiceTMs.Count)
                    {
                        case 0:
                            continue;
                        default:
                            Trace.WriteLine("Duplicate found");
                            Duplicates.Add(qlTM);
                            foreach (ParkerReportTeamMember tm in InvoiceTMs)
                            {
                                Duplicates.Add(tm);
                            }
                            break;
                    }
                        
                }
            }
            foreach (var duplicate in Duplicates) {
                Trace.Write("Duplicate: " + duplicate.FirstName + " " + duplicate.LastName);
                if (duplicate.InvoiceActiveParkerReportID != null){
                    Trace.Write(duplicate.InvoiceActiveParkerReportID);
                    Trace.WriteLine("");
                }

                else{
                    QLTeamMember ql = (QLTeamMember)duplicate;
                    Trace.Write(ql.QLActiveParkerReportID);
                    Trace.WriteLine("");
                }
            }
            foreach (var missing in Missing)
            {
                Trace.WriteLine("Missing: " + missing.FirstName + " " + missing.LastName + " " + missing.QLActiveParkerReportID);
            }
            foreach (var extra in Extra)
            {
                Trace.WriteLine("Extra: " + extra.FirstName + " " + extra.LastName + " " + extra.InvoiceActiveParkerReportID);
            }
            return Duplicates;
        }
    }
}
