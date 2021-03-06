﻿using Design.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Design.Presentation.Views.Section
{
    /// <summary>
    /// Interaction logic for SectionEditor.xaml
    /// </summary>
    /// 
    
    public partial class SectionEditor : Window
    {
        public SectionDialouge SectionDialouge { get; set; }
        public SectionEditor(SectionEditorVM sectionEditorVM)
        {
            DataContext = sectionEditorVM;
            InitializeComponent();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            SectionEditorVM.Sections.Add((SectionEditorVM)this.DataContext);
            Close();
        }
    }
}
