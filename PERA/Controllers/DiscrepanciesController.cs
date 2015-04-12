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
        PERAContext db = new PERAContext();

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


            //RemoveDuplicatesFromSystemGalaxy(QLReport, garageID);
            var duplicates = IdentifyDiscrepancies(InvoiceReport, QLReport, InvoiceReports);

            return Json(duplicates, JsonRequestBehavior.AllowGet);
        }

        //
        public void RemoveDuplicatesFromSystemGalaxy(QLActiveParkerReport QLReport, int garageID)
        {
            List<QLTeamMember> Duplicates = new List<QLTeamMember>(); 
            Dictionary <string, string> Matches = new Dictionary <string, string>();

            foreach (QLTeamMember qlTM in QLReport.TeamMembers)
            {
                //var qlTM = QLReport.TeamMembers[i];
                // Get the most recent badge scan for this TM
                PERAContext db = new PERAContext();
                var BadgeScan = db.BadgeScans.Where(
                     x => x.FirstName == qlTM.FirstName
                       && x.LastName == qlTM.LastName
                       && x.GarageID == QLReport.GarageID)
                .OrderByDescending(x => x.ScanDateTime)
                    .FirstOrDefault();

                string qlName = qlTM.FirstName + qlTM.LastName;

                if (Matches.ContainsKey(qlName)) // it's a duplicate
                    db.ParkerReportTeamMembers.Remove(qlTM);
                //    continue;
                else
                {    //check if it's valid
                    if (qlTM.BadgeID == BadgeScan.BadgeID)
                        Matches[qlName] = qlName;
                    else // it's invalid so remove it later
                        db.ParkerReportTeamMembers.Remove(qlTM);
                    //    Duplicates.Add(qlTM);
                }
            }
            //for (int i = Duplicates.Count - 1; i > -1; i--)
            //{
            //    PERAContext db = new PERAContext();
            //    db.ParkerReportTeamMembers.Remove(Duplicates[i]);
            //}
        }



        
        // Identifies discrepancies
        
        public List<List<Discrepancy>> IdentifyDiscrepancies(InvoiceActiveParkerReport InvoiceReport, QLActiveParkerReport QLReport,
        
            List<InvoiceActiveParkerReport> InvoiceReports)
        {

            
            List<List<Discrepancy>> issues = new List<List<Discrepancy>>();

            //Dictionary<string, string> Matches = new Dictionary<string, string>();
            
            List<Discrepancy> Duplicates = new List<Discrepancy>();


            
            //team members in the QL report that were not in the invoice
            List<Discrepancy> Missing = new List<Discrepancy>();

            //team members in the invoice that we re not in the QL report
            ICollection<ParkerReportTeamMember> Extra = InvoiceReport.TeamMembers;
            List<Discrepancy> Extras = new List<Discrepancy>();

            //List<Discrepancy> Discrepancies = new List<Discrepancy>();

            Trace.WriteLine(InvoiceReport.TeamMembers.First());

            foreach (QLTeamMember qlTM in QLReport.TeamMembers) //for each TM in the qlReport
            {
                PERAContext db = new PERAContext();
                //Trace.WriteLine(qlTM.FirstName + " " + qlTM.LastName + QLReport.ID);

                // Grab the Correct Matching Invoice Team Member find duplicates within this garage
                //  with the correct badge ID

                //TODO: Use the "last used" Badge Scan activity as the correct badgeID
                var Matches = db.ParkerReportTeamMembers.Where(             //grab the matching Invoice TM
                    x => x.InvoiceActiveParkerReportID == InvoiceReport.ID
                      && x.FirstName == qlTM.FirstName
                      && x.LastName == qlTM.LastName
                      && x.BadgeID == qlTM.BadgeID);

                var MatchesList = Matches.ToList();
                ParkerReportTeamMember InvoiceTeamMember;

                switch (MatchesList.Count)
                {
                    case 0:
                        // Correct information for TM missing from Invoice, so we need to add it
                        Discrepancy discrepancy = db.Discrepancies.Create();
                        discrepancy.FirstName = qlTM.FirstName;
                        discrepancy.LastName = qlTM.LastName;
                        discrepancy.TokenID = qlTM.BadgeID;
                        discrepancy.Action = "Add";
                        Missing.Add(discrepancy);
                        break;
                    case 1:
                        // if there's only one, grab it
                        InvoiceTeamMember = MatchesList.First();
                        Extra.Remove(InvoiceTeamMember);
                        break;
                    default:
                        // more than one, grab the first
                        InvoiceTeamMember = MatchesList.First();

                        // and remove it from the matches list
                        MatchesList.Remove(InvoiceTeamMember);

                        // also remove it from the "extras"
                        Extra.Remove(InvoiceTeamMember);

                        foreach (var match in MatchesList)
                        {
                            Discrepancy disc = db.Discrepancies.Create();
                            disc.FirstName = match.FirstName;
                            disc.LastName = match.LastName;
                            disc.TokenID = match.BadgeID;
                            disc.Action = "Remove";
                            Duplicates.Add(disc);
                            Extra.Remove(InvoiceTeamMember);
                        }
                        break;
                }

                //Trace.WriteLine(qlTM.FirstName + " " + qlTM.LastName + " " + QLReport.ID);
                var InvoiceTeamMembers = new List<ParkerReportTeamMember>();
                /*InvoiceTeamMembers = db.ParkerReportTeamMembers.Where(             //grab the matching Invoice TM
                        x => x.InvoiceActiveParkerReportID == InvoiceReport.ID
                          && x.FirstName == qlTM.FirstName
                      && x.LastName == qlTM.LastName)
                      .ToList();*/

                //List<int> tempIdList = MatchesList.Select(q => q.ParkerReportTeamMemberID ).ToList();

                // find team members with incorrect badge ID's

                InvoiceTeamMembers = db.ParkerReportTeamMembers.Where(
                    //q => !tempIdList.Contains(q.ParkerReportTeamMemberID)
                      q => q.InvoiceActiveParkerReportID == InvoiceReport.ID
                        && q.FirstName == qlTM.FirstName
                        && q.LastName == qlTM.LastName
                        && q.BadgeID != qlTM.BadgeID)
                        .ToList();

                if (InvoiceTeamMembers.Count() > 0)
                {
                    foreach (var itm in InvoiceTeamMembers)
                    {
                        Discrepancy discrepancy = db.Discrepancies.Create();
                        discrepancy.FirstName = itm.FirstName;
                        discrepancy.LastName = itm.LastName;
                        discrepancy.TokenID = itm.BadgeID;
                        discrepancy.Action = "Remove";
                        Duplicates.Add(discrepancy);
                    }
                }
                // Next find duplicates within the garage network
                /*foreach (InvoiceActiveParkerReport invoiceReport in InvoiceReports)
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

                }*/
            } //end loop of qlTM
            foreach (var duplicate in Duplicates)
            {
                Trace.WriteLine("Duplicate: " + duplicate.FirstName + " " + duplicate.LastName + " " + duplicate.TokenID + " " + duplicate.Action);

            }
            foreach (var missing in Missing)
            {
                Trace.WriteLine("Missing: " + missing.FirstName + " " + missing.LastName + " " + missing.TokenID);
            }
            // create a discrepacy for each person left in the "extra" list
            foreach (var extra in Extra)
            {
                Discrepancy disc = db.Discrepancies.Create();
                disc.FirstName = extra.FirstName;
                disc.LastName = extra.LastName;
                disc.TokenID = extra.BadgeID;
                disc.Action = "Remove";
                Extras.Add(disc);
                Trace.WriteLine("Extra: " + extra.FirstName + " " + extra.LastName + " " + extra.InvoiceActiveParkerReportID);
            }
            issues.Add(Duplicates);
            issues.Add(Extras);
            issues.Add(Missing);
            //issues.Add(Discrepancies);
            return issues;
        }
    }
}
