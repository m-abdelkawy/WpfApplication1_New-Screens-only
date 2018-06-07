using ConsoleApplication1;
using SAP2000v20;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WpfApplication00_DesignProject.Classes;
using WW.Cad.IO;
using WW.Cad.Model;
using WW.Cad.Model.Entities;
using WW.Cad.Model.Tables;
using WW.Math;

namespace WpfApplication00_DesignProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        List<int> spanList = new List<int>();
        List<int> beamNames = new List<int>();

        List<string> loadCaseName = new List<string>();
        int nSpans;
        
        double[] spanValues;
        double[] comSpanValues; //comlative
        int[] rebarDiameterArr;
        int[] sections = new int[] { 1, 2, 3 };
        int[] shearSections = new int[] { 1, 2 };

        SapPoint[] points;
        SapFrameElement[] xbeams;

        SapRectSection B;

        SapModel mySapModel;
        /*------------------------------*/

        //LoadPattern
        List<string> loadPatternName = new List<string>();
        List<double> SelfWtMultiplier = new List<double>();
        List<eLoadPatternType> patternType = new List<eLoadPatternType>();


        List<SapLoadPattern> loadPatterns = new List<SapLoadPattern>();
        eCNameType ec = eCNameType.LoadCase;

        /*------------------------------*/

        //Load Combinations
        List<string> Combinations = new List<string>();
        List<double> loadFactorList = new List<double>();
        /*------------------------------*/
        /*----------Distributed Loads-----------*/
        List<string> distLoadPattern = new List<string>();
        List<LoadDir> distLoadDirVal = new List<LoadDir>();
        List<double> distLoadVals = new List<double>();
        List<string> DistLoadFrameElement = new List<string>();

        /*----------Point Loads--------*/
        List<string> concLoadPattern = new List<string>();
        List<double> concLoadVals = new List<double>();
        List<LoadDir> concLoadDirVal = new List<LoadDir>();
        List<string> concLoadFrameElement = new List<string>();
        List<double> relDistList = new List<double>();

        /*------------------------------*/


        List<SapFrameDistLoad> distLoads = new List<SapFrameDistLoad>();
        List<SapPointLoad> pointLoads = new List<SapPointLoad>();

        string[] comboArray;

        //Stirrup Arrays
        double[] stirDiaArr240 = new double[] { 8 };
        double[] stirDiaArr360 = new double[] { 10, 12, 14, 16 };
        double[] stirDiaArr400 = new double[] { 10, 12, 14, 16 };


        /*----------DistLoadShape Lists----------*/
        //Rectangle[] distLoadShapeArr;
        List<List<Rectangle>> distLoadShapes = new List<List<Rectangle>>();

        


        public MainWindow()
        {
            InitializeComponent();
        }
        private void nspantxtBox_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void nspantxtBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            nSpans = Convert.ToInt32(nspantxtBox.Text);
        }

        private void nspantxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            nSpans = Convert.ToInt32(nspantxtBox.Text);
            spanValues = new double[nSpans + 1];
            comSpanValues = new double[nSpans + 1];
        }

        private void spanComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            /*for (int i = 1; i < nSpans + 1; i++)
            {
                spanList.Add(i);
            }
            var spanComboBox = sender as ComboBox;
            spanComboBox.ItemsSource = spanList;
            spanComboBox.SelectedIndex = 0;*/
        }

        private void spanComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*var spanComboBox = sender as ComboBox;
            string value = spanComboBox.SelectedItem as string;
            //this.Title = value;

            //Array of span values
            spanValues[0] = 0;
            //lengthtxtBox.Text = $"span {Convert.ToInt32(spanComboBox.SelectedItem)}".ToString();*/
        }

        
        private void lengthtxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            

        }

        private void analysisBtn_Click(object sender, RoutedEventArgs e)
        {
            //file Path and file Name 
            string filePath = "C:\\CSiAPIexample";
            string fileName = "Model1";
            //Create Sap Model
            mySapModel = new SapModel(filePath, fileName);
            //initialize Model units KN, m, C:
            mySapModel.InitializeUnits(eUnits.kN_m_C);
            /*----------------------------------------------------------*/
            /*-----------------------Create Material-----------------------*/
            //Concrete
            SapMaterial concMat = new SapMaterial(mySapModel.MySapObjectModel, $"Fcu{fcutxtBox.Text}", eMatType.Concrete);
            concMat.SetNewMaterial($"Fcu{fcutxtBox.Text}", eMatType.Concrete);
            concMat.SetConcMat($"Fcu{fcutxtBox.Text}", Convert.ToDouble(fcutxtBox.Text));
            concMat.SetIsotropicProps($"Fcu{fcutxtBox.Text}", 220000, 0.20, 9.900E-06);
            concMat.SetWeight($"Fcu{fcutxtBox.Text}", 25);

            //Steel Rebar Material
            //SapMaterial rebarMat = new SapMaterial(mySapModel.MySapObjectModel, )

            /*----------------------------------------------------------*/

            //Creating Sections
            B = new SapRectSection(mySapModel.MySapObjectModel, $"B{Convert.ToDouble(btxtBox.Text)}X{Convert.ToDouble(ttxtBox.Text)}"
                , concMat, Convert.ToDouble(btxtBox.Text)
                , Convert.ToDouble(ttxtBox.Text), -1);
            B.SetRectSec();

            /*----------------------------------------------------------*/

            //Points

            points = new SapPoint[nSpans + 1];
            for (int i = 0; i < nSpans + 1; i++)
            {
                points[i] = new SapPoint(mySapModel.MySapObjectModel, comSpanValues[i], 0, 0);
            }
            /*----------------------------------------------------------*/

            //Beams in X-Direction:
            xbeams = new SapFrameElement[nSpans];
            for (int i = 0; i < nSpans; i++)
            {
                xbeams[i] = new SapFrameElement(mySapModel.MySapObjectModel, points[i], points[i + 1], B, $"{i + 1}", $"B{i + 1}");
            }
            /*----------------------------------------------------------*/
            
            //Initialize hinged joints
            for (int i = 0; i < nSpans + 1; i++)
            {
                points[i].SetRestraints(Restraints.Hinged);
            }

            //xBeamsComboBox.ItemsSource = xbeams;
            //xBeamsComboBox.SelectedIndex = 0;

            /*----------------------Load Patterns----------------------*/
            for (int i = 0; i < loadPatternName.Count; i++)
            {
                SapLoadPattern load = new SapLoadPattern(mySapModel.MySapObjectModel, patternType[i], loadPatternName[i]
                    , SelfWtMultiplier[i], true);

                loadPatterns.Add(load);
                load.AddLoadPattern();
            }
            /*----------------------Load Combinations----------------------*/
            for (int i = 0; i < Combinations.Count; i++)
            {
                mySapModel.MySapObjectModel.RespCombo.Add(Combinations[i], 0);
                
                mySapModel.MySapObjectModel.RespCombo.SetCaseList(Combinations[i], ref ec, loadCaseName[i]
                    , loadFactorList[i]);
            }
            /*----------------------Adding Distributed Loads----------------------*/
            for (int i = 0; i < distLoadVals.Count; i++)
            {
                SapFrameElement item = null;
                SapLoadPattern loadPat = null;
                for (int j = 0; j < nSpans; j++)
                {
                    if (DistLoadFrameElement[i] == xbeams[j].Label)
                    {
                        item = xbeams[j];
                    }
                }
                for (int j = 0; j < loadPatterns.Count; j++)
                {
                    if (distLoadPattern[i] == loadPatterns[j].Name)
                    {
                        loadPat = loadPatterns[j];
                    }
                }
                SapFrameDistLoad distload = new SapFrameDistLoad(loadPat, 1, distLoadDirVal[i], 0, 1, distLoadVals[i], distLoadVals[i], true);
                distLoads.Add(distload);

                item.SetDistributedLoad(distload);
            }
            /*----------------------Adding Point Loads----------------------*/
            for (int i = 0; i < concLoadVals.Count; i++)
            {
                SapFrameElement item = null;
                SapLoadPattern loadPat = null;
                for (int j = 0; j < nSpans; j++)
                {
                    if (concLoadFrameElement[i] == xbeams[j].Label)
                    {
                        item = xbeams[j];
                    }
                }
                for (int j = 0; j < loadPatterns.Count; j++)
                {
                    if (concLoadPattern[i] == loadPatterns[j].Name)
                    {
                        loadPat = loadPatterns[j];
                    }
                }
                SapPointLoad D1 = new SapPointLoad(loadPat, concLoadVals[i], ConsoleApplication1.Type.Force, concLoadDirVal[i]);

                pointLoads.Add(D1);
                item.SetFramePointLoad(D1, relDistList[i]);
            }

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
        private void loadComboComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var loadComboComboBox = sender as ComboBox;
            loadComboComboBox.ItemsSource = loadPatternName;
            loadComboComboBox.SelectedIndex = 0;
        }

        private void loadComboComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var loadComboComboBox = sender as ComboBox;
            string value = loadComboComboBox.SelectedIndex.ToString();

            var loadComboBoxSelectedItem = e.AddedItems[0] as string;

            //loadCaseName = loadComboBoxSelectedItem;
            //this.Title = value;
        }

        private void loadTypeComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            loadTypeComboBox.ItemsSource = Enum.GetValues(typeof(eLoadPatternType)).Cast<eLoadPatternType>();
        }

        private void loadComboBtn_Click(object sender, RoutedEventArgs e)
        {
            Combinations.Add(loadCombotxtBox.Text);
            loadFactorList.Add(Convert.ToDouble(loadFactortxtBox.Text));
            loadCaseName.Add((loadComboComboBox.SelectedItem)as string);

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        int assignBtnClicked = 0;
        int loadComboSelectedIndex = 0;
        int loadIndexShapeList1 = 0;
        int loadIndexShapeList2 = 1;

        private void assignLoadBtn_Click(object sender, RoutedEventArgs e)
        {

            //SapFrameElement item = xBeamsComboBox.SelectedItem as SapFrameElement;

            if (distLoadtxtBox.Text != null)
            {
                //Fill Dist Load Lists:
                DistLoadFrameElement.Add((xBeamsComboBox.SelectedIndex + 1).ToString());
                distLoadPattern.Add((loadComboComboBox2.SelectedValue) as string);
                distLoadDirVal.Add((LoadDir)(loadDirComboBox.SelectedValue));
                distLoadVals.Add(Convert.ToDouble(distLoadtxtBox.Text));
                /*-------------*/
                if (assignBtnClicked == 0 || (assignBtnClicked > 0 && (distLoadPattern[assignBtnClicked] == distLoadPattern[assignBtnClicked - 1])))
                {
                    loadComboSelectedIndex = loadComboComboBox2.SelectedIndex;
                    //Draw Loads
                    if (loadIndexShapeList2 != loadComboComboBox2.SelectedIndex)
                    {
                        for (int i = 0; i < distLoadShapes[loadIndexShapeList2].Count; i++)
                        {
                            LoadCanvas.Children.Remove(distLoadShapes[loadIndexShapeList2][i]);
                        }
                    }
                    
                    Rectangle r = new Rectangle();
                    r.Width = (comSpanValues[xBeamsComboBox.SelectedIndex + 1] - comSpanValues[xBeamsComboBox.SelectedIndex]) * 20;
                    r.Height = Convert.ToDouble(distLoadtxtBox.Text) * 20;
                    r.Fill = Brushes.Red;
                    r.Stroke = Brushes.Blue;
                    r.StrokeThickness = 4;
                    Canvas.SetBottom(r, 380);
                    Canvas.SetLeft(r, comSpanValues[xBeamsComboBox.SelectedIndex] * 20);
                    //Canvas.SetRight(r, comSpanValues[xBeamsComboBox.SelectedIndex + 1] * 20);
                    distLoadShapes[loadComboComboBox2.SelectedIndex].Add(r);
                    LoadCanvas.Children.Add(r);

                    loadIndexShapeList1 = loadComboComboBox2.SelectedIndex;
                    loadComboSelectedIndex = 0;
                }
                else if (assignBtnClicked > 0 && (distLoadPattern[assignBtnClicked] != distLoadPattern[assignBtnClicked - 1]))
                {
                    loadComboSelectedIndex = loadComboComboBox2.SelectedIndex;
                    loadIndexShapeList2 = loadComboComboBox2.SelectedIndex;
                    //DrawLoads:
                    if (loadIndexShapeList1 != loadComboComboBox2.SelectedIndex)
                    {
                        for (int i = 0; i < distLoadShapes[loadIndexShapeList1].Count; i++)
                        {
                            LoadCanvas.Children.Remove(distLoadShapes[loadIndexShapeList1][i]);
                        }
                    }
                    

                    Rectangle r = new Rectangle();
                    r.Width = (comSpanValues[xBeamsComboBox.SelectedIndex + 1] - comSpanValues[xBeamsComboBox.SelectedIndex]) * 20;
                    r.Height = Convert.ToDouble(distLoadtxtBox.Text) * 20;
                    r.Fill = Brushes.Red;
                    r.Stroke = Brushes.Blue;
                    r.StrokeThickness = 4;
                    Canvas.SetBottom(r, 380);
                    Canvas.SetLeft(r, comSpanValues[xBeamsComboBox.SelectedIndex] * 20);
                    //Canvas.SetRight(r, comSpanValues[xBeamsComboBox.SelectedIndex + 1] * 20);
                    distLoadShapes[loadComboComboBox2.SelectedIndex].Add(r);
                    LoadCanvas.Children.Add(r);

                    loadComboSelectedIndex = 0;
                }



                assignBtnClicked += 1;


                //SapFrameDistLoad distload = new SapFrameDistLoad(loadComboComboBox2.SelectedValue as SapLoadPattern, 1
                //    , (LoadDir)loadDirComboBox.SelectedValue, 0, 1, Convert.ToDouble(distLoadtxtBox.Text)
                //    , Convert.ToDouble(distLoadtxtBox.Text), true);
                //distLoads.Add(distload);

                //item.SetDistributedLoad(distload);
            }



        }

        /*private void xBeamsComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var xBeamsComboBox = sender as ComboBox;
            xBeamsComboBox.ItemsSource = xbeams;
            xBeamsComboBox.SelectedIndex = 0;
        }*/

        private void xBeamsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var xBeamsComboBox = sender as ComboBox;
            string value = xBeamsComboBox.SelectedItem as string;
            /*------------------------------------------------------*/

        }
        private void xBeamsComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var xBeamsComboBox2 = sender as ComboBox;
            string value = xBeamsComboBox2.SelectedItem as string;
        }

        private void xBeamsComboBox_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void xBeamsComboBox2_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void loadDirComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            loadDirComboBox.ItemsSource = Enum.GetValues(typeof(LoadDir)).Cast<LoadDir>();
        }

        private void loadDirComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var loadDirComboBox = sender as ComboBox;
            string value = loadDirComboBox.SelectedIndex.ToString();
        }

        

        private void designComboComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var designComboComboBox = sender as ComboBox;
            string value = designComboComboBox.SelectedItem as string;
        }

        private void runAnalysisBtn_Click(object sender, RoutedEventArgs e)
        {
            mySapModel.SaveAndRun();

            int cnt = mySapModel.MySapObjectModel.RespCombo.Count();
            comboArray = new string[cnt];
            mySapModel.MySapObjectModel.RespCombo.GetNameList(ref cnt, ref comboArray);

            designComboComboBox.ItemsSource = comboArray;
            designComboComboBox.SelectedIndex = 0;

        }

        private void DesignBtn_Click(object sender, RoutedEventArgs e)
        {
            int bindex = 0;
            foreach (var item in xbeams)
            {
                //clear all case and combo output selections:
                int ret1 = mySapModel.MySapObjectModel.Results.Setup.DeselectAllCasesAndCombosForOutput();

                //set case and combo output selections
                int ret2 = mySapModel.MySapObjectModel.Results.Setup.SetComboSelectedForOutput(designComboComboBox.SelectedItem as string);

                

                xbeams[bindex].FrameResults.Add(new SapFrameResult(mySapModel.MySapObjectModel, item.Label));

                //add sections created:
                xbeams[bindex].Sec.Add(B);
                bindex++;

            }
            /*----------------------------------------------------------*/
            //Design:
            double fcu = Convert.ToDouble(fcutxtBox.Text);
            double fy = Convert.ToDouble(fytxtBox.Text);
            double fystr = Convert.ToDouble(fstirtxtBox.Text);
            int nBranches = Convert.ToInt32(nBranchestxtBox.Text);

            BeamDesign design = new BeamDesign(xbeams, fy, fystr, fcu, nBranches);
            
        }

        private void rebarDiaComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            rebarDiameterArr = new int[] { 8, 10, 12, 14, 16, 18, 20, 22, 25, 28, 32 };
            var rebarDiaComboBox = sender as ComboBox;
            rebarDiaComboBox.ItemsSource = rebarDiameterArr;
            rebarDiaComboBox.SelectedIndex = 0;
        }

        

        private void stirrupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void xBeamDesignComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var xBeamDesignComboBox = sender as ComboBox;
            string value = xBeamDesignComboBox.SelectedItem as string;
        }

        private void nspantxtBox_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void EnternSpansBtn_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i < nSpans + 1; i++)
            {
                spanList.Add(i);
            }

            var EnternSpansBtn = sender as ComboBox;
            spanComboBox.ItemsSource = spanList;
            spanComboBox.SelectedIndex = 0;

            xBeamsComboBox.ItemsSource = spanList;
            xBeamsComboBox.SelectedIndex = 0;

            xBeamsComboBox2.ItemsSource = spanList;
            xBeamsComboBox2.SelectedIndex = 0;

            xBeamDesignComboBox.ItemsSource = spanList;
            xBeamDesignComboBox.SelectedIndex = 0;
            /*---------------------------------------*/
            string value = spanComboBox.SelectedItem as string;
            //this.Title = value;

            //Array of span values
            spanValues[0] = 0;
            //lengthtxtBox.Text = $"span {Convert.ToInt32(spanComboBox.SelectedItem)}".ToString();
        }

        private void stirrupComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            rebarDiameterArr = new int[] { 8, 10, 12, 14, 16, 18, 20, 22, 25, 28, 32 };
            var stirrupComboBox = sender as ComboBox;
            stirrupComboBox.ItemsSource = rebarDiameterArr;
            stirrupComboBox.SelectedIndex = 0;
        }

        private void SectionComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var SectionComboBox = sender as ComboBox;
            SectionComboBox.ItemsSource = sections;
            SectionComboBox.SelectedIndex = 0;
        }

        private void SectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void calcAsBtn_Click(object sender, RoutedEventArgs e)
        {
            AsTxtBlock.Text = xbeams[Convert.ToInt32(xBeamDesignComboBox.SelectedIndex)]
                .FrameResults.ElementAt(0).Asteel[Convert.ToInt32(SectionComboBox.SelectedIndex)].ToString();

            rebarNoTxtBlock.Text = ((Convert.ToDouble(AsTxtBlock.Text) * 4)
                / (Math.PI * Math.Pow(Convert.ToDouble(rebarDiaComboBox.SelectedItem)/10, 2))).ToString();

            //rebarNoTxtBlock.Text = (rebarDiaComboBox.SelectedItem).ToString();
        }

        private void calcSBtn_Click(object sender, RoutedEventArgs e)
        {
            double dia = Convert.ToDouble(stirrupComboBox.SelectedItem);
            spacingTxtBlock.Text = ((Math.PI * Math.Pow(dia, 2)) / (4 * (xbeams[Convert.ToInt32(xBeamDesignComboBox.SelectedIndex)]
                .FrameResults.ElementAt(0).Astr_sManual[Convert.ToInt32(ShearSectionComboBox.SelectedIndex)]))).ToString();
        }

        private void ShearSectionComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var ShearSectionComboBox = sender as ComboBox;
            ShearSectionComboBox.ItemsSource = shearSections;
            ShearSectionComboBox.SelectedIndex = 0;
        }

        private void ShearSectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void saveDXFBtn_Click(object sender, RoutedEventArgs e)
        {
            DxfModel model = new DxfModel(DxfVersion.Dxf14);

            double thickness = Convert.ToDouble(ttxtBox.Text);
            /////////////
            
            ///////////
            //Comulative spanVals
            double[] comSpanVals = new double[nSpans + 1];
            comSpanVals[0] = 0;
            for (int i = 1; i < comSpanVals.Length; i++)
            {
                comSpanVals[i] = comSpanVals[i - 1] + spanValues[i];
            }

            /*---------*/
            //Points
            Point2D[] startPointsBot = DXFPoints.BottomStartPoints(nSpans, comSpanVals);
            Point2D[] endPointsBot = DXFPoints.BottomEndPoints(nSpans, comSpanVals);

            Point2D[] startPointsTop = DXFPoints.TopStartPoints(nSpans, thickness, comSpanVals);
            Point2D[] endPointsTop = DXFPoints.TopEndPoints(nSpans, thickness, comSpanVals);


            /*---------Lines---------*/
            DXFLines.ConstructBottomLines(model, nSpans, startPointsBot, endPointsBot);
            DXFLines.ConstructTopLines(model, nSpans, startPointsTop, endPointsTop);

            DXFLines.ConstructColLines(model, nSpans, thickness, startPointsBot, endPointsBot, startPointsTop, endPointsTop);
            DXFLines.ConstructGrids(model, nSpans, thickness, comSpanVals);

            /*---------Rebar---------*/
            DxfLine[] BottomRFT = DXFRebar.BotRFT(model, nSpans, startPointsBot, endPointsBot);

            double[] Ln = DXFRebar.Lnet(nSpans, startPointsTop, endPointsTop);

            DxfLine[] TopSpan = DXFRebar.TopSpanRFT(model, nSpans, thickness, Ln, startPointsTop, endPointsTop);

            DxfLine[] TopSupport = DXFRebar.TopSupportRFT(model, nSpans, thickness, Ln, startPointsTop, endPointsTop);

            DXFRebar.Legs(model, nSpans, thickness, BottomRFT, TopSpan);

            /*---------Stirrups-------*/
            DxfLine[,] stirrupsLeft = DXFRebar.StirrupsLeftSec(model, nSpans, thickness, startPointsBot);

            DxfLine[,] stirrupsMidspan = DXFRebar.StirrupsMidSpanSec(model, nSpans, thickness, startPointsBot, endPointsBot);

            DxfLine[,] stirrupsRight = DXFRebar.StirrupsRightSec(model, nSpans, thickness, endPointsBot);

            /*------------------------------Annotation------------------------------*/
            DXFAnnotation.AnnotationStirLeft(model, nSpans, thickness, stirrupsLeft);
            DXFAnnotation.AnnotationStirMidSpan(model, nSpans, thickness, stirrupsMidspan);
            DXFAnnotation.AnnotationStirRight(model, nSpans, thickness, stirrupsRight);

            /*------------------------------Dimensions------------------------------*/
            DXFDimClass.DrawGridDims(model, nSpans, comSpanVals);
            DXFDimClass.DrawLnetDims(model, nSpans, startPointsBot, endPointsBot, comSpanVals);
            DXFDimClass.DrawTopRFTRightDims(model, nSpans, thickness, Ln, startPointsTop, comSpanVals);
            DXFDimClass.DrawTopRFTLeftDims(model, nSpans, thickness, Ln, endPointsTop, comSpanVals);

            /*------------------------------Text------------------------------*/
            double fystr = Convert.ToDouble(fstirtxtBox.Text);

            //01. RFT bottom Text
            int[] nRebarBot = DXFTextClass.GetnRebarBotArr(xbeams);
            double[] chosenDiameterBot = DXFTextClass.GetChosenDiameterArr(xbeams);
            DXFTextClass.BottomRFTTxt(model, nSpans, nRebarBot, chosenDiameterBot, Ln, startPointsBot);

            //02. RFT Top Support Text
            int[] nRebarTopSupportArr = DXFTextClass.GetnRebarTopSupportArr(xbeams);
            double[] chosenDiameterTopSupport = DXFTextClass.GetChosenDiameterTopSupportArr(xbeams);
            DXFTextClass.TopRFTSupportTxt(model, nSpans, thickness, nRebarTopSupportArr, chosenDiameterTopSupport, TopSpan, TopSupport);

            //03. Stirrups Left Section
            {
                double[] spacingLeftArr = DXFTextClass.GetSpacingLeftArr(xbeams, stirDiaArr240, stirDiaArr360, stirDiaArr400);
                int[] spacingLeftIndexes = DXFTextClass.GetSpacingLeftArrIndexes(xbeams, stirDiaArr240, stirDiaArr360, stirDiaArr400);
                DXFTextClass.StirrupLeftTxt(model, nSpans, stirrupsLeft, spacingLeftArr, spacingLeftIndexes, stirDiaArr240, stirDiaArr360, stirDiaArr400);
                
            }
            //05. Stirrups Right Section
            {
                double[] spacingRightArr = DXFTextClass.GetSpacingRightArr(xbeams, stirDiaArr240, stirDiaArr360, stirDiaArr400);
                int[] spacingRightIndexes = DXFTextClass.GetSpacingLeftArrIndexes(xbeams, stirDiaArr240, stirDiaArr360, stirDiaArr400);
                DXFTextClass.StirrupRightTxt(model, nSpans, stirrupsRight, spacingRightArr, spacingRightIndexes, stirDiaArr240, stirDiaArr360, stirDiaArr400);

            }


            ////04. Stirrups MidSpan
            //DXFTextClass.StirrupMidSpanTxt(model, nSpans, stirrupsMidspan);

            ////05. Stirrups Right Section
            //double[] spacingRightSec = DXFTextClass.GetSpacingRightArr(xbeams);
            //DXFTextClass.StirrupRightTxt(model, nSpans, stirrupsRight, spacingRightSec);


            /*------------------------------Create DXF File------------------------------*/
            //Create DXF File
            DxfWriter.Write("Test12.dxf", model, true);

        }

        const double ScaleRate = 1.1;
        private void LoadCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                st.ScaleX *= ScaleRate;
                st.ScaleY *= ScaleRate;
            }
            else
            {
                st.ScaleX /= ScaleRate;
                st.ScaleY /= ScaleRate;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        static int addSpanBtnClicked = 0;
        private void AddSpanBtn_Click(object sender, RoutedEventArgs e)
        {
            spanValues[Convert.ToInt32(spanComboBox.SelectedItem)] = Convert.ToInt32(lengthtxtBox.Text);
            comSpanValues[Convert.ToInt32(spanComboBox.SelectedItem)] = comSpanValues[Convert.ToInt32(spanComboBox.SelectedItem) - 1]
                + spanValues[Convert.ToInt32(spanComboBox.SelectedItem)];

            //Draw Lines
            Line[] BeamLine = new Line[nSpans];
            Line l1 = new Line();
            l1.Stroke = Brushes.Red;
            l1.StrokeThickness = 4;
            l1.X1 = comSpanValues[addSpanBtnClicked] * 20;
            l1.Y1 = 120;

            l1.X2 = comSpanValues[addSpanBtnClicked + 1] * 20;
            l1.Y2 = 120;

            LoadCanvas.Children.Add(l1);

            //Draw Rectangle
            Path triangle = new Path();
            triangle.Fill = Brushes.Orange;
            triangle.Stroke = Brushes.Black;

            PathFigure pf = new PathFigure();
            pf.StartPoint = new Point(comSpanValues[addSpanBtnClicked] * 20, 120);
            pf.IsClosed = true;
            pf.Segments.Add(new LineSegment(new Point(comSpanValues[addSpanBtnClicked] *20 + 10, 130), true));
            pf.Segments.Add(new LineSegment(new Point(comSpanValues[addSpanBtnClicked] * 20 - 10, 130), true));

            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);
            triangle.Data = pg;
            LoadCanvas.Children.Add(triangle);

            addSpanBtnClicked += 1;
            

        }

        private void assignConcLoadBtn_Click(object sender, RoutedEventArgs e)
        {
            if (concLoadtxtBox.Text != null)
            {
                //Fill Concentrated Load Lists:
                concLoadFrameElement.Add((xBeamsComboBox2.SelectedIndex + 1).ToString());
                concLoadPattern.Add((loadComboComboBox2_1.SelectedValue) as string);
                concLoadDirVal.Add((LoadDir)(loadDirComboBox_2.SelectedValue));
                concLoadVals.Add(Convert.ToDouble(concLoadtxtBox.Text));
                relDistList.Add(Convert.ToDouble(relDisttxtBox));
                /*-------------*/





                //SapPointLoad D1 = new SapPointLoad(loadComboComboBox2.SelectedValue as SapLoadPattern, Convert.ToDouble(concLoadtxtBox.Text)
                //    , ConsoleApplication1.Type.Force, (LoadDir)loadDirComboBox.SelectedValue);

                //pointLoads.Add(D1);
                //item.SetFramePointLoad(D1, Convert.ToDouble(relDisttxtBox.Text));
            }
        }

        private void loadDirComboBox_2_Loaded(object sender, RoutedEventArgs e)
        {
            loadDirComboBox_2.ItemsSource = Enum.GetValues(typeof(LoadDir)).Cast<LoadDir>();
        }

        private void loadDirComboBox_2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var loadDirComboBox_2 = sender as ComboBox;
            string value = loadDirComboBox_2.SelectedIndex.ToString();
        }

        
    }
}
