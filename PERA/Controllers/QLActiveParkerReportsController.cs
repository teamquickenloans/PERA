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
    public class QLActiveParkerReportsController : ApiController
    {
        private PERAContext db = new PERAContext();

        // GET: api/QLActiveParkerReports
        public IQueryable<QLActiveParkerReport> GetQLActiveParkerReports()
        {
            return db.QLActiveParkerReports;
        }

        // GET: api/QLActiveParkerReports/5
        [ResponseType(typeof(QLActiveParkerReport))]
        public IHttpActionResult GetQLActiveParkerReport(int id)
        {
            QLActiveParkerReport qLActiveParkerReport = db.QLActiveParkerReports.Find(id);
            if (qLActiveParkerReport == null)
            {
                return NotFound();
            }

            return Ok(qLActiveParkerReport);
        }


        // GET: api/QLActiveParkerReports/garage/5
        [HttpGet]
        [ActionName("Garage")]
        [ResponseType(typeof(QLActiveParkerReport))]
        public QLActiveParkerReport GetQLActiveParkerReportbyGarage(int id)
        {
            QLActiveParkerReport QLReport = db.QLActiveParkerReports.Where(
                x => x.GarageID == id).OrderByDescending(x => x.MonthYear).FirstOrDefault();

            if (QLReport == null)
            {
                return null;
            }

            return QLReport;
        }


        // PUT: api/QLActiveParkerReports/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQLActiveParkerReport(int id, QLActiveParkerReport qLActiveParkerReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != qLActiveParkerReport.ID)
            {
                return BadRequest();
            }

            db.Entry(qLActiveParkerReport).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QLActiveParkerReportExists(id))
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

        // POST: api/QLActiveParkerReports
        [ResponseType(typeof(QLActiveParkerReport))]
        public IHttpActionResult PostQLActiveParkerReport(QLActiveParkerReport qLActiveParkerReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.QLActiveParkerReports.Add(qLActiveParkerReport);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = qLActiveParkerReport.ID }, qLActiveParkerReport);
        }

        // DELETE: api/QLActiveParkerReports/5
        [ResponseType(typeof(QLActiveParkerReport))]
        public IHttpActionResult DeleteQLActiveParkerReport(int id)
        {
            QLActiveParkerReport qLActiveParkerReport = db.QLActiveParkerReports.Find(id);
            if (qLActiveParkerReport == null)
            {
                return NotFound();
            }

            db.QLActiveParkerReports.Remove(qLActiveParkerReport);
            db.SaveChanges();

            return Ok(qLActiveParkerReport);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QLActiveParkerReportExists(int id)
        {
            return db.QLActiveParkerReports.Count(e => e.ID == id) > 0;
        }
    }
}