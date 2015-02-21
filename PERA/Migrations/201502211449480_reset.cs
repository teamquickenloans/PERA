namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BadgeScans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GarageID = c.Int(nullable: false),
                        BadgeID = c.Int(nullable: false),
                        ScanInDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Garages", t => t.GarageID, cascadeDelete: true)
                .ForeignKey("dbo.TeamMembers", t => t.BadgeID, cascadeDelete: true)
                .Index(t => t.GarageID)
                .Index(t => t.BadgeID);
            
            CreateTable(
                "dbo.CarpoolGroups",
                c => new
                    {
                        CarpoolGroupID = c.Int(nullable: false, identity: true),
                        BadgeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CarpoolGroupID)
                .ForeignKey("dbo.TeamMembers", t => t.BadgeID, cascadeDelete: true)
                .Index(t => t.BadgeID);
            
            AddColumn("dbo.Garages", "MinimumNumberOfBufferSpaces", c => c.Int());
            DropColumn("dbo.Garages", "MinimumNumberOfTransientSpaces");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Garages", "MinimumNumberOfTransientSpaces", c => c.Int());
            DropForeignKey("dbo.CarpoolGroups", "BadgeID", "dbo.TeamMembers");
            DropForeignKey("dbo.BadgeScans", "BadgeID", "dbo.TeamMembers");
            DropForeignKey("dbo.BadgeScans", "GarageID", "dbo.Garages");
            DropIndex("dbo.CarpoolGroups", new[] { "BadgeID" });
            DropIndex("dbo.BadgeScans", new[] { "BadgeID" });
            DropIndex("dbo.BadgeScans", new[] { "GarageID" });
            DropColumn("dbo.Garages", "MinimumNumberOfBufferSpaces");
            DropTable("dbo.CarpoolGroups");
            DropTable("dbo.BadgeScans");
        }
    }
}
