using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PERA.Models
{
    public class ParkerReportTeamMember
    {
        [Key]
        public int ParkerReportTeamMemberID { get; set; }
        [Column(Order = 10)]
        public string FirstName { get; set; }
        [Column(Order = 20)]
        public string LastName { get; set; }
        [Column(Order = 30)]
        public int? BusinessHoursTokenID { get; set; }
        public int? AfterHoursTokenID { get; set; }
    }
}