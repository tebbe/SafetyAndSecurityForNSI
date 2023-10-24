using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SafetyAndSecurityForNSI.Models;
using SafetyAndSecurityForNSI.ViewModel;
using System.IO;

namespace SafetyAndSecurityForNSI.Controllers
{
    [Authorize]
    public class HomeLoanController : Controller
    {
        private SSFORNSIDBFinalEntities db = new SSFORNSIDBFinalEntities();

        // GET: HomeLoan
        public ActionResult Index()
        {
            return View(db.HomeLoanModels.ToList());
        }

        // GET: HomeLoan/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeLoanModel homeLoanModel = db.HomeLoanModels.Find(id);
            if (homeLoanModel == null)
            {
                return HttpNotFound();
            }
            return View(homeLoanModel);
        }

        // GET: HomeLoan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeLoan/Create

        public JsonResult HomeLoanEntrySave(HomeLoanViewModel vModel,IEnumerable<HttpPostedFileBase>files)
        {


            string personImg = "";
            int operationStatus = 1;
            string ImagePath = Server.MapPath("~/Image/Home/");
            if (!Directory.Exists(ImagePath))
            {
                Directory.CreateDirectory(ImagePath);
            }
            HomeLoanModel mode = new HomeLoanModel
            {
                HomeLoanId=Guid.NewGuid(),
                CitizenName = vModel.CitizenName,
                NID = vModel.NID,
                BirthId = vModel.BirthId,
                FatherName = vModel.FatherName,
                MotherName = vModel.MotherName,
                Age = vModel.Age,
                PresenrAddress = vModel.PresenrAddress,
                ParmanentAddress = vModel.ParmanentAddress,
                Contact = vModel.Contact,
                RelativeContact = vModel.RelativeContact,
                PassportNo = vModel.PassportNo,
                Email = vModel.Email,
                SSINumber = vModel.SSINumber,
                LoanAmmount = vModel.LoanAmmount,
                MonthlyIncome = vModel.MonthlyIncome,
                JobStatus = vModel.JobStatus,
                YearlyIncome = vModel.YearlyIncome,
                WorkingAddress = vModel.WorkingAddress,
                PersonDetails = vModel.PersonDetails,
                FamilyMembers = vModel.FamilyMembers,
                Gender = vModel.Gender,
            };
            if (files != null)
            {
                foreach(var file in files)
                {
                    if (file != null)
                    {
                        Random generator = new Random();
                        string random = generator.Next(0, 900000).ToString("D6");
                        string s = file.FileName;
                        int idx = s.LastIndexOf('.');
                        string fileName = s.Substring(0, idx);
                        string extension = s.Substring(idx);

                        personImg = "HL" + vModel.NID + fileName + extension;
                        mode.PersonImage = personImg;
                        ImagePath = Path.Combine(Server.MapPath("~/Image/Home/"), personImg);
                        file.SaveAs(ImagePath);

                    }
                }
            }
            try {
                db.HomeLoanModels.Add(mode);
                db.SaveChanges();
                operationStatus = 1;
            }
            catch(Exception)
            {
                operationStatus = -1;
                throw;
            }
            if (operationStatus == 1)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error",JsonRequestBehavior.AllowGet);
            }
        }

        // GET: HomeLoan/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeLoanModel homeLoanModel = db.HomeLoanModels.Find(id);
            if (homeLoanModel == null)
            {
                return HttpNotFound();
            }
            return View(homeLoanModel);
        }

        // POST: HomeLoan/Edit/5
        public JsonResult HomeLoanUpdate(HomeLoanViewModel vModel, IEnumerable<HttpPostedFileBase> files)
        {
            string personImg = "";
            int operationStatus = 1;
            string ImagePath = "";
            HomeLoanModel model = db.HomeLoanModels.Find(vModel.HomeLoanId);
            if (model.HomeLoanId != null)
            {
                if (model.PersonImage != null) {
                    personImg = Path.Combine(Server.MapPath("~/Image/Home/"), model.PersonImage);

                    if (System.IO.File.Exists(personImg))
                    {
                        System.IO.File.Delete(personImg);
                    }
                }
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file != null)
                        {
                            Random generator = new Random();
                            string random = generator.Next(0, 900000).ToString("D6");
                            string s = file.FileName;
                            int idx = s.LastIndexOf('.');
                            string fileName = s.Substring(0, idx);
                            string extension = s.Substring(idx);

                            personImg = "FCImg" + model.PassportNo + fileName + random + extension;
                            model.PersonImage = personImg;
                            ImagePath = Path.Combine(Server.MapPath("~/Image/Home/"), personImg);
                            file.SaveAs(ImagePath);

                        }
                        
                    }

                }

                model.HomeLoanId = vModel.HomeLoanId;
                model.CitizenName = vModel.CitizenName;
                model.NID = vModel.NID;
                model.BirthId = vModel.BirthId;
                model.FatherName = vModel.FatherName;
                model.MotherName = vModel.MotherName;
                model.Age = vModel.Age;
                model.PresenrAddress = vModel.PresenrAddress;
                model.ParmanentAddress = vModel.ParmanentAddress;
                model.Contact = vModel.Contact;
                model.RelativeContact = vModel.RelativeContact;
                model.PassportNo = vModel.PassportNo;
                model.Email = vModel.Email;
                model.SSINumber = vModel.SSINumber;
                model.LoanAmmount = vModel.LoanAmmount;
                model.MonthlyIncome = vModel.MonthlyIncome;
                model.JobStatus = vModel.JobStatus;
                model.YearlyIncome = vModel.YearlyIncome;
                model.WorkingAddress = vModel.WorkingAddress;
                model.PersonDetails = vModel.PersonDetails;
                model.FamilyMembers = vModel.FamilyMembers;
                model.Gender = vModel.Gender;
               
                db.Entry(model).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    operationStatus = 1;
                }
                catch (Exception)
                {
                    operationStatus = -1;
                    throw;
                }
            }
            if (operationStatus == 1)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }


        // GET: HomeLoan/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeLoanModel homeLoanModel = db.HomeLoanModels.Find(id);
            if (homeLoanModel == null)
            {
                return HttpNotFound();
            }
            return View(homeLoanModel);
        }

        // POST: HomeLoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            HomeLoanModel model = db.HomeLoanModels.Find(id);
            if(model.PersonImage != null) { 
                string personImg = Path.Combine(Server.MapPath("~/Image/Home/"), model.PersonImage);

                if (System.IO.File.Exists(personImg))
                {
                    System.IO.File.Delete(personImg);
                }
            }
            db.HomeLoanModels.Remove(model);
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
