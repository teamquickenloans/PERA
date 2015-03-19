namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iapr : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReport_MonthYear", "dbo.InvoiceActiveParkerReports");
            DropIndex("dbo.ParkerReportTeamMembers", new[] { "InvoiceActiveParkerReport_MonthYear" });
            AddColumn("dbo.TeamMembers", "BusinessHoursTokenID", c => c.Int());
            AddColumn("dbo.TeamMembers", "AfterHoursTokenID", c => c.Int());
            AddColumn("dbo.TeamMembers", "InvoiceActiveParkerReport_MonthYear", c => c.DateTime());
            AddColumn("dbo.ParkerReportTeamMembers", "GarageBadgeID", c => c.Int());
            AddColumn("dbo.ParkerReportTeamMembers", "BadgeID", c => c.Int());
            AddColumn("dbo.ParkerReportTeamMembers", "PuckBadgeID", c => c.Int());
            AddColumn("dbo.ParkerReportTeamMembers", "HangTagID", c => c.Int());
            CreateIndex("dbo.TeamMembers", "InvoiceActiveParkerReport_MonthYear");
            AddForeignKey("dbo.TeamMembers", "InvoiceActiveParkerReport_MonthYear", "dbo.InvoiceActiveParkerReports", "MonthYear");
            DropColumn("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReport_MonthYear");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReport_MonthYear", c => c.DateTime());
            DropForeignKey("dbo.TeamMembers", "InvoiceActiveParkerReport_MonthYear", "dbo.InvoiceActiveParkerReports");
            DropIndex("dbo.TeamMembers", new[] { "InvoiceActiveParkerReport_MonthYear" });
            DropColumn("dbo.ParkerReportTeamMembers", "HangTagID");
            DropColumn("dbo.ParkerReportTeamMembers", "PuckBadgeID");
            DropColumn("dbo.ParkerReportTeamMembers", "BadgeID");
            DropColumn("dbo.ParkerReportTeamMembers", "GarageBadgeID");
            DropColumn("dbo.TeamMembers", "InvoiceActiveParkerReport_MonthYear");
            DropColumn("dbo.TeamMembers", "AfterHoursTokenID");
            DropColumn("dbo.TeamMembers", "BusinessHoursTokenID");
            CreateIndex("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReport_MonthYear");
            AddForeignKey("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReport_MonthYear", "dbo.InvoiceActiveParkerReports", "MonthYear");
        }
    }
}
