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
    public partial class UserControl1 
    {
        SqlConnection sqlcon = new SqlConnection(@"Data Source=(local);Initial Catalog=Cars_db;Integrated Security=SSPI");
        public UserControl1()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sqlcon.State == System.Data.ConnectionState.Closed)
                sqlcon.Open();
            int id = Convert.ToInt32(get_id.Text);
            string query = "update UserData set banned = 1 where UserID = @id";
            SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
            sqlcmd.Parameters.AddWithValue("@id", id);
            banSign.Foreground =new SolidColorBrush(Colors.Red);
            sqlcmd.ExecuteNonQuery();
            MainWindow wnd = (MainWindow)Application.Current.MainWindow;
            wnd.refresh_all();

        }

        private void profile_Click(object sender, RoutedEventArgs e)
        {
            if (sqlcon.State == System.Data.ConnectionState.Closed)
                sqlcon.Open();
            int id = Convert.ToInt32(get_id.Text);
            string query = "update UserData set isAdmin = 1 where UserID = @id";
            SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
            sqlcmd.Parameters.AddWithValue("@id", id);
            banSign.Foreground = new SolidColorBrush(Colors.Red);
            sqlcmd.ExecuteNonQuery();
            
            try
            {
                MainWindow wnd = (MainWindow)Application.Current.MainWindow;
                wnd.refresh_all();
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
    }
}
