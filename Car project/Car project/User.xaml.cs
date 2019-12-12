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
        All_Car_Products AllCarsProducts_obj = new All_Car_Products();
        Add_Car add_car_obg = new Add_Car() ;
        public User()
        {
            InitializeComponent();
            // add profile object
            UserWindow.Children.Insert(1,userprofile_obj);
            userprofile_obj.Visibility = Visibility.Hidden;
            // add all cars object
            CarsGrid.Children.Add(AllCarsProducts_obj);
            AllCarsProducts_obj.Visibility = Visibility.Hidden;
            // add car
            CarsGrid.Children.Add(add_car_obg);
            add_car_obg.Visibility = Visibility.Hidden;
        }

        // left bar 
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

        // close and open menu

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

        // Home

        private void Home(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Home");
        }

        // Profile

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            CarsGrid.Visibility = Visibility.Hidden;
            userprofile_obj.Visibility = Visibility.Visible;
        }

       // Cars

        private void Cars_btn_click(object sender, RoutedEventArgs e)
        {
            userprofile_obj.Visibility = Visibility.Hidden;
            CarsGrid.Visibility = Visibility.Visible;
        }

        // Car Parts
        private void CarsParts_btn_click(object sender, RoutedEventArgs e)
        {

        }
        // Cart

        private void Cart_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cart");
        }

        // logout

        private void Logout_clcik(object sender, RoutedEventArgs e)
        {
            Login.MainWindow loginwinow = new Login.MainWindow();
            this.Close();
            loginwinow.Show();
        }


        SqlConnection sqlcon = new SqlConnection(@"Data Source=(local);Initial Catalog=Cars_db;Integrated Security=SSPI");

       
        private List<CarProduct> GetMyProducts()
        {
            List<CarProduct> products = new List<CarProduct>();
            SqlCommand cmd = new SqlCommand("select * from Car left join UserData on Car.SellerID=" +
                "UserData.UserID" +
                " where SellerID=" + Convert.ToString(GlobalVars.userid), sqlcon);
            SqlDataReader reader = cmd.ExecuteReader();
            try
            {

                while (reader.Read())
                {
                    byte[] data = (byte[])reader["Image"];
                    products.Add(new CarProduct(data,
                        reader["CarID"].ToString(), reader["Price"].ToString(),
                        reader["Speed"].ToString(), reader["ExtreriorColor"].ToString(),
                        reader["InteriorColor"].ToString(), reader["TankCapacity"].ToString(),
                        reader["Model"].ToString(), reader["Warranty"].ToString(),
                        reader["FirstName"].ToString() + " " + reader["SecondName"].ToString(), reader["Quantity"].ToString()
                        ));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                MessageBox.Show("Done reading all ...");
                reader.Close();
            }

            return products;
        }

        private void AllCars_Products(object sender, RoutedEventArgs e)
        {
            add_car_obg.Visibility = Visibility.Hidden;
            AllCarsProducts_obj.Visibility = Visibility.Visible;
        }

        private void MyCars_Products(object sender, RoutedEventArgs e)
        {

        }

        private void AddCar_Products(object sender, RoutedEventArgs e)
        {
            AllCarsProducts_obj.Visibility = Visibility.Hidden;
            add_car_obg.Visibility = Visibility.Visible;
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
