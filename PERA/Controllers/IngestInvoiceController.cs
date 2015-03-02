using Excel;
using Newtonsoft.Json;
using PERA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PERA.Controllers
{
    public class IngestInvoiceController : Controller
    {
        public ActionResult One()
        {
            return View();
        }

        public ActionResult Two()
        {
            return View();
        }

        public ActionResult Three()
        {
            return View();
        }


        public string AddFile(string form)
        {
            var invoice = Request["invoice"];
            var file = Request["file"];
            Invoice newInvoice = JsonConvert.DeserializeObject<Invoice>(invoice);

            System.IO.File.WriteAllText("/test.xls", Request["file"]); //writes my string to a temp file!

            FileStream stream = new FileStream("/test.xls", FileMode.Open); //open that temp file and uses it as a fileStream!

            //1. Reading from a binary Excel file ('97-2003 format; *.xls)
            IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);

            ////2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
            //IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);


            //4. DataSet - Create column names from first row
            excelReader.IsFirstRowAsColumnNames = true;
            DataSet result = excelReader.AsDataSet();

            //5. Data Reader methods
            while (excelReader.Read())
            {
                //excelReader.GetInt32(0);
            }

            System.Diagnostics.Debug.WriteLine("before loop");
            foreach (DataTable table in result.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        System.Diagnostics.Debug.WriteLine(row[column]);
                    }
                }
            }

            //6. Free resources (IExcelDataReader is IDisposable)
            excelReader.Close();

            //System.Diagnostics.Debug.WriteLine();

            return "";
          
        }
    }
}