using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PERA.Models;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using PERA.HelperClasses;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace PERA.Controllers
{
    public class Name
    {
        public int first { set; get; }
        public int last { set; get; }
    }

    public class Token
    {
        public int ID_CODE_26W { set; get; }
        public int HID_CORP1K_ID { set; get; }
    }

    public class ParkerReportParserController : ApiController
    {

        private PERAContext db = new PERAContext();

        // key: garageID, value: column index # of token ID (badge, hangtag, puck)
        Dictionary<int, int> tokenColumns = new Dictionary<int, int>()
        {
            { 1, 4 },
            { 2, 2 },
            { 4, 3 },
            { 5, 6 },
            { 7, 5 },
            { 8, 5 },
            { 9, 5 }
        };

        Dictionary<int, Token> splitTokenColumns = new Dictionary<int, Token>()
        {
            //{ 5,  new Token{ID_CODE_26W = 5, HID_CORP1K_ID = 6}}, //!TODO: Change this
            { 6,  new Token{ID_CODE_26W = 10, HID_CORP1K_ID = 12}},
            { 3,  new Token{ID_CODE_26W = 11, HID_CORP1K_ID = 12}},
            { 11, new Token{ID_CODE_26W = 10, HID_CORP1K_ID = 12}},
            { 12, new Token{ID_CODE_26W = 3, HID_CORP1K_ID = 4}}, //!TODO: Change this
            { 13, new Token{ID_CODE_26W = 11, HID_CORP1K_ID = 12}},
            { 14, new Token{ID_CODE_26W = 11, HID_CORP1K_ID = 12}},
            { 15, new Token{ID_CODE_26W = 10, HID_CORP1K_ID = 12}},
            { 16, new Token{ID_CODE_26W = 10, HID_CORP1K_ID = 12}}
            //  5   5,6
            
        };
        //These have the format row[2] = Lastname, Firstname
        Dictionary<int, int> nameColumns = new Dictionary<int, int>()
        {
            {5,2},
            {6,2},
            {3,2},
            {4,2},
            {13,2},
            {11,2},
            {15,2},
            {14,2},
            {1,2},
            {2,2},
            {16,2},
        };


        //The name is split into two columns
        Dictionary<int, Name> splitNameColumns = new Dictionary<int, Name>()
        {
          { 1, new Name{first = 2, last = 1}},
          { 2, new Name{first = 6, last = 5}},
          { 3, new Name{first = 2, last = 1}},
          { 4, new Name{first = 2, last = 1}},
          { 5, new Name{first = 3, last = 2}},
          { 6, new Name{first = 2, last = 1}},
          { 7, new Name{first = 2, last = 1}},
          { 8, new Name{first = 2, last = 1}},
          { 9, new Name{first = 2, last = 1}},
          { 12, new Name{first = 2, last = 1}},
          { 13, new Name{first = 2, last = 1}},
          { 11, new Name{first = 2, last = 1}},
          { 14, new Name{first = 2, last = 1}},
          { 16, new Name{first = 2, last = 1}},
          { 15, new Name{first = 2, last = 1}},

        };


        public List<QLTeamMember> RemoveDuplicatesFromSystemGalaxy(List<QLTeamMember> teamMembers, int garageID)
        {
            List<QLTeamMember> Duplicates = new List<QLTeamMember>();
            Dictionary<string, int> Matches = new Dictionary<string, int>();
            List<QLTeamMember> uniques = new List<QLTeamMember>();

            foreach (QLTeamMember qlTM in teamMembers)
            {
                //var qlTM = QLReport.TeamMembers[i];
                // Get the most recent badge scan for this TM
                PERAContext db = new PERAContext();

                var BadgeScan = db.BadgeScans.Where(
                     x => x.FirstName == qlTM.FirstName
                       && x.LastName == qlTM.LastName
                       && x.GarageID == garageID)
                    .OrderByDescending(x => x.ScanDateTime)
                    .FirstOrDefault();

                string qlName = qlTM.FirstName + qlTM.LastName;

                if (Matches.ContainsKey(qlName)) // it's a duplicate
                    continue;
                //    db.ParkerReportTeamMembers.Remove(qlTM);

                else
                {    //check if it's valid
                    if (BadgeScan != null)
                    {
                        Matches[qlName] = BadgeScan.BadgeID;
                        qlTM.BadgeID = BadgeScan.BadgeID;
                        uniques.Add(qlTM);
                    }
                    else
                    {
                        Matches[qlName] = 1;
                        uniques.Add(qlTM);
                    }
                    /*
                    if (qlTM.BadgeID == BadgeScan.BadgeID)
                        Matches[qlName] = qlName;
                    else // it's invalid so remove it later
                        db.ParkerReportTeamMembers.Remove(qlTM);*/
                    //    Duplicates.Add(qlTM);
                }
            }

            return uniques;

            //for (int i = Duplicates.Count - 1; i > -1; i--)
            //{
            //    PERAContext db = new PERAContext();
            //    db.ParkerReportTeamMembers.Remove(Duplicates[i]);
            //}
        }



        [HttpPost] // This is from System.Web.Http, and not from System.Web.Mvc
        public async Task<HttpResponseMessage> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);


            //string invoice = result.FormData.GetValues(0).FirstOrDefault();
            //Invoice newInvoice = JsonConvert.DeserializeObject<Invoice>(fileUploadObj);
            QLActiveParkerReport apr = GetFormData(result);
            Trace.WriteLine("apr.MonthYear: " + apr.MonthYear);
            Trace.WriteLine("apr.DateReceived: " + apr.DateReceived);
            Trace.WriteLine("apr.DateUploaded: " + apr.DateUploaded);


            FileHandler(result, apr);


            // Through the request response you can return an object to the Angular controller
            // You will be able to access this in the .success callback through its data attribute
            // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
            var returnData = "ReturnTest";
            return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
        }

        private void FileHandler(MultipartFormDataStreamProvider result, QLActiveParkerReport APR)
        {
            foreach (var file in result.FileData)
            {
                // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
                // so this is how you can get the original file name
                var originalFileName = GetDeserializedFileName(file);
                var uploadedFileInfo = new FileInfo(file.LocalFileName);
                int garageID = Convert.ToInt32(result.FormData.GetValues("garageID").First());


                APR.GarageID = garageID;
                
                List<QLTeamMember> teamMembers =
                                    ExcelParser(file.LocalFileName, originalFileName, APR, garageID);
                
                Trace.WriteLine("teamMembers.Count: " + teamMembers.Count);

                List<QLTeamMember> uniqueTeamMembers = RemoveDuplicatesFromSystemGalaxy(teamMembers, garageID);
                Save(APR, uniqueTeamMembers);
            }
        }

        private void Save(QLActiveParkerReport APR, List<QLTeamMember> teamMembers)
        {
            foreach (QLTeamMember teamMember in teamMembers)
            {
                //Trace.WriteLine(teamMember.ParkerReportTeamMemberID);
                //Trace.WriteLine(teamMember.FirstName);
                //Trace.WriteLine(teamMember.LastName);

                APR.TeamMembers.Add(teamMember);
                db.SaveChanges();
            }

            //check if report exists in db
            QLActiveParkerReport report = db.QLActiveParkerReports.FirstOrDefault(
                  x => x.MonthYear.Month == APR.MonthYear.Month
                  && x.MonthYear.Year == APR.MonthYear.Year
                  && x.GarageID == APR.GarageID);

            if(report != null)
            {
                foreach(QLTeamMember qlTM in report.TeamMembers)
                {
                    db.QLTeamMembers.Remove(qlTM);
                    db.ParkerReportTeamMembers.Remove(qlTM);
                }
                report.DateUploaded = APR.DateUploaded;
                report.DateReceived = APR.DateReceived;
                report.TeamMembers = APR.TeamMembers;
                db.SaveChanges();

            }
            else //report doesn't exist yet, add it
            {
                db.QLActiveParkerReports.Add(APR);
            }

            db.SaveChanges();

        }
        private List<QLTeamMember> ExcelParser(string path, string name, QLActiveParkerReport apr, int garageID)
        {
            System.Diagnostics.Debug.WriteLine("begin excel parser");
            IExcelDataReader reader = null;
            //Trace.WriteLine(garageID);
            var excelData = new ExcelData(path);
            reader = excelData.getExcelReader(name);

            // Create column names from first row
            reader.IsFirstRowAsColumnNames = true;

            // The result of each spreadsheet will be created in the result.Tables
            DataSet result = reader.AsDataSet();

            List<QLTeamMember> teamMembers = new List<QLTeamMember>();

            int i = 0;
            System.Diagnostics.Debug.WriteLine("begin for loop");
            Trace.WriteLine(result.Tables.Count);
            DataTable table = result.Tables[0];
            //foreach (DataTable table in result.Tables)
            //{
                foreach (DataRow row in table.Rows)
                {
                    // if this row is the headings, skip this row
                    System.Diagnostics.Debug.WriteLine("loop");
                    if(i < 5)
                    {
                        Trace.WriteLine("continue");
                        i++;
                        continue;
                    }
                    else if (row == null)
                    {
                        System.Diagnostics.Debug.WriteLine("null");
                        continue;
                    }

                    else if (row[0] is String)
                    {
                        Trace.WriteLine("Skipping first row..");
                        continue;
                    }


                    string firstName, lastName;
                    if (splitNameColumns.ContainsKey(garageID))
                    {
                        var first = row[splitNameColumns[garageID].first];
                        var last = row[splitNameColumns[garageID].last];

                        if (last.Equals(DBNull.Value))
                            break;
                        firstName = (string)first;
                        lastName = (string)last;

                    }
                    else if (nameColumns.ContainsKey(garageID))
                    {
                        string fullName = (string)row[nameColumns[garageID]];
                        string[] names = fullName.Split(',');   //split the name

                        if (names.Length < 2)
                        {
                            firstName = names[0];
                            lastName = "";
                        }

                        else
                        {
                            firstName = names[1];
                            lastName = names[0];
                        }
                    }
                    else
                    {
                        throw new System.ArgumentException("Invalid GarageID");
                    }

                    //Convert names to Title Case 
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    firstName = textInfo.ToTitleCase(firstName.ToLower().Trim()); 
                    lastName = textInfo.ToTitleCase(lastName.ToLower().Trim());
                    Trace.WriteLine(firstName);
                    Trace.WriteLine(lastName);

                    double tokenAd, tokenBd;
                    QLTeamMember teamMember = db.QLTeamMembers.Create();
                    if(splitTokenColumns.ContainsKey(garageID))
                    {
                        Token tokens = splitTokenColumns[garageID];
                        var tokenAv = row[tokens.ID_CODE_26W];
                        var tokenBv = row[tokens.HID_CORP1K_ID];
                        if(tokenAv != DBNull.Value ){
                            tokenAd = (System.Double)tokenAv;
                            teamMember.BadgeID = Convert.ToInt32(tokenAd);

                        }
                        if (tokenBv != DBNull.Value) {
                            tokenBd = (System.Double)tokenBv;

                            if (garageID == 6)
                            {
                                teamMember.TokenID = Convert.ToInt32(tokenBd);
                            }
                            else
                                teamMember.BadgeID = Convert.ToInt32(tokenBd);
                        }
                    }
                    else if(tokenColumns.ContainsKey(garageID))
                    {
                        var tokenAv = row[tokenColumns[garageID]];
                        if(tokenAv != DBNull.Value ){
                            tokenAd = (System.Double)tokenAv;
                            teamMember.BadgeID = Convert.ToInt32(tokenAd);
                        }

                    }
                    else{
                        throw new System.ArgumentException("Invalid GarageID");
                    }
                 

                    teamMember.FirstName = firstName;
                    teamMember.LastName = lastName;
                    teamMember.QLActiveParkerReportID = apr.ID;

                    teamMembers.Add(teamMember);

                    //db.QLTeamMembers.Add(teamMember);
                    //db.SaveChanges();

                    //Trace.WriteLine(teamMember.FirstName + teamMember.LastName + teamMember.BadgeID);
                } // end for columns
           // }  // end for tables
            return teamMembers;
        }

        public List<QLTeamMember> RemoveDuplicatesFromSystemGalaxy(QLActiveParkerReport QLReport, int garageID)
        {
            List<QLTeamMember> TeamMembers = new List<QLTeamMember>();
            List<QLTeamMember> Duplicates = new List<QLTeamMember>();
            Dictionary<string, string> Matches = new Dictionary<string, string>();

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
                {
                    Matches[qlName] = qlName;
                    TeamMembers.Add(qlTM);
                    //check if it's valid
                    //if (qlTM.BadgeID == BadgeScan.BadgeID)
                       // Matches[qlName] = qlName;
                    //else // it's invalid so remove it later
                        //db.ParkerReportTeamMembers.Remove(qlTM);
                    //    Duplicates.Add(qlTM);
                }
            }
            return TeamMembers;
            //for (int i = Duplicates.Count - 1; i > -1; i--)
            //{
            //    PERAContext db = new PERAContext();
            //    db.ParkerReportTeamMembers.Remove(Duplicates[i]);
            //}
        }
        // You could extract these two private methods to a separate utility class since
        // they do not really belong to a controller class but that is up to you
        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            // IMPORTANT: replace "(tilde)" with the real tilde character
            // (our editor doesn't allow it, so I just wrote "(tilde)" instead)
            var uploadFolder = "~/App_Data/Tmp/FileUploads"; // you could put this to web.config
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        // Extracts Request FormatData as a strongly typed model
        private QLActiveParkerReport GetFormData(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {

                var unescapedFormData = Uri.UnescapeDataString(result.FormData
                    .GetValues("data").FirstOrDefault() ?? String.Empty);
                Trace.WriteLine(unescapedFormData);

                if (!String.IsNullOrEmpty(unescapedFormData))
                {
                    return JsonConvert.DeserializeObject<QLActiveParkerReport>(unescapedFormData,
                         new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" });
                }
            }
            return null;
        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }
    }
}