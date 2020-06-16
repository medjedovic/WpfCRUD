using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfCRUD
{
    /// <summary>
    /// Interaction logic for WinKAELogin.xaml
    /// </summary>
    
    public partial class WinKAELogin : Window
    {
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
                    dgKorisnici.ItemsSource = db.korisnici.ToList();

                }
                else
                {
                    dgKorisnici.ItemsSource =
                    db.korisnici.Where(k => k.ime.Contains(_pretraga.Trim()) 
                                            || k.kime.Contains(_pretraga.Trim())
                                            ).ToList();
                }
            }
        }

        public WinKAELogin()
        {
            InitializeComponent();
            
            //To Do
            BindingGroup = new BindingGroup();
            
            //TO DO za pretragu malo bolje pojasniti
            //DataContext = this;
            txtPretraga.DataContext = this;
            
                       
            //postavljanje  u datacontext novog korisnika
            DataContext = new clsKorisnik();
            
            //smještanje korisnika iz baze u datagrid
            dgKorisnici.ItemsSource = db.korisnici.ToList();

            //za login
            txtKime.DataContext = this;
            txtLoz.DataContext = this;
        }

        private void btnAK(object sender, RoutedEventArgs e)
        {
            if (BindingGroup.CommitEdit())
            {
                db.korisnici.Add(DataContext as clsKorisnik);
                db.SaveChanges();

                DataContext = new clsKorisnik();
                dgKorisnici.ItemsSource = db.korisnici.ToList();
            }
            else 
                MessageBox.Show("Validacija nije dobra!");
            
        }

        private void btnEK(object sender, RoutedEventArgs e)
        {            
            if (dgKorisnici.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li stvarno želiš da izmijeniš selektovani podatak?", "Izmjena",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question,
                                          MessageBoxResult.Yes);//podrazumevano dugme

                if (result == MessageBoxResult.Yes)
                {
                    BindingGroup.CommitEdit();
                    db.SaveChanges();
                    dgKorisnici.ItemsSource = db.korisnici.ToList();
                }
                db.SaveChanges();
            }
        }

        private void btnDK(object sender, RoutedEventArgs e)
        {
            if (dgKorisnici.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li stvarno želiš da obrišeš selektovani podatak?", "Brisanje", 
                                          MessageBoxButton.YesNo, 
                                          MessageBoxImage.Question, 
                                          MessageBoxResult.Yes);//podrazumevano dugme

                if (result == MessageBoxResult.Yes)
                {
                    //db.korisnici.ToList();
                    db.korisnici.Remove(dgKorisnici.SelectedItem as clsKorisnik);
                    db.SaveChanges();
                    dgKorisnici.ItemsSource = db.korisnici.ToList();
                }
            }
        }

        private void dgSelected(object sender, SelectionChangedEventArgs e)
        {
            if (dgKorisnici.SelectedItem != null)
            {
                DataContext = dgKorisnici.SelectedItem;
            }
            
        }

        private void btnPrijava(object sender, RoutedEventArgs e)
        {
            //List<clsKorisnik> lstLogin = new List<clsKorisnik>();
            var l = db.korisnici.Where(k => k.kime.Contains(txtKime.Text.Trim())
                                                    && k.lozinka.Contains(txtLoz.Text.Trim())).FirstOrDefault();

            if (l != null)
            {
                MessageBox.Show($"Korisničko ime:{txtKime.Text.Trim()} sa lozinkom {txtLoz.Text.Trim()} POSTOJI u bazi");
            }
            else
            {
                MessageBox.Show($"Korisničko ime:{txtKime.Text.Trim()} sa lozinkom {txtLoz.Text.Trim()} NE POSTOJI u bazi");
            }
        }

    }
}
