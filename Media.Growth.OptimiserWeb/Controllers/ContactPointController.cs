using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Media.Growth.OptimiserWeb.Models;

namespace Media.Growth.OptimiserWeb.Controllers
{
    public class ContactPointController : Controller
    {
        private mPhasingEntities db = new mPhasingEntities();

        // GET: ContactPoint
        public ActionResult Index()
        {
            var contactPoints = db.ContactPoints.Include(c => c.Channel);
            return View(contactPoints.ToList());
        }

        // GET: ContactPoint/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactPoint contactPoint = db.ContactPoints.Find(id);
            if (contactPoint == null)
            {
                return HttpNotFound();
            }
            return View(contactPoint);
        }

        // GET: ContactPoint/Create
        public ActionResult Create()
        {
            ViewBag.fkChannelid = new SelectList(db.Channels, "ChannelId", "ChannelName");
            return View();
        }

        // POST: ContactPoint/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactPointId,ContactPointName,fkChannelid")] ContactPoint contactPoint)
        {
            if (ModelState.IsValid)
            {
                db.ContactPoints.Add(contactPoint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fkChannelid = new SelectList(db.Channels, "ChannelId", "ChannelName", contactPoint.ChannelName);
            return View(contactPoint);
        }

        // GET: ContactPoint/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactPoint contactPoint = db.ContactPoints.Find(id);
            if (contactPoint == null)
            {
                return HttpNotFound();
            }
            ViewBag.fkChannelid = new SelectList(db.Channels, "ChannelId", "ChannelName", contactPoint.ChannelName);
            return View(contactPoint);
        }

        // POST: ContactPoint/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactPointId,ContactPointName,fkChannelid")] ContactPoint contactPoint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactPoint).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fkChannelid = new SelectList(db.Channels, "ChannelId", "ChannelName", contactPoint.ChannelName);
            return View(contactPoint);
        }

        // GET: ContactPoint/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactPoint contactPoint = db.ContactPoints.Find(id);
            if (contactPoint == null)
            {
                return HttpNotFound();
            }
            return View(contactPoint);
        }

        // POST: ContactPoint/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactPoint contactPoint = db.ContactPoints.Find(id);
            db.ContactPoints.Remove(contactPoint);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
