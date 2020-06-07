using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KentKonsey.Dal
{
    public class Galeri
    {
        public int ID { get; set; }
        public string FotoUrl { get; set; }
        [NotMapped, DisplayName("Galeri"), DataType(DataType.Upload)]
        public HttpPostedFileBase Foto { get; set; }
    }
}