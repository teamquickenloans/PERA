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
    public class GarageManagersController : ApiController
    {
        private PERAContext db = new PERAContext();

        // GET: api/GarageManagers
        public IQueryable<GarageManager> GetGarageManagers()
        {
            return db.GarageManagers;
        }

        // GET: api/GarageManagers/5
        [ResponseType(typeof(GarageManager))]
        public IHttpActionResult GetGarageManager(int id)
        {
            GarageManager garageManager = db.GarageManagers.Find(id);
            if (garageManager == null)
            {
                return NotFound();
            }

            return Ok(garageManager);
        }

        // PUT: api/GarageManagers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGarageManager(int id, GarageManager garageManager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != garageManager.ID)
            {
                return BadRequest();
            }

            db.Entry(garageManager).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GarageManagerExists(id))
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

        // POST: api/GarageManagers
        [ResponseType(typeof(GarageManager))]
        public IHttpActionResult PostGarageManager(GarageManager garageManager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GarageManagers.Add(garageManager);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = garageManager.ID }, garageManager);
        }

        // DELETE: api/GarageManagers/5
        [ResponseType(typeof(GarageManager))]
        public IHttpActionResult DeleteGarageManager(int id)
        {
            GarageManager garageManager = db.GarageManagers.Find(id);
            if (garageManager == null)
            {
                return NotFound();
            }

            db.GarageManagers.Remove(garageManager);
            db.SaveChanges();

            return Ok(garageManager);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GarageManagerExists(int id)
        {
            return db.GarageManagers.Count(e => e.ID == id) > 0;
        }
    }
}