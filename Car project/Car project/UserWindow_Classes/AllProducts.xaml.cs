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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Car_project
{
    /// <summary>
    /// Interaction logic for AllProducts.xaml
    /// </summary>
    public partial class AllProducts : UserControl
    {
        public AllProducts()
        {
            InitializeComponent();
            var CarProducts = DBManager.getAllProducts();
            if (CarProducts.Count > 0)
                ListViewProducts.ItemsSource = CarProducts;
            else
                MessageBox.Show("No Avaiable Products");
        }
       private void buy_click(object sender, RoutedEventArgs e)
        {

        }
    }
}
