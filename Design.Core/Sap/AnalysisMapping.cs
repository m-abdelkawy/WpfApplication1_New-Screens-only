using Desing.Core.Sap;
using SAP2000v20;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design.Core.Sap
{
    public class AnalysisMapping
    {
        #region DataMembers
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



        //Point Load Storage




        #endregion

        #region Properties

        #endregion

        #region Constructors
        #endregion

        #region Methods
        #endregion
    }
}
