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
        
        public User()
        {

            InitializeComponent();
            if (GlobalVars.firsttime == 0)
            {
                GlobalVars.firsttime++;
                Login.MainWindow loginwinow = new Login.MainWindow();
                this.Close();
                loginwinow.Show();
            }
                
            purchase();
            sale();

            //------------------------Profile---------------------------

        GlobalGrids.userprofile_obj = new UserWindow_Classes.Profile.UserProfile();

            //------------------------Cars---------------------------

            GlobalGrids.allCar_obj = new UserWindow_Classes.Cars.AllCars.All_Car_Products();
            GlobalGrids.mycars_obj = new UserWindow_Classes.Cars.MyCars.MyCarsProducts();
            GlobalGrids.addcar_obj = new UserWindow_Classes.Cars.Add_Car.Add_Car();

            //------------------------CarParts---------------------------

            GlobalGrids.allparts_obj = new UserWindow_Classes.CarParts.All_Products.AllParts();
            GlobalGrids.myparts_obj = new UserWindow_Classes.CarParts.My_Products.MyPartsProducts();
            GlobalGrids.addPart_obg = new UserWindow_Classes.CarParts.Add_Product.AddCarPart();

            //--------------------------------------------------------------------
            GlobalGrids.cart_obj = new Cart();
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // add profile object
            UserWindow.Children.Insert(1, GlobalGrids.userprofile_obj);

            // add all cars object
            CarsGrid.Children.Add(GlobalGrids.allCar_obj);
            CarsGrid.Children.Add(GlobalGrids.mycars_obj);
            CarsGrid.Children.Add(GlobalGrids.addcar_obj);

            // add all car parts object
            PartsGrid.Children.Add(GlobalGrids.allparts_obj);
            PartsGrid.Children.Add(GlobalGrids.myparts_obj);
            PartsGrid.Children.Add(GlobalGrids.addPart_obg);

            // add cart
            UserWindow.Children.Insert(1, GlobalGrids.cart_obj);
            hideGrids();
            CarsGrid.Visibility = Visibility.Visible;

            GlobalGrids.allCar_obj.Visibility = Visibility.Visible;

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
        void hideGrids()
        {
            GlobalGrids.userprofile_obj.Visibility = Visibility.Hidden;

            CarsGrid.Visibility = Visibility.Hidden;
            GlobalGrids.allCar_obj.Visibility = Visibility.Hidden;
            GlobalGrids.mycars_obj.Visibility = Visibility.Hidden;
            GlobalGrids.addcar_obj.Visibility = Visibility.Hidden;

            PartsGrid.Visibility = Visibility.Hidden;
            GlobalGrids.allparts_obj.Visibility = Visibility.Hidden;
            GlobalGrids.myparts_obj.Visibility = Visibility.Hidden;
            GlobalGrids.addPart_obg.Visibility = Visibility.Hidden;

            GlobalGrids.cart_obj.Visibility = Visibility.Hidden;

            feedback.Visibility = Visibility.Hidden;
            payment.Visibility = Visibility.Hidden;
            GlobalGrids.cart_obj.Visibility = Visibility.Hidden;

        }

        //---------------------------------------------Top Menu---------------------------------------------------------

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

        //---------------------------------------------Left Menu-----------------------------------------------------

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

      

       

        //-------------------------------------------Profile---------------------------------------------------------

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            hideGrids();
            GlobalGrids.userprofile_obj.Visibility = Visibility.Visible;
        }

        //-----------------------------------------------Cars---------------------------------------------------------

        private void Cars_btn_click(object sender, RoutedEventArgs e)
        {
            hideGrids();
            CarsGrid.Visibility = Visibility.Visible;
            GlobalGrids.allCar_obj.Visibility = Visibility.Visible;
        }

        private void AllCars_Products(object sender, RoutedEventArgs e)
        {
            hideGrids();
            CarsGrid.Visibility = Visibility.Visible;
            GlobalGrids.allCar_obj.Visibility = Visibility.Visible;
        }

        private void MyCars_Products(object sender, RoutedEventArgs e)
        {
            hideGrids();
            CarsGrid.Visibility = Visibility.Visible;
            GlobalGrids.mycars_obj.Visibility = Visibility.Visible;
        }
        private void AddCar_Products(object sender, RoutedEventArgs e)
        {
            hideGrids();
            CarsGrid.Visibility = Visibility.Visible;
            GlobalGrids.addcar_obj.Visibility = Visibility.Visible;
        }

        //-------------------------------------------Car Parts---------------------------------------------------------

        private void CarsParts_btn_click(object sender, RoutedEventArgs e)
        {
            hideGrids();
            PartsGrid.Visibility = Visibility.Visible;
            GlobalGrids.allparts_obj.Visibility = Visibility.Visible;
        }
        private void AllParts_Products(object sender, RoutedEventArgs e)
        {
            hideGrids();
            PartsGrid.Visibility = Visibility.Visible;
            GlobalGrids.allparts_obj.Visibility = Visibility.Visible;
        }
        private void MyParts_Products(object sender, RoutedEventArgs e)
        {
            hideGrids();
            PartsGrid.Visibility = Visibility.Visible;
            GlobalGrids.myparts_obj.Visibility = Visibility.Visible;
        }

        private void AddPart_Products(object sender, RoutedEventArgs e)
        {
            hideGrids();
            PartsGrid.Visibility = Visibility.Visible;
            GlobalGrids.addPart_obg.Visibility = Visibility.Visible;
        }

        //---------------------------------------------------Cart---------------------------------------------------------

        private void Cart_click(object sender, RoutedEventArgs e)
        {
                hideGrids();
                GlobalGrids.cart_obj.Visibility = Visibility.Visible;
        }
        //---------------------------------------------------Feedback ---------------------------------------------------------

        private void write_feedback(object sender, RoutedEventArgs e)
        {
            hideGrids();
            feedback.Visibility = Visibility.Visible;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Thename = name_text.Text;
            string mail = email_text.Text;
            string messages = message.Text;
            if(Thename == ""  ||  mail == "" || messages == "")
            {
                MessageBox.Show("Please,fill in all textboxes.");
                return;
            }
            DBManager.sendfeedback(Thename, mail, messages);
            
            name_text.Text = "";  email_text.Text = "";     message.Text = "";
        }
        //---------------------------------------------------Payment--------------------------------------------------------
        void purchase()
        {
            DataTable dt = new DataTable();
            DBManager.get_payment(dt);
            purchase_table.ItemsSource = dt.DefaultView;
        }
        void sale()
        {
            DataTable dt = new DataTable();
            DBManager.get_sales(dt);
            sales_table.ItemsSource = dt.DefaultView;
        }

        private void PurchaseButton(object sender, RoutedEventArgs e)
        {
            purchase_table.Visibility = Visibility.Visible;
            sales_table.Visibility = Visibility.Collapsed;
        }

        private void Salebutton(object sender, RoutedEventArgs e)
        {
            purchase_table.Visibility = Visibility.Collapsed;
            sales_table.Visibility = Visibility.Visible;
        }


        //---------------------------------------------------log out---------------------------------------------------------

        private void Logout_clcik(object sender, RoutedEventArgs e)
        {
            Login.MainWindow loginwinow = new Login.MainWindow();
            this.Close();
            loginwinow.Show();
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            hideGrids();
            purchase();
            sale();
            payment.Visibility = Visibility.Visible;
        }
    }
}
