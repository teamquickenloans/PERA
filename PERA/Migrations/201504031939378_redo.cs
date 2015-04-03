namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class redo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BadgeScans", "TeamMember_primaryKey", "dbo.TeamMembers");
            DropForeignKey("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_MonthYear", "dbo.QLActiveParkerReports");
            DropForeignKey("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReport_MonthYear", "dbo.InvoiceActiveParkerReports");
            DropIndex("dbo.BadgeScans", new[] { "TeamMember_primaryKey" });
            DropIndex("dbo.ParkerReportTeamMembers", new[] { "InvoiceActiveParkerReport_MonthYear" });
            DropIndex("dbo.ParkerReportTeamMembers", new[] { "QLActiveParkerReport_MonthYear" });
            //RenameColumn(table: "dbo.ParkerReportTeamMembers", name: "InvoiceActiveParkerReport_MonthYear", newName: "InvoiceActiveParkerReportID");
            //RenameColumn(table: "dbo.ParkerReportTeamMembers", name: "QLActiveParkerReport_MonthYear", newName: "QLActiveParkerReportID");
            DropColumn(table: "dbo.ParkerReportTeamMembers", name: "InvoiceActiveParkerReport_MonthYear");
            DropColumn(table: "dbo.ParkerReportTeamMembers", name: "QLActiveParkerReport_MonthYear");
            AddColumn("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReportID", c => c.Int());
            AddColumn("dbo.ParkerReportTeamMembers", "QLActiveParkerReportID", c => c.Int());

            DropPrimaryKey("dbo.InvoiceActiveParkerReports");
            DropPrimaryKey("dbo.QLActiveParkerReports");
            AddColumn("dbo.BadgeScans", "FirstName", c => c.String());
            AddColumn("dbo.BadgeScans", "LastName", c => c.String());
            AddColumn("dbo.BadgeScans", "ScanDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Garages", "InvoiceNameColumn", c => c.Int());
            AddColumn("dbo.Garages", "InvoiceFirstNameColumn", c => c.Int());
            AddColumn("dbo.Garages", "InvoiceLastNameColumn", c => c.Int());
            AddColumn("dbo.Garages", "InvoiceTokenColumn", c => c.Int());
            AddColumn("dbo.Garages", "QLFirstNameColumn", c => c.Int());
            AddColumn("dbo.Garages", "QLLastNameColumn", c => c.Int());
            AddColumn("dbo.Garages", "QLNameColumn", c => c.Int());
            AddColumn("dbo.Garages", "QLTokenColumn", c => c.Int());
            AddColumn("dbo.Garages", "QLTokenAColumn", c => c.Int());
            AddColumn("dbo.Garages", "QLTokenBColumn", c => c.Int());
            AddColumn("dbo.InvoiceActiveParkerReports", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.QLActiveParkerReports", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReportID", c => c.Int());
            AlterColumn("dbo.ParkerReportTeamMembers", "QLActiveParkerReportID", c => c.Int());
            AddPrimaryKey("dbo.InvoiceActiveParkerReports", "ID");
            AddPrimaryKey("dbo.QLActiveParkerReports", "ID");
            CreateIndex("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReportID");
            CreateIndex("dbo.ParkerReportTeamMembers", "QLActiveParkerReportID");
            AddForeignKey("dbo.ParkerReportTeamMembers", "QLActiveParkerReportID", "dbo.QLActiveParkerReports", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReportID", "dbo.InvoiceActiveParkerReports", "ID");
            DropColumn("dbo.BadgeScans", "BadgeID");
            DropColumn("dbo.BadgeScans", "ScanInDateTime");
            DropColumn("dbo.BadgeScans", "TeamMember_primaryKey");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BadgeScans", "TeamMember_primaryKey", c => c.Int());
            AddColumn("dbo.BadgeScans", "ScanInDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.BadgeScans", "BadgeID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReportID", "dbo.InvoiceActiveParkerReports");
            DropForeignKey("dbo.ParkerReportTeamMembers", "QLActiveParkerReportID", "dbo.QLActiveParkerReports");
            DropIndex("dbo.ParkerReportTeamMembers", new[] { "QLActiveParkerReportID" });
            DropIndex("dbo.ParkerReportTeamMembers", new[] { "InvoiceActiveParkerReportID" });
            DropPrimaryKey("dbo.QLActiveParkerReports");
            DropPrimaryKey("dbo.InvoiceActiveParkerReports");
            AlterColumn("dbo.ParkerReportTeamMembers", "QLActiveParkerReportID", c => c.DateTime());
            AlterColumn("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReportID", c => c.DateTime());
            DropColumn("dbo.QLActiveParkerReports", "ID");
            DropColumn("dbo.InvoiceActiveParkerReports", "ID");
            DropColumn("dbo.Garages", "QLTokenBColumn");
            DropColumn("dbo.Garages", "QLTokenAColumn");
            DropColumn("dbo.Garages", "QLTokenColumn");
            DropColumn("dbo.Garages", "QLNameColumn");
            DropColumn("dbo.Garages", "QLLastNameColumn");
            DropColumn("dbo.Garages", "QLFirstNameColumn");
            DropColumn("dbo.Garages", "InvoiceTokenColumn");
            DropColumn("dbo.Garages", "InvoiceLastNameColumn");
            DropColumn("dbo.Garages", "InvoiceFirstNameColumn");
            DropColumn("dbo.Garages", "InvoiceNameColumn");
            DropColumn("dbo.BadgeScans", "ScanDateTime");
            DropColumn("dbo.BadgeScans", "LastName");
            DropColumn("dbo.BadgeScans", "FirstName");
            AddPrimaryKey("dbo.QLActiveParkerReports", "MonthYear");
            AddPrimaryKey("dbo.InvoiceActiveParkerReports", "MonthYear");
            RenameColumn(table: "dbo.ParkerReportTeamMembers", name: "QLActiveParkerReportID", newName: "QLActiveParkerReport_MonthYear");
            RenameColumn(table: "dbo.ParkerReportTeamMembers", name: "InvoiceActiveParkerReportID", newName: "InvoiceActiveParkerReport_MonthYear");
            CreateIndex("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_MonthYear");
            CreateIndex("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReport_MonthYear");
            CreateIndex("dbo.BadgeScans", "TeamMember_primaryKey");
            AddForeignKey("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReport_MonthYear", "dbo.InvoiceActiveParkerReports", "MonthYear");
            AddForeignKey("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_MonthYear", "dbo.QLActiveParkerReports", "MonthYear");
            AddForeignKey("dbo.BadgeScans", "TeamMember_primaryKey", "dbo.TeamMembers", "primaryKey");
        }
    }
}
