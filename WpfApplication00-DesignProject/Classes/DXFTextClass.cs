using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WW.Cad.IO;
using WW.Cad.Model;
using WW.Cad.Model.Entities;
using WW.Cad.Model.Tables;
using WW.Math;
using SAP2000v20;
using ConsoleApplication1;

namespace WpfApplication00_DesignProject.Classes
{
    class DXFTextClass
    {
        #region Bottom RFT Text
        public static int[] GetnRebarBotArr(SapFrameElement[] elems)
        {
            int[] nRebarBot = new int[elems.Length];
            for (int i = 0; i < nRebarBot.Length; i++)
            {
                nRebarBot[i] = elems[i].FrameResults.ElementAt(0).NRebarChosenArr[1];
            }
            return nRebarBot;
        }

        public static double[] GetChosenDiameterArr(SapFrameElement[] elems)
        {
            double[] chosenDiameterArr = new double[elems.Length];
            for (int i = 0; i < chosenDiameterArr.Length; i++)
            {
                chosenDiameterArr[i] = elems[i].FrameResults.ElementAt(0).DiameterChosenArr[1];
            }
            return chosenDiameterArr;
        }
        public static void BottomRFTTxt(DxfModel model, int nSpans, int[] nRebarBot, double[] chosenDiameter, double[] Ln, Point2D[] startPointsBot)
        {
            //Bottom Rft Text/*------------------*/
            DxfText[] BotRFTTxt = new DxfText[nSpans];
            for (int i = 0; i < BotRFTTxt.Length; i++)
            {
                DxfText text = new DxfText($"{nRebarBot[i]}T{chosenDiameter[i]}", new Point3D(startPointsBot[i].X + 0.60 * Ln[i], 0.10, 0), 0.2d);
                model.Entities.Add(text);
            }
        }
        #endregion


        #region Top Suport RFT Text
        public static int[] GetnRebarTopSupportArr(SapFrameElement[] elems)
        {
            int[] nRebarTopSupport = new int[elems.Length + 1];
            nRebarTopSupport[0] = elems[0].FrameResults.ElementAt(0).NRebarChosenArr[0];
            for (int i = 1; i < nRebarTopSupport.Length - 1; i++)
            {
                nRebarTopSupport[i] = elems[i - 1].FrameResults.ElementAt(0).NRebarChosenArr[2];
            }
            nRebarTopSupport[nRebarTopSupport.Length - 1] = elems[nRebarTopSupport.Length - 2].FrameResults.ElementAt(0).NRebarChosenArr[2];
            return nRebarTopSupport;
        }
        public static double[] GetChosenDiameterTopSupportArr(SapFrameElement[] elems)
        {
            double[] chosenDiameterArr = new double[elems.Length + 1];
            chosenDiameterArr[0] = elems[0].FrameResults.ElementAt(0).DiameterChosenArr[0];
            for (int i = 1; i < chosenDiameterArr.Length; i++)
            {
                chosenDiameterArr[i] = elems[i - 1].FrameResults.ElementAt(0).DiameterChosenArr[2];
            }
            chosenDiameterArr[chosenDiameterArr.Length - 1] = elems[chosenDiameterArr.Length - 2].FrameResults.ElementAt(0).DiameterChosenArr[2];
            return chosenDiameterArr;
        }
        public static void TopRFTSupportTxt(DxfModel model, int nSpans, double thickness,
            int[] nRebarTopSupportArr, double[] chosenDiameterTopSupport, DxfLine[] TopSpan, DxfLine[] TopSupport)
        {
            //Top Support RFT/*------------------*/
            DxfText[] TopRFTSupprtTxt = new DxfText[nSpans + 1];
            TopRFTSupprtTxt[0] = new DxfText($"{nRebarTopSupportArr[0]}T{chosenDiameterTopSupport[0]}", new Point3D(TopSpan[0].Start.X + 10 * 0.15, thickness - 0.3, 0), 0.2d);
            model.Entities.Add(TopRFTSupprtTxt[0]);
            for (int i = 1; i < TopRFTSupprtTxt.Length - 1; i++)
            {
                TopRFTSupprtTxt[i] = new DxfText($"{nRebarTopSupportArr[i]}T{chosenDiameterTopSupport[i]}", new Point3D(TopSupport[i - 1].Start.X + 0.60, thickness - 0.3, 0), 0.2d);
                model.Entities.Add(TopRFTSupprtTxt[i]);
            }
            TopRFTSupprtTxt[nSpans] = new DxfText($"{nRebarTopSupportArr[nSpans]}T{chosenDiameterTopSupport[nSpans]}", new Point3D(TopSpan[nSpans].End.X - 10 * 0.15, thickness - 0.3, 0), 0.2d);
            model.Entities.Add(TopRFTSupprtTxt[nSpans]);
        }
        #endregion

