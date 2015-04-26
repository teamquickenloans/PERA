namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BadgeScans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GarageID = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ScanDateTime = c.DateTime(nullable: false),
                        BadgeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Garages", t => t.GarageID, cascadeDelete: true)
                .Index(t => t.GarageID);
            
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
                        InvoiceNameColumn = c.Int(),
                        InvoiceFirstNameColumn = c.Int(),
                        InvoiceLastNameColumn = c.Int(),
                        InvoiceTokenColumn = c.Int(),
                        QLFirstNameColumn = c.Int(),
                        QLLastNameColumn = c.Int(),
                        QLNameColumn = c.Int(),
                        QLTokenColumn = c.Int(),
                        QLTokenAColumn = c.Int(),
                        QLTokenBColumn = c.Int(),
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
                "dbo.CarpoolGroups",
                c => new
                    {
                        CarpoolGroupID = c.Int(nullable: false, identity: true),
                        BadgeID = c.Int(nullable: false),
                        TeamMember_primaryKey = c.Int(),
                    })
                .PrimaryKey(t => t.CarpoolGroupID)
                .ForeignKey("dbo.TeamMembers", t => t.TeamMember_primaryKey)
                .Index(t => t.TeamMember_primaryKey);
            
            CreateTable(
                "dbo.TeamMembers",
                c => new
                    {
                        primaryKey = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CommonID = c.Int(nullable: false),
                        BadgeID = c.Int(nullable: false),
                        TokenID = c.Int(nullable: false),
                        EmploymentStatus = c.Int(),
                        TerminationDate = c.DateTime(),
                        ParkingStatus = c.Int(),
                        EnrollmentDate = c.DateTime(),
                        QLPerspective = c.Boolean(nullable: false),
                        Garage_GarageID = c.Int(),
                    })
                .PrimaryKey(t => t.primaryKey)
                .ForeignKey("dbo.Garages", t => t.Garage_GarageID)
                .Index(t => t.Garage_GarageID);
            
            CreateTable(
                "dbo.Discrepancies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        TokenID = c.Int(),
                        Action = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.InvoiceActiveParkerReports",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InvoiceID = c.Int(nullable: false),
                        LeasedSpots = c.Int(),
                        GarageID = c.Int(nullable: false),
                        DateReceived = c.DateTime(),
                        DateUploaded = c.DateTime(nullable: false),
                        MonthYear = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Garages", t => t.GarageID, cascadeDelete: true)
                //.ForeignKey("dbo.Invoices", t => t.InvoiceID, cascadeDelete: true)
                .Index(t => t.InvoiceID)
                .Index(t => t.GarageID);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceID = c.Int(nullable: false, identity: true),
                        ID = c.Int(nullable: false),
                        TotalAmountBilled = c.Double(nullable: false),
                        DateReceived = c.DateTime(),
                        DateUploaded = c.DateTime(nullable: false),
                        MonthYear = c.DateTime(nullable: false),
                        TotalLeasedSpots = c.Int(),
                        Validations = c.Int(),
                    })
                .PrimaryKey(t => t.InvoiceID);
            
            CreateTable(
                "dbo.ParkerReportTeamMembers",
                c => new
                    {
                        FirstName = c.String(),
                        LastName = c.String(),
                        BusinessHoursTokenID = c.Int(),
                        ParkerReportTeamMemberID = c.Int(nullable: false, identity: true),
                        AfterHoursTokenID = c.Int(),
                        BadgeID = c.Int(),
                        TokenID = c.Int(),
                        InvoiceActiveParkerReportID = c.Int(),
                        COMMON_ID = c.Int(),
                        ID_CODE_26W = c.Int(),
                        FACILITY_CODE_26W = c.Int(),
                        HID_CORP1K_ID = c.Int(),
                        QLActiveParkerReportID = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ParkerReportTeamMemberID)
                .ForeignKey("dbo.InvoiceActiveParkerReports", t => t.InvoiceActiveParkerReportID)
                .ForeignKey("dbo.QLActiveParkerReports", t => t.QLActiveParkerReportID, cascadeDelete: true)
                .Index(t => t.InvoiceActiveParkerReportID)
                .Index(t => t.QLActiveParkerReportID);
            
            CreateTable(
                "dbo.QLActiveParkerReports",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AllocatedSpots = c.Int(),
                        GarageID = c.Int(nullable: false),
                        DateReceived = c.DateTime(),
                        DateUploaded = c.DateTime(nullable: false),
                        MonthYear = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Garages", t => t.GarageID, cascadeDelete: true)
                .Index(t => t.GarageID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkerReportTeamMembers", "QLActiveParkerReportID", "dbo.QLActiveParkerReports");
            DropForeignKey("dbo.QLActiveParkerReports", "GarageID", "dbo.Garages");
            DropForeignKey("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReportID", "dbo.InvoiceActiveParkerReports");
            DropForeignKey("dbo.InvoiceActiveParkerReports", "InvoiceID", "dbo.Invoices");
            DropForeignKey("dbo.InvoiceActiveParkerReports", "GarageID", "dbo.Garages");
            DropForeignKey("dbo.CarpoolGroups", "TeamMember_primaryKey", "dbo.TeamMembers");
            DropForeignKey("dbo.TeamMembers", "Garage_GarageID", "dbo.Garages");
            DropForeignKey("dbo.BadgeScans", "GarageID", "dbo.Garages");
            DropForeignKey("dbo.Garages", "GarageManagerID", "dbo.GarageManagers");
            DropIndex("dbo.QLActiveParkerReports", new[] { "GarageID" });
            DropIndex("dbo.ParkerReportTeamMembers", new[] { "QLActiveParkerReportID" });
            DropIndex("dbo.ParkerReportTeamMembers", new[] { "InvoiceActiveParkerReportID" });
            DropIndex("dbo.InvoiceActiveParkerReports", new[] { "GarageID" });
            DropIndex("dbo.InvoiceActiveParkerReports", new[] { "InvoiceID" });
            DropIndex("dbo.TeamMembers", new[] { "Garage_GarageID" });
            DropIndex("dbo.CarpoolGroups", new[] { "TeamMember_primaryKey" });
            DropIndex("dbo.Garages", new[] { "GarageManagerID" });
            DropIndex("dbo.BadgeScans", new[] { "GarageID" });
            DropTable("dbo.QLActiveParkerReports");
            DropTable("dbo.ParkerReportTeamMembers");
            DropTable("dbo.Invoices");
            DropTable("dbo.InvoiceActiveParkerReports");
            DropTable("dbo.Discrepancies");
            DropTable("dbo.TeamMembers");
            DropTable("dbo.CarpoolGroups");
            DropTable("dbo.GarageManagers");
            DropTable("dbo.Garages");
            DropTable("dbo.BadgeScans");
        }
    }
}
