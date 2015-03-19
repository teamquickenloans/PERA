namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class APR : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.QLActiveParkerReports", "InvoiceID");
            AddForeignKey("dbo.QLActiveParkerReports", "InvoiceID", "dbo.Invoices", "InvoiceID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QLActiveParkerReports", "InvoiceID", "dbo.Invoices");
            DropIndex("dbo.QLActiveParkerReports", new[] { "InvoiceID" });
        }
    }
}
