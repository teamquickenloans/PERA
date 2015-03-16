namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update315 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "DateReceived", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "DateReceived", c => c.DateTime(nullable: false));
        }
    }
}
