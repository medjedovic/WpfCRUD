using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRUD
{
    public class clsArtKol
    {
        public int id { get; set; }
        public clsArtikal art { get; set; }
        public int kolicina { get; set; }

        public decimal zbir { get => art.ICijena * kolicina;}
        public clsArtKol() { }
        public clsArtKol(clsArtikal a, int kol)
        {
            art = a;
            kolicina = kol;
        }
    }
}
