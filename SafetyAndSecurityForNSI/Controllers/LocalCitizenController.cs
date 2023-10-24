using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using SafetyAndSecurityForNSI.Models;
using SafetyAndSecurityForNSI.ViewModel;

namespace SafetyAndSecurityForNSI.Controllers
{
    [Authorize]
    public class LocalCitizenController : Controller
    {
        private SSFORNSIDBFinalEntities db = new SSFORNSIDBFinalEntities();

        // GET: LocalCitizen
        public ActionResult Index()
        {
            return View(db.LocalCitizenModels.ToList());
        }

        // GET: LocalCitizen/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocalCitizenModel localCitizenModel = db.LocalCitizenModels.Find(id);
            if (localCitizenModel == null)
            {
                return HttpNotFound();
            }
            return View(localCitizenModel);
        }

        // GET: LocalCitizen/Create
        public ActionResult Create()
        {
            return View();
        }
        public JsonResult LocalCitizenSave(LocalCitizenViewModel vModel, IEnumerable<HttpPostedFileBase> files)
        {

            string personImg = "";
            int operationStatus = 0;
            string licenceImg = "";
            
            string ImgPaths = Server.MapPath("~/Image/LocalCitizen/");
           
            LocalCitizenModel model = new LocalCitizenModel
            {
                CitizenId= Guid.NewGuid(),
                CitizenName = vModel.CitizenName,
                MotherName = vModel.MotherName,
                FatherName = vModel.FatherName,
                NID = vModel.NID,
                BirthId = vModel.BirthId,
                Age = vModel.Age,
                Contact = vModel.Contact,
                CreatedBy = vModel.CreatedBy,
                Email = vModel.Email,
                Division = vModel.Division,
                RelativeContact = vModel.RelativeContact,
                Gender = vModel.Gender,
                PresentAddress = vModel.PresentAddress,
                PassportNo = vModel.PassportNo,
                Thana = vModel.Thana,
                Zilla = vModel.Zilla,
                Village = vModel.Village,
                PostOffice = vModel.PostOffice,
                DrivingLicenceNo = vModel.DrivingLicenceNo
            };
            if (files != null)
            {
                if (!Directory.Exists(ImgPaths))
                {
                    Directory.CreateDirectory(ImgPaths);
                }
                int i = 0;
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

                        if (i == 0)
                        {
                            personImg = "CImg" + vModel.NID + fileName + random + extension;
                            model.PersonImg = personImg;
                            ImgPaths = Path.Combine(Server.MapPath("~/Image/LocalCitizen/PersonImg/"), personImg);
                            file.SaveAs(ImgPaths);

                        }
                        if (i == 1)
                        {
                            licenceImg = "DImg" + vModel.NID+ fileName+ random+ extension;
                            model.DLicenceCopy = licenceImg;
                            ImgPaths = Path.Combine(Server.MapPath("~/Image/LocalCitizen/DrivingLicence/"), licenceImg);
                            file.SaveAs(ImgPaths);
                        }
                    }
                    i++;
                }

            }      
            try
            {
                db.LocalCitizenModels.Add(model);
                db.SaveChanges();
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

        // GET: LocalCitizen/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocalCitizenModel model = db.LocalCitizenModels.Find(id);
            LocalCitizenViewModel vModel = new LocalCitizenViewModel
            {
                CitizenId = model.CitizenId,
                CitizenName = model.CitizenName,
                MotherName = model.MotherName,
                FatherName = model.FatherName,
                NID = model.NID,
                BirthId = model.BirthId,
                Age = model.Age,
                Contact = model.Contact,
                CreatedBy = model.CreatedBy,
                Email = model.Email,
                Division = model.Division,
                RelativeContact = model.RelativeContact,
                Gender = model.Gender,
                PresentAddress = model.PresentAddress,
                PassportNo = model.PassportNo,
                Thana = model.Thana,
                Zilla = model.Zilla,
                Village = model.Village,
                PostOffice = model.PostOffice,
                DrivingLicenceNo = model.DrivingLicenceNo,
                DLicenceCopy = model.DLicenceCopy,
                PersonImg = model.PersonImg
            };
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(vModel);
        }

        public JsonResult LocalCitizenUpdate(LocalCitizenViewModel vModel, IEnumerable<HttpPostedFileBase> files)
        {

            string personImg = "";
            int operationStatus = 1;
            string licenceImg = "";
            var model = db.LocalCitizenModels.Find(vModel.CitizenId);
            if (model.CitizenId !=null)
            {
                string personImgPaths = Path.Combine(Server.MapPath("~/Image/LocalCitizen/PersonImg/"),model.PersonImg);
                string licenceImgpaths = Path.Combine(Server.MapPath("~/Image/LocalCitizen/DrivingLicence/"),model.DLicenceCopy);
               
                if (files != null)
                {
                    if (System.IO.File.Exists(personImgPaths))
                    {
                        System.IO.File.Delete(personImgPaths);
                    }
                    if (System.IO.File.Exists(licenceImgpaths))
                    {
                        System.IO.File.Delete(licenceImgpaths);
                    }
                    int i = 0;
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

                            if (i == 0)
                            {
                                personImg = "CImg" + model.NID + fileName + random + extension;
                                model.PersonImg = personImg;
                                string ImgPaths = Path.Combine(Server.MapPath("~/Image/LocalCitizen/PersonImg/"), personImg);
                                file.SaveAs(ImgPaths);

                            }
                            if (i == 1)
                            {
                                licenceImg = "DImg" + model.NID + fileName + random + extension;
                                model.DLicenceCopy = licenceImg;
                                string ImgPaths = Path.Combine(Server.MapPath("~/Image/LocalCitizen/DrivingLicence/"), licenceImg);
                                file.SaveAs(ImgPaths);
                            }
                        }
                        i++;
                    }

                }

                model.CitizenId = vModel.CitizenId;
                model.CitizenName = vModel.CitizenName;
                model.MotherName = vModel.MotherName;
                model.FatherName = vModel.FatherName;
                model.NID = vModel.NID;
                model.BirthId = vModel.BirthId;
                model.Age = vModel.Age;
                model.Contact = vModel.Contact;
                model.CreatedBy = vModel.CreatedBy;
                model.Email = vModel.Email;
                model.Division = vModel.Division;
                model.RelativeContact = vModel.RelativeContact;
                model.Gender = vModel.Gender;
                model.PresentAddress = vModel.PresentAddress;
                model.PassportNo = vModel.PassportNo;
                model.Thana = vModel.Thana;
                model.Zilla = vModel.Zilla;
                model.Village = vModel.Village;
                model.PostOffice = vModel.PostOffice;
                model.DrivingLicenceNo = vModel.DrivingLicenceNo;
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

        // GET: LocalCitizen/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocalCitizenModel model = db.LocalCitizenModels.Find(id);
            
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: LocalCitizen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            LocalCitizenModel model = db.LocalCitizenModels.Find(id);
            if (model.CitizenId !=null)
            {
                string personImgPaths = Path.Combine(Server.MapPath("~/Image/LocalCitizen/PersonImg/"), model.PersonImg);
                string licenceImgpaths = Path.Combine(Server.MapPath("~/Image/LocalCitizen/DrivingLicence/"), model.DLicenceCopy);
                if (model.PersonImg != null)
                {
                    if (System.IO.File.Exists(personImgPaths))
                    {
                        System.IO.File.Delete(personImgPaths);
                    }
                }
                if (model.DLicenceCopy != null)
                {
                    if (System.IO.File.Exists(licenceImgpaths))
                    {
                        System.IO.File.Delete(licenceImgpaths);
                    }
                }
                db.LocalCitizenModels.Remove(model);
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
