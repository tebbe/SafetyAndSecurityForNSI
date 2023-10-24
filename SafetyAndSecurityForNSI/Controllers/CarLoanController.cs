using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SafetyAndSecurityForNSI.Models;
using System.IO;
using SafetyAndSecurityForNSI.ViewModel;

namespace SafetyAndSecurityForNSI.Controllers
{
    [Authorize]
    public class CarLoanController : Controller
    {
        private SSFORNSIDBFinalEntities db = new SSFORNSIDBFinalEntities();

        // GET: CarLoan
        public ActionResult Index()
        {
            return View(db.CarLoanModels.ToList());
        }

        // GET: CarLoan/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarLoanModel carLoanModel = db.CarLoanModels.Find(id);
            if (carLoanModel == null)
            {
                return HttpNotFound();
            }
            return View(carLoanModel);
        }

        // GET: CarLoan/Create
        public ActionResult Create()
        {
            ViewBag.GenderList = db.SexModels.ToList();
            return View();
        }

        public JsonResult SaveCarEntry( CarLoanViewModel vModel, IEnumerable<HttpPostedFileBase> files)
        {
            string personImg = "";
            string bankStateteImg = "";
            int operationStatus = -1;
            string ImgPaths = Server.MapPath("~/Image/Car/");
            if (!Directory.Exists(ImgPaths))
            {
                Directory.CreateDirectory(ImgPaths);
            }
            CarLoanModel model = new CarLoanModel();
          
            if (files != null)
            {
                int i = 0;
                foreach (var file in files)
                {
                    Random generator = new Random();
                    string random = generator.Next(0, 900000).ToString("D6");
                    string s = file.FileName;
                    int idx = s.LastIndexOf('.');
                    string fileName = s.Substring(0, idx);
                    string extension = s.Substring(idx);

                    if (i == 0 && fileName != null)
                    {
                        personImg = "Car" + model.NID + fileName + random + extension;
                        model.PersonImage = personImg;
                        ImgPaths = Path.Combine(Server.MapPath("~/Image/Car/PersonImg/"), personImg);
                        file.SaveAs(ImgPaths);
                    }
                    if (i == 1 && fileName != null)
                    {
                        bankStateteImg = "Car" + model.NID + fileName + random + extension;
                        model.BankStatement = bankStateteImg;
                        ImgPaths = Path.Combine(Server.MapPath("~/Image/Car/BankStateImg/"), bankStateteImg);
                        file.SaveAs(ImgPaths);
                    }
                    i++;
                }

            }
            model.CarLoanId = Guid.NewGuid();
            model.CitizenName = vModel.CitizenName;
            model.NID = vModel.NID;
            model.BirthId = vModel.BirthId;
            model.FatherName = vModel.FatherName;
            model.MotherName = vModel.MotherName;
            model.Age = (int)vModel.Age;
            model.Division = vModel.Division;
            model.Contact = vModel.Contact;
            model.PresenrAddress = vModel.PresenrAddress;
            model.RelativeContact = vModel.RelativeContact;
            model.PassportNo = vModel.PassportNo;
            model.Email = vModel.Email;
            model.SSINumber = vModel.SSINumber;
            model.LoanAmmount = vModel.LoanAmmount;
            model.JobStatus = vModel.JobStatus;
            model.MonthlyIncome = vModel.MonthlyIncome;
            model.YearlyIncome = vModel.YearlyIncome;
            model.WorkingAddress = vModel.WorkingAddress;
            model.PersonDetails = vModel.PersonDetails;
            model.FamilyMembers = vModel.FamilyMembers;
            model.Gender = vModel.Gender;
            model.CreatedBy = vModel.CreatedBy;
            model.City = vModel.City;
            try
            {
                db.CarLoanModels.Add(model);
                db.SaveChanges();
                operationStatus = 1;
            }
            catch (Exception)
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
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

        }
        // GET: CarLoan/Edit/5
        public ActionResult Edit(Guid? id)
        {

            CarLoanViewModel vModel = new CarLoanViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarLoanModel model = db.CarLoanModels.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            else
            {
                vModel = new CarLoanViewModel
                {
                    CarLoanId = model.CarLoanId,
                    CitizenName = model.CitizenName,
                    NID = model.NID,
                    BirthId = model.BirthId,
                    FatherName = model.FatherName,
                    MotherName = model.MotherName,
                    Age = model.Age,
                    Division = model.Division,
                    Contact = model.Contact,
                    PresenrAddress = model.PresenrAddress,
                    RelativeContact = model.RelativeContact,
                    PassportNo = model.PassportNo,
                    Email = model.Email,
                    SSINumber = model.SSINumber,
                    LoanAmmount = model.LoanAmmount,
                    JobStatus = model.JobStatus,
                    MonthlyIncome = model.MonthlyIncome,
                    YearlyIncome = model.YearlyIncome,
                    WorkingAddress = model.WorkingAddress,
                    PersonDetails = model.PersonDetails,
                    FamilyMembers = model.FamilyMembers,
                    BankStatement = model.BankStatement,
                    PersonImage=model.PersonImage,
                    Gender = model.Gender,
                    CreatedBy=model.CreatedBy,
                    UpdatedBy=model.UpdatedBy,
                    City = model.City
                };
            }
            ViewBag.GenderList = db.SexModels.ToList();
            return View(vModel);
        }

