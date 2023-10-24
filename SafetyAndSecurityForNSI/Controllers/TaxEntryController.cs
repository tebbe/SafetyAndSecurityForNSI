using System;
using System.Collections.Generic;
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
    public class TaxEntryController : Controller
    {
        private SSFORNSIDBFinalEntities db = new SSFORNSIDBFinalEntities();

        // GET: TaxEntry
        public ActionResult Index()
        {
            return View(db.TaxEntryModels.ToList());
        }

        // GET: TaxEntry/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxEntryModel taxEntryModel = db.TaxEntryModels.Find(id);
            if (taxEntryModel == null)
            {
                return HttpNotFound();
            }
            return View(taxEntryModel);
        }

        // GET: TaxEntry/Create
        public ActionResult Create()
        {
            ViewBag.GenderCategory = db.SexModels.ToList();
            return View();
        }

        // POST: TaxEntry/Create
        
        public JsonResult TaxEntrySave(TaxEntryViewModel taxModel,IEnumerable<HttpPostedFileBase>files)
        {
            string personImg = "";
            int operationStatus = 1;
            string ImagePath = Server.MapPath("~/Image/Tax/");
            if (!Directory.Exists(ImagePath))
            {
                Directory.CreateDirectory(ImagePath);
            }
            TaxEntryModel model = new TaxEntryModel
            {
                
                TaxId = Guid.NewGuid(),
                NID = taxModel.NID,
                SocialSecurityNumber=taxModel.SocialSecurityNumber,
                ITIN = taxModel.ITIN,
                EIN=taxModel.EIN,
                PassportNo=taxModel.PassportNo,
                TaxYear=taxModel.TaxYear,
                Gender=taxModel.Gender,
                CreatedBy= User.Identity.Name
            };
            if (files != null)
            {
                foreach (var file in files)
                {
                    Random generator = new Random();
                    string random = generator.Next(0, 900000).ToString("D6");
                    string s = file.FileName;
                    int idx = s.LastIndexOf('.');
                    string fileName = s.Substring(0, idx);
                    string extension = s.Substring(idx);
                    personImg = "TaxImg" + taxModel.NID + fileName + random + extension;
                    model.PersonImage = personImg;
                    ImagePath = Path.Combine(Server.MapPath("~/Image/Tax/"), personImg);
                    file.SaveAs(ImagePath);
                }
            }
            try
            {
                db.TaxEntryModels.Add(model);
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

        // GET: TaxEntry/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxEntryModel taxEntryModel = db.TaxEntryModels.Find(id);
            ViewBag.GenderCategory = db.SexModels.ToList();
            if (taxEntryModel == null)
            {
                return HttpNotFound();
            }
            return View(taxEntryModel);
        }

        // POST: TaxEntry/Edit
        public JsonResult TaxEntryUpdate(TaxEntryViewModel taxModel, IEnumerable<HttpPostedFileBase> files)
        {

            string personImg = "";
            string ImgPath = "";
            int operationStatus = 1;
            TaxEntryModel model = db.TaxEntryModels.Find(taxModel.TaxId);
            if (model.TaxId != null)
            {
                if (files != null)
                {
                    personImg = Path.Combine(Server.MapPath("~/Image/Tax/"), model.PersonImage);
                    if (System.IO.File.Exists(personImg))
                    {
                        System.IO.File.Delete(personImg);
                    }
                    foreach (var file in files)
                    {
                        Random generator = new Random();
                        string random = generator.Next(0, 900000).ToString("D6");
                        string s = file.FileName;
                        int idx = s.LastIndexOf('.');
                        string fileName = s.Substring(0, idx);
                        string extension = s.Substring(idx);
                        personImg = "Tax" + taxModel.NID + fileName + random + extension;
                        model.PersonImage = personImg;
                        ImgPath = Path.Combine(Server.MapPath("~/Image/Tax/"), personImg);
                        file.SaveAs(ImgPath);
                    }
                }
                model.TaxId = taxModel.TaxId;
                model.NID = taxModel.NID;
                model.SocialSecurityNumber = taxModel.SocialSecurityNumber;
                model.ITIN = taxModel.ITIN;
                model.EIN = taxModel.EIN;
                model.PassportNo = taxModel.PassportNo;
                model.TaxYear = taxModel.TaxYear;
                model.Gender = taxModel.Gender;
                model.UpdatedBy = User.Identity.Name;
                db.Entry(model).State = EntityState.Modified;
            }
            try
            {
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
        // GET: TaxEntry/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxEntryModel taxEntryModel = db.TaxEntryModels.Find(id);
            if (taxEntryModel == null)
            {
                return HttpNotFound();
            }
            return View(taxEntryModel);
        }

        // POST: TaxEntry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TaxEntryModel taxEntryModel = db.TaxEntryModels.Find(id);
            if (taxEntryModel.TaxId != null)
            {
                if (taxEntryModel.PersonImage != null)
                {
                    string personImgPaths = Path.Combine(Server.MapPath("~/Image/Tax/"), taxEntryModel.PersonImage);
                    if (System.IO.File.Exists(personImgPaths))
                    {
                        System.IO.File.Delete(personImgPaths);
                    }
                }
                db.TaxEntryModels.Remove(taxEntryModel);
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
