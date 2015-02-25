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
            JsonConvert.DeserializeObject<Invoice>(invoice);

            System.IO.File.WriteAllText(string.Concat(Environment.ExpandEnvironmentVariables("%temp%"),   
            @"/test.txt"), Request["file"]); //writes my string to a temp file!




            FileStream stream = new FileStream(string.Concat(Environment.ExpandEnvironmentVariables("%temp%"),
                @"/test.txt"), FileMode.Open); //open that temp file and uses it as a fileStream!

            //1. Reading from a binary Excel file ('97-2003 format; *.xls)
            IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);

            ////2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
            //IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            ////3. DataSet - The result of each spreadsheet will be created in the result.Tables
            //DataSet result = excelReader.AsDataSet();

            //4. DataSet - Create column names from first row
            excelReader.IsFirstRowAsColumnNames = true;
            DataSet result = excelReader.AsDataSet();
            System.Diagnostics.Debug.WriteLine(result);

            //5. Data Reader methods
            while (excelReader.Read())
            {
                //excelReader.GetInt32(0);
            }

            //6. Free resources (IExcelDataReader is IDisposable)
            excelReader.Close();

            //System.Diagnostics.Debug.WriteLine();
            return "";
        }
    }
}