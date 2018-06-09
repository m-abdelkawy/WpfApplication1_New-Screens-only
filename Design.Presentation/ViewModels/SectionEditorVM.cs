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
        public string Name { get; set; }
        public double Depth { get; set; }
        public double Width { get; set; }
        public ObservableCollection<MaterialEditorVM> Materials { get; set; }
        public MaterialEditorVM SelectedMaterial { get; set; }

        public SectionEditorVM(ObservableCollection<MaterialEditorVM> materials)
        {
            Materials = materials;
        }
    }
}
