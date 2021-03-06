﻿using Design.Core.Sap;
using Design.Presentation.Model;
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
    /// Interaction logic for PointLoadAssignmentWindow.xaml
    /// </summary>
    public partial class PointLoadAssignmentWindow : Window
    {
        public PointLoadAssignmentModel PLAM = new PointLoadAssignmentModel();

        public PointLoadAssignmentWindow()
        {
            InitializeComponent();

            PointLoadGridData.CanUserAddRows = false;
            PointLoadGridData.CanUserDeleteRows = false;
            PointLoadGridData.CanUserSortColumns = false;
            PointLoadGridData.CanUserReorderColumns = false;
            PointLoadGridData.CanUserResizeColumns = false;
            PointLoadGridData.CanUserResizeRows = false;
        }

        private void PointLoadCasesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            var s = cb.SelectedItem as LoadCasesModel;
            PLAM.selectedLoadCase = s.LoadcaseName;
        }

        private void PointLoadGridData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PLAM = (PointLoadAssignmentModel)PointLoadGridData.SelectedItem;
        }

        private void PointLoadDirComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var cb = sender as ComboBox;
            cb.ItemsSource = Enum.GetValues(typeof(LoadDir)).Cast<LoadDir>();
        }

        private void PointLoadDirComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            var s = (LoadDir)cb.SelectedItem;
            PLAM.selectedDir = s;
        }

        private void PointLoadAddBtn_Click(object sender, RoutedEventArgs e)
        {
            //New object of the Model(Auto Column Generating)
            var newPointLoadRow = new PointLoadAssignmentModel();

            //Add object of the model to a collection of the object mode(DataGrid)
            PointLoadAssignmentViewModel.PointLoadModelStaticCollection.Add(newPointLoadRow);
        }

        private void PointSpanNoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            var s = (int)cb.SelectedItem;
            PLAM.selectedSpanNo = s;
        }
        

        private void PointLoadDltBtn_Click(object sender, RoutedEventArgs e)
        {
            var index = PointLoadGridData.SelectedIndex;
            PointLoadAssignmentViewModel.PointLoadModelStaticCollection.RemoveAt(index);
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < PointLoadAssignmentViewModel.PointLoadModelStaticCollection.Count; i++)
            {
                if (PointLoadAssignmentViewModel.PointLoadModelStaticCollection[i].selectedLoadCase != null)
                {
                    //Point Load Value
                    AnalysisMapping.concLoadVals.Add(PointLoadAssignmentViewModel.PointLoadModelStaticCollection[i].PointLoadVal);

                    //SapFrame Element we assign the Point Load to
                    AnalysisMapping.concLoadFrameElement.Add((PointLoadAssignmentViewModel.PointLoadModelStaticCollection[i].selectedSpanNo).ToString());

                    //Load Case we assign the load to
                    AnalysisMapping.concLoadPattern.Add(PointLoadAssignmentViewModel.PointLoadModelStaticCollection[i].selectedLoadCase);

                    //Point Load Direction
                    AnalysisMapping.concLoadDirVal.Add(PointLoadAssignmentViewModel.PointLoadModelStaticCollection[i].selectedDir);

                    //Relative Distance List
                    AnalysisMapping.relDistList.Add(PointLoadAssignmentViewModel.PointLoadModelStaticCollection[i].RelativeDistance);

                }
            }

            this.Close();
        }
    }
}
