using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PERA.Models
{
    public class PERAContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public PERAContext() : base("name=PERAContext")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

        }

        public System.Data.Entity.DbSet<PERA.Models.TeamMember> TeamMembers { get; set; }

        public System.Data.Entity.DbSet<PERA.Models.ParkerReportTeamMember> ParkerReportTeamMembers { get; set; }

        public System.Data.Entity.DbSet<PERA.Models.Garage> Garages { get; set; }

        public System.Data.Entity.DbSet<PERA.Models.GarageManager> GarageManagers { get; set; }

        public System.Data.Entity.DbSet<PERA.Models.BadgeScan> BadgeScans { get; set; }

        public System.Data.Entity.DbSet<PERA.Models.CarpoolGroup> CarpoolGroups { get; set; }

        public System.Data.Entity.DbSet<PERA.Models.Invoice> Invoices { get; set; }
    
    }
}
