using Design.Presentation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design.Presentation.ViewModels
{
    public class DistLoadAssignmentViewModel : INotifyPropertyChanged
    {
        DistLoadAssignmentModel distLoadModel;
        #region Properties
        public DistLoadAssignmentModel DistLoadModel
        {
            get { return distLoadModel; }
            set { distLoadModel = value; }
        }
        public static ObservableCollection<DistLoadAssignmentModel> DistLoadModelStaticCollection { get; set; }
        = new ObservableCollection<DistLoadAssignmentModel>();

        public ObservableCollection<DistLoadAssignmentModel> DistLoadCollection { get; set; }


        #endregion

        #region Constructors
        public DistLoadAssignmentViewModel()
        {
            //this.distLoadModel = new DistLoadAssignmentModel();
            DistLoadCollection = DistLoadAssignmentViewModel.DistLoadModelStaticCollection;
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
