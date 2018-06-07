using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Desing.Core.Sap
{
    public class BeamDesign
    {
        #region Member Variables
        SapFrameElement[] xBeamArr;
        //ArrayList[] beamDesigned;
        string frameName;
        double fcu;
        double fy;
        double b;
        double d;
        double c;
        double cover = 50; //in mm
        double asteel;
        double c1;
        double j;
        double m33;
        double astr_s;

        double qcu;
        double qmax;



        #endregion

        #region Properties
        public string FrameName
        {
            get
            {
                return frameName;
            }

            set
            {
                frameName = value;
            }
        }

        public double Fcu
        {
            get
            {
                return fcu;
            }

            set
            {
                fcu = value;
            }
        }

        public double B
        {
            get
            {
                return b;
            }

            set
            {
                b = value;
            }
        }

        public double D
        {
            get
            {
                return d;
            }

            set
            {
                d = value;
            }
        }

        public double C
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

        public double Asteel
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

        public double C1
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

        public double J
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

        public double M33
        {
            get
            {
                return m33;
            }

            set
            {
                m33 = value;
            }
        }

        public double Cover
        {
            get
            {
                return cover;
            }

            set
            {
                cover = value;
            }
        }

        public double Astr_s
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

        public SapFrameElement[] XBeamArr
        {
            get
            {
                return xBeamArr;
            }

            set
            {
                xBeamArr = value;
            }
        }

        #endregion

        #region Constructors
        public BeamDesign(SapFrameElement[] arr, double fy, double fystr, double fcu, int nBranches)
        {

            qcu = 0.24 * Math.Pow((fcu / 1.50), 0.50);
            qmax = 0.70 * Math.Pow((fcu / 1.50), 0.50);

            this.xBeamArr = new SapFrameElement[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                xBeamArr[i] = arr[i];

                /*-----Flexure-----*/
                xBeamArr[i].FrameResults.ElementAt(0).M3 = getM33Arr(arr[i]);
                xBeamArr[i].FrameResults.ElementAt(0).C1 = calcC1(arr[i], xBeamArr[i].FrameResults.ElementAt(0).M3, 50, fcu);
                xBeamArr[i].FrameResults.ElementAt(0).C = calcC(arr[i], xBeamArr[i].FrameResults.ElementAt(0).C1);
                xBeamArr[i].FrameResults.ElementAt(0).Cd = calcCd(arr[i], xBeamArr[i].FrameResults.ElementAt(0).C);
                xBeamArr[i].FrameResults.ElementAt(0).J =
                    calcJ(arr[i], xBeamArr[i].FrameResults.ElementAt(0).Cd, xBeamArr[i].FrameResults.ElementAt(0).C1);
                xBeamArr[i].FrameResults.ElementAt(0).Asteel = calcAs(arr[i], xBeamArr[i].FrameResults.ElementAt(0).J
                    , xBeamArr[i].FrameResults.ElementAt(0).C1, xBeamArr[i].FrameResults.ElementAt(0).M3, 50, fy, fcu);

                //for Mapping
                XBeamArr[i].FrameResults.ElementAt(0).NRebarArr =
                    CalcnRebarArr(new double[] { 12, 14, 16, 18, 20, 22, 25 }, xBeamArr[i].FrameResults.ElementAt(0).Asteel);

                XBeamArr[i].FrameResults.ElementAt(0).AsActual1 =
                    CalcAsActual(new double[] { 12, 14, 16, 18, 20, 22, 25 }, XBeamArr[i].FrameResults.ElementAt(0).NRebarArr);

                XBeamArr[i].FrameResults.ElementAt(0).NRebarMaxInLayer = CalcMaxNRebarinLayer(new double[] { 12, 14, 16, 18, 20, 22, 25 }, arr[i]);

                XBeamArr[i].FrameResults.ElementAt(0).NLayerArr = CalcnLayers(XBeamArr[i].FrameResults.ElementAt(0).NRebarArr, XBeamArr[i].FrameResults.ElementAt(0).NRebarMaxInLayer);

                XBeamArr[i].FrameResults.ElementAt(0).AsChosenIndex =
                    GetAsChosenIndex(XBeamArr[i].FrameResults.ElementAt(0).AsActual1, XBeamArr[i].FrameResults.ElementAt(0).NLayerArr);

                //nRabar Chosen
                XBeamArr[i].FrameResults.ElementAt(0).NRebarChosenArr =
                    getnRebarChosen(XBeamArr[i].FrameResults.ElementAt(0).AsChosenIndex, XBeamArr[i].FrameResults.ElementAt(0).NRebarArr);

                //Diameter Chosen
                XBeamArr[i].FrameResults.ElementAt(0).DiameterChosenArr =
                    getDiameterChosen(XBeamArr[i].FrameResults.ElementAt(0).AsChosenIndex, new double[] { 12, 14, 16, 18, 20, 22, 25 });


                /*-----Shear-----*/
                arr[i].FrameResults.ElementAt(0).Qcu = 0.24 * Math.Pow((fcu / 1.50), 0.50);
                arr[i].FrameResults.ElementAt(0).Qmax = 0.70 * Math.Pow((fcu / 1.50), 0.50);

                xBeamArr[i].FrameResults.ElementAt(0).V2 = getS22Arr(arr[i]);
                xBeamArr[i].FrameResults.ElementAt(0).Qu = getquArr(arr[i], xBeamArr[i].FrameResults.ElementAt(0).V2);
                xBeamArr[i].FrameResults.ElementAt(0).Qs =
                    getqsArr(arr[i], xBeamArr[i].FrameResults.ElementAt(0).Qcu,
                    xBeamArr[i].FrameResults.ElementAt(0).Qmax, xBeamArr[i].FrameResults.ElementAt(0).Qu);
                xBeamArr[i].FrameResults.ElementAt(0).Astr_s = CalcAstr_s(arr[i],
                    xBeamArr[i].FrameResults.ElementAt(0).Qs, nBranches/*branches*/);

                //
                xBeamArr[i].FrameResults.ElementAt(0).Astr_sManual = ManualCalcAstr_s(arr[i],
                    xBeamArr[i].FrameResults.ElementAt(0).Qs, nBranches/*branches*/, fystr/*N/mm2*/);

                //For Mapping
                XBeamArr[i].FrameResults.ElementAt(0).Spacing = CalcSpacing(xBeamArr[i].FrameResults.ElementAt(0).Astr_s, nBranches
                    , new double[] { 8 }, new double[] { 10, 12, 14, 16 }, new double[] { 10, 12, 14, 16 });
            }

        }
        #endregion

        #region Methods
        /*-------------------------------------Flexural Design-------------------------------------*/
        public static double[] getM33Arr(SapFrameElement elem)
        {
            double[] m33Arr = new double[3];
            m33Arr[0] = Math.Abs(elem.FrameResults.ElementAt(0).M3.First());
            m33Arr[1] = Math.Abs(elem.FrameResults.ElementAt(0).M3.Max());
            m33Arr[2] = Math.Abs(elem.FrameResults.ElementAt(0).M3.Last());


            return m33Arr;
        }
        public static double[] calcC1(SapFrameElement elem, double[] m33Arr/*KN.m*/, double cover/*mm*/, double fcu)
        {
            double[] c1Arr = new double[3];
            for (int i = 0; i < c1Arr.Length; i++)
            {
                c1Arr[i] = Math.Abs((elem.MyRectSec.T * 1000 - cover) / Math.Pow(((m33Arr[i] * 100000/*N.mm*/)
                    / (fcu/*fcu N/mm2*/ * elem.MyRectSec.B * 1000)), 0.50));
            }
            return c1Arr;
        }
        public static double[] calcC(SapFrameElement elem, double[] c1Arr)
        {
            double[] cVals = new double[3]; //For Solving the Quadratic Equation
            for (int i = 0; i < cVals.Length; i++)
            {
                cVals[i] = 1 / (0.3573 * Math.Pow(c1Arr[i], 2));
            }
            return cVals;
        }
        public static double[] calcCd(SapFrameElement elem, double[] cVals)
        {
            double[] cdArr = new double[3];
            for (int i = 0; i < cdArr.Length; i++)
            {
                cdArr[i] = Math.Max(Math.Max((1 + Math.Pow((1 - 4 * 0.40 * cVals[i]), 0.50) / 0.80)
                        , (1 - Math.Pow((1 - 4 * 0.40 * cVals[i]), 0.50) / 0.80)), 0.125);
            }
            return cdArr;
        }
        public static double[] calcJ(SapFrameElement elem, double[] cdArr, double[] c1Arr)
        {
            double[] jArr = new double[3];
            for (int i = 0; i < jArr.Length; i++)
            {
                if (c1Arr[i] >= 4.85)
                {
                    jArr[i] = 0.826;
                }
                else
                {
                    jArr[i] = (1 / (1.15 * (1 - 0.4 * cdArr[i])));
                }
            }
            return jArr;
        }
        public static double[] calcAs(SapFrameElement elem, double[] jArr, double[] c1Arr, double[] m33Arr, double cover, double fy, double fcu)
        {// Flexural Steel Area
            double[] AsArr = new double[3]; //As in cm2
            for (int i = 0; i < AsArr.Length; i++)
            {
                double asValue;
                double asMin = (0.225 * (Math.Pow(fcu, 0.50) / fy) *
                    (elem.MyRectSec.B * 1000 * (elem.MyRectSec.T * 1000 - cover))) / 100;
                if (c1Arr[i] < 2.78) //if true, increase dimensions.
                {
                    asValue = -1; //increase dims
                }
                else
                {
                    asValue = (((m33Arr[i]) * 1000000) / (fy * jArr[i] * (elem.MyRectSec.T * 1000 - cover))) / 100;
                    if (asValue < asMin)
                    {
                        asValue = asMin;
                    }
                }
                AsArr[i] = asValue;
            }
            return AsArr;
        }
        public static int[,] CalcnRebarArr(double[] Diameters, double[] Asteel)
        {
            int[,] nRebars = new int[Asteel.Length, Diameters.Length];
            for (int i = 0; i < nRebars.GetLength(0); i++)
            {
                for (int j = 0; j < nRebars.GetLength(1); j++)
                {
                    nRebars[i, j] = Math.Max(Convert.ToInt32(Math.Ceiling(Asteel[i] * (4 / (Math.PI * (Math.Pow((Diameters[j] / 10), 2)))))), 2);
                }
            }
            return nRebars;
        }

        public static double[,] CalcAsActual(double[] Diameters, int[,] nRebars)
        {
            double[,] AsActual = new double[nRebars.GetLength(0), nRebars.GetLength(1)];
            for (int i = 0; i < AsActual.GetLength(0); i++)
            {
                for (int j = 0; j < nRebars.GetLength(1); j++)
                {
                    AsActual[i, j] = nRebars[i, j] * Math.PI * Math.Pow((Diameters[j] / 10), 2) / 4;
                }
            }
            return AsActual;
        }

        public static int[] CalcMaxNRebarinLayer(double[] diameters, SapFrameElement elem)
        {
            int[] maxnRebarInLayer = new int[diameters.Length];
            for (int i = 0; i < maxnRebarInLayer.Length; i++)
            {
                maxnRebarInLayer[i] = Convert.ToInt32(Math.Floor((elem.MyRectSec.B * 1000 + 0.25) / (diameters[i] + 25)));
            }
            return maxnRebarInLayer;
        }

        public int[,] CalcnLayers(int[,] nRebarArr, int[] maxnRebarInLayer)
        {
            int[,] NLayers = new int[nRebarArr.GetLength(0), nRebarArr.GetLength(1)];
            for (int i = 0; i < NLayers.GetLength(0); i++)
            {
                for (int j = 0; j < NLayers.GetLength(1); j++)
                {
                    NLayers[i, j] = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(nRebarArr[i, j]) / Convert.ToDouble(maxnRebarInLayer[j])));
                }
            }
            return NLayers;
        }

        public static int[] GetAsChosenIndex(double[,] AsActual, int[,] NLayers)
        {
            int[] AsChosenIndex = new int[AsActual.GetLength(0)];
            for (int i = 0; i < AsChosenIndex.Length; i++)
            {
                double minAs = 10000;
                //int index = -1;
                for (int j = 0; j < AsActual.GetLength(1); j++)
                {
                    if (AsActual[i, j] < minAs && NLayers[i, j] <= 3)
                    {
                        minAs = AsActual[i, j];
                        AsChosenIndex[i] = j;
                    }
                }
            }
            return AsChosenIndex;
        }

        public static int[] getnRebarChosen(int[] AsChosenIndex, int[,] nRebars)
        {
            int[] nRebarChosen = new int[AsChosenIndex.Length];
            for (int i = 0; i < nRebarChosen.Length; i++)
            {
                nRebarChosen[i] = nRebars[i, AsChosenIndex[i]];
            }
            return nRebarChosen;
        }

        public static double[] getDiameterChosen(int[] AsChosenIndex, double[] Diameters)
        {
            double[] DiameterChosenArr = new double[AsChosenIndex.Length];
            for (int i = 0; i < DiameterChosenArr.Length; i++)
            {
                DiameterChosenArr[i] = Diameters[AsChosenIndex[i]];
            }
            return DiameterChosenArr;
        }

        /*-------------------------------------Shear Design-------------------------------------*/
        public static double[] getS22Arr(SapFrameElement elem)
        {// applied shear force in KN (S22)
            double[] S22Arr = new double[2];

            S22Arr[0] = Math.Abs(elem.FrameResults.ElementAt(0).V2.First());
            S22Arr[1] = Math.Abs(elem.FrameResults.ElementAt(0).V2.Last());

            return S22Arr;
        }
        public static double[] getquArr(SapFrameElement elem, double[] S22Arr)
        {// applied shear stress in N/mm2
            double[] quArr = new double[2];

            quArr[0] = (S22Arr[0] * 1000) / (elem.MyRectSec.B * 1000 * elem.MyRectSec.T * 1000);
            quArr[1] = (S22Arr[1] * 1000) / (elem.MyRectSec.B * 1000 * elem.MyRectSec.T * 1000);

            return quArr;
        }
        public static double[] getqsArr(SapFrameElement elem, double qcu, double qmax, double[] quArr/*applied shear stress*/)
        {//shear stress carried by stirrups in(N/mm2)
            double[] qsArr = new double[2];

            for (int i = 0; i < qsArr.Length; i++)
            {
                if (qcu >= quArr[i])
                {
                    //string s = "Use minimum stirrups";
                    //elem.FrameResults.ElementAt(0).StatusShear[i] = s;
                    qsArr[i] = -1; //use min stirrups(Y8@200mm)
                }
                else if (quArr[i] > qmax)
                {
                    //elem.FrameResults.ElementAt(0).StatusShear[i] = "increase Section Dims";
                    qsArr[i] = -2; //not safe
                }
                else
                {
                    double qsValue = elem.FrameResults.ElementAt(0).Qu[i] - (qcu / 2);
                    if (qsValue < 0)
                    {
                        qsValue = 0;
                    }
                    qsArr[i] = qsValue;
                    //elem.FrameResults.ElementAt(0).StatusShear[i] = "OK!";
                }
            }
            return qsArr;
        }
        public static double[] ManualCalcAstr_s(SapFrameElement elem, double[] qsArr, int nbranches, double fystr)
        {//astr (cross sectional area of one branch in mm2)
            // s(mm) must be (100 - 200 mm)
            // fy in N/mm2
            double AsMin_s = 0;
            if (fystr == 240)
            {
                AsMin_s = Math.Max(((elem.MyRectSec.B * 1000 * (0.4 / fystr)) / nbranches), ((elem.MyRectSec.B * 1000 * (0.15 / 100)) / nbranches));
            }
            else if (fystr == 360 || fystr == 400)
            {
                AsMin_s = Math.Max(((elem.MyRectSec.B * 1000 * (0.4 / fystr)) / nbranches), ((elem.MyRectSec.B * 1000 * (0.10 / 100)) / nbranches));
            }
            double[] Astr_s = new double[2];
            for (int i = 0; i < Astr_s.Length; i++)
            {
                Astr_s[i] = Math.Max((qsArr[i] * elem.MyRectSec.B * 1000) / (nbranches * (fystr / 1.15)), AsMin_s);
                if (qsArr[i] == -2)
                {
                    Astr_s[i] = -2;
                }
            }
            return Astr_s;
        }
        public static double[,] CalcAstr_s(SapFrameElement elem, double[] qsArr, int nbranches)
        {//astr (cross sectional area of one branch in mm2)
            // s(mm) must be (100 - 200 mm)
            // fy in N/mm2

            double AsMin_s240 = Math.Max(((elem.MyRectSec.B * 1000 * (0.4 / 240)) / nbranches), ((elem.MyRectSec.B * 1000 * (0.15 / 100)) / nbranches));
            double AsMin_s360 = Math.Max(((elem.MyRectSec.B * 1000 * (0.4 / 360)) / nbranches), ((elem.MyRectSec.B * 1000 * (0.10 / 100)) / nbranches));
            double AsMin_s400 = Math.Max(((elem.MyRectSec.B * 1000 * (0.4 / 400)) / nbranches), ((elem.MyRectSec.B * 1000 * (0.10 / 100)) / nbranches));


            double[,] Astr_s = new double[2, 3];
            for (int i = 0; i < Astr_s.GetLength(0)/*2*/; i++)
            {
                Astr_s[i, 0] = Math.Max((qsArr[i] * elem.MyRectSec.B * 1000) / (nbranches * (240 / 1.15)), AsMin_s240);
                Astr_s[i, 1] = Math.Max((qsArr[i] * elem.MyRectSec.B * 1000) / (nbranches * (360 / 1.15)), AsMin_s360);
                Astr_s[i, 2] = Math.Max((qsArr[i] * elem.MyRectSec.B * 1000) / (nbranches * (400 / 1.15)), AsMin_s400);
                if (qsArr[i] == -2)
                {
                    Astr_s[i, 0] = -2;
                    Astr_s[i, 1] = -2;
                    Astr_s[i, 2] = -2;
                }
            }
            return Astr_s;
        }

        /*----------------*/
        //Steel 240
        //public static double[,] CalcSpacing240(double[] Astr_s, int nbranches, double fystr, double[] stirDiaArr)
        //{
        //    double[,] spacing240 = new double[Astr_s.Length, stirDiaArr.Length];
        //    for (int i = 0; i < spacing240.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < spacing240.GetLength(1); j++)
        //        {
        //            double s360 = (Math.PI * Math.Pow(stirDiaArr[j], 2) / 4) / Astr_s[i];
        //            if (s360 < 100)
        //            {//unsafe
        //                spacing240[i, j] = -1; //unsafe
        //            }
        //            else if (s360 >= 100 && s360 < 125)
        //            {
        //                spacing240[i, j] = 100;
        //            }
        //            else if (s360 >= 125 && s360 < 150)
        //            {
        //                spacing240[i, j] = 125;
        //            }
        //            else if (s360 >= 150 && s360 < 200)
        //            {
        //                spacing240[i, j] = 150;
        //            }
        //            else if (s360 >= 200)
        //            {
        //                spacing240[i, j] = 200;
        //            }
        //        }
        //    }
        //    return spacing240;
        //}

        ////Steel 360
        //public static double[,] CalcSpacing360(double[] Astr_s, int nbranches, double fystr, double[] stirDiaArr)
        //{
        //    double[,] spacing360 = new double[Astr_s.Length, stirDiaArr.Length];
        //    for (int i = 0; i < spacing360.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < spacing360.GetLength(1); j++)
        //        {
        //            double s360 = (Math.PI * Math.Pow(stirDiaArr[j], 2) / 4) / Astr_s[i];
        //            if (s360 < 100)
        //            {//unsafe
        //                spacing360[i, j] = -1; //unsafe
        //            }
        //            else if (s360 >= 100 && s360 < 125)
        //            {
        //                spacing360[i, j] = 100;
        //            }
        //            else if (s360 >= 125 && s360 < 150)
        //            {
        //                spacing360[i, j] = 125;
        //            }
        //            else if (s360 >= 150 && s360 < 200)
        //            {
        //                spacing360[i, j] = 150;
        //            }
        //            else if (s360 >= 200)
        //            {
        //                spacing360[i, j] = 200;
        //            }
        //        }
        //    }
        //    return spacing360;
        //}

        ////Steel 400
        //public static double[,] CalcSpacing400(double[] Astr_s, int nbranches, double fystr, double[] stirDiaArr)
        //{
        //    double[,] spacing400 = new double[Astr_s.Length, stirDiaArr.Length];
        //    for (int i = 0; i < spacing400.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < spacing400.GetLength(1); j++)
        //        {
        //            double s400 = (Math.PI * Math.Pow(stirDiaArr[j], 2) / 4) / Astr_s[i];
        //            if (s400 < 100)
        //            {//unsafe
        //                spacing400[i, j] = -1; //unsafe
        //            }
        //            else if (s400 >= 100 && s400 < 125)
        //            {
        //                spacing400[i, j] = 100;
        //            }
        //            else if (s400 >= 125 && s400 < 150)
        //            {
        //                spacing400[i, j] = 125;
        //            }
        //            else if (s400 >= 150 && s400 < 200)
        //            {
        //                spacing400[i, j] = 150;
        //            }
        //            else if (s400 >= 200)
        //            {
        //                spacing400[i, j] = 200;
        //            }
        //        }
        //    }
        //    return spacing400;
        //}

        public static double[,][] CalcSpacing(double[,] Astr_s, int nbranches
            , double[] stirDiaArr240, double[] stirDiaArr360, double[] stirDiaArr400)
        {
            double[,][] spacing = new double[2, 3][];
            for (int i = 0; i < spacing.GetLength(0); i++)
            {
                spacing[i, 0] = new double[stirDiaArr240.Length];
                spacing[i, 1] = new double[stirDiaArr360.Length];
                spacing[i, 2] = new double[stirDiaArr400.Length];


                //Steel240
                for (int j = 0; j < spacing[i, 0].Length; j++)
                {
                    double s240 = (Math.PI * Math.Pow(stirDiaArr240[j], 2) / 4) / Astr_s[i, 0];
                    if (Astr_s[i, 0] == -2)
                    {
                        spacing[i, 0][j] = -2; //Increase Section Dimensions
                    }
                    else
                    {
                        if (s240 < 100)
                        {
                            spacing[i, 0][j] = -1; //unsafe
                        }
                        else if (s240 >= 100 && s240 < 125)
                        {
                            spacing[i, 0][j] = 100;
                        }
                        else if (s240 >= 125 && s240 < 150)
                        {
                            spacing[i, 0][j] = 125;
                        }
                        else if (s240 >= 150 && s240 < 200)
                        {
                            spacing[i, 0][j] = 150;
                        }
                        else if (s240 >= 200)
                        {
                            spacing[i, 0][j] = 200;
                        }
                    }
                }

                //Steel360
                for (int k = 0; k < stirDiaArr360.Length; k++)
                {
                    double s360 = (Math.PI * Math.Pow(stirDiaArr360[k], 2) / 4) / Astr_s[i, 1];
                    if (Astr_s[i, 1] == -2)
                    {
                        spacing[i, 1][k] = -2; //Increase Section Dimensions
                    }
                    else
                    {
                        if (s360 < 100)
                        {
                            spacing[i, 1][k] = -1; //unsafe
                        }
                        else if (s360 >= 100 && s360 < 125)
                        {
                            spacing[i, 1][k] = 100;
                        }
                        else if (s360 >= 125 && s360 < 150)
                        {
                            spacing[i, 1][k] = 125;
                        }
                        else if (s360 >= 150 && s360 < 200)
                        {
                            spacing[i, 1][k] = 150;
                        }
                        else if (s360 >= 200)
                        {
                            spacing[i, 1][k] = 200;
                        }
                    }
                }
                //Steel400
                for (int n = 0; n < spacing[i, 2].Length; n++)
                {
                    double s400 = (Math.PI * Math.Pow(stirDiaArr400[n], 2) / 4) / Astr_s[i, 2];
                    if (Astr_s[i, 2] == -2)
                    {
                        spacing[i, 2][n] = -2; //Increase Section Dimensions
                    }
                    else
                    {
                        if (s400 < 100)
                        {
                            spacing[i, 2][n] = -1; //unsafe
                        }
                        else if (s400 >= 100 && s400 < 125)
                        {
                            spacing[i, 2][n] = 100;
                        }
                        else if (s400 >= 125 && s400 < 150)
                        {
                            spacing[i, 2][n] = 125;
                        }
                        else if (s400 >= 150 && s400 < 200)
                        {
                            spacing[i, 2][n] = 150;
                        }
                        else if (s400 >= 200)
                        {
                            spacing[i, 2][n] = 200;
                        }
                    }
                }
            }
            return spacing;
        }

        /*---------------*/
        //public static double[,][] CalcAs_sActual(double[,][] Spacing)
        //{
        //    double[][][] As_sAct = new double[2][][];
        //    for (int i = 0; i < Spacing.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < length; j++)
        //        {

        //        }
        //    }
        //}

        #endregion
    }
}
