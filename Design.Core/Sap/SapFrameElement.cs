using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP2000v20;

namespace Desing.Core.Sap
{
     public class SapFrameElement
    {
        #region Member Variables

        private SapPoint p1;
        private SapPoint p2;
        private cSapModel mySapObjectModel;
        private string label;
        private string name;
        private SapRectSection myRectSec;

        private List<SapFrameDistLoad> distLoads;
        private List<SapPointLoad> pointLoads;
        private List<SapFrameResult> frameResults;
        //private List<SapRectSection> sec;
        

        #endregion


        #region Properties
        internal SapPoint P1
        {
            get { return p1; }
            set { p1 = value; }
        }

        internal SapPoint P2
        {
            get { return p2; }
            set { p2 = value; }
        }

        public cSapModel MySapObjectModel
        {
            get { return mySapObjectModel; }
            set { mySapObjectModel = value; }
        }

        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        internal SapRectSection MyRectSec
        {
            get { return myRectSec; }
            set { myRectSec = value; }
        }

        public List<SapFrameDistLoad> DistLoads
        {
            get { return distLoads; }
            set { distLoads = value; }
        }

        internal List<SapPointLoad> PointLoads
        {
            get
            {
                return pointLoads;
            }

            set
            {
                pointLoads = value;
            }
        }

        public List<SapFrameResult> FrameResults
        {
            get
            {
                return frameResults;
            }

            set
            {
                frameResults = value;
            }
        }

        //internal List<SapRectSection> Sec
        //{
        //    get
        //    {
        //        return sec;
        //    }

        //    set
        //    {
        //        sec = value;
        //    }
        //}
        #endregion

        #region Constructors

        public SapFrameElement(cSapModel mySapObjectModel, SapPoint p1, SapPoint p2, SapRectSection myRectSec, string label, string name)
        {
            this.mySapObjectModel = mySapObjectModel;
            this.p1 = p1;
            this.p2 = p2;
            this.myRectSec = myRectSec;
            this.label = label;
            this.name = name;
            this.distLoads = new List<SapFrameDistLoad>();
            this.pointLoads = new List<SapPointLoad>();
            this.frameResults = new List<SapFrameResult>();
            //this.sec = new List<SapRectSection>();
            mySapObjectModel.FrameObj.AddByPoint(this.p1.PointName, this.p2.PointName, ref this.name, this.name);
        }

        #endregion

        #region Methods

        public void SetDistributedLoad(SapFrameDistLoad _distLoad)
        {
            this.distLoads.Add(_distLoad);
            int check1 = this.mySapObjectModel.FrameObj.SetLoadDistributed(this.name, _distLoad.LoadPattern.Name, _distLoad.Type
                , (int)_distLoad.Dir, _distLoad.Dist1, _distLoad.Dist2, _distLoad.Val1, _distLoad.Val2, "Global",  _distLoad.RelDist);
        }

        public void SetFramePointLoad(SapPointLoad pointLoad, double dist)
        {
            this.pointLoads.Add(pointLoad);
            int check2 = this.mySapObjectModel.FrameObj.SetLoadPoint(this.name, pointLoad.LoadPattern.Name, 
                (int)pointLoad.LoadType, (int)pointLoad.Dir, dist, pointLoad.Value);
        }
        #endregion
    }
}