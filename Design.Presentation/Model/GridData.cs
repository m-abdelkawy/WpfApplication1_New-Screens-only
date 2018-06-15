using Design.Presentation.Views.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desing.Core.Sap;
using System.ComponentModel;
using Design.Presentation.ViewModels;
using System.Collections.ObjectModel;

namespace Design.Presentation.Model
{
    public class GridData
    {

        public int Id { get; set; }
        public double Span { get; set; }
        public ObservableCollection<SectionEditorVM> SectionProperties { get; set; }
        public SectionEditorVM SelectedSection { get; set; }
        //public Restraints SelectedRestrain { get; set; }
        //public Restraints Restrain { get; set; }
    }
}
