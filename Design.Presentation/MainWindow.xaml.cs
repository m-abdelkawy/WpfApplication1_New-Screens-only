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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SAP2000v20;

namespace Design.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Storage
        public List<int> spanList = new List<int>();
        public List<int> beamNames = new List<int>();

        public List<string> loadCaseName = new List<string>();
        public int nSpans;

        public double[] spanValues;
        public double[] comSpanValues; //comlative
        public int[] rebarDiameterArr;
        public int[] sections = new int[] { 1, 2, 3 };
        public int[] shearSections = new int[] { 1, 2 };

        SapPoint[] points;
        SapFrameElement[] xbeams;

        SapRectSection B;

        SapModel mySapModel;
        /*------------------------------*/

        

        /*------------------------------*/

        //Load Combinations
        public List<string> Combinations = new List<string>();
        public List<double> loadFactorList = new List<double>();
        /*------------------------------*/
        /*----------Distributed Loads-----------*/
        public List<string> distLoadPattern = new List<string>();
        public List<LoadDir> distLoadDirVal = new List<LoadDir>();
        public List<double> distLoadVals = new List<double>();
        public List<string> DistLoadFrameElement = new List<string>();

        /*----------Point Loads--------*/
        public List<string> concLoadPattern = new List<string>();
        public List<double> concLoadVals = new List<double>();
        public List<LoadDir> concLoadDirVal = new List<LoadDir>();
        public List<string> concLoadFrameElement = new List<string>();
        public List<double> relDistList = new List<double>();

        /*------------------------------*/


        public List<SapFrameDistLoad> distLoads = new List<SapFrameDistLoad>();
        public List<SapPointLoad> pointLoads = new List<SapPointLoad>();

        string[] comboArray;

        //Stirrup Arrays
        public double[] stirDiaArr240 = new double[] { 8 };
        public double[] stirDiaArr360 = new double[] { 10, 12, 14, 16 };
        public double[] stirDiaArr400 = new double[] { 10, 12, 14, 16 };


        /*----------DistLoadShape Lists----------*/
        //Rectangle[] distLoadShapeArr;
        public List<List<Rectangle>> distLoadShapes = new List<List<Rectangle>>();
        #endregion

        /*-------------------------------------------------------------------------------------------------------*/
        /*-------------------------------------------------------------------------------------------------------*/
        /*-------------------------------------------------------------------------------------------------------*/
        #region Properties
        public GeometryEditor GeometryEditor { get; set; }
        public SectionProperties SectioProperties { get; set; }

        //Load Cases Window
        public LoadCases LoadCases { get; set; }
        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }



        #region Btn_Events

        private void Btn_editBeam_Click(object sender, RoutedEventArgs e)
        {
            GeometryEditor= new GeometryEditor();
            GeometryEditor.ShowDialog();

        }
        #endregion

        private void btn_analyse_Click(object sender, RoutedEventArgs e)
        {
            var gemoemtryData = (GeometryEditorVM)GeometryEditor.DataContext;

        }

        private void btn_loadCases_Click(object sender, RoutedEventArgs e)
        {
            LoadCases = new LoadCases();
            LoadCases.Show();
        }
    }
}
