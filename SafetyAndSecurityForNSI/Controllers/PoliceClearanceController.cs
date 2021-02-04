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
using SafetyAndSecurityForNSI.ModelView;
using System.Configuration;

namespace SafetyAndSecurityForNSI.Controllers
{
    [Authorize]
    public class PoliceClearanceController : Controller
    {
        private SSFORNSIDBFinalEntities db = new SSFORNSIDBFinalEntities();
        static private int offset = Convert.ToInt32(ConfigurationManager.AppSettings["localTime"]);
        DateTime now = DateTime.UtcNow.AddMinutes(offset);

        // GET: PoliceClearance
        public ActionResult Index(int?page,Guid? thanaId)
        {
            int pageNo = 0;
            pageNo = page == null ? 1 : int.Parse(page.ToString());
            var allList = db.PoliceClearanceModels.ToList();
            if (thanaId != null)
            {
               allList = allList.Where(m => m.ThanaId == thanaId).ToList();
            }
            int allCount = db.PoliceClearanceModels.Count();
            int dataPerPage = 20;
            int dataPerPageEnd = pageNo * dataPerPage;
            int dataPerPageStart = dataPerPageEnd - dataPerPage;
            var data = allList.OrderBy(e => e.ThanaId).Skip(dataPerPageStart).Take(dataPerPage);
            Pager<PoliceClearanceModel> pager = new Pager<PoliceClearanceModel>(data.AsQueryable(), pageNo, dataPerPage, allCount);

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
                var data = db.PoliceClearanceModels.Where(m => m.NID == searchId).Distinct().ToList();
                return Json(data,JsonRequestBehavior.AllowGet);
            }

        }
        // GET: PoliceClearance/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceClearanceModel policeClearanceModel = db.PoliceClearanceModels.Find(id);
            if (policeClearanceModel == null)
            {
                return HttpNotFound();
            }
            return View(policeClearanceModel);
        }

        // GET: PoliceClearance/Create
        public ActionResult Create()
        {
            ViewBag.GenderCategory = db.SexModels.ToList();
            return View();
        }


        public JsonResult PoliceVarificationSave(PoliceClearanceViewModel vModel,IEnumerable<HttpPostedFileBase>files)
        {

            string personImg = "";
            int operationStatus = 1;
            string ImagePath = Server.MapPath("~/Image/PoliceClearance/");
            if (!Directory.Exists(ImagePath))
            {
                Directory.CreateDirectory(ImagePath);
            }
            PoliceClearanceModel model = new PoliceClearanceModel
            {
                ThanaId = Guid.NewGuid(),
                OfficerName= vModel.OfficerName,
                ThanaName=vModel.ThanaName,
                ThanaDetails=vModel.ThanaDetails,
                NID=vModel.NID,
                Gender=vModel.Gender,
                PassportNo=vModel.PassportNo,
                TokenNumber=vModel.TokenNumber,
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
                    personImg = "PCImg" + vModel.NID + fileName + random + extension;
                    model.PoliceClearImg = personImg;
                    ImagePath = Path.Combine(Server.MapPath("~/Image/PoliceClearance/"), personImg);
                    file.SaveAs(ImagePath);
                }
            }
            try
            {
                db.PoliceClearanceModels.Add(model);
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

        // GET: PoliceClearance/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceClearanceModel model = db.PoliceClearanceModels.Find(id);
            ViewBag.GenderCategory = db.SexModels.ToList();
            PoliceClearanceViewModel vModel = new PoliceClearanceViewModel
            {
                ThanaId=model.ThanaId,
                TokenNumber=model.TokenNumber,
                ThanaName=model.ThanaName,
                ThanaDetails=model.ThanaDetails,
                Gender=model.Gender,
                NID=model.NID,
                OfficerName=model.OfficerName,
                PassportNo=model.PassportNo,
                PoliceClearImg=model.PoliceClearImg

            };

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(vModel);
        }


        public ActionResult UpdatePoliceClearance(PoliceClearanceViewModel vModel,IEnumerable<HttpPostedFileBase>files)
        {
            string clearanceImage = "";
            string ImgPath = "";
            int operationStatus = 1;
            PoliceClearanceModel model = db.PoliceClearanceModels.Find(vModel.ThanaId);
            if (model.ThanaId != null)
            {
                if (files != null)
                {
                    clearanceImage = Path.Combine(Server.MapPath("~/Image/PoliceClearance/"), model.PoliceClearImg);
                    if (System.IO.File.Exists(clearanceImage))
                    {
                        System.IO.File.Delete(clearanceImage);
                    }
                    foreach (var file in files)
                    {
                        Random generator = new Random();
                        string random = generator.Next(0, 900000).ToString("D6");
                        string s = file.FileName;
                        int idx = s.LastIndexOf('.');
                        string fileName = s.Substring(0, idx);
                        string extension = s.Substring(idx);
                        clearanceImage = "SSIImg" + vModel.NID + fileName + random + extension;
                        model.PoliceClearImg = clearanceImage;
                        ImgPath = Path.Combine(Server.MapPath("~/Image/PoliceClearance/"), clearanceImage);
                        file.SaveAs(ImgPath);
                    }
                }
                model.ThanaId = vModel.ThanaId;
                model.ThanaName = vModel.ThanaName;
                model.TokenNumber = vModel.TokenNumber;
                model.OfficerName = vModel.OfficerName;
                model.PassportNo = vModel.PassportNo;
                model.ThanaDetails = vModel.ThanaDetails;
                model.NID = vModel.NID;
                model.TokenNumber = vModel.TokenNumber;
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

        // GET: PoliceClearance/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceClearanceModel policeClearanceModel = db.PoliceClearanceModels.Find(id);
            if (policeClearanceModel == null)
            {
                return HttpNotFound();
            }
            return View(policeClearanceModel);
        }

        // POST: PoliceClearance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PoliceClearanceModel model = db.PoliceClearanceModels.Find(id);
            if (model.ThanaId !=null)
            {
                if (model.PoliceClearImg != null)
                {
                    string imgPath = Path.Combine(Server.MapPath("~/Image/PoliceClearance/"),model.PoliceClearImg);
                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                }
                db.PoliceClearanceModels.Remove(model);
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
