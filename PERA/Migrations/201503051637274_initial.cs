namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BadgeScans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GarageID = c.Int(nullable: false),
                        BadgeID = c.Int(nullable: false),
                        ScanInDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Garages", t => t.GarageID, cascadeDelete: true)
                .ForeignKey("dbo.TeamMembers", t => t.BadgeID, cascadeDelete: true)
                .Index(t => t.GarageID)
                .Index(t => t.BadgeID);
            
            CreateTable(
                "dbo.Garages",
                c => new
                    {
                        GarageID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Capacity = c.Int(nullable: false),
                        NumberOfLeasedSpaces = c.Int(nullable: false),
                        NumberOfTeamMemberSpaces = c.Int(nullable: false),
                        MinimumNumberOfBufferSpaces = c.Int(),
                        SpaceCost = c.Double(nullable: false),
                        TransientSalePrice = c.Double(),
                        Owner = c.String(),
                        BillingParty = c.String(),
                        ReportType = c.Int(nullable: false),
                        AccessToken = c.Int(nullable: false),
                        AccessTokenOptional = c.Int(),
                        AccessTokenCost = c.Double(nullable: false),
                        ChangeCost = c.Double(nullable: false),
                        ValidationCost = c.Double(),
                        NumberOfValidations = c.Int(),
                        GarageManagerID = c.Int(),
                    })
                .PrimaryKey(t => t.GarageID)
                .ForeignKey("dbo.GarageManagers", t => t.GarageManagerID)
                .Index(t => t.GarageManagerID);
            
            CreateTable(
                "dbo.GarageManagers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.Int(),
                        CompanyName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TeamMembers",
                c => new
                    {
                        BadgeID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CommonID = c.Int(nullable: false),
                        TokenID = c.Int(nullable: false),
                        GarageID = c.Int(),
                        EmploymentStatus = c.Int(),
                        TerminationDate = c.DateTime(nullable: false),
                        ParkingStatus = c.Int(),
                        EnrollmentDate = c.DateTime(nullable: false),
                        Invoice_InvoiceID = c.Int(),
                    })
                .PrimaryKey(t => t.BadgeID)
                .ForeignKey("dbo.Garages", t => t.GarageID)
                .ForeignKey("dbo.Invoices", t => t.Invoice_InvoiceID)
                .Index(t => t.GarageID)
                .Index(t => t.Invoice_InvoiceID);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceID = c.Int(nullable: false, identity: true),
                        GarageID = c.Int(nullable: false),
                        TotalAmountBilled = c.Double(nullable: false),
                        DateReceived = c.DateTime(nullable: false),
                        BillingStartDate = c.DateTime(nullable: false),
                        BillingEndDate = c.DateTime(nullable: false),
                        NumberOfLeasedSpots = c.Int(),
                        NumberOfValidations = c.Int(),
                        Format = c.Int(),
                        TeamMember_BadgeID = c.Int(),
                        TeamMember_BadgeID1 = c.Int(),
                        InvoiceTeamMember_pk = c.Int(),
                    })
                .PrimaryKey(t => t.InvoiceID)
                .ForeignKey("dbo.Garages", t => t.GarageID, cascadeDelete: true)
                .ForeignKey("dbo.TeamMembers", t => t.TeamMember_BadgeID)
                .ForeignKey("dbo.TeamMembers", t => t.TeamMember_BadgeID1)
                .ForeignKey("dbo.InvoiceTeamMembers", t => t.InvoiceTeamMember_pk)
                .Index(t => t.GarageID)
                .Index(t => t.TeamMember_BadgeID)
                .Index(t => t.TeamMember_BadgeID1)
                .Index(t => t.InvoiceTeamMember_pk);
            
            CreateTable(
                "dbo.CarpoolGroups",
                c => new
                    {
                        CarpoolGroupID = c.Int(nullable: false, identity: true),
                        BadgeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CarpoolGroupID)
                .ForeignKey("dbo.TeamMembers", t => t.BadgeID, cascadeDelete: true)
                .Index(t => t.BadgeID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "InvoiceTeamMember_pk", "dbo.InvoiceTeamMembers");
            DropForeignKey("dbo.InvoiceTeamMembers", "GarageID", "dbo.Garages");
            DropForeignKey("dbo.CarpoolGroups", "BadgeID", "dbo.TeamMembers");
            DropForeignKey("dbo.BadgeScans", "BadgeID", "dbo.TeamMembers");
            DropForeignKey("dbo.Invoices", "TeamMember_BadgeID1", "dbo.TeamMembers");
            DropForeignKey("dbo.Invoices", "TeamMember_BadgeID", "dbo.TeamMembers");
            DropForeignKey("dbo.TeamMembers", "Invoice_InvoiceID", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "GarageID", "dbo.Garages");
            DropForeignKey("dbo.TeamMembers", "GarageID", "dbo.Garages");
            DropForeignKey("dbo.BadgeScans", "GarageID", "dbo.Garages");
            DropForeignKey("dbo.Garages", "GarageManagerID", "dbo.GarageManagers");
            DropIndex("dbo.InvoiceTeamMembers", new[] { "GarageID" });
            DropIndex("dbo.CarpoolGroups", new[] { "BadgeID" });
            DropIndex("dbo.Invoices", new[] { "InvoiceTeamMember_pk" });
            DropIndex("dbo.Invoices", new[] { "TeamMember_BadgeID1" });
            DropIndex("dbo.Invoices", new[] { "TeamMember_BadgeID" });
            DropIndex("dbo.Invoices", new[] { "GarageID" });
            DropIndex("dbo.TeamMembers", new[] { "Invoice_InvoiceID" });
            DropIndex("dbo.TeamMembers", new[] { "GarageID" });
            DropIndex("dbo.Garages", new[] { "GarageManagerID" });
            DropIndex("dbo.BadgeScans", new[] { "BadgeID" });
            DropIndex("dbo.BadgeScans", new[] { "GarageID" });
            DropTable("dbo.InvoiceTeamMembers");
            DropTable("dbo.CarpoolGroups");
            DropTable("dbo.Invoices");
            DropTable("dbo.TeamMembers");
            DropTable("dbo.GarageManagers");
            DropTable("dbo.Garages");
            DropTable("dbo.BadgeScans");
        }
    }
}
