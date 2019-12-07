using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_project
{
    public class Product
    {
     ///   public string Name { get; set; }
     ///   public double Value { get; set; }
        public string Image { get; set; }

        public Product(string image)
        {
         ///   Name = name;
         ///   Value = value;
            Image = image;
        }
    }
}