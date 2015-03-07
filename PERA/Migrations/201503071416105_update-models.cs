namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BadgeScans", "TeamMember_CommonID", "dbo.TeamMembers");
            DropForeignKey("dbo.CarpoolGroups", "TeamMember_CommonID", "dbo.TeamMembers");
            RenameColumn(table: "dbo.BadgeScans", name: "TeamMember_CommonID", newName: "TeamMember_primaryKey");
            RenameColumn(table: "dbo.CarpoolGroups", name: "TeamMember_CommonID", newName: "TeamMember_primaryKey");
            RenameIndex(table: "dbo.BadgeScans", name: "IX_TeamMember_CommonID", newName: "IX_TeamMember_primaryKey");
            RenameIndex(table: "dbo.CarpoolGroups", name: "IX_TeamMember_CommonID", newName: "IX_TeamMember_primaryKey");
            DropPrimaryKey("dbo.TeamMembers");
            AlterColumn("dbo.TeamMembers", "CommonID", c => c.Int(nullable: false));
            AlterColumn("dbo.TeamMembers", "primaryKey", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TeamMembers", "primaryKey");
            AddForeignKey("dbo.BadgeScans", "TeamMember_primaryKey", "dbo.TeamMembers", "primaryKey");
            AddForeignKey("dbo.CarpoolGroups", "TeamMember_primaryKey", "dbo.TeamMembers", "primaryKey");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarpoolGroups", "TeamMember_primaryKey", "dbo.TeamMembers");
            DropForeignKey("dbo.BadgeScans", "TeamMember_primaryKey", "dbo.TeamMembers");
            DropPrimaryKey("dbo.TeamMembers");
            AlterColumn("dbo.TeamMembers", "primaryKey", c => c.Int(nullable: false));
            AlterColumn("dbo.TeamMembers", "CommonID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TeamMembers", "CommonID");
            RenameIndex(table: "dbo.CarpoolGroups", name: "IX_TeamMember_primaryKey", newName: "IX_TeamMember_CommonID");
            RenameIndex(table: "dbo.BadgeScans", name: "IX_TeamMember_primaryKey", newName: "IX_TeamMember_CommonID");
            RenameColumn(table: "dbo.CarpoolGroups", name: "TeamMember_primaryKey", newName: "TeamMember_CommonID");
            RenameColumn(table: "dbo.BadgeScans", name: "TeamMember_primaryKey", newName: "TeamMember_CommonID");
            AddForeignKey("dbo.CarpoolGroups", "TeamMember_CommonID", "dbo.TeamMembers", "CommonID");
            AddForeignKey("dbo.BadgeScans", "TeamMember_CommonID", "dbo.TeamMembers", "CommonID");
        }
    }
}
