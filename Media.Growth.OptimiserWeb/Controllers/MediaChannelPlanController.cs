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
    public class MediaChannelPlanController : Controller
    {
        private mPhasingEntities db = new mPhasingEntities();

        // GET: MediaChannelPlan
        public ActionResult Index()
        {
            var mediaChannelPlans = db.MediaChannelPlans.Include(m => m.ContactPoint);
            return View(mediaChannelPlans.ToList());
        }

        // GET: MediaChannelPlan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediaChannelPlan mediaChannelPlan = db.MediaChannelPlans.Find(id);
            if (mediaChannelPlan == null)
            {
                return HttpNotFound();
            }
            return View(mediaChannelPlan);
        }

        // GET: MediaChannelPlan/Create
        public ActionResult Create()
        {
            ViewBag.Media = new SelectList(db.ContactPoints, "ContactPointId", "ContactPointName");
            return View();
        }

        // POST: MediaChannelPlan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMediaChannel,Period,Media,SalesBase,CarryOver,UpliftMax,SpendHalf,SpendOpt")] MediaChannelPlan mediaChannelPlan)
        {
            if (ModelState.IsValid)
            {
                db.MediaChannelPlans.Add(mediaChannelPlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Media = new SelectList(db.ContactPoints, "ContactPointId", "ContactPointName", mediaChannelPlan.Media);
            return View(mediaChannelPlan);
        }

        // GET: MediaChannelPlan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediaChannelPlan mediaChannelPlan = db.MediaChannelPlans.Find(id);
            if (mediaChannelPlan == null)
            {
                return HttpNotFound();
            }
            ViewBag.Media = new SelectList(db.ContactPoints, "ContactPointId", "ContactPointName", mediaChannelPlan.Media);
            return View(mediaChannelPlan);
        }

        // POST: MediaChannelPlan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMediaChannel,Period,Media,SalesBase,CarryOver,UpliftMax,SpendHalf,SpendOpt")] MediaChannelPlan mediaChannelPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mediaChannelPlan).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Media = new SelectList(db.ContactPoints, "ContactPointId", "ContactPointName", mediaChannelPlan.Media);
            return View(mediaChannelPlan);
        }

        // GET: MediaChannelPlan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediaChannelPlan mediaChannelPlan = db.MediaChannelPlans.Find(id);
            if (mediaChannelPlan == null)
            {
                return HttpNotFound();
            }
            return View(mediaChannelPlan);
        }

        // POST: MediaChannelPlan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MediaChannelPlan mediaChannelPlan = db.MediaChannelPlans.Find(id);
            db.MediaChannelPlans.Remove(mediaChannelPlan);
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
