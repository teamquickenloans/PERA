namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Garages", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Garages", "Longitude", c => c.Double(nullable: false));
            DropColumn("dbo.Garages", "LatitudeLongitude_Latitude");
            DropColumn("dbo.Garages", "LatitudeLongitude_Longitude");
            DropColumn("dbo.Garages", "LatitudeLongitude_Altitude");
            DropColumn("dbo.Garages", "LatitudeLongitude_HorizontalAccuracy");
            DropColumn("dbo.Garages", "LatitudeLongitude_VerticalAccuracy");
            DropColumn("dbo.Garages", "LatitudeLongitude_Speed");
            DropColumn("dbo.Garages", "LatitudeLongitude_Course");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Garages", "LatitudeLongitude_Course", c => c.Double(nullable: false));
            AddColumn("dbo.Garages", "LatitudeLongitude_Speed", c => c.Double(nullable: false));
            AddColumn("dbo.Garages", "LatitudeLongitude_VerticalAccuracy", c => c.Double(nullable: false));
            AddColumn("dbo.Garages", "LatitudeLongitude_HorizontalAccuracy", c => c.Double(nullable: false));
            AddColumn("dbo.Garages", "LatitudeLongitude_Altitude", c => c.Double(nullable: false));
            AddColumn("dbo.Garages", "LatitudeLongitude_Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.Garages", "LatitudeLongitude_Latitude", c => c.Double(nullable: false));
            DropColumn("dbo.Garages", "Longitude");
            DropColumn("dbo.Garages", "Latitude");
        }
    }
}
