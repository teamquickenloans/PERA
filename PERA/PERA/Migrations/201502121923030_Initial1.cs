namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Garages", "AccessTokenCost", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Garages", "AccessTokenCost", c => c.Int(nullable: false));
        }
    }
}
