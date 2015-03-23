namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datereceived : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InvoiceActiveParkerReports", "DateReceived", c => c.DateTime());
            AlterColumn("dbo.QLActiveParkerReports", "DateReceived", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QLActiveParkerReports", "DateReceived", c => c.DateTime(nullable: false));
            AlterColumn("dbo.InvoiceActiveParkerReports", "DateReceived", c => c.DateTime(nullable: false));
        }
    }
}
