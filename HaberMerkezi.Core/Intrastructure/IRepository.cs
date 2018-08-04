using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HaberinMerkezi.Core.Intrastructure
{
   public  interface IRepository<T> where T:class
    {
       
        IEnumerable<T> GetAll();
        T GetByID(int id);

        T Get(Expression<Func<T, bool>> kosul);
        IQueryable<T> GetMany(Expression<Func<T, bool>> kosul);
        void Insert(T obj);
        void Delete(int id);
        void Update(T obj);
        int Count();
        void Save();


    }
}
