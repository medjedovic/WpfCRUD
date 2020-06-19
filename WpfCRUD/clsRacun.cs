using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRUD
{
    public class clsRacun
    {

        public int rbr { get; set; }

        public DateTime vrijemeIzdavanja { get; set; } = DateTime.Now;

        //public List<clsArtKol> listaArt = new List<clsArtKol>();
        public List<clsArtKol> listaArt { get; set; } = new List<clsArtKol>();

        public decimal total { get => listaArt.Sum(a => a.zbir); }

    }
}
