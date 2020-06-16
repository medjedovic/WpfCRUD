using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class MainWindow : Window
    {
        //observable collection je mnogo bolji za komunikaciju i senzitivniji na promene u toj kolekciji
        //ObservableCollection<clsArtikal> lstArt = new ObservableCollection<clsArtikal>();

        clsEF db = new clsEF();

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

        public MainWindow()
        {
            InitializeComponent();
            //dgArtikli.ItemsSource = lstArt;

            dgArtikli.ItemsSource = db.artikals.ToList();

            //local observable collection
            //dgArtikli.ItemsSource = db.artikals.Local;

            DataContext = this;
            
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
            if(dgArtikli.SelectedItem != null)
            {
                WinAE ae = new WinAE();
                ae.Owner = this;
                ae.DataContext = dgArtikli.SelectedItem;
                ae.ShowDialog();
                db.SaveChanges();
                dgArtikli.ItemsSource = db.artikals.ToList();
            }
        }

        private void btnD(object sender, RoutedEventArgs e)
        {
            if (dgArtikli.SelectedItem != null)
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
                    db.artikals.Remove(dgArtikli.SelectedItem as clsArtikal);
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
    }
    }
