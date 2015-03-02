namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tokenID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamMembers", "TokenID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeamMembers", "TokenID");
        }
    }
}
