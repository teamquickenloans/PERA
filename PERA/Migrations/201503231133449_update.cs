namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ParkerReportTeamMembers", "BadgeID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ParkerReportTeamMembers", "BadgeID", c => c.Int(nullable: false));
        }
    }
}
