using Design.Presentation.Model;
using SAP2000v20;
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
    public class LoadCasesViewModel: INotifyPropertyChanged
    {

        private LoadCasesModel loadCasesmodel;
        #region Properties
        public LoadCasesModel LoadCasesmodel
        {
            get { return loadCasesmodel; }
            set { loadCasesmodel = value; }
        }
        public static ObservableCollection<LoadCasesModel> LoadCasesModelCollection { get; set; }
        =new ObservableCollection<LoadCasesModel>();
        #endregion

        //Costructor
       
        public LoadCasesViewModel()
        {
            this.loadCasesmodel = new LoadCasesModel();
           
        }
        public LoadCasesViewModel(LoadCasesModel _loadCasesmodel)
        {
            this.loadCasesmodel = _loadCasesmodel;
            //LoadCasesModelCollection = new ObservableCollection<LoadCasesModel>() { };
        }

        #region Property Wrapping

        public int Id
        {
            get { return this.loadCasesmodel.Id; }
            set { this.loadCasesmodel.Id = value; OnPropertyChanged("Id"); }
        }

        public string LoadcaseName
        {
            get { return this.loadCasesmodel.LoadcaseName; }
            set { this.loadCasesmodel.LoadcaseName = value; OnPropertyChanged("LoadcaseName"); }
        }
        public double SelfWtMult
        {
            get { return this.loadCasesmodel.SelfWtMult; }
            set { this.loadCasesmodel.SelfWtMult = value; OnPropertyChanged("SelfWtMult"); }
        }
        public eLoadPatternType loadPatternType
        {
            get { return this.loadCasesmodel.loadPatternType; }
            set { this.loadCasesmodel.loadPatternType = value; OnPropertyChanged("loadPatternType"); }
        }


        
        #endregion
        //Events
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Methods
        #endregion


    }
}
