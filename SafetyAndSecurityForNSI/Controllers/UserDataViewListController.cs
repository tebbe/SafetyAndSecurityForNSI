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
        public ActionResult LocalCitizenList(int NID)
        {
            return View(db.LocalCitizenModels.Where(m=>m.NID==NID).ToList());
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
        public ActionResult DoctorList(int NID)
        {
            return View(db.DoctorsEntryModels.Where(m => m.NID == NID).ToList());
        }
        public ActionResult DoctorDetails(Guid id)
        {
            return View(db.DoctorsEntryModels.Where(m => m.DoctorsId == id).FirstOrDefault());
        }
        public ActionResult HomeLoanList(int NID)
        {
            return View(db.HomeLoanModels.Where(m => m.NID == NID).ToList());
        }
        public ActionResult HomeLoanDetils(Guid id)
        {
            return View(db.HomeLoanModels.Where(m => m.HomeLoanId == id).FirstOrDefault());
        }
        public ActionResult CarLoanList(int NID)
        {
            return View(db.CarLoanModels.Where(m => m.NID == NID).ToList());
        }
        public ActionResult CarLoanDetails(Guid id)
        {
            return View(db.CarLoanModels.Where(m => m.CarLoanId == id).FirstOrDefault());
        }
        public ActionResult SSIList(int NID)
        {
            return View(db.SSIModels.Where(m => m.NID == NID).ToList());
        }
        public ActionResult SSIDetails(Guid id)
        {
            return View(db.SSIModels.Where(m => m.SSIId == id).FirstOrDefault());
        }
        public ActionResult TaxList(int NID)
        {
            return View(db.TaxEntryModels.Where(m => m.NID == NID).ToList());
        }
        public ActionResult TaxDetails(Guid id)
        {
            return View(db.TaxEntryModels.Where(m => m.TaxId == id).FirstOrDefault());
        }
        public ActionResult TaxYearList(int NID)
        {
            return View(db.TaxEntryModels.Where(m => m.NID == NID).ToList());
        }
        public ActionResult TaxYearDetails(Guid id)
        {
            return View(db.TaxEntryModels.Where(m => m.TaxId == id).FirstOrDefault());
        }
        public ActionResult PoliceClearanceList(int NID)
        {
            return View(db.PoliceClearanceModels.Where(m => m.NID == NID).ToList());
        }
        public ActionResult PoliceClearanceDetils(Guid id)
        {
            return View(db.PoliceClearanceModels.Where(m => m.ThanaId == id).FirstOrDefault());
        }

    }
}