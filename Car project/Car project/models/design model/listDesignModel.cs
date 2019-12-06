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
using System.Data;

namespace Car_project
{
   public partial class listDesignModel:listforUsers
    {
        SqlConnection sqlcon = new SqlConnection(@"Data Source=(local);Initial Catalog=Cars_db;Integrated Security=SSPI");
        //getter
        
        public static listDesignModel instance=>new listDesignModel();
        //constructor
        public listDesignModel()
        {
            try
            {
                if (sqlcon.State == System.Data.ConnectionState.Closed)
                    sqlcon.Open();


                items = new List<model_for_each_one>();
                BannedItems = new List<model_for_each_one>();
                string query = "get_data";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    var item = new model_for_each_one();
                    string fn = (string)rdr["FirstName"], sn = (string)rdr["SecondName"];
                    item.name = fn + ' ' + sn;
                   

                    item.email = (string)rdr["Email"];
                    item.button_id = (int)rdr["UserID"];
                    item.initials = fn[0].ToString().ToUpper() + sn[0].ToString().ToUpper();
                    item.isbanned = (bool)rdr["banned"];
                    if (item.isbanned)
                    {
                        BannedItems.Add(item);
                        
                        
                    }

                    else
                    {
                        items.Add(item);

                    }
                     
                }



                rdr.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                sqlcon.Close();
            }





        }
        




    }

}
