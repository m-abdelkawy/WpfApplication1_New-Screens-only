using SAP2000v20;

namespace Desing.Core.Sap
{
    public enum matColor
    {
        Default = -1
    }
     public class SapMaterial
    {
        #region Member Variables

        private eMatType matType;
        private cSapModel mySapObjectModel;
        private string matName;
        private matColor materialColor;
        
        #endregion

        #region Properties
        public eMatType MatType
        {
            get { return matType; } 
            set { matType = value; }
        }

        public cSapModel MySapObjectModel
        {
            get { return mySapObjectModel; } 
            set
            { mySapObjectModel = value; }
        }

        public string MatName
        {
            get { return matName; } 
            set { matName = value; }
        }

        public matColor MaterialColor
        {
            get { return materialColor; }
            set { materialColor = value; }
        }
        #endregion

        #region Constructor
        public SapMaterial(cSapModel _mySapObjectModel, string _matName, eMatType _matType, matColor _materialColor = matColor.Default)
        {
            this.MySapObjectModel = _mySapObjectModel;
            this.matName = _matName;
            this.matType = _matType;
            this.materialColor = _materialColor;
        }
        #endregion

        #region Methods
        public void SetNewMaterial(string matName, eMatType matType, matColor materialColor = matColor.Default)
        {
            mySapObjectModel.PropMaterial.SetMaterial(matName, matType, (int)materialColor);
        }
        public void SetIsotropicProps(string matName, double E, double U, double A)
        {
            mySapObjectModel.PropMaterial.SetMPIsotropic(matName, E, U, A);
        }
        public void SetConcMat(string matName, double Fc, bool isLightWeight = false)
        {
            mySapObjectModel.PropMaterial.SetOConcrete_1(matName, Fc, isLightWeight, 0, 1, 2, 0.0022, 0.0052, -0.1);
        }
        public void SetSteelMat(string matName, double Fy, double Fu, double EFy, double EFu)
        {
            mySapObjectModel.PropMaterial.SetOSteel_1(matName, Fy, Fu, EFy, EFu, 1, 2, 0.02, 0.1, 0.2, -0.1);
        }
        public void SetWeight(string matName, double wtPerUnitVol)
        {
            MySapObjectModel.PropMaterial.SetWeightAndMass(matName, 1, wtPerUnitVol);
        }


        #endregion
    }
}