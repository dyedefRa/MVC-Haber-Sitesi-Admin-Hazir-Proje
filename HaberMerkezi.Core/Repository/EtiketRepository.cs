using HaberinMerkezi.Core.Intrastructure;
using HaberinMerkezi.Data.Context;

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;




namespace HaberinMerkezi.Core.Repository
{
    public class EtiketRepository : IEtiketRepository
    {

        private readonly HaberContext ctx = new HaberContext();
        public int Count()
        {
            return ctx.Etiket.Count();
        }

        public void Delete(int id)
        {
            var etiket = ctx.Etiket.Find(id);
            if (etiket != null)
            {
                ctx.Etiket.Remove(etiket);
            }
        }



        public Etiket Get(Expression<Func<Etiket, bool>> kosul)
        {
            return ctx.Etiket.FirstOrDefault(kosul);
        }

        public IEnumerable<Etiket> GetAll()
        {
            return ctx.Etiket.Select(x => x);
        }

        public Etiket GetByID(int id)
        {
            return ctx.Etiket.Find(id);
        }

        public IQueryable<Etiket> GetMany(Expression<Func<Etiket, bool>> kosul)
        {
            return ctx.Etiket.Where(kosul);
        }



        public void EtiketEkle(string etiketler, int eklenecekHaberID)
        {



            if (etiketler != null)
            {
                List<Etiket> etiketListesi = new List<Etiket>();
                //EKLEYECEGIMIZ HABERI ALDIK
                var eklenecekHaber = ctx.Haber.FirstOrDefault(x => x.Id == eklenecekHaberID);
                //HABER YOKSA HATA VERSIN
                if (eklenecekHaber == null)
                {
                    //UMARIM BURAYA GIRMEZ :D
                    throw new Exception("Haber bulunamadı");
                }
                //VIRGUL ILE AYRILMIS TAGLERI DIZIYE ATTIK
                string[] etiketDizisi = etiketler.ToLower().Split(',');
                //DIZININ ICINDE GEZIYORUZ
                for (int i = 0; i < etiketDizisi.Length; i++)
                {
                    var donenAd = etiketDizisi[i].ToString();
                    //BOYLE BIR ETIKET ADINDA BIR ETIKET VARMI
                    var etiketVarmi = ctx.Etiket.FirstOrDefault(x => x.Adi == donenAd);
                    //YOKSA ETIKETI SQLE EKLE VE ETIKET LISTESINE  BU ETIKETI EKLE
                    if (etiketVarmi == null)
                    {
                        Etiket et = new Etiket { Adi = etiketDizisi[i] };
                        ctx.Etiket.Add(et);
                        etiketListesi.Add(et);
                    }
                    //VARSA ETIKET LISTESINE BU ETIKETI EKLE
                    else
                    {
                        etiketListesi.Add(etiketVarmi);
                    }


                }
                ctx.SaveChanges();
              
                //ETIKET DUZENLENME OLAYLARI ICIN VAR OLAN HABERETIKETLERI ALIYORUZ
                var varolanHaberEtiketListesi = ctx.HaberEtiket.Where(x => x.HaberID == eklenecekHaberID).ToList();


                //GUNCEL HABERETIKETLERINI ATACAGIMIZ LISTEYI OLUSTURDUK (  EXCEPT LERIMIZ ESIT OLSUN DIYE)
                
                List<int> yeniHaberEtiketEqual = new List<int>();
                //HER ETIKET LISTESI ICIN BIR HABER ETIKET OLUSTURACAGIZ
                foreach (var etiket in etiketListesi)
                {
                    HaberEtiket yeni = new HaberEtiket { HaberID = eklenecekHaberID, EtiketID = etiket.Id };
                    //BOYLE BIR HABERETIKET VARSA EKLEME YAPMICAZ
                    var haberEtiketVarmi = ctx.HaberEtiket.FirstOrDefault(x => x.HaberID == yeni.HaberID && x.EtiketID == yeni.EtiketID);
                    //YOKSA HABERETIKET E SQLDE EKELME YAPICAZ
                    if (haberEtiketVarmi == null)
                    {
                        ctx.HaberEtiket.Add(yeni);
                    }
                    //VE HER HALUKARDA YENI HABERETIKET LISTEMIZE BU HABER ETIKETI EKLIYORUZ
                   
                    yeniHaberEtiketEqual.Add(yeni.EtiketID);

                }
               
                List<int> varolanHaberEtiketEqual = new List<int>();
                foreach (var item in varolanHaberEtiketListesi)
                {
                    
                    varolanHaberEtiketEqual.Add(item.EtiketID);
                }


                //AMAC SU ; YOLLADIGIM VIRGULLU ETIKET LISTESINI EGER OYLE BIR ETIKET YOKSA ETIKET LISTESINE KAYDETMEK ARDINDAN HABERETIKET LISTESINE KAYDETMEK
                // BURADAKI AMAC ISE KALDIRILMIS OLAN ETIKET HABERETIKETTEN KALDIRMAK

                var fazlalik = varolanHaberEtiketEqual.Except(yeniHaberEtiketEqual).ToList();
                foreach (var item in fazlalik)
                {
                    var haberetiket = ctx.HaberEtiket.FirstOrDefault(x => x.EtiketID == item && x.HaberID == eklenecekHaberID);
                    ctx.HaberEtiket.Remove(haberetiket);
                }
            

                ctx.SaveChanges();


            }
        }






        public void Insert(Etiket obj)
        {
            ctx.Etiket.Add(obj);
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        public void Update(Etiket obj)
        {
            ctx.Etiket.AddOrUpdate(obj);
        }
    }
}
