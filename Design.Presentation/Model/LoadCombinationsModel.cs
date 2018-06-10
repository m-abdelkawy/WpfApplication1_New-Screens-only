using Design.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Design.Presentation.Model
{
    public class LoadCombinationsModel: INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string LoadComboName { get; set; }
        public ObservableCollection<LoadCasesModel> LoadCases { get; set; }
        public double LoadFactor { get; set; }
        public string loadCaseSelectedItem { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public LoadCombinationsModel()
        {
            LoadCases = LoadCasesViewModel.LoadCasesModelCollection;
        }
    }
}
