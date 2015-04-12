namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BadgeScan_BadgeID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BadgeScans", "BadgeID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BadgeScans", "BadgeID");
        }
    }
}
