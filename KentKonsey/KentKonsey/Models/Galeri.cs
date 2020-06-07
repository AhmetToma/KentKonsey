using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KentKonsey.Models
{
    public class Galeri
    {
        public int Id { get; set; }
        public string ResimUrl { get; set; }
        public string Ad { get; set; }
        public bool Durum { get; set; }

    }
}