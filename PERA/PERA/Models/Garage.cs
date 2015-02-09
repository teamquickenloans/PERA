using PERA.Models;
using System;
using System.Collections.Generic;
using System.Device.Location;


namespace PERA.Models
{
    public enum ReportType
    {
        A, B, C
    }
    // B: badge
    // P: puck
    // H: hangtag
    public enum AccessToken
    {
        B, P, H
    }
	public class Garage
	{
        public int GarageID { get; set; } // pk
        public string Name { get; set; }
        public string Address { get; set; }
        public GeoCoordinate LatitudeLongitude { get; set;}
        public int WholeGarageCapacity { get; set; }
        public int NumberOfLeasedSpaces { get; set; }
        public int NumberOfTeamMemberSpaces { get; set; }
        public int MinimumNumberOfTransientSpaces { get; set; }
        public double SpaceCost { get; set; }
        public double ValidationCost { get; set; }
        public double TransientSalePrice { get; set; }
        public string Owner { get; set; }
        public string BillingParty { get; set; }
        public ReportType ReportType { get; set; }
        public AccessToken AccessToken { get; set; }
        public int AccessTokenCost { get; set; }
        public double ChangeCost { get; set; }
        public int NumberOfValidations { get; set; }
        public int GarageManagerID { get; set; }

        public virtual GarageManager GarageManager { get; set; }
	}
}
