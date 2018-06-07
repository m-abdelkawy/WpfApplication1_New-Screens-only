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
using System.Windows.Shapes;

namespace Design.Presentation
{
    /// <summary>
    /// Interaction logic for SectionProperties.xaml
    /// </summary>
    public partial class SectionProperties : Window
    {

        public double  Width { get; set; }
        public double Thickness { get; set; }
        public Window Parent { get; set; }
        public SectionProperties()
        {
            InitializeComponent();
            var mainWindow = (MainWindow)Parent;
           
        }
    }
}
