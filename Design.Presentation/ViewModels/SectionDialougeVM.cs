using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design.Presentation.ViewModels
{
    public class SectionDialougeVM
    {
      public  ObservableCollection<SectionEditorVM> Sections { get; set; }
        public SectionDialougeVM(ObservableCollection<SectionEditorVM> sections)
        {
            Sections = sections;
        }
    }
}
