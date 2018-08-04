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
 public   class RolRepository:IRolRepository
    {
        private readonly HaberContext ctx = new HaberContext();

        public int Count()
        {
            return ctx.Rol.Count();
        }

        public void Delete(int id)
        {
            var rsm = GetByID(id);
            if (rsm != null)
            {
                ctx.Rol.Remove(rsm);
            }
        }

        public Rol Get(Expression<Func<Rol, bool>> kosul)
        {
            return ctx.Rol.FirstOrDefault(kosul);
        }

        public IEnumerable<Rol> GetAll()
        {
            return ctx.Rol.Select(x => x);
        }

        public Rol GetByID(int id)
        {
            return ctx.Rol.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Rol> GetMany(Expression<Func<Rol, bool>> kosul)
        {
            return ctx.Rol.Where(kosul);
        }

        public void Insert(Rol obj)
        {
            ctx.Rol.Add(obj);
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        public void Update(Rol obj)
        {
            ctx.Rol.AddOrUpdate(obj);
        }
    }
}
