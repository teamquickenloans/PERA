namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BSBS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BadgeScans", "ReportID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BadgeScans", "ReportID");
        }
    }
}
