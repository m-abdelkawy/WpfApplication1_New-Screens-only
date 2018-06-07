using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP2000v20;

namespace Desing.Core.Sap
{
    public  class SapLoadPattern
    {
        #region Member Variables

        private string name;
        private eLoadPatternType patternType;
        private double selfWtMultiplier;
        private bool addLoadCase;
        private cSapModel myObjectModel;
        
        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double SelfWtMultiplier
        {
            get { return selfWtMultiplier; }
            set { selfWtMultiplier = value; }
        }

        public bool AddLoadCase
        {
            get { return addLoadCase; }
            set { addLoadCase = value; }
        }

        public cSapModel MyObjectModel
        {
            get { return myObjectModel; }
            set { myObjectModel = value; }
        }

        #endregion

        #region Constructors
        public SapLoadPattern(cSapModel myObjectModel, eLoadPatternType _patternType, string _name, double _selfWtMultiplier, bool _addLoadCase)
        {
            this.myObjectModel = myObjectModel;
            this.patternType = _patternType;
            this.name = _name;
            this.selfWtMultiplier = _selfWtMultiplier;
            this.addLoadCase = _addLoadCase; //if true, a linear static load Case is added.
        }

        #endregion

        #region Methods
        public void AddLoadPattern()
        {
            MyObjectModel.LoadPatterns.Add(this.name, this.patternType, this.selfWtMultiplier, this.addLoadCase);
        }
        #endregion
    }
}