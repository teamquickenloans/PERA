using PERA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core;

namespace PERA.Models
{
    // P: parking team member
    // T: terminated
    // L: on leave
    public enum EmploymentStatus
    {
        P, T, L
    }

    // O: opt out
    // C: carpool
    // R: works remotely
    public enum ParkingStatus
    {
        O, C, R
    }


    public class TeamMember
    {
        [Key]
        public int primaryKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CommonID { get; set; }
        public int? GarageID { get; set; }
        public int BadgeID { get; set; }
        /*public int? GarageBadgeBadgeID { get; set; }
        public int? BadgeID { get; set; }
        public int? PuckBadgeID { get; set; }
        public int? HangTagID { get; set; }*/
		public EmploymentStatus? EmploymentStatus { get; set; }
        public DateTime? TerminationDate { get; set; }
        public ParkingStatus? ParkingStatus { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public bool QLPerspective { get; set; }
		public Garage Garage{ get; set; }

    }
}