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
    public class KategoriRepository : IKategoriRepository
    {
        private readonly HaberContext ctx = new HaberContext();

        public int Count()
        {
          return   ctx.Kategori.Count();
        }

        public void Delete(int id)
        {
            var kategori = ctx.Kategori.Find(id);
            if (kategori!=null)
            {
                ctx.Kategori.Remove(kategori);
            }
        }

        public Kategori Get(Expression<Func<Kategori, bool>> kosul)
        {
            return ctx.Kategori.FirstOrDefault(kosul);
        }

        public IEnumerable<Kategori> GetAll()
        {
            return ctx.Kategori.Select(x => x);
        }

        public Kategori GetByID(int id)
        {
            return ctx.Kategori.Find(id);
        }

        public IQueryable<Kategori> GetMany(Expression<Func<Kategori, bool>> kosul)
        {
            return ctx.Kategori.Where(kosul);
        }

        public void Insert(Kategori obj)
        {
            ctx.Kategori.Add(obj);
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        public void Update(Kategori obj)
        {
            ctx.Kategori.AddOrUpdate(obj);
        }
    }
}
