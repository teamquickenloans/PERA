using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core;

namespace PERA.Models
{
    public class InvoiceTeamMember
    {
        [Key]
        public int pk { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TokenID { get; set; }
        public int? GarageID { get; set; }
        public Garage Garage { get; set; }
        public ICollection<Invoice> Invoices { get; set; } 
    }
}