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
    public class ResimRepository : IResimRepository
    {
        private readonly HaberContext ctx = new HaberContext();

        public int Count()
        {
            return ctx.Resim.Count();
        }

        public void Delete(int id)
        {
            var rsm = GetByID(id);
            if (rsm != null)
            {
                ctx.Resim.Remove(rsm);
            }
        }

        public Resim Get(Expression<Func<Resim, bool>> kosul)
        {
            return ctx.Resim.FirstOrDefault(kosul);
        }

        public IEnumerable<Resim> GetAll()
        {
            return ctx.Resim.Select(x => x);
        }

        public Resim GetByID(int id)
        {
            return ctx.Resim.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Resim> GetMany(Expression<Func<Resim, bool>> kosul)
        {
            return ctx.Resim.Where(kosul);
        }

        public void Insert(Resim obj)
        {
            ctx.Resim.Add(obj);
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        public void Update(Resim obj)
        {
            ctx.Resim.AddOrUpdate(obj);
        }
    }
}
