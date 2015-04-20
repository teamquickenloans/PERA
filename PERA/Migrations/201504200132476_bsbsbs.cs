namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bsbsbs : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BadgeScans", "ReportID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BadgeScans", "ReportID", c => c.Int(nullable: false));
        }
    }
}
