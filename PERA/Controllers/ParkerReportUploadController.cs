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

    public class ParkerReportUploadController : ApiController
    {

        private PERAContext db = new PERAContext();

        // key: garageID, value: column index # of token ID (badge, hangtag, puck)
        Dictionary<int, int> tokenColumns = new Dictionary<int, int>()
        {
            { 1, 4 },
            { 2, 2 },
            { 4, 3 },
            { 7, 5 },
            { 8, 5 },
            { 9, 5 }
        };

        Dictionary<int, Token> splitTokenColumns = new Dictionary<int, Token>()
        {
            { 5,  new Token{ID_CODE_26W = 5, HID_CORP1K_ID = 6}}, //!TODO: Change this
            { 6,  new Token{ID_CODE_26W = 10, HID_CORP1K_ID = 12}},
            { 3,  new Token{ID_CODE_26W = 10, HID_CORP1K_ID = 12}},
            { 11, new Token{ID_CODE_26W = 10, HID_CORP1K_ID = 12}},
            { 12, new Token{ID_CODE_26W = 3, HID_CORP1K_ID = 4}}, //!TODO: Change this
            { 13, new Token{ID_CODE_26W = 10, HID_CORP1K_ID = 12}},
            { 14, new Token{ID_CODE_26W = 10, HID_CORP1K_ID = 12}},
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
            ActiveParkerReport apr = GetFormData(result);
            Trace.WriteLine("apr.MonthYear: " + apr.MonthYear);
            Trace.WriteLine("apr.DateReceived: " + apr.DateReceived);

            FileHandler(result, apr);


            // Through the request response you can return an object to the Angular controller
            // You will be able to access this in the .success callback through its data attribute
            // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
            var returnData = "ReturnTest";
            return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
        }

        private void FileHandler(MultipartFormDataStreamProvider result, ActiveParkerReport apr)
        {
            foreach (var file in result.FileData)
            {
                // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
                // so this is how you can get the original file name
                var originalFileName = GetDeserializedFileName(file);
                var uploadedFileInfo = new FileInfo(file.LocalFileName);
                int garageID = Convert.ToInt32(result.FormData.GetValues("garageID").First());

                QLActiveParkerReport APR = new QLActiveParkerReport();
                APR.GarageID = garageID;
                APR.DateUploaded = DateTime.Now;
                
                //System.Diagnostics.Debug.WriteLine(uploadedFileInfo);
                List<QLTeamMember> teamMembers =
                    ExcelParser(file.LocalFileName, originalFileName, apr, garageID);
                foreach (QLTeamMember teamMember in teamMembers)
                {
                    APR.TeamMembers.Add(teamMember);
                }
            }
        }

        private List<QLTeamMember> ExcelParser(string path, string name, ActiveParkerReport apr, int garageID)
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

            List<QLTeamMember> teamMembers = new List<QLTeamMember>();

            int i = 0;
            System.Diagnostics.Debug.WriteLine("begin for loop");
            DataTable table = result.Tables[0];
            //foreach (DataTable table in result.Tables)
            //{
                foreach (DataRow row in table.Rows)
                {
                    // if this row is the headings, skip this row
                    System.Diagnostics.Debug.WriteLine("loop");
                    if(i == 0)
                    {
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
                    i++;

                    string firstName, lastName;
                    if (splitNameColumns.ContainsKey(garageID))
                    {
                        var first = row[splitNameColumns[garageID].first];
                        var last = row[splitNameColumns[garageID].last];
                        if (first == null)
                        {
                            firstName = "";
                        }
                        else if (last == null)
                        {
                            lastName = "";
                        }
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

                    double tokenAd, tokenBd;
                    QLTeamMember teamMember = new QLTeamMember();
                    int itokenA, itokenB = 0;
                    if(splitTokenColumns.ContainsKey(garageID))
                    {
                        Token tokens = splitTokenColumns[garageID];
                        itokenA = tokens.ID_CODE_26W;
                        itokenB = tokens.HID_CORP1K_ID;

                    }
                    else if(tokenColumns.ContainsKey(garageID))
                    {
                        itokenA = tokenColumns[garageID];
                    }
                    else{
                        throw new System.ArgumentException("Invalid GarageID");
                    }


                    var tokenAv = row[itokenA];
                    var tokenBv = row[itokenB];


                    if (tokenAv != DBNull.Value)
                    {
                        tokenAd = (System.Double)tokenAv;
                        teamMember.BadgeID = Convert.ToInt32(tokenAd);
                    }

                    else
                    {
                        teamMember.BadgeID = null;
                    }
                    if (tokenBv != DBNull.Value)
                    {
                        tokenBd = (System.Double)tokenBv;
                        teamMember.TokenID = Convert.ToInt32(tokenBd);
                    }

                    teamMember.FirstName = firstName;
                    teamMember.LastName = lastName;


                    teamMembers.Add(teamMember);

                    db.QLTeamMembers.Add(teamMember);
                    db.SaveChanges();

                    Trace.WriteLine(teamMember.FirstName + teamMember.LastName + teamMember.BadgeID);
                } // end for columns
           // }  // end for tables
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
        private ActiveParkerReport GetFormData(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {

                var unescapedFormData = Uri.UnescapeDataString(result.FormData
                    .GetValues("data").FirstOrDefault() ?? String.Empty);
                Trace.WriteLine(unescapedFormData);

                if (!String.IsNullOrEmpty(unescapedFormData))
                {
                    return JsonConvert.DeserializeObject<ActiveParkerReport>(unescapedFormData,
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