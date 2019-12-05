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
        SqlConnection sqlcon = new SqlConnection(@"Data Source=(local);Initial Catalog=Cars_db;Integrated Security=SSPI");
        public User()
        {
            InitializeComponent();
        }
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
        /// profile begin
        private void savebtn_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlcon.State == System.Data.ConnectionState.Closed)
                    sqlcon.Open();
                updatedata("FirstName", FirstNameText.Text, GlobalVars.userid);
                updatedata("SecondName", SecondNameText.Text, GlobalVars.userid);
                updatedata("Email", EmailText.Text, GlobalVars.userid);
                updatedata("Phone", PhoneText.Text, GlobalVars.userid);
                updatedata("Address", AddressText.Text, GlobalVars.userid);
                if (Newpass.Password == ConfPass.Password)
                    updatedata("UserPassword", Newpass.Password, GlobalVars.userid);
                else
                    MessageBox.Show("Password does not match");
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
        /// profile end
        private void Products(object sender, RoutedEventArgs e)
        {
            
            MessageBox.Show("Products");
            MyProducts.Visibility = Visibility.Visible;
            for (int i = 0; i < 5; i++)
            {
                MaterialDesignThemes.Wpf.Flipper flip = new MaterialDesignThemes.Wpf.Flipper();
                flip = orgflipper;
                flip.Height=400;

            }
        }
        private void Cart(object sender, RoutedEventArgs e)
        {
            MyProducts.Visibility = Visibility.Hidden;
            MessageBox.Show("Cart");
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
