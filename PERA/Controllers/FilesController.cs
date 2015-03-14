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

public struct columns
{
    public int invoice;
    public int QL;
}




namespace PERA.Controllers
{
    public class FilesController : ApiController
    {

        private PERAContext db = new PERAContext();

        Dictionary<int, int> invoiceTokenColumns = new Dictionary<int, int>()
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
            {16,3},
        };

        Dictionary<int, int> invoiceNameColumns = new Dictionary<int, int>()
        {

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

            Dictionary<string, string> invoice = GetFormData(result);

            //string invoice = result.FormData.GetValues(0).FirstOrDefault();
            //Invoice newInvoice = JsonConvert.DeserializeObject<Invoice>(fileUploadObj);
           
            Trace.WriteLine(invoice);
            int i = 0;
            foreach(var file in result.FileData)
            {
                // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
                // so this is how you can get the original file name
                var originalFileName = GetDeserializedFileName(file);
                var uploadedFileInfo = new FileInfo(file.LocalFileName);

                //System.Diagnostics.Debug.WriteLine(uploadedFileInfo);

                ExcelParser(file.LocalFileName, originalFileName, invoice, i);
                i++;
            }


            // Through the request response you can return an object to the Angular controller
            // You will be able to access this in the .success callback through its data attribute
            // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
            var returnData = "ReturnTest";
            return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
        }

        private void ExcelParser(string path, string name, Dictionary<string, string> invoice, int index)
        {
            System.Diagnostics.Debug.WriteLine("begin excel parser");
            IExcelDataReader reader = null;
            Trace.WriteLine(path);
            var excelData = new ExcelData(path);
            reader = excelData.getExcelReader(name);


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
                   
                    ParkerReportTeamMember teamMember = new ParkerReportTeamMember();

                    string fullName = (string)row[2];

                    string[] names = fullName.Split(',');   //split the name
                    string firstName, lastName;
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

                    //string cardNumber = (string)row[3];
                    double cardNumberD = 0;
                    int cardNumber = 0;
                    
                    cardNumberD = (System.Double)row[1];
                    cardNumber = Convert.ToInt32(cardNumberD);
                    


                    teamMember.FirstName = firstName;
                    teamMember.LastName = lastName;
                    teamMember.BusinessHoursTokenID = cardNumber;

                    teamMembers.Add(teamMember);

                    db.ParkerReportTeamMembers.Add(teamMember);
                    db.SaveChanges();

                      /*
                    foreach (DataColumn column in table.Columns)
                    {
                        System.Diagnostics.Debug.WriteLine(row[column]);
                    }*/
                                       
                } // end for columns
            }  // end for tables
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
        private Dictionary<string,string> GetFormData(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {
                var unescapedFormData = Uri.UnescapeDataString(result.FormData
                    .GetValues(0).FirstOrDefault() ?? String.Empty);
                if (!String.IsNullOrEmpty(unescapedFormData))
                {
                    System.Diagnostics.Debug.WriteLine("inside if form data");
                    return JsonConvert.DeserializeObject<Dictionary<string, string>>(result.FormData.GetValues(0).FirstOrDefault());
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

        /*  private void XLSXParser(string filename)
  {

      SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(filename, false);
      WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
      // Iterate through all WorksheetParts
      foreach (WorksheetPart worksheetPart in workbookPart.WorksheetParts)
      {
          OpenXmlPartReader reader = new OpenXmlPartReader(worksheetPart);
          string text;
          string rowNum;
          while (reader.Read())
          {
             if (reader.ElementType == typeof(Cell))
              {
                  Cell c = (Cell)reader.LoadCurrentElement();

                  string cellValue;

                  if (c.DataType != null && c.DataType == CellValues.SharedString)
                  {
                      SharedStringItem ssi = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(int.Parse(c.CellValue.InnerText));

                      cellValue = ssi.Text.Text;
                  }
                  else
                  {
                      cellValue = c.CellValue.InnerText;
                  }

                  Console.Out.Write("{0}: {1} ", c.CellReference, cellValue);
              }
                          

              System.Diagnostics.Debug.WriteLine("xlsx for loop");
              Trace.WriteLine("row");
              do
              {

                  if (reader.HasAttributes)
                  {
                      rowNum = reader.Attributes.First(a => a.LocalName == "r").Value;
                      Trace.WriteLine("rowNum: " + rowNum);
                  }

              } while (reader.ReadNextSibling()); // Skip to the next row

              break; // We just looped through all the rows so no 
              // need to continue reading the worksheet
          }

          if (reader.ElementType != typeof(Worksheet))
              reader.Skip();

          reader.Close();
      }        
  }
        
  */
    }
}

