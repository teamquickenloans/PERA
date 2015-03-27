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

    public class InvoiceParserController : ApiController
    {

        private PERAContext db = new PERAContext();

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


        //The name is split into two columns
        Dictionary<int, Name> splitNameColumns = new Dictionary<int, Name>()
        {
          { 2, new Name{first = 3, last = 2}},
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


            //string invoice = result.FormData.GetValues(0).FirstOrDefault();
            //Invoice newInvoice = JsonConvert.DeserializeObject<Invoice>(fileUploadObj);
            Invoice invoice = GetFormData(result);

            //var exists = db.Invoices.Where(x => x.InvoiceID == invoice.InvoiceID);

            var exists = db.Invoices.Any(x => x.InvoiceID == invoice.InvoiceID);

            if(exists)
            {
                invoice = db.Invoices.Find(invoice.InvoiceID);
            }

            Trace.WriteLine("invoice.InvoiceID: " + invoice.InvoiceID);
            Trace.WriteLine("invoice.MonthYear: " + invoice.MonthYear);
            Trace.WriteLine("invoice.TotalAmountBilled: " + invoice.TotalAmountBilled);

            FileHandler(result, invoice);


            // Through the request response you can return an object to the Angular controller
            // You will be able to access this in the .success callback through its data attribute
            // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
            var returnData = "ReturnTest";
            return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
        }

        private void FileHandler(MultipartFormDataStreamProvider result, Invoice invoice){

            int i = 0;
            foreach(var file in result.FileData)
            {
                // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
                // so this is how you can get the original file name
                var originalFileName = GetDeserializedFileName(file);
                var uploadedFileInfo = new FileInfo(file.LocalFileName);
                int garageID = Convert.ToInt32(result.FormData.GetValues("garageID").First());
            
                InvoiceActiveParkerReport APR =  db.InvoiceActiveParkerReports.Create();
                APR.GarageID = garageID;
                APR.DateUploaded = invoice.DateUploaded;
                APR.DateReceived = invoice.DateReceived;
                APR.MonthYear = invoice.MonthYear;
                APR.Invoice = invoice;
                //System.Diagnostics.Debug.WriteLine(uploadedFileInfo);
                List<ParkerReportTeamMember> teamMembers = 
                    ExcelParser(file.LocalFileName, originalFileName, invoice, garageID);
                foreach (ParkerReportTeamMember teamMember in teamMembers)
                {
                    if(teamMember == null)
                    {
                        continue;
                    }
                    Trace.WriteLine(teamMember.ParkerReportTeamMemberID);
                    Trace.WriteLine(teamMember.FirstName);
                    Trace.WriteLine(teamMember.LastName);
                    Trace.WriteLine(APR.TeamMembers);
                    APR.TeamMembers.Add(teamMember);
                }
                db.InvoiceActiveParkerReports.Add(APR);
                db.SaveChanges();
                invoice.InvoiceActiveParkerReports.Add(APR);
                db.SaveChanges();
                i++;
            }
            db.SaveChanges();


        }
        
        private List<ParkerReportTeamMember> ExcelParser(string path, string name, Invoice invoice, 
            int garageID)
        {
            System.Diagnostics.Debug.WriteLine("begin excel parser");
            IExcelDataReader reader = null;
            //Trace.WriteLine(garageID);
            var excelData = new ExcelData(path);
            reader = excelData.getExcelReader(name);
            //Trace.WriteLine(invoice.InvoiceID);

            /*if (reader == null)
            {
                System.Diagnostics.Debug.WriteLine("excel reader null");
            }*/

            // Create column names from first row
            reader.IsFirstRowAsColumnNames = true;

            // The result of each spreadsheet will be created in the result.Tables
            DataSet result = reader.AsDataSet();

            List<ParkerReportTeamMember> teamMembers = new List<ParkerReportTeamMember>();
            
            System.Diagnostics.Debug.WriteLine("begin for loop");

            foreach (DataTable table in result.Tables)
            { 
                foreach (DataRow row in table.Rows)
                {
                    // if this row is the headings, skip this row
                    System.Diagnostics.Debug.WriteLine("loop");
                    if(row == null )
                    {
                        System.Diagnostics.Debug.WriteLine("null");
                        continue;
                    }

                    if(row[0] is String)
                    {
                        System.Diagnostics.Debug.WriteLine(row[0]);
                        continue;
                    }

                    ParkerReportTeamMember teamMember = db.ParkerReportTeamMembers.Create();
                    string firstName, lastName;
                    if(garageID == 2)
                    {
                        var first = row[splitNameColumns[garageID].first];
                        var last = row[splitNameColumns[garageID].last];
                        if(first == null){
                            firstName = "";
                        }
                        //else if(last == null)
                        //{
                        //    lastName = "";
                        //}
                        firstName = (string)first;
                        lastName = (string)last;

                    }
                    else
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

                    //Convert names to Title Case 
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    firstName = textInfo.ToTitleCase(firstName.ToLower());
                    lastName = textInfo.ToTitleCase(lastName.ToLower());

                    int index = tokenColumns[garageID];
                    var cardNumberV = row[index];
                    Trace.WriteLine(cardNumberV.GetType());
                    double cardNumberD;
                    int cardNumber;

                    if(cardNumberV != DBNull.Value)
                    {
                        try
                        {
                            cardNumberD = (System.Double)cardNumberV;
                            cardNumber = Convert.ToInt32(cardNumberD);
                            teamMember.BadgeID = cardNumber;
                        }
                        catch (InvalidCastException e)
                        {
                            // Perform some action here, and then throw a new exception.
                            //teamMember.BadgeID = null;
                        }

                    }

                    teamMember.FirstName = firstName;
                    teamMember.LastName = lastName;


                    teamMembers.Add(teamMember);

                    //db.ParkerReportTeamMembers.Add(teamMember);
                    db.SaveChanges();
                    Trace.WriteLine(teamMember.ParkerReportTeamMemberID);
                      /*
                    foreach (DataColumn column in table.Columns)
                    {
                        System.Diagnostics.Debug.WriteLine(row[column]);
                    }*/
                                       
                } // end for columns
            }  // end for tables
            return teamMembers;
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
        private Invoice GetFormData(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {

                var unescapedFormData = Uri.UnescapeDataString(result.FormData
                    .GetValues("data").FirstOrDefault() ?? String.Empty);
                Trace.WriteLine(unescapedFormData);

                if (!String.IsNullOrEmpty(unescapedFormData))
                {
                    return JsonConvert.DeserializeObject<Invoice>(unescapedFormData,
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