        #region Stirrups Left
        public static double[] GetSpacingLeftArr(SapFrameElement[] elems, double[] stirDiaArr240, double[] stirDiaArr360, double[] stirDiaArr400)
        {
            double[] spacing240 = new double[/*elems.Length * */stirDiaArr240.Length];
            double[] spacing360 = new double[/*elems.Length * */stirDiaArr360.Length];
            double[] spacing400 = new double[/*elems.Length * */stirDiaArr400.Length];

            double[] spacingLeftSec = new double[elems.Length];
            int[] indexes = new int[elems.Length];

            for (int i = 0; i < spacingLeftSec.Length; i++)
            {
                double s240 = 0;
                double s360 = 0;
                double s400 = 0;

                int index240 = 0;
                int index360 = 0;
                int index400 = 0;

                spacing240 = elems[i].FrameResults.ElementAt(0).Spacing[0, 0];
                spacing360 = elems[i].FrameResults.ElementAt(0).Spacing[0, 1];
                spacing400 = elems[i].FrameResults.ElementAt(0).Spacing[0, 2];

                for (int j = 0; j < spacing240.Length; j++)
                {
                    if (spacing240[j] == -1)
                    {
                        s240 = -1;
                        index240 = -1;
                    }
                    else if (spacing240[j] == -2)
                    {
                        s240 = -2;
                        index240 = -2;
                    }
                    else if (spacing240[j] != -1 && spacing240[j] != -2)
                    {
                        s240 = spacing240[j];
                        index240 = j;
                        break;
                    }
                }

                for (int j = 0; j < spacing360.Length; j++)
                {
                    if (spacing360[j] == -1)
                    {
                        s360 = -1;
                        index360 = -1;
                    }
                    else if (spacing360[j] == -2)
                    {
                        s360 = -2;
                        index360 = -2;
                    }
                    else if (spacing360[j] != -1 && spacing360[j] != -2)
                    {
                        s360 = spacing360[j];
                        index360 = j + spacing240.Length;
                        break;
                    }
                }

                for (int j = 0; j < spacing400.Length; j++)
                {
                    if (spacing400[j] == -1)
                    {
                        s400 = -1;
                        index400 = -1;
                    }
                    else if (spacing400[j] == -2)
                    {
                        s400 = -2;
                        index400 = -2;
                    }
                    else if (spacing400[j] != -1 && spacing400[j] != -2)
                    {
                        s400 = spacing400[j];
                        index400 = j + spacing240.Length + spacing360.Length;
                        break;
                    }
                }




                //double s240 = spacing240.FirstOrDefault(e => e != -1);
                //double s360 = spacing360.FirstOrDefault(e => e != -1);
                //double s400 = spacing400.FirstOrDefault(e => e != -1);

                //int index240 = Array.IndexOf(spacing240, spacing240.FirstOrDefault(e => e != -1));
                //int index360 = Array.IndexOf(spacing360, spacing360.FirstOrDefault(e => e != -1));
                //int index400 = Array.IndexOf(spacing400, spacing400.FirstOrDefault(e => e != -1));

                if (s240 != -1)
                {
                    spacingLeftSec[i] = s240;
                    indexes[i] = index240;
                }
                else if (s360 != -1)
                {
                    spacingLeftSec[i] = s360;
                    indexes[i] = index360;
                }
                else if (s400 != -1)
                {
                    spacingLeftSec[i] = s400;
                    indexes[i] = index400;
                }
                else if (s240 == -2 || s360 == -2 || s400 == -2)
                {
                    spacingLeftSec[i] = -2;
                    indexes[i] = -2;
                }
                else
                {
                    spacingLeftSec[i] = -1;
                    indexes[i] = -1;
                }
            }
            return spacingLeftSec;
        }
        public static int[] GetSpacingLeftArrIndexes(SapFrameElement[] elems/*, double[,][] spacing*/
            , double[] stirDiaArr240, double[] stirDiaArr360, double[] stirDiaArr400)
        {
            double[] spacing240 = new double[/*elems.Length * */stirDiaArr240.Length];
            double[] spacing360 = new double[/*elems.Length * */stirDiaArr360.Length];
            double[] spacing400 = new double[/*elems.Length * */stirDiaArr400.Length];

            double[] spacingLeftSec = new double[elems.Length];
            int[] indexes = new int[elems.Length];

            for (int i = 0; i < spacingLeftSec.Length; i++)
            {
                double s240 = 0;
                double s360 = 0;
                double s400 = 0;

                int index240 = 0;
                int index360 = 0;
                int index400 = 0;

                spacing240 = elems[i].FrameResults.ElementAt(0).Spacing[0, 0];
                spacing360 = elems[i].FrameResults.ElementAt(0).Spacing[0, 1];
                spacing400 = elems[i].FrameResults.ElementAt(0).Spacing[0, 2];

                for (int j = 0; j < spacing240.Length; j++)
                {
                    if (spacing240[j] == -1)
                    {
                        s240 = -1;
                        index240 = -1;
                    }
                    else if (spacing240[j] == -2)
                    {
                        s240 = -2;
                        index240 = -2;
                    }
                    else if (spacing240[j] != -1 && spacing240[j] != -2)
                    {
                        s240 = spacing240[j];
                        index240 = j;
                        break;
                    }
                }

                for (int j = 0; j < spacing360.Length; j++)
                {
                    if (spacing360[j] == -1)
                    {
                        s360 = -1;
                        index360 = -1;
                    }
                    else if (spacing360[j] == -2)
                    {
                        s360 = -2;
                        index360 = -2;
                    }
                    else if (spacing360[j] != -1 && spacing360[j] != -2)
                    {
                        s360 = spacing360[j];
                        index360 = j + spacing240.Length;
                        break;
                    }
                }

                for (int j = 0; j < spacing400.Length; j++)
                {
                    if (spacing400[j] == -1)
                    {
                        s400 = -1;
                        index400 = -1;
                    }
                    else if (spacing400[j] == -2)
                    {
                        s400 = -2;
                        index400 = -2;
                    }
                    else if (spacing400[j] != -1 && spacing400[j] != -2)
                    {
                        s400 = spacing400[j];
                        index400 = j + spacing240.Length + spacing360.Length;
                        break;
                    }
                }

                if (s240 != -1)
                {
                    spacingLeftSec[i] = s240;
                    indexes[i] = index240;
                }
                else if (s360 != -1)
                {
                    spacingLeftSec[i] = s360;
                    indexes[i] = index360;
                }
                else if (s400 != -1)
                {
                    spacingLeftSec[i] = s400;
                    indexes[i] = index400;
                }
                else if (s240 == -2 || s360 == -2 || s400 == -2)
                {
                    spacingLeftSec[i] = -2;
                    indexes[i] = -2;
                }
                else
                {
                    spacingLeftSec[i] = -1;
                    indexes[i] = -1;
                }
            }
            return indexes;
        }


