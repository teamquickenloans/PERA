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
        /*
        // GET: api/BadgeScans/GetNumberOfScans
        public int GetNumberOfScans(int id, string firstName, string lastName)
        {
            BadgeScanReport report = db.BadgeScanReports.Where(x => x.GarageID == id).OrderByDescending(x => x.MonthYear).FirstOrDefault();

            List<BadgeScan> scans = report.BadgeScans.Where(x => x.FirstName == firstName && x.LastName == lastName).ToList();

            return scans.Count;
        }
        */
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
          //  List<BadgeScan> scans = report.BadgeScans.ToList();
            List<BadgeScan> scans = db.BadgeScans.Where(x => 1002 == report.ID).ToList();
            
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