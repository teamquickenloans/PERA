using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PERA.Models
{
    public class CarpoolGroup
    {
        public int CarpoolGroupID { get; set; } // pk
        public int BadgeID { get; set; } //team member

        public virtual TeamMember TeamMember { get; set; } //TeamMember
    }
}