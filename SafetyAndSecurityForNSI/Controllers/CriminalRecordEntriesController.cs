using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SafetyAndSecurityForNSI.Models;
using System.Configuration;

namespace SafetyAndSecurityForNSI.Controllers
{
    public class CriminalRecordEntriesController : Controller
    {
        private SSFORNSIDBFinalEntities db = new SSFORNSIDBFinalEntities();        
        static private int offset = Convert.ToInt32(ConfigurationManager.AppSettings["localTime"]);
        DateTime now = DateTime.UtcNow.AddMinutes(offset);

        // GET: CriminalRecordEntries
        public ActionResult Index()
        {
            return View(db.CriminalRecordEntries.ToList());
        }

        // GET: CriminalRecordEntries/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CriminalRecordEntry criminalRecordEntry = db.CriminalRecordEntries.Find(id);
            if (criminalRecordEntry == null)
            {
                return HttpNotFound();
            }
            return View(criminalRecordEntry);
        }

        // GET: CriminalRecordEntries/Create
        public ActionResult Create()
        {
            ViewBag.GenderList = db.SexModels.ToList();
            ViewBag.CrimeStatus = db.CrimeProgressStatus.ToList();
            return View();
        }

       
        [ValidateInput(false)]
        public JsonResult CrimeReportSave(CriminalRecordEntry criminalRecordEntry)
        {
            int operationStatus = -1;
            long userId = 0;
            HttpCookie cookie = HttpContext.Request.Cookies.Get("CookieAdminInfo");
            if (cookie != null)
            {
                userId = Convert.ToInt64(Request.Cookies["CookieAdminInfo"].Values["nid"]);
            }
            if (ModelState.IsValid)
            {
                criminalRecordEntry.CrimeId = Guid.NewGuid();
                criminalRecordEntry.CreatedBy = userId;
                criminalRecordEntry.CreatedDate = now;
                db.CriminalRecordEntries.Add(criminalRecordEntry);
                db.SaveChanges();
                operationStatus = 1;
            }
            if (operationStatus == 1)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
        }

        // GET: CriminalRecordEntries/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CriminalRecordEntry criminalRecordEntry = db.CriminalRecordEntries.Find(id);
            ViewBag.GenderList = db.SexModels.ToList();
            ViewBag.CrimeStatus = db.CrimeProgressStatus.ToList();
            if (criminalRecordEntry == null)
            {
                return HttpNotFound();
            }
            return View(criminalRecordEntry);
        }

        
        [ValidateInput(false)]
        public JsonResult CrimeReportUpdate(CriminalRecordEntry criminalRecordEntry)
        {
            int operationStatus = -1;
            long userId = 0;
            HttpCookie cookie = HttpContext.Request.Cookies.Get("CookieAdminInfo");
            if (cookie != null)
            {
                userId = Convert.ToInt64(Request.Cookies["CookieAdminInfo"].Values["nid"]);
            }
            if (ModelState.IsValid)
            {
               
                criminalRecordEntry.UpdateBy = userId;
                criminalRecordEntry.UpdateDate = now;
                db.Entry(criminalRecordEntry).State = EntityState.Modified;
                db.SaveChanges();
                operationStatus = 1;
                
            }
            if (operationStatus == 1)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            
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
