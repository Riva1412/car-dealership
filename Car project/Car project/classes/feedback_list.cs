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
using System.Globalization;

namespace Car_project
{

    class feedback_list : listforUsers
    {
        SqlConnection sqlcon = new SqlConnection(@"Data Source=(local);Initial Catalog=Cars_db;Integrated Security=SSPI");
        public static feedback_list instance => new feedback_list();
        public feedback_list()
        {
            try
            {
                if (sqlcon.State == System.Data.ConnectionState.Closed)
                    sqlcon.Open();


                feedbacks = new List<feedback_contains>();

                string query = "read_all_feedbacks";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    var item = new feedback_contains();
                    string fn = (string)rdr["name"];
                    item.name = fn;


                    item.email = (string)rdr["email"];
                    item.message = (string)rdr["message"];
                    item.initials = fn[0].ToString().ToUpper();
                    item.feedback_time = rdr["date"].ToString() + " days ago.";

                    feedbacks.Add(item);

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
