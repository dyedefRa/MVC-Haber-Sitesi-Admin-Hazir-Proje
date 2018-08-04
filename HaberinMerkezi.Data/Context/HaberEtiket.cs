namespace HaberinMerkezi.Data.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HaberEtiket")]
    public partial class HaberEtiket
    {
        public HaberEtiket()
        {
            Aktif = true;
            
        }
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HaberID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EtiketID { get; set; }

        public bool? Aktif { get; set; }

      

        public virtual Etiket Etiket { get; set; }

        public virtual Haber Haber { get; set; }
    }
}
