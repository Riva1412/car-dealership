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
using System.Globalization;

namespace Car_project
{


    public class model_for_each_one
    {

        public string name { get; set; }
        public string initials { get; set; }
        public string email { get; set; }

        public int button_id { get; set; }
        public bool isbanned { get; set; }
    }
    public class feedback_contains
    {

        public string name { get; set; }
        public string initials { get; set; }
        public string email { get; set; }
        public string message { get; set; }
        public string feedback_time { get; set; }



    }
}
