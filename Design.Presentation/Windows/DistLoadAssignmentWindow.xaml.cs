﻿using Design.Presentation.Model;
using Design.Presentation.ViewModels;
using Desing.Core.Sap;
using System;
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

namespace Design.Presentation.Windows
{
    /// <summary>
    /// Interaction logic for DistLoadAssignmentWindow.xaml
    /// </summary>
    public partial class DistLoadAssignmentWindow : Window
    {
        public DistLoadAssignmentModel DLAM = new DistLoadAssignmentModel();

        public DistLoadAssignmentWindow()
        {
            InitializeComponent();

            DistLoadGridData.CanUserAddRows = false;
            DistLoadGridData.CanUserDeleteRows = false;
            DistLoadGridData.CanUserSortColumns = false;
            DistLoadGridData.CanUserReorderColumns = false;
            DistLoadGridData.CanUserResizeColumns = false;
            DistLoadGridData.CanUserResizeRows = false;
        }

        private void DistLoadAddBtn_Click(object sender, RoutedEventArgs e)
        {
            //New object of the Model(Auto Column Generating)
            var newDistLoadRow = new DistLoadAssignmentModel();

            //Add object of the model to a collection of the object mode(DataGrid)
            DistLoadAssignmentViewModel.DistLoadModelStaticCollection.Add(newDistLoadRow);
        }

        private void DistLoadGridData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DLAM = (DistLoadAssignmentModel)DistLoadGridData.SelectedItem;
        }

        private void DistLoadCasesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            var s = cb.SelectedItem as LoadCasesModel;
            DLAM.selectedLoadCase = s.LoadcaseName;
        }

        private void DistLoadDirComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var cb = sender as ComboBox;
            cb.ItemsSource = Enum.GetValues(typeof(LoadDir)).Cast<LoadDir>();
        }

        private void DistLoadDirComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            var s = (LoadDir)cb.SelectedItem;
            DLAM.selectedDir = s;
        }

        private void DistSpanNoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            var s = (int)cb.SelectedItem;
            DLAM.selectedSpanNo = s;
        }
    }
}