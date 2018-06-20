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
using Desing.Core.Sap;
using Design.Presentation.ViewModels;

namespace Design.Core.Dxf
{
    public class DXFTextClass
    {
        #region Static Data Members
        //Bottom RFT:
        public static int[] nRebarBot;
        public static double[] BotchosenDiameterArr;

        //Top RFT
        public static int[] nRebarTopSupport;
        public static double[] TopchosenDiameterArr;

        //RFT TEXT
        public static DxfText[] BotRFTTxt;
        public static DxfText[] TopRFTSupprtTxt;

        //Stirrup Arrays
        public static double[] stirDiaArr240 = new double[] { 8 };
        public static double[] stirDiaArr360 = new double[] { 10, 12, 14, 16 };
        public static double[] stirDiaArr400 = new double[] { 10, 12, 14, 16 };

        //Stirrups Left
        public static double[] spacingLeftSec;
        public static int[] indexesLeft;
        public static DxfText[] stirLeftTxt;

        //Stirrups Right
        public static double[] spacingRightSec;
        public static int[] indexesRight;
        public static DxfText[] stirRightTxt;

        #endregion

        #region Bottom RFT Text
        public static void GetnRebarBotArr(SapFrameElement[] elems)
        {
            nRebarBot = new int[elems.Length];
            for (int i = 0; i < nRebarBot.Length; i++)
            {
                nRebarBot[i] = elems[i].FrameResults.ElementAt(0).NRebarChosenArr[1];
            }
        }

        public static void GetChosenDiameterArr(SapFrameElement[] elems)
        {
            BotchosenDiameterArr = new double[elems.Length];
            for (int i = 0; i < BotchosenDiameterArr.Length; i++)
            {
                BotchosenDiameterArr[i] = elems[i].FrameResults.ElementAt(0).DiameterChosenArr[1];
            }
        }
        public static void BottomRFTTxt(DxfModel model, int nSpans)
        {
            BotRFTTxt = new DxfText[nSpans];

            //Case of Cantilever at start
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[0].SelectedRestraint != Restraints.NoRestraints)
            {
                DxfText text = new DxfText($"{nRebarBot[0]}T{BotchosenDiameterArr[0]}"
                    , new Point3D(DXFPoints.startPointsBot[0].X + 0.60 * DXFRebar.Ln[0], DXFPoints.startPointsBot[0].Y + 0.10/*---*/, 0), 0.2d);
                model.Entities.Add(text);
            }

            //Bottom Rft Text/*------------------*/
            for (int i = 1; i < BotRFTTxt.Length - 1; i++)
            {
                DxfText text = new DxfText($"{nRebarBot[i]}T{BotchosenDiameterArr[i]}"
                    , new Point3D(DXFPoints.startPointsBot[i].X + 0.60 * DXFRebar.Ln[i], DXFPoints.startPointsBot[i].Y + 0.10/*---*/, 0), 0.2d);
                model.Entities.Add(text);
            }

