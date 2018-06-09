using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP2000v20;
using System.Collections.ObjectModel;
using Design.Presentation.Model;

namespace Design.Presentation.ViewModels
{
    //enum loadPatternType { Ahmed, Ali, Maha}

    class LoadCasesVM
    {
        public string LoadCaseName { get; set; }
        public double SelfWtMult { get; set; } = 0;
        public eLoadPatternType loadPatternType
        {
            get { return loadPatternType; }
            set { loadPatternType = value; }
        }

        public ObservableCollection<LoadCaseGridData> loadCaseGridData { get; set; }
        public ObservableCollection<LoadComboGridData> loadComboGridData { get; set; }


        public LoadCasesVM()
        {
            loadCaseGridData = new ObservableCollection<LoadCaseGridData>()
            {
                new LoadCaseGridData {Id=1,LoadcaseName = "Dead", SelfWtMult = 1, loadPatternType = eLoadPatternType.Dead},
                new LoadCaseGridData {Id=2,LoadcaseName = "Dead", SelfWtMult = 1, loadPatternType = eLoadPatternType.Dead},
                new LoadCaseGridData {Id=3,LoadcaseName = "Dead", SelfWtMult = 1, loadPatternType = eLoadPatternType.Dead},
            };
            loadComboGridData = new ObservableCollection<LoadComboGridData>()
            {
                new LoadComboGridData {Id=1,LoadCaseName = "Dead", LoadComboName = "Dead", LoadFactor =1},
                new LoadComboGridData {Id=2,LoadCaseName = "Dead", LoadComboName = "Dead", LoadFactor =1},
                new LoadComboGridData {Id=3,LoadCaseName = "Dead", LoadComboName = "Dead", LoadFactor =1},
            };
        }
        
    }
}
