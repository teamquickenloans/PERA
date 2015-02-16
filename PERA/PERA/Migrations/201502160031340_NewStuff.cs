namespace PERA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewStuff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Garages", "GarageManagerID", "dbo.GarageManagers");
            DropIndex("dbo.Garages", new[] { "GarageManagerID" });
            AddColumn("dbo.GarageManagers", "CompanyName", c => c.String());
            AddColumn("dbo.Garages", "Capacity", c => c.Int(nullable: false));
            AddColumn("dbo.Garages", "AccessTokenOptional", c => c.Int());
            AlterColumn("dbo.GarageManagers", "PhoneNumber", c => c.Int());
            AlterColumn("dbo.Garages", "MinimumNumberOfTransientSpaces", c => c.Int());
            AlterColumn("dbo.Garages", "ValidationCost", c => c.Double());
            AlterColumn("dbo.Garages", "TransientSalePrice", c => c.Double());
            AlterColumn("dbo.Garages", "NumberOfValidations", c => c.Int());
            AlterColumn("dbo.Garages", "GarageManagerID", c => c.Int());
            CreateIndex("dbo.Garages", "GarageManagerID");
            AddForeignKey("dbo.Garages", "GarageManagerID", "dbo.GarageManagers", "ID");
            DropColumn("dbo.GarageManagers", "FirstName");
            DropColumn("dbo.GarageManagers", "LastName");
            DropColumn("dbo.GarageManagers", "Email");
            DropColumn("dbo.GarageManagers", "ManagementCompany");
            DropColumn("dbo.Garages", "WholeGarageCapacity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Garages", "WholeGarageCapacity", c => c.Int(nullable: false));
            AddColumn("dbo.GarageManagers", "ManagementCompany", c => c.String());
            AddColumn("dbo.GarageManagers", "Email", c => c.String());
            AddColumn("dbo.GarageManagers", "LastName", c => c.String());
            AddColumn("dbo.GarageManagers", "FirstName", c => c.String());
            DropForeignKey("dbo.Garages", "GarageManagerID", "dbo.GarageManagers");
            DropIndex("dbo.Garages", new[] { "GarageManagerID" });
            AlterColumn("dbo.Garages", "GarageManagerID", c => c.Int(nullable: false));
            AlterColumn("dbo.Garages", "NumberOfValidations", c => c.Int(nullable: false));
            AlterColumn("dbo.Garages", "TransientSalePrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Garages", "ValidationCost", c => c.Double(nullable: false));
            AlterColumn("dbo.Garages", "MinimumNumberOfTransientSpaces", c => c.Int(nullable: false));
            AlterColumn("dbo.GarageManagers", "PhoneNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Garages", "AccessTokenOptional");
            DropColumn("dbo.Garages", "Capacity");
            DropColumn("dbo.GarageManagers", "CompanyName");
            CreateIndex("dbo.Garages", "GarageManagerID");
            AddForeignKey("dbo.Garages", "GarageManagerID", "dbo.GarageManagers", "ID", cascadeDelete: true);
        }
    }
}
