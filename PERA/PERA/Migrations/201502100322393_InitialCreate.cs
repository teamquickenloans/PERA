namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BadgeScan",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GarageID = c.Int(nullable: false),
                        BadgeID = c.Int(nullable: false),
                        ScanInDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Garage", t => t.GarageID, cascadeDelete: true)
                .ForeignKey("dbo.TeamMember", t => t.BadgeID, cascadeDelete: true)
                .Index(t => t.GarageID)
                .Index(t => t.BadgeID);
            
            CreateTable(
                "dbo.Garage",
                c => new
                    {
                        GarageID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        WholeGarageCapacity = c.Int(nullable: false),
                        NumberOfLeasedSpaces = c.Int(nullable: false),
                        NumberOfTeamMemberSpaces = c.Int(),
                        MinimumNumberOfTransientSpaces = c.Int(),
                        SpaceCost = c.Double(),
                        ValidationCost = c.Double(),
                        TransientSalePrice = c.Double(),
                        Owner = c.String(),
                        BillingParty = c.String(),
                        ReportType = c.Int(),
                        AccessToken = c.Int(),
                        AccessTokenCost = c.Int(),
                        ChangeCost = c.Double(),
                        NumberOfValidations = c.Int(),
                        GarageManager_ID = c.Int(),
                    })
                .PrimaryKey(t => t.GarageID)
                .ForeignKey("dbo.GarageManager", t => t.GarageManager_ID)
                .Index(t => t.GarageManager_ID);
            
            CreateTable(
                "dbo.GarageManager",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        ManagementCompany = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TeamMember",
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
                        Invoice_InvoiceID = c.Int(),
                    })
                .PrimaryKey(t => t.BadgeID)
                .ForeignKey("dbo.Garage", t => t.GarageID)
                .ForeignKey("dbo.Invoice", t => t.Invoice_InvoiceID)
                .Index(t => t.GarageID)
                .Index(t => t.Invoice_InvoiceID);
            
            CreateTable(
                "dbo.Invoice",
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
                    })
                .PrimaryKey(t => t.InvoiceID)
                .ForeignKey("dbo.Garage", t => t.GarageID, cascadeDelete: true)
                .ForeignKey("dbo.TeamMember", t => t.TeamMember_BadgeID)
                .ForeignKey("dbo.TeamMember", t => t.TeamMember_BadgeID1)
                .Index(t => t.GarageID)
                .Index(t => t.TeamMember_BadgeID)
                .Index(t => t.TeamMember_BadgeID1);
            
            CreateTable(
                "dbo.CarpoolGroup",
                c => new
                    {
                        CarpoolGroupID = c.Int(nullable: false, identity: true),
                        BadgeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CarpoolGroupID)
                .ForeignKey("dbo.TeamMember", t => t.BadgeID, cascadeDelete: true)
                .Index(t => t.BadgeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarpoolGroup", "BadgeID", "dbo.TeamMember");
            DropForeignKey("dbo.BadgeScan", "BadgeID", "dbo.TeamMember");
            DropForeignKey("dbo.Invoice", "TeamMember_BadgeID1", "dbo.TeamMember");
            DropForeignKey("dbo.Invoice", "TeamMember_BadgeID", "dbo.TeamMember");
            DropForeignKey("dbo.TeamMember", "Invoice_InvoiceID", "dbo.Invoice");
            DropForeignKey("dbo.Invoice", "GarageID", "dbo.Garage");
            DropForeignKey("dbo.TeamMember", "GarageID", "dbo.Garage");
            DropForeignKey("dbo.BadgeScan", "GarageID", "dbo.Garage");
            DropForeignKey("dbo.Garage", "GarageManager_ID", "dbo.GarageManager");
            DropIndex("dbo.CarpoolGroup", new[] { "BadgeID" });
            DropIndex("dbo.Invoice", new[] { "TeamMember_BadgeID1" });
            DropIndex("dbo.Invoice", new[] { "TeamMember_BadgeID" });
            DropIndex("dbo.Invoice", new[] { "GarageID" });
            DropIndex("dbo.TeamMember", new[] { "Invoice_InvoiceID" });
            DropIndex("dbo.TeamMember", new[] { "GarageID" });
            DropIndex("dbo.Garage", new[] { "GarageManager_ID" });
            DropIndex("dbo.BadgeScan", new[] { "BadgeID" });
            DropIndex("dbo.BadgeScan", new[] { "GarageID" });
            DropTable("dbo.CarpoolGroup");
            DropTable("dbo.Invoice");
            DropTable("dbo.TeamMember");
            DropTable("dbo.GarageManager");
            DropTable("dbo.Garage");
            DropTable("dbo.BadgeScan");
        }
    }
}
