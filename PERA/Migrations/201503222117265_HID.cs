namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParkerReportTeamMembers", "HID_CORP1K_ID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParkerReportTeamMembers", "HID_CORP1K_ID");
        }
    }
}
