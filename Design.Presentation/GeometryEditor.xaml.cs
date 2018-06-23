using Design.Core.Sap;
using Design.Presentation.Geometry;
using Design.Presentation.Model;
using Design.Presentation.ViewModels;
using Design.Presentation.Views.Section;
using Desing.Core.Sap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for Geometry.xaml
    /// </summary>
    public partial class GeometryEditor : Window
    {
        private GridData GridData { get; set; }
        private GeometryEngine GeometryEngine { get; set; }
        private RestraintsModel RestraintsModel { get; set; }
        public GeometryEditor(GeometryEditorVM geometryEditorVM, GeometryEngine geometryEngine)
        {
            DataContext = geometryEditorVM;
            this.GeometryEngine = geometryEngine;
            InitializeComponent();

            Gr_GridData.CanUserAddRows = false;
            Gr_GridData.CanUserDeleteRows = false;
            Gr_GridData.CanUserSortColumns = false;
            Gr_GridData.CanUserReorderColumns = false;
            Gr_GridData.CanUserResizeColumns = false;
            Gr_GridData.CanUserResizeRows = false;

            SupportDataGrid.CanUserAddRows = false;
            SupportDataGrid.CanUserDeleteRows = false;
            SupportDataGrid.CanUserSortColumns = false;
            SupportDataGrid.CanUserReorderColumns = false;
            SupportDataGrid.CanUserResizeColumns = false;
            SupportDataGrid.CanUserResizeRows = false;

        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // Display Properties on DataGrid Columns
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var displayName = GetPropertyDisplayName(e.PropertyDescriptor);

            if (!string.IsNullOrEmpty(displayName))
            {
                e.Column.Header = displayName;
            }

        }

        public static string GetPropertyDisplayName(object descriptor)
        {
            var pd = descriptor as PropertyDescriptor;

            if (pd != null)
            {
                // Check for DisplayName attribute and set the column header accordingly
                var displayName = pd.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;

                if (displayName != null && displayName != DisplayNameAttribute.Default)
                {
                    return displayName.DisplayName;
                }

            }
            else
            {
                var pi = descriptor as PropertyInfo;

                if (pi != null)
                {
                    // Check for DisplayName attribute and set the column header accordingly
                    Object[] attributes = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    for (int i = 0; i < attributes.Length; ++i)
                    {
                        var displayName = attributes[i] as DisplayNameAttribute;
                        if (displayName != null && displayName != DisplayNameAttribute.Default)
                        {
                            return displayName.DisplayName;
                        }
                    }
                }
            }

            return null;
        }

        private void Btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (GeometryEditorVM.GeometryEditor.GridData.Count < 1)
            {
                return;
            }
            DrawingHelper.GLineList.Clear();
            List<double> GComSpanValues = new List<double>();
            GComSpanValues.Clear();
            GComSpanValues.Add(0);
            double cumulativeSpans = 0;
            for (int i = 0; i < GeometryEditorVM.GeometryEditor.NumberOfSpans; i++)
            {
                cumulativeSpans += GeometryEditorVM.GeometryEditor.GridData[i].Span;

                GComSpanValues.Add(cumulativeSpans);
            }


            if (GeometryEditorVM.GeometryEditor.NumberOfSpans < 1)
            {
                return;
            }
            GeometryEngine.RemoveAll();

            for (int i = 0; i < GeometryEditorVM.GeometryEditor.NumberOfSpans; i++)
            {
                GeometryEngine.Shapes["Beams"].Add(new GLine(GeometryEngine.GCanvas, new Point(GComSpanValues[i] * 20, 130)
                    , new Point(GComSpanValues[i + 1] * 20, 130)));
               // GeometryEngine.Shapes["Beams"].ForEach(d => { d.Scale = 10;d.New(); });
                //new Hinged(GeometryEngine.GCanvas, new Point(GComSpanValues[i] * 20, 100));
            }


            for (int i = 0; i < GeometryEditorVM.GeometryEditor.RestraintsCollection.Count; i++)
            {
                switch (GeometryEditorVM.GeometryEditor.RestraintsCollection[i].SelectedRestraint)
                {
                    case Restraints.Fixed:
                        //Revise
                     GeometryEngine.Shapes["Supports"].Add(
                         new Fixed(GeometryEngine.GCanvas, new Point(GComSpanValues[i]* 20, 130), 20));
                        break;
                    case Restraints.Hinged:
                        GeometryEngine.Shapes["Supports"].Add(
                        new Hinged(GeometryEngine.GCanvas, new Point(GComSpanValues[i] * 20, 130)));
                        break;
                    case Restraints.Roller:
                        GeometryEngine.Shapes["Supports"].Add(
                        new Roller(GeometryEngine.GCanvas, new Point(GComSpanValues[i] * 20, 130)));
                        break;
                    case Restraints.NoRestraints:
                        break;
                    default:
                        break;
                }
            }

            GeometryEngine.RenderAll();
            GeometryEngine.Shapes["Supports"].ForEach(d => d.SetScale(0.75));
            //GeometryEngine.Shapes.Add("li", new List<GShape>()); Add List in Real Time
            this.Close();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Add_Section_Click(object sender, RoutedEventArgs e)
        {
            var se = new SectionEditor(new SectionEditorVM());

            se.ShowDialog();

        }

        private void SelectionRestrain_Loaded(object sender, RoutedEventArgs e)
        {
            var cb = (ComboBox)sender;
            cb.ItemsSource = Enum.GetValues(typeof(Restraints)).Cast<Restraints>();
        }



        private void Gr_GridData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridData = Gr_GridData.SelectedItem as GridData;
        }

        private void SelectionCol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var cb = (ComboBox)sender;
            var se = (SectionEditorVM)cb.SelectedItem;
            if (se == null) return;
            GridData.SelectedSection = se;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void cb_SelectedSection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = ((ComboBox)sender).SelectedItem as SectionEditorVM;
            if (cb == null) return;
            var selectedSection = ((GeometryEditorVM)DataContext).SelectedSection = cb;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AnalysisMapping.spanList.Clear();
            if (nSpansTxtBox.Text != null && nSpansTxtBox.Text != "")
            {
                for (int i = 0; i < Convert.ToInt32(nSpansTxtBox.Text); i++)
                {
                    AnalysisMapping.spanList.Add(i + 1);
                }
            }
            
        }

        private void Btn_AddBeam_Click(object sender, RoutedEventArgs e)
        {//-------------Revise
            //New object of the Model(Auto Column Generating)
            var newBeaElementRow = new GridData();

            //Add object of the model to a collection of the object mode(DataGrid)
            GeometryEditorVM.GeometryEditor.GridData.Add(newBeaElementRow);

            //Update SpanList //------------Revise
            AnalysisMapping.spanList.Clear();
            for (int i = 1; i < GeometryEditorVM.GeometryEditor.GridData.Count + 1; i++)
            {
                AnalysisMapping.spanList.Add(i);
            }
        }

        private void Btn_DltBeam_Click(object sender, RoutedEventArgs e)
        {//------------Revise
            var index = Gr_GridData.SelectedIndex;
            if (index < GeometryEditorVM.GeometryEditor.GridData.Count)
            {
                GeometryEditorVM.GeometryEditor.GridData.RemoveAt(index);

                //Update SpanList //------------Revise
                AnalysisMapping.spanList.Clear();
                for (int i = 1; i < GeometryEditorVM.GeometryEditor.GridData.Count + 1; i++)
                {
                    AnalysisMapping.spanList.Add(i);
                }
            }

        }

        private void Gr_GridData_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {

        }

        private void SupportDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RestraintsModel = SupportDataGrid.SelectedItem as RestraintsModel;
        }

        private void SelectionRestraint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = (ComboBox)sender;
            var s = (Restraints)cb.SelectedItem;
            RestraintsModel.SelectedRestraint = s;
        }
    }
}
