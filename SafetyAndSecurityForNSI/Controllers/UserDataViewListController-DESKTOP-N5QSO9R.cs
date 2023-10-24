using SafetyAndSecurityForNSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SafetyAndSecurityForNSI.Controllers
{
    [Authorize]
    public class UserDataViewListController : Controller
    {
        private SSFORNSIDBFinalEntities db = new SSFORNSIDBFinalEntities();
        // GET: UserDataViewList
        public ActionResult LocalCitizenList(long NID,string Passport)
        {
            return View(db.LocalCitizenModels.Where(m=>m.NID==NID || m.PassportNo == Passport).ToList());
        }
        public ActionResult LocalCitizenView(Guid id)
        {            
            return View(db.LocalCitizenModels.Where(m => m.CitizenId == id).FirstOrDefault());
        }
        public ActionResult ForeignCitizenList(string passportNo)
        {
            return View(db.ForeignCitizenModels.Where(m => m.PassportNo == passportNo).ToList());
        }
        public ActionResult ForeignCitizenDetils(Guid id)
        {
            return View(db.ForeignCitizenModels.Where(m => m.ForignCitizenId == id).FirstOrDefault());
        }
        public ActionResult DoctorList(long NID)
        {
            return View(db.DoctorsEntryModels.Where(m => m.NID == NID).ToList());
        }
        public ActionResult DoctorDetails(Guid id)
        {
            return View(db.DoctorsEntryModels.Where(m => m.DoctorsId == id).FirstOrDefault());
        }
        public ActionResult HomeLoanList(long NID, string Passport)
        {
            return View(db.HomeLoanModels.Where(m => m.NID == NID || m.PassportNo == Passport).ToList());
        }
        public ActionResult HomeLoanDetils(Guid id)
        {
            return View(db.HomeLoanModels.Where(m => m.HomeLoanId == id).FirstOrDefault());
        }
        public ActionResult CarLoanList(long NID, string Passport)
        {
            return View(db.CarLoanModels.Where(m => m.NID == NID || m.PassportNo == Passport).ToList());
        }
        public ActionResult CarLoanDetails(Guid id)
        {
            return View(db.CarLoanModels.Where(m => m.CarLoanId == id).FirstOrDefault());
        }
        public ActionResult SSIList(long NID, string Passport)
        {
            return View(db.SSIModels.Where(m => m.NID == NID || m.PassportNo == Passport).ToList());
        }
        public ActionResult SSIDetails(Guid id)
        {
            return View(db.SSIModels.Where(m => m.SSIId == id).FirstOrDefault());
        }
        public ActionResult TaxList(long NID, string Passport)
        {
            return View(db.TaxEntryModels.Where(m => m.NID == NID || m.PassportNo == Passport).ToList());
        }
        public ActionResult TaxDetails(Guid id)
        {
            return View(db.TaxEntryModels.Where(m => m.TaxId == id).FirstOrDefault());
        }
        public ActionResult TaxYearList(long NID)
        {
            return View(db.TaxYearModels.Where(m => m.NID == NID).ToList());
        }
        public ActionResult TaxYearDetails(int id)
        {
            return View(db.TaxYearModels.Where(m => m.TaxYearId == id).FirstOrDefault());
        }
        public ActionResult PoliceClearanceList(int NID, string Passport)
        {
            return View(db.PoliceClearanceModels.Where(m => m.NID == NID || m.PassportNo==Passport).ToList());
        }
        public ActionResult PoliceClearanceDetils(Guid id)
        {
            return View(db.PoliceClearanceModels.Where(m => m.ThanaId == id).FirstOrDefault());
        }

        public ActionResult JobList(int? page, Guid? JobId)
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
                var data = db.GovernmentJobs.Where(m => m.JobTitle.ToLower().Contains(searText.ToLower()) || m.DepartmentName.ToLower().Contains(searText.ToLower())).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("No Record Found", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult JobDetailsView(Guid JobId)
        {
            if (JobId != null)
            {
                var data = db.GovernmentJobs.Where(m => m.JobId == JobId).FirstOrDefault();
                return View(data);
            }
            return View();
        }
    }
}