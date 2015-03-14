using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace PERA.HelperClasses
{
    public class ExcelData
    {
        string _path;
        IExcelDataReader _reader;

        public ExcelData(string path)
        {
            _path = path;
            _reader = null;
        }

        public IEnumerable<string> getWorksheetNames()
        {
            var reader = _reader;
            var workbook = reader.AsDataSet();
            var sheets = from DataTable sheet in workbook.Tables select sheet.TableName;
            return sheets;
        }

        public IEnumerable<DataRow> getData(string sheet, bool firstRowIsColumnNames = true)
        {
            var reader = _reader;
            reader.IsFirstRowAsColumnNames = firstRowIsColumnNames;
            var workSheet = reader.AsDataSet().Tables[sheet];
            var rows = from DataRow a in workSheet.Rows select a;
            return rows;
        }

        public IExcelDataReader getExcelReader(string name)
        {
            // ExcelDataReader works with the binary Excel file, so it needs a FileStream
            // to get started. This is how we avoid dependencies on ACE or Interop:
            FileStream stream = File.Open(_path, FileMode.Open, FileAccess.Read);

            // We return the interface, so that
            try
            {
                if (name.EndsWith(".xls"))
                {
                    _reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                if (name.EndsWith(".xlsx"))
                {
                    _reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                return _reader;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}