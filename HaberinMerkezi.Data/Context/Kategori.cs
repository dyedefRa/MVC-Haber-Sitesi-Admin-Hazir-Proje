namespace HaberinMerkezi.Data.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kategori")]
    public partial class Kategori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kategori()
        {
            Haber = new HashSet<Haber>();
            EklenmeTarihi = DateTime.Now;
            Aktif = true;
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Adi { get; set; }

        public int? ParentID { get; set; }

        [StringLength(150)]
        public string URL { get; set; }

        public int KullaniciID { get; set; }

        public DateTime EklenmeTarihi { get; set; }

        public bool Aktif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Haber> Haber { get; set; }
        public virtual Kullanici Kullanici { get; set; }
    }
}
