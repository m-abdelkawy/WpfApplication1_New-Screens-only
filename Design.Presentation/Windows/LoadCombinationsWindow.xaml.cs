using Design.Core.Sap;
using Design.Presentation.Model;
using Design.Presentation.ViewModels;
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
    /// Interaction logic for LoadCombinationsWindow.xaml
    /// </summary>
    public partial class LoadCombinationsWindow : Window
    {
        LoadCombinationsModel lcM = new LoadCombinationsModel();
        public MainWindow MainWindow { get; set; }
        #region Load Combination Storage
        //Load Combinations
        
        #endregion

        public LoadCombinationsWindow()
        {
            DataContext = new LoadCombinationsViewModel();
            InitializeComponent();

            loadComboGridData.CanUserAddRows = false;
            loadComboGridData.CanUserDeleteRows = false;
            loadComboGridData.CanUserSortColumns = false;
            loadComboGridData.CanUserReorderColumns = false;
            loadComboGridData.CanUserResizeColumns = false;
            loadComboGridData.CanUserResizeRows = false;
        }

        private void AddLoadComboBtn_Click(object sender, RoutedEventArgs e)
        {
            //New Object(Row) of the Model
            var newLoadComboRow = new LoadCombinationsModel();

            //Add the created object to a collection
            LoadCombinationsViewModel.LoadComboModelCollection.Add(newLoadComboRow);
        }

        private void DltLoadComboBtn_Click(object sender, RoutedEventArgs e)
        {

            var index = loadComboGridData.SelectedIndex;
            LoadCombinationsViewModel.LoadComboModelCollection.RemoveAt(index);
        }
        

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < LoadCombinationsViewModel.LoadComboModelCollection.Count; i++)
            {
                if (LoadCombinationsViewModel.LoadComboModelCollection[i].LoadComboName != null)
                {
                    AnalysisMapping.Combinations.Add(LoadCombinationsViewModel.LoadComboModelCollection[i].LoadComboName);
                    //loadCaseName.Add(dgcbLoadCases.);
                    AnalysisMapping.loadCaseName.Add(LoadCombinationsViewModel.LoadComboModelCollection[i].loadCaseSelectedItem);
                    AnalysisMapping.loadFactorList.Add(LoadCombinationsViewModel.LoadComboModelCollection[i].LoadFactor);
                }
            }

            this.Close();
        }

        private void SelectionLoadCase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            var s = cb.SelectedItem as LoadCasesModel;
            lcM.loadCaseSelectedItem = s.LoadcaseName;

        }

        private void loadComboGridData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lcM = (LoadCombinationsModel)loadComboGridData.SelectedItem;
        }
    }
}
