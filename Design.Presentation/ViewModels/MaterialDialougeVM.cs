using Design.Presentation.Views.Material;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design.Presentation.ViewModels
{
    public class MaterialDialougeVM
    {
        public ObservableCollection<MaterialEditorVM> Materials { get; set; }
    }
}
