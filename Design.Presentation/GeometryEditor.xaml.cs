using Design.Core.Sap;
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

        public GeometryEditor(GeometryEditorVM geometryEditorVM)
        {
            DataContext = geometryEditorVM;
            InitializeComponent();
            Gr_GridData.CanUserResizeColumns = false;
            Gr_GridData.CanUserReorderColumns = false;

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

        private void SelectionRestrain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = (ComboBox)sender;
            var s = (Restraints)cb.SelectedItem;
            GridData.SelectedRestrain = s;
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
            for (int i = 1; i < Convert.ToInt32(nSpansTxtBox.Text) + 1; i++)
            {
                AnalysisMapping.spanList.Add(i);
            }
        }
    }
}
