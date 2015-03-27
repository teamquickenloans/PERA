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
    public class InvoiceTeamMembersController : ApiController
    {
        private PERAContext db = new PERAContext();

        // GET: api/InvoiceTeamMembers
        public IQueryable<InvoiceTeamMember> GetInvoiceTeamMembers()
        {
            return db.InvoiceTeamMembers;
        }

        // GET: api/InvoiceTeamMembers/5
        [ResponseType(typeof(InvoiceTeamMember))]
        public IHttpActionResult GetInvoiceTeamMember(int id)
        {
            InvoiceTeamMember invoiceTeamMember = db.InvoiceTeamMembers.Find(id);
            if (invoiceTeamMember == null)
            {
                return NotFound();
            }

            return Ok(invoiceTeamMember);
        }

        // PUT: api/InvoiceTeamMembers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInvoiceTeamMember(int id, InvoiceTeamMember invoiceTeamMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoiceTeamMember.pk)
            {
                return BadRequest();
            }

            db.Entry(invoiceTeamMember).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceTeamMemberExists(id))
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

        // POST: api/InvoiceTeamMembers
        [ResponseType(typeof(InvoiceTeamMember))]
        public IHttpActionResult PostInvoiceTeamMember(InvoiceTeamMember invoiceTeamMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InvoiceTeamMembers.Add(invoiceTeamMember);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = invoiceTeamMember.pk }, invoiceTeamMember);
        }

        // DELETE: api/InvoiceTeamMembers/5
        [ResponseType(typeof(InvoiceTeamMember))]
        public IHttpActionResult DeleteInvoiceTeamMember(int id)
        {
            InvoiceTeamMember invoiceTeamMember = db.InvoiceTeamMembers.Find(id);
            if (invoiceTeamMember == null)
            {
                return NotFound();
            }

            db.InvoiceTeamMembers.Remove(invoiceTeamMember);
            db.SaveChanges();

            return Ok(invoiceTeamMember);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceTeamMemberExists(int id)
        {
            return db.InvoiceTeamMembers.Count(e => e.pk == id) > 0;
        }
    }
}