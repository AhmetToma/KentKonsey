using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using KentKonsey.Dal;
using System.Web.Configuration;
using System.Drawing;
using PagedList;
using KentKonsey.Models;
using System.IO;

namespace KentKonSey.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        const string FolderImagePath = "/Content/images/HaberFoto/";


        public ActionResult Index()
        {
             if (Session["Giris"] == null)
            {
                return RedirectToAction("Haberler", "KentKonsey", new { area = "" });
            }
            
            return View();
       
        }

        public ActionResult Giris(string kullAdi, string sifre)
        {
           
            string cAd = WebConfigurationManager.AppSettings["KullaniciAd"];
            string cSifre = WebConfigurationManager.AppSettings["Sifre"];
            if (kullAdi == cAd && sifre == cSifre)
            {
                Session.Add("Giris", true);
                return RedirectToAction("Index", "Home");
              
            }
            string a2 = "Alert";
            ViewBag.alert = a2;

            return RedirectToAction("Haberler", "KentKonsey", new { area = "" });

        }



        public ActionResult Cikis ()
        {
            Session.Clear();
            return RedirectToAction("Haberler", "KentKonsey", new { area = "" });
        }

        public JsonResult GetHaberList( )
        {

            using (KentKonseyContext db = new KentKonseyContext())
            {

                var haberlist = db.Haber.OrderByDescending(x=>x.id).ToList();
                return Json(haberlist, JsonRequestBehavior.AllowGet);
            }
            
        }

       
        public JsonResult GetHaberById(int haberid)
        {
            KentKonseyContext db = new KentKonseyContext();
            Haber model = db.Haber.Where(x => x.id == haberid).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveDataInDatabase(Haber model)
        {
            KentKonseyContext db = new KentKonseyContext();
            var result = false;
            try
            {
                if (model.id > 0)
                {
                    Haber Stu = db.Haber.SingleOrDefault(x => x.id == model.id);
                    Stu.baslik = model.baslik;
                    Stu.aciklama = model.aciklama;
                    
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    Haber hab = new Haber();
                    hab.baslik = model.baslik;
                    hab.aciklama = model.aciklama;
                    hab.tarih = DateTime.Now;
                    var fileName = model.Foto.FileName;
                    var path = Path.Combine(Server.MapPath("~" + FolderImagePath), fileName);
                    model.Foto.SaveAs(path);
                    model.FotoUrl = FolderImagePath + fileName;
                    hab.FotoUrl = model.FotoUrl;
                    db.Haber.Add(hab);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult HaberSilme(int HaberId)
        {
            KentKonseyContext db = new KentKonseyContext();
            bool result = false;
            Haber hab = db.Haber.SingleOrDefault(x => x.id == HaberId);
            if (hab != null)
            {
                db.Haber.Remove(hab);
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }




        public ActionResult EkleOzel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EkleOzel(HaberModel model)
        {
            

            
                if (ModelState.IsValid)
            
          {
                KentKonseyContext db = new KentKonseyContext();

                Haber hab = new Haber();
                hab.baslik = model.baslik;
                hab.aciklama = model.aciklama;
                hab.tarih = DateTime.Now;
                if (model.Foto != null && model.Foto.ContentLength > 0)
                {
                    var fileName = model.Foto.FileName;
                    var path = Path.Combine(Server.MapPath("~" + FolderImagePath), fileName);
                    model.Foto.SaveAs(path);
                    model.FotoUrl = FolderImagePath + fileName;
                    hab.FotoUrl = model.FotoUrl;
                }

                db.Haber.Add(hab);
                db.SaveChanges();
                
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
        public ActionResult EditOzel(int? id)
        {
            using (KentKonseyContext db = new KentKonseyContext())
            {
                var model = db.Haber.Where(x => x.id == id).SingleOrDefault();
                HaberModel Hab = new HaberModel();
                Hab.id = model.id;
                Hab.baslik = model.baslik;
                Hab.aciklama = model.aciklama;
              
                return View(Hab);
            }
        }

        [HttpPost]
        public ActionResult EditOzel(HaberModel model)
        {
            if (ModelState.IsValid)
            {
                using (KentKonseyContext db = new KentKonseyContext())
                {

                    Haber hab = new Haber();
                    hab.id = model.id;
                    hab.baslik = model.baslik;
                    hab.aciklama = model.aciklama;
                    hab.tarih = DateTime.Now;
                    if (model.Foto != null && model.Foto.ContentLength > 0)
                    {
                        var fileName = model.Foto.FileName;
                        var path = Path.Combine(Server.MapPath("~" + FolderImagePath), fileName);
                        model.Foto.SaveAs(path);
                        model.FotoUrl = FolderImagePath + fileName;
                        hab.FotoUrl = model.FotoUrl;
                    }

                    db.Haber.Attach(hab);
                    db.Entry(hab).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                   
                }
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

       
    }
    
}