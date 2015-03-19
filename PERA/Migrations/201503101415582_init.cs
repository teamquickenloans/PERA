namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
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
                        TeamMember_primaryKey = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Garages", t => t.GarageID, cascadeDelete: true)
                .ForeignKey("dbo.TeamMembers", t => t.TeamMember_primaryKey)
                .Index(t => t.GarageID)
                .Index(t => t.TeamMember_primaryKey);
            
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
                        primaryKey = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CommonID = c.Int(nullable: false),
                        GarageID = c.Int(),
                        BadgeID = c.Int(nullable: false),
                        EmploymentStatus = c.Int(),
                        TerminationDate = c.DateTime(),
                        ParkingStatus = c.Int(),
                        EnrollmentDate = c.DateTime(),
                        QLPerspective = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.primaryKey)
                .ForeignKey("dbo.Garages", t => t.GarageID)
                .Index(t => t.GarageID);
            
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
                "dbo.InvoiceActiveParkerReports",
                c => new
                    {
                        MonthYear = c.DateTime(nullable: false),
                        LeasedSpots = c.Int(),
                        InvoiceID = c.Int(nullable: false),
                        GarageID = c.Int(nullable: false),
                        DateReceived = c.DateTime(nullable: false),
                        DateUploaded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MonthYear)
                .ForeignKey("dbo.Garages", t => t.GarageID, cascadeDelete: true)
                .ForeignKey("dbo.Invoices", t => t.InvoiceID, cascadeDelete: true)
                .Index(t => t.InvoiceID)
                .Index(t => t.GarageID);
            
            CreateTable(
                "dbo.ParkerReportTeamMembers",
                c => new
                    {
                        FirstName = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(),
                        BusinessHoursTokenID = c.Int(nullable: false),
                        AfterHoursTokenID = c.Int(nullable: false),
                        InvoiceActiveParkerReport_MonthYear = c.DateTime(),
                    })
                .PrimaryKey(t => t.FirstName)
                .ForeignKey("dbo.InvoiceActiveParkerReports", t => t.InvoiceActiveParkerReport_MonthYear)
                .Index(t => t.InvoiceActiveParkerReport_MonthYear);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceID = c.Int(nullable: false, identity: true),
                        TotalAmountBilled = c.Double(nullable: false),
                        DateReceived = c.DateTime(nullable: false),
                        DateUploaded = c.DateTime(nullable: false),
                        MonthYear = c.DateTime(nullable: false),
                        TotalLeasedSpots = c.Int(),
                        Validations = c.Int(),
                        Garage_GarageID = c.Int(),
                    })
                .PrimaryKey(t => t.InvoiceID)
                .ForeignKey("dbo.Garages", t => t.Garage_GarageID)
                .Index(t => t.Garage_GarageID);
            
            CreateTable(
                "dbo.QLActiveParkerReports",
                c => new
                    {
                        MonthYear = c.DateTime(nullable: false),
                        AllocatedSpots = c.Int(),
                        InvoiceID = c.Int(nullable: false),
                        GarageID = c.Int(nullable: false),
                        DateReceived = c.DateTime(nullable: false),
                        DateUploaded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MonthYear)
                .ForeignKey("dbo.Garages", t => t.GarageID, cascadeDelete: true)
                .Index(t => t.GarageID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QLActiveParkerReports", "GarageID", "dbo.Garages");
            DropForeignKey("dbo.InvoiceActiveParkerReports", "InvoiceID", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "Garage_GarageID", "dbo.Garages");
            DropForeignKey("dbo.ParkerReportTeamMembers", "InvoiceActiveParkerReport_MonthYear", "dbo.InvoiceActiveParkerReports");
            DropForeignKey("dbo.InvoiceActiveParkerReports", "GarageID", "dbo.Garages");
            DropForeignKey("dbo.CarpoolGroups", "TeamMember_primaryKey", "dbo.TeamMembers");
            DropForeignKey("dbo.BadgeScans", "TeamMember_primaryKey", "dbo.TeamMembers");
            DropForeignKey("dbo.TeamMembers", "GarageID", "dbo.Garages");
            DropForeignKey("dbo.BadgeScans", "GarageID", "dbo.Garages");
            DropForeignKey("dbo.Garages", "GarageManagerID", "dbo.GarageManagers");
            DropIndex("dbo.QLActiveParkerReports", new[] { "GarageID" });
            DropIndex("dbo.Invoices", new[] { "Garage_GarageID" });
            DropIndex("dbo.ParkerReportTeamMembers", new[] { "InvoiceActiveParkerReport_MonthYear" });
            DropIndex("dbo.InvoiceActiveParkerReports", new[] { "GarageID" });
            DropIndex("dbo.InvoiceActiveParkerReports", new[] { "InvoiceID" });
            DropIndex("dbo.CarpoolGroups", new[] { "TeamMember_primaryKey" });
            DropIndex("dbo.TeamMembers", new[] { "GarageID" });
            DropIndex("dbo.Garages", new[] { "GarageManagerID" });
            DropIndex("dbo.BadgeScans", new[] { "TeamMember_primaryKey" });
            DropIndex("dbo.BadgeScans", new[] { "GarageID" });
            DropTable("dbo.QLActiveParkerReports");
            DropTable("dbo.Invoices");
            DropTable("dbo.ParkerReportTeamMembers");
            DropTable("dbo.InvoiceActiveParkerReports");
            DropTable("dbo.CarpoolGroups");
            DropTable("dbo.TeamMembers");
            DropTable("dbo.GarageManagers");
            DropTable("dbo.Garages");
            DropTable("dbo.BadgeScans");
        }
    }
}