        public static void StirrupLeftTxt(DxfModel model, int nSpans, DxfLine[,] stirrupsLeft, double[] spacingLeftSec, int[] spacingLeftIndexes
            , double[] stirDiaArr240, double[] stirDiaArr360, double[] stirDiaArr400)
        {
            //Stirrups Text Left/*------------------*/
            double fystr = -1;
            DxfText[] stirLeftTxt = new DxfText[nSpans];
            for (int i = 0; i < stirLeftTxt.Length; i++)
            {
                if (spacingLeftIndexes[i] != -2)
                {
                    if (spacingLeftIndexes[i] < stirDiaArr240.Length)
                    {
                        fystr = 240;
                        stirLeftTxt[i] = new DxfText($"Y{stirDiaArr240[spacingLeftIndexes[i]]}@{spacingLeftSec[i]}\n{fystr}", new Point3D(stirrupsLeft[i, 2].Start.X - 0.85, -1.40, 0), 0.2d);
                        model.Entities.Add(stirLeftTxt[i]);
                    }
                    else if (spacingLeftIndexes[i] < stirDiaArr240.Length + stirDiaArr360.Length)
                    {
                        fystr = 360;
                        stirLeftTxt[i] = new DxfText($"T{stirDiaArr360[spacingLeftIndexes[i] - stirDiaArr240.Length]}@{spacingLeftSec[i]}\n{fystr}", new Point3D(stirrupsLeft[i, 2].Start.X - 0.85, -1.40, 0), 0.2d);
                        model.Entities.Add(stirLeftTxt[i]);
                    }
                    else if (spacingLeftIndexes[i] < stirDiaArr240.Length + stirDiaArr360.Length + stirDiaArr400.Length)
                    {
                        fystr = 400;
                        stirLeftTxt[i] = new DxfText($"T{stirDiaArr400[spacingLeftIndexes[i] - stirDiaArr360.Length - stirDiaArr240.Length]}@{spacingLeftSec[i]}\n{fystr}", new Point3D(stirrupsLeft[i, 2].Start.X - 0.85, -1.40, 0), 0.2d);
                        model.Entities.Add(stirLeftTxt[i]);
                    }
                    
                }
                else
                {
                    stirLeftTxt[i] = new DxfText($"Increase Dims", new Point3D(stirrupsLeft[i, 2].Start.X - 0.85, -1.40, 0), 0.2d);
                    model.Entities.Add(stirLeftTxt[i]);
                }
            }
        }
        #endregion

