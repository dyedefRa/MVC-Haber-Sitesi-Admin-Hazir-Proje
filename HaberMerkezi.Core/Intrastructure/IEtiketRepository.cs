﻿using HaberinMerkezi.Core.Intrastructure;
using HaberinMerkezi.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberinMerkezi.Core.Intrastructure
{
  public  interface IEtiketRepository:IRepository<Etiket>
    {
      
        void EtiketEkle(string etiketler, int eklenecekHaberID);
    }
}