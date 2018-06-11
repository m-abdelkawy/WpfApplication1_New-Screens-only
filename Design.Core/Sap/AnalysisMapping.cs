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
        //LoadPattern
        public static List<string> loadPatternName = new List<string>();
        public static List<double> SelfWtMultiplier = new List<double>();
        public static List<eLoadPatternType> patternType = new List<eLoadPatternType>();

        public List<SapLoadPattern> loadPatterns = new List<SapLoadPattern>();


        //Load Combinations
        public static List<string> Combinations = new List<string>();
        public static List<string> loadCaseName = new List<string>();
        public static List<double> loadFactorList = new List<double>();
        #endregion

        #region Properties

        #endregion

        #region Constructors
        #endregion

        #region Methods
        #endregion
    }
}
