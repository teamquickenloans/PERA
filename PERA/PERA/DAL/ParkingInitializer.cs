using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PERA.Models;
using System.Device.Location;
namespace PERA.DAL
{
    public class ParkingInitializer : System.Data.Entity.DropCreateDatabaseAlways<ParkingContext>//DropCreateDatabaseIfModelChanges<ParkingContext>
    {
        protected override void Seed(ParkingContext context)
        {

            var garages = new List<Garage>
            {
                new Garage{GarageID=1,Name="OCM",Address="1234 Street Detroit, MI",WholeGarageCapacity=50,NumberOfLeasedSpaces=30,NumberOfTeamMemberSpaces=20,MinimumNumberOfTransientSpaces=2,SpaceCost=1.0,ValidationCost=1.0,TransientSalePrice=1.0,Owner="UPM",BillingParty="UPM"},
                new Garage{GarageID=2,Name="Premier",Address="1234 Street Detroit, MI",WholeGarageCapacity=30,NumberOfLeasedSpaces=30,Owner="UPM",BillingParty="UPM",SpaceCost=0.0,ValidationCost=0.0,TransientSalePrice=0.0},
                new Garage{GarageID=3,Name="Book Cadillac",Address="1234 Street Detroit, MI",WholeGarageCapacity=30,NumberOfLeasedSpaces=30,Owner="UPM",BillingParty="UPM",SpaceCost=0.0,ValidationCost=0.0,TransientSalePrice=0.0},
                new Garage{GarageID=4,Name="1 Detroit",Address="1234 Street Detroit, MI",WholeGarageCapacity=50,NumberOfLeasedSpaces=30,Owner="UPM",BillingParty="UPM",SpaceCost=0.0,ValidationCost=0.0,TransientSalePrice=0.0},
                new Garage{GarageID=5,Name="2 Detroit",Address="1234 Street Detroit, MI",WholeGarageCapacity=50,NumberOfLeasedSpaces=30,Owner="UPM",BillingParty="UPM",SpaceCost=0.0,ValidationCost=0.0,TransientSalePrice=0.0},
                new Garage{GarageID=6,Name="COBO Congress",Address="1234 Street Detroit, MI",WholeGarageCapacity=50,NumberOfLeasedSpaces=30,Owner="UPM",BillingParty="UPM",SpaceCost=0.0,ValidationCost=0.0,TransientSalePrice=0.0},
                new Garage{GarageID=7,Name="AT&T Lot",Address="1234 Street Detroit, MI",WholeGarageCapacity=50,NumberOfLeasedSpaces=30,Owner="UPM",BillingParty="UPM",SpaceCost=0.0,ValidationCost=0.0,TransientSalePrice=0.0}
            };
            garages.ForEach(s => context.Garages.Add(s));
            context.SaveChanges();

            var teamMembers = new List<TeamMember>
            {
            new TeamMember{BadgeID=4041,FirstName="Carson",LastName="Alexander",GarageID=1,EnrollmentDate=DateTime.Parse("2005-09-01"), CommonID=423535, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
            new TeamMember{BadgeID=4042,FirstName="Meredith",LastName="Alonso",GarageID=2,EnrollmentDate=DateTime.Parse("2002-09-01"), CommonID=423536, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
            new TeamMember{BadgeID=4043,FirstName="Arturo",LastName="Anand",GarageID=3,EnrollmentDate=DateTime.Parse("2003-09-01"), CommonID=423537, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
            new TeamMember{BadgeID=4044,FirstName="Gytis",LastName="Barzdukas",GarageID=4,EnrollmentDate=DateTime.Parse("2002-09-01"), CommonID=423538, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
            new TeamMember{BadgeID=4045,FirstName="Yan",LastName="Li",GarageID=5,EnrollmentDate=DateTime.Parse("2002-09-01"), CommonID=423539, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
            new TeamMember{BadgeID=4046,FirstName="Peggy",LastName="Justice",GarageID=5,EnrollmentDate=DateTime.Parse("2001-09-01"), CommonID=423540, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
            new TeamMember{BadgeID=4047,FirstName="Laura",LastName="Norman",GarageID=6,EnrollmentDate=DateTime.Parse("2003-09-01"), CommonID=423541, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
            new TeamMember{BadgeID=4048,FirstName="Nino",LastName="Olivetto",GarageID=7,EnrollmentDate=DateTime.Parse("2005-09-01"), CommonID=423542, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")}
            };
            teamMembers.ForEach(s => context.TeamMembers.Add(s));
            context.SaveChanges();

            var Invoices = new List<Invoice>
            {
                new Invoice{InvoiceID=1,GarageID=1,TotalAmountBilled=200000.0,DateReceived=DateTime.Parse("2001-01-03"),BillingStartDate=DateTime.Parse("2001-12-01"),BillingEndDate=DateTime.Parse("2001-12-31")},
                new Invoice{InvoiceID=2,GarageID=2,TotalAmountBilled=200000.0,DateReceived=DateTime.Parse("2001-01-03"),BillingStartDate=DateTime.Parse("2001-12-01"),BillingEndDate=DateTime.Parse("2001-12-31")},
                new Invoice{InvoiceID=3,GarageID=3,TotalAmountBilled=200000.0,DateReceived=DateTime.Parse("2001-01-03"),BillingStartDate=DateTime.Parse("2001-12-01"),BillingEndDate=DateTime.Parse("2001-12-31")},
                new Invoice{InvoiceID=4,GarageID=4,TotalAmountBilled=200000.0,DateReceived=DateTime.Parse("2001-01-03"),BillingStartDate=DateTime.Parse("2001-12-01"),BillingEndDate=DateTime.Parse("2001-12-31")},
                new Invoice{InvoiceID=5,GarageID=5,TotalAmountBilled=200000.0,DateReceived=DateTime.Parse("2001-01-03"),BillingStartDate=DateTime.Parse("2001-12-01"),BillingEndDate=DateTime.Parse("2001-12-31")},
                new Invoice{InvoiceID=6,GarageID=5,TotalAmountBilled=200000.0,DateReceived=DateTime.Parse("2001-01-03"),BillingStartDate=DateTime.Parse("2001-12-01"),BillingEndDate=DateTime.Parse("2001-12-31")},
                new Invoice{InvoiceID=7,GarageID=6,TotalAmountBilled=200000.0,DateReceived=DateTime.Parse("2001-01-03"),BillingStartDate=DateTime.Parse("2001-12-01"),BillingEndDate=DateTime.Parse("2001-12-31")},
                new Invoice{InvoiceID=8,GarageID=6,TotalAmountBilled=200000.0,DateReceived=DateTime.Parse("2001-01-03"),BillingStartDate=DateTime.Parse("2001-12-01"),BillingEndDate=DateTime.Parse("2001-12-31")},
                new Invoice{InvoiceID=9,GarageID=7,TotalAmountBilled=200000.0,DateReceived=DateTime.Parse("2001-01-03"),BillingStartDate=DateTime.Parse("2001-12-01"),BillingEndDate=DateTime.Parse("2001-12-31")},
                new Invoice{InvoiceID=10,GarageID=7,TotalAmountBilled=200000.0,DateReceived=DateTime.Parse("2001-01-03"),BillingStartDate=DateTime.Parse("2001-12-01"),BillingEndDate=DateTime.Parse("2001-12-31")},
                new Invoice{InvoiceID=11,GarageID=2,TotalAmountBilled=200000.0,DateReceived=DateTime.Parse("2001-01-03"),BillingStartDate=DateTime.Parse("2001-12-01"),BillingEndDate=DateTime.Parse("2001-12-31")},
                new Invoice{InvoiceID=12,GarageID=3,TotalAmountBilled=200000.0,DateReceived=DateTime.Parse("2001-01-03"),BillingStartDate=DateTime.Parse("2001-12-01"),BillingEndDate=DateTime.Parse("2001-12-31")},
            };
            Invoices.ForEach(s => context.Invoices.Add(s));
            context.SaveChanges();




        }

    }
}