using Design.Presentation.Geometry;
using Desing.Core.Sap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Design.Presentation.ViewModels
{
    class TxtRFTCanvas
    {
        #region Static Members
        //Stirrup Arrays
        public static double[] stirDiaArr240 = new double[] { 8 };
        public static double[] stirDiaArr360 = new double[] { 10, 12, 14, 16 };
        public static double[] stirDiaArr400 = new double[] { 10, 12, 14, 16 };

        public static double[] SpanVals = new double[GeometryEditorVM.GeometryEditor.NumberOfSpans];
        public static double[] comSpanVals = new double[SpanVals.Length + 1];

        public static int[] nRebarBot;
        public static double[] BotChosenDiameterArr;

        public static int[] nRebarTopSupport;
        public static double[] TopChosenDiameterArr;

        public static GText[] BotRFTTxt = new GText[SpanVals.Length];
        public static GText[] TopRFTTxt = new GText[SpanVals.Length + 1];


        /*Stirrups Left*/
        public static double[] spacingLeftSec;
        public static int[] indexesLeft;
        public static GText[] stirLeftTxt = new GText[SpanVals.Length];

        /*Stirrups Right*/
        public static double[] spacingRightSec;
        public static int[] indexesRight;
        public static GText[] stirRightTxt = new GText[SpanVals.Length];

        #endregion

        #region General Methods
        public static void CalcSpanVals()
        {
            for (int i = 0; i < SpanVals.Length; i++)
            {
                SpanVals[i] = GeometryEditorVM.GeometryEditor.GridData[i].Span;
            }
        }
        public static void CalcComSpanVals()
        {
            comSpanVals[0] = 0;
            for (int i = 1; i < comSpanVals.Length; i++)
            {
                comSpanVals[i] = comSpanVals[i - 1] + SpanVals[i - 1];
            }
        }
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
            BotChosenDiameterArr = new double[elems.Length];
            for (int i = 0; i < BotChosenDiameterArr.Length; i++)
            {
                BotChosenDiameterArr[i] = elems[i].FrameResults.ElementAt(0).DiameterChosenArr[1];
            }
        }

        public static void CreateBottomRFTTxt(GCanvas gCanvas, GeometryEngine GeometryEngineRFT, double scale)
        {
            //Case of Cantilever at start
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[0].SelectedRestraint != Restraints.NoRestraints)
            {
                BotRFTTxt[0] = new GText(gCanvas, new Point(RFTCanvas.StartPointBotArr[0].X + 0.30 * SpanVals[0] * scale
                    , RFTCanvas.StartPointBotArr[0].Y - 0.70 * scale), $"{nRebarBot[0]}T{BotChosenDiameterArr[0]}");
                GeometryEngineRFT.Shapes["Text"].Add(BotRFTTxt[0]);
            }
            //Bottom Rft Text/*------------------*/
            for (int i = 1; i < BotRFTTxt.Length - 1; i++)
            {
                BotRFTTxt[i] = new GText(gCanvas, new Point(RFTCanvas.StartPointBotArr[i].X + 0.30 * SpanVals[i] * scale
                    , RFTCanvas.StartPointBotArr[i].Y - 0.70 * scale), $"{nRebarBot[i]}T{BotChosenDiameterArr[i]}");
                GeometryEngineRFT.Shapes["Text"].Add(BotRFTTxt[i]);
            }
            //Case of Cantilever End
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[SpanVals.Length].SelectedRestraint != Restraints.NoRestraints)
            {
                BotRFTTxt[BotRFTTxt.Length - 1] = new GText(gCanvas, new Point(RFTCanvas.StartPointBotArr[BotRFTTxt.Length - 1].X + 0.30 * SpanVals[BotRFTTxt.Length - 1] * scale
                    , RFTCanvas.StartPointBotArr[BotRFTTxt.Length - 1].Y - 0.70 * scale), $"{nRebarBot[BotRFTTxt.Length - 1]}T{BotChosenDiameterArr[BotRFTTxt.Length - 1]}");
                GeometryEngineRFT.Shapes["Text"].Add(BotRFTTxt[BotRFTTxt.Length - 1]);
            }
        }
        #endregion


        #region Top RFT Text

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
            TopChosenDiameterArr = new double[elems.Length + 1];
            TopChosenDiameterArr[0] = elems[0].FrameResults.ElementAt(0).DiameterChosenArr[0];
            for (int i = 1; i < TopChosenDiameterArr.Length; i++)
            {
                TopChosenDiameterArr[i] = elems[i - 1].FrameResults.ElementAt(0).DiameterChosenArr[2];
            }
            TopChosenDiameterArr[TopChosenDiameterArr.Length - 1] = elems[TopChosenDiameterArr.Length - 2].FrameResults.ElementAt(0).DiameterChosenArr[2];
        }

        public static void CreateTopRFTTxt(GCanvas gCanvas, GeometryEngine GeometryEngineRFT, double scale)
        {
            //Top Support RFT/*------------------*/
            
            //Case Of Cantilever Start Span
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[0].SelectedRestraint != Restraints.NoRestraints)
            {
                TopRFTTxt[0] = new GText(gCanvas, new Point(RFTCanvas.TopRFTLines[0].EndPoint.X /*+ 0.30 * SpanVals[0] * scale*/
                    , RFTCanvas.TopRFTLines[0].EndPoint.Y /*+ 0.07 * scale*/), $"{nRebarTopSupport[0]}T{TopChosenDiameterArr[0]}");
                GeometryEngineRFT.Shapes["Text"].Add(TopRFTTxt[0]);
            }


            for (int i = 1; i < TopRFTTxt.Length - 1; i++)
            {
                TopRFTTxt[i] = new GText(gCanvas, new Point(RFTCanvas.TopRFTLines[i].EndPoint.X /*+ 0.30 * SpanVals[i] * scale*/
                    , RFTCanvas.TopRFTLines[i].EndPoint.Y /*+ 0.07 * scale*/), $"{nRebarTopSupport[i]}T{TopChosenDiameterArr[i]}");
                GeometryEngineRFT.Shapes["Text"].Add(TopRFTTxt[i]);
            }

            //Case of Cantilever end Span
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[SpanVals.Length].SelectedRestraint != Restraints.NoRestraints)
            {
                TopRFTTxt[TopRFTTxt.Length - 1] = new GText(gCanvas, new Point(RFTCanvas.TopRFTLines[TopRFTTxt.Length - 1].EndPoint.X /*+ 0.30 * SpanVals[TopRFTTxt.Length - 1] * scale*/
                    , RFTCanvas.TopRFTLines[TopRFTTxt.Length - 1].EndPoint.Y /*+ 0.07 * scale*/), $"{nRebarTopSupport[TopRFTTxt.Length]}T{TopChosenDiameterArr[TopRFTTxt.Length]}");
                GeometryEngineRFT.Shapes["Text"].Add(TopRFTTxt[TopRFTTxt.Length - 1]);
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
        }

        public static void GetSpacingLeftArrIndexes(SapFrameElement[] elems)
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


        public static void StirrupLeftTxt(GCanvas gCanvas, GeometryEngine GeometryEngineRFT, double scale)
        {
            //Stirrups Text Left/*------------------*/
            double fystr = -1;
            /*Case Of Cantilever at left End*/
            //Case of Cantilever at start
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[0].SelectedRestraint != Restraints.NoRestraints)
            {
                if (indexesLeft[0] != -2)
                {
                    if (indexesLeft[0] < stirDiaArr240.Length)
                    {
                        fystr = 240;
                        stirLeftTxt[0] = new GText(gCanvas
                            , new Point(RFTCanvas.StirrupsLeftSec[0, 2].StartPoint.X, RFTCanvas.StirrupsLeftSec[0, 2].StartPoint.Y - 0.07 * scale)
                            , $"Y{stirDiaArr240[indexesLeft[0]]}@{spacingLeftSec[0]}");
                        GeometryEngineRFT.Shapes["Text"].Add(stirLeftTxt[0]);

                    }
                    else if (indexesLeft[0] < stirDiaArr240.Length + stirDiaArr360.Length)
                    {
                        fystr = 360;
                        stirLeftTxt[0] = new GText(gCanvas
                            , new Point(RFTCanvas.StirrupsLeftSec[0, 2].StartPoint.X, RFTCanvas.StirrupsLeftSec[0, 2].StartPoint.Y - 0.07 * scale)
                            , $"T{stirDiaArr360[indexesLeft[0] - stirDiaArr240.Length]}@{spacingLeftSec[0]}");
                        GeometryEngineRFT.Shapes["Text"].Add(stirLeftTxt[0]);
                    }
                    else if (indexesLeft[0] < stirDiaArr240.Length + stirDiaArr360.Length + stirDiaArr400.Length)
                    {
                        fystr = 400;
                        stirLeftTxt[0] = new GText(gCanvas
                            , new Point(RFTCanvas.StirrupsLeftSec[0, 2].StartPoint.X, RFTCanvas.StirrupsLeftSec[0, 2].StartPoint.Y - 0.07 * scale)
                            , $"T{stirDiaArr400[indexesLeft[0] - stirDiaArr360.Length - stirDiaArr240.Length]}@{spacingLeftSec[0]}");
                        GeometryEngineRFT.Shapes["Text"].Add(stirLeftTxt[0]);
                    }

                }
                else
                {
                    stirLeftTxt[0] = new GText(gCanvas
                            , new Point(RFTCanvas.StirrupsLeftSec[0, 2].StartPoint.X, RFTCanvas.StirrupsLeftSec[0, 2].StartPoint.Y - 0.07 * scale)
                            , $"Increase Dims");
                    GeometryEngineRFT.Shapes["Text"].Add(stirLeftTxt[0]);
                }
            }
            

            for (int i = 1; i < stirLeftTxt.Length; i++)
            {
                if (indexesLeft[i] != -2)
                {
                    if (indexesLeft[i] < stirDiaArr240.Length)
                    {
                        fystr = 240;
                        stirLeftTxt[i] = new GText(gCanvas
                            , new Point(RFTCanvas.StirrupsLeftSec[i, 2].StartPoint.X, RFTCanvas.StirrupsLeftSec[i, 2].StartPoint.Y - 0.07 * scale)
                            , $"Y{stirDiaArr240[indexesLeft[i]]}@{spacingLeftSec[i]}");
                        GeometryEngineRFT.Shapes["Text"].Add(stirLeftTxt[i]);

                    }
                    else if (indexesLeft[i] < stirDiaArr240.Length + stirDiaArr360.Length)
                    {
                        fystr = 360;
                        stirLeftTxt[i] = new GText(gCanvas
                            , new Point(RFTCanvas.StirrupsLeftSec[i, 2].StartPoint.X, RFTCanvas.StirrupsLeftSec[i, 2].StartPoint.Y - 0.07 * scale)
                            , $"T{stirDiaArr360[indexesLeft[i] - stirDiaArr240.Length]}@{spacingLeftSec[i]}");
                        GeometryEngineRFT.Shapes["Text"].Add(stirLeftTxt[i]);
                    }
                    else if (indexesLeft[i] < stirDiaArr240.Length + stirDiaArr360.Length + stirDiaArr400.Length)
                    {
                        fystr = 400;
                        stirLeftTxt[i] = new GText(gCanvas
                            , new Point(RFTCanvas.StirrupsLeftSec[i, 2].StartPoint.X, RFTCanvas.StirrupsLeftSec[i, 2].StartPoint.Y - 0.07 * scale)
                            , $"T{stirDiaArr400[indexesLeft[i] - stirDiaArr360.Length - stirDiaArr240.Length]}@{spacingLeftSec[i]}");
                        GeometryEngineRFT.Shapes["Text"].Add(stirLeftTxt[i]);
                    }

                }
                else
                {
                    stirLeftTxt[i] = new GText(gCanvas
                            , new Point(RFTCanvas.StirrupsLeftSec[i, 2].StartPoint.X, RFTCanvas.StirrupsLeftSec[i, 2].StartPoint.Y - 0.07 * scale)
                            , $"Increase Dims");
                    GeometryEngineRFT.Shapes["Text"].Add(stirLeftTxt[i]);
                }
            }
        }
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
        public static void GetSpacingRightArrIndexes(SapFrameElement[] elems)
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


        public static void StirrupRightTxt(GCanvas gCanvas, GeometryEngine GeometryEngineRFT, double scale)
        {
            //Stirrups Text Left/*------------------*/
            double fystr = -1;

            

            for (int i = 0; i < stirRightTxt.Length - 1; i++)
            {
                if (indexesRight[i] != -2)
                {
                    if (indexesRight[i] < stirDiaArr240.Length)
                    {
                        fystr = 240;
                        stirRightTxt[i] = new GText(gCanvas
                            , new Point(RFTCanvas.StirrupsRightSec[i, 2].StartPoint.X, RFTCanvas.StirrupsRightSec[i, 2].StartPoint.Y - 0.07 * scale)
                            , $"Y{stirDiaArr240[indexesRight[i]]}@{spacingRightSec[i]}");
                        GeometryEngineRFT.Shapes["Text"].Add(stirRightTxt[i]);
                    }
                    else if (indexesRight[i] < stirDiaArr240.Length + stirDiaArr360.Length)
                    {
                        fystr = 360;
                        stirRightTxt[i] = new GText(gCanvas
                            , new Point(RFTCanvas.StirrupsRightSec[i, 2].StartPoint.X, RFTCanvas.StirrupsRightSec[i, 2].StartPoint.Y - 0.07 * scale)
                            , $"T{stirDiaArr360[indexesRight[i] - stirDiaArr240.Length]}@{spacingRightSec[i]}");
                        GeometryEngineRFT.Shapes["Text"].Add(stirRightTxt[i]);
                    }
                    else if (indexesRight[i] < stirDiaArr240.Length + stirDiaArr360.Length + stirDiaArr400.Length)
                    {
                        fystr = 400;
                        stirRightTxt[i] = new GText(gCanvas
                            , new Point(RFTCanvas.StirrupsRightSec[i, 2].StartPoint.X, RFTCanvas.StirrupsRightSec[i, 2].StartPoint.Y - 0.07 * scale)
                            , $"T{stirDiaArr400[indexesRight[i] - stirDiaArr360.Length - stirDiaArr240.Length]}");
                        GeometryEngineRFT.Shapes["Text"].Add(stirRightTxt[i]);
                    }

                }
                else
                {
                    stirRightTxt[i] = new GText(gCanvas
                            , new Point(RFTCanvas.StirrupsRightSec[i, 2].StartPoint.X, RFTCanvas.StirrupsRightSec[i, 2].StartPoint.Y - 0.07 * scale)
                            , $"Increase Dims");
                    GeometryEngineRFT.Shapes["Text"].Add(stirRightTxt[i]);
                }
            }

            //Case Of Cantilever at right End
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[0].SelectedRestraint != Restraints.NoRestraints)
            {
                if (indexesRight[stirRightTxt.Length - 1] != -2)
                {
                    if (indexesRight[stirRightTxt.Length - 1] < stirDiaArr240.Length)
                    {
                        fystr = 240;
                        stirRightTxt[stirRightTxt.Length - 1] = new GText(gCanvas
                            , new Point(RFTCanvas.StirrupsRightSec[stirRightTxt.Length - 1, 2].StartPoint.X, RFTCanvas.StirrupsRightSec[stirRightTxt.Length - 1, 2].StartPoint.Y - 0.07 * scale)
                            , $"Y{stirDiaArr240[indexesRight[stirRightTxt.Length - 1]]}@{spacingRightSec[stirRightTxt.Length - 1]}");
                        GeometryEngineRFT.Shapes["Text"].Add(stirRightTxt[stirRightTxt.Length - 1]);
                    }
                    else if (indexesRight[stirRightTxt.Length - 1] < stirDiaArr240.Length + stirDiaArr360.Length)
                    {
                        fystr = 360;
                        stirRightTxt[stirRightTxt.Length - 1] = new GText(gCanvas
                            , new Point(RFTCanvas.StirrupsRightSec[stirRightTxt.Length - 1, 2].StartPoint.X, RFTCanvas.StirrupsRightSec[stirRightTxt.Length - 1, 2].StartPoint.Y - 0.07 * scale)
                            , $"T{stirDiaArr360[indexesRight[stirRightTxt.Length - 1] - stirDiaArr240.Length]}@{spacingRightSec[stirRightTxt.Length - 1]}");
                        GeometryEngineRFT.Shapes["Text"].Add(stirRightTxt[stirRightTxt.Length - 1]);
                    }
                    else if (indexesRight[stirRightTxt.Length - 1] < stirDiaArr240.Length + stirDiaArr360.Length + stirDiaArr400.Length)
                    {
                        fystr = 400;
                        stirRightTxt[stirRightTxt.Length - 1] = new GText(gCanvas
                            , new Point(RFTCanvas.StirrupsRightSec[stirRightTxt.Length - 1, 2].StartPoint.X, RFTCanvas.StirrupsRightSec[stirRightTxt.Length - 1, 2].StartPoint.Y - 0.07 * scale)
                            , $"T{stirDiaArr400[indexesRight[stirRightTxt.Length - 1] - stirDiaArr360.Length - stirDiaArr240.Length]}");
                        GeometryEngineRFT.Shapes["Text"].Add(stirRightTxt[stirRightTxt.Length - 1]);
                    }

                }
                else
                {
                    stirRightTxt[stirRightTxt.Length - 1] = new GText(gCanvas
                            , new Point(RFTCanvas.StirrupsRightSec[stirRightTxt.Length - 1, 2].StartPoint.X, RFTCanvas.StirrupsRightSec[stirRightTxt.Length - 1, 2].StartPoint.Y - 0.07 * scale)
                            , $"Increase Dims");
                    GeometryEngineRFT.Shapes["Text"].Add(stirRightTxt[stirRightTxt.Length - 1]);
                }
            }
        }
        #endregion

    }
}
