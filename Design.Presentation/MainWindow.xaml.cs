using Design.Presentation.ViewModels;
using Design.Presentation.Views.Material;
using Design.Presentation.Views.Section;
using Desing.Core.Sap;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SAP2000v20;
using Design.Presentation.Windows;
using Design.Presentation.Model;

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
       
        public MainWindow()
        {
            InitializeComponent();
           
        }



        #region Btn_Events

        private void Btn_editBeam_Click(object sender, RoutedEventArgs e)
        {
            var ge = new GeometryEditor(GeometryEditorVM.GeometryEditor);
            ge.ShowDialog();
        }
        #endregion

        private void btn_analyse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_loadCases_Click(object sender, RoutedEventArgs e)
        {
            var lc=new LoadCasesWindow();
            lc.DataContext = new LoadCasesViewModel();
            lc.ShowDialog();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void Btn_Material_Click(object sender, RoutedEventArgs e)
        {
            MaterialEditor me = new MaterialEditor( MaterialEditorVM.Material);
            me.ShowDialog();
        }

        private void Btn_Section_Click(object sender, RoutedEventArgs e)
        {
            SectionDialouge sd = new SectionDialouge();
            sd.DataContext = new SectionDialougeVM() ;
            
         
            sd.Show();
        }

        private void Btn_LoadCombos_Click(object sender, RoutedEventArgs e)
        {
            var loadComboWin = new LoadCombinationsWindow();
            loadComboWin.DataContext = new LoadCombinationsViewModel();
            loadComboWin.ShowDialog();
        }
    }
}
