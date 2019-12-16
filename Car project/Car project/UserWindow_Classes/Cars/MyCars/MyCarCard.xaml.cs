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

namespace Car_project.UserWindow_Classes.Cars.MyCars
{
    /// <summary>
    /// Interaction logic for MyCarCard.xaml
    /// </summary>
    public partial class MyCarCard : UserControl
    {
        public byte[] CarImage { get; set; }
        public MyCarCard()
        {
            InitializeComponent();
        }
        private void EditImage_click(object sender, RoutedEventArgs e)
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
        private void Done_click(object sender, RoutedEventArgs e)
        {
            if(CarImage==null || CarImage.Length==0)
                CarImage = DBManager.GetImage("Car","CarID",Carid.Text);
            if ( Price.Text == "" || Speed.Text == "" || ExColour.Text == "" || TankCapacity.Text == ""
            || Model.Text == "" || Model.Text == "" || Warranty.Text == "" || InColour.Text == "" 
            || quantity.Text == "" || CarName.Text=="")
            {
                MessageBox.Show("Please Fill All Details");
                return;
            }
            DBManager.UpdateCarProduct(CarImage , Price.Text, Speed.Text, ExColour.Text,
                InColour.Text, TankCapacity.Text, Model.Text, Warranty.Text, quantity.Text ,CarName.Text, Carid.Text);
        }
        private void Delete_click(object sender, RoutedEventArgs e)
        {
            DBManager.deleteCar_Byid(Carid.Text);
            GlobalGrids.updateviewitems();
        }
    }
}
