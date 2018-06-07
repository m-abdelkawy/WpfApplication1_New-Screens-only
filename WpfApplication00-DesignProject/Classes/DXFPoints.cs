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
    internal class DXFPoints
    {
        #region Member Variables
        //double x;
        //double y;
        //double z;

        

        #endregion

        #region Properties
        //public double X
        //{
        //    get
        //    {
        //        return x;
        //    }

        //    set
        //    {
        //        x = value;
        //    }
        //}

        //public double Y
        //{
        //    get
        //    {
        //        return y;
        //    }

        //    set
        //    {
        //        y = value;
        //    }
        //}

        //public double Z
        //{
        //    get
        //    {
        //        return z;
        //    }

        //    set
        //    {
        //        z = value;
        //    }
        //}
        #endregion

        #region Constructors
        //public DXFPoints(double x, double y, double z)
        //{
        //    this.x = x;
        //    this.y = y;
        //    this.z = z;
            
        //}
        #endregion

        #region Methods
        public static Point2D[] BottomStartPoints(int nSpans, double[] comSpanVals)
        {
            Point2D[] startPointsBot = new Point2D[nSpans];
            for (int i = 0; i < nSpans; i++)
            {
                startPointsBot[i] = new Point2D(comSpanVals[i] + 0.15, 0);
            }
            return startPointsBot;
        }
        public static Point2D[] BottomEndPoints(int nSpans, double[] comSpanVals)
        {
            Point2D[] endPointsBot = new Point2D[nSpans];
            for (int i = 0; i < nSpans; i++)
            {
                endPointsBot[i] = new Point2D(comSpanVals[i + 1] - 0.15, 0);
            }
            return endPointsBot;
        }



        public static Point2D[] TopStartPoints(int nSpans, double thickness, double[] comSpanVals)
        {
            Point2D[] startPointsTop = new Point2D[nSpans];
            for (int i = 0; i < nSpans; i++)
            {
                startPointsTop[i] = new Point2D(comSpanVals[i] + 0.15, thickness);
            }
            return startPointsTop;
        }
        public static Point2D[] TopEndPoints(int nSpans, double thickness, double[] comSpanVals)
        {
            Point2D[] endPointsTop = new Point2D[nSpans];
            for (int i = 0; i < nSpans; i++)
            {
                endPointsTop[i] = new Point2D(comSpanVals[i + 1] - 0.15, thickness);
            }
            return endPointsTop;
        }


        #endregion
    }
}
