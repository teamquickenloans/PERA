namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoiceID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "ID", c => c.Int());
        }
        
        public override void Down()
        {
        }
    }
}