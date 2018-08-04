namespace HaberinMerkezi.Data.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Haber")]
    public partial class Haber
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Haber()
        {
            Resim = new HashSet<Resim>();
            HaberEtiket = new HashSet<HaberEtiket>();
            EklenmeTarihi = DateTime.Now;
            Aktif = true;
            OkunmaSayisi = 0;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Baslik { get; set; }

        [StringLength(250)]
        public string KisaAciklama { get; set; }

        public string Aciklama { get; set; }

        public int KullaniciID { get; set; }

        public int? OkunmaSayisi { get; set; }

        public int KategoriID { get; set; }

        public DateTime EklenmeTarihi { get; set; }

        public bool Aktif { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resim> Resim { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HaberEtiket> HaberEtiket { get; set; }
    }
}
