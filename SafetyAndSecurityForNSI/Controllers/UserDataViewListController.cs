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
        public ActionResult LocalCitizenList(int NID,string Passport)
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
        public ActionResult DoctorList(int NID)
        {
            return View(db.DoctorsEntryModels.Where(m => m.NID == NID).ToList());
        }
        public ActionResult DoctorDetails(Guid id)
        {
            return View(db.DoctorsEntryModels.Where(m => m.DoctorsId == id).FirstOrDefault());
        }
        public ActionResult HomeLoanList(int NID, string Passport)
        {
            return View(db.HomeLoanModels.Where(m => m.NID == NID || m.PassportNo == Passport).ToList());
        }
        public ActionResult HomeLoanDetils(Guid id)
        {
            return View(db.HomeLoanModels.Where(m => m.HomeLoanId == id).FirstOrDefault());
        }
        public ActionResult CarLoanList(int NID, string Passport)
        {
            return View(db.CarLoanModels.Where(m => m.NID == NID || m.PassportNo == Passport).ToList());
        }
        public ActionResult CarLoanDetails(Guid id)
        {
            return View(db.CarLoanModels.Where(m => m.CarLoanId == id).FirstOrDefault());
        }
        public ActionResult SSIList(int NID, string Passport)
        {
            return View(db.SSIModels.Where(m => m.NID == NID || m.PassportNo == Passport).ToList());
        }
        public ActionResult SSIDetails(Guid id)
        {
            return View(db.SSIModels.Where(m => m.SSIId == id).FirstOrDefault());
        }
        public ActionResult TaxList(int NID, string Passport)
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

    }
}