        public JsonResult UpdateCarEntry(CarLoanViewModel vModel, IEnumerable<HttpPostedFileBase> files)
        {
            string personImg = "";
            string bankStateteImg = "";
            string ImgPaths = "";
            int operationStatus = -1;
            CarLoanModel model = db.CarLoanModels.Find(vModel.CarLoanId);
            if (model.CarLoanId != null) {
                string personImgPaths = Path.Combine(Server.MapPath("~/Image/Car/PersonImg/"), model.PersonImage);
                string namkStateImgPaths = Path.Combine(Server.MapPath("~/Image/Car/BankStateImg/"), model.BankStatement);

                if (model.PersonImage != null && model.BankStatement!=null)
                {
                    if (System.IO.File.Exists(personImgPaths))
                    {
                        System.IO.File.Delete(personImgPaths);
                    }
                    if (System.IO.File.Exists(namkStateImgPaths))
                    {
                        System.IO.File.Delete(namkStateImgPaths);
                    }
                }
                
                if (files != null)
                {                  
                    int i = 0;
                    foreach (var file in files)
                    {
                        Random generator = new Random();
                        string random = generator.Next(0, 900000).ToString("D6");
                        string s = file.FileName;
                        int idx = s.LastIndexOf('.');
                        string fileName = s.Substring(0, idx);
                        string extension = s.Substring(idx);

                        if (i == 0 && fileName != null)
                        {
                            personImg = "Car" + model.NID + fileName + random + extension;
                            model.PersonImage = personImg;
                            ImgPaths = Path.Combine(Server.MapPath("~/Image/Car/PersonImg"), personImg);
                            file.SaveAs(ImgPaths);
                        }
                        if (i == 1 && fileName != null)
                        {
                            bankStateteImg = "Car" + model.NID + fileName + random + extension;
                            model.BankStatement = bankStateteImg;
                            ImgPaths = Path.Combine(Server.MapPath("~/Image/Car/PersonImg"), bankStateteImg);
                            file.SaveAs(ImgPaths);
                        }
                        i++;
                    }

                }

                model.CarLoanId = vModel.CarLoanId;
                model.CitizenName = vModel.CitizenName;
                model.NID = vModel.NID;
                model.BirthId = vModel.BirthId;
                model.FatherName = vModel.FatherName;
                model.MotherName = vModel.MotherName;
                model.Age = (int)vModel.Age;
                model.Division = vModel.Division;
                model.Contact = vModel.Contact;
                model.PresenrAddress = vModel.PresenrAddress;
                model.RelativeContact = vModel.RelativeContact;
                model.PassportNo = vModel.PassportNo;
                model.Email = vModel.Email;
                model.SSINumber = vModel.SSINumber;
                model.LoanAmmount = vModel.LoanAmmount;
                model.JobStatus = vModel.JobStatus;
                model.MonthlyIncome = vModel.MonthlyIncome;
                model.YearlyIncome = vModel.YearlyIncome;
                model.WorkingAddress = vModel.WorkingAddress;
                model.PersonDetails = vModel.PersonDetails;
                model.FamilyMembers = vModel.FamilyMembers;
                model.Gender = vModel.Gender;
                model.City = vModel.City;
                model.UpdatedBy = vModel.UpdatedBy;
                db.Entry(model).State = EntityState.Modified;
            }
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

            if (operationStatus == 1)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

        }
        // GET: CarLoan/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarLoanModel carLoanModel = db.CarLoanModels.Find(id);
            if (carLoanModel == null)
            {
                return HttpNotFound();
            }
            return View(carLoanModel);
        }

        // POST: CarLoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CarLoanModel carLoanModel = db.CarLoanModels.Find(id);
            if (carLoanModel.CarLoanId != null)
            {
                if (carLoanModel.PersonImage != null)
                {
                    string personImgPaths = Path.Combine(Server.MapPath("~/Image/Car/PersonImg/"), carLoanModel.PersonImage);
                    
                    if (System.IO.File.Exists(personImgPaths))
                    {
                        System.IO.File.Delete(personImgPaths);
                    }
                }
                if (carLoanModel.BankStatement != null)
                {
                    string bankStatImgpaths = Path.Combine(Server.MapPath("~/Image/Car/BankStateImg/"), carLoanModel.BankStatement);

                    if (System.IO.File.Exists(bankStatImgpaths))
                    {
                        System.IO.File.Delete(bankStatImgpaths);
                    }
                }
                db.CarLoanModels.Remove(carLoanModel);
                db.SaveChanges();
            }
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
