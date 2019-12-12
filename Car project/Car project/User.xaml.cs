using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Car_project
{
    public partial class User : Window
    {
        UserProfile userprofile_obj = new UserProfile();

        public User()
        {
            InitializeComponent();
            // add profile object
            UserWindow.Children.Insert(1,userprofile_obj);
            userprofile_obj.Visibility = Visibility.Hidden;
            // add all cars object
            CarsGrid.Children.Add(new All_Car_Products());
        }
        void hideGrids()
        {
            userprofile_obj.Visibility = Visibility.Hidden;
            CarsGrid.Visibility = Visibility.Hidden;
            PartsGrid.Visibility = Visibility.Hidden;
        }

        //---------------------------------------------Top Menu---------------------------------------------------------

        private void Buttonclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        //---------------------------------------------Left Menu-----------------------------------------------------

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;


        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        //---------------------------------------------Home---------------------------------------------------------

        private void Home(object sender, RoutedEventArgs e)
        {
            hideGrids();
            MessageBox.Show("Home");
        }

        //-------------------------------------------Profile---------------------------------------------------------

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            hideGrids();
            userprofile_obj.Visibility = Visibility.Visible;
        }

        //-----------------------------------------------Cars---------------------------------------------------------

        private void Cars_btn_click(object sender, RoutedEventArgs e)
        {
            hideGrids();
            CarsGrid.Visibility = Visibility.Visible;
        }
        private void AllCars_Products(object sender, RoutedEventArgs e)
        {
            CarsGrid.Children.RemoveAt(1);
            CarsGrid.Children.Add (new All_Car_Products());
        }

        private void MyCars_Products(object sender, RoutedEventArgs e)
        {
           
        }
        private void AddCar_Products(object sender, RoutedEventArgs e)
        {
            CarsGrid.Children.RemoveAt(1);
            CarsGrid.Children.Add(new Add_Car());
        }

        //-------------------------------------------Car Parts---------------------------------------------------------

        private void CarsParts_btn_click(object sender, RoutedEventArgs e)
        {
            hideGrids();
            PartsGrid.Visibility = Visibility.Visible;
        }
        private void AllParts_Products(object sender, RoutedEventArgs e)
        {

        }
        private void MyParts_Products(object sender, RoutedEventArgs e)
        {

        }

        private void AddPart_Products(object sender, RoutedEventArgs e)
        {

        }

        //---------------------------------------------------Cart---------------------------------------------------------

        private void Cart_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cart");
        }

        //---------------------------------------------------log out---------------------------------------------------------

        private void Logout_clcik(object sender, RoutedEventArgs e)
        {
            Login.MainWindow loginwinow = new Login.MainWindow();
            this.Close();
            loginwinow.Show();
        }


        /*  private void My_Products(object sender, RoutedEventArgs e)
{
ProductsGrid.Visibility = Visibility.Hidden;
var CarProducts = GetMyProducts();
if (CarProducts.Count > 0)
ListViewProducts.ItemsSource = CarProducts;
ShowProducts.Visibility = Visibility.Visible;

}*/

    }
}
