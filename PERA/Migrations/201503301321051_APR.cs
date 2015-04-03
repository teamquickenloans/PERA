namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class APR : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReport_MonthYear", "dbo.InvoiceActiveParkerReports");
            DropForeignKey("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_MonthYear", "dbo.QLActiveParkerReports");
            DropIndex("dbo.ParkerReportTeamMembers", new[] { "InvoiceActiveParkerReport_MonthYear" });
            DropIndex("dbo.ParkerReportTeamMembers", new[] { "QLActiveParkerReport_MonthYear" });
            //RenameColumn(table: "dbo.ParkerReportTeamMembers", name: "InvoiceActiveParkerReport_MonthYear", newName: "InvoiceActiveParkerReport_ID");
            //RenameColumn(table: "dbo.ParkerReportTeamMembers", name: "QLActiveParkerReport_MonthYear", newName: "QLActiveParkerReport_ID");
            AddColumn("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReport_ID", c => c.Int());
            AddColumn("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_ID", c => c.Int());
            DropColumn(table: "dbo.ParkerReportTeamMembers", name: "InvoiceActiveParkerReport_MonthYear");
            DropColumn(table: "dbo.ParkerReportTeamMembers", name: "QLActiveParkerReport_MonthYear");
            
            DropPrimaryKey("dbo.InvoiceActiveParkerReports");
            DropPrimaryKey("dbo.QLActiveParkerReports");
            AddColumn("dbo.BadgeScans", "BadgeID", c => c.Int(nullable: false));
            AddColumn("dbo.BadgeScans", "ScanInDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.BadgeScans", "TeamMember_primaryKey", c => c.Int());
            AddColumn("dbo.InvoiceActiveParkerReports", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.QLActiveParkerReports", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReport_ID", c => c.Int());
            AlterColumn("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_ID", c => c.Int());
            AddPrimaryKey("dbo.InvoiceActiveParkerReports", "ID");
            AddPrimaryKey("dbo.QLActiveParkerReports", "ID");
            CreateIndex("dbo.BadgeScans", "TeamMember_primaryKey");
            CreateIndex("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReport_ID");
            CreateIndex("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_ID");
            AddForeignKey("dbo.BadgeScans", "TeamMember_primaryKey", "dbo.TeamMembers", "primaryKey");
            AddForeignKey("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReport_ID", "dbo.InvoiceActiveParkerReports", "ID");
            AddForeignKey("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_ID", "dbo.QLActiveParkerReports", "ID");
            DropColumn("dbo.BadgeScans", "FirstName");
            DropColumn("dbo.BadgeScans", "LastName");
            DropColumn("dbo.BadgeScans", "ScanDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BadgeScans", "ScanDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.BadgeScans", "LastName", c => c.String());
            AddColumn("dbo.BadgeScans", "FirstName", c => c.String());
            DropForeignKey("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_ID", "dbo.QLActiveParkerReports");
            DropForeignKey("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReport_ID", "dbo.InvoiceActiveParkerReports");
            DropForeignKey("dbo.BadgeScans", "TeamMember_primaryKey", "dbo.TeamMembers");
            DropIndex("dbo.ParkerReportTeamMembers", new[] { "QLActiveParkerReport_ID" });
            DropIndex("dbo.ParkerReportTeamMembers", new[] { "InvoiceActiveParkerReport_ID" });
            DropIndex("dbo.BadgeScans", new[] { "TeamMember_primaryKey" });
            DropPrimaryKey("dbo.QLActiveParkerReports");
            DropPrimaryKey("dbo.InvoiceActiveParkerReports");
            AlterColumn("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_ID", c => c.DateTime());
            AlterColumn("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReport_ID", c => c.DateTime());
            DropColumn("dbo.QLActiveParkerReports", "ID");
            DropColumn("dbo.InvoiceActiveParkerReports", "ID");
            DropColumn("dbo.BadgeScans", "TeamMember_primaryKey");
            DropColumn("dbo.BadgeScans", "ScanInDateTime");
            DropColumn("dbo.BadgeScans", "BadgeID");
            AddPrimaryKey("dbo.QLActiveParkerReports", "MonthYear");
            AddPrimaryKey("dbo.InvoiceActiveParkerReports", "MonthYear");
            RenameColumn(table: "dbo.ParkerReportTeamMembers", name: "QLActiveParkerReport_ID", newName: "QLActiveParkerReport_MonthYear");
            RenameColumn(table: "dbo.ParkerReportTeamMembers", name: "InvoiceActiveParkerReport_ID", newName: "InvoiceActiveParkerReport_MonthYear");
            CreateIndex("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_MonthYear");
            CreateIndex("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReport_MonthYear");
            AddForeignKey("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_MonthYear", "dbo.QLActiveParkerReports", "MonthYear");
            AddForeignKey("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReport_MonthYear", "dbo.InvoiceActiveParkerReports", "MonthYear");
        }
    }
}
