using KentKonsey.Dal;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KentKonsey.Controllers
{
    public class KentKonseyController : Controller
    {
        // GET: KentKonsey
        public ActionResult AnaSayfa()
        {

            return View();
        }
        public ActionResult Baskan()
        {
            return View();
        }

        public ActionResult Konsey()
        {
            return View();
        }
        public ActionResult komisyonlar()
        {
            return View();
        }

        public ActionResult OrganizasyonSemasi()
        {
            return View();
        }
        public ActionResult Haberler(int? page)
        {
            using (KentKonseyContext db = new KentKonseyContext())
            {
                var model = db.Haber.OrderByDescending(x=>x.id).ToList().ToPagedList(page ?? 1,3);
                return View(model);
            }
        }
        public ActionResult ArananHaber(string arama)
        {

            using (KentKonseyContext db = new KentKonseyContext())
            {
                var model = db.Haber.Where(x => x.baslik.Contains(arama) || x.aciklama.Contains(arama)).ToList();

                if (arama == "")
                {
                    return RedirectToAction("NotFound", "KentKonsey");

                }
                else if (model.Count() > 0)
                {

                    return View(model);
                }

                else
                {
                    return RedirectToAction("NotFound", "KentKonsey");

                }
            }
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public JsonResult GetHaberById(int haberid)
        {
            using (KentKonseyContext db = new KentKonseyContext())
            {
                var model = db.Haber.Where(x => x.id == haberid).SingleOrDefault();
                string value = string.Empty;
                value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);
            }
        }

  

        public ActionResult Galeri(int? page)
        {
            using (KentKonseyContext db = new KentKonseyContext())
            {
                var model = db.Galeri.OrderByDescending(x => x.ID).ToList().ToPagedList(page ?? 1, 6);
                return View(model);
            }


        }
    }
}