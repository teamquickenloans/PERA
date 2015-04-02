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
        

        public struct Scanner
        {
            public int garageID;
            public string scannerName;
        };

        List<Scanner> garageScanners = new List<Scanner> //TODO: finish data-entry (one way or another)
        {
            //1001 Woodward
            new Scanner { garageID = 2, scannerName = ""},

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

           

            //Trace.WriteLine("badgeScan.ID: " + badgeScan.ID);
            //Trace.WriteLine("badgeScan.GarageID: " + badgeScan.GarageID);
            //Trace.WriteLine("badgeScan.BadgeID: " + badgeScan.BadgeID);

            FileHandler(result);


            // Through the request response you can return an object to the Angular controller
            // You will be able to access this in the .success callback through its data attribute
            // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
            var returnData = "ReturnTest";
            return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
        }

        private void FileHandler(MultipartFormDataStreamProvider result)
        {
            // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
            // so this is how you can get the original file name
            var file = result.FileData[0];
            var originalFileName = GetDeserializedFileName(file);
            int garageID = Convert.ToInt32(result.FormData.GetValues("garageID").First());

            ExcelParser(file.LocalFileName, originalFileName, garageID);
        }

        public void ExcelParser(string path, string name, int garageID)
        {
            System.Diagnostics.Debug.WriteLine("begin excel parser");
            IExcelDataReader reader = null;
            
            var excelData = new ExcelData(path);
            reader = excelData.getExcelReader(name);


            // Create column names from first row
            reader.IsFirstRowAsColumnNames = true;

            // The result of each spreadsheet will be created in the result.Tables
            DataSet result = reader.AsDataSet();

            System.Diagnostics.Debug.WriteLine("begin for loop");

            DataTable table = result.Tables[0];
            foreach (DataRow row in table.Rows)
            {
                // if this row is the headings, skip this row
                System.Diagnostics.Debug.WriteLine("loop");
                if (row == null)
                {
                    System.Diagnostics.Debug.WriteLine("null");
                    continue;
                }

                if (row[0] is String)
                {
                    System.Diagnostics.Debug.WriteLine(row[0]);
                    continue;
                }

                BadgeScan badgeScan = db.BadgeScans.Create();
                string firstName, lastName;
                
                string fullName = (string)row[2];
                string[] names = fullName.Split(',');   //split the name
                string[] firstnames = names[1].Split(' ');

                if (names.Length < 2)
                {
                    firstName = names[0];
                    lastName = "";
                }

                else
                {
                    firstName = firstnames[1];
                    lastName = names[0];
                }
                

                //Convert names to Title Case 
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                firstName = textInfo.ToTitleCase(firstName.ToLower());
                lastName = textInfo.ToTitleCase(lastName.ToLower());

                badgeScan.FirstName = firstName;
                badgeScan.LastName = lastName;


                if(row[0] == DBNull.Value)
                {
                    System.Diagnostics.Debug.WriteLine("row[0] is null");
                    continue;
                }


                //Parse the DateTime
                badgeScan.ScanDateTime = ExcelDateToDateTime( (double)row[0] );
                System.Diagnostics.Debug.WriteLine("DateTime: " + badgeScan.ScanDateTime);

                //TODO: we need access to HR database so we can have one place where we can find additional info on all QL Team Members  \\MAYBE, but not for this
                //TODO: ask Megan for a list of how the garages are referred to in the badge scan excel files so we can parse a GarageID out.
                //TODO: only add one entry per person per day into the database.


                
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
                    

                //Check if the database already holds a report of a person with the same name checking into the same garage on the same day already. 
                //If so, don't add another entry into the database.
                BadgeScan bs = db.BadgeScans.FirstOrDefault(
                        x => x.FirstName == badgeScan.FirstName
                        && x.LastName == badgeScan.LastName
                        && x.GarageID == badgeScan.GarageID
                        && x.ScanDateTime == badgeScan.ScanDateTime);
                if (bs == null)
                {
                    db.BadgeScans.Add(badgeScan);
                    db.SaveChanges();
                }  

            } // end for rows
            System.Diagnostics.Debug.WriteLine("Parsing Completed!");
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
    }
}

