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
        AllProducts AllProducts_obj = new AllProducts();
        public User()
        {
            InitializeComponent();
            // add profile object
            UserWindow.Children.Insert(1,userprofile_obj);
            userprofile_obj.Visibility = Visibility.Hidden;
            // add all products object
            UserWindow.Children.Insert(1,AllProducts_obj);
            AllProducts_obj.Visibility = Visibility.Hidden;
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
            AllProducts_obj.Visibility = Visibility.Hidden;

            userprofile_obj.Visibility = Visibility.Visible;
        }

       // Cars

        private void Cars_btn_click(object sender, RoutedEventArgs e)
        {
            userprofile_obj.Visibility = Visibility.Hidden;
            AllProducts_obj.Visibility = Visibility.Visible;
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

        private void Add_Product(object sender, RoutedEventArgs e)
        {
            if (sqlcon.State == System.Data.ConnectionState.Closed)
                sqlcon.Open();
            if (Price.Text == "" || Speed.Text == "" || ExColour.Text == "" || TankCapacity.Text == "" || Model.Text == "" || Model.Text == "" || Warranty.Text == "" || InColour.Text == "")
            { MessageBox.Show("Please Fill All Details"); }
            else
            {
                string query = "INSERT INTO [Car](Price,Speed,ExtrerioColor,InteriorColor,TankCapacity,Model,Warranty,SellerID,carPartOrNot)" +
                        "values(@Price,@Speed,@ExtrerioColor,@InteriorColor,@TankCapacity,@Model,@Warranty,@SellerID,@carPartOrNot)";

                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@Price", Price.Text);
                sqlcmd.Parameters.AddWithValue("@Speed", Speed.Text);
                sqlcmd.Parameters.AddWithValue("@ExtrerioColor", ExColour.Text);
                sqlcmd.Parameters.AddWithValue("@InteriorColor", InColour.Text);
                sqlcmd.Parameters.AddWithValue("@TankCapacity", TankCapacity.Text);
                sqlcmd.Parameters.AddWithValue("@Model", Model.Text);
                sqlcmd.Parameters.AddWithValue("@Warranty", Warranty.Text);
                sqlcmd.Parameters.AddWithValue("@SellerID", Convert.ToString(GlobalVars.userid));
                sqlcmd.Parameters.AddWithValue("@carPartOrNot", 0);
                MessageBox.Show("Product Added Successfully");
                sqlcmd.ExecuteNonQuery();
                //  sqlcon.Close();
                Price.Text = ""; Speed.Text = ""; ExColour.Text = ""; InColour.Text = ""; TankCapacity.Text = ""; Model.Text = ""; Warranty.Text = "";
            }

        }
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
                        reader["Speed"].ToString(), reader["ExtrerioColor"].ToString(),
                        reader["InteriorColor"].ToString(), reader["TankCapacity"].ToString(),
                        reader["Model"].ToString(), reader["Warranty"].ToString(),
                        reader["FirstName"].ToString() + " " + reader["SecondName"].ToString()
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
      /*  private void My_Products(object sender, RoutedEventArgs e)
        {
            ProductsGrid.Visibility = Visibility.Hidden;
            var CarProducts = GetMyProducts();
            if (CarProducts.Count > 0)
                ListViewProducts.ItemsSource = CarProducts;
            ShowProducts.Visibility = Visibility.Visible;

        }*/

        private void Add_Products(object sender, RoutedEventArgs e)
        {
            AddProduct.Visibility = Visibility.Visible;

        }
    }
}
