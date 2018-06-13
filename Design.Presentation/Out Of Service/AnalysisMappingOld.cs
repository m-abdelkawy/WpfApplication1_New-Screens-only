using Desing.Core.Sap;
using SAP2000v20;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Design;
using Design.Presentation.ViewModels;
using System.Linq;

namespace Design.Core.Sap
{
    public class AnalysisMappingOld
    {
        #region DataMembers

        public static SapModel mySapModel;
        public static double[] spanValues;
        public static double[] comSpanValues; //comlative
        public static SapPoint[] points;
        public static SapRectSection[] SapSectionsArr;
        public static SapFrameElement[] xbeams;

        

        public static string[] comboArray;
        /*-----------------------------*/
        public static List<int> spanList = new List<int>();



        //LoadPattern
        public static List<string> loadPatternName = new List<string>();
        public static List<double> SelfWtMultiplier = new List<double>();
        public static List<eLoadPatternType> patternType = new List<eLoadPatternType>();

        public static List<SapLoadPattern> loadPatterns = new List<SapLoadPattern>();


        //Load Combinations
        public static List<string> Combinations = new List<string>();
        public static List<string> loadCaseName = new List<string>();
        public static List<double> loadFactorList = new List<double>();

        /*------------------Distributed Load Storage------------------*/
        public static List<string> distLoadPattern = new List<string>();
        public static List<LoadDir> distLoadDirVal = new List<LoadDir>();
        public static List<double> distLoadVals = new List<double>();
        public static List<string> DistLoadFrameElement = new List<string>();

        /*------------------Point Load Storage------------------*/
        public static List<string> concLoadPattern = new List<string>();
        public static List<double> concLoadVals = new List<double>();
        public static List<LoadDir> concLoadDirVal = new List<LoadDir>();
        public static List<string> concLoadFrameElement = new List<string>();
        public static List<double> relDistList = new List<double>();



        /*------------------Sap2000 Load Storage------------------*/
        public static List<SapFrameDistLoad> distLoads = new List<SapFrameDistLoad>();
        public static List<SapPointLoad> pointLoads = new List<SapPointLoad>();




        #endregion

        #region Properties

        #endregion

        #region Constructors
        #endregion

        #region Methods
        /*public static void InitializeModel(SapModel mySapModel, string filePath, string fileName)
        {
            //Create Sap Model
            mySapModel = new SapModel(filePath, fileName);
            //initialize Model units KN, m, C:
            mySapModel.InitializeUnits(eUnits.kN_m_C);
        }*/
        public static void CalcSpanValues()
        {
            double[] spans = GeometryEditorVM.GeometryEditor.GridData.Select(e => e.Span).ToArray();

            spanValues = new double[spanList.Count + 1];
            spanValues[0] = 0;
            for (int i = 1; i < spans.Length + 1; i++)
            {
                spanValues[i] = spans[i - 1];
            }
            
        }

        public static void CalcComSpanValues()
        {
            comSpanValues = new double[spanList.Count + 1];
            comSpanValues[0] = 0;
            for (int i = 1; i < comSpanValues.Length; i++)
            {
                comSpanValues[i] = comSpanValues[i - 1] + spanValues[i];
            }
        }


        public static SapMaterial CreateConcMat(double fcu)
        {
            //fcuName = fcutxtBox.Text
            //Concrete
            SapMaterial concMat = new SapMaterial(mySapModel.MySapObjectModel, $"Fcu{fcu}", eMatType.Concrete);
            concMat.SetNewMaterial($"Fcu{fcu}", eMatType.Concrete);
            concMat.SetConcMat($"Fcu{fcu}", Convert.ToDouble(fcu));
            concMat.SetIsotropicProps($"Fcu{fcu}", 220000, 0.20, 9.900E-06);
            concMat.SetWeight($"Fcu{fcu}", 25);

            return concMat;
        }

        //Create Sections
        public static void CreateSapSections(ObservableCollection<SectionEditorVM> Sections, SapMaterial concMat)
        {
            for (int i = 0; i < Sections.Count; i++)
            {
                SapSectionsArr = new SapRectSection[Sections.Count];

                SapSectionsArr[i] = new SapRectSection(mySapModel.MySapObjectModel
                    , $"B{Convert.ToDouble(Sections[i].Width)}X{Convert.ToDouble(Sections[i].Depth)}"
                , concMat, Convert.ToDouble(Sections[i].Width)
                , Convert.ToDouble(Sections[i].Depth), -1);


                SapSectionsArr[i].SetRectSec();
            }
        }

        //Create Sap Points
        public static void CreateSapPoints()
        {
            //Points

            points = new SapPoint[spanList.Count + 1];

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new SapPoint(mySapModel.MySapObjectModel, comSpanValues[i], 0, 0);
            }
        }


        //Create Beams
        public static void CreateXbeams()
        {
            //Beams in X-Direction:
            xbeams = new SapFrameElement[spanList.Count];

            for (int i = 0; i < xbeams.Length; i++)
            {
                xbeams[i] = new SapFrameElement(mySapModel.MySapObjectModel, points[i], points[i + 1], SapSectionsArr[0]/*check later*/, $"{i + 1}", $"B{i + 1}");
            }
        }

        //Initialize Supports
        public static void InitializeSuports()
        {
            //Initialize hinged joints
            for (int i = 0; i < spanList.Count + 1; i++)
            {
                points[i].SetRestraints(Restraints.Hinged);
            }
        }

        //Add Load Patterns to Sap
        public static void AddLoadPatterns()
        {
            for (int i = 0; i < loadPatternName.Count; i++)
            {
                SapLoadPattern load = new SapLoadPattern(mySapModel.MySapObjectModel, patternType[i], loadPatternName[i]
                    , SelfWtMultiplier[i], true);

                loadPatterns.Add(load);
                load.AddLoadPattern();
            }
        }

        //Add Load Combinations to Sap
        public static void AddLoadCombinations()
        {
            eCNameType ec = eCNameType.LoadCase;
            for (int i = 0; i < Combinations.Count; i++)
            {
                mySapModel.MySapObjectModel.RespCombo.Add(Combinations[i], 0);

                mySapModel.MySapObjectModel.RespCombo.SetCaseList(Combinations[i], ref ec, loadCaseName[i]
                    , loadFactorList[i]);
            }
        }

        //Add Distributed Load to elements on specific pattern
        public static void AddDistributedLoads()
        {
            for (int i = 0; i < distLoadVals.Count; i++)
            {
                SapFrameElement item = null;
                SapLoadPattern loadPat = null;
                for (int j = 0; j < spanList.Count; j++)
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
        }

        //Add Point Load to elements on specific Pattern
        public static void AddPointLoads()
        {
            for (int i = 0; i < concLoadVals.Count; i++)
            {
                SapFrameElement item = null;
                SapLoadPattern loadPat = null;
                for (int j = 0; j < spanList.Count; j++)
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
                SapPointLoad D1 = new SapPointLoad(loadPat, concLoadVals[i], Desing.Core.Sap.Type.Force, concLoadDirVal[i]);

                pointLoads.Add(D1);
                item.SetFramePointLoad(D1, relDistList[i]);
            }
        }

        //Run Analysis
        public static void RunModel()
        {
            mySapModel.SaveAndRun();

            int cnt = mySapModel.MySapObjectModel.RespCombo.Count();
            comboArray = new string[cnt];
            mySapModel.MySapObjectModel.RespCombo.GetNameList(ref cnt, ref comboArray);
            
        }



        #endregion


    }
}
