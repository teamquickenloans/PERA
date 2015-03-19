namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PRTpk : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ParkerReportTeamMembers");
            AlterColumn("dbo.ParkerReportTeamMembers", "LastName", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ParkerReportTeamMembers", new[] { "FirstName", "LastName", "BusinessHoursTokenID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ParkerReportTeamMembers");
            AlterColumn("dbo.ParkerReportTeamMembers", "LastName", c => c.String());
            AddPrimaryKey("dbo.ParkerReportTeamMembers", "FirstName");
        }
    }
}
