using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Design.Presentation.Model
{
    
    public class LoadComboGridData:INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string LoadComboName { get; set; }
        public string LoadCaseName { get; set; }

        //public ObservableCollection<string> loadPatName2
        //{
        //    get { return LoadCases.loadPatName; }
        //    private set { LoadCases.loadPatName = value; }
        //}
        
        

        public double LoadFactor { get; set; }

        //Events
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
