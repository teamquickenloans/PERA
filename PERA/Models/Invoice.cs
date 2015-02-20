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
        public int GarageID { get; set; }
        public double TotalAmountBilled { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime BillingStartDate { get; set; } //Use BillingStartDate.Month and BillingStartDate.Year for the month and year
        public DateTime BillingEndDate { get; set; }
        public int? NumberOfLeasedSpots { get; set; }
        public int? NumberOfValidations { get; set; }
        public Format? Format { get; set; }

        public virtual ICollection<TeamMember> TeamMembers { get; set; }
        public virtual Garage Garage { get; set; }

        public Invoice()
        {
            TeamMembers = new HashSet<TeamMember>();
        }

    }
}