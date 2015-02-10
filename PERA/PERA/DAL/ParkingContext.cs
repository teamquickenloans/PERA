using PERA.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PERA.DAL
{
    public class ParkingContext : DbContext
    {
        public ParkingContext() : base("ParkingContext")
        {
        }
        public DbSet<TeamMember> TeamMembers { get; set; } // each DbSet is a table
        public DbSet<Garage> Garages { get; set; }
        public DbSet<GarageManager> GarageManagers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<BadgeScan> BadgeScans { get; set; }
        public DbSet<CarpoolGroup> CarpoolGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) // prevents table names from being pluralized
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}