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
using System.Windows.Shapes;

namespace Car_project
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loginShowBtn.Foreground = Brushes.Black;
            loginShowBtn.Background = Brushes.White;
            signUpShowBtn.Background = (Brush)new BrushConverter().ConvertFrom("#FF9C27B0");
            signUpShowBtn.Foreground = Brushes.White;
            loginPanel.Visibility = Visibility.Visible;
            signUpPanel.Visibility = Visibility.Hidden;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            signUpShowBtn.Foreground = Brushes.Black;
            signUpShowBtn.Background = Brushes.White;
            loginShowBtn.Background = (Brush)new BrushConverter().ConvertFrom("#FF9C27B0");
            loginShowBtn.Foreground = Brushes.White;
            loginPanel.Visibility = Visibility.Hidden;
            signUpPanel.Visibility = Visibility.Visible;
        }
    }
}
