using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design.Presentation.Model
{
    public class SteelMaterialProperties : MaterialProperty
    {
        public double Fy { get; set; }
        public double Fu { get; set; }
        public double Fye { get; set; }
        public double Fue { get; set; }
        public SteelMaterialProperties()
        {
            Fy = 55555;
            Fu = 55555;
            Fye = 555555;
            Fue = 5555;
        }
    }
}
