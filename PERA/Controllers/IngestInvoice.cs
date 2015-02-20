﻿using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PERA.Controllers
{
    public class IngestInvoice
    {
        Application excelApp;
        public void ExcelOpenSpreadsheets(string thisFileName)
        {
            try
            {
                //
                // This mess of code opens an Excel workbook. I don't know what all
                // those arguments do, but they can be changed to influence behavior.
                //
                Workbook workBook = excelApp.Workbooks.Open(thisFileName,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

                //
                // Pass the workbook to a separate function. This new function
                // will iterate through the worksheets in the workbook.
                //
                //ExcelScanIntenal(workBook);

                //
                // Clean up.
                //
                workBook.Close(false, thisFileName, null);
                //Marshal.ReleaseComObject(workBook);
            }
            catch
            {
                //
                // Deal with exceptions.
                //
            }
        }

    }
}