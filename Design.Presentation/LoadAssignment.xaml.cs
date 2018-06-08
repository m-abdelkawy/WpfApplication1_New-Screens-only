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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SAP2000v20;
using Desing.Core.Sap;

namespace Design.Presentation
{
    /// <summary>
    /// Interaction logic for LoadAssignment.xaml
    /// </summary>
    public partial class LoadAssignment : Page
    {
        public LoadAssignment()
        {
            InitializeComponent();
        }

        //    private void loadCaseComboBox_Loaded(object sender, RoutedEventArgs e)
        //    {

        //    }

        //    private void loadCaseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //    {

        //    }

        //    private void distLoadCaseComboBox_Loaded(object sender, RoutedEventArgs e)
        //    {
        //        var distLoadCaseComboBox = sender as ComboBox;
        //        distLoadCaseComboBox.ItemsSource = loadPatternName;
        //        distLoadCaseComboBox.SelectedIndex = 0;
        //    }

        //    private void distLoadCaseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //    {
        //        var distLoadCaseComboBox = sender as ComboBox;
        //        string value = distLoadCaseComboBox.SelectedIndex.ToString();

        //        var distLoadCaseComboBoxSelectedItem = e.AddedItems[0] as string;
        //    }

        //    private void xBeamsComboBox2_Loaded(object sender, RoutedEventArgs e)
        //    {

        //    }

        //    private void xBeamsComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //    {
        //        var xBeamsComboBox2 = sender as ComboBox;
        //        string value = xBeamsComboBox2.SelectedItem as string;
        //    }

        //    private void xBeamsComboBox_Loaded(object sender, RoutedEventArgs e)
        //    {

        //    }

        //    private void xBeamsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //    {
        //        var xBeamsComboBox = sender as ComboBox;
        //        string value = xBeamsComboBox.SelectedItem as string;
        //    }


        //    private void pointLoadDirComboBox_Loaded(object sender, RoutedEventArgs e)
        //    {
        //        pointLoadDirComboBox.ItemsSource = Enum.GetValues(typeof(LoadDir)).Cast<LoadDir>();
        //    }

        //    private void pointLoadDirComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //    {
        //        var pointLoadDirComboBox = sender as ComboBox;
        //        string value = pointLoadDirComboBox.SelectedIndex.ToString();
        //    }

        //    private void distLoadDirComboBox_Loaded(object sender, RoutedEventArgs e)
        //    {
        //        distLoadDirComboBox.ItemsSource = Enum.GetValues(typeof(LoadDir)).Cast<LoadDir>();
        //    }

        //    private void distLoadDirComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //    {
        //        var distLoadDirComboBox = sender as ComboBox;
        //        string value = distLoadDirComboBox.SelectedIndex.ToString();
        //    }

        //    int assignBtnClicked = 0;
        //    int loadComboSelectedIndex = 0;
        //    int loadIndexShapeList1 = 0;
        //    int loadIndexShapeList2 = 1;
        //    private void distLoadAssignBtn_Click(object sender, RoutedEventArgs e)
        //    {
        //        if (distLoadtxtBox.Text != null)
        //        {
        //            //Fill Dist Load Lists:
        //            DistLoadFrameElement.Add((xBeamsComboBox.SelectedIndex + 1).ToString());
        //            distLoadPattern.Add((distLoadCaseComboBox.SelectedValue) as string);
        //            distLoadDirVal.Add((LoadDir)(distLoadDirComboBox.SelectedValue));
        //            distLoadVals.Add(Convert.ToDouble(distLoadtxtBox.Text));
        //            /*-------------*/
        //            if (assignBtnClicked == 0 || (assignBtnClicked > 0 && (distLoadPattern[assignBtnClicked] == distLoadPattern[assignBtnClicked - 1])))
        //            {
        //                loadComboSelectedIndex = distLoadCaseComboBox.SelectedIndex;
        //                //Draw Loads
        //                if (loadIndexShapeList2 != distLoadCaseComboBox.SelectedIndex)
        //                {
        //                    for (int i = 0; i < distLoadShapes[loadIndexShapeList2].Count; i++)
        //                    {
        //                        LoadCanvas.Children.Remove(distLoadShapes[loadIndexShapeList2][i]);
        //                    }
        //                }

