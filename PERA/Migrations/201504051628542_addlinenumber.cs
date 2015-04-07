namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlinenumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParkerReportTeamMembers", "LineNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParkerReportTeamMembers", "LineNumber");
        }
    }
}
