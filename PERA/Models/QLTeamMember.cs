using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PERA.Models
{
    public class QLTeamMember : ParkerReportTeamMember
    {
        public int COMMON_ID { set; get; }
        public int ID_CODE_26W { set; get; }
        public int FACILITY_CODE_26W { set; get; }
        public int HID_CORP1K_ID { set; get; }

    }
}