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
    /// Interaction logic for WinAE.xaml
    /// </summary>
    public partial class WinAE : Window
    {
        public WinAE()
        {
            InitializeComponent();
            BindingGroup = new BindingGroup();
            DataContext = new clsArtikal();
        }

        

        private void btnOk(object sender, RoutedEventArgs e)
        {
            if (BindingGroup.CommitEdit())
            {
                DialogResult = true;
                Close();
            }
        }

        private void btnX(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
