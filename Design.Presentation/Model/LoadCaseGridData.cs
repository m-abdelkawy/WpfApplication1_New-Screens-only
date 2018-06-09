using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP2000v20;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Design.Presentation.Model
{
    public class LoadCaseGridData: INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string LoadcaseName { get; set; }
        public double SelfWtMult { get; set; } = 0;
        public eLoadPatternType loadPatternType { get; set; }

        public static IEnumerable<LoadCaseGridData> GetloadCases()
        {
            return new List<LoadCaseGridData>();
        }


        //Events
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
