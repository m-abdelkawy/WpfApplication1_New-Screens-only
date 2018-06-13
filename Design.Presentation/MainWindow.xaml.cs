﻿using Design.Presentation.ViewModels;
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
using Design.Presentation.Geometry;
using Microsoft.Win32;
using System.IO;
using Design.Core.Sap;

namespace Design.Presentation
{
    /// <summary>
    /// Interaction Geometry for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //lists
        int[] spanIndex = new int[GeometryEditorVM.GeometryEditor.GridData.Count];
        int[] rebarDiameterArr = new int[] { 8, 10, 12, 14, 16, 18, 20, 22, 25, 28, 32 };
        int[] StirrupsDiameterArr = new int[] { 8, 10, 12, 14, 16, 18 };
        int[] Flexuresections = new int[] { 1, 2, 3 };
        int[] shearSections = new int[] { 1, 2 };
        #region Storage
        //public List<int> spanList = new List<int>();
        //public List<int> beamNames = new List<int>();

        //public List<string> loadCaseName = new List<string>();
        //public int nSpans;

        //public double[] spanValues;
        //public double[] comSpanValues; //comlative
        ////public int[] rebarDiameterArr;
        //public int[] sections = new int[] { 1, 2, 3 };
        //public int[] shearSections = new int[] { 1, 2 };

        //SapPoint[] points;
        //SapFrameElement[] xbeams;

        //SapRectSection B;

        /*------------------------------*/



        /*------------------------------*/

        ////Load Combinations
        //public List<string> Combinations = new List<string>();
        //public List<double> loadFactorList = new List<double>();
        ///*------------------------------*/
        ///*----------Distributed Loads-----------*/
        //public List<string> distLoadPattern = new List<string>();
        //public List<LoadDir> distLoadDirVal = new List<LoadDir>();
        //public List<double> distLoadVals = new List<double>();
        //public List<string> DistLoadFrameElement = new List<string>();

        ///*----------Point Loads--------*/
        //public List<string> concLoadPattern = new List<string>();
        //public List<double> concLoadVals = new List<double>();
        //public List<LoadDir> concLoadDirVal = new List<LoadDir>();
        //public List<string> concLoadFrameElement = new List<string>();
        //public List<double> relDistList = new List<double>();

        ///*------------------------------*/


        //public List<SapFrameDistLoad> distLoads = new List<SapFrameDistLoad>();
        //public List<SapPointLoad> pointLoads = new List<SapPointLoad>();

        //string[] comboArray;

        ////Stirrup Arrays
        //public double[] stirDiaArr240 = new double[] { 8 };
        //public double[] stirDiaArr360 = new double[] { 10, 12, 14, 16 };
        //public double[] stirDiaArr400 = new double[] { 10, 12, 14, 16 };


        /*----------DistLoadShape Lists----------*/
        //Rectangle[] distLoadShapeArr;
        public List<List<Rectangle>> distLoadShapes = new List<List<Rectangle>>();
        #endregion

        /*-------------------------------------------------------------------------------------------------------*/
        /*-------------------------------------------------------------------------------------------------------*/
        /*-------------------------------------------------------------------------------------------------------*/

        public GeometryEngine GeometryEngine { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            GeometryEngine = new GeometryEngine();
            GeometryEngine.GCanvas.Canvas = canvas_Geometry;
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
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //if (saveFileDialog.ShowDialog() == true)
            //    File.WriteAllText(saveFileDialog.FileName, txtEditor.Text);

            /*-----------Initialize Model-----------*/
            //file Path and file Name 
            string filePath = "C:\\CSiAPIexample";
            string fileName = "Model1";
            //Create Sap Model
            AnalysisMapping.mySapModel = new SapModel(filePath, fileName);
            //initialize Model units KN, m, C:
            AnalysisMapping.mySapModel.InitializeUnits(eUnits.kN_m_C);
            /*-----------------------------------------------------------*/
            AnalysisMapping.CalcSpanValues();
            AnalysisMapping.CalcComSpanValues();
            SapMaterial concMat = AnalysisMapping.CreateConcMat(MaterialEditorVM.Material.Fcu);
            AnalysisMapping.CreateSapSections(SectionEditorVM.Sections, concMat);
            AnalysisMapping.CreateSapPoints();
            AnalysisMapping.CreateXbeams();
            AnalysisMapping.InitializeSuports();
            AnalysisMapping.AddLoadPatterns();
            AnalysisMapping.AddLoadCombinations();
            AnalysisMapping.AddDistributedLoads();
            AnalysisMapping.AddPointLoads();
            AnalysisMapping.RunModel();











        }

        private void btn_loadCases_Click(object sender, RoutedEventArgs e)
        {
            var lc = new LoadCasesWindow();
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
            MaterialEditor me = new MaterialEditor(MaterialEditorVM.Material);
            me.ShowDialog();
        }

        private void Btn_Section_Click(object sender, RoutedEventArgs e)
        {
            SectionDialouge sd = new SectionDialouge();
            sd.DataContext = new SectionDialougeVM();


            sd.Show();
        }

