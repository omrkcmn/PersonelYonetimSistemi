using PersonelYonetimSistemi.Models;
using PersonelYonetimSistemi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonelYonetimSistemi.Controllers
{
    public class DepartmanController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();

        [Authorize]
        public ActionResult Index()
        {
            var model = db.Departman.ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult Yeni()
        {
            return View("DepartmanForm",new Departman());
        }

        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Departman dep)
        {
            if (!ModelState.IsValid)
                return View("DepartmanForm");

            MesajViewModel model = new MesajViewModel();

            if (dep.id == 0)
            {
                db.Departman.Add(dep);
                model.Mesaj = dep.ad + " eklendi";
            }
            else
            {
                var kayit = db.Departman.Find(dep.id);
                if(kayit == null)
                {
                    return HttpNotFound();
                }
                kayit.ad = dep.ad;
                model.Mesaj = dep.ad + " güncellendi";

            }
            db.SaveChanges();

            model.Status = true;
            model.LinkText = "Departman Listesi";
            model.Url = "/Departman";

            return View("_Mesaj",model);
        }

        public ActionResult Sil(int id)
        {
            var silinecekDep = db.Departman.Find(id);
            if (silinecekDep == null)
            {
                return HttpNotFound();
            }
            db.Departman.Remove(silinecekDep);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var model = db.Departman.Find(id);//modeli buldu viewe gönderdi.

            if (model == null)
            {
                return HttpNotFound();
            }

            return View("DepartmanForm", model);
        }


    }
}