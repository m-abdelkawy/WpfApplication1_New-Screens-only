using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design.Presentation.Model
{
   public class ConcreteMaterialProperties:MaterialProperty
    {
        public double Fc { get; set; }
        public double CompressiveStrength { get; set; }
        public bool IsLightWeight { get; set; }
        public ConcreteMaterialProperties()
        {
            Fc = 100000;
            CompressiveStrength = 10000;
        }

    }
}
