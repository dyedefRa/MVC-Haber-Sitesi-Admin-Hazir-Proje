using HaberinMerkezi.Admin.App_Classes;

using HaberinMerkezi.Core.Intrastructure;
using HaberinMerkezi.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Drawing;
using System.IO;
using System.Text;

namespace HaberinMerkezi.Admin.Controllers
{
    [LoginFilter]
    public class HaberController : Controller
    {
        #region Kategori Repository
        HaberContext ctx = new HaberContext();

        private readonly IHaberRepository _haberRepository;
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly IKategoriRepository _kategoriRepository;
        private readonly IResimRepository _resimRepository;
        private readonly IEtiketRepository _etiketRepository;
        private readonly IHaberEtiketRepository _haberEtiketRepository;

        public HaberController(IHaberRepository haberRepository, IKullaniciRepository kullaniciRepository, IKategoriRepository kategoriRepository, IResimRepository resimRepository, IEtiketRepository etiketRepository , IHaberEtiketRepository haberEtiketRepository)
        {
            _haberRepository = haberRepository;
            _kullaniciRepository = kullaniciRepository;
            _kategoriRepository = kategoriRepository;
            _resimRepository = resimRepository;
            _etiketRepository = etiketRepository;
            _haberEtiketRepository = haberEtiketRepository;
        }

        #endregion


        // GET: Haber
        public ActionResult Index(int Sayfa = 1)
        {

            var haberList = _haberRepository.GetAll().OrderByDescending(x => x.Id).ToPagedList(Sayfa, 10);
            return View(haberList);
        }

        #region HaberEkle GET/POST

        [HttpGet]
        public ActionResult Ekle()
        {
            SetAllKategori();
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Haber temp, HttpPostedFileBase vitrinResim, List<HttpPostedFileBase> detayResimler, string etiketler)
        {
            if (temp.Baslik == null || temp.Aciklama == null || temp.KategoriID == 0 || vitrinResim == null)
            {
                TempData["haberEklenmeOlayi"] = "Lütfen başlık,açıklama,kategori seçimi ve resim alanlarını boş geçmeyiniz..";
                return RedirectToAction("Ekle");
            }
            Kullanici aktif = (Kullanici)HttpContext.Session["AktifKullanici"];



            try
            {
                //HABER EKLEMEK
                temp.KullaniciID = aktif.Id;

                _haberRepository.Insert(temp);
                _haberRepository.Save();


                //EKLENEN HABERIN ID SINI ALDIM
                //ACIKLAMAYLA ALDIM RISKLI !!!!
                var ekleneksonhaber = (Haber)_haberRepository.Get(x => x.Aciklama == temp.Aciklama);
                var sonhaberID = ekleneksonhaber.Id;


                //VITRIN RESIM ISLEMLERI
                Image orj = Image.FromStream(vitrinResim.InputStream);
                Bitmap bmOrta = new Bitmap(orj, boyutCeken.ortaBoyutCek);
                string resimAd = Guid.NewGuid() + Path.GetExtension(vitrinResim.FileName);
                string resimOrtaYol = $"/Images/HaberResimleri/OrtaBoyut/" + resimAd;

                //VITRIN RESIM EKLEMEK
                Resim eklenecekResim = new Resim { HaberID = sonhaberID, ResimYol = resimOrtaYol, VitrinMi = true };
                _resimRepository.Insert(eklenecekResim);
                _resimRepository.Save();
                bmOrta.Save(Server.MapPath(resimOrtaYol));

                //TOPLU DETAY RESMI EKLEME OLAYI
                if (detayResimler[0] != null)
                {

                    foreach (var detayResim in detayResimler)
                    {

                        Image detay = Image.FromStream(detayResim.InputStream);
                        Bitmap bmDetayOrta = new Bitmap(detay, boyutCeken.ortaBoyutCek);

                        string detayResimAd = Guid.NewGuid() + Path.GetExtension(detayResim.FileName);
                        string detayOrtaYol = $"/Images/HaberResimleri/OrtaBoyut/" + detayResimAd;

                        Resim diziResmi = new Resim { HaberID = sonhaberID, ResimYol = detayOrtaYol };
                        _resimRepository.Insert(diziResmi);
                        _resimRepository.Save();
                        bmDetayOrta.Save(Server.MapPath(detayOrtaYol));
                    }
                }

                //ETİKET OLAYI
                if (etiketler!=null)
                {
 _etiketRepository.EtiketEkle(etiketler, temp.Id);
                }
               



                TempData["haberEklenmeOlayi"] = "Haber başarıyla eklendi.";

                return RedirectToAction("Ekle");


        }
            catch
            {

                TempData["haberEklenmeOlayi"] = "Sorun oluştu lütfen tekrar deneyiniz..";
                return RedirectToAction("Ekle");
    }

}
        #endregion


        #region HaberSil 


        public JsonResult Sil(int id)
        {
            var haber = _haberRepository.GetByID(id);
            if (haber != null)
            {


                //Haberin Resimlerini SQL den ve Serverdan silmek
                List<Resim> haberResimleri = _resimRepository.GetMany(x => x.HaberID == id).ToList();
                foreach (var resim in haberResimleri)
                {
                    _resimRepository.Delete(resim.Id);
                    _resimRepository.Save();
                    System.IO.File.Delete(Server.MapPath(resim.ResimYol));

                }

                //Haberin HaberEtiketlerini SQLDEN silmek
                _haberEtiketRepository.HaberIDyeGoreSil(haber.Id);
                _haberEtiketRepository.Save();

            
                _haberRepository.Delete(id);
                _haberRepository.Save();
                return Json(new ResultJSON { Message = "Haber silme işlemi başarıyla tamamlandı.." , Success=true});
            }
            return Json(new ResultJSON { Message = "Haber silme işlemi başarısız ",Success=false });
        }
        #endregion


