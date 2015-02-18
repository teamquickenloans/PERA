<<<<<<< HEAD
namespace PERA.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PERA.DAL.ParkingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PERA.DAL.ParkingContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
=======
namespace PERA.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Device.Location;
    using System.Linq;
    using PERA.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using CsvHelper;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<PERA.Models.PERAContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PERA.Models.PERAContext context)
        {

           context.Configuration.LazyLoadingEnabled = false;
           context.GarageManagers.AddOrUpdate(x => x.ID,
            new GarageManager { ID = 1, CompanyName = "Ultimate Parking Management" },
            new GarageManager { ID = 2, CompanyName = "Park-Rite" },
            new GarageManager { ID = 3, CompanyName = "Lanier Group" },
            new GarageManager { ID = 4, CompanyName = "COBO" },
            new GarageManager { ID = 5, CompanyName = "SPPlus Parking" },
            new GarageManager { ID = 6, CompanyName = "The Diggs Group" },
            new GarageManager { ID = 7, CompanyName = "City Of Detroit" }

            );

            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "PERA.DAL.garages.csv";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                Console.Write("in here");
                Console.Write(stream);
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.WillThrowOnMissingField = false;
                    var garages = csvReader.GetRecords<Garage>().ToArray();
                    context.Garages.AddOrUpdate(c => c.GarageID, garages);
                }
            }
 
    
            context.TeamMembers.AddOrUpdate(x => x.BadgeID,
                new TeamMember () {BadgeID=4041,FirstName = "Carson", LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01"), CommonID=423535, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
                new TeamMember () {BadgeID=4042,FirstName = "Meredith", LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01"), CommonID=423536, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
                new TeamMember () {BadgeID=4043,FirstName = "Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01"), CommonID=423537, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
                new TeamMember () {BadgeID=4044,FirstName = "Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01"), CommonID=423538, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
                new TeamMember () {BadgeID=4045,FirstName = "Yan",LastName="Li", EnrollmentDate=DateTime.Parse("2002-09-01"), CommonID=423539, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
                new TeamMember () {BadgeID=4046,FirstName = "Peggy", LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01"), CommonID=423540, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
                new TeamMember () {BadgeID=4047,FirstName = "Laura",LastName="Norman", EnrollmentDate=DateTime.Parse("2003-09-01"), CommonID=423541, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")},
                new TeamMember () {BadgeID=4048,FirstName = "Nino",LastName="Olivetto", EnrollmentDate=DateTime.Parse("2005-09-01"), CommonID=423542, EmploymentStatus=null,TerminationDate=DateTime.Parse("2005-09-01")}
            );

            /*
            context.Garages.AddOrUpdate(x => x.GarageID,
                new Garage { GarageID = 1050, Name = "OCM", Latitude = 12.0, Longitude = 12.0, WholeGarageCapacity = 3, NumberOfLeasedSpaces = 30, NumberOfTeamMemberSpaces = 20, MinimumNumberOfTransientSpaces = 5, SpaceCost = 100.00, ValidationCost = 30.00, TransientSalePrice = 120.00, Owner = "UPM", BillingParty = "UPM", ReportType = ReportType.A, AccessToken = AccessToken.B, AccessTokenCost = 20.00, ChangeCost = 0.0, NumberOfValidations = 10, GarageManagerID = 10 },
                new Garage { GarageID = 4022, Name = "Premier", Latitude = 12.0, Longitude = 12.0, WholeGarageCapacity = 3, NumberOfLeasedSpaces = 30, NumberOfTeamMemberSpaces = 20, MinimumNumberOfTransientSpaces = 5, SpaceCost = 100.00, ValidationCost = 30.00, TransientSalePrice = 120.00, Owner = "UPM", BillingParty = "UPM", ReportType = ReportType.A, AccessToken = AccessToken.B, AccessTokenCost = 20.00, ChangeCost = 0.0, NumberOfValidations = 10, GarageManagerID = 10 },
                new Garage { GarageID = 4041, Name = "Book Cadillac", Latitude = 12.0, Longitude = 12.0, WholeGarageCapacity = 3, NumberOfLeasedSpaces = 30, NumberOfTeamMemberSpaces = 20, MinimumNumberOfTransientSpaces = 5, SpaceCost = 100.00, ValidationCost = 30.00, TransientSalePrice = 120.00, Owner = "UPM", BillingParty = "UPM", ReportType = ReportType.A, AccessToken = AccessToken.B, AccessTokenCost = 20.00, ChangeCost = 0.0, NumberOfValidations = 10, GarageManagerID = 10 },
                new Garage { GarageID = 1045, Name = "1 Detroit", Latitude = 12.0, Longitude = 12.0, WholeGarageCapacity = 4, NumberOfLeasedSpaces = 30, NumberOfTeamMemberSpaces = 20, MinimumNumberOfTransientSpaces = 5, SpaceCost = 100.00, ValidationCost = 30.00, TransientSalePrice = 120.00, Owner = "UPM", BillingParty = "UPM", ReportType = ReportType.A, AccessToken = AccessToken.B, AccessTokenCost = 20.00, ChangeCost = 0.0, NumberOfValidations = 10, GarageManagerID = 10 },
                new Garage { GarageID = 3141, Name = "2 Detroit", Latitude = 12.0, Longitude = 12.0, WholeGarageCapacity = 4, NumberOfLeasedSpaces = 30, NumberOfTeamMemberSpaces = 20, MinimumNumberOfTransientSpaces = 5, SpaceCost = 100.00, ValidationCost = 30.00, TransientSalePrice = 120.00, Owner = "UPM", BillingParty = "UPM", ReportType = ReportType.A, AccessToken = AccessToken.B, AccessTokenCost = 20.00, ChangeCost = 0.0, NumberOfValidations = 10, GarageManagerID = 10 },
                new Garage { GarageID = 2021, Name = "COBO Congress", Latitude = 12.0, Longitude = 12.0, WholeGarageCapacity = 3, NumberOfLeasedSpaces = 30, NumberOfTeamMemberSpaces = 20, MinimumNumberOfTransientSpaces = 5, SpaceCost = 100.00, ValidationCost = 30.00, TransientSalePrice = 120.00, Owner = "UPM", BillingParty = "UPM", ReportType = ReportType.A, AccessToken = AccessToken.B, AccessTokenCost = 20.00, ChangeCost = 0.0, NumberOfValidations = 10, GarageManagerID = 10 },
                 );
             

                
            */
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
>>>>>>> origin/webapi
