namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateInvoice : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Invoices");
            CreateTable(
                "dbo.Invoices",
                c => new
                {
                    InvoiceID = c.Int(nullable: false, identity: true),
                    TotalAmountBilled = c.Double(nullable: false),
                    DateReceived = c.DateTime(),
                    DateUploaded = c.DateTime(nullable: false),
                    MonthYear = c.DateTime(nullable: false),
                    TotalLeasedSpots = c.Int(),
                    Validations = c.Int(),
                })
                .PrimaryKey(t => t.InvoiceID);
        }
        
        public override void Down()
        {
        }
    }
}
