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
using SafetyAndSecurityForNSI.ModelView;
using System.Configuration;

namespace SafetyAndSecurityForNSI.Controllers
{
    [Authorize]
    public class SSIController : Controller
    {
        private SSFORNSIDBFinalEntities db = new SSFORNSIDBFinalEntities();
        static private int offset = Convert.ToInt32(ConfigurationManager.AppSettings["localTime"]);
        DateTime now = DateTime.UtcNow.AddMinutes(offset);
        // GET: SSI
        public ActionResult Index(int?page,Guid? SSIId)
        {
            int pageNo = 0;
            pageNo = page == null ? 1 : int.Parse(page.ToString());
            var allList = db.SSIModels.ToList();
            if (allList != null)
            {
                allList = allList.Where(m => m.SSIId == SSIId).ToList();
            }
            int allCount = db.SSIModels.Count();
            int dataPerPage = 20;
            int dataPerPageEnd = pageNo * dataPerPage;
            int dataPerPageStart = dataPerPageEnd - dataPerPage;
            var data = allList.OrderBy(e => e.SSIId).Skip(dataPerPageStart).Take(dataPerPage);
            Pager<SSIModel> pager = new Pager<SSIModel>(data.AsQueryable(), pageNo, dataPerPage, allCount);


            return View(pager);
        }
        public JsonResult SearchData(long searchId)
        {
            if (searchId == 0)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = db.SSIModels.Where(m => m.NID == searchId || m.ParentsSSINo == searchId).Distinct().ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }
        // GET: SSI/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SSIModel sSIModel = db.SSIModels.Find(id);
            if (sSIModel == null)
            {
                return HttpNotFound();
            }
            return View(sSIModel);
        }

