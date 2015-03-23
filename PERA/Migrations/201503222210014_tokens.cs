namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tokens : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamMembers", "TokenID", c => c.Int(nullable: false));
            AlterColumn("dbo.TeamMembers", "BadgeID", c => c.Int(nullable: false));
            DropColumn("dbo.TeamMembers", "BusinessHoursTokenID");
            DropColumn("dbo.TeamMembers", "AfterHoursTokenID");
            DropColumn("dbo.TeamMembers", "GarageBadgeID");
            DropColumn("dbo.TeamMembers", "PuckBadgeID");
            DropColumn("dbo.TeamMembers", "HangTagID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeamMembers", "HangTagID", c => c.Int());
            AddColumn("dbo.TeamMembers", "PuckBadgeID", c => c.Int());
            AddColumn("dbo.TeamMembers", "GarageBadgeID", c => c.Int());
            AddColumn("dbo.TeamMembers", "AfterHoursTokenID", c => c.Int());
            AddColumn("dbo.TeamMembers", "BusinessHoursTokenID", c => c.Int());
            AlterColumn("dbo.TeamMembers", "BadgeID", c => c.Int());
            DropColumn("dbo.TeamMembers", "TokenID");
        }
    }
}
