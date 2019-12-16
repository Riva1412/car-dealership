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

namespace Car_project.UserWindow_Classes.CarParts.All_Products
{
    /// <summary>
    /// Interaction logic for AllParts.xaml
    /// </summary>
    public partial class AllParts : UserControl
    {
        public AllParts()
        {
            InitializeComponent();
            var products =  DBManager.Get_CarParts("");
            if (products.Count() > 0)
                ListViewProducts.ItemsSource = products;
            else
                MessageBox.Show("No Avaiable Products");
        }
    }
}
