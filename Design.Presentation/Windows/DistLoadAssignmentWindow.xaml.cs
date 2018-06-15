using Design.Core.Sap;
using Design.Presentation.Geometry;
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
    /// Interaction logic for DistLoadAssignmentWindow.xaml
    /// </summary>
    public partial class DistLoadAssignmentWindow : Window
    {
        public DistLoadAssignmentModel DLAM = new DistLoadAssignmentModel();
        private GeometryEngine GeometryEngine { get; set; }
        public DistLoadAssignmentWindow(GeometryEngine geometryEngine)
        {
            InitializeComponent();
            this.GeometryEngine = geometryEngine;

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
            if (s <0) return;
            DLAM.selectedSpanNo = s;

        }

        private void DistLoadDltBtn_Click(object sender, RoutedEventArgs e)
        {
            var index = DistLoadGridData.SelectedIndex;
            DistLoadAssignmentViewModel.DistLoadModelStaticCollection.RemoveAt(index);
        }

        private void DistLoadSubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            /*------------------------Comulative Span------------------------*/
            //List<double> GComSpanValues = new List<double>();
            //GComSpanValues.Clear();
            //GComSpanValues.Add(0);
            //double cumulativeSpans = 0;
            //for (int i = 0; i < GeometryEditorVM.GeometryEditor.NumberOfSpans; i++)
            //{
            //    cumulativeSpans += GeometryEditorVM.GeometryEditor.GridData[i].Span;

            //    GComSpanValues.Add(cumulativeSpans);
            //}
            /*--------------------------------------------------------------------*/
            for (int i = 0; i < DistLoadAssignmentViewModel.DistLoadModelStaticCollection.Count; i++)
            {
                if (DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].LoadCases!=null)
                {
                    //Load Value
                    AnalysisMapping.distLoadVals.Add(DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].DistLoadVal);

                    // Load Pattern we Assign load to
                    AnalysisMapping.distLoadPattern.Add(DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].selectedLoadCase);

                    //Sap Frame Element We Assign Load to:
                    AnalysisMapping.DistLoadFrameElement.Add((DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].selectedSpanNo).ToString());

                    //Load Direction List
                    AnalysisMapping.distLoadDirVal.Add(DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].selectedDir);
                }
            }

            //Draw Loads
            if (AnalysisMapping.distLoadDirVal.Count<1)
            {
                return;
            }
            DrawingHelper.DistLoadShapes.Clear();
            List<double> GComSpanValues = new List<double>();
            GComSpanValues.Clear();
            GComSpanValues.Add(0);
            double cumulativeSpans = 0;
            for (int i = 0; i < GeometryEditorVM.GeometryEditor.NumberOfSpans; i++)
            {
                cumulativeSpans += GeometryEditorVM.GeometryEditor.GridData[i].Span;

                GComSpanValues.Add(cumulativeSpans);
            }
            List<string> Dead = new List<string>();
            List<string> Live = new List<string>();
            List<string> SD = new List<string>();

            int[] Js = new int[DistLoadAssignmentViewModel.DistLoadModelStaticCollection.Count];
            int YPosition = 100;

            for (int i = 0; i < DistLoadAssignmentViewModel.DistLoadModelStaticCollection.Count; i++)
            {
                double startX = GComSpanValues[DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].selectedSpanNo - 1] * 20;
                double endX = GComSpanValues[DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].selectedSpanNo] * 20;
                for (int j = 0; j < AnalysisMapping.loadPatternName.Count; j++)
                {
                    if (DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].selectedLoadCase == AnalysisMapping.loadPatternName[j])
                    {
                        switch (j)
                        {
                            case 0:
                                Dead.Add(AnalysisMapping.distLoadPattern[i]);
                                GeometryEngine.Shapes["DistributedLoad"].Add(new ArrowLoad(GeometryEngine.GCanvas
                                    , new Point(startX, 100), new Point(endX, 100), DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].DistLoadVal));
                                //Text
                                {
                                    TextBlock textBlock = new TextBlock();

                                    textBlock.Text = AnalysisMapping.loadPatternName[j] + " Load";

                                    textBlock.Foreground = new SolidColorBrush(Colors.Black);
                                    textBlock.Background = new SolidColorBrush(Colors.PaleVioletRed);
                                    textBlock.Height = 20;
                                    textBlock.Width = 80;
                                    textBlock.Visibility = Visibility.Visible;

                                    Canvas.SetLeft(textBlock, GComSpanValues[GeometryEditorVM.GeometryEditor.NumberOfSpans] * 20 + 20);

                                    Canvas.SetTop(textBlock, 110);

                                    GeometryEngine.GCanvas.Canvas.Children.Add(textBlock);
                                }
                                
                                break;
                            case 1:
                                Live.Add(AnalysisMapping.distLoadPattern[i]);
                                GeometryEngine.Shapes["DistributedLoad"].Add(new ArrowLoad(GeometryEngine.GCanvas
                                    , new Point(startX, 200), new Point(endX, 200), DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].DistLoadVal * 0.50));
                                break;
                            case 2:
                                SD.Add(AnalysisMapping.distLoadPattern[i]);
                                GeometryEngine.Shapes["DistributedLoad"].Add(new ArrowLoad(GeometryEngine.GCanvas
                                    , new Point(startX, 300), new Point(endX, 300), DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].DistLoadVal * 0.50));
                                break;
                            case 3:
                                SD.Add(AnalysisMapping.distLoadPattern[i]);
                                GeometryEngine.Shapes["DistributedLoad"].Add(new ArrowLoad(GeometryEngine.GCanvas
                                    , new Point(startX, 400), new Point(endX, 400), DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].DistLoadVal * 0.50));
                                break;
                            case 4:
                                SD.Add(AnalysisMapping.distLoadPattern[i]);
                                GeometryEngine.Shapes["DistributedLoad"].Add(new ArrowLoad(GeometryEngine.GCanvas
                                    , new Point(startX, 500), new Point(endX, 500), DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].DistLoadVal * 0.50));
                                break;
                            default:
                                break;
                        }
                    }
                }
            GeometryEngine.GCanvas.Render();
                

                
            }


            #region Trial
            //for (int i = 0; i < DistLoadAssignmentViewModel.DistLoadModelStaticCollection.Count; i++)
            //{
            //    double startX = GComSpanValues[DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].selectedSpanNo - 1] * 20;
            //    double endX = GComSpanValues[DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].selectedSpanNo] * 20;
            //    for (int j = 0; j < AnalysisMapping.loadPatternName.Count; j++)
            //    {
            //        if (AnalysisMapping.distLoadPattern[i] == AnalysisMapping.loadPatternName[j])
            //        {
            //            Js[i] = j;
            //            if (i != 0 && Js[i] != Js[i - 1])
            //            {
            //                YPosition += 100;
            //                GeometryEngine.Shapes["DistributedLoad"].Add(new ArrowLoad(GeometryEngine.GCanvas
            //                , new Point(startX, YPosition), new Point(endX, YPosition)));
            //            }
            //            YPosition += 100;
            //            GeometryEngine.Shapes["DistributedLoad"].Add(new ArrowLoad(GeometryEngine.GCanvas
            //            , new Point(startX, YPosition), new Point(endX, YPosition)));
            //        }
            //    }

            //}
            #endregion
            GeometryEngine.GCanvas.Render();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
