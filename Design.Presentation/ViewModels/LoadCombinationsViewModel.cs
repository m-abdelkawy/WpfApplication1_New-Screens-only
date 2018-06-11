using Design.Presentation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Design.Presentation.ViewModels
{
    public class LoadCombinationsViewModel :INotifyPropertyChanged
    {
        
        public string SelectedLoadCase
        {
            get;
            set;
        }


        public static ObservableCollection<LoadCombinationsModel> LoadComboModelCollection { get; set; }
       = new ObservableCollection<LoadCombinationsModel>();

        public ObservableCollection<LoadCombinationsModel> LoadComboCollection { get; set; }
        #region Constructor
        public LoadCombinationsViewModel()
        {
            LoadComboCollection = LoadCombinationsViewModel.LoadComboModelCollection;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion





    }
}
