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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class User 
    {
        public User()
        {
            InitializeComponent();
        }
        private void shownamedate()
        {
            FirstNameText.Text =  DBManager.getUserData("FirstName", GlobalVars.userid);
            SecondNameText.Text = DBManager.getUserData("SecondName", GlobalVars.userid);

        }
        private void showinfodate()
        {
            PhoneText.Text = DBManager.getUserData("Phone", GlobalVars.userid);
            EmailText.Text = DBManager.getUserData("Email", GlobalVars.userid);
            AddressText.Text = DBManager.getUserData("Address", GlobalVars.userid);
        }

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

        private void Home(object sender, RoutedEventArgs e)
        {
            ShowProducts.Visibility = Visibility.Hidden;
            MessageBox.Show("Home");

        }

        /// profile begin
        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            UserProfile x = new UserProfile();
            UserWindow.Children.Add(x);
            x.Visibility = Visibility.Hidden;
            ShowProducts.Visibility = Visibility.Hidden;
            Profile.Visibility = Visibility.Visible;
            try
            {
                shownamedate();
                showinfodate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
      
        }
        // name expander
        private void Nameconfirm_click(object sender, RoutedEventArgs e)
        {
            DBManager.updateUserData("FirstName", FirstNameText.Text, GlobalVars.userid);
            DBManager.updateUserData("SecondName", SecondNameText.Text, GlobalVars.userid);
            Nameexpander.IsExpanded = false;
        }
        private void Namecancel_click(object sender, RoutedEventArgs e)
        {
            Nameexpander.IsExpanded = false;
            shownamedate();
        }
        //-------------------------------
        // pass expander
        private void passconfirm_click(object sender, RoutedEventArgs e)
        {
            if (Newpass.Password.Length > 0 && Newpass.Password != ConfPass.Password)
            {
                MessageBox.Show("Password does not match");
                return;
            }
            if (Newpass.Password.Length==0)
            {
                MessageBox.Show("type at least one character");
                return;
            }
            DBManager.updateUserData("UserPassword", Newpass.Password, GlobalVars.userid);
            Newpass.Password = ConfPass.Password= "";
            passwordexpander.IsExpanded = false;
        }
        private void passcancel_click(object sender, RoutedEventArgs e)
        {
            passwordexpander.IsExpanded = false;
            Newpass.Password = ConfPass.Password = "";
        }
        //-------------------------------------------------------------
        // info expander
        private void infoconfrim_click(object sender, RoutedEventArgs e)
        {
            if (PhoneText.Text.Length != 11)
            {
                MessageBox.Show("Invalid phone number");
                return;
            }
            DBManager.updateUserData("Email", EmailText.Text, GlobalVars.userid);
            DBManager.updateUserData("Phone", PhoneText.Text, GlobalVars.userid);
            DBManager.updateUserData("Address", AddressText.Text, GlobalVars.userid);
            infoexpander.IsExpanded = false;
        }

        private void infocancel_click(object sender, RoutedEventArgs e)
        {
            infoexpander.IsExpanded = false;
            showinfodate();
        }
        //----------------------------
        // profile end
        private void Products(object sender, RoutedEventArgs e)
        {
            Profile.Visibility = Visibility.Hidden;
            ShowProducts.Visibility = Visibility.Visible;

            
            var CarProducts = DBManager.getAllProducts();
            if (CarProducts.Count > 0)
                ListViewProducts.ItemsSource = CarProducts;
            else
                MessageBox.Show("No Avaiable Products");
        }
        private void Cart(object sender, RoutedEventArgs e)
        {
            ShowProducts.Visibility = Visibility.Hidden;
            MessageBox.Show("Cart");
        }


        /// logout
        private void Logout(object sender, RoutedEventArgs e)
        {
            Login.MainWindow loginwinow = new Login.MainWindow();
            loginwinow.Show();
            this.Close();
        }

        /// close maxmisze minimize
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
        private void buy_click(object sender, RoutedEventArgs e)
        {

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
        private void My_Products(object sender, RoutedEventArgs e)
        {
            ProductsGrid.Visibility = Visibility.Hidden;
            var CarProducts = GetMyProducts();
            if (CarProducts.Count > 0)
                ListViewProducts.ItemsSource = CarProducts;
            ShowProducts.Visibility = Visibility.Visible;

        }

        private void Add_Products(object sender, RoutedEventArgs e)
        {
            ProductsGrid.Visibility = Visibility.Hidden;
            AddProduct.Visibility = Visibility.Visible;

        }

        private void All_Products(object sender, RoutedEventArgs e)
        {
            ProductsGrid.Visibility = Visibility.Hidden;
            var CarProducts =DBManager.getAllProducts();
            if (CarProducts.Count > 0)
                ListViewProducts.ItemsSource = CarProducts;
            ShowProducts.Visibility = Visibility.Visible;

        }
    }
}
