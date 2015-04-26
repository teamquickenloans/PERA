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
using PERA.Models;

namespace PERA.Controllers
{
    public class BadgeScanReportsController : ApiController
    {
        private PERAContext db = new PERAContext();

        // GET: api/BadgeScanReports
        public IQueryable<BadgeScanReport> GetBadgeScanReports()
        {
            return db.BadgeScanReports;
        }

        // GET: api/BadgeScanReports/5
        [ResponseType(typeof(BadgeScanReport))]
        public IHttpActionResult GetBadgeScanReport(int id)
        {
            BadgeScanReport badgeScanReport = db.BadgeScanReports.Find(id);
            if (badgeScanReport == null)
            {
                return NotFound();
            }

            return Ok(badgeScanReport);
        }

        // PUT: api/BadgeScanReports/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBadgeScanReport(int id, BadgeScanReport badgeScanReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != badgeScanReport.ID)
            {
                return BadRequest();
            }

            db.Entry(badgeScanReport).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BadgeScanReportExists(id))
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

        // POST: api/BadgeScanReports
        [ResponseType(typeof(BadgeScanReport))]
        public IHttpActionResult PostBadgeScanReport(BadgeScanReport badgeScanReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BadgeScanReports.Add(badgeScanReport);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = badgeScanReport.ID }, badgeScanReport);
        }

        // DELETE: api/BadgeScanReports/5
        [ResponseType(typeof(BadgeScanReport))]
        public IHttpActionResult DeleteBadgeScanReport(int id)
        {
            BadgeScanReport badgeScanReport = db.BadgeScanReports.Find(id);
            if (badgeScanReport == null)
            {
                return NotFound();
            }

            db.BadgeScanReports.Remove(badgeScanReport);
            db.SaveChanges();

            return Ok(badgeScanReport);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BadgeScanReportExists(int id)
        {
            return db.BadgeScanReports.Count(e => e.ID == id) > 0;
        }
    }
}