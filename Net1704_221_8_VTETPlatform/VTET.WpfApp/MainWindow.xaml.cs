using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VTET.Business;
using VTET.Data.Models;
using VTET.WpfApp.UI;

namespace VTET.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

       
        private async void Open_wEvaluation_Click(object sender, RoutedEventArgs e)
        {
            var p = new wEvaluation();
            p.Owner = this;
            p.Show();
        }


     /*   private async void Open_wOrder_Click(object sender, RoutedEventArgs e)
        {
            var p = new wOrder();
            p.Owner = this;
            p.Show();
        }
*/
        private async void Open_wWatch_Click(object sender, RoutedEventArgs e)
        {
            var p = new wWatch();
            p.Owner = this;
            p.Show();
        }
    }
}