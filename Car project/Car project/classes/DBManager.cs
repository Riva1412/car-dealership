using System;
using System.IO;
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
    public static class DBManager
    {
        public static SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=Cars_db;Integrated Security=SSPI");


        public static string getUserData(string colname, int userid)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            string query = "select " + colname + " from UserData where UserID=" +
                Convert.ToString(userid);
            SqlCommand userdata = new SqlCommand(query, con);
            return Convert.ToString(userdata.ExecuteScalar());
        }
        public static byte[] GetCarImage(string carid)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            string query = "select Image from Car where CarID=" +
                Convert.ToString(carid);
            SqlCommand carimg = new SqlCommand(query, con);
            return (byte[])carimg.ExecuteScalar();
        }
        public static void updateUserData(string colname, string val, int userid)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            string query = "update UserData set " + colname + " = '" + val + "' where UserID = " +
                Convert.ToString(userid);
            SqlCommand updatecmd = new SqlCommand(query, con);
            updatecmd.ExecuteNonQuery();
        }

        public static int check_Login(string email, string password)
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
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

        public static void signInBtn_Function(TextBox loginMail, PasswordBox loginPassword)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
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
        public static void addNewUser(TextBox emailSignUpTxt, PasswordBox passwordSignUpTxt, TextBox firstNameTxt, TextBox secondNameTxt, TextBox phoneTxt, TextBox adressTxt)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
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

        public static  List<CarProduct> getAllCar_Products()
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            List<CarProduct> products = new List<CarProduct>();
            SqlCommand cmd = new SqlCommand("select * from Car left join UserData on Car.SellerID=" +
                "UserData.UserID", con);
            SqlDataReader reader = cmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    byte[] data = (byte[])reader["Image"];
                    /*
                     UPDATE Car 
                     SET [Image] = (SELECT MyImage.* from Openrowset(Bulk 'C:\Delete\B.bmp', Single_Blob) MyImage) 
                     where CarID = 1 
                     */
                    products.Add(new CarProduct(data,
                        reader["CarID"].ToString(), reader["Price"].ToString(),
                        reader["Speed"].ToString(), reader["ExtreriorColor"].ToString(),
                        reader["InteriorColor"].ToString(), reader["TankCapacity"].ToString(),
                        reader["Model"].ToString(), reader["Warranty"].ToString(),
                         reader["FirstName"].ToString() + " " + reader["SecondName"].ToString(), reader["Quantity"].ToString()
                        , reader["Name"].ToString() ));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                reader.Close();
            }

            return products;
        }
        // Get my Cars Products
        public static List<CarProduct> GetMyCarsProducts()
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            List<CarProduct> products = new List<CarProduct>();

            SqlCommand cmd = new SqlCommand("select * from Car left join UserData on Car.SellerID=" +
                "UserData.UserID" +
                " where SellerID=" + Convert.ToString(GlobalVars.userid), con);
            SqlDataReader reader = cmd.ExecuteReader();
            try
            {

                while (reader.Read())
                {
                    byte[] data = (byte[])reader["Image"];
                    products.Add(new CarProduct(data,
                        reader["CarID"].ToString(), reader["Price"].ToString(),
                        reader["Speed"].ToString(), reader["ExtreriorColor"].ToString(),
                        reader["InteriorColor"].ToString(), reader["TankCapacity"].ToString(),
                        reader["Model"].ToString(), reader["Warranty"].ToString(),
                        reader["FirstName"].ToString() + " " + reader["SecondName"].ToString(), reader["Quantity"].ToString()
                       , reader["Name"].ToString() ));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                reader.Close();
            }
            return products;
        }

        public static void AddCarProduct(byte[] image , string Price , string Speed ,string ExColour , string InColour , string TankCapacity ,
            string Model , string Warranty , string quantity , string carname)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();


                string query = "INSERT INTO [Car]" +
                "(Name , Quantity , Image , Price,Speed,ExtreriorColor,InteriorColor,TankCapacity,Model,Warranty,SellerID,carPartOrNot)" +
                 "values(@carname , @Quantity , @image , @Price,@Speed,@ExtreriorColor,@InteriorColor,@TankCapacity,@Model,@Warranty,@SellerID,@carPartOrNot)";
                try
                {
                SqlCommand sqlcmd = new SqlCommand(query, con);
                sqlcmd.Parameters.AddWithValue("@carname", carname);
                sqlcmd.Parameters.AddWithValue("@Quantity", quantity);
                sqlcmd.Parameters.AddWithValue("@image", image);
                sqlcmd.Parameters.AddWithValue("@Price", Price);
                sqlcmd.Parameters.AddWithValue("@Speed", Speed);
                sqlcmd.Parameters.AddWithValue("@ExtreriorColor", ExColour);
                sqlcmd.Parameters.AddWithValue("@InteriorColor", InColour);
                sqlcmd.Parameters.AddWithValue("@TankCapacity", TankCapacity);
                sqlcmd.Parameters.AddWithValue("@Model", Model);
                sqlcmd.Parameters.AddWithValue("@Warranty", Warranty);
                sqlcmd.Parameters.AddWithValue("@SellerID", Convert.ToString(GlobalVars.userid));
                sqlcmd.Parameters.AddWithValue("@carPartOrNot", 0);
                sqlcmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                MessageBox.Show(e.ToString());
                }
                finally
                {
                    MessageBox.Show("Product Added Successfully");
                }
        }

        public static void UpdateCarProduct(byte[] image, string Price, string Speed, string ExColour, string InColour, string TankCapacity,
    string Model, string Warranty, string quantity ,string carname , string CarID)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();

            string query = "update  [Car]" +
            " set Image=@image , Name = @carname , Quantity = @Quantity , Price = @Price , Speed =  @Speed ," +
            "ExtreriorColor = @ExtreriorColor ,InteriorColor = @InteriorColor ,TankCapacity = @TankCapacity , " +
            "Model = @Model ,Warranty = @Warranty ,SellerID = @SellerID ,carPartOrNot = @carPartOrNot  " +
             "where CarId= @CarID";
            try
            {
                SqlCommand sqlcmd = new SqlCommand(query, con);
                sqlcmd.Parameters.AddWithValue("@carname", carname);
                sqlcmd.Parameters.AddWithValue("@CarID", CarID);
                sqlcmd.Parameters.AddWithValue("@Quantity", quantity);
                sqlcmd.Parameters.AddWithValue("@image", image);
                sqlcmd.Parameters.AddWithValue("@Price", Price);
                sqlcmd.Parameters.AddWithValue("@Speed", Speed);
                sqlcmd.Parameters.AddWithValue("@ExtreriorColor", ExColour);
                sqlcmd.Parameters.AddWithValue("@InteriorColor", InColour);
                sqlcmd.Parameters.AddWithValue("@TankCapacity", TankCapacity);
                sqlcmd.Parameters.AddWithValue("@Model", Model);
                sqlcmd.Parameters.AddWithValue("@Warranty", Warranty);
                sqlcmd.Parameters.AddWithValue("@SellerID", Convert.ToString(GlobalVars.userid));
                sqlcmd.Parameters.AddWithValue("@carPartOrNot", 0);
                sqlcmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                MessageBox.Show("Product Updated Successfully");
            }
        }

        public static void Add_CarPart_Fn(string bFCPPrice, string bFCPName, string bFCPColour, string bFCPQuantity, string bFCPWarranty)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();


                string query = "INSERT INTO [CarPart](Name,color,Warranty,Price,Quantity,SellerID,carPartOrNot)" +
                        "values(@Name,@Color,@Warranty,@Price,@Quantity,@SellerID,@carPartOrNot)";

               try
               {
                 SqlCommand sqlcmd = new SqlCommand(query, con);
                 sqlcmd.Parameters.AddWithValue("@Name", bFCPName);
                 sqlcmd.Parameters.AddWithValue("@Color", bFCPColour);
                 sqlcmd.Parameters.AddWithValue("@Warranty", bFCPWarranty);
                 sqlcmd.Parameters.AddWithValue("@Price", bFCPPrice);
                 sqlcmd.Parameters.AddWithValue("@Quantity", bFCPQuantity);
                 sqlcmd.Parameters.AddWithValue("@SellerID", Convert.ToString(GlobalVars.userid));
                 sqlcmd.Parameters.AddWithValue("@carPartOrNot", 1);
                 sqlcmd.ExecuteNonQuery();
               }
               catch (Exception e)
               {
                MessageBox.Show(e.ToString());
               }
               finally
               {
                MessageBox.Show("Product Added Successfully");
               }

        }

    }
}
