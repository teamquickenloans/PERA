using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PERA.Models
{
    public class InvoiceToTeamMember
    {
        public int InvoiceID { get; set; }
        public int BadgeID { get; set; }

        public virtual TeamMember TeamMember{ get; set; }
        public virtual Invoice Invoice { get; set; }

    }
}