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
    public class HaberEtiketRepository : IHaberEtiketRepository
    {
        private readonly HaberContext ctx = new HaberContext();

        public void HaberIDyeGoreSil(int id)
        {
            var dataList = ctx.HaberEtiket.Where(x => x.HaberID == id);
            if (dataList!=null)
            {
                foreach (var haberet in dataList)
                {
                    ctx.HaberEtiket.Remove(haberet);
                }
            }
        }


        public int Count()
        {
            return ctx.HaberEtiket.Count();
        }

        public void Delete(int id)
        {
            var hbretiket = ctx.HaberEtiket.Find(id);
            if (hbretiket != null)
            {
                ctx.HaberEtiket.Remove(hbretiket);
            }
        }

        public HaberEtiket Get(Expression<Func<HaberEtiket, bool>> kosul)
        {
            return ctx.HaberEtiket.FirstOrDefault(kosul);
        }

        public IEnumerable<HaberEtiket> GetAll()
        {
            return ctx.HaberEtiket.Select(x => x);
        }

        public HaberEtiket GetByID(int id)
        {
            return ctx.HaberEtiket.Find(id);
        }

        public IQueryable<HaberEtiket> GetMany(Expression<Func<HaberEtiket, bool>> kosul)
        {
            return ctx.HaberEtiket.Where(kosul);
        }

        public void Insert(HaberEtiket obj)
        {
            ctx.HaberEtiket.Add(obj);
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        public void Update(HaberEtiket obj)
        {
            ctx.HaberEtiket.AddOrUpdate(obj);
        }
    }
}
