using Design.Core.Sap;
using Design.Presentation.Model;
using Design.Presentation.ViewModels;
using Desing.Core.Sap;
using SAP2000v20;
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
    /// Interaction logic for LoadCasesWindow.xaml
    /// </summary>
    public partial class LoadCasesWindow : Window
    {
        #region Load Pattern Storage
        
        public eCNameType ec = eCNameType.LoadCase;
        /*------------------------------*/
        #endregion
        public LoadCasesWindow()
        {
            DataContext = new LoadCasesViewModel();
            InitializeComponent();
            loadCaseGridData.CanUserAddRows = false;
            loadCaseGridData.CanUserDeleteRows = false;
            loadCaseGridData.CanUserSortColumns = false;
            loadCaseGridData.CanUserReorderColumns = false;
            loadCaseGridData.CanUserResizeColumns = false;
            loadCaseGridData.CanUserResizeRows = false;
        }

        private void AddLoadCaseBtn_Click(object sender, RoutedEventArgs e)
        {
            //New object of the Model(Auto Column Generating)
            var newLoadCaseRow = new LoadCasesModel(); 

            //Add object of the model to a collection of the object mode(DataGrid)
            LoadCasesViewModel.LoadCasesModelCollection.Add(newLoadCaseRow); 
        }

        private void DltLoadCaseBtn_Click(object sender, RoutedEventArgs e)
        {
            var index = loadCaseGridData.SelectedIndex;
            LoadCasesViewModel.LoadCasesModelCollection.RemoveAt(index);
        }

        private void loadCasesSubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            /*--------------> Consider Revision*/
            AnalysisMapping.loadPatternName.Clear();
            AnalysisMapping.SelfWtMultiplier.Clear();
            AnalysisMapping.patternType.Clear();

            for (int i = 0; i < LoadCasesViewModel.LoadCasesModelCollection.Count; i++)
            {
                if (LoadCasesViewModel.LoadCasesModelCollection[i].LoadcaseName != null)
                {
                    AnalysisMapping.loadPatternName.Add(LoadCasesViewModel.LoadCasesModelCollection[i].LoadcaseName);
                    AnalysisMapping.SelfWtMultiplier.Add(LoadCasesViewModel.LoadCasesModelCollection[i].SelfWtMult);
                    AnalysisMapping.patternType.Add(LoadCasesViewModel.LoadCasesModelCollection[i].loadPatternType);

                }
            }
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
