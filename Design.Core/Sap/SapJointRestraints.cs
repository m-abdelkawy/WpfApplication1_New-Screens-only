using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desing.Core.Sap
{
    public enum Restraints
    {
       [Description("Fixed")] 
        Fixed,
        [Description("Hinged")]
        Hinged,
        [Description("Roller")]
        Roller,
        [Description("NoRestraints")]
        NoRestraints

    }
    public class SapJointRestraints
    {
        #region Member Variables

        private bool[] restraints;
        
        #endregion

        #region Properties

        public bool[] Restraints
        {
            get { return restraints; }
            set { restraints = value; }
        }

        #endregion

        #region Constructors

        public SapJointRestraints(Restraints restraintType)
        {
            this.restraints = new bool[6];
            switch (restraintType)
            {
                case Desing.Core.Sap.Restraints.Fixed:
                    for (int i = 0; i < restraints.Length; i++)
                    {
                        restraints[i] = true;
                    }
                    break;

                case Desing.Core.Sap.Restraints.Hinged:
                    for (int i = 0; i < 3; i++)
                    {
                        restraints[i] = true;
                    }
                    break;

                case Desing.Core.Sap.Restraints.Roller:
                    restraints[2] = true;
                    break;

                case Desing.Core.Sap.Restraints.NoRestraints:
                    for (int i = 0; i < restraints.Length; i++)
                    {
                        restraints[i] = false;
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Methods

        public void SetRestraints(bool u1, bool u2, bool u3, bool r1, bool r2, bool r3)
        {
            restraints[0] = u1;
            restraints[1] = u2;
            restraints[2] = u3;
            restraints[3] = r1;
            restraints[4] = r2;
            restraints[5] = r3;

        }

        #endregion
    }
}
