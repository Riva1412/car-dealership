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
    /// Interaction logic for banned_user_control.xaml
    /// </summary>
    public partial class banned_user_control : UserControl
    {
        SqlConnection sqlcon = new SqlConnection(@"Data Source=(local);Initial Catalog=Cars_db;Integrated Security=SSPI");
        public class please_connect: listDesignModel
        {

        }
        public banned_user_control()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            if (sqlcon.State == System.Data.ConnectionState.Closed)
                sqlcon.Open();
            int id = Convert.ToInt32(get_id.Text);
            string query = "update UserData set banned = 0 where UserID = @id";
            SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
            sqlcmd.Parameters.AddWithValue("@id", id);
            sqlcmd.ExecuteNonQuery();
            undo.Foreground = new SolidColorBrush(Colors.Blue);
            listDesignModel ob = new listDesignModel();
           
          

           

           
        }
    }
    }

