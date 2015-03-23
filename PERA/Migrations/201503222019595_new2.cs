namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParkerReportTeamMembers", "COMMON_ID", c => c.Int());
            AddColumn("dbo.ParkerReportTeamMembers", "ID_CODE_26W", c => c.Int());
            AddColumn("dbo.ParkerReportTeamMembers", "FACILITY_CODE_26W", c => c.Int());
            AddColumn("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_MonthYear", c => c.DateTime());
            CreateIndex("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_MonthYear");
            AddForeignKey("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_MonthYear", "dbo.QLActiveParkerReports", "MonthYear");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_MonthYear", "dbo.QLActiveParkerReports");
            DropIndex("dbo.ParkerReportTeamMembers", new[] { "QLActiveParkerReport_MonthYear" });
            DropColumn("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_MonthYear");
            DropColumn("dbo.ParkerReportTeamMembers", "FACILITY_CODE_26W");
            DropColumn("dbo.ParkerReportTeamMembers", "ID_CODE_26W");
            DropColumn("dbo.ParkerReportTeamMembers", "COMMON_ID");
        }
    }
}
