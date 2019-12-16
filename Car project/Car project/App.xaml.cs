using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Car_project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

    }
    public static class GlobalVars

    {
        public static int userid = 1;
    }
    public static class GlobalGrids
    {
        public static void updateviewitems()
        {
            allCar_obj.ListViewProducts.ItemsSource= DBManager.getCar_Products("");
            mycars_obj.ListViewProducts.ItemsSource = DBManager.getCar_Products(GlobalVars.userid.ToString());

            allparts_obj.ListViewProducts.ItemsSource = DBManager.Get_CarParts("");
            myparts_obj.ListViewProducts.ItemsSource  = DBManager.Get_CarParts(GlobalVars.userid.ToString());
        }

        //------------------------Profile---------------------------

        public static UserWindow_Classes.Profile.UserProfile userprofile_obj = new UserWindow_Classes.Profile.UserProfile();

        //------------------------Cars---------------------------

        public static UserWindow_Classes.Cars.AllCars.All_Car_Products allCar_obj = new UserWindow_Classes.Cars.AllCars.All_Car_Products();
        public static UserWindow_Classes.Cars.MyCars.MyCarsProducts mycars_obj = new UserWindow_Classes.Cars.MyCars.MyCarsProducts();
        public static UserWindow_Classes.Cars.Add_Car.Add_Car addcar_obj = new UserWindow_Classes.Cars.Add_Car.Add_Car();

        //------------------------CarParts---------------------------

        public static UserWindow_Classes.CarParts.All_Products.AllParts allparts_obj = new UserWindow_Classes.CarParts.All_Products.AllParts();
        public static UserWindow_Classes.CarParts.My_Products.MyPartsProducts myparts_obj = new UserWindow_Classes.CarParts.My_Products.MyPartsProducts();
        public static UserWindow_Classes.CarParts.Add_Product.AddCarPart addPart_obg = new UserWindow_Classes.CarParts.Add_Product.AddCarPart();

        //--------------------------------------------------------------------
        public static Cart cart_obj = new Cart();
       
    }
   
}
