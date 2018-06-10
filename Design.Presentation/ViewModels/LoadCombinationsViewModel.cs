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
    public class LoadCombinationsViewModel : INotifyPropertyChanged
    {
        private LoadCombinationsModel loadComboModel;

        #region Properties
        public LoadCombinationsModel LoadComboModel
        {
            get { return loadComboModel; }
            set { loadComboModel = value; }
        }
        public static ObservableCollection<LoadCombinationsModel> LoadComboModelCollection{ get; set; }
        #endregion

        #region Constructor
        public LoadCombinationsViewModel()
        {
            this.loadComboModel = new LoadCombinationsModel();
            LoadComboModelCollection = new ObservableCollection<LoadCombinationsModel>() { };
        }
        #endregion

        #region Property Wrapping

        public int Id
        {
            get { return this.loadComboModel.Id; }
            set { this.loadComboModel.Id = value; OnPropertyChanged("Id"); }
        }

        public string LoadComboName
        {
            get { return this.loadComboModel.LoadComboName; }
            set { this.loadComboModel.LoadComboName = value; OnPropertyChanged("LoadComboName"); }
        }

        public string LoadCaseName
        {
            get { return this.loadComboModel.LoadCaseName; }
            set { this.loadComboModel.LoadCaseName = value; OnPropertyChanged("LoadCaseName"); }
        }

        public double LoadFactor
        {
            get { return this.loadComboModel.LoadFactor; }
            set { this.loadComboModel.LoadFactor = value; OnPropertyChanged("LaodFactor"); }
        }
        

        #endregion

        #region PropertyChange Events
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
