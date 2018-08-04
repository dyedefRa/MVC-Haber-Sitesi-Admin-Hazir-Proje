using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberinMerkezi.Data.Context
{
    [Table("Slider")]
   public class Slider {


        public Slider()
        {
            Aktif = true;
            EklenmeTarihi = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        [Display(Name ="Baslik")]
        [MinLength(3,ErrorMessage ="En az {0} karakter olabilir."),MaxLength(255,ErrorMessage ="{1} Karakterden Fazla Olamaz")]
        public string Baslik { get; set; }
        [Display(Name = "URL")]
        [MinLength(3, ErrorMessage = "En az {0} karakter olabilir."), MaxLength(255, ErrorMessage = "{1} Karakterden Fazla Olamaz")]
        public string URL { get; set; }
        [Display(Name = "Açıklama")]
        [MinLength(3, ErrorMessage = "En az {0} karakter olabilir."), MaxLength(255, ErrorMessage = "{1} Karakterden Fazla Olamaz")]
        public string Aciklama { get; set; }
        [Display(Name = "Resim")]
        [MinLength(3, ErrorMessage = "En az {0} karakter olabilir."), MaxLength(255, ErrorMessage = "{1} Karakterden Fazla Olamaz")]
        [Required]
        public string ResimURL { get; set; }

        public DateTime EklenmeTarihi { get; set; }

        public bool Aktif { get; set; }
    }
}
