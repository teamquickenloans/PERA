namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamMembers", "Invoice_ID", "dbo.Invoices");
            RenameColumn(table: "dbo.TeamMembers", name: "Invoice_ID", newName: "Invoice_InvoiceID");
            RenameIndex(table: "dbo.TeamMembers", name: "IX_Invoice_ID", newName: "IX_Invoice_InvoiceID");
            DropPrimaryKey("dbo.Invoices");
            DropColumn("dbo.Invoices", "ID");
            AddColumn("dbo.Invoices", "InvoiceID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Invoices", "NumberOfLeasedSpots", c => c.Int());
            AlterColumn("dbo.Invoices", "NumberOfValidations", c => c.Int());
            AddPrimaryKey("dbo.Invoices", "InvoiceID");
            AddForeignKey("dbo.TeamMembers", "Invoice_InvoiceID", "dbo.Invoices", "InvoiceID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "ID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.TeamMembers", "Invoice_InvoiceID", "dbo.Invoices");
            DropPrimaryKey("dbo.Invoices");
            AlterColumn("dbo.Invoices", "NumberOfValidations", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoices", "NumberOfLeasedSpots", c => c.Int(nullable: false));
            DropColumn("dbo.Invoices", "InvoiceID");
            AddPrimaryKey("dbo.Invoices", "ID");
            RenameIndex(table: "dbo.TeamMembers", name: "IX_Invoice_InvoiceID", newName: "IX_Invoice_ID");
            RenameColumn(table: "dbo.TeamMembers", name: "Invoice_InvoiceID", newName: "Invoice_ID");
            AddForeignKey("dbo.TeamMembers", "Invoice_ID", "dbo.Invoices", "ID");
        }
    }
}
