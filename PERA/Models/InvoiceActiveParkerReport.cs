using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PERA.Models
{
    public class InvoiceActiveParkerReport : ActiveParkerReport
    {
        public int InvoiceID { get; set; }
        public Invoice Invoice { get; set; }
        [JsonIgnore]
        public virtual ICollection<ParkerReportTeamMember> TeamMembers { get; set; }
        
        protected InvoiceActiveParkerReport()
        {
            TeamMembers = new List<ParkerReportTeamMember>();
        }
         
        public int? LeasedSpots { get; set; }

    }
}
 