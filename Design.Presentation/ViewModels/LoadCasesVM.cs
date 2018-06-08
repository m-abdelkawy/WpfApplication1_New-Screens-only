using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP2000v20;

namespace Design.Presentation.ViewModels
{
    enum loadPatternType { Ahmed, Ali, Maha}

    class LoadCasesVM
    {
        public string LoadCaseName { get; set; } = "Dead";
        public double SelfWtMult { get; set; } = 0;
        public eLoadPatternType loadPatternType
        {
            get { return loadPatternType; }
            set { loadPatternType = value; }
        }


        public LoadCasesVM()
        {

        }
    }
}
