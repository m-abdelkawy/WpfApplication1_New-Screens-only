using Design.Presentation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Design.Presentation.ViewModels
{
    public class GeometryEditorVM
    {
        public int NumberOfSpans { get; set; } = 1;
        public int SpanLength { get; set; } = 5;
        public ObservableCollection<GridData> GridData { get; set; }

        public GeometryEditorVM()
        {
            // if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;
            GridData = new ObservableCollection<GridData>()
            {
                new GridData{Id=1,Span=10},
                new GridData{Id=2,Span=10},
                new GridData{Id=3,Span=10},
            };
        }
    }
}
