using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRUD
{
    public class clsKorisnik
    {
        public int korisnik_id { get; set; }
        public string ime { get; set; }
        public string jmbg { get; set; }
        public string email { get; set; }
        public uint god { get; set; }
        public string prezime { get; set; }
        public string kime { get; set; }
        public string lozinka { get; set; }



        //override u ovom slučaju će da nam vrati identifikaciju objekta klase
        public override string ToString() => $"Ime korisnika:{ime} sa korisničkim imenom: {kime}";

        //ovaj override će nam vratiti da li su kimena isti u ovom slučaju da li im je korisnik_id isti 
        public override bool Equals(object obj)
                                => obj is clsKorisnik k && this.korisnik_id == k.korisnik_id && this.kime == k.kime;

        
        //has code unikatno vraća vrijednost određenog objekta
        public override int GetHashCode()
        {
            if (kime != null)
            {
                return korisnik_id + kime.GetHashCode();
            }
            else
            {
                return korisnik_id;
            }
        }



    }
}
