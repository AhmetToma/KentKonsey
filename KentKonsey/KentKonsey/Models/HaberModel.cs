using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KentKonsey.Models
{
    public class HaberModel
    {
        public int id { get; set; }

        [Required]
        [StringLength(1250)]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string baslik { get; set; }
        [Required]
        [StringLength(10000)]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string aciklama { get; set; }

        public DateTime? tarih { get; set; }

        public string FotoUrl { get; set; }
        [NotMapped, DisplayName("Haber Fotoğrafı"), DataType(DataType.Upload)]
        public HttpPostedFileBase Foto { get; set; }

    }
}