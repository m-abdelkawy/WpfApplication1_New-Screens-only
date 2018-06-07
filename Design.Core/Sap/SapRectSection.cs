using SAP2000v20;

namespace Desing.Core.Sap
{
    public enum secColor
    {
        Default = -1
    }
     public class SapRectSection
    {
        #region Memeber Variables

        private cSapModel mySapObjectModel;
        private string secName;
        private SapMaterial matProp;
        private double b; // T2 width
        private double t; //T3 depth
        private int sectionColor;
        private double[] modifiers;

        
        #endregion

        #region Properties
        public cSapModel MySapObjectModel
        {
            get { return mySapObjectModel; }
            set { mySapObjectModel = value; }
        }

        public string SecName
        {
            get { return secName; }
            set { secName = value; }
        }

        public SapMaterial MatProp
        {
            get { return matProp; }
            set { matProp = value; }
        }

        public double B
        {
            get { return b; }
            set{ b = value; }
        }

        public double T
        {
            get { return t; }
            set { t = value; }
        }

        public int SectionColor
        {
            get { return sectionColor; }
            set { sectionColor = value; }
        }

        public double[] Modifiers
        {
            get { return modifiers; }
            set { modifiers = value; }
        }

        #endregion

        #region Constructors
        public SapRectSection(cSapModel mySapObjectModel, string secName, SapMaterial matProp, double b, double t, int sectionColor)
        {
            this.mySapObjectModel = mySapObjectModel;
            this.secName = secName;
            this.matProp = matProp;
            this.b = b;
            this.t = t;
            this.sectionColor = sectionColor;
        }
        #endregion

        #region Methods
        public void SetRectSec()
        {
            mySapObjectModel.PropFrame.SetRectangle(this.secName, this.matProp.MatName, this.t, this.b, this.sectionColor);
        }
        public void SetModifiers(double[] _modifiers)
        {
            this.modifiers = _modifiers;
            mySapObjectModel.PropFrame.SetModifiers(SecName, ref modifiers);
        }
        #endregion


    }
}