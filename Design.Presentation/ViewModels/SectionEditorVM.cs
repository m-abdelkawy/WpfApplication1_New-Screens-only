using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design.Presentation.ViewModels
{
    public class SectionEditorVM
    {
        public static ObservableCollection<SectionEditorVM> Sections { get; set; }
        = new ObservableCollection<SectionEditorVM>();

        public string Name { get; set; }
        public double Depth { get; set; }
        public double Width { get; set; }
        public SectionEditorVM()
        {
           
        }
    }
}
