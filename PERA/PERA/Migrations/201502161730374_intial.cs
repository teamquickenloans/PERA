namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intial : DbMigration
    {
        public override void Up()
        {
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
                        MinimumNumberOfTransientSpaces = c.Int(),
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
                "dbo.TeamMembers",
                c => new
                    {
                        BadgeID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CommonID = c.Int(nullable: false),
                        GarageID = c.Int(),
                        EmploymentStatus = c.Int(),
                        TerminationDate = c.DateTime(nullable: false),
                        ParkingStatus = c.Int(),
                        EnrollmentDate = c.DateTime(nullable: false),
                        Invoice_ID = c.Int(),
                    })
                .PrimaryKey(t => t.BadgeID)
                .ForeignKey("dbo.Garages", t => t.GarageID)
                .ForeignKey("dbo.Invoices", t => t.Invoice_ID)
                .Index(t => t.GarageID)
                .Index(t => t.Invoice_ID);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GarageID = c.Int(nullable: false),
                        TotalAmountBilled = c.Double(nullable: false),
                        DateReceived = c.DateTime(nullable: false),
                        BillingStartDate = c.DateTime(nullable: false),
                        BillingEndDate = c.DateTime(nullable: false),
                        NumberOfLeasedSpots = c.Int(nullable: false),
                        NumberOfValidations = c.Int(nullable: false),
                        Format = c.Int(),
                        TeamMember_BadgeID = c.Int(),
                        TeamMember_BadgeID1 = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Garages", t => t.GarageID, cascadeDelete: true)
                .ForeignKey("dbo.TeamMembers", t => t.TeamMember_BadgeID)
                .ForeignKey("dbo.TeamMembers", t => t.TeamMember_BadgeID1)
                .Index(t => t.GarageID)
                .Index(t => t.TeamMember_BadgeID)
                .Index(t => t.TeamMember_BadgeID1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "TeamMember_BadgeID1", "dbo.TeamMembers");
            DropForeignKey("dbo.Invoices", "TeamMember_BadgeID", "dbo.TeamMembers");
            DropForeignKey("dbo.TeamMembers", "Invoice_ID", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "GarageID", "dbo.Garages");
            DropForeignKey("dbo.TeamMembers", "GarageID", "dbo.Garages");
            DropForeignKey("dbo.Garages", "GarageManagerID", "dbo.GarageManagers");
            DropIndex("dbo.Invoices", new[] { "TeamMember_BadgeID1" });
            DropIndex("dbo.Invoices", new[] { "TeamMember_BadgeID" });
            DropIndex("dbo.Invoices", new[] { "GarageID" });
            DropIndex("dbo.TeamMembers", new[] { "Invoice_ID" });
            DropIndex("dbo.TeamMembers", new[] { "GarageID" });
            DropIndex("dbo.Garages", new[] { "GarageManagerID" });
            DropTable("dbo.Invoices");
            DropTable("dbo.TeamMembers");
            DropTable("dbo.Garages");
            DropTable("dbo.GarageManagers");
        }
    }
}
