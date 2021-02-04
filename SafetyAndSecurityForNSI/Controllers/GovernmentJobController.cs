using SafetyAndSecurityForNSI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SafetyAndSecurityForNSI.Controllers
{
    public class GovernmentJobController : Controller
    {
        private SSFORNSIDBFinalEntities db = new SSFORNSIDBFinalEntities();
        static private int offset = Convert.ToInt32(ConfigurationManager.AppSettings["localTime"]);
        DateTime now = DateTime.UtcNow.AddMinutes(offset);
        // GET: GovernmentJob
        public ActionResult Index(int? page,Guid? JobId)
        {

            int pageNo = 0;
            pageNo = page == null ? 1 : int.Parse(page.ToString());
            var allList = db.GovernmentJobs.ToList();
            if (JobId != null)
            {
                allList = allList.Where(m => m.JobId == JobId).ToList();
            }
            int allCount = db.GovernmentJobs.Count();
            int dataPerPage = 20;
            int dataPerPageEnd = pageNo * dataPerPage;
            int dataPerPageStart = dataPerPageEnd - dataPerPage;
            var data = allList.OrderBy(e => e.JobId).Skip(dataPerPageStart).Take(dataPerPage);
            Pager<GovernmentJob> pager = new Pager<GovernmentJob>(data.AsQueryable(), pageNo, dataPerPage, allCount);


            return View(pager);
        }

        public JsonResult SearchJob(string searText)
        {
            if (!string.IsNullOrEmpty(searText))
            {
                var data = db.GovernmentJobs.Where(m => m.Job_Title.Trim() == searText.Trim() || m.DepartmentName.Trim() == searText.Trim()).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("No Record Found", JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Details(Guid JobId)
        {
            if (JobId != null)
            {
                var data = db.GovernmentJobs.Where(m => m.JobId == JobId).FirstOrDefault();
                return View(data);
            }
           
          return View();
           
        }
        public ActionResult Create()
        {

            return View();
        }
        
        [ValidateInput(true)]
        public JsonResult SaveJobEntry(GovernmentJob job)
        {
            if (ModelState.IsValid)
            {
                GovernmentJob model = new GovernmentJob
                {
                    DepartmentName=job.DepartmentName,
                    Job_Title=job.Job_Title,
                    CreatedDate=now,
                    Body=job.Body

                };
                db.GovernmentJobs.Add(model);
                db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
           
        }

        public ActionResult Edit(Guid JobId)
        {
            if (JobId != null)
            {
                var data = db.GovernmentJobs.Find(JobId);
                return View(data);
            }
            return View();
        }

        [ValidateInput(true)]
        
        public JsonResult UpdateJobEntry(GovernmentJob job)
        {
            if (ModelState.IsValid)
            {
                GovernmentJob model = new GovernmentJob
                {
                    DepartmentName = job.DepartmentName,
                    Job_Title = job.Job_Title,
                    CreatedDate = now,
                    Body = job.Body

                };
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult DeleteJob(Guid JobId)
        {
            if (JobId != null)
            {
                var data = db.GovernmentJobs.Where(m => m.JobId == JobId).FirstOrDefault();
                if (data != null)
                {
                    db.GovernmentJobs.Remove(data);
                    db.SaveChanges();
                }
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

    }
}