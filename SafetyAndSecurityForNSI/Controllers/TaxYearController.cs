using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SafetyAndSecurityForNSI.Models;

namespace SafetyAndSecurityForNSI.Controllers
{
    [Authorize]
    public class TaxYearController : Controller
    {
        private SSFORNSIDBFinalEntities db = new SSFORNSIDBFinalEntities();

        // GET: TaxYear
        public ActionResult Index()
        {
            return View(db.TaxYearModels.ToList());
        }

        // GET: TaxYear/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxYearModel taxYearModel = db.TaxYearModels.Find(id);
            if (taxYearModel == null)
            {
                return HttpNotFound();
            }
            return View(taxYearModel);
        }

        // GET: TaxYear/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaxYear/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaxYear,SocialSecurityNumber,NID,ITIN,EIN")] TaxYearModel taxYearModel)
        {
            if (ModelState.IsValid)
            {
                db.TaxYearModels.Add(taxYearModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taxYearModel);
        }

        // GET: TaxYear/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxYearModel taxYearModel = db.TaxYearModels.Find(id);
            if (taxYearModel == null)
            {
                return HttpNotFound();
            }
            return View(taxYearModel);
        }

        // POST: TaxYear/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaxYear,SocialSecurityNumber,NID,ITIN,EIN")] TaxYearModel taxYearModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taxYearModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taxYearModel);
        }

        // GET: TaxYear/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxYearModel taxYearModel = db.TaxYearModels.Find(id);
            if (taxYearModel == null)
            {
                return HttpNotFound();
            }
            return View(taxYearModel);
        }

        // POST: TaxYear/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaxYearModel taxYearModel = db.TaxYearModels.Find(id);
            db.TaxYearModels.Remove(taxYearModel);
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
