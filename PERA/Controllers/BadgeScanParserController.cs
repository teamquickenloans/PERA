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
        // key: garageID, value: column index # of token ID (badge, hangtag, puck)
        Dictionary<int, int> tokenColumns = new Dictionary<int, int>()
        {
            {5,3},
            {6,3},
            {3,3},
            {4,3},
            {13,3},
            {11,3},
            {15,3},
            {14,3},
            {1,0},
            {2,1},
            {16,3}
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
            {16,2}
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
                

                //Convert names to Title Case 
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                firstName = textInfo.ToTitleCase(firstName.ToLower());
                lastName = textInfo.ToTitleCase(lastName.ToLower());


                DateTime datetime = (DateTime)row[0];
                badgeScan.ScanDateTime = datetime;


                //TODO: we need access to HR database so we can have one place where we can find additional info on all QL Team Members
                //TODO: ask Megan for a list of how the garages are referred to in the badge scan excel files so we can parse a GarageID out.
                //TODO: only add one entry per person per day into the database.

               /* PARSE GARAGEID (this code might help with that?)
               // int index = tokenColumns[garageID];
                //var cardNumberV = row[index];
                //Trace.WriteLine(cardNumberV.GetType());
                double cardNumberD;
                int cardNumber;
                
                //if (cardNumberV != DBNull.Value)
               // {
                    try
                    {
                        cardNumberD = (System.Double)cardNumberV;
                        cardNumber = Convert.ToInt32(cardNumberD);
                 //       teamMember.BadgeID = cardNumber;
                    }
                    catch (InvalidCastException e)
                    {
                        // Perform some action here, and then throw a new exception.
                        //teamMember.BadgeID = null;
                    }

                //}
                */

                badgeScan.FirstName = firstName;
                badgeScan.LastName = lastName;


                db.BadgeScans.Add(badgeScan);

                //db.ParkerReportTeamMembers.Add(teamMember);
                db.SaveChanges();
               // Trace.WriteLine(teamMember.ParkerReportTeamMemberID);
                /*
                foreach (DataColumn column in table.Columns)
                {
                    System.Diagnostics.Debug.WriteLine(row[column]);
                }*/

            } // end for rows
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
    }
}