            //Case of Cantilever End
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[RFTCanvas.SpanVals.Length].SelectedRestraint != Restraints.NoRestraints)
            {
                DxfText text = new DxfText($"{nRebarBot[BotRFTTxt.Length - 1]}T{BotchosenDiameterArr[BotRFTTxt.Length - 1]}"
                    , new Point3D(DXFPoints.startPointsBot[BotRFTTxt.Length - 1].X + 0.60 * DXFRebar.Ln[BotRFTTxt.Length - 1], DXFPoints.startPointsBot[BotRFTTxt.Length - 1].Y + 0.10/*---*/, 0), 0.2d);
                model.Entities.Add(text);
            }
        }
        #endregion


        #region Top Suport RFT Text
        public static void GetnRebarTopSupportArr(SapFrameElement[] elems)
        {
            nRebarTopSupport = new int[elems.Length + 1];
            nRebarTopSupport[0] = elems[0].FrameResults.ElementAt(0).NRebarChosenArr[0];
            for (int i = 1; i < nRebarTopSupport.Length - 1; i++)
            {
                nRebarTopSupport[i] = elems[i - 1].FrameResults.ElementAt(0).NRebarChosenArr[2];
            }
            nRebarTopSupport[nRebarTopSupport.Length - 1] = elems[nRebarTopSupport.Length - 2].FrameResults.ElementAt(0).NRebarChosenArr[2];
        }
        public static void GetChosenDiameterTopSupportArr(SapFrameElement[] elems)
        {
            TopchosenDiameterArr = new double[elems.Length + 1];
            TopchosenDiameterArr[0] = elems[0].FrameResults.ElementAt(0).DiameterChosenArr[0];
            for (int i = 1; i < TopchosenDiameterArr.Length; i++)
            {
                TopchosenDiameterArr[i] = elems[i - 1].FrameResults.ElementAt(0).DiameterChosenArr[2];
            }
            TopchosenDiameterArr[TopchosenDiameterArr.Length - 1] = elems[TopchosenDiameterArr.Length - 2].FrameResults.ElementAt(0).DiameterChosenArr[2];
        }
        public static void TopRFTSupportTxt(DxfModel model, int nSpans)
        {
            TopRFTSupprtTxt = new DxfText[nSpans + 1];

            //Case Of Cantilever Start Span
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[0].SelectedRestraint != Restraints.NoRestraints)
            {
                TopRFTSupprtTxt[0] = new DxfText($"{nRebarTopSupport[0]}T{TopchosenDiameterArr[0]}"
                    , new Point3D(DXFRebar.TopSpan[0].Start.X + 10 * 0.15, DXFPoints.startPointsTop[0].Y - 0.3, 0), 0.2d);
                model.Entities.Add(TopRFTSupprtTxt[0]);
            }

            //Top Support RFT/*------------------*/
            
            for (int i = 1; i < TopRFTSupprtTxt.Length - 1; i++)
            {
                /*TopRFTSupprtTxt[i] = new DxfText($"{nRebarTopSupport[i]}T{TopchosenDiameterArr[i]}"
                    , new Point3D(DXFRebar.TopSupport[i - 1].Start.X + 0.60, DXFPoints.startPointsTop[0].Y - 0.3, 0), 0.2d);*/
                TopRFTSupprtTxt[i] = new DxfText($"{nRebarTopSupport[i]}T{TopchosenDiameterArr[i]}"
                , new Point3D(DXFPoints.startPointsTop[i].X + 0.60, DXFPoints.startPointsTop[0].Y - 0.3, 0), 0.2d);
                model.Entities.Add(TopRFTSupprtTxt[i]);
            }

            //Case of Cantilever end Span
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[RFTCanvas.SpanVals.Length].SelectedRestraint != Restraints.NoRestraints)
            {
                TopRFTSupprtTxt[nSpans] = new DxfText($"{nRebarTopSupport[nSpans]}T{TopchosenDiameterArr[nSpans]}"
                , new Point3D(DXFRebar.TopSpan[nSpans].End.X - 10 * 0.15, DXFPoints.startPointsTop[0].Y - 0.3, 0), 0.2d);
                model.Entities.Add(TopRFTSupprtTxt[nSpans]);
            }
            
        }
        #endregion

        #region Stirrups Left
        public static void GetSpacingLeftArr(SapFrameElement[] elems)
        {
            double[] spacing240 = new double[/*elems.Length * */stirDiaArr240.Length];
            double[] spacing360 = new double[/*elems.Length * */stirDiaArr360.Length];
            double[] spacing400 = new double[/*elems.Length * */stirDiaArr400.Length];

            spacingLeftSec = new double[elems.Length];
            indexesLeft = new int[elems.Length];

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
                    indexesLeft[i] = index240;
                }
                else if (s360 != -1)
                {
                    spacingLeftSec[i] = s360;
                    indexesLeft[i] = index360;
                }
                else if (s400 != -1)
                {
                    spacingLeftSec[i] = s400;
                    indexesLeft[i] = index400;
                }
                else if (s240 == -2 || s360 == -2 || s400 == -2)
                {
                    spacingLeftSec[i] = -2;
                    indexesLeft[i] = -2;
                }
                else
                {
                    spacingLeftSec[i] = -1;
                    indexesLeft[i] = -1;
                }
            }
        }


        public static void StirrupLeftTxt(DxfModel model, int nSpans)
        {
            //Stirrups Text Left/*------------------*/
            double fystr = -1;
            stirLeftTxt = new DxfText[nSpans];
            for (int i = 0; i < stirLeftTxt.Length; i++)
            {
                if (indexesLeft[i] != -2)
                {
                    if (indexesLeft[i] < stirDiaArr240.Length)
                    {
                        fystr = 240;
                        stirLeftTxt[i] = new DxfText($"Y{stirDiaArr240[indexesLeft[i]]}@{spacingLeftSec[i]}"
                            , new Point3D(DXFRebar.stirrupsLeft[i, 2].Start.X - 0.85, DXFPoints.startPointsBot[i].Y - 1.40, 0), 0.2d);
                        model.Entities.Add(stirLeftTxt[i]);
                    }
                    else if (indexesLeft[i] < stirDiaArr240.Length + stirDiaArr360.Length)
                    {
                        fystr = 360;
                        stirLeftTxt[i] = new DxfText($"T{stirDiaArr360[indexesLeft[i] - stirDiaArr240.Length]}@{spacingLeftSec[i]}"
                            , new Point3D(DXFRebar.stirrupsLeft[i, 2].Start.X - 0.85, DXFPoints.startPointsBot[i].Y - 1.40, 0), 0.2d);
                        model.Entities.Add(stirLeftTxt[i]);
                    }
                    else if (indexesLeft[i] < stirDiaArr240.Length + stirDiaArr360.Length + stirDiaArr400.Length)
                    {
                        fystr = 400;
                        stirLeftTxt[i] = new DxfText($"T{stirDiaArr400[indexesLeft[i] - stirDiaArr360.Length - stirDiaArr240.Length]}@{spacingLeftSec[i]}"
                            , new Point3D(DXFRebar.stirrupsLeft[i, 2].Start.X - 0.85, DXFPoints.startPointsBot[i].Y - 1.40, 0), 0.2d);
                        model.Entities.Add(stirLeftTxt[i]);
                    }
                    
                }
                else
                {
                    stirLeftTxt[i] = new DxfText($"Increase Dims"
                        , new Point3D(DXFRebar.stirrupsLeft[i, 2].Start.X - 0.85, DXFPoints.startPointsBot[i].Y - 1.40, 0), 0.2d);
                    model.Entities.Add(stirLeftTxt[i]);
                }
            }
        }
        #endregion

        #region Stirrups MidSpan
        //public static void StirrupMidSpanTxt(DxfModel model, int nSpans, DxfLine[,] stirrupsMidspan)
        //{
        //    //Stirrups Text MidSpan/*------------------*/
        //    DxfText[] stirMidSpanTxt = new DxfText[nSpans];
        //    for (int i = 0; i < stirMidSpanTxt.Length; i++)
        //    {
        //        stirMidSpanTxt[i] = new DxfText($"Y8@200mm", new Point3D(stirrupsMidspan[i, 2].Start.X - 0.85, -1.40, 0), 0.2d);
        //        model.Entities.Add(stirMidSpanTxt[i]);
        //    }
        //}
        #endregion

        #region Stirrups Right
        public static void GetSpacingRightArr(SapFrameElement[] elems)
        {
            double[] spacing240 = new double[/*elems.Length * */stirDiaArr240.Length];
            double[] spacing360 = new double[/*elems.Length * */stirDiaArr360.Length];
            double[] spacing400 = new double[/*elems.Length * */stirDiaArr400.Length];

            spacingRightSec = new double[elems.Length];
            indexesRight = new int[elems.Length];

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
                    indexesRight[i] = index240;
                }
                else if (s360 != -1)
                {
                    spacingRightSec[i] = s360;
                    indexesRight[i] = index360;
                }
                else if (s400 != -1)
                {
                    spacingRightSec[i] = s400;
                    indexesRight[i] = index400;
                }
                else if (s240 == -2 || s360 == -2 || s400 == -2)
                {
                    spacingRightSec[i] = -2;
                    indexesRight[i] = -2;
                }
                else
                {
                    spacingRightSec[i] = -1;
                    indexesRight[i] = -1;
                }
            }
        }
        public static void StirrupRightTxt(DxfModel model, int nSpans)
        {
            //Stirrups Text Left/*------------------*/
            double fystr = -1;
            DxfText[] stirRightTxt = new DxfText[nSpans];
            for (int i = 0; i < stirRightTxt.Length; i++)
            {
                if (indexesRight[i] != -2)
                {
                    if (indexesRight[i] < stirDiaArr240.Length)
                    {
                        fystr = 240;
                        stirRightTxt[i] = new DxfText($"Y{stirDiaArr240[indexesRight[i]]}@{spacingRightSec[i]}"
                            , new Point3D(DXFRebar.stirrupsRight[i, 2].Start.X - 0.85, DXFPoints.startPointsBot[i].Y - 1.40, 0), 0.2d);
                        model.Entities.Add(stirRightTxt[i]);
                    }
                    else if (indexesRight[i] < stirDiaArr240.Length + stirDiaArr360.Length)
                    {
                        fystr = 360;
                        stirRightTxt[i] = new DxfText($"T{stirDiaArr360[indexesRight[i] - stirDiaArr240.Length]}@{spacingRightSec[i]}"
                            , new Point3D(DXFRebar.stirrupsRight[i, 2].Start.X - 0.85, DXFPoints.startPointsBot[i].Y - 1.40, 0), 0.2d);
                        model.Entities.Add(stirRightTxt[i]);
                    }
                    else if (indexesRight[i] < stirDiaArr240.Length + stirDiaArr360.Length + stirDiaArr400.Length)
                    {
                        fystr = 400;
                        stirRightTxt[i] = new DxfText($"T{stirDiaArr400[indexesRight[i] - stirDiaArr360.Length - stirDiaArr240.Length]}@{spacingRightSec[i]}"
                            , new Point3D(DXFRebar.stirrupsRight[i, 2].Start.X - 0.85, DXFPoints.startPointsBot[i].Y - 1.40, 0), 0.2d);
                        model.Entities.Add(stirRightTxt[i]);
                    }

                }
                else
                {
                    stirRightTxt[i] = new DxfText($"Increase Dims"
                        , new Point3D(DXFRebar.stirrupsRight[i, 2].Start.X - 0.85, DXFPoints.startPointsBot[i].Y - 1.40, 0), 0.2d);
                    model.Entities.Add(stirRightTxt[i]);
                }
            }
        }
        #endregion
    }
}
