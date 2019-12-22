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
        public static string CCN;
        public static string CCV;
        public static bool confirm = false;

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

        //-----------------BESH ---------------------//

        public static void addNewUser(TextBox emailSignUpTxt, PasswordBox passwordSignUpTxt,
        TextBox firstNameTxt, TextBox secondNameTxt, TextBox phoneTxt, TextBox adressTxt,
         ComboBox restoringQuestion, TextBox QuestionAnswering, TextBox accNumtxt)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            string query = "insert into userdata(Email, UserPassword,  FirstName, SecondName, Phone, Address,Question, Answer, AccountNumber)" +
                    "values(@email, @password,  @FirstName, @SecondName, @Phone, @Address, @question, @ans, @accNum)";

            SqlCommand sqlcmd = new SqlCommand(query, con);
            sqlcmd.Parameters.AddWithValue("@email", emailSignUpTxt.Text);
            sqlcmd.Parameters.AddWithValue("@password", passwordSignUpTxt.Password);
            sqlcmd.Parameters.AddWithValue("@FirstName", firstNameTxt.Text);
            sqlcmd.Parameters.AddWithValue("@SecondName", secondNameTxt.Text);
            sqlcmd.Parameters.AddWithValue("@Phone", phoneTxt.Text);
            sqlcmd.Parameters.AddWithValue("@Address", adressTxt.Text);
            sqlcmd.Parameters.AddWithValue("@question", restoringQuestion.Text);
            sqlcmd.Parameters.AddWithValue("@ans", QuestionAnswering.Text);
            sqlcmd.Parameters.AddWithValue("@accNum", accNumtxt.Text);

            sqlcmd.ExecuteNonQuery();
        }

        public static string getUserDataByMail(string colname, string mail)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            string query = "select " + colname + " from UserData where Email=@mail"; 
            SqlCommand userdata = new SqlCommand(query, con);
            userdata.Parameters.AddWithValue("@mail", mail);
            return Convert.ToString(userdata.ExecuteScalar());
        }

        //----------------------------------------//


        public static byte[] GetImage(string table , string col ,string id)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            string query = "select Image from " + table + " where " + col + " = " + id;
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


        //----------------------------------------------CAR-----------------------------------------------------------------------
        public static  List<CarProduct> getCar_Products(string id)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            List<CarProduct> products = new List<CarProduct>();
            string query = "select * from Car  join UserData on Car.SellerID = UserData.UserID";
            if (id != "")
                query = query + " where Car.SellerID = " + id;
            else
                query = query + " where Car.SellerID != " + GlobalVars.userid.ToString();

            try
            {
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
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
                reader.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
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

        public static void deleteCar_Byid(string id)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            SqlCommand sqlcmd = new SqlCommand("exec deleteCar_byID " + id, con);
            sqlcmd.ExecuteNonQuery();

        }
        
        //-----------------------------------------------Parts-------------------------------------------------------------
        public static List<PartProduct> Get_CarParts(string id)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();

            List<PartProduct> products = new List<PartProduct>();

            string query = "select * from CarPart join UserData on CarPart.SellerID=UserData.UserID ";
            if (id != "")
                query =query + "where CarPart.SellerID = " + id;
            else
                query = query + "where CarPart.SellerID != " + GlobalVars.userid.ToString();
            try
            {
                SqlCommand  sqlcmd = new SqlCommand(query, con);
                SqlDataReader reader = sqlcmd.ExecuteReader();
                while(reader.Read())
                {
                    byte[] data = (byte[])reader["Image"];
                    products.Add(new PartProduct(data, reader["ProductID"].ToString(),reader["Price"].ToString(),reader["Color"].ToString()
                        ,reader["Warranty"].ToString(), reader["FirstName"].ToString() + " " + reader["SecondName"].ToString() ,
                         reader["Quantity"].ToString(),reader["Name"].ToString()));
                }
                reader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return products;
        }
        public static void Add_CarPart_Fn(byte[] PartImage, string bFCPPrice, string bFCPName, 
            string bFCPColour, string bFCPQuantity, string bFCPWarranty)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();

                string query = "INSERT INTO [CarPart](Image , Name,color,Warranty,Price,Quantity,SellerID,carPartOrNot)" +
                        "values(@Image , @Name,@Color,@Warranty,@Price,@Quantity,@SellerID,@carPartOrNot)";
               try
               {
                 SqlCommand sqlcmd = new SqlCommand(query, con);
                sqlcmd.Parameters.AddWithValue("@Image", PartImage);
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

        public static void Upate_CarPart(byte[] PartImage, string bFCPPrice, string bFCPName,
           string bFCPColour, string bFCPQuantity, string bFCPWarranty , string partid)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();

            string query = "Update [CarPart] set " +
                "Image = @Image , Name =@Name,color=@Color,Warranty=@Warranty,Price=@Price,Quantity=@Quantity where ProductID =" +
                "@PartID";
            try
            {
                SqlCommand sqlcmd = new SqlCommand(query, con);
                sqlcmd.Parameters.AddWithValue("@Image", PartImage);
                sqlcmd.Parameters.AddWithValue("@Name", bFCPName);
                sqlcmd.Parameters.AddWithValue("@Color", bFCPColour);
                sqlcmd.Parameters.AddWithValue("@Warranty", bFCPWarranty);
                sqlcmd.Parameters.AddWithValue("@Price", bFCPPrice);
                sqlcmd.Parameters.AddWithValue("@Quantity", bFCPQuantity);
                sqlcmd.Parameters.AddWithValue("@PartID", partid);
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

        public static void deleteCarPart_Byid(string id)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            SqlCommand sqlcmd = new SqlCommand("exec deleteCarPart_byID " + id, con);
            sqlcmd.ExecuteNonQuery();
        }
        //
        public static void MoveToCart(string quantity, string userid, int totalprice,
             string productid, string carorpart)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            string query = "exec MoveToCart " + quantity + "," + userid + "," +
                totalprice.ToString() + "," + productid + "," + carorpart;
            SqlCommand sqlcmd = new SqlCommand(query, con);
            sqlcmd.ExecuteNonQuery();
        }
        //-----------------------------------------------Cart-------------------------------------------------------------

        public static List<CartProducts> Get_CartProducts()
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            List<CartProducts> products = new List<CartProducts>();
            SqlCommand cmd = new SqlCommand("Select * from Cart inner join car on Cart.ProductID = Car.CarID where UserID =" + GlobalVars.userid + "and Cart.carPartOrNot=0 and IsConfirmed= 0", con);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    products.Add(new CartProducts(
                        reader["OrderID"].ToString(), reader["Name"].ToString(),
                        reader["Quantity"].ToString(), reader["Price"].ToString(), reader["TotalPrice"].ToString(),
                        reader["ProductID"].ToString(), reader["carPartOrNot"].ToString()));
                }
                reader.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            SqlCommand cmd1 = new SqlCommand("Select * from Cart  join CarPart on Cart.ProductID = CarPart.ProductID where UserID =" + GlobalVars.userid + "and Cart.carPartOrNot=1 and IsConfirmed= 0", con);
            try
            {
                SqlDataReader reader = cmd1.ExecuteReader();

                while (reader.Read())
                {

                    products.Add(new CartProducts(
                        reader["OrderID"].ToString(), reader["Name"].ToString(),
                        reader["Quantity"].ToString(), reader["Price"].ToString(), reader["TotalPrice"].ToString(),
                        reader["ProductID"].ToString(), reader["carPartOrNot"].ToString()));
                }
                reader.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return products;
        }
        public static void removeFromCart(string id, string carorpart)
        {
            try
            {
                MessageBox.Show(carorpart.ToString());
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                string query = "delete from Cart where ProductID = " + id + " and carPartOrNot = ";
                if (carorpart == "True")
                    query += "1";
                else
                    query += "0";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        public static List<CarProduct> viewFromCart(string id, string Carorpart)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            List<CarProduct> products = new List<CarProduct>();
            string query = "select * from Car  join UserData on Car.SellerID = UserData.UserID";
            if (id != "")
                query = query + " where Car.CarID = " + id;

            try
            {
                MessageBox.Show("here");
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
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
                        , reader["Name"].ToString()));
                }
                reader.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return products;
        }
        public static bool checkccnccv()
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            string query = "select * from Bank";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if ((reader["CardNumber"]).ToString() == CCN && (reader["CVV"]).ToString() == CCV)
                {
                    reader.Close();
                    return true;
                }

            }
            reader.Close();
            return false;

        }
        public static bool Check(string total_price)
        {
            if (total_price == null)
            {
                return false;
            }
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();

            try
            {
                string query = "select Credits from Bank where CardNumber= " + CCN + " AND CVV=" + CCV;
                SqlCommand cmd = new SqlCommand(query, con);
                string credits = cmd.ExecuteScalar().ToString();
                int tp = Convert.ToInt32(total_price);
                if (Convert.ToInt32(credits) > tp)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());

            }
            return false;

        }
        public static void IsConfirmed(string orderid)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            string query = "update Cart set IsConfirmed= 1 where OrderID = "+orderid ;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            
        }
        public static void UpdateProducts(string id, string Carorpart, string quantity)
        {
            try { 
            if (Carorpart == "False")
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                
                string query = "select Quantity from Car where CarID= " + id + "AND carPartOrNot=" + 0;
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                int newQuantity = Convert.ToInt32(reader["Quantity"]) - Convert.ToInt32(quantity);
                reader.Close();
                if (newQuantity == 0)
                {
                    if (con.State == System.Data.ConnectionState.Closed)
                        con.Open();
                    string query2 = "delete from Car where CarID = " + id;
                    SqlCommand cmd1 = new SqlCommand(query2, con);
                    cmd1.ExecuteNonQuery();
                    return;

                }
                else
                {
                    string query1 = "update Car set Quantity = " + Convert.ToString(newQuantity) + " where CarID = " + id;
                    SqlCommand updatecmd = new SqlCommand(query1, con);
                    updatecmd.ExecuteNonQuery();
                }
            }
            if (Carorpart == "True")
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                string query = "select Quantity from CarPart where ProductID= " + id + "AND carPartOrNot=" + 1;
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                int newQuantity = Convert.ToInt32(reader["Quantity"]) - Convert.ToInt32(quantity);
                reader.Close();
                if (newQuantity == 0)
                {
                    if (con.State == System.Data.ConnectionState.Closed)
                        con.Open();
                    string query2 = "delete from CarPart where ProductID = " + id;
                    SqlCommand cmd1 = new SqlCommand(query2, con);
                    cmd1.ExecuteNonQuery();
                    return;

                }
                else
                {
                    string query1 = "update CarPart set Quantity = " + Convert.ToString(newQuantity) + " where ProductID = " + id;
                    SqlCommand updatecmd = new SqlCommand(query1, con);
                    updatecmd.ExecuteNonQuery();
                }
            }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }


        }

        public static void AddtoPayment(string orderid,string name)
        {
            DateTime localDate = DateTime.Now;
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            try
            {
                string query = "select * from Cart where OrderID= " + orderid;
                SqlCommand cmd1 = new SqlCommand(query, con);
                SqlDataReader reader = cmd1.ExecuteReader();
                reader.Read();
                string Name = name;
                string sellerid = reader["UserID"].ToString();
                reader.Close();
                string query1 = "Insert into Payment (BuyerID,SellerID,Product_name,Date,orderID) values (@buyer,@seller,@name,@date,@order)";
                SqlCommand cmd = new SqlCommand(query1, con);

                cmd.Parameters.AddWithValue("@buyer", GlobalVars.userid.ToString());
                cmd.Parameters.AddWithValue("@seller", sellerid);
                cmd.Parameters.AddWithValue("@name", Name);
                cmd.Parameters.AddWithValue("@date", localDate);
                cmd.Parameters.AddWithValue("@order", orderid);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }

        }

        // feedback
        public static void sendfeedback(string Thename , string mail , string messages)
        {
            DateTime localDate = DateTime.Now;
            try
            {
                    if (con.State == System.Data.ConnectionState.Closed)
                        con.Open();

                    string query = "give_feedBack";
                    SqlCommand sqlcmd = new SqlCommand(query, con);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add(new SqlParameter("@name", Thename));
                    sqlcmd.Parameters.Add(new SqlParameter("@email", mail));
                    sqlcmd.Parameters.Add(new SqlParameter("@message", messages));
                    sqlcmd.Parameters.Add(new SqlParameter("@date_time", localDate));
                    sqlcmd.ExecuteNonQuery();

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
        public static void get_payment(DataTable dt)
        {
            try
            {
                dt.Columns.Add("Product Name");
                dt.Columns.Add("Seller Name");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Date");
                dt.Columns.Add("Price");
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                string query = "get_payment";
                SqlCommand sqlcmd = new SqlCommand(query, con);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@id", GlobalVars.userid);
                SqlDataReader rdr = sqlcmd.ExecuteReader();
                DataRow row;
                while (rdr.Read())
                {
                    row = dt.NewRow();
                    row["Product Name"] = rdr["Product_name"];
                    row["Seller Name"] = rdr["seller_name"];
                    row["Quantity"] = rdr["Quantity"];
                    row["Date"] = rdr["Date"];
                    row["price"] = rdr["TotalPrice"];
                    dt.Rows.Add(row);

                }
                rdr.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }
        public static void get_sales(DataTable dt)
        {
            try
            {
                dt.Columns.Add("buyer Name");
                dt.Columns.Add("Product Name");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Date");
                dt.Columns.Add("Price");
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                string query = "get_sales";
                SqlCommand sqlcmd = new SqlCommand(query, con);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@id", GlobalVars.userid);
                SqlDataReader rdr = sqlcmd.ExecuteReader();
                DataRow row;
                while (rdr.Read())
                {
                    row = dt.NewRow();
                    row["Product Name"] = rdr["Product_name"];
                    row["Buyer Name"] = rdr["buyer_name"];
                    row["Quantity"] = rdr["Quantity"];
                    row["Date"] = rdr["Date"];
                    row["price"] = rdr["TotalPrice"];
                    dt.Rows.Add(row);

                }
                rdr.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }
        public static bool get_admin(int id)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();

            string query = "select isAdmin from UserData where UserID=@id";
            SqlCommand sqlcmd = new SqlCommand(query, con);

            sqlcmd.Parameters.AddWithValue("@id", id);
            bool bannedOrNot = Convert.ToBoolean(sqlcmd.ExecuteScalar());
            return bannedOrNot;


        }
    }
}
