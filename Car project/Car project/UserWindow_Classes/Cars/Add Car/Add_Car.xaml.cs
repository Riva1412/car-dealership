using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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

namespace Car_project
{
    /// <summary>
    /// Interaction logic for Add_Car.xaml
    /// </summary>
    public partial class Add_Car : UserControl
    {
        public Add_Car()
        {
            InitializeComponent();
        }
        public byte[] CarImage { get; set; }
        private void AddImage_click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();
            if (dlg.FileName == "")
                return;
            FileStream fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            ImageSourceConverter imgs = new ImageSourceConverter();
            imagebox.SetValue(Image.SourceProperty, imgs.
            ConvertFromString(dlg.FileName.ToString()));
            CarImage = data;
        }
        private void Add_Product(object sender, RoutedEventArgs e)
        {
            if (CarImage==null || CarImage.Length==0 || Price.Text == "" || Speed.Text == "" || ExColour.Text == "" || TankCapacity.Text == "" 
                || Model.Text == "" || Model.Text == "" || Warranty.Text == "" || InColour.Text == "" 
                || quantity.Text=="" || CarName.Text=="")
            {
                MessageBox.Show("Please Fill All Details");
                return;
            }
            DBManager.AddCarProduct(CarImage, Price.Text, Speed.Text, ExColour.Text, 
                InColour.Text, TankCapacity.Text, Model.Text, Warranty.Text, quantity.Text, CarName.Text);
           quantity.Text= Price.Text = Speed.Text = ExColour.Text =  InColour.Text = TankCapacity.Text =  Model.Text =  Warranty.Text = "";
        }
    }
}
