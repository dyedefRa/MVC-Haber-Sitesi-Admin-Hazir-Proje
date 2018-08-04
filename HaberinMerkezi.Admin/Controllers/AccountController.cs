
using HaberinMerkezi.Core.Intrastructure;
using HaberinMerkezi.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaberinMerkezi.Admin.Controllers
{
    public class AccountController : Controller
    {
        #region Kullanıcı
        private readonly IKullaniciRepository _kullaniciRepository;
        public AccountController(IKullaniciRepository kullaniciRepository)
        {
            _kullaniciRepository = kullaniciRepository;
        }
        #endregion
       
        [HttpGet]
        public ActionResult Login()
        {
            Session["AktifKullanici"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Login(Kullanici temp, string hatirlabeni)
        {
            var kullaniciVarmi = _kullaniciRepository.GetMany(x => x.Email == temp.Email && x.Parola == temp.Parola && x.Aktif == true).SingleOrDefault();
            if (kullaniciVarmi!=null)
            {
                if (kullaniciVarmi.Rol.RolAdi=="Admin")
                {
                    


                    Session["AktifKullanici"] = kullaniciVarmi;
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Mesaj = "Yetkisiz Kullanıcı";
                return View();

            }
            ViewBag.Mesaj = "Kullanıcı Bulunamadı..";
            return View();
        }

       

   
    }
}