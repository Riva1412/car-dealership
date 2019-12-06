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
using System.Text.RegularExpressions; // for regular expression in email


namespace Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection sqlcon = new SqlConnection(@"Data Source=(local);Initial Catalog=Cars_db;Integrated Security=SSPI");


        public MainWindow()
        {
            InitializeComponent();
            loginMail.Focus();
        }

        private void loginShowBtn_Click(object sender, RoutedEventArgs e)
        {
            loginMail.Focus();
            loginShowBtn.Foreground = Brushes.Black;
            loginShowBtn.Background = Brushes.White;
            signUpShowBtn.Background = (Brush)new BrushConverter().ConvertFrom("#FF9C27B0");
            signUpShowBtn.Foreground = Brushes.White;
            loginPanel.Visibility = Visibility.Visible;
            signUpPanel.Visibility = Visibility.Hidden;
        }

        private void signUpShowBtn_Click(object sender, RoutedEventArgs e)
        {
            emailSignUpTxt.Focus();
            signUpShowBtn.Foreground = Brushes.Black;
            signUpShowBtn.Background = Brushes.White;
            loginShowBtn.Background = (Brush)new BrushConverter().ConvertFrom("#FF9C27B0");
            loginShowBtn.Foreground = Brushes.White;
            loginPanel.Visibility = Visibility.Hidden;
            signUpPanel.Visibility = Visibility.Visible;
        }



        string userDataItem(string item)
        {
            try
            {
                if (sqlcon.State == System.Data.ConnectionState.Closed)
                    sqlcon.Open();

                string query = "select " + item + " from UserData where Email=@usermail and UserPassword=@password";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@usermail", loginMail.Text);
                sqlcmd.Parameters.AddWithValue("@password", loginPassword.Password);
                return Convert.ToString(sqlcmd.ExecuteScalar());
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return "";
        }


        private void signInBtn_Function()
        {
            int count = Convert.ToInt32(userDataItem("count(*)"));
            if (count == 1)
            {
                string userName = userDataItem("FirstName");
                MessageBox.Show("Welcome " + userName);

                // open user window //
                Car_project.GlobalVars.userid = Convert.ToInt32(userDataItem("userID"));
                Car_project.User userwindow = new Car_project.User();
                this.Close();
                userwindow.Show();
            }
            else MessageBox.Show("Wrong username or password");

            sqlcon.Close();
        }



        private void signUpBtn_Function()
        {
            if (emailSignUpTxt.Text == "" || firstNameTxt.Text == "" || secondNameTxt.Text == "" || adressTxt.Text == "" || phoneTxt.Text == "" || passwordSignUpTxt.Password == "" || confPasswordSignUpTxt.Password == "")
            {
                MessageBox.Show("You must fill in all fields");
                return;
            }

            if (passwordSignUpTxt.Password != confPasswordSignUpTxt.Password)
            {
                MessageBox.Show("Password does not match");
                return;
            }


            if (phoneTxt.Text.Length != 11)
            {
                MessageBox.Show("Invalid phone number");
                return;
            }

            // Regular expression
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            bool isValid = regex.IsMatch(emailSignUpTxt.Text.Trim());
            if (!isValid)
            {
                System.Windows.MessageBox.Show("Invalid Email.");
                return;
            }
            //------------------------------//


            int count = Convert.ToInt32(userDataItem("count(*)"));
            if (count == 1)
                MessageBox.Show("This account is already registered");

            else
            {
                string query = "insert into userdata(Email, UserPassword,  FirstName, SecondName, Phone, Address)" +
                    "values(@email, @password,  @FirstName, @SecondName, @Phone, @Address)";

                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@email", emailSignUpTxt.Text);
                sqlcmd.Parameters.AddWithValue("@password", passwordSignUpTxt.Password);
                sqlcmd.Parameters.AddWithValue("@FirstName", firstNameTxt.Text);
                sqlcmd.Parameters.AddWithValue("@SecondName", secondNameTxt.Text);
                sqlcmd.Parameters.AddWithValue("@Phone", phoneTxt.Text);
                sqlcmd.Parameters.AddWithValue("@Address", adressTxt.Text);

                sqlcmd.ExecuteNonQuery();
            }

            MessageBox.Show("You have registered successfully");
            sqlcon.Close();
        }


        private void signInBtn_Click(object sender, RoutedEventArgs e)
        {
            signInBtn_Function();
        }

        private void signInBtn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                signInBtn_Function();
            }
        }

        private void loginPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                signInBtn_Function();
            }

        }




        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            signUpBtn_Function();

        }
        private void signUpBtn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                signUpBtn_Function();
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
