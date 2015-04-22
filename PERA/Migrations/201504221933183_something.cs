namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class something : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BadgeScanReports",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GarageID = c.Int(nullable: false),
                        DateReceived = c.DateTime(),
                        DateUploaded = c.DateTime(nullable: false),
                        MonthYear = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Garages", t => t.GarageID, cascadeDelete: true)
                .Index(t => t.GarageID);
            
            AddColumn("dbo.BadgeScans", "BadgeScanReport_ID", c => c.Int());
            CreateIndex("dbo.BadgeScans", "BadgeScanReport_ID");
            AddForeignKey("dbo.BadgeScans", "BadgeScanReport_ID", "dbo.BadgeScanReports", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BadgeScanReports", "GarageID", "dbo.Garages");
            DropForeignKey("dbo.BadgeScans", "BadgeScanReport_ID", "dbo.BadgeScanReports");
            DropIndex("dbo.BadgeScans", new[] { "BadgeScanReport_ID" });
            DropIndex("dbo.BadgeScanReports", new[] { "GarageID" });
            DropColumn("dbo.BadgeScans", "BadgeScanReport_ID");
            DropTable("dbo.BadgeScanReports");
        }
    }
}
