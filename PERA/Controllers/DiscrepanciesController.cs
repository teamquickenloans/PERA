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
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using System.Globalization;
using System.IO;
using System.Data;
using System.Text;
using System.Timers;
using Newtonsoft.Json;
using System.Windows.Forms;

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
            CalculatePrice(InvoiceReport);

            string result = JsonConvert.SerializeObject(duplicates);
            JArray jsonobj = JArray.Parse(result);
            Trace.WriteLine(result);
            Trace.WriteLine("hello");
            Trace.WriteLine(jsonobj[0][0]["TokenID"]);
            string fileLocation = Path.GetFullPath(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath) + "output.xlsx";
            System.IO.File.Copy(Path.GetFullPath(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath)+"book.xlsx", fileLocation, true);
            //Trace.Write(System.IO.File.Exists("output.xlsx"));
            Trace.WriteLine("Exporting");
            //Trace.WriteLine(Path.GetFullPath(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath));
            //Trace.WriteLine(Path.GetFullPath(System.Web.HttpContext.Current.Server.MapPath.ToString());
            //Trace.WriteLine(Path.GetFullPath(@"output.xlsx"), "File exported to: ");
            Trace.WriteLine(jsonobj.Count());
            int max_row = 0;
            for(int i=0; i<jsonobj.Count(); i++){
                max_row = max_row + jsonobj[i].Count();
            }
            Trace.WriteLine(max_row, "Excel row size: ");

            /*
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Excel|*.xlsx";
            //saveFileDialog.FilterIndex = saveAsType;
            saveFileDialog.Title = "Save Data";
            saveFileDialog.FileName = "My File";
            saveFileDialog.ShowDialog();*/

            ExportIssues(fileLocation, max_row, jsonobj[0][0].Count(), jsonobj);
            
            
            //WebClient wc = new WebClient();
            //wc.DownloadFile(Path.GetFullPath(@"output.xlsx"), "output.xlsx");
            //Response.AddHeader("Content-Disposition", "attachment; filename=" +"output.xlsx");
            //Response.WriteFile("C:\\Program Files (x86)\\IIS Express\\output.xlsx");
            //Response.AddHeader("Content-Length", "output.xlsx".Length.ToString());
            Trace.WriteLine(duplicates[0][0].TokenID);
            //Process.Start("output.xlsx", "-p");
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


        public void CalculatePrice(InvoiceActiveParkerReport IAPR)
        {
            // for this invoice APR

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
            Trace.WriteLine("# of parkers: " + QLReport.TeamMembers.Count);

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
            string result2 = JsonConvert.SerializeObject(issues);
            Trace.WriteLine(result2);
            return issues;
        }
        public void ExportIssues(string filename, int numRows, int numCols, JArray json)
        {
            using (SpreadsheetDocument myDoc = SpreadsheetDocument.Open(filename, true))
            {
                WorkbookPart workbookPart = myDoc.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();

                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                Trace.Write(json[0][0]["FirstName"]);
                DateTime time = DateTime.Now;              // Use current time
                string format = "MMM ddd d HH:mm yyyy";    // Use this format
                for (int i = 0; i < json.Count(); i++)
                {
                    for (int row = 0; row < json[i].Count(); row++)
                    {
                        Row r = new Row();

                        for (int col = 0; col < numCols; col++)
                        {
                            if (col == 0)
                            {
                                Cell c = new Cell();
                                c.CellValue = new CellValue(json[i][row]["FirstName"].ToString());
                                //c.Append(x);
                                c.DataType = new EnumValue<CellValues>(CellValues.String);
                                r.Append(c);
                            }

                            else if (col == 1)
                            {
                                Cell c = new Cell();
                                c.CellValue = new CellValue(json[i][row]["LastName"].ToString());
                                //c.Append(x);
                                c.DataType = new EnumValue<CellValues>(CellValues.String);
                                r.Append(c);
                            }

                            else if (col == 2)
                            {
                                Cell c = new Cell();
                                c.CellValue = new CellValue(json[i][row]["TokenID"].ToString());
                                //c.Append(x);
                                c.DataType = new EnumValue<CellValues>(CellValues.String);
                                r.Append(c);
                            }

                            else if (col == 3)
                            {
                                Cell c = new Cell();
                                c.CellValue = new CellValue(json[i][row]["Action"].ToString());
                                //c.Append(x);
                                c.DataType = new EnumValue<CellValues>(CellValues.String);
                                r.Append(c);
                            }

                            else
                            {
                                Cell c = new Cell();
                                c.CellValue = new CellValue(time.ToString(format));
                                //c.Append(x);
                                c.DataType = new EnumValue<CellValues>(CellValues.String);
                                r.Append(c);
                            }
                        }
                        sheetData.Append(r);
                    }
                }
            }
        }
        
    }
}
