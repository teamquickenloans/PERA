namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class howdyhiho : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BadgeScans", "TeamMember_primaryKey", "dbo.TeamMembers");
            DropIndex("dbo.BadgeScans", new[] { "TeamMember_primaryKey" });
            AddColumn("dbo.BadgeScans", "FirstName", c => c.String());
            AddColumn("dbo.BadgeScans", "LastName", c => c.String());
            AddColumn("dbo.BadgeScans", "ScanDateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.BadgeScans", "BadgeID");
            DropColumn("dbo.BadgeScans", "ScanInDateTime");
            DropColumn("dbo.BadgeScans", "TeamMember_primaryKey");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BadgeScans", "TeamMember_primaryKey", c => c.Int());
            AddColumn("dbo.BadgeScans", "ScanInDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.BadgeScans", "BadgeID", c => c.Int(nullable: false));
            DropColumn("dbo.BadgeScans", "ScanDateTime");
            DropColumn("dbo.BadgeScans", "LastName");
            DropColumn("dbo.BadgeScans", "FirstName");
            CreateIndex("dbo.BadgeScans", "TeamMember_primaryKey");
            AddForeignKey("dbo.BadgeScans", "TeamMember_primaryKey", "dbo.TeamMembers", "primaryKey");
        }
    }
}
