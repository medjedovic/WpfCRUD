using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


/*
 Sistematizacija prvog dijela kursa iz C# WPF-a 
    CRUD - unos, prikaz, izmjena, brisanje i pretraga podataka za artikle, urađen. 
    CRUD za korisnike sa login provjerom ostala validacija sa FLUENTValidatorom da se imlementira u XAML-u
 
 */
namespace WpfCRUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //observable collection je mnogo bolji za komunikaciju i senzitivniji na promene u toj kolekciji
        //ObservableCollection<clsArtikal> lstArt = new ObservableCollection<clsArtikal>();

        clsEF db = new clsEF();

        private int _tSifra;
        public int tSifra 
        {
            get => _tSifra;
            set 
            {
                _tSifra = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("tSifra"));
            } 
        }
        private int _tKol;
        public int tKol 
        {
            get => _tKol;
            set
            {
                _tKol = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("tKol"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public clsRacun tRacun = new clsRacun();

        private string _pretraga { get; set; }
        public string pretraga 
        { 
            get => _pretraga;
            set
            {
                _pretraga = value;
                if (string.IsNullOrWhiteSpace(_pretraga))
                {
                    dgArtikli.ItemsSource = db.artikals.ToList();

                }
                else 
                { 
                    dgArtikli.ItemsSource =
                        db.artikals.Where(a => a.naziv.Contains(_pretraga.Trim())).ToList();
                }
            }
        }

        clsArtikal sa = null;

        public MainWindow()
        {
            InitializeComponent();
            //dgArtikli.ItemsSource = lstArt;
            DataContext = this;

            dgArtikli.ItemsSource = db.artikals.ToList();

            //snimamo bazu kako bi kreirao id racuna
            //db.SaveChanges();
            //foreach (clsArtikal a in db.artikals.ToList())
            //{
            //    db.artkols.Add(new clsArtKol(a, 2));
            //}
            //clsRacun r = new clsRacun();
            //db.racuni.Add(r);
            //db.artkols.ToList().ForEach(ak => r.listaArt.Add(ak));
            //db.SaveChanges();

            //local observable collection
            //dgArtikli.ItemsSource = db.artikals.Local;

            //dgStavke.ItemsSource = tRacun.listaArt;
            
            UnosRac.BindingGroup = new BindingGroup();
            db.racuni.ToList();
            dgRacuni.ItemsSource = db.racuni.Local;

        }

        private void btnA(object sender, RoutedEventArgs e)
        {
            WinAE ae = new WinAE();
            ae.Owner = this;

            //obzirom da bool nije nullabilan (ne može biti null) koristimo ? npr 
            //ae.ShowDialog() == true ae.ShowDialog().Value
            if (ae.ShowDialog() == true)
            {
                //lstArt.Add(ae.DataContext as clsArtikal);
                db.artikals.Add(ae.DataContext as clsArtikal);
                //snimi podatke koji su dodani
                db.SaveChanges();
                //učitaj ponovo u datageid listu artikala dataseta
                dgArtikli.ItemsSource = db.artikals.ToList();
            }
            else MessageBox.Show("Odustali ste od unosa podataka!");
        }

        private void btnE(object sender, RoutedEventArgs e)
        {
            if(sa != null)
            {
                WinAE ae = new WinAE();
                ae.Owner = this;
                ae.DataContext = dgArtikli.SelectedItem;
                ae.ShowDialog();
                db.SaveChanges();
                //dgArtikli.ItemsSource = db.artikals.ToList();
            }
        }

        private void btnD(object sender, RoutedEventArgs e)
        {
            if (sa != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li stvarno želiš da obrišeš selektovani podatak?", 
                                                            "Delete", 
                                                            MessageBoxButton.YesNo, 
                                                            MessageBoxImage.Question, 
                                                            MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    //lstArt.Remove(dgArtikli.SelectedItem as clsArtikal);
                    //kod asp.net-a
                    //db.artikals.Remove(db.artikals.Where(a => a.Equals(dgArtikli.SelectedItem)).FirstOrDefault());
                    db.artikals.Remove(sa);
                    db.SaveChanges();
                    dgArtikli.ItemsSource = db.artikals.ToList();
                }
            }
        }

        private void btnKorisnik(object sender, RoutedEventArgs e)
        {
            WinKAELogin ae = new WinKAELogin();
            ae.Owner = this;
            ae.ShowDialog();
        }

        private void dgArtikliSelektovano(object sender, SelectionChangedEventArgs e)
        {
            if (dgArtikli.SelectedItem != null)
            {
                sa = dgArtikli.SelectedItem as clsArtikal;
            }
            else
                sa = null;
        }

        private void btnUnosStavke(object sender, RoutedEventArgs e)
        {
            if (UnosRac.BindingGroup.CommitEdit())
            {
                var art = db.artikals.Find(tSifra);
                if (art != null)
                {
                    var ak = new clsArtKol(art, tKol);
                    tRacun.listaArt.Add(ak);

                    //db.artkols.Add(ak);
                    //db.racuni.Add(tRacun);
                    //db.SaveChanges();

                    dgStavke.ItemsSource = null;
                    dgStavke.ItemsSource = tRacun.listaArt;
                    tKol = 0;
                    tSifra = 0;
                    //tRacun = new clsRacun();
                }
                else
                {
                    MessageBox.Show("Artikal ne postoji!");
                }
            }
            else
            {
                MessageBox.Show("Proverite podatke!");
            }
        }

        private void btnIzdajRacun(object sender, RoutedEventArgs e)
        {
            tRacun.vrijemeIzdavanja = DateTime.Now;
            tRacun.listaArt.ForEach(ak => db.artkols.Add(ak));
            db.racuni.Add(tRacun);
            db.SaveChanges();
            tRacun = new clsRacun();
            dgStavke.ItemsSource = null;
        }
    }
}
