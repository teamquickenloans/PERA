using PERA.Models;
using System;
using System.Collections.Generic;

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
        public int BadgeID { get; set; }  // pk
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CommonID { get; set; }
        public int? GarageID { get; set; }
		public EmploymentStatus? EmploymentStatus { get; set; }
        public DateTime TerminationDate { get; set; }
        public ParkingStatus ParkingStatus { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public virtual ICollection<Invoice> PreviousInvoices { get; set; }

		public virtual Garage Garage{ get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; } 

        public TeamMember()
        {
            Invoices = new HashSet<Invoice>();
        }

    }
}