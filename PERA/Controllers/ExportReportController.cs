using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json.Linq;

namespace PERA.Controllers
{
    public class ExportReportController : ApiController
    {
        public void Main(string[] args)//Discrepancy disc)
        {
            //File.Copy("PERA.Controllers.book.xlsx", "output.xlsx", true);
            //File.Create("book.xlsx");
            File.Copy("book.xlsx", "output.xlsx", true);
            Trace.Write(File.Exists("output.xlsx"));
            string Json = "{ 'Issues': [{'garage': 'The Z', 'name': 'Matt', 'commonID': 23532,  'issue': 'Duplicate','date': '12/25/13'}, {'garage': 'OCM', 'name': 'Tim', 'commonID': 24000,  'issue': 'Duplicate','date': '12/25/14'}]}";
            JObject jsonobj = JObject.Parse(Json);
            WriteRandomValuesDOM("output.xlsx", jsonobj["Issues"].Count(), jsonobj["Issues"][0].Count(), jsonobj);
        }

        public void WriteRandomValuesDOM(string filename, int numRows, int numCols, JObject json)
        {
            using (SpreadsheetDocument myDoc = SpreadsheetDocument.Open(filename, true))
            {
                WorkbookPart workbookPart = myDoc.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();

                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                string[] names = new string[3] { "Matt", "Joanne", "Robert" };
                string[,] siblings = new string[,] { {"Mike","Amy"}, {"Mary","Albert"}, {"Joe", "Rogan"} };

                Random rnd = new Random();

                //string someJson = "{ \"ErrorMessage\": \"\", \"ErrorDetails\": {\"ErrorID\": 111,\"Description\": {\"Short\": 0,\"Verbose\": 20}, \"ErrorDate\": \"\" }}\";
                string someJson = "{\"ErrorMessage\": \"\",\"ErrorDetails\": {\"ErrorID\": 111,\"Description\":{\"Short\": 0,\"Verbose\": 20},\"ErrorDate\": \"\"}}";
                //string json = JsonConvert.SerializeObject(someJson);
                
                Trace.Write(json);
                Trace.Write(json["Issues"][1]["commonID"].ToString());

                for (int row = 0; row < numRows; row++)
                {
                    Row r = new Row();
                    //r.Append(names[0]);
                    //r.Append(Discrepancy.FirstName);
                    //r.Append(Discrepancy.LastName);
                    for (int col = 0; col < numCols; col++)
                    {
                        if (col == 0)
                        {
                            Cell c = new Cell();
                            CellValue x = new CellValue(json["Issues"][row]["garage"].ToString());
                            c.Append(x);
                            r.Append(c);
                        }

                        else if (col == 1)
                        {
                            Cell c = new Cell();
                            CellValue x = new CellValue(json["Issues"][row]["name"].ToString());
                            c.Append(x);
                            r.Append(c);
                        }

                        else if (col == 2)
                        {
                            Cell c = new Cell();
                            CellValue x = new CellValue(json["Issues"][row]["commonID"].ToString());
                            c.Append(x);
                            r.Append(c);
                        }

                        else if (col == 3)
                        {
                            Cell c = new Cell();
                            CellValue x = new CellValue(json["Issues"][row]["issue"].ToString());
                            c.Append(x);
                            r.Append(c);
                        }

                        else
                        {
                            Cell c = new Cell();
                            CellValue x = new CellValue(json["Issues"][row]["date"].ToString());
                            c.Append(x);
                            r.Append(c);
                        }
                    }
                    sheetData.Append(r);
                }
            }
        }
    }
}
