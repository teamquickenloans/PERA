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
    public class ParkerReportTeamMembersController : ApiController
    {
        private PERAContext db = new PERAContext();

        // GET: api/ParkerReportTeamMembers
        public IQueryable<ParkerReportTeamMember> GetParkerReportTeamMembers()
        {
            return db.ParkerReportTeamMembers;
        }

        // GET: api/ParkerReportTeamMembers/5
        [ResponseType(typeof(ParkerReportTeamMember))]
        public IHttpActionResult GetParkerReportTeamMember(string id)
        {
            ParkerReportTeamMember parkerReportTeamMember = db.ParkerReportTeamMembers.Find(id);
            if (parkerReportTeamMember == null)
            {
                return NotFound();
            }

            return Ok(parkerReportTeamMember);
        }

        // GET: api/ParkerReportTeamMembers/garage/5
        [HttpGet]
        [ActionName("Garage")]
        [ResponseType(typeof(ParkerReportTeamMember))]
        public ICollection<QLTeamMember> GetParkerReportTeamMembers(string id)
        {
            int garageid = Convert.ToInt32(id);
            QLActiveParkerReport QLReport = db.QLActiveParkerReports.Where(
                x => x.GarageID == garageid).OrderByDescending(x => x.MonthYear).FirstOrDefault();

            if (QLReport == null)
            {
                //return NotFound();
            }

            return QLReport.TeamMembers;
        }

        // PUT: api/ParkerReportTeamMembers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParkerReportTeamMember(string id, ParkerReportTeamMember parkerReportTeamMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parkerReportTeamMember.FirstName)
            {
                return BadRequest();
            }

            db.Entry(parkerReportTeamMember).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkerReportTeamMemberExists(id))
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

        // POST: api/ParkerReportTeamMembers
        [ResponseType(typeof(ParkerReportTeamMember))]
        public IHttpActionResult PostParkerReportTeamMember(ParkerReportTeamMember parkerReportTeamMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ParkerReportTeamMembers.Add(parkerReportTeamMember);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ParkerReportTeamMemberExists(parkerReportTeamMember.FirstName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = parkerReportTeamMember.FirstName }, parkerReportTeamMember);
        }

        // DELETE: api/ParkerReportTeamMembers/5
        [ResponseType(typeof(ParkerReportTeamMember))]
        public IHttpActionResult DeleteParkerReportTeamMember(string id)
        {
            ParkerReportTeamMember parkerReportTeamMember = db.ParkerReportTeamMembers.Find(id);
            if (parkerReportTeamMember == null)
            {
                return NotFound();
            }

            db.ParkerReportTeamMembers.Remove(parkerReportTeamMember);
            db.SaveChanges();

            return Ok(parkerReportTeamMember);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParkerReportTeamMemberExists(string id)
        {
            return db.ParkerReportTeamMembers.Count(e => e.FirstName == id) > 0;
        }
    }
}