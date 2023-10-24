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
    public class ForeignCitizenController : Controller
    {
        private SSFORNSIDBFinalEntities db = new SSFORNSIDBFinalEntities();

        // GET: ForeignCitizen
        public ActionResult Index()
        {
            return View(db.ForeignCitizenModels.ToList());
        }

        // GET: ForeignCitizen/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForeignCitizenModel foreignCitizenModel = db.ForeignCitizenModels.Find(id);
            if (foreignCitizenModel == null)
            {
                return HttpNotFound();
            }
            return View(foreignCitizenModel);
        }

        // GET: ForeignCitizen/Create
        public ActionResult Create()
        {
            ViewBag.CountryList = db.Countries.ToList();
            return View();
        }

        // POST: ForeignCitizen/Create
       
       
        public JsonResult ForeignCtizenSave(ForeignCitizenViewModel Vmodel,IEnumerable<HttpPostedFileBase>files)
        {
            string personImg="";
            string passportImg="";
            string policeClearance="";
            string drivingLicence="";
            int operationStatus = 1;
            string ImagePath = Server.MapPath("~/Image/ForeignCitizen/");
            if (!Directory.Exists(ImagePath))
            {
                Directory.CreateDirectory(ImagePath);
            }
            ForeignCitizenModel model = new ForeignCitizenModel
            {
                ForignCitizenId = Guid.NewGuid(),
                CitizenName = Vmodel.CitizenName,
                FatherName = Vmodel.FatherName,
                MotherName = Vmodel.MotherName,
                PassportNo = Vmodel.PassportNo,
                CountryName = Vmodel.CountryName,
                Age = Vmodel.Age,
                State=Vmodel.State,
                Contact=Vmodel.Contact,
                PresenrAddress=Vmodel.PresenrAddress,
                Email=Vmodel.Email,
                DrivingLicenceNo=Vmodel.DrivingLicenceNo,
                PostCode=Vmodel.PostCode,
                City=Vmodel.City,
                LivingTown=Vmodel.LivingTown,
                Gender=Vmodel.Gender,
                MaritalStatus=Vmodel.MaritalStatus

            };
            if (files != null)
            {
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
                            personImg = "FCImg" + Vmodel.PassportNo + fileName + random + extension;
                            model.PersonImage = personImg;
                            ImagePath = Path.Combine(Server.MapPath("~/Image/ForeignCitizen/PersonImg/"), personImg);
                            file.SaveAs(ImagePath);

                        }
                        if (i == 1)
                        {
                            passportImg = "FCImg" + Vmodel.PassportNo + fileName + random + extension;
                            model.DLicenceCopy = passportImg;
                            ImagePath = Path.Combine(Server.MapPath("~/Image/ForeignCitizen/FPassport/"), passportImg);
                            file.SaveAs(ImagePath);
                        }
                        if (i == 2)
                        {
                            drivingLicence = "FCImg" + Vmodel.PassportNo + fileName + random + extension;
                            model.DLicenceCopy = drivingLicence;
                            ImagePath = Path.Combine(Server.MapPath("~/Image/ForeignCitizen/FDrivingLicence/"), drivingLicence);
                            file.SaveAs(ImagePath);
                        }
                        if (i == 3)
                        {
                            policeClearance = "FCImg" + Vmodel.PassportNo + fileName + random + extension;
                            model.PoliceClearImg = policeClearance;
                            ImagePath = Path.Combine(Server.MapPath("~/Image/ForeignCitizen/PoliceClearance/"), policeClearance);
                            file.SaveAs(ImagePath);
                        }
                    }
                    i++;
                }

            }
            model.PersonImage = personImg;
            model.PassportImagCopy = passportImg;
            model.DLicenceCopy = drivingLicence;
            model.PoliceClearImg = policeClearance;
            try
            {
                db.ForeignCitizenModels.Add(model);
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

        // GET: ForeignCitizen/Edit/5
        public ActionResult Edit(Guid? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForeignCitizenModel model = db.ForeignCitizenModels.Find(id);
            ViewBag.CountryList = db.Countries.ToList();
            ForeignCitizenViewModel Vmodel = new ForeignCitizenViewModel
            {
                ForignCitizenId = model.ForignCitizenId,
                CitizenName = model.CitizenName,
                FatherName = model.FatherName,
                MotherName = model.MotherName,
                PassportNo = model.PassportNo,
                CountryName = model.CountryName,
                Age = model.Age,
                State = model.State,
                Contact = model.Contact,
                PresenrAddress = model.PresenrAddress,
                Email = model.Email,
                DrivingLicenceNo = model.DrivingLicenceNo,
                PostCode = model.PostCode,
                City = model.City,
                LivingTown = model.LivingTown,
                Gender = model.Gender,
                MaritalStatus = model.MaritalStatus,
                PersonImage = model.PersonImage,
                PassportImagCopy = model.PassportImagCopy,
                DLicenceCopy = model.DLicenceCopy,
                PoliceClearImg = model.PoliceClearImg

            };           
            return View(Vmodel);
        }

        // POST: ForeignCitizen/Edit/5

        public JsonResult ForeignCitizenUpdate(ForeignCitizenViewModel Vmodel, IEnumerable<HttpPostedFileBase> files)
        {
            string personImg = "";
            string passportImg = "";
            string policeClearance = "";
            string drivingLicence = "";
            int operationStatus = 1;
            string ImagePath = "";
            ForeignCitizenModel fCitizen = db.ForeignCitizenModels.Find(Vmodel.ForignCitizenId);
            if (fCitizen.ForignCitizenId != null) { 
                if (fCitizen.PersonImage != null)
                {
                    personImg = Path.Combine(Server.MapPath("~/Image/ForeignCitizen/PersonImg/"), fCitizen.PassportImagCopy);
                    if (System.IO.File.Exists(personImg))
                    {
                        System.IO.File.Delete(personImg);
                    }
                }
                if (fCitizen.DrivingLicenceNo!=null)
                {
                    drivingLicence = Path.Combine(Server.MapPath("~/Image/ForeignCitizen/FDrivingLicence/"), fCitizen.DLicenceCopy);
                    if (System.IO.File.Exists(drivingLicence))
                    {
                        System.IO.File.Delete(drivingLicence);
                    }
                }
                if (fCitizen.PoliceClearImg != null)
                {
                    policeClearance = Path.Combine(Server.MapPath("~/Image/ForeignCitizen/FPassport/"), fCitizen.PoliceClearImg);
                    if (System.IO.File.Exists(policeClearance))
                    {
                        System.IO.File.Delete(policeClearance);
                    }
                }
                if (fCitizen.PassportImagCopy != null)
                {
                    passportImg = Path.Combine(Server.MapPath("~/Image/ForeignCitizen/PoliceClearance/"), fCitizen.PassportImagCopy);
                    if (System.IO.File.Exists(passportImg))
                    {
                        System.IO.File.Delete(passportImg);
                    }
                }

                if (files != null)
                {
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

                            if (i == 0 && fileName != null)
                            {
                                personImg = "FCImg" + fCitizen.PassportNo + fileName + random + extension;
                                fCitizen.PersonImage = personImg;
                                ImagePath = Path.Combine(Server.MapPath("~/Image/ForeignCitizen/PersonImg/"), personImg);
                                file.SaveAs(ImagePath);

                            }
                            if (i == 1 && fileName != null)
                            {
                                passportImg = "FCImg" + fCitizen.PassportNo + fileName + random + extension;
                                fCitizen.PassportImagCopy = passportImg;
                                ImagePath = Path.Combine(Server.MapPath("~/Image/ForeignCitizen/FPassport/"), passportImg);
                                file.SaveAs(ImagePath);
                            }
                            if (i == 2 && fileName != null)
                            {
                                drivingLicence = "FCImg" + fCitizen.PassportNo + fileName + random + extension;
                                fCitizen.DLicenceCopy = drivingLicence;
                                ImagePath = Path.Combine(Server.MapPath("~/Image/ForeignCitizen/FDrivingLicence/"), drivingLicence);
                                file.SaveAs(ImagePath);
                            }
                            if (i == 3 && fileName != null)
                            {
                                policeClearance = "FCImg" + fCitizen.PassportNo + fileName + random + extension;
                                fCitizen.PoliceClearImg = policeClearance;
                                ImagePath = Path.Combine(Server.MapPath("~/Image/ForeignCitizen/PoliceClearance/"), policeClearance);
                                file.SaveAs(ImagePath);
                            }
                        }
                        i++;
                    }

                }
                fCitizen.ForignCitizenId = Vmodel.ForignCitizenId;
                fCitizen.CitizenName = Vmodel.CitizenName;
                fCitizen.FatherName = Vmodel.FatherName;
                fCitizen.MotherName = Vmodel.MotherName;
                fCitizen.PassportNo = Vmodel.PassportNo;
                fCitizen.CountryName = Vmodel.CountryName;
                fCitizen.Age = Vmodel.Age;
                fCitizen.State = Vmodel.State;
                fCitizen.Contact = Vmodel.Contact;
                fCitizen.PresenrAddress = Vmodel.PresenrAddress;
                fCitizen.Email = Vmodel.Email;
                fCitizen.DrivingLicenceNo = Vmodel.DrivingLicenceNo;
                fCitizen.PostCode = Vmodel.PostCode;
                fCitizen.City = Vmodel.City;
                fCitizen.LivingTown = Vmodel.LivingTown;
                fCitizen.Gender = Vmodel.Gender;
                fCitizen.MaritalStatus = Vmodel.MaritalStatus;
                db.Entry(fCitizen).State = EntityState.Modified;
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

        // GET: ForeignCitizen/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForeignCitizenModel foreignCitizenModel = db.ForeignCitizenModels.Find(id);
            if (foreignCitizenModel == null)
            {
                return HttpNotFound();
            }
            return View(foreignCitizenModel);
        }

        // POST: ForeignCitizen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ForeignCitizenModel model = db.ForeignCitizenModels.Find(id);
            if (model.ForignCitizenId != null)
            {
                string personImg = Path.Combine(Server.MapPath("~/Image/ForeignCitizen/PersonImg/"), model.PassportImagCopy);
                string drivingLicence = Path.Combine(Server.MapPath("~/Image/ForeignCitizen/FDrivingLicence/"), model.DLicenceCopy);
                string policeClearance = Path.Combine(Server.MapPath("~/Image/ForeignCitizen/FPassport/"), model.PoliceClearImg);
                string passportImg = Path.Combine(Server.MapPath("~/Image/ForeignCitizen/PoliceClearance/"), model.PassportImagCopy);

                if (System.IO.File.Exists(personImg))
                {
                    System.IO.File.Delete(personImg);
                }
                if (System.IO.File.Exists(drivingLicence))
                {
                    System.IO.File.Delete(drivingLicence);
                }
                if (System.IO.File.Exists(policeClearance))
                {
                    System.IO.File.Delete(policeClearance);
                }
                if (System.IO.File.Exists(passportImg))
                {
                    System.IO.File.Delete(passportImg);
                }


            }
            db.ForeignCitizenModels.Remove(model);
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
