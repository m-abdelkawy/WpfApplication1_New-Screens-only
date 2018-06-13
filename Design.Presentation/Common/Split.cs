using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design.Presentation.Common
{
    class Split
    {
        public static List<double> Equal(double distance, double maxDistance)
        {
            List<double> res = new List<double>();
            var no = Math.Round(distance / maxDistance);
            var accmul = 0.0;
            var spacings = (distance / no);
            res.Add(accmul);
            for (int i = 0; i < no; i++)
            {
                accmul += spacings;
                res.Add(accmul);
            }
            return res;
        }
    }
}
