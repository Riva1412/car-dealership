using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Car_project
{
    public class CarProduct
    {
        public byte[] Image { get; set; }
        public string CarID { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Speed { get; set; }
        public string ExtreriorColor { get; set; }
        public string InteriorColor { get; set; }
        public string TankCapacity { get; set; }
        public string Model { get; set; }
        public string Warranty { get; set; }
        public string SellerName { get; set; }
        public string Quantity { get; set; }
        public CarProduct(byte[] image ,String carid, String price, String speed, 
         String extreriorcolor, String interiorcolor, String tankcapacity, 
         String model, String warranty, String sellername , string quantity , string name)
        {
            Image = image;
            CarID = carid;
            Price = price;
            Speed = speed;
            ExtreriorColor = extreriorcolor;
            InteriorColor = interiorcolor;
            TankCapacity = tankcapacity;
            Model = model;
            Warranty = warranty;
            SellerName = sellername;
            Quantity = quantity;
            Name = name; 
        }
    }
}