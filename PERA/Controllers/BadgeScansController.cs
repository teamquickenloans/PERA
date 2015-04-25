using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Diagnostics;
using PERA.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace PERA.Controllers
{
    public class BadgeScansController : ApiController
    {
        private PERAContext db = new PERAContext();

        // GET: api/BadgeScans
        public IQueryable<BadgeScan> GetBadgeScans()
        {
            return db.BadgeScans;
        }

        // GET: api/BadgeScans/5
        [ResponseType(typeof(BadgeScan))]
        public IHttpActionResult GetBadgeScan(int id)
        {
            BadgeScan badgeScan = db.BadgeScans.Find(id);
            if (badgeScan == null)
            {
                return NotFound();
            }

            return Ok(badgeScan);
        }
        
        // GET: api/BadgeScans/GetNumberOfScans
        public string GetNumberOfScans(int id)
        {
            // Grab the most recent badge scan report for this garage
            BadgeScanReport report = db.BadgeScanReports.Where(x => x.GarageID == id).OrderByDescending(x => x.MonthYear).FirstOrDefault();

            QLActiveParkerReport QLReport = db.QLActiveParkerReports.Where( x => x.GarageID == id).OrderByDescending(x => x.MonthYear).FirstOrDefault();

            Dictionary<string, int> usage = new Dictionary<string,int>();

            if (report.BadgeScans != null)
            {
                Trace.WriteLine(report.BadgeScans.Count);
            }


            // Grab all of the scans for this person in that report
            foreach(QLTeamMember qlTM in QLReport.TeamMembers) {
                List<BadgeScan> scans = report.BadgeScans.Where(x => x.FirstName == qlTM.FirstName && x.LastName == qlTM.LastName).ToList();
                usage[qlTM.FirstName + qlTM.LastName] = scans.Count;
            }

            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //string json = serializer.Serialize((object)usage);

            return JsonConvert.SerializeObject((object)usage);  
        }

        /*
        // GET: api/BadgeScans/GetNumberOfScans
        public int[] GetNumberOfScans(int id)
        {
            //Get most recent card activity report for the selected garage
            BadgeScanReport report = db.BadgeScanReports.Where(x => x.GarageID == id).OrderByDescending(x => x.MonthYear).FirstOrDefault();
            
            if (report == null)
            {
                return null;
            }

            //Organize badge scans into a list
            // List<BadgeScan> scans = report.BadgeScans.ToList();
             List<BadgeScan> scans = db.BadgeScans.Where(x => 1 == report.ID).ToList();
            
            Trace.WriteLine("VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV");
            Trace.WriteLine("Number of badge scan results for selected garage: " + scans.Count);
            Trace.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");

            //Create a dictionary to hold usage value for each person
            Dictionary<int, int> dict = new Dictionary<int, int>();

            //Gather usage data for each person in card activity report
            foreach(BadgeScan scan in scans) 
            {
                if ( !dict.ContainsKey(scan.BadgeID) )
                {
                    //dict.Add(scan.BadgeID, report.BadgeScans.Where(x => x.FirstName == scan.FirstName && x.LastName == scan.LastName).ToList().Count);
                    dict[scan.BadgeID] = db.BadgeScans.Where(x => x.FirstName == scan.FirstName && x.LastName == scan.LastName).ToList().Count;
                }
            }

            //Convert dictionary to array to allow for passing back to javascript
            int[] dictArray = new int[dict.Count];
            dict.Values.CopyTo(dictArray, 0);

            return dictArray;
        }
        */
        // PUT: api/BadgeScans/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBadgeScan(int id, BadgeScan badgeScan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != badgeScan.ID)
            {
                return BadRequest();
            }

            db.Entry(badgeScan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BadgeScanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: api/BadgeScans/getBadgeScans/5
        //[HttpGet]
        //[ActionName("Garage")]
        //[ResponseType(typeof(BadgeScan))]
        //public ICollection<BadgeScan> GetBadgeScans(string id)
        //{
        //    int garageid = Convert.ToInt32(id);
        //    BadgeScan BadgeScan = db.BadgeScans.Where(
        //        x => x.GarageID == garageid).OrderByDescending(x => x.MonthYear).FirstOrDefault();

        //    if (QLReport == null)
        //    {
        //        return null;
        //    }

        //    return QLReport.TeamMembers;
        //}


        // POST: api/BadgeScans
        [ResponseType(typeof(BadgeScan))]
        public IHttpActionResult PostBadgeScan(BadgeScan badgeScan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BadgeScans.Add(badgeScan);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = badgeScan.ID }, badgeScan);
        }

        // DELETE: api/BadgeScans/5
        [ResponseType(typeof(BadgeScan))]
        public IHttpActionResult DeleteBadgeScan(int id)
        {
            BadgeScan badgeScan = db.BadgeScans.Find(id);
            if (badgeScan == null)
            {
                return NotFound();
            }

            db.BadgeScans.Remove(badgeScan);
            db.SaveChanges();

            return Ok(badgeScan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BadgeScanExists(int id)
        {
            return db.BadgeScans.Count(e => e.ID == id) > 0;
        }
    }
}