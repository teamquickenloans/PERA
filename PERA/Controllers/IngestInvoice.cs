﻿using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PERA.Controllers
{
    public class IngestInvoice
    {

        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

        //1. Reading from a binary Excel file ('97-2003 format; *.xls)
        IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);

    }
}