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
using Design.Presentation.Geometry;
using Microsoft.Win32;
using System.IO;
using Design.Core.Sap;
using System.Globalization;

namespace Design.Presentation
{
    /// <summary>
    /// Interaction Geometry for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //lists

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
        public static List<string> LoadCasesToShowLoads { get; set; }
        = new List<string>();

        #region RFT 
        public GeometryEngine GeometryEngineRFT { get; set; }

        #endregion
        public MainWindow()
        {
            InitializeComponent();
            GeometryEngine = new GeometryEngine();
            GeometryEngine.GCanvas.Canvas = canvas_Geometry;
            LoadCasesToShowLoads = AnalysisMapping.loadPatternName;
            ShowLoadComboBox.ItemsSource = LoadCasesToShowLoads;

            //RFT Canvas
            GeometryEngineRFT = new GeometryEngine();
            GeometryEngineRFT.GCanvas.Canvas = canvas_Design;


        }



        #region Btn_Events

        private void Btn_editBeam_Click(object sender, RoutedEventArgs e)
        {
            var ge = new GeometryEditor(GeometryEditorVM.GeometryEditor, GeometryEngine);
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
            AnalysisMapping.CalcSpanValues(FlexureSpanComboBox, ShearSpanComboBox);
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
            AnalysisMapping.RunModel(DesignCombinationComboBox);











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


        Hinged hinged;
        Roller roller;
        Fixed fixd;
        Arrow arrow;
        ArrowLoad arrowLoad;
        GLine l;
        GCircle c;
        GText gText;
        private void Btn_DrawLine_Click(object sender, RoutedEventArgs e)
        {
            // GeometryEngine.Remove("Supports");

            // var traingle = new GTriangle(GeometryEngine.GCanvas, new Point(60, 60), 20);
            l = new GLine(GeometryEngine.GCanvas, new Point(60, 60), new Point(100, 200));
            c = new GCircle(GeometryEngine.GCanvas, new Point(200, 200), 50);
            c.Fill = Brushes.Green;
            hinged = new Hinged(GeometryEngine.GCanvas, new Point(500, 500));
            hinged.Fill = Brushes.Transparent;
            hinged.Stroke = Brushes.Red;
            hinged.StrokeThickness = 2;
            roller = new Roller(GeometryEngine.GCanvas, new Point(160, 60));
            roller.Fill = Brushes.Transparent;
            roller.StrokeThickness = 2;
            //var rectangle = new GRectangle(GeometryEngine.GCanvas, 30, 15, new Point(140, 60+15));
            fixd = new Fixed(GeometryEngine.GCanvas, new Point(100, 60), 20);
            fixd.Fill = Brushes.Orange;
            fixd.StrokeThickness = 2;
            arrow = new Arrow(GeometryEngine.GCanvas, new Point(220, 60), 20);
            arrow.Stroke = Brushes.Yellow;
            arrow.StrokeThickness = 4;
            arrowLoad = new ArrowLoad(GeometryEngine.GCanvas, new Point(100, 120), new Point(200, 120), 20);
            arrowLoad.Stroke = Brushes.Blue;
            arrowLoad.StrokeThickness = 2;

            gText = new GText(GeometryEngine.GCanvas, new Point(60, 60), "Hello Text!");
            gText.Text = "Hello Text!";
            gText.Size = 50;
            gText.Render();
            
          
            //arrow.Rotate(45);
            //render on Screen
            //GeometryEngine.RenderAll();
            //   GeometryEngine.Render(new List<GShape>() { roller, hinged, fixd });
            GeometryEngine.Shapes["Beams"].Add(c);
            GeometryEngine.Shapes["Beams"].Add(arrow);
            GeometryEngine.Shapes["Supports"].Add(arrowLoad);

            GeometryEngine.Shapes["Supports"].Add(roller);
            GeometryEngine.Shapes["Supports"].Add(fixd);
            GeometryEngine.Shapes["Supports"].Add(l);
            GeometryEngine.Shapes["Supports"].Add(hinged);
          //  GeometryEngine.Shapes["Supports"].Add(gText);
            // GeometryEngine.Render("Beams");
            // GeometryEngine.Render("Supports");
            l.Stroke = Brushes.Red;
            l.StrokeThickness = 4;
           
            GeometryEngine.RenderAll();
          
        }

        private void bBtn_Show_Click(object sender, RoutedEventArgs e)
        {

            //  GeometryEngine.Hide(new List<GShape>() { roller, fixd, arrow,arrowLoad,hinged });
            //GeometryEngine.Remove("Beams");
            //l.SetScale(2);
            //hinged.SetScale(2);
            //fixd.SetScale(2);
            //roller.SetScale(2);
            //arrowLoad.SetScale(1.5);
          
           //gText.SetTranslate(50, 50);
           
           // gText.SetScale(1.5);
            gText.Rotate(45);


        }

        private void Btn_DistLoadAssinment_Click(object sender, RoutedEventArgs e)
        {
            var distLoadWin = new DistLoadAssignmentWindow(GeometryEngine);
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
        {//----------------Consider Revision
            int[] secIndex = new int[GeometryEditorVM.GeometryEditor.GridData.Count];
            for (int i = 0; i < GeometryEditorVM.GeometryEditor.GridData.Count; i++)
            {
                for (int j = 0; j < AnalysisMapping.SapSectionsArr.Length; j++)
                {
                    if (GeometryEditorVM.GeometryEditor.GridData[i].SelectedSection.Width == AnalysisMapping.SapSectionsArr[j].B
                        && GeometryEditorVM.GeometryEditor.GridData[i].SelectedSection.Depth == AnalysisMapping.SapSectionsArr[j].T)
                    {
                        secIndex[i] = j;
                    }
                }
            }
            int bindex = 0;
            foreach (var item in AnalysisMapping.xbeams)
            {
                //clear all case and combo output selections:
                int ret1 = AnalysisMapping.mySapModel.MySapObjectModel.Results.Setup.DeselectAllCasesAndCombosForOutput();

                //set case and combo output selections
                int ret2 = AnalysisMapping.mySapModel.MySapObjectModel.Results.Setup.SetComboSelectedForOutput(DesignCombinationComboBox.SelectedItem as string);



                AnalysisMapping.xbeams[bindex].FrameResults.Add(new SapFrameResult(AnalysisMapping.mySapModel.MySapObjectModel, item.Label));

                //add sections created:
                AnalysisMapping.xbeams[bindex].Sec.Add(AnalysisMapping.SapSectionsArr[secIndex[bindex]]);
                bindex++;
            }
            /*----------------------------------------------------------*/
            //Design:
            double fcu = Convert.ToDouble(MaterialEditorVM.Material.Fcu);
            double fy = Convert.ToDouble(MaterialEditorVM.Material.Fy);
            double fystr = Convert.ToDouble(MaterialEditorVM.Material.FyStirrups);
            int nBranches = Convert.ToInt32(MaterialEditorVM.Material.NoOfBranches);

            BeamDesign design = new BeamDesign(AnalysisMapping.xbeams, fy, fystr, fcu, nBranches);


        }

        private void FlexureSpanComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var FlexureSpanComboBox = sender as ComboBox;
            string value = FlexureSpanComboBox.SelectedItem as string;
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

        private void ShearSpanComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ShearSpanComboBox = sender as ComboBox;
            string value = ShearSpanComboBox.SelectedItem as string;
        }

        private void Btn_LoadShow_Click(object sender, RoutedEventArgs e)
        {


            GeometryEngine.Remove("DistributedLoad");
            GeometryEngine.Remove("ConcentratedLoad");

            List<double> GComSpanValues = new List<double>();
            GComSpanValues.Clear();
            GComSpanValues.Add(0);
            double cumulativeSpans = 0;
            for (int i = 0; i < GeometryEditorVM.GeometryEditor.NumberOfSpans; i++)
            {
                cumulativeSpans += GeometryEditorVM.GeometryEditor.GridData[i].Span;

                GComSpanValues.Add(cumulativeSpans);
            }
            //List<string> Dead = new List<string>();
            //List<string> Live = new List<string>();
            //List<string> SD = new List<string>();

            int[] Js = new int[DistLoadAssignmentViewModel.DistLoadModelStaticCollection.Count];
            int YPosition = 100;

            //Draw Distributed Load
            for (int i = 0; i < DistLoadAssignmentViewModel.DistLoadModelStaticCollection.Count; i++)
            {
                double startX = GComSpanValues[DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].selectedSpanNo - 1] * 20;
                double endX = GComSpanValues[DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].selectedSpanNo] * 20;
                if (DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].selectedLoadCase
                    == (ShowLoadComboBox.SelectedItem).ToString())
                {
                    GeometryEngine.Shapes["DistributedLoad"].Add(new ArrowLoad(GeometryEngine.GCanvas
                                   , new Point(startX, 100), new Point(endX, 100)
                                   , DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].DistLoadVal * 0.50));

                }
            }
            List<double> spanList = GeometryEditorVM.GeometryEditor.GridData.Select(d => d.Span).ToList();
            //Draw Point Load
            for (int i = 0; i < PointLoadAssignmentViewModel.PointLoadModelStaticCollection.Count; i++)
            {
                //double startX = GComSpanValues[DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].selectedSpanNo - 1] * 20;
                //double endX = GComSpanValues[DistLoadAssignmentViewModel.DistLoadModelStaticCollection[i].selectedSpanNo] * 20;
                if (PointLoadAssignmentViewModel.PointLoadModelStaticCollection[i].selectedLoadCase
                    == (ShowLoadComboBox.SelectedItem).ToString() && ShowLoadComboBox.SelectedItem != null)
                {
                    var arrow = new Arrow(GeometryEngine.GCanvas,
                        new Point(GComSpanValues[PointLoadAssignmentViewModel.PointLoadModelStaticCollection[i].selectedSpanNo - 1] * 20
                        + PointLoadAssignmentViewModel.PointLoadModelStaticCollection[i].RelativeDistance *
                        spanList[PointLoadAssignmentViewModel.PointLoadModelStaticCollection[i].selectedSpanNo - 1] * 20, 100), 30);
                    arrow.Rotate(180);
                    GeometryEngine.Shapes["ConcentratedLoad"].Add(arrow);


                }
            }
            GeometryEngine.Render("DistributedLoad");
            GeometryEngine.Render("ConcentratedLoad");
            //GeometryEngine.RenderAll();
        }

        private void ShowLoadComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Btn_RFT_Click(object sender, RoutedEventArgs e)
        {
            GeometryEngineRFT.Remove("RFT");

            /*----------RFT Visualization---------*/
            RFTCanvas.CalcSpanVals();
            RFTCanvas.CalcComSpanVals(GeometryEditorVM.GeometryEditor.NumberOfSpans);
            RFTCanvas.CalcThickness();

            //Points  /*--------the Order is Important-----------*/
            RFTCanvas.ConstructTopStartPoints(20);
            RFTCanvas.ConstructTopEndPoints(20);

            RFTCanvas.ConstructBotStartPoints(20);
            RFTCanvas.ConstructBotEndPoints(20);



            //Lines
            RFTCanvas.ConstructTopLines(GeometryEngineRFT.GCanvas, GeometryEngineRFT);
            RFTCanvas.ConstructBotLines(GeometryEngineRFT.GCanvas, GeometryEngineRFT);
            RFTCanvas.ConstructColLines(GeometryEngineRFT.GCanvas, GeometryEngineRFT, 20);

            //RFT Lines
            RFTCanvas.BotRFT(GeometryEngineRFT.GCanvas, GeometryEngineRFT, 20);
            RFTCanvas.TopRFT(GeometryEngineRFT.GCanvas, GeometryEngineRFT, 20);

            //Stirrups
            RFTCanvas.LeftSecStirrups(GeometryEngineRFT.GCanvas, GeometryEngineRFT, 20);
            RFTCanvas.RightSecStirrups(GeometryEngineRFT.GCanvas, GeometryEngineRFT, 20);

            GeometryEngineRFT.RenderAll();

        }

        private void Btn_ClrRFTCanvas_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
