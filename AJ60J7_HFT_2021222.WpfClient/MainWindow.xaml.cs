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

namespace AJ60J7_HFT_2021222.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void brandButton_Click(object sender, RoutedEventArgs e)
        {
            BrandWindow brandWindow = new BrandWindow();
            brandWindow.Show();
        }

        private void engineButton_Click(object sender, RoutedEventArgs e)
        {
            EngineWindow engineWindow = new EngineWindow();
            engineWindow.Show();
        }

        private void carButton_Click(object sender, RoutedEventArgs e)
        {
            CarWindow carWindow = new CarWindow();
            carWindow.Show();
        }
    }
}
