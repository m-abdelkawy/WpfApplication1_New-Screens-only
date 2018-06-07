//using System;

//using System.Collections.Generic;

//using System.Linq;

//using System.Text;

//using SAP2000v20;



//namespace Desing.Core.Sap

//{
//    public class Program
//    {
//        static void Main(string[] args)
//        {
//            //Beams in XZ plan
//            Console.Write("No. of Spans: ");
//            int nSpans = Convert.ToInt32(Console.ReadLine());

//            double[] spanValues = new double[nSpans + 1];
//            spanValues[0] = 0;
//            for (int i = 1; i < nSpans + 1; i++)
//            {
//                Console.Write($"Enter Span no{i}: ");
//                spanValues[i] = Convert.ToDouble(Console.ReadLine());
//            }

//            //Comulative SpanValues
//            double[] comSpanValues = new double[nSpans + 1];
//            comSpanValues[0] = 0;
//            for (int i = 1; i < comSpanValues.Length; i++)
//            {
//                comSpanValues[i] = comSpanValues[i - 1] + spanValues[i];
//            }

//            //check spans
//            /*for (int i = 0; i < comSpanValues.Length; i++)
//            {
//                Console.WriteLine(comSpanValues[i]);
//            }

//            Console.ReadLine();*/


//            //file Path and file Name 
//            string filePath = "C:\\CSiAPIexample";
//            string fileName = "Model1";

//            //Create Sap Model
//            SapModel mySapModel = new SapModel(filePath, fileName);
//            //initialize Model units KN, m, C:
//            mySapModel.InitializeUnits(eUnits.kN_m_C);
//            /*----------------------------------------------------------*/

//            //Creating Material
//            SapMaterial concMat = new SapMaterial(mySapModel.MySapObjectModel, "Fcu25", eMatType.Concrete);
//            concMat.SetNewMaterial("Fcu25", eMatType.Concrete);
//            concMat.SetConcMat("Fcu25", 250);
//            concMat.SetIsotropicProps("Fcu25", 220000, 0.20, 9.900E-06);
//            concMat.SetWeight("Fcu25", 25);
//            /*----------------------------------------------------------*/

//            //Creating Sections
//            SapRectSection B250X600 = new SapRectSection(mySapModel.MySapObjectModel, "B300X600", concMat, 0.25, 0.60, -1);
//            B250X600.SetRectSec();
//            //Set Modifiers

//            /*----------------------------------------------------------*/
//            //for testing points:
//            /*for (int i = 0; i < nSpans; i++)
//            {
//                mySapModel.MySapObjectModel.FrameObj.AddByCoord(0, 0, 0, 0, 0, 0, ref );

//                Console.Write($"Enter Span no{i + 1}: ");
//                spanValues[i] = Convert.ToDouble(Console.ReadLine());
//            }*/
//            /*----------------------------------------------------------*/

//            //Points

//            SapPoint[] points = new SapPoint[nSpans + 1];
//            for (int i = 0; i < nSpans + 1; i++)
//            {
//                points[i] = new SapPoint(mySapModel.MySapObjectModel, comSpanValues[i], 0, 0);
//            }

//            /*----------------------------------------------------------*/

//            //Beams in X-Direction:
//            SapFrameElement[] xbeams = new SapFrameElement[nSpans];
//            for (int i = 0; i < nSpans; i++)
//            {
//                xbeams[i] = new SapFrameElement(mySapModel.MySapObjectModel, points[i], points[i + 1], B250X600, $"{i + 1}", $"B{i + 1}");
//            }

//            /*----------------------------------------------------------*/
//            //Initialize hinged joints
//            for (int i = 0; i < nSpans + 1; i++)
//            {
//                points[i].SetRestraints(Restraints.Hinged);
//            }

//            /*----------------------------------------------------------*/
//            //Add Load Patterns
//            List<SapLoadPattern> loadPatterns = new List<SapLoadPattern>();
//            SapLoadPattern DeadLoad = new SapLoadPattern(mySapModel.MySapObjectModel, eLoadPatternType.Dead, "Dead", 1, true);
//            SapLoadPattern LiveLoad = new SapLoadPattern(mySapModel.MySapObjectModel, eLoadPatternType.Live, "Live", 0, true);


//            loadPatterns.Add(DeadLoad);
//            loadPatterns.Add(LiveLoad);

//            DeadLoad.AddLoadPattern();
//            LiveLoad.AddLoadPattern();


//            mySapModel.MySapObjectModel.RespCombo.Add("Ult", 0);
//            eCNameType ec = eCNameType.LoadCase;
//            mySapModel.MySapObjectModel.RespCombo.SetCaseList("Ult", ref ec, "Dead", 1.4);
//            mySapModel.MySapObjectModel.RespCombo.SetCaseList("Ult", ref ec, "Live", 1.6);

