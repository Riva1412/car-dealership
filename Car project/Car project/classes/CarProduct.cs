using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Car_project
{
    public class CarProduct
    {
        public string Image { get; set; }
        public string CarID { get; set; }
        public string Price { get; set; }
        public string Speed { get; set; }
        public string ExtrerioColor { get; set; }
        public string InteriorColor { get; set; }
        public string TankCapacity { get; set; }
        public string Model { get; set; }
        public string Warranty { get; set; }
        public string SellerID { get; set; }
        public CarProduct(string image ,String carid, String price, String speed, 
         String extreriocolor, String interiorcolor, String tankcapacity, 
         String model, String warranty, String sellerid)
        {
            Image = image;
            CarID = carid;
            Price = price;
            Speed = speed;
            ExtrerioColor = extreriocolor;
            InteriorColor = interiorcolor;
            TankCapacity = tankcapacity;
            Model = model;
            Warranty = warranty;
            SellerID = sellerid;
        }
    }
}