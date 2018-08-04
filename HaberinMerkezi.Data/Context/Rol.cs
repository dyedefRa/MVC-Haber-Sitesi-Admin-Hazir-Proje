namespace HaberinMerkezi.Data.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rol")]
    public partial class Rol
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rol()
        {
            Kullanici = new HashSet<Kullanici>();
            EklenmeTarihi = DateTime.Now;
            Aktif = true;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string RolAdi { get; set; }

        public DateTime EklenmeTarihi { get; set; }

        public bool Aktif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kullanici> Kullanici { get; set; }
    }
}