        #region Stirrups MidSpan
        public static void StirrupMidSpanTxt(DxfModel model, int nSpans, DxfLine[,] stirrupsMidspan)
        {
            //Stirrups Text MidSpan/*------------------*/
            DxfText[] stirMidSpanTxt = new DxfText[nSpans];
            for (int i = 0; i < stirMidSpanTxt.Length; i++)
            {
                stirMidSpanTxt[i] = new DxfText($"Y8@200mm", new Point3D(stirrupsMidspan[i, 2].Start.X - 0.85, -1.40, 0), 0.2d);
                model.Entities.Add(stirMidSpanTxt[i]);
            }
        }
        #endregion

        #region Stirrups Right
        public static double[] GetSpacingRightArr(SapFrameElement[] elems, double[] stirDiaArr240, double[] stirDiaArr360, double[] stirDiaArr400)
        {
            double[] spacing240 = new double[/*elems.Length * */stirDiaArr240.Length];
            double[] spacing360 = new double[/*elems.Length * */stirDiaArr360.Length];
            double[] spacing400 = new double[/*elems.Length * */stirDiaArr400.Length];

            double[] spacingRightSec = new double[elems.Length];
            int[] indexes = new int[elems.Length];

            for (int i = 0; i < spacingRightSec.Length; i++)
            {
                double s240 = 0;
                double s360 = 0;
                double s400 = 0;

                int index240 = 0;
                int index360 = 0;
                int index400 = 0;

                spacing240 = elems[i].FrameResults.ElementAt(0).Spacing[1, 0];
                spacing360 = elems[i].FrameResults.ElementAt(0).Spacing[1, 1];
                spacing400 = elems[i].FrameResults.ElementAt(0).Spacing[1, 2];

                for (int j = 0; j < spacing240.Length; j++)
                {
                    if (spacing240[j] == -1)
                    {
                        s240 = -1;
                        index240 = -1;
                    }
                    else if (spacing240[j] == -2)
                    {
                        s240 = -2;
                        index240 = -2;
                    }
                    else if (spacing240[j] != -1 && spacing240[j] != -2)
                    {
                        s240 = spacing240[j];
                        index240 = j;
                        break;
                    }
                }

                for (int j = 0; j < spacing360.Length; j++)
                {
                    if (spacing360[j] == -1)
                    {
                        s360 = -1;
                        index360 = -1;
                    }
                    else if (spacing360[j] == -2)
                    {
                        s360 = -2;
                        index360 = -2;
                    }
                    else if (spacing360[j] != -1 && spacing360[j] != -2)
                    {
                        s360 = spacing360[j];
                        index360 = j + spacing240.Length;
                        break;
                    }
                }

                for (int j = 0; j < spacing400.Length; j++)
                {
                    if (spacing400[j] == -1)
                    {
                        s400 = -1;
                        index400 = -1;
                    }
                    else if (spacing400[j] == -2)
                    {
                        s400 = -2;
                        index400 = -2;
                    }
                    else if (spacing400[j] != -1 && spacing400[j] != -2)
                    {
                        s400 = spacing400[j];
                        index400 = j + spacing240.Length + spacing360.Length;
                        break;
                    }
                }

                if (s240 != -1)
                {
                    spacingRightSec[i] = s240;
                    indexes[i] = index240;
                }
                else if (s360 != -1)
                {
                    spacingRightSec[i] = s360;
                    indexes[i] = index360;
                }
                else if (s400 != -1)
                {
                    spacingRightSec[i] = s400;
                    indexes[i] = index400;
                }
                else if (s240 == -2 || s360 == -2 || s400 == -2)
                {
                    spacingRightSec[i] = -2;
                    indexes[i] = -2;
                }
                else
                {
                    spacingRightSec[i] = -1;
                    indexes[i] = -1;
                }
            }
            return spacingRightSec;
        }
        public static int[] GetSpacingRightArrIndexes(SapFrameElement[] elems/*, double[,][] spacing*/
            , double[] stirDiaArr240, double[] stirDiaArr360, double[] stirDiaArr400)
        {
            double[] spacing240 = new double[/*elems.Length * */stirDiaArr240.Length];
            double[] spacing360 = new double[/*elems.Length * */stirDiaArr360.Length];
            double[] spacing400 = new double[/*elems.Length * */stirDiaArr400.Length];

            double[] spacingRightSec = new double[elems.Length];
            int[] indexes = new int[elems.Length];

            for (int i = 0; i < spacingRightSec.Length; i++)
            {
                double s240 = 0;
                double s360 = 0;
                double s400 = 0;

                int index240 = 0;
                int index360 = 0;
                int index400 = 0;

                spacing240 = elems[i].FrameResults.ElementAt(0).Spacing[1, 0];
                spacing360 = elems[i].FrameResults.ElementAt(0).Spacing[1, 1];
                spacing400 = elems[i].FrameResults.ElementAt(0).Spacing[1, 2];

                for (int j = 0; j < spacing240.Length; j++)
                {
                    if (spacing240[j] == -1)
                    {
                        s240 = -1;
                        index240 = -1;
                    }
                    else if (spacing240[j] == -2)
                    {
                        s240 = -2;
                        index240 = -2;
                    }
                    else if (spacing240[j] != -1 && spacing240[j] != -2)
                    {
                        s240 = spacing240[j];
                        index240 = j;
                        break;
                    }
                }

                for (int j = 0; j < spacing360.Length; j++)
                {
                    if (spacing360[j] == -1)
                    {
                        s360 = -1;
                        index360 = -1;
                    }
                    else if (spacing360[j] == -2)
                    {
                        s360 = -2;
                        index360 = -2;
                    }
                    else if (spacing360[j] != -1 && spacing360[j] != -2)
                    {
                        s360 = spacing360[j];
                        index360 = j + spacing240.Length;
                        break;
                    }
                }

                for (int j = 0; j < spacing400.Length; j++)
                {
                    if (spacing400[j] == -1)
                    {
                        s400 = -1;
                        index400 = -1;
                    }
                    else if (spacing400[j] == -2)
                    {
                        s400 = -2;
                        index400 = -2;
                    }
                    else if (spacing400[j] != -1 && spacing400[j] != -2)
                    {
                        s400 = spacing400[j];
                        index400 = j + spacing240.Length + spacing360.Length;
                        break;
                    }
                }

                if (s240 != -1)
                {
                    spacingRightSec[i] = s240;
                    indexes[i] = index240;
                }
                else if (s360 != -1)
                {
                    spacingRightSec[i] = s360;
                    indexes[i] = index360;
                }
                else if (s400 != -1)
                {
                    spacingRightSec[i] = s400;
                    indexes[i] = index400;
                }
                else if (s240 == -2 || s360 == -2 || s400 == -2)
                {
                    spacingRightSec[i] = -2;
                    indexes[i] = -2;
                }
                else
                {
                    spacingRightSec[i] = -1;
                    indexes[i] = -1;
                }
            }
            return indexes;
        }


