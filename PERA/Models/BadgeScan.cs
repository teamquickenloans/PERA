using PERA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PERA.Models
{
    public class BadgeScan
    {
        public int ID { get; set; }
        public int GarageID { get; set; }
        public int BadgeID { get; set; }
        public DateTime ScanInDateTime { get; set; }

        public virtual Garage Garage { get; set; }
        public virtual TeamMember TeamMember { get; set; }
    }
}