namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoiceteammembers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParkerReportTeamMembers",
                c => new
                    {
                        pk = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BadgeID = c.Int(nullable: false),
                        GarageID = c.Int(),
                    })
                .PrimaryKey(t => t.pk)
                .ForeignKey("dbo.Garages", t => t.GarageID)
                .Index(t => t.GarageID);
            
            AddColumn("dbo.Invoices", "ParkerReportTeamMember_pk", c => c.Int());
            CreateIndex("dbo.Invoices", "ParkerReportTeamMember_pk");
            AddForeignKey("dbo.Invoices", "ParkerReportTeamMember_pk", "dbo.ParkerReportTeamMembers", "pk");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "ParkerReportTeamMember_pk", "dbo.ParkerReportTeamMembers");
            DropForeignKey("dbo.ParkerReportTeamMembers", "GarageID", "dbo.Garages");
            DropIndex("dbo.ParkerReportTeamMembers", new[] { "GarageID" });
            DropIndex("dbo.Invoices", new[] { "ParkerReportTeamMember_pk" });
            DropColumn("dbo.Invoices", "ParkerReportTeamMember_pk");
            DropTable("dbo.ParkerReportTeamMembers");
        }
    }
}
