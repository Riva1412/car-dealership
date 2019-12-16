using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Car_project
{
    public class CartProducts
    {
        public String OrderID { get; set; }

        public string Name { get; set; }

        public string Quantity { get; set; }

        public string Price { get; set; }
        public string ProductID { get; set; }
        public string CarPartOrNot { get; set; }

        public String TotalPrice { get; set; }

        public CartProducts(String orderid, string name, string quantity, string price, String totalprice, string productid, string carpartornot)
        {
            OrderID = orderid;
            Name = name;
            Quantity = quantity;
            Price = price;
            ProductID = productid;
            CarPartOrNot = carpartornot;
            TotalPrice = totalprice;

        }

    }
}

