using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Text.RegularExpressions; 

namespace Car_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        
        public MainWindow()
        {
            InitializeComponent();
            
         
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

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            ban_page.Visibility = Visibility.Collapsed;
            feedback.Visibility = Visibility.Collapsed;

        }

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Helllo");
            ban_page.Visibility = Visibility.Collapsed;
            feedback.Visibility = Visibility.Collapsed;

        }

        private void ListViewItem_Selected_2(object sender, RoutedEventArgs e)
        {
            ban_page.Visibility = Visibility.Collapsed;
            feedback.Visibility = Visibility.Collapsed;
            MessageBox.Show("Helllo");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
      

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            all_users.Visibility = Visibility.Visible;
            banned_users.Visibility = Visibility.Collapsed;
           
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            all_users.Visibility = Visibility.Collapsed;
            banned_users.Visibility = Visibility.Visible;
            

        }

        private void ListViewItem_Selected_3(object sender, RoutedEventArgs e)
        {
            ban_page.Visibility = Visibility.Visible;
            feedback.Visibility = Visibility.Collapsed;


        }

        private void get_name_Click(object sender, RoutedEventArgs e)
        {
          
           

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

        private void ListViewItem_Selected_4(object sender, RoutedEventArgs e)
        {
            ban_page.Visibility = Visibility.Collapsed;
            feedback.Visibility = Visibility.Visible;
        }
    }
}
