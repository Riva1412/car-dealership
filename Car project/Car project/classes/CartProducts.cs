using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Car_project
{
    public class CartProducts
    {
        public String OrderID { get; set; }

        public string Name { get; set; }

        public string Quantity { get; set; }

        public string Price { get; set; }

        //public Button View { get; set; }
        //public String Remove { get; set; }
        //public CheckBox Check { get; set; }
        public CartProducts(String orderid,string name,string quantity,string price)
        {
            OrderID = orderid;
            Name = name;
            Quantity = quantity;
            Price = price;
        }
    }
}
    
