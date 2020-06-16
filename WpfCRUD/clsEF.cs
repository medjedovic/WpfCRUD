using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRUD
{
    public class clsEF: DbContext
    {
        public DbSet<clsArtikal> artikals { get; set; }
        
        public DbSet<clsKorisnik> korisnici { get; set; }


        //public clsEF() : base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dbCRUD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        //{ }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //definisanje primarnog ključa
            modelBuilder.Entity<clsArtikal>().HasKey(a => a.sifra);

            //definisanje indexa da je naziv obavezno polje
            //modelBuilder.Entity<clsArtikal>().HasRequired(a => a.naziv);
            //definisanje indexa da je naziv jedinstvena vrijednost
            //modelBuilder.Entity<clsArtikal>().HasIndex(a => a.naziv);

            //definisanje primarnog ključa
            modelBuilder.Entity<clsKorisnik>().HasKey(k => k.korisnik_id);

        }
    }
}
