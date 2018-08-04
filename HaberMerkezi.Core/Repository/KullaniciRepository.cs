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
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly HaberContext ctx = new HaberContext();

        public int Count()
        {
            return ctx.Kullanici.Count();
        }

        public void Delete(int id)
        {
            var rsm = GetByID(id);
            if (rsm != null)
            {
                ctx.Kullanici.Remove(rsm);
            }
        }

        public Kullanici Get(Expression<Func<Kullanici, bool>> kosul)
        {
            return ctx.Kullanici.FirstOrDefault(kosul);
        }

        public IEnumerable<Kullanici> GetAll()
        {
            return ctx.Kullanici.Select(x => x);
        }

        public Kullanici GetByID(int id)
        {
            return ctx.Kullanici.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Kullanici> GetMany(Expression<Func<Kullanici, bool>> kosul)
        {
            return ctx.Kullanici.Where(kosul);
        }

        public void Insert(Kullanici obj)
        {
            ctx.Kullanici.Add(obj);
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        public void Update(Kullanici obj)
        {
            ctx.Kullanici.AddOrUpdate(obj);
        }
    }
}
