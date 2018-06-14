using Design.Presentation.ViewModels;
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

namespace Design.Presentation.Views.Material
{
    /// <summary>
    /// Interaction logic for MaterialEditor.xaml
    /// </summary>
    public partial class MaterialEditor : Window
    {
        double[] fcuVals = new double[] { 25, 30, 35, 40 };
        double[] fyVals = new double[] { 240, 360, 400 };
        double[] fystrVals = new double[] { 240, 360, 400 };
        public MaterialEditor( MaterialEditorVM materialEditorVM)
        {
            DataContext = materialEditorVM;
            InitializeComponent();
        }

        private void Btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            MaterialEditorVM.Counter--;
            Close();
        }

        private void fcuComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var fcuComboBox = sender as ComboBox;
            fcuComboBox.ItemsSource = fcuVals;
            fcuComboBox.SelectedIndex = 0;
        }

        private void fcuComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fcuComboBox = sender as ComboBox;
            double value = Convert.ToDouble(fcuComboBox.SelectedItem);
            MaterialEditorVM.Material.Fcu = value;

        }

        private void fyComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var fyComboBox = sender as ComboBox;
            fyComboBox.ItemsSource = fyVals;
            fyComboBox.SelectedIndex = 0;
        }

        private void fyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//------------------->Consider Revision
            var fyComboBox = sender as ComboBox;
            double value = Convert.ToDouble(fyComboBox.SelectedItem);
            MaterialEditorVM.Material.Fy = value;
        }

        private void fystrComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var fystrComboBox = sender as ComboBox;
            fystrComboBox.ItemsSource = fystrVals;
            fystrComboBox.SelectedIndex = 0;
        }

        private void fystrComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fystrComboBox = sender as ComboBox;
            double value = Convert.ToDouble(fystrComboBox.SelectedItem);
            MaterialEditorVM.Material.FyStirrups = value;
        }
    }
}
