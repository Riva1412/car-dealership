using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
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
    public class DBManager
    {
        private SqlConnection con = new SqlConnection("Data Source=(.);Initial Catalog=Cars_db;Integrated Security=true");
        DBManager()
        {
            con.Open();
        }

        public string getUserData(string colname, int userid)
        {
            string query = "select " + colname + " from UserData where UserID=" +
                Convert.ToString(userid);
            SqlCommand userdata = new SqlCommand(query, con);
            return Convert.ToString(userdata.ExecuteScalar());
        }
        public void updateUserData(string colname, string val, int userid)
        {
            string query = "update UserData set " + colname + " = '" + val + "' where UserID = " +
                Convert.ToString(userid);
            SqlCommand updatecmd = new SqlCommand(query, con);
            updatecmd.ExecuteNonQuery();

        }

        private int check_Login(string email, string password)
        {
            try
            {
                string query = "select count(1) from UserData where Email=@usermail and UserPassword=@password";
                SqlCommand sqlcmd = new SqlCommand(query, con);
                sqlcmd.Parameters.AddWithValue("@usermail", email);
                sqlcmd.Parameters.AddWithValue("@password", password);
                return Convert.ToInt32(sqlcmd.ExecuteScalar());
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }

        public void signInBtn_Function(TextBox loginMail, PasswordBox loginPassword)
        {
            int count = check_Login(loginMail.Text, loginPassword.Password);
            if (count == 1)
            {
                string query = "select FirstName from UserData where Email=@usermail and UserPassword=@password";
                SqlCommand sqlcmd = new SqlCommand(query, con);

                sqlcmd = new SqlCommand(query, con);
                sqlcmd.Parameters.AddWithValue("@usermail", loginMail.Text);
                sqlcmd.Parameters.AddWithValue("@password", loginPassword.Password);
                string userName = Convert.ToString(sqlcmd.ExecuteScalar());
                MessageBox.Show("Welcome " + userName);
            }
            else MessageBox.Show("Wrong username or password");
        }
        public void addNewUser(TextBox emailSignUpTxt, PasswordBox passwordSignUpTxt, TextBox firstNameTxt, TextBox secondNameTxt, TextBox phoneTxt, TextBox adressTxt)
        {
            string query = "insert into userdata(Email, UserPassword,  FirstName, SecondName, Phone, Address)" +
                    "values(@email, @password,  @FirstName, @SecondName, @Phone, @Address)";

            SqlCommand sqlcmd = new SqlCommand(query, con);
            sqlcmd.Parameters.AddWithValue("@email", emailSignUpTxt.Text);
            sqlcmd.Parameters.AddWithValue("@password", passwordSignUpTxt.Password);
            sqlcmd.Parameters.AddWithValue("@FirstName", firstNameTxt.Text);
            sqlcmd.Parameters.AddWithValue("@SecondName", secondNameTxt.Text);
            sqlcmd.Parameters.AddWithValue("@Phone", phoneTxt.Text);
            sqlcmd.Parameters.AddWithValue("@Address", adressTxt.Text);

            sqlcmd.ExecuteNonQuery();
        }

        /// <summary>
        //producs Fns
        /// </summary>

        public  List<CarProduct> getAllProducts()
        {
            List<CarProduct> products = new List<CarProduct>();
            SqlCommand cmd = new SqlCommand("select * from Car", con);
            SqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    products.Add(new CarProduct((string)reader["Image"], 
                        (string)reader["CarID"], (string)reader["Price"], 
                        (string)reader["Speed"], (string)reader["ExtrerioColor"], 
                        (string)reader["InteriorColor"], (string)reader["TankCapacity"], 
                        (string)reader["Model"], (string)reader["Warranty"], 
                        (string)reader["SellerID"]));
                }
            }
            finally
            {
                MessageBox.Show("Done reading all ...");
                reader.Close();
            }

            return products;
        }

        ~DBManager()
        {
            con.Close();
        }
    }
}
