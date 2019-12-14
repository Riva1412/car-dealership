using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_project
{
    public class PartProduct
    {
        public byte[] Image { get; set; }
        public string PartID { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Color { get; set; }
        public string Warranty { get; set; }
        public string SellerName { get; set; }
        public string Quantity { get; set; }

        public PartProduct(byte[] image, String partid, String price , String color,
          String warranty, String sellername, string quantity, string name)
        {
            Image = image;
            PartID = partid;
            Price = price;
            Color = color;
            Warranty = warranty;
            SellerName = sellername;
            Quantity = quantity;
            Name = name;
        }
    }
}
