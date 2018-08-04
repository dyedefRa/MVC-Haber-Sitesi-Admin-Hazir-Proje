using HaberinMerkezi.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberinMerkezi.Core.Intrastructure
{
   public interface IHaberEtiketRepository:IRepository<HaberEtiket>
    {

        void HaberIDyeGoreSil(int id);
    }
}
