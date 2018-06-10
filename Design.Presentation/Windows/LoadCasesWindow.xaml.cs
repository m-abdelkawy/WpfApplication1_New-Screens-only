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
        //LoadPattern
        public static List<string> loadPatternName = new List<string>();
        public List<double> SelfWtMultiplier = new List<double>();
        public List<eLoadPatternType> patternType = new List<eLoadPatternType>();


        public List<SapLoadPattern> loadPatterns = new List<SapLoadPattern>();
        public eCNameType ec = eCNameType.LoadCase;
        /*------------------------------*/
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
    }
}
