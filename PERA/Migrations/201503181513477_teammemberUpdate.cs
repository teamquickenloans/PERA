namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teammemberUpdate : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TeamMembers", name: "GarageID", newName: "Garage_GarageID");
            RenameIndex(table: "dbo.TeamMembers", name: "IX_GarageID", newName: "IX_Garage_GarageID");
            AddColumn("dbo.TeamMembers", "GarageBadgeID", c => c.Int());
            AddColumn("dbo.TeamMembers", "PuckBadgeID", c => c.Int());
            AddColumn("dbo.TeamMembers", "HangTagID", c => c.Int());
            AlterColumn("dbo.TeamMembers", "BadgeID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TeamMembers", "BadgeID", c => c.Int(nullable: false));
            DropColumn("dbo.TeamMembers", "HangTagID");
            DropColumn("dbo.TeamMembers", "PuckBadgeID");
            DropColumn("dbo.TeamMembers", "GarageBadgeID");
            RenameIndex(table: "dbo.TeamMembers", name: "IX_Garage_GarageID", newName: "IX_GarageID");
            RenameColumn(table: "dbo.TeamMembers", name: "Garage_GarageID", newName: "GarageID");
        }
    }
}
