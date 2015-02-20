﻿using System;
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
    public class CarpoolGroupsController : ApiController
    {
        private PERAContext db = new PERAContext();

        // GET: api/CarpoolGroups
        public IQueryable<CarpoolGroup> GetCarpoolGroups()
        {
            return db.CarpoolGroups;
        }

        // GET: api/CarpoolGroups/5
        [ResponseType(typeof(CarpoolGroup))]
        public IHttpActionResult GetCarpoolGroup(int id)
        {
            CarpoolGroup carpoolGroup = db.CarpoolGroups.Find(id);
            if (carpoolGroup == null)
            {
                return NotFound();
            }

            return Ok(carpoolGroup);
        }

        // PUT: api/CarpoolGroups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCarpoolGroup(int id, CarpoolGroup carpoolGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carpoolGroup.CarpoolGroupID)
            {
                return BadRequest();
            }

            db.Entry(carpoolGroup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarpoolGroupExists(id))
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

        // POST: api/CarpoolGroups
        [ResponseType(typeof(CarpoolGroup))]
        public IHttpActionResult PostCarpoolGroup(CarpoolGroup carpoolGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CarpoolGroups.Add(carpoolGroup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = carpoolGroup.CarpoolGroupID }, carpoolGroup);
        }

        // DELETE: api/CarpoolGroups/5
        [ResponseType(typeof(CarpoolGroup))]
        public IHttpActionResult DeleteCarpoolGroup(int id)
        {
            CarpoolGroup carpoolGroup = db.CarpoolGroups.Find(id);
            if (carpoolGroup == null)
            {
                return NotFound();
            }

            db.CarpoolGroups.Remove(carpoolGroup);
            db.SaveChanges();

            return Ok(carpoolGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarpoolGroupExists(int id)
        {
            return db.CarpoolGroups.Count(e => e.CarpoolGroupID == id) > 0;
        }
    }
}