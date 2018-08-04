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
    public class SliderRepository : ISliderRepository
    {
        private readonly HaberContext ctx = new HaberContext();

        public int Count()
        {
            return ctx.Slider.Count();
        }

        public void Delete(int id)
        {
            var etiket = ctx.Slider.Find(id);
            if (etiket != null)
            {
                ctx.Slider.Remove(etiket);
            }
        }

        public Slider Get(Expression<Func<Slider, bool>> kosul)
        {
            return ctx.Slider.FirstOrDefault(kosul);
        }

        public IEnumerable<Slider> GetAll()
        {
            return ctx.Slider.Select(x => x);
        }

        public Slider GetByID(int id)
        {
            return ctx.Slider.Find(id);
        }

        public IQueryable<Slider> GetMany(Expression<Func<Slider, bool>> kosul)
        {
            return ctx.Slider.Where(kosul);
        }

        public void Insert(Slider obj)
        {
            ctx.Slider.Add(obj);
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        public void Update(Slider obj)
        {
            ctx.Slider.AddOrUpdate(obj);
        }
       
    }
}
