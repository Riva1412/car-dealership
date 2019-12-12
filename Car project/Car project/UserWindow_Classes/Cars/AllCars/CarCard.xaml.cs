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
    /// Interaction logic for CarCard.xaml
    /// </summary>
    public partial class CarCard : UserControl
    {
        public CarCard()
        {
            InitializeComponent();
        }
        private void Add_to_Cart_click(object sender, RoutedEventArgs e)
        {
            int av = Convert.ToInt32(available_quantity.Text);
            int re = Convert.ToInt32(required_quantity.Text);

            if (av<re)
            {
                MessageBox.Show("Not Enough amounts");
                return;
            }
        }
    }
}
