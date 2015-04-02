namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class garage : DbMigration
    {
        public override void Up()
        {
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
        }
        
        public override void Down()
        {
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
        }
    }
}
