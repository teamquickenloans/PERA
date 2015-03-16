namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PRTpk2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ParkerReportTeamMembers");
            AddColumn("dbo.ParkerReportTeamMembers", "ParkerReportTeamMemberID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ParkerReportTeamMembers", "FirstName", c => c.String());
            AlterColumn("dbo.ParkerReportTeamMembers", "LastName", c => c.String());
            AlterColumn("dbo.ParkerReportTeamMembers", "BusinessHoursTokenID", c => c.Int());
            AddPrimaryKey("dbo.ParkerReportTeamMembers", "ParkerReportTeamMemberID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ParkerReportTeamMembers");
            AlterColumn("dbo.ParkerReportTeamMembers", "BusinessHoursTokenID", c => c.Int(nullable: false));
            AlterColumn("dbo.ParkerReportTeamMembers", "LastName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ParkerReportTeamMembers", "FirstName", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.ParkerReportTeamMembers", "ParkerReportTeamMemberID");
            AddPrimaryKey("dbo.ParkerReportTeamMembers", new[] { "FirstName", "LastName", "BusinessHoursTokenID" });
        }
    }
}
