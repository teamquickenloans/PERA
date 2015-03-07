namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ParkerReportTeamMembers", "BadgeID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ParkerReportTeamMembers", "BadgeID", c => c.Double(nullable: false));
        }
    }
}
