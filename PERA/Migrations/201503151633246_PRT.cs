namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PRT : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ParkerReportTeamMembers", "AfterHoursTokenID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ParkerReportTeamMembers", "AfterHoursTokenID", c => c.Int(nullable: false));
        }
    }
}
