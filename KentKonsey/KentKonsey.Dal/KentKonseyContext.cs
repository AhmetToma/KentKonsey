using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KentKonsey.Dal
{
    public class KentKonseyContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public KentKonseyContext()
        {
            Database.Connection.ConnectionString = "server=.;database=KentKonsey;Trusted_Connection=true";

        }

        public DbSet<Haber> Haber { get; set; }
        public DbSet<Galeri> Galeri { get; set; }

    }
}