        public static void StirrupRightTxt(DxfModel model, int nSpans, DxfLine[,] stirrupsRight, double[] spacingRightSec, int[] spacingRightIndexes
            , double[] stirDiaArr240, double[] stirDiaArr360, double[] stirDiaArr400)
        {
            //Stirrups Text Left/*------------------*/
            double fystr = -1;
            DxfText[] stirRightTxt = new DxfText[nSpans];
            for (int i = 0; i < stirRightTxt.Length; i++)
            {
                if (spacingRightIndexes[i] != -2)
                {
                    if (spacingRightIndexes[i] < stirDiaArr240.Length)
                    {
                        fystr = 240;
                        stirRightTxt[i] = new DxfText($"Y{stirDiaArr240[spacingRightIndexes[i]]}@{spacingRightSec[i]}\n{fystr}", new Point3D(stirrupsRight[i, 2].Start.X - 0.85, -1.40, 0), 0.2d);
                        model.Entities.Add(stirRightTxt[i]);
                    }
                    else if (spacingRightIndexes[i] < stirDiaArr240.Length + stirDiaArr360.Length)
                    {
                        fystr = 360;
                        stirRightTxt[i] = new DxfText($"T{stirDiaArr360[spacingRightIndexes[i] - stirDiaArr240.Length]}@{spacingRightSec[i]}\n{fystr}", new Point3D(stirrupsRight[i, 2].Start.X - 0.85, -1.40, 0), 0.2d);
                        model.Entities.Add(stirRightTxt[i]);
                    }
                    else if (spacingRightIndexes[i] < stirDiaArr240.Length + stirDiaArr360.Length + stirDiaArr400.Length)
                    {
                        fystr = 400;
                        stirRightTxt[i] = new DxfText($"T{stirDiaArr400[spacingRightIndexes[i] - stirDiaArr360.Length - stirDiaArr240.Length]}@{spacingRightSec[i]}\n{fystr}", new Point3D(stirrupsRight[i, 2].Start.X - 0.85, -1.40, 0), 0.2d);
                        model.Entities.Add(stirRightTxt[i]);
                    }

                }
                else
                {
                    stirRightTxt[i] = new DxfText($"Increase Dims", new Point3D(stirrupsRight[i, 2].Start.X - 0.85, -1.40, 0), 0.2d);
                    model.Entities.Add(stirRightTxt[i]);
                }
            }
        }
        #endregion
    }
}
