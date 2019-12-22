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
using Car_project;

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
            signUpShowBtn.Background = (Brush)new BrushConverter().ConvertFrom("#9E9E9E");
            signUpShowBtn.Foreground = Brushes.White;
            loginPanel.Visibility = Visibility.Visible;
            signUpPanel.Visibility = Visibility.Hidden;
            restorPassPanel.Visibility = Visibility.Hidden;
        }

        private void signUpShowBtn_Click(object sender, RoutedEventArgs e)
        {
            emailSignUpTxt.Focus();
            signUpShowBtn.Foreground = Brushes.Black;
            signUpShowBtn.Background = Brushes.White;
            loginShowBtn.Background = (Brush)new BrushConverter().ConvertFrom("#9E9E9E");
            loginShowBtn.Foreground = Brushes.White;
            loginPanel.Visibility = Visibility.Hidden;
            signUpPanel.Visibility = Visibility.Visible;
            restorPassPanel.Visibility = Visibility.Hidden;
        }


         bool check_SignUp_Constraints()
        {
            if (emailSignUpTxt.Text == "" || firstNameTxt.Text == "" || secondNameTxt.Text == "" || adressTxt.Text == "" ||
                phoneTxt.Text == "" || passwordSignUpTxt.Password == "" || confPasswordSignUpTxt.Password == ""
                || (restoringQuestion.SelectedIndex > -1 && QuestionAnswering.Text == ""))
            {
                MessageBox.Show("You must fill in all fields");
                return false;
            }
            if (passwordSignUpTxt.Password.Length < 8)  //check password length
            {
                MessageBox.Show("Password Minimum is 8 Characters");

                return false;
            }

            

            if (passwordSignUpTxt.Password != confPasswordSignUpTxt.Password)
            {
                MessageBox.Show("Password does not match");

                return false;
            }


            if (!Regex.IsMatch(phoneTxt.Text, @"^([0-9]{11})$")) // Validate Phone Number
            {

                MessageBox.Show("Invalid phone number");
                return false;
            }

            if (!Regex.IsMatch(accNumtxt.Text, @"^([0-9]{14})$")&& accNumtxt.Text!="") // Validate account Number
            {

                MessageBox.Show("Invalid account number");
                return false;
            }


            //Regular expression
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            bool isValid = regex.IsMatch(emailSignUpTxt.Text.Trim());
            if (!isValid)
            {
                System.Windows.MessageBox.Show("Invalid Email.");
                return false;
            }
            return true;
        }






        private void signInBtn_Function()
        {
            int count = DBManager.check_Login(loginMail.Text, loginPassword.Password);
            if (count == 1)
            {
                try 
                {
                    string ban = DBManager.getUserDataByMail("banned", loginMail.Text);
                    if (ban == "True")
                    {
                        MessageBox.Show("You are banned :(");
                        return;
                    }
                    GlobalVars.userid = Convert.ToInt32(DBManager.getUserDataByMail("UserID", loginMail.Text));

                    if (DBManager.getUserDataByMail("isAdmin", loginMail.Text) == "True")

                    {
                        Car_project.MainWindow admin = new Car_project.MainWindow();
                        this.Close();
                        admin.Show();
                        return;
                    }

                    Car_project.User userwindow = new Car_project.User();
                    this.Close();
                    userwindow.Show();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            else MessageBox.Show("Wrong username or password");

            sqlcon.Close();
        }



        private void signUpBtn_Function()
        {

            if (!check_SignUp_Constraints()) {
                return;
            }


            int count = DBManager.check_Login(emailSignUpTxt.Text, passwordSignUpTxt.Password);
            if (count == 1)
            { MessageBox.Show("This account is already registered"); return; }

            else
                DBManager.addNewUser(emailSignUpTxt, passwordSignUpTxt, firstNameTxt, secondNameTxt,
                                         phoneTxt, adressTxt, restoringQuestion, QuestionAnswering, accNumtxt);

            MessageBox.Show("You have registered successfully");
            //Empty The Signup After Success Registration
            emailSignUpTxt.Text = ""; passwordSignUpTxt.Password = ""; firstNameTxt.Text = ""; secondNameTxt.Text = ""; phoneTxt.Text = ""; adressTxt.Text = "";

            //Return to Login
            loginMail.Focus();
            loginShowBtn.Foreground = Brushes.Black;
            loginShowBtn.Background = Brushes.White;
            signUpShowBtn.Background = (Brush)new BrushConverter().ConvertFrom("#9E9E9E");
            signUpShowBtn.Foreground = Brushes.White;
            loginPanel.Visibility = Visibility.Visible;
            signUpPanel.Visibility = Visibility.Hidden;
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

        private void loginMail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void forgotPassword_Click(object sender, RoutedEventArgs e)
        {

            if (loginMail.Text == "")
            { MessageBox.Show("Please, insert your mail first"); return; }

            Questiontxt.Text = DBManager.getUserDataByMail("Question", loginMail.Text);

            loginPanel.Visibility = Visibility.Hidden;
            signUpPanel.Visibility = Visibility.Hidden;
            restorPassPanel.Visibility = Visibility.Visible;
        }

        private void confirmbtn_Click(object sender, RoutedEventArgs e)
        {
            if (answertxtToRestore.Text == "" || newPass.Password == "")
            { MessageBox.Show("You must fill in all fields"); return; }

            string answer = DBManager.getUserDataByMail("Answer", loginMail.Text);

            if (answer != answertxtToRestore.Text || answer == "" || Questiontxt.Text == "") { MessageBox.Show("Wrong answer"); return; }



            if (sqlcon.State == System.Data.ConnectionState.Closed)
                sqlcon.Open();
            string query = "update userdata set UserPassword=@pass where Email=@usermail";
            SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
            sqlcmd.Parameters.AddWithValue("@pass", newPass.Password);
            sqlcmd.Parameters.AddWithValue("@usermail", loginMail.Text);
            sqlcmd.ExecuteScalar();
            MessageBox.Show("Your password was updated with successfully");

            answertxtToRestore.Text = newPass.Password = "";
            loginPanel.Visibility = Visibility.Visible;
            restorPassPanel.Visibility = Visibility.Hidden;

            sqlcon.Close();

        }

        private void Backbtn_Click(object sender, RoutedEventArgs e)
        {
            answertxtToRestore.Text = newPass.Password = "";
            loginPanel.Visibility = Visibility.Visible;
            restorPassPanel.Visibility = Visibility.Hidden;
        }
    }
}