namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PRTM_IAPRid : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ParkerReportTeamMembers", name: "InvoiceActiveParkerReport_ID", newName: "InvoiceActiveParkerReportID");
            RenameIndex(table: "dbo.ParkerReportTeamMembers", name: "IX_InvoiceActiveParkerReport_ID", newName: "IX_InvoiceActiveParkerReportID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ParkerReportTeamMembers", name: "IX_InvoiceActiveParkerReportID", newName: "IX_InvoiceActiveParkerReport_ID");
            RenameColumn(table: "dbo.ParkerReportTeamMembers", name: "InvoiceActiveParkerReportID", newName: "InvoiceActiveParkerReport_ID");
        }
    }
}
