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
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : UserControl
    {
        public Cart()
        {


            InitializeComponent();
            var cartproducts = DBManager.Get_CartProducts();
            if (cartproducts.Count() > 0)
            {


                ListViewProducts.ItemsSource = cartproducts;

            }
            else
                MessageBox.Show("No Products");


        }

        private void Validate_Click(object sender, RoutedEventArgs e)
        {
            DBManager.CCN = CCN.Text;
            DBManager.CCV = CCV.Text;
            if (DBManager.CCN == "" || DBManager.CCV == "")
            {
                MessageBox.Show("Please fill CCN and CVV Details");
                DBManager.confirm = false;
            }
            else
            {

                if (DBManager.checkccnccv() == false)
                {
                    MessageBox.Show("Please Enter valid CCN and CVV Details");
                    DBManager.confirm = false;
                }
                else
                {
                    MessageBox.Show("Correct One");
                    DBManager.confirm = true;
                }
            }
        }
    }
}
