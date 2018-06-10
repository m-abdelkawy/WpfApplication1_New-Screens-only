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
        public MainWindow MainWindow { get; set; }
        #region Load Combination Storage
        //Load Combinations
        List<string> Combinations = new List<string>();
        public static List<string> loadCaseName = new List<string>();
        List<double> loadFactorList = new List<double>();
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

        private void loadComboSubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < LoadCombinationsViewModel.LoadComboModelCollection.Count; i++)
            {
                if (LoadCombinationsViewModel.LoadComboModelCollection[i].LoadComboName != null)
                {
                    Combinations.Add(LoadCombinationsViewModel.LoadComboModelCollection[i].LoadComboName);
                    //loadCaseName.Add(dgcbLoadCases.);
                    loadCaseName.Add(LoadCombinationsViewModel.LoadComboModelCollection[i].LoadCases[0].LoadcaseName);
                    loadFactorList.Add(LoadCombinationsViewModel.LoadComboModelCollection[i].LoadFactor);
                }
            }
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