        //                Rectangle r = new Rectangle();
        //                r.Width = (comSpanValues[xBeamsComboBox.SelectedIndex + 1] - comSpanValues[xBeamsComboBox.SelectedIndex]) * 20;
        //                r.Height = Convert.ToDouble(distLoadtxtBox.Text) * 20;
        //                r.Fill = Brushes.Red;
        //                r.Stroke = Brushes.Blue;
        //                r.StrokeThickness = 4;
        //                Canvas.SetBottom(r, 380);
        //                Canvas.SetLeft(r, comSpanValues[xBeamsComboBox.SelectedIndex] * 20);
        //                //Canvas.SetRight(r, comSpanValues[xBeamsComboBox.SelectedIndex + 1] * 20);
        //                distLoadShapes[distLoadCaseComboBox.SelectedIndex].Add(r);
        //                LoadCanvas.Children.Add(r);

        //                loadIndexShapeList1 = distLoadCaseComboBox.SelectedIndex;
        //                loadComboSelectedIndex = 0;
        //            }
        //            else if (assignBtnClicked > 0 && (distLoadPattern[assignBtnClicked] != distLoadPattern[assignBtnClicked - 1]))
        //            {
        //                loadComboSelectedIndex = distLoadCaseComboBox.SelectedIndex;
        //                loadIndexShapeList2 = distLoadCaseComboBox.SelectedIndex;
        //                //DrawLoads:
        //                if (loadIndexShapeList1 != distLoadCaseComboBox.SelectedIndex)
        //                {
        //                    for (int i = 0; i < distLoadShapes[loadIndexShapeList1].Count; i++)
        //                    {
        //                        LoadCanvas.Children.Remove(distLoadShapes[loadIndexShapeList1][i]);
        //                    }
        //                }


        //                Rectangle r = new Rectangle();
        //                r.Width = (comSpanValues[xBeamsComboBox.SelectedIndex + 1] - comSpanValues[xBeamsComboBox.SelectedIndex]) * 20;
        //                r.Height = Convert.ToDouble(distLoadtxtBox.Text) * 20;
        //                r.Fill = Brushes.Red;
        //                r.Stroke = Brushes.Blue;
        //                r.StrokeThickness = 4;
        //                Canvas.SetBottom(r, 380);
        //                Canvas.SetLeft(r, comSpanValues[xBeamsComboBox.SelectedIndex] * 20);
        //                //Canvas.SetRight(r, comSpanValues[xBeamsComboBox.SelectedIndex + 1] * 20);
        //                distLoadShapes[distLoadCaseComboBox.SelectedIndex].Add(r);
        //                LoadCanvas.Children.Add(r);

        //                loadComboSelectedIndex = 0;
        //            }

        //            assignBtnClicked += 1;
        //        }
        //    }

        //    private void pointLoadAssignBtn_Click(object sender, RoutedEventArgs e)
        //    {
        //        if (concLoadtxtBox.Text != null)
        //        {
        //            //Fill Concentrated Load Lists:
        //            concLoadFrameElement.Add((xBeamsComboBox2.SelectedIndex + 1).ToString());
        //            concLoadPattern.Add((pointLoadCaseComboBox.SelectedValue) as string);
        //            concLoadDirVal.Add((LoadDir)(pointLoadDirComboBox.SelectedValue));
        //            concLoadVals.Add(Convert.ToDouble(concLoadtxtBox.Text));
        //            relDistList.Add(Convert.ToDouble(relDisttxtBox));
        //        }
        //    }

        //    private void pointLoadCaseComboBox_Loaded(object sender, RoutedEventArgs e)
        //    {
        //        var pointLoadCaseComboBox = sender as ComboBox;
        //        pointLoadCaseComboBox.ItemsSource = loadPatternName;
        //        pointLoadCaseComboBox.SelectedIndex = 0;
        //    }

        //    private void pointLoadCaseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //    {
        //        var pointLoadCaseComboBox = sender as ComboBox;
        //        string value = pointLoadCaseComboBox.SelectedIndex.ToString();

        //        var pointLoadCaseComboBoxSelectedItem = e.AddedItems[0] as string;
        //    }
        //}
    }
}