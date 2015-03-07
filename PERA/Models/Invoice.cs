using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PERA.Models
{
    public enum Format
    {
        A, B, C
    }
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public double TotalAmountBilled { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime DateUploaded { get; set; }
        public DateTime MonthYear { get; set; }
        public int? TotalLeasedSpots { get; set; }
        public int? Validations { get; set; }
        //public Format? Format { get; set; }

        public ICollection<InvoiceActiveParkerReport> InvoiceActiveParkerReports { get; set; }
        public Garage Garage { get; set; }


    }
}