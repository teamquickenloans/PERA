﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PERA.Models
{
    public class InvoiceActiveParkerReport : ActiveParkerReport
    {
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
        public int? LeasedSpots { get; set; }

    }
}
 