using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SafetyAndSecurityForNSI.Models;
using SafetyAndSecurityForNSI.ModelView;
using System.Configuration;


namespace SafetyAndSecurityForNSI.Controllers
{
    [Authorize]
    public class ApplicationsController : Controller
    {
        private SSFORNSIDBFinalEntities db = new SSFORNSIDBFinalEntities();
        static private int offset = Convert.ToInt32(ConfigurationManager.AppSettings["localTime"]);
        DateTime now = DateTime.UtcNow.AddMinutes(offset);
        // GET: Applications
        public ActionResult Index(int NID,int?page)
        {
            int pageNo = 0;
            pageNo = page == null ? 1 : int.Parse(page.ToString());
            
            var allData = db.Applications.Where(m => m.NID == NID).ToList();
            int allCount = db.Applications.Where(m => m.NID == NID).Count();
            int dataPerPage = 20;
            int dataPerPageEnd = pageNo * dataPerPage;
            int dataPerPageStart = dataPerPageEnd - dataPerPage;
            var allList = allData.OrderBy(e => e.ApplicationId).Skip(dataPerPageStart).Take(dataPerPage);
            Pager<Application> pager = new Pager<Application>(allList.AsQueryable(), pageNo, dataPerPage, allCount);
            return View();
        }

        // GET: Applications/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationCategory = db.ApplicationCategories.ToList();
            return View();
        }

        // POST: Applications/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationId,NID,BirthId,Email,Contact,ApplicationType,Name,Details")] Application application)
        {
            if (ModelState.IsValid)
            {
                application.ApplicationId = Guid.NewGuid();
                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(application);
        }

        // GET: Applications/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            ViewBag.ApplicationCategory = db.ApplicationCategories.ToList();
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationId,NID,BirthId,Email,Contact,ApplicationType,Name,Details")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(application);
        }

        // GET: Applications/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Category Add and Update actions bellow
        public ActionResult AddApplicationCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddApplicationCategory(ApplicationCategoryViewModel vModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationCategory model = new ApplicationCategory
                {
                    ApplicationCategoryId=Guid.NewGuid(),
                    ApplicationName=vModel.ApplicationName

                };

                db.ApplicationCategories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vModel);
        }
        // GET: Applications/Edit/5
        public ActionResult EditCategory(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationCategory appCategory = db.ApplicationCategories.Find(id);
           
            if (appCategory == null)
            {
                return HttpNotFound();
            }
            return View(appCategory);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(ApplicationCategoryViewModel appCategory)
        {
            if (ModelState.IsValid) { 

                ApplicationCategory mode = new ApplicationCategory
                {
                    ApplicationCategoryId=appCategory.ApplicationCategoryId,
                    ApplicationName=appCategory.ApplicationName
                };
                db.Entry(mode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appCategory);
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
