using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PERA.Models
{
    public class Discrepancy
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? TokenID { get; set; }
        public string Action { get; set; }
    }
}