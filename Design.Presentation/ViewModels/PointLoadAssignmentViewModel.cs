using Design.Presentation.Model;
using Desing.Core.Sap;
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
    public class PointLoadAssignmentViewModel : INotifyPropertyChanged
    {
        PointLoadAssignmentModel pointLoadModel;



        #region Properties
        public PointLoadAssignmentModel PointLoadModel
        {
            get { return pointLoadModel; }
            set { pointLoadModel = value; }
        }

        public static ObservableCollection<PointLoadAssignmentModel> PointLoadModelStaticCollection { get; set; }
        = new ObservableCollection<PointLoadAssignmentModel>();

        public ObservableCollection<PointLoadAssignmentModel> PointLoadCollection { get; set; }
        #endregion

        #region Constructors
        public PointLoadAssignmentViewModel()
        {
            PointLoadCollection = PointLoadAssignmentViewModel.PointLoadModelStaticCollection;
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