        private void Btn_LoadCombos_Click(object sender, RoutedEventArgs e)
        {
            var loadComboWin = new LoadCombinationsWindow();
            loadComboWin.DataContext = new LoadCombinationsViewModel();
            loadComboWin.ShowDialog();
        }



        private void Btn_DrawLine_Click(object sender, RoutedEventArgs e)
        {

            // var traingle = new GTriangle(GeometryEngine.GCanvas, new Point(60, 60), 20);
            var hinged = new Hinged(GeometryEngine.GCanvas, new Point(60, 60));
            var roller = new Roller(GeometryEngine.GCanvas, new Point(100, 60));

            //var rectangle = new GRectangle(GeometryEngine.GCanvas, 30, 15, new Point(140, 60+15));
            var fixedSupport = new Fixed(GeometryEngine.GCanvas, new Point(160, 60));
            var arrow = new Arrow(GeometryEngine.GCanvas, new Point(220, 60));

            var arrowLoad = new ArrowLoad(GeometryEngine.GCanvas, new Point(100, 120), new Point(200, 120));

            arrow.Rotate(45);
            GeometryEngine.GCanvas.Render();
        }

        private void bBtn_Show_Click(object sender, RoutedEventArgs e)
        {
            GeometryEngine.GCanvas.Hide();
            //  GeometryEngine.GCanvas.Canvas.UpdateLayout();

        }

        private void Btn_DistLoadAssinment_Click(object sender, RoutedEventArgs e)
        {
            var distLoadWin = new DistLoadAssignmentWindow();
            distLoadWin.DataContext = new DistLoadAssignmentViewModel();
            distLoadWin.ShowDialog();
        }

        private void Btn_PointLoadAssignment_Click(object sender, RoutedEventArgs e)
        {
            var pointLoadWin = new PointLoadAssignmentWindow();
            pointLoadWin.DataContext = new PointLoadAssignmentViewModel();
            pointLoadWin.ShowDialog();
        }

        private void Btn_Design_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FlexureSpanComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var FlexureSpanComboBox = sender as ComboBox;
            string value = FlexureSpanComboBox.SelectedItem as string;
        }

        private void FlexureSpanComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < spanIndex.Length; i++)
            {
                spanIndex[i] = i + 1;
            }
            var FlexureSpanComboBox = sender as ComboBox;
            FlexureSpanComboBox.ItemsSource = spanIndex;
            FlexureSpanComboBox.SelectedIndex = 0;
        }

        private void FlexureSectionComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var FlexureSectionComboBox = sender as ComboBox;
            FlexureSectionComboBox.ItemsSource = Flexuresections;
            FlexureSectionComboBox.SelectedIndex = 0;
        }

        private void FlexureSectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void rebarDiaComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var rebarDiaComboBox = sender as ComboBox;
            rebarDiaComboBox.ItemsSource = rebarDiameterArr;
            rebarDiaComboBox.SelectedIndex = 0;
        }

        private void ShearSpanComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < spanIndex.Length; i++)
            {
                spanIndex[i] = i + 1;
            }
            var ShearSpanComboBox = sender as ComboBox;
            ShearSpanComboBox.ItemsSource = spanIndex;
            ShearSpanComboBox.SelectedIndex = 0;
        }

        private void ShearSectionComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var ShearSectionComboBox = sender as ComboBox;
            ShearSectionComboBox.ItemsSource = shearSections;
            ShearSectionComboBox.SelectedIndex = 0;
        }

        private void stirrupComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var stirrupComboBox = sender as ComboBox;
            stirrupComboBox.ItemsSource = StirrupsDiameterArr;
            stirrupComboBox.SelectedIndex = 0;
        }

        private void calcAsBtn_Click(object sender, RoutedEventArgs e)
        {
            AsTxtBlock.Text = AnalysisMapping.xbeams[Convert.ToInt32(FlexureSpanComboBox.SelectedIndex)]
                .FrameResults.ElementAt(0).Asteel[Convert.ToInt32(FlexureSectionComboBox.SelectedIndex)].ToString();

            rebarNoTxtBlock.Text = ((Convert.ToDouble(AsTxtBlock.Text) * 4)
                / (Math.PI * Math.Pow(Convert.ToDouble(rebarDiaComboBox.SelectedItem) / 10, 2))).ToString();
        }

        private void calcSBtn_Click(object sender, RoutedEventArgs e)
        {
            double dia = Convert.ToDouble(stirrupComboBox.SelectedItem);
            spacingTxtBlock.Text = ((Math.PI * Math.Pow(dia, 2)) / (4 * (AnalysisMapping.xbeams[Convert.ToInt32(ShearSpanComboBox.SelectedIndex)]
                .FrameResults.ElementAt(0).Astr_sManual[Convert.ToInt32(ShearSectionComboBox.SelectedIndex)]))).ToString();
        }
    }
}
