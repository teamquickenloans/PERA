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