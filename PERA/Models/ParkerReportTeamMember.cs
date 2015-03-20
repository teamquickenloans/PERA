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
        public enum BadgeType
        {
            GarageBadgeID,
            BadgeID,
            PuckBadgeID,
            HangTagID
        }
        [Key]
        public int ParkerReportTeamMemberID { get; set; }
        [Column(Order = 10)]
        public string FirstName { get; set; }
        [Column(Order = 20)]
        public string LastName { get; set; }
        [Column(Order = 30)]
        public BadgeType? BusinessHoursTokenID { get; set; }
        public BadgeType? AfterHoursTokenID { get; set; }
        public int? GarageBadgeID { get; set; }
        public int? BadgeID { get; set; }
        public int? PuckBadgeID { get; set; }
        public int? HangTagID { get; set; }
    }
}