using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP2000v20;

namespace Desing.Core.Sap
{
    public class SapFrameResult
    {
        #region Member Variables

        cSapModel mySapObjectModel;
        int nResults = 0;
        string frameName;
        string[] frameObjName = new string[0];
        double[] frameObjStation = new double[0];
        string[] frameElmName = new string[0];
        double[] frameElmStation = new double[0];
        string[] loadCase = new string[0];
        string[] stepType = new string[0];
        double[] stepNumber = new double[0];

        //Internal Forces
        double[] p = new double[0];
        double[] v2 = new double[0];
        double[] v3 = new double[0];
        double[] t = new double[0]; //Torsion
        double[] m2 = new double[0];
        double[] m3 = new double[0];

        //Flexural Design Parameters
        double[] c1;
        double[] c;
        double[] cd;
        double[] j;
        double[] asteel;
        string[] statusFlexure;

        //Shear Design Parameters
        double qcu;
        double qmax;
        double[] qu; //Actual Shear Stress
        int[] nbranches;
        int[] s;
        double[] asbranch;
        double[,] astr_s;
        double[] astr_sManual;
        double[] qs;
        string[] statusShear;

        //for Mapping Design for Flexure
        int[] DiameterArr = new int[] { 12, 14, 16, 18, 20, 22, 25 };
        int[,] nRebarArr;
        double[,] AsActual;
        int nLayerMax = 3;
        int[] nRebarMaxInLayer;
        int[,] nLayerArr;
        int[] asChosenIndex;

        int[] nRebarChosenArr;
        double[] diameterChosenArr;

        /*Mapping Desin for Shear*/
        int[] StirDiaArr = new int[] { 8, 10, 12, 14, 16, 18, 20, 22, 25 };

        double[,][] spacing;
        int[] stirDiaArr240 = new int[] { 8 };
        int[] stirDiaArr360 = new int[] { 10, 12, 14, 16 };
        int[] stirDiaArr400 = new int[] { 10, 12, 14, 16 };




        #endregion

        #region Properties
        public cSapModel MySapObjectModel
        {
            get { return mySapObjectModel; }
            set { mySapObjectModel = value; }
        }

        public int NResults
        {
            get { return nResults; }
            set { nResults = value; }
        }

        public string FrameName
        {
            get { return frameName; }
            set { frameName = value; }
        }

        public string[] FrameObjName
        {
            get { return frameObjName; }
            set { frameObjName = value; }
        }

        public double[] FrameObjStation
        {
            get { return frameObjStation; }
            set { frameObjStation = value; }
        }

        public string[] FrameElmName
        {
            get { return frameElmName; }
            set { frameElmName = value; }
        }

        public double[] FrameElmStation
        {
            get { return frameElmStation; }
            set { frameElmStation = value; }
        }

        public string[] LoadCase
        {
            get { return loadCase; }
            set { loadCase = value; }
        }

        public string[] StepType
        {
            get { return stepType; }
            set { stepType = value; }
        }

        public double[] StepNumber
        {
            get { return stepNumber; }
            set { stepNumber = value; }
        }

        public double[] P
        {
            get { return p; }
            set { p = value; }
        }

        public double[] V2
        {
            get { return v2; }
            set { v2 = value; }
        }

        public double[] V3
        {
            get { return v3; }
            set { v3 = value; }
        }

        public double[] T
        {
            get { return t; }
            set { t = value; }
        }

        public double[] M2
        {
            get { return m2; }
            set { m2 = value; }
        }

        public double[] M3
        {
            get { return m3; }
            set { m3 = value; }
        }

        public double[] C1
        {
            get
            {
                return c1;
            }

            set
            {
                c1 = value;
            }
        }

        public double[] C
        {
            get
            {
                return c;
            }

            set
            {
                c = value;
            }
        }

        public double[] Cd
        {
            get
            {
                return cd;
            }

            set
            {
                cd = value;
            }
        }

        public double[] J
        {
            get
            {
                return j;
            }

            set
            {
                j = value;
            }
        }

        public double[] Asteel
        {
            get
            {
                return asteel;
            }

            set
            {
                asteel = value;
            }
        }

