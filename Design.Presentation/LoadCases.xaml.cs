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

namespace Design.Presentation
{
    /// <summary>
    /// Interaction logic for LoadCases.xaml
    /// </summary>
    public partial class LoadCases : Page
    {
        public LoadCases()
        {
            InitializeComponent();
        }

        static int loadCaseBtnClicked = 0;
        private void loadCaseBtn_Click(object sender, RoutedEventArgs e)
        {
            loadPatternName.Add(loadCasetxtBox.Text);
            SelfWtMultiplier.Add(Convert.ToDouble(selfWttxtBox.Text));
            patternType.Add((eLoadPatternType)loadTypeComboBox.SelectedValue);

            distLoadShapes.Add(new List<Rectangle>());
            loadCaseBtnClicked += 1;
        }

        private void loadTypeComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            loadTypeComboBox.ItemsSource = Enum.GetValues(typeof(eLoadPatternType)).Cast<eLoadPatternType>();
        }

        private void loadCaseComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var loadCaseComboBox = sender as ComboBox;
            loadCaseComboBox.ItemsSource = loadPatternName;
            loadCaseComboBox.SelectedIndex = 0;
        }

        private void loadCaseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var loadCaseComboBox = sender as ComboBox;
            string value = loadCaseComboBox.SelectedIndex.ToString();

            var loadComboBoxSelectedItem = e.AddedItems[0] as string;

            //loadCaseName = loadComboBoxSelectedItem;
            //this.Title = value;
        }

        private void loadComboBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