        #region HaberDuzenle GET/POST
        [HttpGet]
        public ActionResult Duzenle(int id)
        {
            var haber = _haberRepository.GetByID(id);


            if (haber == null)
            {
                return RedirectToAction("Index", "Error");
            }

            //ETIKETI DUZENLE TARAFINA ATMA OLAYI
            #region ETİKET OLAYLARI
            var etiketDizim = haber.HaberEtiket.Select(x => x.Etiket.Adi).ToArray();
            HaberEtiketModel model = new HaberEtiketModel
            {
                EtiketDizisi = etiketDizim,
                Haber = haber,
                TumEtiketler = _etiketRepository.GetAll().ToList()
            };
            StringBuilder sb = new StringBuilder();
            foreach (var etiket in model.EtiketDizisi)
            {
                sb.Append(etiket + ",");
            }
            sb.Remove(sb.Length - 1, 1);
            model.EtiketAdlari = sb.ToString();
            #endregion


            SetAllKategori();
            return View(model);
        }
        [HttpPost]
        public ActionResult Duzenle(HaberEtiketModel temp, HttpPostedFileBase yeniVitrinResim, List<HttpPostedFileBase> detayResimler)
        {

            if (yeniVitrinResim != null)
            {
                var eskiVitrin = _resimRepository.Get(x => x.HaberID == temp.Haber.Id && x.VitrinMi == true);
                eskiVitrin.VitrinMi = false;


                Image orj = Image.FromStream(yeniVitrinResim.InputStream);
                Bitmap bmOrta = new Bitmap(orj, boyutCeken.ortaBoyutCek);
                string resimAd = Guid.NewGuid() + Path.GetExtension(yeniVitrinResim.FileName);
                string resimOrtaYol = $"/Images/HaberResimleri/OrtaBoyut/" + resimAd;

                //YENI VITRIN RESIM EKLEMEK
                Resim eklenecekResim = new Resim { HaberID = temp.Haber.Id, ResimYol = resimOrtaYol, VitrinMi = true };
                _resimRepository.Insert(eklenecekResim);
                _resimRepository.Save();
                bmOrta.Save(Server.MapPath(resimOrtaYol));

            }

            if (detayResimler[0] != null)
            {

                foreach (var detayResim in detayResimler)
                {

                    Image detay = Image.FromStream(detayResim.InputStream);
                    Bitmap bmDetayOrta = new Bitmap(detay, boyutCeken.ortaBoyutCek);

                    string detayResimAd = Guid.NewGuid() + Path.GetExtension(detayResim.FileName);
                    string detayOrtaYol = $"/Images/HaberResimleri/OrtaBoyut/" + detayResimAd;

                    Resim diziResmi = new Resim { HaberID = temp.Haber.Id, ResimYol = detayOrtaYol };
                    _resimRepository.Insert(diziResmi);
                    _resimRepository.Save();
                    bmDetayOrta.Save(Server.MapPath(detayOrtaYol));
                }
            }

            //ETIKET DUZENLEME OLAYI
            //EKLEME ILE AYNI METODDA
            if (temp.EtiketAdlari!=null)
            {
                _etiketRepository.EtiketEkle(temp.EtiketAdlari, temp.Haber.Id);
            }


         



            var haber = _haberRepository.GetByID(temp.Haber.Id);
            haber.KategoriID = temp.Haber.KategoriID;
            haber.KisaAciklama = temp.Haber.KisaAciklama;
            haber.Aciklama = temp.Haber.Aciklama;
            haber.Baslik = temp.Haber.Baslik;
            haber.Aktif = temp.Haber.Aktif;

            _haberRepository.Save();
            TempData["haberDuzenle"] = "Haber düzenleme işlemi başarılı";

            return RedirectToAction("Duzenle");
        }



        public JsonResult DetayResimSil(int id)
        {
            var resim = _resimRepository.GetByID(id);
            if (resim == null)
            {
                throw new Exception("Resim bulunmadı");
            }
            try
            {
                _resimRepository.Delete(id);
                _resimRepository.Save();
                System.IO.File.Delete(Server.MapPath(resim.ResimYol));
                return Json(new ResultJSON { Success = true });
            }
            catch
            {

                return Json(new ResultJSON { Success = false });
            }

        }

        public JsonResult AktifPasif(int id)
        {
            var data = _haberRepository.GetByID(id);
            if (data == null)
            {
                throw new Exception("Haber bulunamadı");
            }
            if (data.Aktif == true)
            {
                data.Aktif = false;
            }
            else
            {
                data.Aktif = true;
            }
            _haberRepository.Save();

            return Json(new ResultJSON { Message = $"{id} numaralı haber aktif/pasif durumu değişti.", Success = true });
        }


        #endregion

        public void SetAllKategori()
        {
            var kategoriList = _kategoriRepository.GetMany(x => x.Aktif == true).ToList();
            ViewBag.Kategori = kategoriList;
        }


    }
}