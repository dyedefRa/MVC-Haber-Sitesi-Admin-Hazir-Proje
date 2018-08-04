using HaberinMerkezi.Admin.App_Classes;
using HaberinMerkezi.Core.Intrastructure;
using HaberinMerkezi.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

//Paged list 1 USING
namespace HaberinMerkezi.Admin.Controllers
{
    [LoginFilter]
    public class KategoriController : Controller
    {


        #region Kategori

        private readonly IKategoriRepository _kategoriRepository;

        public KategoriController(IKategoriRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
        }

        #endregion

   

        //Paged list olayi 2 Parametre 
        // Ayrıca Sile burdan gidiyoz
        public ActionResult Index(int Sayfa = 1)
        {
          
            //Paged list olayi 3
            var kategoriList = _kategoriRepository.GetAll().OrderByDescending(x => x.Id).ToPagedList(Sayfa, 10);
            return View(kategoriList);
        }


        #region Kategori Ekle
        [HttpGet]
        public ActionResult Ekle()
        {
            SetKategoriListele();
            return View();
        }
        [HttpPost]
        public JsonResult Ekle(Kategori temp)
        {

            try
            {
                if (temp.ParentID == null)
                {
                    temp.ParentID = 0;
                }
                var aktifKullanici = (Kullanici)Session["AktifKullanici"];

                temp.KullaniciID = aktifKullanici.Id;
                _kategoriRepository.Insert(temp);
                _kategoriRepository.Save();
                return Json(new ResultJSON { Success = true, Message = "Kategori ekleme işleminiz başarılı." });
            }
            catch (Exception)
            {

                return Json(new ResultJSON { Success = false, Message = "Kategori ekleme işleminde sorun oluştu.." });
            }

        }
        #endregion

        //BURASI ÇOK ONEMLII @@@@@@@@@@@@@@@@@@@@@@@@@@
        //resultJSON nin DİBİ
        //bootBox.js kullanımı
        public JsonResult Sil(int id)
        {
            var kategori = _kategoriRepository.GetByID(id);
            if (kategori == null)
            {
                return Json(new ResultJSON { Success = false, Message = "Kategori bulunamadı." });
            }
            _kategoriRepository.Delete(id);
            _kategoriRepository.Save();
            return Json(new ResultJSON { Success = true, Message = "Kategori silme işlemi başarıyla tamamlandı.." });
        }


        #region KategoriDüzenle GET/POST
        //[LoginFilter]
        [HttpGet]
        public ActionResult Duzenle(int id)
        {
            var kategori = _kategoriRepository.GetByID(id);
            if (kategori == null)
            {
                return RedirectToAction("Index", "Error");
            }
            //Dropdownlist ile üst kategorileri değiştirme olayları yapabiliriz..
            SetKategoriListele();
            return View(kategori);
        }
        
        [HttpPost]
        public JsonResult Duzenle(Kategori temp)
        {
            if (ModelState.IsValid)
            {
                var kategori = _kategoriRepository.GetByID(temp.Id);
                kategori.Aktif = temp.Aktif;
                kategori.Adi = temp.Adi;
                kategori.ParentID = temp.ParentID;
                if (temp.ParentID==null)
                {
                    kategori.ParentID = 0;
                }
                kategori.URL = temp.URL;

                _kategoriRepository.Save();

               
                TempData["katDuzenle"] = "Düzenleme işlemi başarıyla tamamlandı..";
                return Json(new ResultJSON { Success = true, Message = "Kaydetme işleminiz başarıyla tamamlandı" });
            }
            TempData["katDuzenle"] = "Düzenleme işlemi başarısız..";
            return Json(new ResultJSON { Success = false, Message = "Kaydetme işleminiz başarısız." });
        }


      
        #endregion

        #region setKategori
        public void SetKategoriListele()
        {
            //ParentID==0 demek üst kategori demek.
            var kategoriList = _kategoriRepository.GetMany(x => x.ParentID == 0).ToList();
            ViewBag.Kategori = kategoriList;
        }
        #endregion
    }
}