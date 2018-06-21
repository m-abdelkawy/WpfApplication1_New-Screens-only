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
    public class DXFLines
    {

        #region Methods
        public static void ConstructBottomLines(DxfModel model ,int nSpans)
        {
            DxfLine[] linesBot = new DxfLine[nSpans];
            for (int i = 0; i < nSpans; i++)
            {
                linesBot[i] = new DxfLine(DXFPoints.startPointsBot[i], DXFPoints.endPointsBot[i]);
                model.Entities.Add(linesBot[i]);
            }
        }

        public static void ConstructTopLines(DxfModel model, int nSpans)
        {
            DxfLine[] linesTop = new DxfLine[nSpans];
            for (int i = 0; i < nSpans; i++)
            {
                linesTop[i] = new DxfLine(DXFPoints.startPointsTop[i], DXFPoints.endPointsTop[i]);
                model.Entities.Add(linesTop[i]);
            }
        }

        public static void ConstructColLines(DxfModel model, int nSpans)
        {
            //bottom start
            DxfLine[] colLinesBotStart = new DxfLine[nSpans];

            //Case Of Cantilever at start
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[0].SelectedRestraint != Restraints.NoRestraints)
            {
                colLinesBotStart[0] = new DxfLine(DXFPoints.startPointsBot[0]
                    , new Point2D(DXFPoints.startPointsBot[0].X, DXFPoints.startPointsBot[0].Y - 0.40));
                model.Entities.Add(colLinesBotStart[0]);
            }
            else
            {
                DxfLine comLineBotStart = new DxfLine(DXFPoints.startPointsBot[0]
                    , new Point2D(DXFPoints.startPointsBot[0].X - 0.15/*0.30 previously*/, DXFPoints.startPointsBot[0].Y));
                model.Entities.Add(comLineBotStart);
            }

            for (int i = 1; i < colLinesBotStart.Length; i++)
            {
                colLinesBotStart[i] = new DxfLine(DXFPoints.startPointsBot[i]
                    , new Point2D(DXFPoints.startPointsBot[i].X, DXFPoints.startPointsBot[i].Y - 0.40));
                model.Entities.Add(colLinesBotStart[i]);
            }
            /*-------------------------------------------*/
            //bottom end
            DxfLine[] colLinesBotEnd = new DxfLine[nSpans];
            
            for (int i = 0; i < colLinesBotEnd.Length - 1; i++)
            {
                colLinesBotEnd[i] = new DxfLine(DXFPoints.endPointsBot[i]
                    , new Point2D(DXFPoints.endPointsBot[i].X, DXFPoints.endPointsBot[i].Y - 0.40));
                model.Entities.Add(colLinesBotEnd[i]);
            }

            //Case Of Cantilever at End
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[RFTCanvas.SpanVals.Length].SelectedRestraint != Restraints.NoRestraints)
            {
                colLinesBotEnd[colLinesBotEnd.Length - 1] = new DxfLine(DXFPoints.endPointsBot[colLinesBotEnd.Length - 1]
                    , new Point2D(DXFPoints.endPointsBot[colLinesBotEnd.Length - 1].X, DXFPoints.endPointsBot[colLinesBotEnd.Length - 1].Y - 0.40));
                model.Entities.Add(colLinesBotEnd[colLinesBotEnd.Length - 1]);

            }
            else
            {
                DxfLine comLineBotEnd = new DxfLine(DXFPoints.endPointsBot[colLinesBotEnd.Length - 1]
                    , new Point2D(DXFPoints.endPointsBot[colLinesBotEnd.Length - 1].X + 0.15/*0.30 previously*/, DXFPoints.endPointsBot[colLinesBotEnd.Length - 1].Y));
                model.Entities.Add(comLineBotEnd);

            }

            /*----------------------------------------*/
            //Top Start
            DxfLine[] colLinesTopStart = new DxfLine[nSpans];
            //Case Of Cantilever at start
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[0].SelectedRestraint != Restraints.NoRestraints)
            {
                colLinesTopStart[0] = new DxfLine(DXFPoints.startPointsTop[0]
                    , new Point2D(DXFPoints.startPointsTop[0].X, DXFPoints.startPointsTop[0].Y + 0.40));
                model.Entities.Add(colLinesTopStart[0]);
            }
            else
            {
                DxfLine comLineTopStart = new DxfLine(DXFPoints.startPointsTop[0]
                    , new Point2D(DXFPoints.startPointsTop[0].X - 0.15/*0.30 previously*/, DXFPoints.startPointsTop[0].Y));
                model.Entities.Add(comLineTopStart);
            }

            for (int i = 1; i < colLinesTopStart.Length; i++)
            {
                colLinesTopStart[i] = new DxfLine(DXFPoints.startPointsTop[i]
                    , new Point2D(DXFPoints.startPointsTop[i].X, DXFPoints.startPointsTop[i].Y + 0.40));
                model.Entities.Add(colLinesTopStart[i]);
            }
            /*---------------------------------------*/
            //Top End
            DxfLine[] colLinesTopEnd = new DxfLine[nSpans];
            for (int i = 0; i < colLinesTopEnd.Length - 1; i++)
            {
                colLinesTopEnd[i] = new DxfLine(DXFPoints.endPointsTop[i]
                    , new Point2D(DXFPoints.endPointsTop[i].X, DXFPoints.endPointsTop[i].Y + 0.40));
                model.Entities.Add(colLinesTopEnd[i]);
            }

            //Case Of Cantilever at End
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[RFTCanvas.SpanVals.Length].SelectedRestraint != Restraints.NoRestraints)
            {
                colLinesTopEnd[colLinesTopEnd.Length - 1] = new DxfLine(DXFPoints.endPointsTop[colLinesTopEnd.Length - 1]
                    , new Point2D(DXFPoints.endPointsTop[colLinesTopEnd.Length - 1].X, DXFPoints.endPointsTop[colLinesTopEnd.Length - 1].Y + 0.40));
                model.Entities.Add(colLinesTopEnd[colLinesTopEnd.Length - 1]);
            }
            else
            {
                DxfLine comLineTopEnd = new DxfLine(DXFPoints.endPointsTop[colLinesTopEnd.Length - 1]
                    , new Point2D(DXFPoints.endPointsTop[colLinesTopEnd.Length - 1].X + 0.15/*0.30 Previously*/, DXFPoints.endPointsTop[colLinesTopEnd.Length - 1].Y));
                model.Entities.Add(comLineTopEnd);
            }
            /*----------------------------------------------*/
            //Start and end col Lines
            //Start col line
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[0].SelectedRestraint != Restraints.NoRestraints)
            {
                DxfLine StartLine = new DxfLine(new Point2D(DXFPoints.startPointsBot[0].X - 0.30, DXFPoints.startPointsBot[0].Y - 0.40)
                , new Point2D(DXFPoints.startPointsTop[0].X - 0.30, DXFPoints.startPointsTop[0].Y + 0.40));
                model.Entities.Add(StartLine);
            }
            else
            {
                DxfLine StartLine = new DxfLine(new Point2D(DXFPoints.startPointsBot[0].X - 0.15/*0.30 Previously*/, DXFPoints.startPointsBot[0].Y)
                , new Point2D(DXFPoints.startPointsTop[0].X - 0.15/*0.30 Previously*/, DXFPoints.startPointsTop[0].Y));
                model.Entities.Add(StartLine);
            }

            //End col Line
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[RFTCanvas.SpanVals.Length].SelectedRestraint != Restraints.NoRestraints)
            {
                DxfLine EndLine = new DxfLine(new Point2D(DXFPoints.endPointsBot[nSpans - 1].X + 0.30, DXFPoints.endPointsBot[nSpans - 1].Y - 0.40)
                    , new Point2D(DXFPoints.endPointsTop[nSpans - 1].X + 0.30, DXFPoints.endPointsTop[nSpans - 1].Y + 0.40));
                model.Entities.Add(EndLine);
            }
            else
            {
                DxfLine EndLine = new DxfLine(new Point2D(DXFPoints.endPointsBot[nSpans - 1].X + 0.15/*0.30 Previously*/, DXFPoints.endPointsBot[nSpans - 1].Y)
                    , new Point2D(DXFPoints.endPointsTop[nSpans - 1].X + 0.15/*0.30 Previously*/, DXFPoints.endPointsTop[nSpans - 1].Y));
                model.Entities.Add(EndLine);
            }
            
        }

        public static void ConstructGrids(DxfModel model, int nSpans, double[] comSpanVals)
        {
            DxfLine[] grids = new DxfLine[nSpans + 1];

            //Case of Cantilever at start
            //if (GeometryEditorVM.GeometryEditor.RestraintsCollection[0].SelectedRestraint != Restraints.NoRestraints)
            //{
            //    grids[0] = new DxfLine(EntityColors.Red, new Point2D(comSpanVals[0], DXFPoints.startPointsTop[0].Y - 2)
            //        , new Point2D(comSpanVals[0], DXFPoints.startPointsTop[0].Y + 1));
            //    model.Entities.Add(grids[0]);
            //}

            for (int i = 0; i < grids.Length; i++)
            {
                grids[i] = new DxfLine(EntityColors.Red, new Point2D(comSpanVals[i], DXFPoints.startPointsTop[0].Y - 2)
                    , new Point2D(comSpanVals[i], DXFPoints.startPointsTop[0].Y + 1));
                model.Entities.Add(grids[i]);
            }

            //Case of Cantilever at End
            //if (GeometryEditorVM.GeometryEditor.RestraintsCollection[RFTCanvas.SpanVals.Length].SelectedRestraint != Restraints.NoRestraints)
            //{
            //    grids[grids.Length - 1] = new DxfLine(EntityColors.Red, new Point2D(comSpanVals[grids.Length - 1], DXFPoints.startPointsTop[0].Y - 2)
            //        , new Point2D(comSpanVals[grids.Length - 1], DXFPoints.startPointsTop[0].Y + 1));
            //    model.Entities.Add(grids[grids.Length - 1]);
            //}
        }
        #endregion
    }
}
