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

namespace Design.Core.Dxf
{
    public class DXFRebar
    {
        #region Member Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        public static DxfLine[] BotRFT(DxfModel model, int nSpans, Point2D[] startPointsBot, Point2D[] endPointsBot)
        {
            DxfLine[] BottomRFT = new DxfLine[nSpans];
            BottomRFT[0] = new DxfLine(EntityColors.Blue, new Point2D(startPointsBot[0].X - 0.25, 0.05), new Point2D(endPointsBot[0].X + 0.35, 0.05));
            model.Entities.Add(BottomRFT[0]);
            for (int i = 1; i < BottomRFT.Length - 1; i++)
            {
                if (i % 2 == 1)
                {
                    BottomRFT[i] = new DxfLine(EntityColors.Blue, new Point2D(startPointsBot[i].X - 0.35, 0.07), new Point2D(endPointsBot[i].X + 0.35, 0.07));
                }
                else
                {
                    BottomRFT[i] = new DxfLine(EntityColors.Blue, new Point2D(startPointsBot[i].X - 0.35, 0.05), new Point2D(endPointsBot[i].X + 0.35, 0.05));
                }
                model.Entities.Add(BottomRFT[i]);
            }
            if ((nSpans - 1) % 2 == 1)
            {
                BottomRFT[nSpans - 1] = new DxfLine(EntityColors.Blue, new Point2D(startPointsBot[nSpans - 1].X - 0.35, 0.07), new Point2D(endPointsBot[nSpans - 1].X + 0.25, 0.07));
            }
            else if ((nSpans - 1) % 2 == 0)
            {
                BottomRFT[nSpans - 1] = new DxfLine(EntityColors.Blue, new Point2D(startPointsBot[nSpans - 1].X - 0.35, 0.05), new Point2D(endPointsBot[nSpans - 1].X + 0.25, 0.05));
            }
            model.Entities.Add(BottomRFT[nSpans - 1]);

            return BottomRFT;
        }

        public static double[] Lnet(int nSpans, Point2D[] startPointsTop, Point2D[] endPointsTop)
        {
            double[] Ln = new double[nSpans];
            for (int i = 0; i < Ln.Length; i++)
            {
                Ln[i] = endPointsTop[i].X - startPointsTop[i].X;
            }
            return Ln;
        }

        public static DxfLine[] TopSpanRFT(DxfModel model, int nSpans, double[] thickness, double[] Ln, Point2D[] startPointsTop, Point2D[] endPointsTop)
        {
            DxfLine[] TopSpan = new DxfLine[nSpans + 1];

            TopSpan[0] = new DxfLine(EntityColors.Blue, new Point2D(startPointsTop[0].X - 0.20, thickness[0] - 0.07),
                new Point2D(endPointsTop[0].X - Math.Max(0.33 * Ln[0], 0.33 * Ln[1]) + 0.25, thickness[0] - 0.07));
            model.Entities.Add(TopSpan[0]);

            for (int i = 1; i < TopSpan.Length - 2; i++)
            {
                TopSpan[i] = new DxfLine(EntityColors.Blue, new Point2D(startPointsTop[i].X + Math.Max(0.33 * Ln[i - 1], 0.33 * Ln[i]) - 0.25, thickness[i - 1] - 0.07)
                    , new Point2D(endPointsTop[i].X - Math.Max(0.33 * Ln[i], 0.33 * Ln[i + 1]) + 0.25, thickness[i] - 0.07));
                model.Entities.Add(TopSpan[i]);
            }
            TopSpan[nSpans] = new DxfLine(EntityColors.Blue, new Point2D(startPointsTop[nSpans - 1].X + 0.33 * Ln[nSpans - 1] - 0.25, thickness[nSpans - 1] - 0.07)
                , new Point2D(endPointsTop[nSpans - 1].X + 0.20, thickness[nSpans - 1] - 0.07));
            model.Entities.Add(TopSpan[nSpans]);

            return TopSpan;
        }

        public static DxfLine[] TopSupportRFT(DxfModel model, int nSpans, double[] thickness, double[] Ln, Point2D[] startPointsTop, Point2D[] endPointsTop)
        {
            DxfLine[] TopSupport = new DxfLine[nSpans - 1];
            for (int i = 0; i < TopSupport.Length; i++)
            {
                TopSupport[i] = new DxfLine(EntityColors.Blue, new Point2D(endPointsTop[i].X - Math.Max(0.33 * Ln[i], 0.33 * Ln[i + 1]) - 0.25, thickness[i] - 0.05)
                    , new Point2D(startPointsTop[i + 1].X + Math.Max(0.33 * Ln[i], 0.33 * Ln[i + 1]) + 0.25, thickness[i] - 0.05));
                model.Entities.Add(TopSupport[i]);
            }
            return TopSupport;
        }

