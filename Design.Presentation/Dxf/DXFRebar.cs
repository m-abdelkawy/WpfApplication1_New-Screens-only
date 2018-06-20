using Design.Presentation.ViewModels;
using Desing.Core.Sap;
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
        #region Static Data Members
        public static DxfLine[] BottomRFT;
        public static DxfLine[] TopSpan;
        public static DxfLine[] TopSupport;
        public static double[] Ln;

        //Stirrups
        public static DxfLine[,] stirrupsLeft;
        public static DxfLine[,] stirrupsRight;



        #endregion

        #region Methods
        public static void BotRFT(DxfModel model, int nSpans)
        {
            BottomRFT = new DxfLine[nSpans];
            //Case of Cantilever at start
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[0].SelectedRestraint != Restraints.NoRestraints)
            {
                BottomRFT[0] = new DxfLine(EntityColors.Blue, new Point2D(DXFPoints.startPointsBot[0].X - 0.25, DXFPoints.startPointsBot[0].Y + 0.05)
                , new Point2D(DXFPoints.endPointsBot[0].X + 0.35, DXFPoints.startPointsBot[0].Y + 0.05));
                model.Entities.Add(BottomRFT[0]);
            }
            else
            {
                BottomRFT[0] = new DxfLine(EntityColors.Blue, new Point2D(DXFPoints.startPointsBot[0].X - 0.25, DXFPoints.startPointsBot[0].Y + 0.05)
                , new Point2D(DXFPoints.endPointsBot[0].X + 0.25/*notice this one*/, DXFPoints.startPointsBot[0].Y + 0.05));
                model.Entities.Add(BottomRFT[0]);
            }

            for (int i = 1; i < BottomRFT.Length - 1; i++)
            {
                if (i % 2 == 1)
                {
                    BottomRFT[i] = new DxfLine(EntityColors.Blue, new Point2D(DXFPoints.startPointsBot[i].X - 0.35, DXFPoints.startPointsBot[i].Y + 0.07)
                        , new Point2D(DXFPoints.endPointsBot[i].X + 0.35, DXFPoints.endPointsBot[i].Y + 0.07));
                }
                else
                {
                    BottomRFT[i] = new DxfLine(EntityColors.Blue, new Point2D(DXFPoints.startPointsBot[i].X - 0.35, DXFPoints.startPointsBot[i].Y + 0.05)
                        , new Point2D(DXFPoints.endPointsBot[i].X + 0.35, DXFPoints.endPointsBot[i].Y + 0.05));
                }
                model.Entities.Add(BottomRFT[i]);
            }


            //Case of Cantilever End
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[RFTCanvas.SpanVals.Length].SelectedRestraint != Restraints.NoRestraints)
            {
                if ((nSpans - 1) % 2 == 1)
                {
                    BottomRFT[nSpans - 1] = new DxfLine(EntityColors.Blue, new Point2D(DXFPoints.startPointsBot[nSpans - 1].X - 0.35, DXFPoints.startPointsBot[nSpans - 1].Y + 0.07)
                        , new Point2D(DXFPoints.endPointsBot[nSpans - 1].X + 0.25, DXFPoints.endPointsBot[nSpans - 1].Y + 0.07));
                }
                else if ((nSpans - 1) % 2 == 0)
                {
                    BottomRFT[nSpans - 1] = new DxfLine(EntityColors.Blue, new Point2D(DXFPoints.startPointsBot[nSpans - 1].X - 0.35, DXFPoints.startPointsBot[nSpans - 1].Y + 0.05)
                        , new Point2D(DXFPoints.endPointsBot[nSpans - 1].X + 0.25, DXFPoints.endPointsBot[nSpans - 1].Y + 0.05));
                }
                model.Entities.Add(BottomRFT[nSpans - 1]);
            }
            else
            {
                if ((nSpans - 1) % 2 == 1)
                {
                    BottomRFT[nSpans - 1] = new DxfLine(EntityColors.Blue, new Point2D(DXFPoints.startPointsBot[nSpans - 1].X - 0.25/*----*/, DXFPoints.startPointsBot[nSpans - 1].Y + 0.07)
                        , new Point2D(DXFPoints.endPointsBot[nSpans - 1].X + 0.25, DXFPoints.endPointsBot[nSpans - 1].Y + 0.07));
                }
                else if ((nSpans - 1) % 2 == 0)
                {
                    BottomRFT[nSpans - 1] = new DxfLine(EntityColors.Blue, new Point2D(DXFPoints.startPointsBot[nSpans - 1].X - 0.25/*----*/, DXFPoints.startPointsBot[nSpans - 1].Y + 0.05)
                        , new Point2D(DXFPoints.endPointsBot[nSpans - 1].X + 0.25, DXFPoints.endPointsBot[nSpans - 1].Y + 0.05));
                }
                model.Entities.Add(BottomRFT[nSpans - 1]);
            }
            
        }

        public static void Lnet(int nSpans)
        {
            Ln = new double[nSpans];
            for (int i = 0; i < Ln.Length; i++)
            {
                Ln[i] = DXFPoints.endPointsTop[i].X - DXFPoints.startPointsTop[i].X;
            }
        }

        public static void TopSpanRFT(DxfModel model, int nSpans)
        {
            TopSpan = new DxfLine[nSpans + 1];

            //Case Of Cantilever Start Span
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[0].SelectedRestraint != Restraints.NoRestraints)
            {
                TopSpan[0] = new DxfLine(EntityColors.Blue, new Point2D(DXFPoints.startPointsTop[0].X - 0.20, DXFPoints.startPointsTop[0].Y - 0.07),
                new Point2D(DXFPoints.endPointsTop[0].X - Math.Max(0.33 * Ln[0], 0.33 * Ln[1]) + 0.25, DXFPoints.endPointsTop[0].Y - 0.07));
                model.Entities.Add(TopSpan[0]);
            }
            else
            {
                TopSpan[0] = new DxfLine(EntityColors.Blue, new Point2D(DXFPoints.startPointsTop[0].X - 0.25/*------*/, DXFPoints.startPointsTop[0].Y - 0.05),
                new Point2D(DXFPoints.endPointsTop[0].X + 0.15 + 1.50 * Ln[0]/*-------*/, DXFPoints.endPointsTop[0].Y - 0.05));
                model.Entities.Add(TopSpan[0]);
            }

            

            for (int i = 1; i < TopSpan.Length - 2; i++)
            {
                TopSpan[i] = new DxfLine(EntityColors.Blue, new Point2D(DXFPoints.startPointsTop[i].X + Math.Max(0.33 * Ln[i - 1], 0.33 * Ln[i]) - 0.25, DXFPoints.startPointsTop[i].Y - 0.07)
                    , new Point2D(DXFPoints.endPointsTop[i].X - Math.Max(0.33 * Ln[i], 0.33 * Ln[i + 1]) + 0.25, DXFPoints.endPointsTop[i].Y - 0.07));
                model.Entities.Add(TopSpan[i]);
            }

            //Case of Cantilever end Span
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[RFTCanvas.SpanVals.Length].SelectedRestraint != Restraints.NoRestraints)
            {
                TopSpan[nSpans] = new DxfLine(EntityColors.Blue, new Point2D(DXFPoints.startPointsTop[nSpans - 1].X + 0.33 * Ln[nSpans - 1] - 0.25, DXFPoints.startPointsTop[nSpans - 1].Y - 0.07)
                , new Point2D(DXFPoints.endPointsTop[nSpans - 1].X + 0.20, DXFPoints.endPointsTop[nSpans - 1].Y - 0.07));
                model.Entities.Add(TopSpan[nSpans]);
            }
            else
            {
                TopSpan[nSpans] = new DxfLine(EntityColors.Blue, new Point2D(DXFPoints.startPointsTop[nSpans - 1].X - 0.15 - 1.50 * Ln[nSpans - 1]/*----*/, DXFPoints.startPointsTop[nSpans - 1].Y - 0.05)
                , new Point2D(DXFPoints.endPointsTop[nSpans - 1].X + 0.25/*----*/, DXFPoints.endPointsTop[nSpans - 1].Y - 0.05));
                model.Entities.Add(TopSpan[nSpans]);
            }
        }

        public static void TopSupportRFT(DxfModel model, int nSpans)
        {
            TopSupport = new DxfLine[nSpans - 1];

            //Case Of Cantilever Start Span
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[0].SelectedRestraint != Restraints.NoRestraints)
            {
                TopSupport[0] = new DxfLine(EntityColors.Blue, new Point2D(DXFPoints.endPointsTop[0].X - Math.Max(0.33 * Ln[0], 0.33 * Ln[1]) - 0.25, DXFPoints.endPointsTop[0].Y - 0.05)
                    , new Point2D(DXFPoints.startPointsTop[1].X + Math.Max(0.33 * Ln[0], 0.33 * Ln[1]) + 0.25, DXFPoints.startPointsTop[1].Y - 0.05));
                model.Entities.Add(TopSupport[0]);
            }

            for (int i = 1; i < TopSupport.Length - 1; i++)
            {
                TopSupport[i] = new DxfLine(EntityColors.Blue, new Point2D(DXFPoints.endPointsTop[i].X - Math.Max(0.33 * Ln[i], 0.33 * Ln[i + 1]) - 0.25, DXFPoints.endPointsTop[i].Y - 0.05)
                    , new Point2D(DXFPoints.startPointsTop[i + 1].X + Math.Max(0.33 * Ln[i], 0.33 * Ln[i + 1]) + 0.25, DXFPoints.startPointsTop[i + 1].Y - 0.05));
                model.Entities.Add(TopSupport[i]);
            }

            //Case of Cantilever end Span
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[RFTCanvas.SpanVals.Length].SelectedRestraint != Restraints.NoRestraints)
            {
                TopSupport[TopSupport.Length - 1] = new DxfLine(EntityColors.Blue, new Point2D(DXFPoints.endPointsTop[TopSupport.Length - 1].X - Math.Max(0.33 * Ln[TopSupport.Length - 1], 0.33 * Ln[TopSupport.Length - 1 + 1]) - 0.25, DXFPoints.endPointsTop[TopSupport.Length - 1].Y - 0.05)
                    , new Point2D(DXFPoints.startPointsTop[TopSupport.Length - 1 + 1].X + Math.Max(0.33 * Ln[TopSupport.Length - 1], 0.33 * Ln[TopSupport.Length - 1 + 1]) + 0.25, DXFPoints.startPointsTop[TopSupport.Length - 1 + 1].Y - 0.05));
                model.Entities.Add(TopSupport[TopSupport.Length - 1]);
            }
        }

        public static void Legs(DxfModel model, int nSpans)
        {
            //legs:
            DxfLine legBot1 = new DxfLine(EntityColors.Blue, new Point2D(BottomRFT[0].Start.X, BottomRFT[0].Start.Y)
                , new Point2D(BottomRFT[0].Start.X, DXFPoints.startPointsTop[0].Y - 0.15));
            model.Entities.Add(legBot1);

            DxfLine legBot2 = new DxfLine(EntityColors.Blue, new Point2D(BottomRFT[nSpans - 1].End.X, BottomRFT[nSpans - 1].End.Y)
                , new Point2D(BottomRFT[nSpans - 1].End.X, DXFPoints.startPointsTop[nSpans - 1].Y - 0.15));
            model.Entities.Add(legBot2);

            DxfLine legTop1 = new DxfLine(EntityColors.Blue, new Point2D(TopSpan[0].Start.X, TopSpan[0].Start.Y)
                , new Point2D(TopSpan[0].Start.X, /*TopSpan[0].Start.Y -*/ DXFPoints.startPointsBot[0].Y + 0.15));
            model.Entities.Add(legTop1);

            DxfLine legTop2 = new DxfLine(EntityColors.Blue, new Point2D(TopSpan[nSpans].End.X, TopSpan[nSpans].End.Y)
                , new Point2D(TopSpan[nSpans].End.X, /*TopSpan[nSpans].End.Y -*/ DXFPoints.startPointsBot[nSpans - 1].Y + 0.15));
            model.Entities.Add(legTop2);
        }

        /*-------------Stirrups-------------*/
        public static void StirrupsLeftSec(DxfModel model, int nSpans)
        {
            double spacingLeft = 0;
            double sec1Spacing = 150;
            stirrupsLeft = new DxfLine[nSpans, 3];
            for (int i = 0; i < stirrupsLeft.GetLength(0); i++)
            {
                spacingLeft = 0;
                for (int j = 0; j < stirrupsLeft.GetLength(1); j++)
                {
                    stirrupsLeft[i, j] = new DxfLine(new Point2D(DXFPoints.startPointsBot[i].X + 0.05 + (spacingLeft / 1000), DXFPoints.startPointsTop[i].Y/* - 0.05*/)
                        , new Point2D(DXFPoints.startPointsBot[i].X + 0.05 + (spacingLeft / 1000), DXFPoints.startPointsBot[i].Y/*--*/ /*+ 0.07*/));
                    model.Entities.Add(stirrupsLeft[i, j]);
                    spacingLeft += sec1Spacing;
                }
            }
        }

        //public static DxfLine[,] StirrupsMidSpanSec(DxfModel model, int nSpans, double thickness, Point2D[] startPointsBot, Point2D[] endPointsBot)
        //{
        //    double spacingMidSpan = 0;
        //    double sec2Spacing = 150;
        //    DxfLine[,] stirrupsMidspan = new DxfLine[nSpans, 3];
        //    for (int i = 0; i < stirrupsMidspan.GetLength(0); i++)
        //    {
        //        spacingMidSpan = 0;
        //        for (int j = 0; j < stirrupsMidspan.GetLength(1); j++)
        //        {
        //            stirrupsMidspan[i, j] = new DxfLine(new Point2D(startPointsBot[i].X + 0.50 * (endPointsBot[i].X - startPointsBot[i].X) - 0.15 + (spacingMidSpan / 1000), 0.07)
        //            , new Point2D(startPointsBot[i].X + 0.50 * (endPointsBot[i].X - startPointsBot[i].X) - 0.15 + (spacingMidSpan / 1000), thickness - 0.07));
        //            model.Entities.Add(stirrupsMidspan[i, j]);
        //            spacingMidSpan += sec2Spacing;
        //        }
        //    }
        //    return stirrupsMidspan;
        //}

        public static void StirrupsRightSec(DxfModel model, int nSpans)
        {
            double spacingRight = 0;
            double sec3Spacing = 150;
            stirrupsRight = new DxfLine[nSpans, 3];
            for (int i = 0; i < stirrupsRight.GetLength(0); i++)
            {
                spacingRight = 0;
                for (int j = 0; j < stirrupsRight.GetLength(1); j++)
                {
                    stirrupsRight[i, j] = new DxfLine(new Point2D(DXFPoints.endPointsBot[i].X - 0.05 - (spacingRight / 1000), DXFPoints.endPointsTop[i].Y /*- 0.05*/)
                    , new Point2D(DXFPoints.endPointsBot[i].X - 0.05 - (spacingRight / 1000), DXFPoints.endPointsBot[i].Y /*+ 0.07*/));
                    model.Entities.Add(stirrupsRight[i, j]);
                    spacingRight += sec3Spacing;
                }
            }
        }
        
        #endregion
    }
}
