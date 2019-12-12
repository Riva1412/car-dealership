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
    /// Interaction logic for AddCarPart.xaml
    /// </summary>
    public partial class AddCarPart : UserControl
    {
        public AddCarPart()
        {
            InitializeComponent();
        }
        private void Update_Flipper(object sender, RoutedEventArgs e)
        {
            fFCPName.Text = bFCPName.Text;
            fFCPPrice.Text = bFCPPrice.Text;
        }
        private void Add_CarPart(object sender, RoutedEventArgs e)
        {
            DBManager.Add_CarPart_Fn(bFCPPrice.Text, bFCPName.Text, bFCPColour.Text, bFCPQuantity.Text, bFCPWarranty.Text);
            bFCPPrice.Text = ""; bFCPColour.Text = ""; bFCPQuantity.Text = ""; bFCPWarranty.Text = "";
        }
    }
}
