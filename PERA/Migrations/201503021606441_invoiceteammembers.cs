namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoiceteammembers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoiceTeamMembers",
                c => new
                    {
                        pk = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        TokenID = c.Int(nullable: false),
                        GarageID = c.Int(),
                    })
                .PrimaryKey(t => t.pk)
                .ForeignKey("dbo.Garages", t => t.GarageID)
                .Index(t => t.GarageID);
            
            AddColumn("dbo.Invoices", "InvoiceTeamMember_pk", c => c.Int());
            CreateIndex("dbo.Invoices", "InvoiceTeamMember_pk");
            AddForeignKey("dbo.Invoices", "InvoiceTeamMember_pk", "dbo.InvoiceTeamMembers", "pk");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "InvoiceTeamMember_pk", "dbo.InvoiceTeamMembers");
            DropForeignKey("dbo.InvoiceTeamMembers", "GarageID", "dbo.Garages");
            DropIndex("dbo.InvoiceTeamMembers", new[] { "GarageID" });
            DropIndex("dbo.Invoices", new[] { "InvoiceTeamMember_pk" });
            DropColumn("dbo.Invoices", "InvoiceTeamMember_pk");
            DropTable("dbo.InvoiceTeamMembers");
        }
    }
}
