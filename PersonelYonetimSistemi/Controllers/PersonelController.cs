using PersonelYonetimSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PersonelYonetimSistemi.ViewModels;

namespace PersonelYonetimSistemi.Controllers
{
    public class PersonelController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();

        // GET: Personel
        public ActionResult Index()
        {
            var model = db.Personel.Include(x => x.Departman).ToList();

            return View(model);
        }
        [HttpGet]
        public ActionResult Yeni()
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlarlar = db.Departman.ToList(),Personel = new Personel()
            };
            return View("PersonelForm",model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Personel personel)
        {
            MesajViewModel m = new MesajViewModel();

            if (!ModelState.IsValid)
            {
                var model = new PersonelFormViewModel()
                {
                    Departmanlarlar = db.Departman.ToList(),
                    Personel = personel
                };
                return View("PersonelForm", model);
            }

            if(personel.id == 0)
            {
                db.Personel.Add(personel);
                m.Mesaj = "Ekleme Başarılı";
            }
            else
            {
                db.Entry(personel).State = System.Data.Entity.EntityState.Modified;
                m.Mesaj = "Başarıyla Güncellendi";
            }
            db.SaveChanges();
            m.Status = true;
            m.LinkText = "Personel Listesi";
            m.Url = "/Personel";
            return View("_Mesaj", m);
        }

        public ActionResult Guncelle(int id)
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlarlar = db.Departman.ToList(),
                Personel = db.Personel.Find(id)
            };
            return View("PersonelForm",model);
        }


        public ActionResult Sil(int id)
        {
            var silinecekPers = db.Personel.Find(id);

            if (silinecekPers == null)
            {
                return HttpNotFound();
            }

            db.Personel.Remove(silinecekPers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelListele(int id)
        {
            var model = db.Personel.Where(x => x.departmanID == id).ToList();
            return PartialView(model);
        }

        public int? ToplamMaas()
        {
            return db.Personel.Sum(x => x.maas);
        }

    }
}