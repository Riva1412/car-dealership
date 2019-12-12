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
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : UserControl
    {
        public UserProfile()
        {
            InitializeComponent();
            shownamedate();
            showinfodate();
        }
        private void shownamedate()
        {
            FirstNameText.Text = DBManager.getUserData("FirstName", GlobalVars.userid);
            SecondNameText.Text = DBManager.getUserData("SecondName", GlobalVars.userid);

        }
        private void showinfodate()
        {
            PhoneText.Text = DBManager.getUserData("Phone", GlobalVars.userid);
            EmailText.Text = DBManager.getUserData("Email", GlobalVars.userid);
            AddressText.Text = DBManager.getUserData("Address", GlobalVars.userid);
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
            if (Newpass.Password.Length == 0)
            {
                MessageBox.Show("type at least one character");
                return;
            }
            DBManager.updateUserData("UserPassword", Newpass.Password, GlobalVars.userid);
            Newpass.Password = ConfPass.Password = "";
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
    }
}
