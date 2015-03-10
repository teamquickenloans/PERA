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
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.WillThrowOnMissingField = false;
                    var garages = csvReader.GetRecords<Garage>().ToArray();
                    context.Garages.AddOrUpdate(c => c.GarageID, garages);
                }
            }


            context.TeamMembers.AddOrUpdate(x => x.BadgeID,
                new TeamMember() { BadgeID = 4041, FirstName = "Carson", LastName = "Alexander", EnrollmentDate = DateTime.Parse("2005-09-01"), CommonID = 423535, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4042, FirstName = "Meredith", LastName = "Alonso", EnrollmentDate = DateTime.Parse("2002-09-01"), CommonID = 423536, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4043, FirstName = "Arturo", LastName = "Anand", EnrollmentDate = DateTime.Parse("2003-09-01"), CommonID = 423537, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4044, FirstName = "Gytis", LastName = "Barzdukas", EnrollmentDate = DateTime.Parse("2002-09-01"), CommonID = 423538, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4045, FirstName = "Yan", LastName = "Li", EnrollmentDate = DateTime.Parse("2002-09-01"), CommonID = 423539, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4046, FirstName = "Peggy", LastName = "Justice", EnrollmentDate = DateTime.Parse("2001-09-01"), CommonID = 423540, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4047, FirstName = "Laura", LastName = "Norman", EnrollmentDate = DateTime.Parse("2003-09-01"), CommonID = 423541, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4048, FirstName = "Nino", LastName = "Olivetto", EnrollmentDate = DateTime.Parse("2005-09-01"), CommonID = 423542, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4041, FirstName = "Ben", LastName = "Gibbard", EnrollmentDate = DateTime.Parse("2005-09-01"), CommonID = 423545, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4042, FirstName = "Jesse", LastName = "Lacey", EnrollmentDate = DateTime.Parse("2002-09-01"), CommonID = 423546, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4043, FirstName = "Frank", LastName = "Wood", EnrollmentDate = DateTime.Parse("2003-09-01"), CommonID = 423547, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4044, FirstName = "Ben", LastName = "Howard", EnrollmentDate = DateTime.Parse("2002-09-01"), CommonID = 423548, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4045, FirstName = "Quellin", LastName = "Quinn", EnrollmentDate = DateTime.Parse("2002-09-01"), CommonID = 423549, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4046, FirstName = "Arthur", LastName = "Clark", EnrollmentDate = DateTime.Parse("2001-09-01"), CommonID = 423550, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4047, FirstName = "David", LastName = "Hoffman", EnrollmentDate = DateTime.Parse("2003-09-01"), CommonID = 423551, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4048, FirstName = "Arte", LastName = "Keller", EnrollmentDate = DateTime.Parse("2005-09-01"), CommonID = 423552, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4041, FirstName = "Naomi", LastName = "Walker", EnrollmentDate = DateTime.Parse("2005-09-01"), CommonID = 423545, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4042, FirstName = "Trevor", LastName = "Lonso", EnrollmentDate = DateTime.Parse("2002-09-01"), CommonID = 423546, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4043, FirstName = "Danicka", LastName = "Anderson", EnrollmentDate = DateTime.Parse("2003-09-01"), CommonID = 423557, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4044, FirstName = "Gena", LastName = "Werner", EnrollmentDate = DateTime.Parse("2002-09-01"), CommonID = 423558, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4045, FirstName = "Ryan", LastName = "Kellogg", EnrollmentDate = DateTime.Parse("2002-09-01"), CommonID = 423559, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4046, FirstName = "Steve", LastName = "Witicker", EnrollmentDate = DateTime.Parse("2001-09-01"), CommonID = 423560, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4047, FirstName = "Ron", LastName = "Smith", EnrollmentDate = DateTime.Parse("2003-09-01"), CommonID = 423561, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") },
                new TeamMember() { BadgeID = 4048, FirstName = "Bill", LastName = "Davis", EnrollmentDate = DateTime.Parse("2005-09-01"), CommonID = 423562, EmploymentStatus = null, TerminationDate = DateTime.Parse("2005-09-01") }
            );


       
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
