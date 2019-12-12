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
    /// Interaction logic for AddCarPart.xaml
    /// </summary>
    public partial class AddCarPart : UserControl
    {
        public AddCarPart()
        {
            InitializeComponent();
        }
        public byte[] PartImage { get; set; }
        private void AddImage_click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();
            FileStream fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            ImageSourceConverter imgs = new ImageSourceConverter();
            imagebox.SetValue(Image.SourceProperty, imgs.
            ConvertFromString(dlg.FileName.ToString()));
            PartImage = data;
        }
        private void Update_Flipper(object sender, RoutedEventArgs e)
        {
            fFCPName.Text = bFCPName.Text;
            fFCPPrice.Text = bFCPPrice.Text;
        }
        private void Add_CarPart(object sender, RoutedEventArgs e)
        {
            if (bFCPPrice.Text == "" || bFCPName.Text == "" || bFCPColour.Text == "" ||
                bFCPQuantity.Text == "" || bFCPWarranty.Text == "")
            { 
                MessageBox.Show("Please Fill All Details");
                return;
            }
            DBManager.Add_CarPart_Fn(bFCPPrice.Text, bFCPName.Text, bFCPColour.Text, bFCPQuantity.Text, bFCPWarranty.Text);
            bFCPPrice.Text = ""; bFCPColour.Text = ""; bFCPQuantity.Text = ""; bFCPWarranty.Text = "";
        }
    }
}
