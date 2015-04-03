namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PRTM_QLAPR : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_ID", "dbo.QLActiveParkerReports");
            RenameColumn(table: "dbo.ParkerReportTeamMembers", name: "QLActiveParkerReport_ID", newName: "QLActiveParkerReportID");
            RenameIndex(table: "dbo.ParkerReportTeamMembers", name: "IX_QLActiveParkerReport_ID", newName: "IX_QLActiveParkerReportID");
            AddForeignKey("dbo.ParkerReportTeamMembers", "QLActiveParkerReportID", "dbo.QLActiveParkerReports", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkerReportTeamMembers", "QLActiveParkerReportID", "dbo.QLActiveParkerReports");
            RenameIndex(table: "dbo.ParkerReportTeamMembers", name: "IX_QLActiveParkerReportID", newName: "IX_QLActiveParkerReport_ID");
            RenameColumn(table: "dbo.ParkerReportTeamMembers", name: "QLActiveParkerReportID", newName: "QLActiveParkerReport_ID");
            AddForeignKey("dbo.ParkerReportTeamMembers", "QLActiveParkerReport_ID", "dbo.QLActiveParkerReports", "ID");
        }
    }
}
