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

namespace WpfApplication00_DesignProject.Classes
{
    class DXFLines
    {
        #region Member Variables

        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        public static void ConstructBottomLines(DxfModel model ,int nSpans, Point2D[] startPointsBot, Point2D[] endPointsBot)
        {
            DxfLine[] linesBot = new DxfLine[nSpans];
            for (int i = 0; i < nSpans; i++)
            {
                linesBot[i] = new DxfLine(startPointsBot[i], endPointsBot[i]);
                model.Entities.Add(linesBot[i]);
            }
        }

        public static void ConstructTopLines(DxfModel model, int nSpans, Point2D[] startPointsTop, Point2D[] endPointsTop)
        {
            DxfLine[] linesTop = new DxfLine[nSpans];
            for (int i = 0; i < nSpans; i++)
            {
                linesTop[i] = new DxfLine(startPointsTop[i], endPointsTop[i]);
                model.Entities.Add(linesTop[i]);
            }
        }

        public static void ConstructColLines(DxfModel model, int nSpans, double thickness, Point2D[] startPointsBot,
            Point2D[] endPointsBot, Point2D[] startPointsTop, Point2D[] endPointsTop)
        {
            //bottom start
            DxfLine[] colLinesBotStart = new DxfLine[nSpans];
            for (int i = 0; i < colLinesBotStart.Length; i++)
            {
                colLinesBotStart[i] = new DxfLine(startPointsBot[i], new Point2D(startPointsBot[i].X, -0.40));
                model.Entities.Add(colLinesBotStart[i]);
            }
            //bottom end
            DxfLine[] colLinesBotEnd = new DxfLine[nSpans];
            for (int i = 0; i < colLinesBotEnd.Length; i++)
            {
                colLinesBotEnd[i] = new DxfLine(endPointsBot[i], new Point2D(endPointsBot[i].X, -0.40));
                model.Entities.Add(colLinesBotEnd[i]);
            }
            //Top Start
            DxfLine[] colLinesTopStart = new DxfLine[nSpans];
            for (int i = 0; i < colLinesTopStart.Length; i++)
            {
                colLinesTopStart[i] = new DxfLine(startPointsTop[i], new Point2D(startPointsTop[i].X, thickness + 0.40));
                model.Entities.Add(colLinesTopStart[i]);
            }
            //Top End
            DxfLine[] colLinesTopEnd = new DxfLine[nSpans];
            for (int i = 0; i < colLinesTopEnd.Length; i++)
            {
                colLinesTopEnd[i] = new DxfLine(endPointsTop[i], new Point2D(endPointsTop[i].X, thickness + 0.40));
                model.Entities.Add(colLinesTopEnd[i]);
            }
            //Start and end col Lines
            DxfLine StartLine = new DxfLine(new Point2D(startPointsBot[0].X - 0.30, -0.40), new Point2D(startPointsTop[0].X - 0.30, thickness + 0.40));
            model.Entities.Add(StartLine);
            DxfLine EndLine = new DxfLine(new Point2D(endPointsBot[nSpans - 1].X + 0.30, -0.40), new Point2D(endPointsTop[nSpans - 1].X + 0.30, thickness + 0.40));
            model.Entities.Add(EndLine);
        }

        public static void ConstructGrids(DxfModel model, int nSpans, double thickness, double[] comSpanVals)
        {
            DxfLine[] grids = new DxfLine[nSpans + 1];
            for (int i = 0; i < grids.Length; i++)
            {
                grids[i] = new DxfLine(EntityColors.Red, new Point2D(comSpanVals[i], -1), new Point2D(comSpanVals[i], thickness + 1));
                model.Entities.Add(grids[i]);
            }
        }
        #endregion
    }
}
