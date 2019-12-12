﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for AllProducts.xaml
    /// </summary>
    public partial class All_Car_Products : UserControl
    {
        public All_Car_Products()
        {
            InitializeComponent();
            var CarProducts = DBManager.getAllCar_Products();
            if (CarProducts.Count > 0)
                ListViewProducts.ItemsSource = CarProducts;
            else
                MessageBox.Show("No Avaiable Products");
        }
      
    }
}