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

    public class BadgeScanParserController : ApiController
    {

        private PERAContext db = new PERAContext();
        
        /*
        public struct Scanner
        {
            public int garageID;
            public string scannerName;
        };

        List<Scanner> garageScanners = new List<Scanner> //TODO: finish data-entry (one way or another)
        {
            //1001 Woodward
            new Scanner { garageID = 2, scannerName = "1001 Garage - 1st. Fl. Left Inbound Lane 49.1.1.1"},

            //2 Detroit
            new Scanner { garageID = 5, scannerName = ""},

            //Financial District (FDG/DIME)
            new Scanner { garageID = 10, scannerName = ""},

            //Brush St / Greektown
            new Scanner { garageID = 12, scannerName = ""},

            //Fort St / Greektown
            new Scanner { garageID = 13, scannerName = ""},

            //First National (FNG)
            new Scanner { garageID = 14, scannerName = "FNB Garage - Entrance Lane 1 55.1.1.1"},
            new Scanner { garageID = 14, scannerName = "FNB Garage - Entrance Lane 2 55.1.1.2"},
            new Scanner { garageID = 14, scannerName = "FNB Garage - Exit Lane 1 55.1.2.1"},
            new Scanner { garageID = 14, scannerName = "FNB Garage - Exit Lane 2 55.1.2.2"},

            //The Z
            new Scanner { garageID = 15, scannerName = ""}
        };
        */



        [HttpPost] // This is from System.Web.Http, and not from System.Web.Mvc
        public async Task<HttpResponseMessage> Upload()
        {
            Trace.WriteLine("Begin Upload!!!");
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);


            //string invoice = result.FormData.GetValues(0).FirstOrDefault();
            //Invoice newInvoice = JsonConvert.DeserializeObject<Invoice>(fileUploadObj);
            BadgeScanReport bsReport0 = GetFormData(result);

            //var exists = db.BadgeScanReports.Any(x => x.GarageID == bsReport0.GarageID && x.MonthYear == bsReport0.MonthYear);

            if (db.BadgeScanReports.Any(
                x => x.MonthYear.Month == bsReport0.MonthYear.Month
                  && x.MonthYear.Year == bsReport0.MonthYear.Year
                  && x.GarageID == bsReport0.GarageID))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid ID");
            }


            /*if(exists)
            {
                bsReport0 = db.BadgeScanReports.Find(bsReport0.GarageID, bsReport0.MonthYear);
            }*/

            Trace.WriteLine("bsReport.GarageID: " + bsReport0.GarageID);
            Trace.WriteLine("bsReport.MonthYear: " + bsReport0.MonthYear);
            Trace.WriteLine("bsReport.DateReceived: " + bsReport0.DateReceived);
            Trace.WriteLine("bsReport.DateUploaded: " + bsReport0.DateUploaded);
            

            FileHandler(result, bsReport0);


            // Through the request response you can return an object to the Angular controller
            // You will be able to access this in the .success callback through its data attribute
            // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
            var returnData = "ReturnTest";
            return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
        }

        private void FileHandler(MultipartFormDataStreamProvider result, BadgeScanReport bsReport0)
        {
            int i = 0;
            foreach (var file in result.FileData)
            {
                // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
                // so this is how you can get the original file name
                //var file = result.FileData[0];
                var originalFileName = GetDeserializedFileName(file);
                int garageID = Convert.ToInt32(result.FormData.GetValues("garageID").First());


                //BadgeScanReport bsReport = db.BadgeScanReports.Create();
                bsReport0.GarageID = garageID;
                //bsReport.MonthYear = bsReport0.MonthYear;
                //bsReport.DateReceived = bsReport0.DateReceived;
                //bsReport.DateUploaded = bsReport0.DateUploaded;


                List<BadgeScan> badgeScans = 
                    ExcelParser(file.LocalFileName, originalFileName, garageID);
                foreach (BadgeScan badgeScan in badgeScans)
                {
                    if(badgeScan == null)
                    {
                        Trace.WriteLine("null team member");
                        continue;
                    }
                    
                    Trace.WriteLine(badgeScan.ID);
                    Trace.WriteLine(badgeScan.FirstName);
                    Trace.WriteLine(badgeScan.LastName);
                    Trace.WriteLine(badgeScan.ScanDateTime);
                   // Trace.WriteLine(bsReport.DateUploaded);
                   // Trace.WriteLine(bsReport.MonthYear);
                    Trace.WriteLine(badgeScan.BadgeID);
                  //  Trace.WriteLine(bsReport.BadgeScans);
                    
                    bsReport0.BadgeScans.Add(badgeScan);
                    db.SaveChanges();
                }
                i++;
            }

            if(db.BadgeScanReports.Any(
                x => x.MonthYear.Month == bsReport0.MonthYear.Month
                  && x.MonthYear.Year == bsReport0.MonthYear.Year
                  && x.GarageID == bsReport0.GarageID))
            {

            }
            else
            {
                db.BadgeScanReports.Add(bsReport0);
                db.SaveChanges();
            }

        }

        public List<BadgeScan> ExcelParser(string path, string name, int garageID)
        {
            System.Diagnostics.Debug.WriteLine("begin excel parser");
            IExcelDataReader reader = null;
            
            var excelData = new ExcelData(path);
            reader = excelData.getExcelReader(name);

            List<BadgeScan> badgeScans = new List<BadgeScan>();
            // Create column names from first row
            reader.IsFirstRowAsColumnNames = true;

            // The result of each spreadsheet will be created in the result.Tables
            DataSet result = reader.AsDataSet();

            System.Diagnostics.Debug.WriteLine("begin for loop");

            DataTable table = result.Tables[0];
            foreach (DataRow row in table.Rows)
            {

                // if this row is the headings, skip this row
                Trace.WriteLine("loop");
                if (row == null || row.Equals( System.DBNull.Value) )
                {
                    System.Diagnostics.Debug.WriteLine("null");
                    continue;
                }

                if (row[0] == null || row[0].Equals(System.DBNull.Value))
                {
                    System.Diagnostics.Debug.WriteLine("null");
                    continue;
                }
                
                if (row[0].GetType() == typeof(String) )
                {
                    System.Diagnostics.Debug.WriteLine(row[0]);
                    continue;
                }
                
                BadgeScan badgeScan = db.BadgeScans.Create();

                if (row[2] == DBNull.Value)
                {
                    System.Diagnostics.Debug.WriteLine("row[2] is null");
                    continue;
                }




                string firstName, lastName;
                
                string fullName = row[2].ToString();
                string[] names = fullName.Split(',');   //split the name

                if (names.Length < 2)
                {
                    System.Diagnostics.Debug.WriteLine("No Last Name Detected: " + row[2].ToString());
                    firstName = names[0];
                    lastName = "";
                }
                else
                {
                    //string[] firstnames = names[1].Split(' ');
                    firstName = names[1];
                    lastName = names[0];
                }
                

                //Convert names to Title Case 
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                firstName = textInfo.ToTitleCase(firstName.ToLower());
                lastName = textInfo.ToTitleCase(lastName.ToLower());

                badgeScan.FirstName = firstName;
                badgeScan.LastName = lastName;



                if (row[0] == DBNull.Value)
                {
                    Trace.WriteLine("row[0] is null");
                    continue;
                }

                Trace.WriteLine(row[3].GetType());
                Trace.WriteLine(row[3]);
                var doubleBadgeID = (double)row[3];
                badgeScan.BadgeID = Convert.ToInt32(doubleBadgeID);

                //Parse the DateTime
                badgeScan.ScanDateTime = ExcelDateToDateTime( (double)row[0] );
                System.Diagnostics.Debug.WriteLine("DateTime: " + badgeScan.ScanDateTime);



               /*
                //look up what garage the scanner is in and find the corresponding garageID. If the scanner is not for garage entrance/exit disregard the badge scan?
                badgeScan.GarageID = -1; //to check if the actual GarageID was found
                for (int i = 0; i < garageScanners.Count(); i++)
                {
                    if(row[1].ToString() == garageScanners[i].scannerName)
                    {
                        badgeScan.GarageID = garageScanners[i].garageID;
                    }
                }

                if(badgeScan.GarageID == -1) //if the GarageID was not found
                {
                    continue; //don't add this row to the database
                }
               */

                badgeScan.GarageID = garageID;
                    

                //Check if the database already holds a report of a person with the same name checking into the same garage on the same day already. 
                //If so, don't add another entry into the database.

                if (db.BadgeScans.Any(
                        x => x.FirstName == badgeScan.FirstName
                        && x.LastName == badgeScan.LastName
                        && x.GarageID == badgeScan.GarageID
                        && x.ScanDateTime.Year == badgeScan.ScanDateTime.Year
                        && x.ScanDateTime.Month == badgeScan.ScanDateTime.Month
                        && x.ScanDateTime.Day == badgeScan.ScanDateTime.Day)
                    )
                {
                    continue;
                }

                else {
                    badgeScans.Add(badgeScan);
                    db.BadgeScans.Add(badgeScan);
                    db.SaveChanges();
                }  
                
            } // end for rows
            return badgeScans;
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

       

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }



        /* This function converts from an Excel Date to a DateTime which we insert into the database
         * 
         * 86400 = Seconds in a day
         * 25569 = Days between 1970/01/01 and 1900/01/01 (min date in Windows Excel)
        */
        public DateTime ExcelDateToDateTime(double excelDate)
        {
            //Excel Date -> Unix TimeStamp
            double unixTimeStamp = (excelDate - 25569) * 86400;

            //Unix TimeStamp -> DateTime
            DateTime datetime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            return datetime.AddSeconds(unixTimeStamp);
        }



        // Extracts Request FormatData as a strongly typed model
        private BadgeScanReport GetFormData(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {

                var unescapedFormData = Uri.UnescapeDataString(result.FormData
                    .GetValues("data").FirstOrDefault() ?? String.Empty);
                Trace.WriteLine(unescapedFormData);

                if (!String.IsNullOrEmpty(unescapedFormData))
                {
                    return JsonConvert.DeserializeObject<BadgeScanReport>(unescapedFormData,
                         new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" });
                }
            }
            return null;
        }



    }
}