        public double Qcu
        {
            get
            {
                return qcu;
            }

            set
            {
                qcu = value;
            }
        }

        public double Qmax
        {
            get
            {
                return qmax;
            }

            set
            {
                qmax = value;
            }
        }

        public double[] Qu
        {
            get
            {
                return qu;
            }

            set
            {
                qu = value;
            }
        }

        public int[] Nbranches
        {
            get
            {
                return nbranches;
            }

            set
            {
                nbranches = value;
            }
        }

        public int[] S
        {
            get
            {
                return s;
            }

            set
            {
                s = value;
            }
        }

        public double[] Asbranch
        {
            get
            {
                return asbranch;
            }

            set
            {
                asbranch = value;
            }
        }

        public string[] StatusFlexure
        {
            get
            {
                return statusFlexure;
            }

            set
            {
                statusFlexure = value;
            }
        }

        public string[] StatusShear
        {
            get
            {
                return statusShear;
            }

            set
            {
                statusShear = value;
            }
        }

        public double[] Qs
        {
            get
            {
                return qs;
            }

            set
            {
                qs = value;
            }
        }

        public double[,] Astr_s
        {
            get
            {
                return astr_s;
            }

            set
            {
                astr_s = value;
            }
        }

        public int[] DiameterArr1
        {
            get
            {
                return DiameterArr;
            }

            set
            {
                DiameterArr = value;
            }
        }

        public int[,] NRebarArr
        {
            get
            {
                return nRebarArr;
            }

            set
            {
                nRebarArr = value;
            }
        }

        public double[,] AsActual1
        {
            get
            {
                return AsActual;
            }

            set
            {
                AsActual = value;
            }
        }

        public int NLayerMax
        {
            get
            {
                return nLayerMax;
            }

            set
            {
                nLayerMax = value;
            }
        }

        public int[] NRebarMaxInLayer
        {
            get
            {
                return nRebarMaxInLayer;
            }

            set
            {
                nRebarMaxInLayer = value;
            }
        }

        public int[,] NLayerArr
        {
            get
            {
                return nLayerArr;
            }

            set
            {
                nLayerArr = value;
            }
        }

        public int[] AsChosenIndex
        {
            get
            {
                return asChosenIndex;
            }

            set
            {
                asChosenIndex = value;
            }
        }

        public int[] NRebarChosenArr
        {
            get
            {
                return nRebarChosenArr;
            }

            set
            {
                nRebarChosenArr = value;
            }
        }

        public double[] DiameterChosenArr
        {
            get
            {
                return diameterChosenArr;
            }

            set
            {
                diameterChosenArr = value;
            }
        }

        public int[] StirDiaArr1
        {
            get
            {
                return StirDiaArr;
            }

            set
            {
                StirDiaArr = value;
            }
        }





        public int[] StirDiaArr240
        {
            get
            {
                return stirDiaArr240;
            }

            set
            {
                stirDiaArr240 = value;
            }
        }

        public int[] StirDiaArr360
        {
            get
            {
                return stirDiaArr360;
            }

            set
            {
                stirDiaArr360 = value;
            }
        }

        public int[] StirDiaArr400
        {
            get
            {
                return stirDiaArr400;
            }

            set
            {
                stirDiaArr400 = value;
            }
        }

        public double[,][] Spacing
        {
            get
            {
                return spacing;
            }

            set
            {
                spacing = value;
            }
        }

        public double[] Astr_sManual
        {
            get
            {
                return astr_sManual;
            }

            set
            {
                astr_sManual = value;
            }
        }

        #endregion

        #region Constructors
        public SapFrameResult(cSapModel mySapObjectModel, string frameName)
        {
            this.mySapObjectModel = mySapObjectModel;
            this.c1 = new double[3];
            this.c = new double[3];
            this.cd = new double[3];
            this.j = new double[3];
            this.asteel = new double[3];

            int ret = mySapObjectModel.Results.FrameForce(frameName, eItemTypeElm.ObjectElm, ref nResults
                , ref frameObjName, ref frameObjStation, ref frameElmName, ref frameElmStation
                , ref loadCase, ref stepType, ref stepNumber
                , ref p, ref v2, ref v3, ref t, ref m2, ref m3);
        }
        #endregion

    }
}
