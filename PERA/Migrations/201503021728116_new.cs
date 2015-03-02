namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InvoiceTeamMembers", "TokenID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InvoiceTeamMembers", "TokenID", c => c.Double(nullable: false));
        }
    }
}
