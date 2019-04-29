using PersonelYonetimSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PersonelYonetimSistemi.Controllers
{
    [AllowAnonymous]

    public class SecurityController : Controller
    {


        PersonelDbEntities db = new PersonelDbEntities();

        

        // GET: Security

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
       
        public ActionResult Login(Kullanici kullanici)
        {
            var kullaniciInDB = db.Kullanici.FirstOrDefault(x => x.Ad == kullanici.Ad && x.Sifre==kullanici.Sifre);
            if(kullaniciInDB != null)
            {
                FormsAuthentication.SetAuthCookie(kullaniciInDB.Ad,false);//auth olduk
                return RedirectToAction("Index", "Departman");
            }
            else
            {
                ViewBag.Mesaj = "Geçersiz Kullanıcı Adı veya Şifre";
                return View();
            }
        }

        public ActionResult YeniUye()
        {
            return View("YeniKullanici");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }
    }
}