        // GET: SSI/Create
        public ActionResult Create()
        {
            ViewBag.CitizenCategory = db.CitizenShipModels.ToList();
            ViewBag.EthnicityCategory = db.EthnicityModels.ToList();
            ViewBag.RaceCategory = db.RaceModels.ToList();
            ViewBag.GenderCategory = db.SexModels.ToList();
            return View();
        }
        public JsonResult SSIEntrySave(SSIViewModel sSIModel, IEnumerable<HttpPostedFileBase> files)
        {

            string personImg = "";
            int operationStatus = 1;
            string ImagePath = Server.MapPath("~/Image/SSI/");
            if (!Directory.Exists(ImagePath))
            {
                Directory.CreateDirectory(ImagePath);
            }
            SSIModel model = new SSIModel
            {
                SSIId = Guid.NewGuid(),
                FirstName=sSIModel.FirstName,
                MiddleName=sSIModel.MiddleName,
                LastName=sSIModel.LastName,
                BirthFirstName=sSIModel.BirthFirstName,
                BirthMiddleName=sSIModel.BirthMiddleName,
                BirthLastName=sSIModel.BirthLastName,
                ThanaCode=sSIModel.ThanaCode,
                PostalCode=sSIModel.PostalCode,
                NidLastdigit=sSIModel.NidLastdigit,
                PlaceOfBirth=sSIModel.PlaceOfBirth,
                City=sSIModel.City,
                Thana=sSIModel.Thana,
                DateOfBirth=sSIModel.DateOfBirth,
                CitizenShip=sSIModel.CitizenShip,
                Ethnicity=sSIModel.Ethnicity,
                Race=sSIModel.Race,
                ParentsFirstName=sSIModel.ParentsFirstName,
                ParentsMiddleName=sSIModel.ParentsMiddleName,
                ParentsLastName=sSIModel.ParentsLastName,
                ParentsSSINo=sSIModel.ParentsSSINo,
                PhoneNumber=sSIModel.PhoneNumber,
                MailingAddress=sSIModel.MailingAddress,
                NID=sSIModel.NID,
                PassportNo=sSIModel.PassportNo,
                Gender=sSIModel.Gender,
                Division=sSIModel.Division,
                Address=sSIModel.Address,
                CreateDate=now
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
                            personImg = "SSIImg" + sSIModel.NID + fileName + random + extension;
                            model.PersonImage = personImg;
                            ImagePath = Path.Combine(Server.MapPath("~/Image/SSI/PersonImage/"), personImg);
                            file.SaveAs(ImagePath);
                }
            }
            try
            {
                db.SSIModels.Add(model);
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

        // GET: SSI/Edit/5
        public ActionResult Edit(Guid? id)
        {
            SSIViewModel model = new SSIViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SSIModel sSIModel = db.SSIModels.Find(id);
            if(sSIModel.SSIId != null) { 
            model = new SSIViewModel
            {
                SSIId = sSIModel.SSIId,
                FirstName = sSIModel.FirstName,
                MiddleName = sSIModel.MiddleName,
                LastName = sSIModel.LastName,
                BirthFirstName = sSIModel.BirthFirstName,
                BirthMiddleName = sSIModel.BirthMiddleName,
                BirthLastName = sSIModel.BirthLastName,
                ThanaCode = sSIModel.ThanaCode,
                PostalCode = sSIModel.PostalCode,
                NidLastdigit = sSIModel.NidLastdigit,
                PlaceOfBirth = sSIModel.PlaceOfBirth,
                City = sSIModel.City,
                Thana = sSIModel.Thana,
                DateOfBirth = sSIModel.DateOfBirth,
                CitizenShip = sSIModel.CitizenShip,
                Ethnicity = sSIModel.Ethnicity,
                Race = sSIModel.Race,
                ParentsFirstName = sSIModel.ParentsFirstName,
                ParentsMiddleName = sSIModel.ParentsMiddleName,
                ParentsLastName = sSIModel.ParentsLastName,
                ParentsSSINo = sSIModel.ParentsSSINo,
                PhoneNumber = sSIModel.PhoneNumber,
                MailingAddress = sSIModel.MailingAddress,
                NID = sSIModel.NID,
                PersonImage=sSIModel.PersonImage,
                PassportNo = sSIModel.PassportNo,
                Gender = sSIModel.Gender,
                Division = sSIModel.Division,
                Address = sSIModel.Address
                
            };
            }
            ViewBag.CitizenCategory = db.CitizenShipModels.ToList();
            ViewBag.EthnicityCategory =db.EthnicityModels.ToList();
            ViewBag.RaceCategory = db.RaceModels.ToList();
            ViewBag.GenderCategory = db.SexModels.ToList();

            if (sSIModel == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        public JsonResult SSIEntryUpdate(SSIViewModel sSIModel, IEnumerable<HttpPostedFileBase> files)
        {

            string personImg = "";
            string ImgPath = "";
            int operationStatus = 1;
            SSIModel model = db.SSIModels.Find(sSIModel.SSIId);
            if (model.SSIId != null)
            {
                if (files != null)
                {
                    personImg= Path.Combine(Server.MapPath("~/Image/SSI/PersonImage/"), model.PersonImage);
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
                        personImg = "SSIImg" + sSIModel.NID + fileName + random + extension;
                        model.PersonImage = personImg;
                        ImgPath = Path.Combine(Server.MapPath("~/Image/SSI/PersonImage/"), personImg);
                        file.SaveAs(ImgPath);
                    }
                }
                model.SSIId = sSIModel.SSIId;
                model.FirstName = sSIModel.FirstName;
                model.MiddleName = sSIModel.MiddleName;
                model.LastName = sSIModel.LastName;
                model.BirthFirstName = sSIModel.BirthFirstName;
                model.BirthMiddleName = sSIModel.BirthMiddleName;
                model.BirthLastName = sSIModel.BirthLastName;
                model.ThanaCode = sSIModel.ThanaCode;
                model.PostalCode = sSIModel.PostalCode;
                model.NidLastdigit = sSIModel.NidLastdigit;
                model.PlaceOfBirth = sSIModel.PlaceOfBirth;
                model.City = sSIModel.City;
                model.Thana = sSIModel.Thana;
                model.DateOfBirth = sSIModel.DateOfBirth;
                model.CitizenShip = sSIModel.CitizenShip;
                model.Ethnicity = sSIModel.Ethnicity;
                model.Race = sSIModel.Race;
                model.ParentsFirstName = sSIModel.ParentsFirstName;
                model.ParentsMiddleName = sSIModel.ParentsMiddleName;
                model.ParentsLastName = sSIModel.ParentsLastName;
                model.ParentsSSINo = sSIModel.ParentsSSINo;
                model.PhoneNumber = sSIModel.PhoneNumber;
                model.MailingAddress = sSIModel.MailingAddress;
                model.NID = sSIModel.NID;
                model.PassportNo = sSIModel.PassportNo;
                model.Gender = sSIModel.Gender;
                model.Division = sSIModel.Division;
                model.Address = sSIModel.Address;
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


        // GET: SSI/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SSIModel sSIModel = db.SSIModels.Find(id);
            if (sSIModel == null)
            {
                return HttpNotFound();
            }
            return View(sSIModel);
        }

        // POST: SSI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SSIModel sSIModel = db.SSIModels.Find(id);
            if (sSIModel.SSIId != null)
            {
                if (sSIModel.PersonImage != null)
                {
                    string personImgPaths = Path.Combine(Server.MapPath("~/Image/SSI/PersonImage/"), sSIModel.PersonImage);

                    if (System.IO.File.Exists(personImgPaths))
                    {
                        System.IO.File.Delete(personImgPaths);
                    }
                }

                db.SSIModels.Remove(sSIModel);
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
