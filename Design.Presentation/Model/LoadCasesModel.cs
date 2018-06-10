using SAP2000v20;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Design.Presentation.Model
{
    public class LoadCasesModel:INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string LoadcaseName { get; set; }
        public double SelfWtMult { get; set; } = 0;
        public eLoadPatternType loadPatternType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
