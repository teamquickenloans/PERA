namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecreateInvoice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InvoiceActiveParkerReports", "InvoiceID", "dbo.Invoices");
            DropPrimaryKey("dbo.Invoices");
            AlterColumn("dbo.Invoices", "InvoiceID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Invoices", "InvoiceID");
            AddForeignKey("dbo.InvoiceActiveParkerReports", "InvoiceID", "dbo.Invoices", "InvoiceID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceActiveParkerReports", "InvoiceID", "dbo.Invoices");
            DropPrimaryKey("dbo.Invoices");
            AlterColumn("dbo.Invoices", "InvoiceID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Invoices", "InvoiceID");
            AddForeignKey("dbo.InvoiceActiveParkerReports", "InvoiceID", "dbo.Invoices", "InvoiceID", cascadeDelete: true);
        }
    }
}