        public static void Legs(DxfModel model, int nSpans, double[] thickness, DxfLine[] BottomRFT, DxfLine[] TopSpan)
        {
            //legs:
            DxfLine legBot1 = new DxfLine(EntityColors.Blue, new Point2D(BottomRFT[0].Start.X, BottomRFT[0].Start.Y)
                , new Point2D(BottomRFT[0].Start.X, thickness[0] - 0.15));
            model.Entities.Add(legBot1);

            DxfLine legBot2 = new DxfLine(EntityColors.Blue, new Point2D(BottomRFT[nSpans - 1].End.X, BottomRFT[nSpans - 1].End.Y)
                , new Point2D(BottomRFT[nSpans - 1].End.X, thickness[nSpans - 1] - 0.15));
            model.Entities.Add(legBot2);

            DxfLine legTop1 = new DxfLine(EntityColors.Blue, new Point2D(TopSpan[0].Start.X, TopSpan[0].Start.Y)
                , new Point2D(TopSpan[0].Start.X, TopSpan[0].Start.Y - thickness[0] + 0.15));
            model.Entities.Add(legTop1);

            DxfLine legTop2 = new DxfLine(EntityColors.Blue, new Point2D(TopSpan[nSpans].End.X, TopSpan[nSpans].End.Y)
                , new Point2D(TopSpan[nSpans].End.X, TopSpan[nSpans].End.Y - thickness[nSpans - 1] + 0.15));
            model.Entities.Add(legTop2);
        }

        /*-------------Stirrups-------------*/
        public static DxfLine[,] StirrupsLeftSec(DxfModel model, int nSpans, double thickness, Point2D[] startPointsBot)
        {
            double spacingLeft = 0;
            double sec1Spacing = 150;
            DxfLine[,] stirrupsLeft = new DxfLine[nSpans, 3];
            for (int i = 0; i < stirrupsLeft.GetLength(0); i++)
            {
                spacingLeft = 0;
                for (int j = 0; j < stirrupsLeft.GetLength(1); j++)
                {
                    stirrupsLeft[i, j] = new DxfLine(new Point2D(startPointsBot[i].X + 0.05 + (spacingLeft / 1000), 0.05)
                        , new Point2D(startPointsBot[i].X + 0.05 + (spacingLeft / 1000), thickness - 0.07));
                    model.Entities.Add(stirrupsLeft[i, j]);
                    spacingLeft += sec1Spacing;
                }
            }
            return stirrupsLeft;
        }

        public static DxfLine[,] StirrupsMidSpanSec(DxfModel model, int nSpans, double thickness, Point2D[] startPointsBot, Point2D[] endPointsBot)
        {
            double spacingMidSpan = 0;
            double sec2Spacing = 150;
            DxfLine[,] stirrupsMidspan = new DxfLine[nSpans, 3];
            for (int i = 0; i < stirrupsMidspan.GetLength(0); i++)
            {
                spacingMidSpan = 0;
                for (int j = 0; j < stirrupsMidspan.GetLength(1); j++)
                {
                    stirrupsMidspan[i, j] = new DxfLine(new Point2D(startPointsBot[i].X + 0.50 * (endPointsBot[i].X - startPointsBot[i].X) - 0.15 + (spacingMidSpan / 1000), 0.07)
                    , new Point2D(startPointsBot[i].X + 0.50 * (endPointsBot[i].X - startPointsBot[i].X) - 0.15 + (spacingMidSpan / 1000), thickness - 0.07));
                    model.Entities.Add(stirrupsMidspan[i, j]);
                    spacingMidSpan += sec2Spacing;
                }
            }
            return stirrupsMidspan;
        }

        public static DxfLine[,] StirrupsRightSec(DxfModel model, int nSpans, double thickness, Point2D[] endPointsBot)
        {
            double spacingRight = 0;
            double sec3Spacing = 150;
            DxfLine[,] stirrupsRight = new DxfLine[nSpans, 3];
            for (int i = 0; i < stirrupsRight.GetLength(0); i++)
            {
                spacingRight = 0;
                for (int j = 0; j < stirrupsRight.GetLength(1); j++)
                {
                    stirrupsRight[i, j] = new DxfLine(new Point2D(endPointsBot[i].X - 0.05 - (spacingRight / 1000), 0.05)
                    , new Point2D(endPointsBot[i].X - 0.05 - (spacingRight / 1000), thickness - 0.07));
                    model.Entities.Add(stirrupsRight[i, j]);
                    spacingRight += sec3Spacing;
                }
            }
            return stirrupsRight;
        }






        #endregion
    }
}
