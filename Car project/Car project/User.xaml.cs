using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
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
    public partial class User : Window
    {
        public User()
        {
            InitializeComponent();

            
        }

        private List<Product> GetProducts()
        {
            return new List<Product>()
            {
        new Product("/imgs/google.jpg"),
        new Product("/imgs/nissan.jpg"),
        new Product("/Assets/3.jpg"),
        new Product("/Assets/4.jpg"),
        new Product("/Assets/5.jpg"),
        new Product( "/Assets/6.jpg"),
        new Product( "/Assets/7.jpg"),
        new Product( "/Assets/8.jpg")
            };
        }
        SqlConnection sqlcon = new SqlConnection(@"Data Source=(local);Initial Catalog=Cars_db;Integrated Security=SSPI");

        private string getdata(string colname, int userid)
        {
            string query = "select " + colname + " from UserData where UserID=" +
                Convert.ToString(userid);
            SqlCommand userdata = new SqlCommand(query, sqlcon);
            return Convert.ToString(userdata.ExecuteScalar());
        }
        private void updatedata(string colname, string val, int userid)
        {
            string query = "update UserData set " + colname + " = '" + val + "' where UserID = " +
                Convert.ToString(userid);
            SqlCommand updatecmd = new SqlCommand(query, sqlcon);
            updatecmd.ExecuteNonQuery();

        }
        private void LogoutButtonPopUpMenu_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
            MyProducts.Visibility = Visibility.Hidden;
            MessageBox.Show("Home");

        }
        /// profile begin

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            MyProducts.Visibility = Visibility.Hidden;
            Profile.Visibility = Visibility.Visible;
            try
            {
                if (sqlcon.State == System.Data.ConnectionState.Closed)
                    sqlcon.Open();
                FirstNameText.Text = getdata("FirstName", GlobalVars.userid);
                SecondNameText.Text = getdata("SecondName", GlobalVars.userid);
                PhoneText.Text = getdata("Phone", GlobalVars.userid);
                EmailText.Text = getdata("Email", GlobalVars.userid);
                AddressText.Text = getdata("Address", GlobalVars.userid);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void savebtn_click(object sender, RoutedEventArgs e)
        {
            if (Newpass.Password.Length > 0 && Newpass.Password != ConfPass.Password)
            {
                MessageBox.Show("Password does not match");
                return;
            }
            if (PhoneText.Text.Length != 11)
            {
                MessageBox.Show("Invalid phone number");
                return;
            }
            try
            {
                if (sqlcon.State == System.Data.ConnectionState.Closed)
                    sqlcon.Open();
                updatedata("FirstName", FirstNameText.Text, GlobalVars.userid);
                updatedata("SecondName", SecondNameText.Text, GlobalVars.userid);
                updatedata("Email", EmailText.Text, GlobalVars.userid);
                updatedata("Phone", PhoneText.Text, GlobalVars.userid);
                updatedata("Address", AddressText.Text, GlobalVars.userid);
                if (Newpass.Password.Length > 0)
                    updatedata("UserPassword", Newpass.Password, GlobalVars.userid);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Profile.Visibility = Visibility.Hidden;
        }

        private void cancelbtn_click(object sender, RoutedEventArgs e)
        {
            Profile.Visibility = Visibility.Collapsed;
        }
        // profile end
        private void Products(object sender, RoutedEventArgs e)
        {
            Profile.Visibility = Visibility.Hidden;
            MyProducts.Visibility = Visibility.Visible;
            var products = GetProducts();
            if (products.Count > 0)
                ListViewProducts.ItemsSource = products;
        }
        private void Cart(object sender, RoutedEventArgs e)
        {
            MyProducts.Visibility = Visibility.Hidden;
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
    }
}
