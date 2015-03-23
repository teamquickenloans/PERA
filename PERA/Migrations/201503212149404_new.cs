namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QLActiveParkerReports", "InvoiceID", "dbo.Invoices");
            DropIndex("dbo.QLActiveParkerReports", new[] { "InvoiceID" });
            AddColumn("dbo.ParkerReportTeamMembers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.QLActiveParkerReports", "InvoiceID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QLActiveParkerReports", "InvoiceID", c => c.Int(nullable: false));
            DropColumn("dbo.ParkerReportTeamMembers", "Discriminator");
            CreateIndex("dbo.QLActiveParkerReports", "InvoiceID");
            AddForeignKey("dbo.QLActiveParkerReports", "InvoiceID", "dbo.Invoices", "InvoiceID", cascadeDelete: true);
        }
    }
}
