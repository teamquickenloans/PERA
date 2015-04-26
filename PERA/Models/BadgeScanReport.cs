using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PERA.Models
{
    public class BadgeScanReport : ActiveParkerReport
    {
        [JsonIgnore]
        public virtual ICollection<BadgeScan> BadgeScans { get; set; }
        

        protected BadgeScanReport()
        {
            BadgeScans = new List<BadgeScan>();
        }
    }
}