using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Description;
using PERA.DAL;
using PERA.Models;

namespace PERA.Controllers
{
    public class TeamMemberController : Controller
    {
        //private ParkingContext db = new ParkingContext();

        // GET: TeamMember
        public ActionResult Index()
        {
            var teamMembers = db.TeamMembers.Include(t => t.Garage);
            return View(teamMembers.ToList());
        }//test


        // GET: TeamMember/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMember teamMember = db.TeamMembers.Find(id);
            if (teamMember == null)
            {
                return HttpNotFound();
            }
            return View(teamMember);
        }

        // GET: TeamMember/Create
        public ActionResult Create()
        {
            ViewBag.GarageID = new SelectList(db.Garages, "GarageID", "Name");
            return View();
        }

        // POST: TeamMember/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BadgeID,FirstName,LastName,CommonID,GarageID,EmploymentStatus,TerminationDate,ParkingStatus,EnrollmentDate")] TeamMember teamMember)
        {
            if (ModelState.IsValid)
            {
                db.TeamMembers.Add(teamMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GarageID = new SelectList(db.Garages, "GarageID", "Name", teamMember.GarageID);
            return View(teamMember);
        }

        // GET: TeamMember/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMember teamMember = db.TeamMembers.Find(id);
            if (teamMember == null)
            {
                return HttpNotFound();
            }
            ViewBag.GarageID = new SelectList(db.Garages, "GarageID", "Name", teamMember.GarageID);
            return View(teamMember);
        }

        // POST: TeamMember/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BadgeID,FirstName,LastName,CommonID,GarageID,EmploymentStatus,TerminationDate,ParkingStatus,EnrollmentDate")] TeamMember teamMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GarageID = new SelectList(db.Garages, "GarageID", "Name", teamMember.GarageID);
            return View(teamMember);
        }

        // GET: TeamMember/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMember teamMember = db.TeamMembers.Find(id);
            if (teamMember == null)
            {
                return HttpNotFound();
            }
            return View(teamMember);
        }

        // POST: TeamMember/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamMember teamMember = db.TeamMembers.Find(id);
            db.TeamMembers.Remove(teamMember);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
