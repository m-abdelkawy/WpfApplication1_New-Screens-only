using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP2000v20;


namespace ConsoleApplication1
{
    public enum LoadDir
    {
        local1 = 1,
        local2 = 2,
        local3 = 3,
        xDir = 4,
        yDir = 5,
        zDir = 6,
        projxDir = 7,
        projyDir = 8,
        projzDir = 9,
        gravityDir = 10,
        projGravityDir = 11,
    }
    public class SapFrameDistLoad
    {
        #region Member Variables
        private SapLoadPattern loadPattern;
        private int type; // 1 = force per unit length.
                          // 2 = Moment per unit length.
        private LoadDir dir;  //direction of the load.
        private double dist1; // distance from End-1 to the start of the distributed load.
        private double dist2; // distance from End-1 to the end of the distributed load.

        private double val1;  // value of the distributed load at the start.
        private double val2;  // value of the distributed load at the end.
        private bool relDist; //if true, the distances are relative, otherwise they are absolute.
                              //Default value = true.
        

        #endregion

        #region Properties
        internal SapLoadPattern LoadPattern
        {
            get { return loadPattern; }
            set { loadPattern = value; }
        }

        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        public LoadDir Dir
        {
            get { return dir; }
            set { dir = value; }
        }

        public double Dist1
        {
            get { return dist1; }
            set { dist1 = value; }
        }

        public double Dist2
        {
            get { return dist2; }
            set { dist2 = value; }
        }

        public double Val1
        {
            get { return val1; }
            set { val1 = value; }
        }

        public double Val2
        {
            get { return val2; }
            set { val2 = value; }
        }

        public bool RelDist
        {
            get { return relDist; }
            set { relDist = value; }
        }

        #endregion

        #region Constructors
        public SapFrameDistLoad(SapLoadPattern loadPattern, int type, LoadDir dir, double dist1, double dist2, double val1, double val2, bool relDist = true)
        {
            this.loadPattern = loadPattern;
            this.type = type;
            this.dir = dir;
            this.dist1 = dist1;
            this.dist2 = dist2;
            this.val1 = val1;
            this.val2 = val2;
            this.relDist = relDist;
        }
        #endregion
    }
}