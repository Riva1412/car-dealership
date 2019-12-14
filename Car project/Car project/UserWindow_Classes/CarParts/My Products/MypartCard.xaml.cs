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

namespace Car_project.UserWindow_Classes.CarParts.My_Products
{
    /// <summary>
    /// Interaction logic for MypartCard.xaml
    /// </summary>
    public partial class MypartCard : UserControl
    {
        public MypartCard()
        {
            InitializeComponent();
        }
        public byte[] PartImage { get; set; }
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
            PartImage = data;
        }
        private void Done_click(object sender, RoutedEventArgs e)
        {
            if (PartImage == null || PartImage.Length == 0)
                PartImage = DBManager.GetImage("CarPart", "ProductID",partid.Text);
            if (PartImage == null || PartImage.Length == 0 || bFCPPrice.Text == "" || bFCPName.Text == "" || bFCPColour.Text == "" ||
                bFCPQuantity.Text == "" || bFCPWarranty.Text == "")
            {
                MessageBox.Show("Please Fill All Details");
                return;
            }
            DBManager.Upate_CarPart(PartImage, bFCPPrice.Text, bFCPName.Text, bFCPColour.Text, bFCPQuantity.Text,
                bFCPWarranty.Text , partid.Text);
        }
    }
}
