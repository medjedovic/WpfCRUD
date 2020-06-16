using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRUD
{
    public class clsArtikal : INotifyPropertyChanged
    {
        public int sifra { get; set; }
        
        private string _naziv;
        public string naziv 
        {
            get => _naziv;
            set 
            {
                _naziv = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("naziv"));
            } 
        }


        private decimal _ucijena;
        public decimal ucijena 
        { 
            get => _ucijena;
            set
            {
                _ucijena = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ucijena"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ICijena"));
            }
        }

        private int _marza;
        public int marza { 
            get => _marza;
            set
            {
                _marza = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("marza"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ICijena"));
            }
        }

        //property koji vraća izlaznu cijenu
        //obzirom da će ovde da vraća vrijednost celobrojnu treba se rezultat castovati
        public decimal ICijena { get => ucijena * (1 + (decimal)marza / 100); }
        
        public event PropertyChangedEventHandler PropertyChanged;

        //ova metoda daje istu stvar kao i property
        //public decimal di() => ucijena * (1 + (decimal)marza / 100);


        //override u ovom slučaju će da nam vrati identifikaciju artikla
        public override string ToString() => $"Šifra artikla:{sifra} sa nazivom: {naziv}";

        //ovaj override će nam vratiti da li su artikli isti u ovom slučaju da li im je id isti 
        public override bool Equals(object obj)
                                => obj is clsArtikal a && this.sifra == a.sifra && this.naziv == a.naziv;
        //{
        //obzirom da ovaj izraz vraća bool može se ovo napisati ovako 
        //a sve to uz pomoć lambda u gore navedenom redu
        //return obj is clsArtikal a && this.sifra == a.sifra;

        //if (obj is clsArtikal a && this.sifra == a.sifra)
        //    return true;
        //else
        //    return false;
        //}


        //has code unikatno vraća vrijednost određenog objekta
        public override int GetHashCode()
        {
            if (naziv != null) 
            {
                return sifra + naziv.GetHashCode();
            }
            else
            {
                return sifra;
            }
        }
        
    }
}
