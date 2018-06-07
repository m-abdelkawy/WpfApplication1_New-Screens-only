using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    enum Type
    {
        Force = 1,
        Moment = 2
    }
    class SapPointLoad
    {
        #region Member Variables

        SapLoadPattern loadPattern;
        double[] values;
        double value;
        Type loadType;
        LoadDir dir;

        
        #endregion

        #region Properties
        public SapLoadPattern LoadPattern
        {
            get { return loadPattern; }
            set { loadPattern = value; }
        }

        public double[] Values
        {
            get { return values; }
            set { values = value; }
        }

        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }

        internal Type LoadType
        {
            get
            {
                return loadType;
            }

            set
            {
                loadType = value;
            }
        }

        public LoadDir Dir
        {
            get
            {
                return dir;
            }

            set
            {
                dir = value;
            }
        }

        #endregion
        #region Constructors
        public SapPointLoad(SapLoadPattern loadPattern, double val, Type loadType, LoadDir _dir)
        {
            this.loadPattern = loadPattern;
            this.value = val;
            this.loadType = loadType;
            this.dir = _dir;
        }
        #endregion

        #region Methods
        
        #endregion
    }
}