//            /*----------------------------------------------------------*/
//            //Distributed Loads
//            SapFrameDistLoad deadDistLoad = new SapFrameDistLoad(DeadLoad, 1, LoadDir.gravityDir, 0, 1, 5, 5, true);
//            SapFrameDistLoad liveDistLoad = new SapFrameDistLoad(LiveLoad, 1, LoadDir.gravityDir, 0, 1, 2, 2, true);

//            /*----------------------------------------------------------*/
//            //For Testing Load Functtions:(Delete Later)

//            /*----------------------------------------------------------*/

//            //Assign Load to Beams:
//            foreach (var item in xbeams)
//            {
//                item.SetDistributedLoad(deadDistLoad);
//                item.SetDistributedLoad(liveDistLoad);
//            }

//            /*----------------------------------------------------------*/
//            //Point Loads:
//            SapPointLoad D1 = new SapPointLoad(DeadLoad, 5, Type.Force, LoadDir.gravityDir);
//            foreach (var item in xbeams)
//            {
//                item.SetFramePointLoad(D1, 0.50);
//            }
//            /*----------------------------------------------------------*/

//            //to Delete Later(SaveModel)
//            mySapModel.SaveAndRun();

//            int cnt = mySapModel.MySapObjectModel.RespCombo.Count();


//            string[] comboArray = new string[cnt];
//            mySapModel.MySapObjectModel.RespCombo.GetNameList(ref cnt, ref comboArray);

//            /*----------------------------------------------------------*/
//            // Results:

//            #region Trial
//            /*int nResults = 2;
//            string[] frameName = new string[2];
//            string[] frameObjName = new string[2];
//            double[] frameObjStation = new double[2];
//            //frameObjStation[0] = 0.5;

//            string[] frameElmName = new string[2];
//            double[] frameElmStation = new double[2];
//            //frameElmStation[0] = 0.50;

//            string[] loadCase = new string[2];
//            //loadCase[0] = "Dead";

//            string[] stepType = new string[2];
//            double[] stepNumber = new double[2];

//            //Internal Forces
//            double[] p = new double[2];
//            double[] v2 = new double[2];
//            double[] v3 = new double[2];
//            double[] t = new double[2];
//            double[] m2 = new double[2];
//            double[] m3 = new double[2];*/
//            #endregion

//            int bindex = 0;
//            foreach (var item in xbeams)
//            {
//                //clear all case and combo output selections:
//                int ret1 = mySapModel.MySapObjectModel.Results.Setup.DeselectAllCasesAndCombosForOutput();

//                //set case and combo output selections
//                int ret2 = mySapModel.MySapObjectModel.Results.Setup.SetComboSelectedForOutput("Ult");

//                #region Trial
//                /*mySapModel.MySapObjectModel.Results.FrameForce(item.Name, eItemTypeElm.ObjectElm, ref nResults, ref frameObjName
//                    , ref frameObjStation
//                    , ref frameElmName, ref frameElmStation, ref loadCase, ref stepType, ref stepNumber
//                    , ref p, ref v2, ref v3, ref t, ref m2, ref m3);*/

//                //m3.CopyTo(m3, 0);
//                //m3.CopyTo(m3, 0);
//                #endregion

//                xbeams[bindex].FrameResults.Add(new SapFrameResult(mySapModel.MySapObjectModel, item.Label));

//                //add sections created:
//                xbeams[bindex].Sec.Add(B250X600);
//                bindex++;

//            }
//            /*----------------------------------------------------------*/
//            //Design:
//            #region Trial
//            /*string matname;
//            double fc1;
//            bool isLight;
//            double fcsFactor;
//            int sstype;
//            double strainAtFc;


//            mySapModel.MySapObjectModel.PropMaterial.GetOConcrete_1()*/
//            #endregion

//            //not used yet
//            BeamDesign design = new BeamDesign(xbeams);

//            //double d = B250X600.T * 1000 - 50;  //Section Depth in mm



//            //List<double>[] m33Arr = new List<double>[xbeams.Length];

//            //List<double>[] c1Arr = new List<double>[xbeams.Length];

//            //List<double>[] cVals = new List<double>[xbeams.Length]; //For Solving the Quadratic Equation

//            //List<double>[] cdArr = new List<double>[xbeams.Length];

//            //List<double>[] cdArrNew = new List<double>[xbeams.Length];

//            //List<double>[] jArr = new List<double>[xbeams.Length];

//            //List<double>[] AsArr = new List<double>[xbeams.Length]; //As in cm2




//            Console.ReadLine();

//        }
//    }

//}

