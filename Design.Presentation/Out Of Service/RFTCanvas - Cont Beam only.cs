﻿using Design.Core.Sap;
using Design.Presentation.Geometry;
using Design.Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Design.Presentation.ViewModels
{
    public class RFTCanvas
    {
        #region Static Member Variables
        //Comulative Span Values
        public static double[] SpanVals = new double[GeometryEditorVM.GeometryEditor.NumberOfSpans];
        public static double[] comSpanVals = new double[SpanVals.Length + 1];

        //thicknesses
        public static double[] thickness = new double[SpanVals.Length];

        /*-------------Points------------*/
        //Top Points
        public static Point[] StartPointTopArr = new Point[SpanVals.Length];
        public static Point[] EndPointTopArr = new Point[SpanVals.Length];

        //Bottom Points
        public static Point[] StartPointBotArr = new Point[SpanVals.Length];
        public static Point[] EndPointBotArr = new Point[SpanVals.Length];

        /*------------Beam Lines-------------*/
        //Top Lines
        public static GLine[] TopLineArr = new GLine[SpanVals.Length];
        //Bottom Lines
        public static GLine[] BotLineArr = new GLine[SpanVals.Length];

        /*------------Column Lines-------------*/
        public static GLine[] ColLinesTopStart = new GLine[SpanVals.Length];
        public static GLine[] ColLinesTopEnd = new GLine[SpanVals.Length];
        public static GLine[] ColLinesBotStart = new GLine[SpanVals.Length];
        public static GLine[] ColLinesBotEnd = new GLine[SpanVals.Length];

        /*------------Reinforcement Lines-------------*/
        public static GLine[] BotRFTLines = new GLine[SpanVals.Length];
        public static GLine[] TopRFTLines = new GLine[SpanVals.Length + 1];

        /*------------Stirrups Lines-------------*/
        public static GLine[,] StirrupsLeftSec = new GLine[SpanVals.Length, 3];
        public static GLine[,] StirrupsMidSpan = new GLine[SpanVals.Length, 3];
        public static GLine[,] StirrupsRightSec = new GLine[SpanVals.Length, 3];



        #endregion

        #region Methods
        public static void CalcSpanVals()
        {
            for (int i = 0; i < SpanVals.Length; i++)
            {
                SpanVals[i] = GeometryEditorVM.GeometryEditor.GridData[i].Span;
            }
        }
        public static void CalcComSpanVals(int nSpans)
        {
            comSpanVals[0] = 0;
            for (int i = 1; i < comSpanVals.Length; i++)
            {
                comSpanVals[i] = comSpanVals[i - 1] + SpanVals[i - 1];
            }
        }
        public static void CalcThickness()
        {
            for (int i = 0; i < thickness.Length; i++)
            {
                thickness[i] = GeometryEditorVM.GeometryEditor.GridData[i].SelectedSection.Depth;
            }
        }

        #region CalcPoints

        /*---------------Top Points--------------*/

        public static void ConstructTopStartPoints(double scale)/*scale = 20 as previously stated*/
        {
            for (int i = 0; i < SpanVals.Length; i++)
            {
                StartPointTopArr[i] = new Point((comSpanVals[i] + 0.15) * scale, 100);
            }
        }
        public static void ConstructTopEndPoints(double scale)
        {
            for (int i = 0; i < SpanVals.Length; i++)
            {
                EndPointTopArr[i] = new Point((comSpanVals[i + 1] - 0.15) * scale, 100);
            }
        }
        /*---------------Bottom Points--------------*/
        public static void ConstructBotStartPoints(double scale)
        {
            for (int i = 0; i < SpanVals.Length; i++)
            {
                StartPointBotArr[i] = new Point((comSpanVals[i] + 0.15) * scale, StartPointTopArr[i].Y + thickness[i] * scale);
            }
        }
        public static void ConstructBotEndPoints(double scale)
        {
            for (int i = 0; i < SpanVals.Length; i++)
            {
                EndPointBotArr[i] = new Point((comSpanVals[i + 1] - 0.15) * scale, EndPointTopArr[i].Y + thickness[i] * scale);
            }
        }

        #endregion

        #region BeamLines
        /*--------------------------Top Lines-------------------------*/
        public static void ConstructTopLines(GCanvas gCanvas, GeometryEngine GeometryEngineRFT)
        {
            for (int i = 0; i < SpanVals.Length; i++)
            {
                TopLineArr[i] = new GLine(gCanvas, StartPointTopArr[i], EndPointTopArr[i]);
                //add to the CANVAS 
                GeometryEngineRFT.Shapes["RFT"].Add(TopLineArr[i]);
            }
        }

        /*--------------------------Bottom Lines-------------------------*/
        public static void ConstructBotLines(GCanvas gCanvas, GeometryEngine GeometryEngineRFT)
        {
            for (int i = 0; i < SpanVals.Length; i++)
            {
                BotLineArr[i] = new GLine(gCanvas, StartPointBotArr[i], EndPointBotArr[i]);
                //add to the CANVAS 
                GeometryEngineRFT.Shapes["RFT"].Add(BotLineArr[i]);
            }
        }
        public static void ConstructColLines(GCanvas gCanvas, GeometryEngine GeometryEngineRFT, double scale)
        {
            /*----------------------Bottom Start Lines----------------------*/
            for (int i = 0; i < ColLinesBotStart.Length; i++)
            {
                ColLinesBotStart[i] = new GLine(gCanvas, StartPointBotArr[i], new Point(StartPointBotArr[i].X, StartPointBotArr[i].Y + 0.4 * scale));
                //add to the CANVAS 
                GeometryEngineRFT.Shapes["RFT"].Add(ColLinesBotStart[i]);
            }
            /*----------------------Bottom End Lines----------------------*/
            for (int i = 0; i < ColLinesBotEnd.Length; i++)
            {
                ColLinesBotEnd[i] = new GLine(gCanvas, EndPointBotArr[i], new Point(EndPointBotArr[i].X, EndPointBotArr[i].Y + 0.4 * scale));
                //add to the CANVAS 
                GeometryEngineRFT.Shapes["RFT"].Add(ColLinesBotEnd[i]);
            }

            /*-----------------Top Start Lines---------------*/
            for (int i = 0; i < ColLinesTopStart.Length; i++)
            {
                ColLinesTopStart[i] = new GLine(gCanvas, StartPointTopArr[i], new Point(StartPointTopArr[i].X, StartPointTopArr[i].Y - 0.40 * scale));
                //add to the CANVAS 
                GeometryEngineRFT.Shapes["RFT"].Add(ColLinesTopStart[i]);
            }
            //Top End Lines
            for (int i = 0; i < ColLinesTopEnd.Length; i++)
            {
                ColLinesTopEnd[i] = new GLine(gCanvas, EndPointTopArr[i], new Point(EndPointTopArr[i].X, EndPointTopArr[i].Y - 0.40 * scale));
                //add to the CANVAS 
                GeometryEngineRFT.Shapes["RFT"].Add(ColLinesTopEnd[i]);
            }

            /*-------------Start and End Column Lines-------------*/
            GLine startColLine = new GLine(gCanvas, new Point(StartPointTopArr[0].X - 0.15 * scale, StartPointTopArr[0].Y - 0.40 * scale)
                , new Point(StartPointBotArr[0].X - 0.15 * scale, StartPointBotArr[0].Y + 0.40 * scale));
            //add to the CANVAS 
            GeometryEngineRFT.Shapes["RFT"].Add(startColLine);

            GLine endColLine = new GLine(gCanvas, new Point(EndPointTopArr[SpanVals.Length - 1].X + 0.15 * scale, EndPointTopArr[SpanVals.Length - 1].Y - 0.40 * scale)
                , new Point(EndPointBotArr[SpanVals.Length - 1].X + 0.15 * scale, EndPointBotArr[SpanVals.Length - 1].Y + 0.40 * scale));
            //add to the CANVAS 
            GeometryEngineRFT.Shapes["RFT"].Add(endColLine);
        }

        #endregion


        #region Longitudinal Reinforcement
        public static void BotRFT(GCanvas gCanvas, GeometryEngine GeometryEngineRFT, double scale)
        {
            for (int i = 0; i < BotRFTLines.Length; i++)
            {
                BotRFTLines[i] = new GLine(gCanvas, new Point(StartPointBotArr[i].X + 0.15 * (EndPointBotArr[i].X - StartPointBotArr[i].X), StartPointBotArr[i].Y - 0.07 * scale),
                    new Point(StartPointBotArr[i].X + 0.85 * (EndPointBotArr[i].X - StartPointBotArr[i].X), StartPointBotArr[i].Y - 0.07 * scale));
                //add to the CANVAS 
                GeometryEngineRFT.Shapes["RFT"].Add(BotRFTLines[i]);
            }
        }
        public static void TopRFT(GCanvas gCanvas, GeometryEngine GeometryEngineRFT, double scale)
        {
            TopRFTLines[0] = new GLine(gCanvas, new Point(StartPointTopArr[0].X, StartPointTopArr[0].Y + 0.07 * scale),
                new Point(StartPointTopArr[0].X + 0.20 * (EndPointTopArr[0].X - StartPointTopArr[0].X), StartPointTopArr[0].Y + 0.07 * scale));
            //add to the CANVAS 
            GeometryEngineRFT.Shapes["RFT"].Add(TopRFTLines[0]);

            for (int i = 1; i < TopRFTLines.Length - 1; i++)
            {
                TopRFTLines[i] = new GLine(gCanvas, new Point(EndPointTopArr[i - 1].X - 0.20 * (EndPointTopArr[i - 1].X - StartPointTopArr[i - 1].X), EndPointTopArr[i - 1].Y + 0.07 * scale),
                    new Point(StartPointTopArr[i].X + 0.20 * (EndPointTopArr[i].X - StartPointTopArr[i].X), StartPointTopArr[i].Y + 0.07 * scale));
                //add to the CANVAS 
                GeometryEngineRFT.Shapes["RFT"].Add(TopRFTLines[i]);
            }

            TopRFTLines[TopRFTLines.Length - 1] = new GLine(gCanvas, new Point(StartPointTopArr[TopRFTLines.Length - 2].X + 0.80 * (EndPointTopArr[TopRFTLines.Length - 2].X - StartPointTopArr[TopRFTLines.Length - 2].X), StartPointTopArr[TopRFTLines.Length - 2].Y + 0.07 * scale),
                new Point(EndPointTopArr[TopRFTLines.Length - 2].X, EndPointTopArr[TopRFTLines.Length - 2].Y + 0.07 * scale));
            //add to the CANVAS 
            GeometryEngineRFT.Shapes["RFT"].Add(TopRFTLines[TopRFTLines.Length - 1]);
        }

        #endregion

        #region Stirrups
        public static void LeftSecStirrups(GCanvas gCanvas, GeometryEngine GeometryEngineRFT, double scale)
        {
            double spacingLeft = 0;
            for (int i = 0; i < StirrupsLeftSec.GetLength(0); i++)
            {
                spacingLeft = 0;
                for (int j = 0; j < StirrupsLeftSec.GetLength(1); j++)
                {
                    StirrupsLeftSec[i, j] = new GLine(gCanvas, new Point(StartPointTopArr[i].X + 0.05 * scale + (spacingLeft / 1000) * scale, StartPointTopArr[i].Y + 0.07 * scale)
                        , new Point(StartPointBotArr[i].X + 0.05 * scale + (spacingLeft / 1000) * scale, StartPointBotArr[i].Y/* - 0.07 * scale*/));
                    spacingLeft += 150;

                    //add to the CANVAS 
                    GeometryEngineRFT.Shapes["RFT"].Add(StirrupsLeftSec[i, j]);
                }
            }
        }

        public static void RightSecStirrups(GCanvas gCanvas, GeometryEngine GeometryEngineRFT, double scale)
        {
            double spacingRight = 0;
            for (int i = 0; i < StirrupsRightSec.GetLength(0); i++)
            {
                spacingRight = 0;
                for (int j = 0; j < StirrupsRightSec.GetLength(1); j++)
                {
                    StirrupsRightSec[i, j] = new GLine(gCanvas, new Point(EndPointTopArr[i].X - (0.05 + spacingRight / 1000) * scale, EndPointTopArr[i].Y + 0.07 * scale)
                        , new Point(EndPointBotArr[i].X - (0.05 + spacingRight / 1000) * scale, EndPointBotArr[i].Y/* -  0.07 * scale*/));
                    spacingRight += 150;

                    //add to the CANVAS 
                    GeometryEngineRFT.Shapes["RFT"].Add(StirrupsRightSec[i, j]);
                }
            }
        }

        #endregion

        #endregion
    }
}
