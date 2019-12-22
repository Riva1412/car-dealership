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
    /// Interaction logic for admin_control.xaml
    /// </summary>
    public partial class admin_control : UserControl
    {
        SqlConnection sqlcon = new SqlConnection(@"Data Source=(local);Initial Catalog=Cars_db;Integrated Security=SSPI");

        public admin_control()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sqlcon.State == System.Data.ConnectionState.Closed)
                sqlcon.Open();
            int id = Convert.ToInt32(get_id.Text);
            string query = "update UserData set isAdmin = 0 where UserID = @id";
            SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
            sqlcmd.Parameters.AddWithValue("@id", id);
            sqlcmd.ExecuteNonQuery();
            try
            {
             MainWindow wnd =(MainWindow)Application.Current.MainWindow;
            wnd.refresh_admins();
            }
          catch(Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            

        }
    }
}
