using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PERA.Models;

namespace PERA.DAL
{
    public class ParkingInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ParkingContext>
    {
        protected override void Seed(ParkingContext context)
        {
            var teamMembers = new List<TeamMember>
            {
            new TeamMember{BadgeID=4041,FirstName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01"), CommonID=423535, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
            new TeamMember{BadgeID=4042,FirstName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01"), CommonID=423536, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
            new TeamMember{BadgeID=4043,FirstName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01"), CommonID=423537, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
            new TeamMember{BadgeID=4044,FirstName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01"), CommonID=423538, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
            new TeamMember{BadgeID=4045,FirstName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01"), CommonID=423539, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
            new TeamMember{BadgeID=4046,FirstName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01"), CommonID=423540, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
            new TeamMember{BadgeID=4047,FirstName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01"), CommonID=423541, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
            new TeamMember{BadgeID=4048,FirstName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01"), CommonID=423542, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")}
            };

            teamMembers.ForEach(s => context.TeamMembers.Add(s));
            context.SaveChanges();
            /*var garages = new List<Garage>
            {
            new Garage{GarageID=1050,Name="OCM",WholeGarageCapacity=3,},
            new Garage{GarageID=4022,Name="Premier",WholeGarageCapacity=3,},
            new Garage{GarageID=4041,Name="Book Cadillac",WholeGarageCapacity=3,},
            new Garage{GarageID=1045,Name="1 Detroit",WholeGarageCapacity=4,},
            new Garage{GarageID=3141,Name="2 Detroit",WholeGarageCapacity=4,},
            new Garage{GarageID=2021,Name="COBO Congress",WholeGarageCapacity=3,},
            new Garage{GarageID=2042,Name="AT&T Lot",WholeGarageCapacity=4,}
            };
            garages.ForEach(s => context.Garages.Add(s));
            context.SaveChanges();*/
            //var enrollments = new List<Enrollment>
            //{
            //new Enrollment{TeamMemberID=1,GarageID=1050,Grade=Grade.A},
            //new Enrollment{TeamMemberID=1,GarageID=4022,Grade=Grade.C},
            //new Enrollment{TeamMemberID=1,GarageID=4041,Grade=Grade.B},
            //new Enrollment{TeamMemberID=2,GarageID=1045,Grade=Grade.B},
            //new Enrollment{TeamMemberID=2,GarageID=3141,Grade=Grade.F},
            //new Enrollment{TeamMemberID=2,GarageID=2021,Grade=Grade.F},
            //new Enrollment{TeamMemberID=3,GarageID=1050},
            //new Enrollment{TeamMemberID=4,GarageID=1050,},
            //new Enrollment{TeamMemberID=4,GarageID=4022,Grade=Grade.F},
            //new Enrollment{TeamMemberID=5,GarageID=4041,Grade=Grade.C},
            //new Enrollment{TeamMemberID=6,GarageID=1045},
            //new Enrollment{TeamMemberID=7,GarageID=3141,Grade=Grade.A},
            //};
            //enrollments.ForEach(s => context.Enrollments.Add(s));
            //context.SaveChanges();
        }
    }
}