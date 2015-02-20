namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTeam : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BadgeScans", "TeamMember_CommonID", "dbo.TeamMembers");
            DropForeignKey("dbo.CarpoolGroups", "TeamMember_CommonID", "dbo.TeamMembers");
            DropForeignKey("dbo.Invoices", "TeamMember_CommonID", "dbo.TeamMembers");
            DropForeignKey("dbo.Invoices", "TeamMember_CommonID1", "dbo.TeamMembers");
            DropIndex("dbo.BadgeScans", new[] { "TeamMember_CommonID" });
            DropIndex("dbo.CarpoolGroups", new[] { "TeamMember_CommonID" });
            DropColumn("dbo.BadgeScans", "BadgeID");
            DropColumn("dbo.CarpoolGroups", "BadgeID");
            RenameColumn(table: "dbo.BadgeScans", name: "TeamMember_CommonID", newName: "BadgeID");
            RenameColumn(table: "dbo.Invoices", name: "TeamMember_CommonID", newName: "TeamMember_BadgeID");
            RenameColumn(table: "dbo.Invoices", name: "TeamMember_CommonID1", newName: "TeamMember_BadgeID1");
            RenameColumn(table: "dbo.CarpoolGroups", name: "TeamMember_CommonID", newName: "BadgeID");
            RenameIndex(table: "dbo.Invoices", name: "IX_TeamMember_CommonID", newName: "IX_TeamMember_BadgeID");
            RenameIndex(table: "dbo.Invoices", name: "IX_TeamMember_CommonID1", newName: "IX_TeamMember_BadgeID1");
            DropPrimaryKey("dbo.TeamMembers");
            AlterColumn("dbo.BadgeScans", "BadgeID", c => c.Int(nullable: false));
            AlterColumn("dbo.TeamMembers", "CommonID", c => c.Int(nullable: false));
            AlterColumn("dbo.TeamMembers", "BadgeID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.CarpoolGroups", "BadgeID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.TeamMembers", "BadgeID");
            CreateIndex("dbo.BadgeScans", "BadgeID");
            CreateIndex("dbo.CarpoolGroups", "BadgeID");
            AddForeignKey("dbo.BadgeScans", "BadgeID", "dbo.TeamMembers", "BadgeID", cascadeDelete: true);
            AddForeignKey("dbo.CarpoolGroups", "BadgeID", "dbo.TeamMembers", "BadgeID", cascadeDelete: true);
            AddForeignKey("dbo.Invoices", "TeamMember_BadgeID", "dbo.TeamMembers", "BadgeID");
            AddForeignKey("dbo.Invoices", "TeamMember_BadgeID1", "dbo.TeamMembers", "BadgeID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "TeamMember_BadgeID1", "dbo.TeamMembers");
            DropForeignKey("dbo.Invoices", "TeamMember_BadgeID", "dbo.TeamMembers");
            DropForeignKey("dbo.CarpoolGroups", "BadgeID", "dbo.TeamMembers");
            DropForeignKey("dbo.BadgeScans", "BadgeID", "dbo.TeamMembers");
            DropIndex("dbo.CarpoolGroups", new[] { "BadgeID" });
            DropIndex("dbo.BadgeScans", new[] { "BadgeID" });
            DropPrimaryKey("dbo.TeamMembers");
            AlterColumn("dbo.CarpoolGroups", "BadgeID", c => c.Int());
            AlterColumn("dbo.TeamMembers", "BadgeID", c => c.Int(nullable: false));
            AlterColumn("dbo.TeamMembers", "CommonID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.BadgeScans", "BadgeID", c => c.Int());
            AddPrimaryKey("dbo.TeamMembers", "CommonID");
            RenameIndex(table: "dbo.Invoices", name: "IX_TeamMember_BadgeID1", newName: "IX_TeamMember_CommonID1");
            RenameIndex(table: "dbo.Invoices", name: "IX_TeamMember_BadgeID", newName: "IX_TeamMember_CommonID");
            RenameColumn(table: "dbo.CarpoolGroups", name: "BadgeID", newName: "TeamMember_CommonID");
            RenameColumn(table: "dbo.Invoices", name: "TeamMember_BadgeID1", newName: "TeamMember_CommonID1");
            RenameColumn(table: "dbo.Invoices", name: "TeamMember_BadgeID", newName: "TeamMember_CommonID");
            RenameColumn(table: "dbo.BadgeScans", name: "BadgeID", newName: "TeamMember_CommonID");
            AddColumn("dbo.CarpoolGroups", "BadgeID", c => c.Int(nullable: false));
            AddColumn("dbo.BadgeScans", "BadgeID", c => c.Int(nullable: false));
            CreateIndex("dbo.CarpoolGroups", "TeamMember_CommonID");
            CreateIndex("dbo.BadgeScans", "TeamMember_CommonID");
            AddForeignKey("dbo.Invoices", "TeamMember_CommonID1", "dbo.TeamMembers", "CommonID");
            AddForeignKey("dbo.Invoices", "TeamMember_CommonID", "dbo.TeamMembers", "CommonID");
            AddForeignKey("dbo.CarpoolGroups", "TeamMember_CommonID", "dbo.TeamMembers", "CommonID");
            AddForeignKey("dbo.BadgeScans", "TeamMember_CommonID", "dbo.TeamMembers", "CommonID");
        }
    }
}
