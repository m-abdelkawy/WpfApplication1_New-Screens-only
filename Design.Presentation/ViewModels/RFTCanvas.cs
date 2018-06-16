using Design.Core.Sap;
using Design.Presentation.Geometry;
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
        public static double[] SpanVals = AnalysisMapping.spanValues;
        public static double[] comSpanVals = new double[SpanVals.Length + 1];

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
        public static GLine[] TopRFTLines = new GLine[SpanVals.Length - 1];

        /*------------Stirrups Lines-------------*/
        public static GLine[,] StirrupsLeftSec = new GLine[SpanVals.Length, 3];
        public static GLine[,] StirrupsMidSpan = new GLine[SpanVals.Length, 3];
        public static GLine[,] StirrupsRightSec = new GLine[SpanVals.Length, 3];



        #endregion

        #region Methods

        public static void CalcComSpanVals(int nSpans)
        {
            comSpanVals[0] = 0;
            for (int i = 1; i < comSpanVals.Length; i++)
            {
                comSpanVals[i] += SpanVals[i];
            }
        }

        #region CalcPoints
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
        public static void ConstructBotStartPoints(double scale, double[] thickness)
        {
            for (int i = 0; i < SpanVals.Length; i++)
            {
                StartPointBotArr[i] = new Point((comSpanVals[i] + 0.15) * scale, thickness[i] * scale);
            }
        }
        public static void ConstructBotEndPoints(double scale, double[] thickness)
        {
            for (int i = 0; i < SpanVals.Length; i++)
            {
                EndPointBotArr[i] = new Point((comSpanVals[i + 1] - 0.15) * scale, thickness[i] * scale);
            }
        }


        #endregion

        #region BeamLines
        public static void ConstructTopLines(GCanvas gCanvas)
        {
            for (int i = 0; i < SpanVals.Length; i++)
            {
                TopLineArr[i] = new GLine(gCanvas, StartPointTopArr[i], EndPointTopArr[i]);
                //add to the CANVAS 
                /*Code here*/
            }
        }
        public static void ConstructBotLines(GCanvas gCanvas)
        {
            for (int i = 0; i < SpanVals.Length; i++)
            {
                BotLineArr[i] = new GLine(gCanvas, StartPointBotArr[i], EndPointBotArr[i]);
                //add to the CANVAS 
                /*Code here*/
            }
        }
        public static void ConstructColLines(GCanvas gCanvas, double scale, double[] thickness)
        {
            //Top Start Lines
            for (int i = 0; i < ColLinesTopStart.Length; i++)
            {
                ColLinesTopStart[i] = new GLine(gCanvas, StartPointTopArr[i], new Point(StartPointTopArr[i].X, -0.40 * scale));
                //add to the CANVAS 
                /*Code here*/
            }
            //Top End Lines
            for (int i = 0; i < ColLinesTopEnd.Length; i++)
            {
                ColLinesTopEnd[i] = new GLine(gCanvas, EndPointTopArr[i], new Point(EndPointTopArr[i].X, -0.40 * scale));
                //add to the CANVAS 
                /*Code here*/
            }
            //Bottom Start Lines
            for (int i = 0; i < ColLinesBotStart.Length; i++)
            {
                ColLinesBotStart[i] = new GLine(gCanvas, StartPointBotArr[i], new Point(StartPointBotArr[i].X, (thickness[i] + 0.40) * scale));
                //add to the CANVAS 
                /*Code here*/
            }
            //Bottom End Lines
            for (int i = 0; i < ColLinesBotEnd.Length; i++)
            {
                ColLinesBotEnd[i] = new GLine(gCanvas, EndPointBotArr[i], new Point(EndPointBotArr[i].X, (thickness[i] + 0.40) * scale));
                //add to the CANVAS 
                /*Code here*/
            }
            /*-------------Start and End Column Lines-------------*/
            GLine startColLine = new GLine(gCanvas, new Point(StartPointTopArr[0].X - 0.15 * scale, -0.40 * scale), new Point(StartPointBotArr[0].X - 0.15 * scale, 0.40 * scale));
            //add to the CANVAS 
            /*Code here*/
            GLine endColLine = new GLine(gCanvas, new Point(EndPointTopArr[SpanVals.Length - 1].X + 0.15 * scale, -0.40 * scale), new Point(EndPointBotArr[SpanVals.Length - 1].X + 0.15 * scale, 0.40 * scale));
            //add to the CANVAS 
            /*Code here*/
        }

        #endregion


        #region Longitudinal Reinforcement
        public static void BotRFT(GCanvas gCanvas, double scale)
        {
            for (int i = 0; i < BotRFTLines.Length; i++)
            {
                BotRFTLines[i] = new GLine(gCanvas, new Point(StartPointBotArr[i].X + 0.15 * (EndPointBotArr[i].X - StartPointBotArr[i].X), StartPointBotArr[i].Y - 0.07 * scale),
                    new Point(StartPointBotArr[i].X + 0.85 * (EndPointBotArr[i].X - StartPointBotArr[i].X), StartPointBotArr[i].Y - 0.07 * scale));
                //add to the CANVAS 
                /*Code here*/
            }
        }
        public static void TopRFT(GCanvas gCanvas, double scale)
        {
            TopRFTLines[0] = new GLine(gCanvas, new Point(StartPointTopArr[0].X, StartPointTopArr[0].Y + 0.07 * scale),
                new Point(StartPointTopArr[0].X + 0.15 * (EndPointBotArr[0].X - StartPointBotArr[0].X), StartPointTopArr[0].Y + 0.07 * scale));
            //add to the CANVAS 
            /*Code here*/
            for (int i = 1; i < TopRFTLines.Length - 1; i++)
            {
                TopRFTLines[i] = new GLine(gCanvas, new Point(EndPointTopArr[i - 1].X - 0.15 * (EndPointTopArr[i - 1].X - StartPointTopArr[i - 1].X), EndPointTopArr[i - 1].Y + 0.07 * scale),
                    new Point(StartPointTopArr[i].X + 0.15 * (EndPointTopArr[i].X - StartPointTopArr[i].X), StartPointTopArr[i].Y + 0.07 * scale));
                //add to the CANVAS 
                /*Code here*/
            }
            TopRFTLines[TopRFTLines.Length - 1] = new GLine(gCanvas, new Point(StartPointTopArr[TopRFTLines.Length - 1].X + 0.85 * (EndPointTopArr[TopRFTLines.Length - 1].X - StartPointTopArr[TopRFTLines.Length - 1].X), StartPointTopArr[TopRFTLines.Length - 1].Y + 0.07 * scale),
                new Point(EndPointTopArr[TopRFTLines.Length - 1].X, EndPointTopArr[TopRFTLines.Length - 1].Y + 0.07 * scale));
            //add to the CANVAS 
            /*Code here*/
        }

        #endregion

        #region Stirrups
        public static void LeftSecStirrups(GCanvas gCanvas, double[] thickness, double scale)
        {
            double spacingLeft = 0;
            for (int i = 0; i < StirrupsLeftSec.GetLength(0); i++)
            {
                spacingLeft = 0;
                for (int j = 0; j < StirrupsLeftSec.GetLength(1); j++)
                {
                    StirrupsLeftSec[i, j] = new GLine(gCanvas, new Point(StartPointTopArr[i].X + 0.05 * scale + (spacingLeft / 1000) * scale, StartPointTopArr[i].Y + 0.07 * scale)
                        , new Point(StartPointTopArr[i].X + 0.05 * scale + (spacingLeft / 1000) * scale, StartPointTopArr[i].Y + thickness[i] * scale - 0.07 * scale));
                    spacingLeft += 150;

                    //add to the CANVAS 
                    /*Code here*/
                }
            }
        }

        public static void RightSecStirrups(GCanvas gCanvas, double[] thickness, double scale)
        {
            double spacingRight = 0;
            for (int i = 0; i < StirrupsRightSec.GetLength(0); i++)
            {
                spacingRight = 0;
                for (int j = 0; j < StirrupsRightSec.GetLength(1); j++)
                {
                    StirrupsRightSec[i, j] = new GLine(gCanvas, new Point(EndPointTopArr[i].X - (0.05 + spacingRight / 1000) * scale, EndPointTopArr[i].Y + 0.07 * scale)
                        , new Point(EndPointTopArr[i].X - (0.05 + spacingRight / 1000) * scale, EndPointTopArr[i].Y + thickness[i] * scale - 0.07 * thickness[i]));
                    spacingRight += 150;

                    //add to the CANVAS 
                    /*Code here*/
                }
            }
        }

        #endregion

        #endregion
    }
}
