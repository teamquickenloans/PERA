namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BadgeScans", "GarageID", "dbo.Garages");
            DropForeignKey("dbo.BadgeScans", "BadgeID", "dbo.TeamMembers");
            DropForeignKey("dbo.CarpoolGroups", "BadgeID", "dbo.TeamMembers");
            DropForeignKey("dbo.InvoiceToTeamMembers", "InvoiceID", "dbo.Invoices");
            DropForeignKey("dbo.InvoiceToTeamMembers", "BadgeID", "dbo.TeamMembers");
            DropIndex("dbo.BadgeScans", new[] { "GarageID" });
            DropIndex("dbo.BadgeScans", new[] { "BadgeID" });
            DropIndex("dbo.CarpoolGroups", new[] { "BadgeID" });
            DropIndex("dbo.InvoiceToTeamMembers", new[] { "InvoiceID" });
            DropIndex("dbo.InvoiceToTeamMembers", new[] { "BadgeID" });
            DropTable("dbo.BadgeScans");
            DropTable("dbo.CarpoolGroups");
            DropTable("dbo.InvoiceToTeamMembers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.InvoiceToTeamMembers",
                c => new
                    {
                        InvoiceID = c.Int(nullable: false),
                        BadgeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.InvoiceID, t.BadgeID });
            
            CreateTable(
                "dbo.CarpoolGroups",
                c => new
                    {
                        CarpoolGroupID = c.Int(nullable: false, identity: true),
                        BadgeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CarpoolGroupID);
            
            CreateTable(
                "dbo.BadgeScans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GarageID = c.Int(nullable: false),
                        BadgeID = c.Int(nullable: false),
                        ScanInDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.InvoiceToTeamMembers", "BadgeID");
            CreateIndex("dbo.InvoiceToTeamMembers", "InvoiceID");
            CreateIndex("dbo.CarpoolGroups", "BadgeID");
            CreateIndex("dbo.BadgeScans", "BadgeID");
            CreateIndex("dbo.BadgeScans", "GarageID");
            AddForeignKey("dbo.InvoiceToTeamMembers", "BadgeID", "dbo.TeamMembers", "BadgeID", cascadeDelete: true);
            AddForeignKey("dbo.InvoiceToTeamMembers", "InvoiceID", "dbo.Invoices", "InvoiceID", cascadeDelete: true);
            AddForeignKey("dbo.CarpoolGroups", "BadgeID", "dbo.TeamMembers", "BadgeID", cascadeDelete: true);
            AddForeignKey("dbo.BadgeScans", "BadgeID", "dbo.TeamMembers", "BadgeID", cascadeDelete: true);
            AddForeignKey("dbo.BadgeScans", "GarageID", "dbo.Garages", "GarageID", cascadeDelete: true);
        }
    }
}
