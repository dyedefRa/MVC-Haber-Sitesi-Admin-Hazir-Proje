using HaberinMerkezi.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaberinMerkezi.Admin.App_Classes
{
    public class HaberEtiketModel
    {
        public Haber   Haber { get; set; }
        public string EtiketAdlari { get; set; }
        public List<Etiket> TumEtiketler { get; set; }
        public string [] EtiketDizisi { get; set; }
    }
}