namespace HaberinMerkezi.Data.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Resim")]
    public partial class Resim
    {
        public Resim()
        {
            EklenmeTarihi = DateTime.Now;
            VitrinMi = false;
            Aktif = true;
        }
        public int Id { get; set; }

        public int HaberID { get; set; }

        [Required]
        [StringLength(250)]
        public string ResimYol { get; set; }

        public DateTime EklenmeTarihi { get; set; }

        public bool Aktif { get; set; }

        public bool? VitrinMi { get; set; }

        public virtual Haber Haber { get; set; }
    }
}
