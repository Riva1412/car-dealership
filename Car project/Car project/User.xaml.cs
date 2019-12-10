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
            MyProducts.Visibility = Visibility.Visible;

            
            var CarProducts = DBManager.getAllProducts();
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
    }
}
