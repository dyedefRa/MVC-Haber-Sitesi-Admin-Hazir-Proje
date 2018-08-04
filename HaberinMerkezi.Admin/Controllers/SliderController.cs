using HaberinMerkezi.Admin.App_Classes;
using HaberinMerkezi.Core.Intrastructure;
using HaberinMerkezi.Data.Context;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace HaberinMerkezi.Admin.Controllers
{
    [LoginFilter]
    public class SliderController : Controller
    {

        private readonly ISliderRepository _sliderRepository;


        public SliderController(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }
        // GET: Slider
        public ActionResult Index(int Sayfa=1)
        {
            var data = _sliderRepository.GetAll().OrderByDescending(x => x.Id).ToPagedList(Sayfa, 10);
            return View(data);
        }

        #region Ekle GET/POST
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Ekle(Slider temp,HttpPostedFileBase ResimURL)
        {
           
           

                if (ResimURL!=null)
                {

                    Image orj = Image.FromStream(ResimURL.InputStream);
                    Bitmap bmOrta = new Bitmap(orj, boyutCeken.ortaBoyutCek);
                    string resimAd = Guid.NewGuid() + Path.GetExtension(ResimURL.FileName);
                    string resimOrtaYol = $"/Images/SliderResimleri/OrtaBoyut/" + resimAd;

                    temp.ResimURL = resimOrtaYol;
                    bmOrta.Save(Server.MapPath(resimOrtaYol));


                }
                _sliderRepository.Insert(temp);

                try
                {
                    _sliderRepository.Save();
                    return Json(new ResultJSON { Message = "Slider Ekleme işleminiz başarılı", Success = true });
                }
                catch (Exception)
                {

                    return Json(new ResultJSON { Message = "Slider Ekleme işleminiz başarısız", Success = false });
                }
           
          
        }

        #endregion

        public JsonResult Sil(int id)
        {
            var slider = _sliderRepository.GetByID(id);
            if (slider != null)
            {


                
                _sliderRepository.Delete(id);
                _sliderRepository.Save();
                System.IO.File.Delete(Server.MapPath(slider.ResimURL));
                return Json(new ResultJSON { Message = "Slider silme işlemi başarıyla tamamlandı..", Success = true });
            }
            return Json(new ResultJSON { Message = "Slider silme işlemi başarısız " , Success=false});
        }


       
       

        #region Duzenle GET/POST
        [HttpGet]
        public ActionResult Duzenle(int id)
        {
            var data = _sliderRepository.GetByID(id);
            if (data == null)
            {//????
                return View();
            }
            return View(data);
        }

        [HttpPost]
        public JsonResult Duzenle(Slider temp,HttpPostedFileBase resimGotur)
        {
            var data = _sliderRepository.GetByID(temp.Id);
            if (data==null)
            {
                throw new Exception("Slider bulunamadı!");
            }
            if (resimGotur!=null)
            {
                Image orj = Image.FromStream(resimGotur.InputStream);
                Bitmap bmOrta = new Bitmap(orj, boyutCeken.ortaBoyutCek);
                string resimAd = Guid.NewGuid() + Path.GetExtension(resimGotur.FileName);
                System.IO.File.Delete(Server.MapPath(data.ResimURL));
                string resimOrtaYol = $"/Images/SliderResimleri/OrtaBoyut/" + resimAd;
                bmOrta.Save(Server.MapPath(resimOrtaYol));
                data.ResimURL = resimOrtaYol;
            }
            data.Aciklama = temp.Aciklama;
            data.Aktif = temp.Aktif;
            data.Baslik = temp.Baslik;
            data.URL = temp.URL;
            try
            {
                _sliderRepository.Save();
                return Json(new ResultJSON { Message = "Slider Düzenleme işlemi başarılı.." , Success=true });

            }
            catch 
            {
                return Json(new ResultJSON { Message = "Slider Düzenleme işlemi başarısız..", Success = false });
            }
        }
        #endregion


        public JsonResult AktifPasif(int id)
        {
            var data = _sliderRepository.GetByID(id);
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
            _sliderRepository.Save();

            return Json(new ResultJSON { Message = $"{id} numaralı Slider aktif/pasif durumu değişti.", Success = true });
        }
    }
}