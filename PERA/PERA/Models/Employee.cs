using System;
using System.Collections.Generic;

namespace PERA.Models
{
    // P: parking team member
    // T: terminated
    // L: on leave
    public enum EmployeeStatus
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
    public class Employee
    {
        public int ID { get; set; }  //this is the pk
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BadgeID { get; set; }
        public int CommonID { get; set; }
		public EmployeeStatus EmployeeStatus { get; set; }
        public DateTime TerminationDate { get; set; }
        public ParkingStatus ParkingStatus { get; set; }
        public bool IrregularParker { get; set; }
		public virtual Garage { get; set; }
        //public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}