namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ParkerReportTeamMembers", "LineNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ParkerReportTeamMembers", "LineNumber", c => c.Int(nullable: false));
        }
    }
}
