using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PERA.Models
{
    public class QLActiveParkerReport : ActiveParkerReport
    {
        [JsonIgnore]
        public virtual ICollection<QLTeamMember> TeamMembers { get; set; }
        public int? AllocatedSpots { get; set; }

        protected QLActiveParkerReport()
        {
            TeamMembers = new List<QLTeamMember>();
        }
    }
}