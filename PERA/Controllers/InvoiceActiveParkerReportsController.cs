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
    public class InvoiceActiveParkerReportsController : ApiController
    {
        private PERAContext db = new PERAContext();

        // GET: api/InvoiceActiveParkerReports
        public IQueryable<InvoiceActiveParkerReport> GetInvoiceActiveParkerReports()
        {
            return db.InvoiceActiveParkerReports;
        }

        // GET: api/InvoiceActiveParkerReports/5
        [ResponseType(typeof(InvoiceActiveParkerReport))]
        public IHttpActionResult GetInvoiceActiveParkerReport(int id)
        {
            InvoiceActiveParkerReport invoiceActiveParkerReport = db.InvoiceActiveParkerReports.Find(id);
            if (invoiceActiveParkerReport == null)
            {
                return NotFound();
            }

            return Ok(invoiceActiveParkerReport);
        }
        
        // GET: api/InvoiceActiveParkerReports/invoice/5
        [HttpGet]
        [ActionName("Invoice")]
        [ResponseType(typeof(InvoiceActiveParkerReport))]
        public IQueryable<InvoiceActiveParkerReport> Invoice(int id)
        {
            //int invoiceInt = Convert.ToInt32(id);
            return db.InvoiceActiveParkerReports.Where(x => x.InvoiceID == id);
        }


        // PUT: api/InvoiceActiveParkerReports/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInvoiceActiveParkerReport(int id, InvoiceActiveParkerReport invoiceActiveParkerReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoiceActiveParkerReport.ID)
            {
                return BadRequest();
            }

            db.Entry(invoiceActiveParkerReport).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceActiveParkerReportExists(id))
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

        // POST: api/InvoiceActiveParkerReports
        [ResponseType(typeof(InvoiceActiveParkerReport))]
        public IHttpActionResult PostInvoiceActiveParkerReport(InvoiceActiveParkerReport invoiceActiveParkerReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InvoiceActiveParkerReports.Add(invoiceActiveParkerReport);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = invoiceActiveParkerReport.ID }, invoiceActiveParkerReport);
        }

        // DELETE: api/InvoiceActiveParkerReports/5
        [ResponseType(typeof(InvoiceActiveParkerReport))]
        public IHttpActionResult DeleteInvoiceActiveParkerReport(int id)
        {
            InvoiceActiveParkerReport invoiceActiveParkerReport = db.InvoiceActiveParkerReports.Find(id);
            if (invoiceActiveParkerReport == null)
            {
                return NotFound();
            }

            db.InvoiceActiveParkerReports.Remove(invoiceActiveParkerReport);
            db.SaveChanges();

            return Ok(invoiceActiveParkerReport);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceActiveParkerReportExists(int id)
        {
            return db.InvoiceActiveParkerReports.Count(e => e.ID == id) > 0;
        }
    }
}