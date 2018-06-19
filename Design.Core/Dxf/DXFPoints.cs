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
     public class DXFPoints
    {
        #region Static DataMembers
        public static Point2D[] startPointsTop;
        public static Point2D[] endPointsTop;

        public static Point2D[] startPointsBot;
        public static Point2D[] endPointsBot;


        #endregion

        #region Methods

        #region Top Points
        public static void TopStartPoints(int nSpans, double[] comSpanVals)
        {
            startPointsTop = new Point2D[nSpans];
            for (int i = 0; i < nSpans; i++)
            {
                startPointsTop[i] = new Point2D(comSpanVals[i] + 0.15, 0);
            }
        }
        public static void TopEndPoints(int nSpans, double[] comSpanVals)
        {
            endPointsTop = new Point2D[nSpans];
            for (int i = 0; i < nSpans; i++)
            {
                endPointsTop[i] = new Point2D(comSpanVals[i + 1] - 0.15, 0);
            }
        }
        #endregion

        #region Bottom Points
        public static void BottomStartPoints(int nSpans, double[] thickness, double[] comSpanVals)
        {
            startPointsBot = new Point2D[nSpans];
            for (int i = 0; i < nSpans; i++)
            {
                startPointsBot[i] = new Point2D(comSpanVals[i] + 0.15, startPointsTop[i].Y - thickness[i]);
            }
        }
        public static void BottomEndPoints(int nSpans, double[] thickness, double[] comSpanVals)
        {
            endPointsBot = new Point2D[nSpans];
            for (int i = 0; i < nSpans; i++)
            {
                endPointsBot[i] = new Point2D(comSpanVals[i + 1] - 0.15, endPointsTop[i].Y - thickness[i]);
            }
        }
        #endregion

        #endregion
    }
}
