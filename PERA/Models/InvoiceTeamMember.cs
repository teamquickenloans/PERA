using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PERA.Models
{
    public class InvoiceTeamMember
    {
        [Key]
        public int primaryKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BusinessHoursTokenID { get; set; }
        public int AfterHoursTokenID { get; set; }
    }
}