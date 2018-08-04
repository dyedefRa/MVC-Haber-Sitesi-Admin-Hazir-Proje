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
    public class HaberRepository : IHaberRepository

    {

        private readonly HaberContext ctx = new HaberContext(); 

        public int Count()
        {
            return ctx.Haber.Count();
        }

        public void Delete(int id)
        {
            var haber = GetByID(id);
            if (haber!=null)
            {
                ctx.Haber.Remove(haber);
            }
        }

        public Haber Get(Expression<Func<Haber, bool>> kosul)
        {
            return ctx.Haber.FirstOrDefault(kosul);
        }

        public IEnumerable<Haber> GetAll()
        {
            return ctx.Haber.Select(x => x);
        }

        public Haber GetByID(int id)
        {
            return ctx.Haber.FirstOrDefault(x => x.Id==id);
        }

        public IQueryable<Haber> GetMany(Expression<Func<Haber, bool>> kosul)
        {
            return ctx.Haber.Where(kosul);
        }

        public void Insert(Haber obj)
        {
            ctx.Haber.Add(obj);
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        public void Update(Haber obj)
        {
            ctx.Haber.AddOrUpdate(obj);
        }
    }
}
