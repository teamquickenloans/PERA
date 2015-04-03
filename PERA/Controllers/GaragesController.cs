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
using System.Diagnostics;

namespace PERA.Controllers
{
    public class GaragesController : ApiController
    {
        private PERAContext db = new PERAContext();

        // GET: api/Garages
        public IQueryable<Garage> GetGarages()
        {
            return db.Garages;
        }

        // GET: api/Garages/5
        [ResponseType(typeof(Garage))]
        public IHttpActionResult GetGarage(int id)
        {
            Garage garage = db.Garages.Find(id);
            if (garage == null)
            {
                return NotFound();
            }

            return Ok(garage);
        }

        // PUT: api/Garages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGarage(int id, Garage garage)
        {
            Trace.WriteLine("Put garage");
            if (!ModelState.IsValid)
            {
                Trace.WriteLine("model state invalid");
                return BadRequest(ModelState);
            }
            Trace.WriteLine(id);
            Trace.WriteLine(garage.GarageID);
            if (id != garage.GarageID)
            {
                Trace.WriteLine("garageID invalid");
                return BadRequest();
            }

            db.Entry(garage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GarageExists(id))
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

        // POST: api/Garages
        [ResponseType(typeof(Garage))]
        public IHttpActionResult PostGarage(Garage garage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Garages.Add(garage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = garage.GarageID }, garage);
        }

        // DELETE: api/Garages/5
        [ResponseType(typeof(Garage))]
        public IHttpActionResult DeleteGarage(int id)
        {
            Garage garage = db.Garages.Find(id);
            if (garage == null)
            {
                return NotFound();
            }

            db.Garages.Remove(garage);
            db.SaveChanges();

            return Ok(garage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GarageExists(int id)
        {
            return db.Garages.Count(e => e.GarageID == id) > 0;
        }
    }
}