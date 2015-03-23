namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PRTM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParkerReportTeamMembers", "TokenID", c => c.Int());
            DropColumn("dbo.ParkerReportTeamMembers", "GarageBadgeID");
            DropColumn("dbo.ParkerReportTeamMembers", "PuckBadgeID");
            DropColumn("dbo.ParkerReportTeamMembers", "HangTagID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ParkerReportTeamMembers", "HangTagID", c => c.Int());
            AddColumn("dbo.ParkerReportTeamMembers", "PuckBadgeID", c => c.Int());
            AddColumn("dbo.ParkerReportTeamMembers", "GarageBadgeID", c => c.Int());
            DropColumn("dbo.ParkerReportTeamMembers", "TokenID");
        }
    }
}
