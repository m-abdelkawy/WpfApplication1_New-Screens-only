using Design.Presentation.ViewModels;
using Desing.Core.Sap;
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
    public class DistLoadAssignmentModel : INotifyPropertyChanged
    {
        #region Properties
        public int Id { get; set; }
        public double DistLoadVal { get; set; } = 0;
        public ObservableCollection<int> SpanNo
        {
            get { return new ObservableCollection<int>(GeometryEditorVM.GeometryEditor.GridData.Select(e => e.Id)); } 
            set { SpanNo = value; }
        }
        public ObservableCollection<LoadCasesModel> LoadCases { get; set; }
        public LoadDir DistLoadDirection { get; set; }


        //Selected Items
        public string selectedLoadCase { get; set; } //to set the selected value in the textBlock
        public int selectedSpanNo;
        public LoadDir selectedDir { get; set; }
        #endregion

        #region Constructors
        public DistLoadAssignmentModel()
        {
            this.LoadCases = LoadCasesViewModel.LoadCasesModelCollection;

        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
