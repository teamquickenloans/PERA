namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "Garage_GarageID", "dbo.Garages");
            DropIndex("dbo.Invoices", new[] { "Garage_GarageID" });
            DropColumn("dbo.Invoices", "Garage_GarageID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "Garage_GarageID", c => c.Int());
            CreateIndex("dbo.Invoices", "Garage_GarageID");
            AddForeignKey("dbo.Invoices", "Garage_GarageID", "dbo.Garages", "GarageID");
        }
    }
}
