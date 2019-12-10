﻿using System;
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
            if (sqlcon.State == System.Data.ConnectionState.Closed)
                sqlcon.Open();

        }
        SqlConnection sqlcon = new SqlConnection(@"Data Source=(local);Initial Catalog=Cars_db;Integrated Security=SSPI");

        private List<CarProduct> GetProducts()
        {
            List<CarProduct> products = new List<CarProduct>();
            SqlCommand cmd = new SqlCommand("select * from Car", sqlcon);
            SqlDataReader reader = cmd.ExecuteReader();
            try
            {

                while (reader.Read())
                {
                    products.Add(new CarProduct(reader["Image"].ToString(),
                        reader["CarID"].ToString(), reader["Price"].ToString(),
                        reader["Speed"].ToString(), reader["ExtrerioColor"].ToString(),
                        reader["InteriorColor"].ToString(), reader["TankCapacity"].ToString(),
                        reader["Model"].ToString(), reader["Warranty"].ToString(),
                        reader["SellerID"].ToString()));
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
        private void shownamedate()
        {
            FirstNameText.Text = getdata("FirstName", GlobalVars.userid);
            SecondNameText.Text = getdata("SecondName", GlobalVars.userid);
        }
        private void showinfodate()
        {
            PhoneText.Text = getdata("Phone", GlobalVars.userid);
            EmailText.Text = getdata("Email", GlobalVars.userid);
            AddressText.Text = getdata("Address", GlobalVars.userid);
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
            updatedata("FirstName", FirstNameText.Text, GlobalVars.userid);
            updatedata("SecondName", SecondNameText.Text, GlobalVars.userid);
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
            updatedata("UserPassword", Newpass.Password, GlobalVars.userid);
            Newpass.Password = ConfPass.Password= "";
            Nameexpander.IsExpanded = false;
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
            updatedata("Email", EmailText.Text, GlobalVars.userid);
            updatedata("Phone", PhoneText.Text, GlobalVars.userid);
            updatedata("Address", AddressText.Text, GlobalVars.userid);
            Nameexpander.IsExpanded = false;
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
            MyProducts.Visibility = Visibility.Visible;

            
            var CarProducts = GetProducts();
            if (CarProducts.Count > 0)
                ListViewProducts.ItemsSource = CarProducts;
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
            sqlcon.Close();
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
