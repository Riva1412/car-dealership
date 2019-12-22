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
    public partial class CartCard : UserControl
    {
        public CartCard()
        {
            InitializeComponent();


        }
        private void remove_click(object sender, RoutedEventArgs e)
        {
            string Carorpart = carpartornot.Text;
            string id = ProductID.Text;
            DBManager.removeFromCart(id, Carorpart);
            GlobalGrids.updateviewitems();

        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            //string Carorpart = carpartornot.Text;
            //string id = ProductID.Text;
            //    var CarProducts = DBManager.getCar_Products("");

            //DBManager.viewFromCart(id, Carorpart);
        }

        private void Confirm_click(object sender, RoutedEventArgs e)
        {
            if (DBManager.CCN == "" || DBManager.CCV == "")
            {
                MessageBox.Show("Please fill CCN and CVV Details");
                return;
            }
            string Carorpart = carpartornot.Text;
            string id = ProductID.Text;
            string quantity = Quantity.Text;
            string orderid = Orderid.Text;
            string total_price = Total_Price.Text;
            string name = Name.Text;
            if (DBManager.confirm)
            {
                if (DBManager.Check(total_price))
                {

                    MessageBox.Show("you can buy it :)");
                    DBManager.UpdateProducts(id, Carorpart, quantity);
                    DBManager.AddtoPayment(orderid, name);
                    DBManager.IsConfirmed( orderid);
                    GlobalGrids.updateviewitems();

                }
                else { MessageBox.Show("you can't buy it :("); }


            }
            else
            {
                MessageBox.Show("Sorry, Can't confirm. Make sure you wrote Your CCN and CVV Correctly. ");
            }

        }
    }
}
