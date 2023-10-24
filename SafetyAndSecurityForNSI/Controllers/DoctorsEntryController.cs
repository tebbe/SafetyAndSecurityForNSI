using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SafetyAndSecurityForNSI.ViewModel;
using SafetyAndSecurityForNSI.Models;
using System.IO;

namespace SafetyAndSecurityForNSI.Controllers
{
    [Authorize]
    public class DoctorsEntryController : Controller
    {
        private SSFORNSIDBFinalEntities db = new SSFORNSIDBFinalEntities();

        // GET: DoctorsEntry
        public ActionResult Index()
        {
            return View(db.DoctorsEntryModels.ToList());
        }

        // GET: DoctorsEntry/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorsEntryModel doctorsEntryModel = db.DoctorsEntryModels.Find(id);
            if (doctorsEntryModel == null)
            {
                return HttpNotFound();
            }
            return View(doctorsEntryModel);
        }

        // GET: DoctorsEntry/Create
        public ActionResult Create()
        {
            ViewBag.GenderCategory = db.SexModels.ToList();
            return View();
        }

        public JsonResult SaveDoctorEntry(DoctorsEntryViewModel vModel,IEnumerable<HttpPostedFileBase>files)
        {
            string personImg = "";
            int operationStatus = -1;
            string ImgPaths = Server.MapPath("~/Image/Doctors/");
            if (!Directory.Exists(ImgPaths))
            {
                Directory.CreateDirectory(ImgPaths);
            }
            DoctorsEntryModel model = new DoctorsEntryModel
            {
               DoctorsId = Guid.NewGuid(),
               DoctorName=vModel.DoctorName,
               DegreeOne=vModel.DegreeOne,
               DegreeTwo=vModel.DegreeTwo,
               DegreeThree=vModel.DegreeThree,
               TrainingInstitute=vModel.TrainingInstitute,
               TrainingPeriod=vModel.TrainingPeriod,
               SpecialTraining=vModel.TrainingPeriod,
               BMAMembershipNo=vModel.TrainingPeriod,
               BranceCode=vModel.TrainingPeriod,
               MembershipCategory=vModel.MembershipCategory,
               BcsRegNo=vModel.BcsRegNo,
               NID=vModel.NID,
               Gender=vModel.Gender,
               MaritalStatus=vModel.MaritalStatus
            };
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
                        personImg = "DImg" + model.NID + fileName + random + extension;
                        model.DoctorImage = personImg;
                        ImgPaths = Path.Combine(Server.MapPath("~/Image/Doctors/"), personImg);
                        file.SaveAs(ImgPaths);
                   i++;
                }

            }
            try
            {
                db.DoctorsEntryModels.Add(model);
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

        // GET: DoctorsEntry/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorsEntryModel model = db.DoctorsEntryModels.Find(id);
            ViewBag.GenderCategory = db.SexModels.ToList();
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }


        public JsonResult DoctorInfoUpdate(DoctorsEntryViewModel vModel, IEnumerable<HttpPostedFileBase> files)
        {
            string doctorImg = "";
            int operationStatus = 1;
            string ImagePath = "";
            DoctorsEntryModel model = db.DoctorsEntryModels.Find(vModel.DoctorsId);
            if (model.DoctorsId != null)
            {
                if (model.DoctorImage != null)
                {
                    doctorImg = Path.Combine(Server.MapPath("~/Image/Doctors/"), model.DoctorImage);
                }
                
                if (files != null)
                {
                    if (System.IO.File.Exists(doctorImg))
                    {
                        System.IO.File.Delete(doctorImg);
                    }

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

                                doctorImg = "DocImg" + vModel.BMAMembershipNo + fileName + random + extension;
                                model.DoctorImage = doctorImg;
                                ImagePath = Path.Combine(Server.MapPath("~/Image/Doctors/"), doctorImg);
                                file.SaveAs(ImagePath);                           
                        }
                    }

                }
                model.DoctorsId = vModel.DoctorsId;
                model.DoctorName = vModel.DoctorName;
                model.DegreeOne = vModel.DegreeOne;
                model.DegreeTwo = vModel.DegreeTwo;
                model.DegreeThree = vModel.DegreeThree;
                model.TrainingInstitute = vModel.TrainingInstitute;
                model.TrainingPeriod = vModel.TrainingPeriod;
                model.SpecialTraining = vModel.TrainingPeriod;
                model.BMAMembershipNo = vModel.TrainingPeriod;
                model.BranceCode = vModel.TrainingPeriod;
                model.MembershipCategory = vModel.MembershipCategory;
                model.BcsRegNo = vModel.BcsRegNo;
                model.NID = vModel.NID;
                model.Gender = vModel.Gender;
                model.MaritalStatus = vModel.MaritalStatus;
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


        // GET: DoctorsEntry/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorsEntryModel model = db.DoctorsEntryModels.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: DoctorsEntry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorsEntryModel model = db.DoctorsEntryModels.Find(id);
            if (model.DoctorsId != null)
            {
                if (model.DoctorImage != null)
                {
                    string doctorImg = Path.Combine(Server.MapPath("~/Image/Doctors/"), model.DoctorImage);
                    if (System.IO.File.Exists(doctorImg))
                    {
                        System.IO.File.Delete(doctorImg);
                    }
                }
                db.DoctorsEntryModels.Remove(model);
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
