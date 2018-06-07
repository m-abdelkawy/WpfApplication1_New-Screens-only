using SAP2000v20;

namespace Desing.Core.Sap
{
     public class SapPoint
    {
        #region Memeber Variables

        private string pointName;
        private double x;
        private double y;
        private double z;

        private cSapModel mySapObjectModel;
        private SapJointRestraints jointRestraint;
        #endregion
        
        #region Properties
        public string PointName
        {
            get { return pointName; }
            set { pointName = value; }
        }

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public double Z
        {
            get { return z; }
            set { z = value; }
        }

        internal SapJointRestraints JointRestraint
        {
            get { return jointRestraint; }
            set { jointRestraint = value; }
        }

        public cSapModel MySapObjectModel
        {
            get { return mySapObjectModel; }
            set { mySapObjectModel = value; }
        }

        #endregion

        #region Constructors
        public SapPoint(cSapModel _mySapObjectModel, double x, double y, double z)
        {
            this.mySapObjectModel = _mySapObjectModel;

            this.x = x;
            this.y = y;
            this.z = z;

            //initialize point with no restraints
            jointRestraint = new SapJointRestraints(Restraints.NoRestraints);

            mySapObjectModel.PointObj.AddCartesian(x, y, z, ref pointName);
        }
        public SapPoint(cSapModel _mySapObjectModel) : this(_mySapObjectModel, 0, 0, 0) { }
        #endregion

        #region Methods
        public void SetRestraints(Restraints restriantType)
        {
            jointRestraint = new SapJointRestraints(restriantType);
            bool[] tempRestraints = jointRestraint.Restraints;
            mySapObjectModel.PointObj.SetRestraint(this.pointName, ref tempRestraints, eItemType.Objects);
        }
        public void DeleteRestraints()
        {
            mySapObjectModel.PointObj.DeleteRestraint(this.pointName);
        }

        #endregion
    }
}