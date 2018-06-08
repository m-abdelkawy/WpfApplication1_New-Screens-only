using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Design.Presentation.ViewModels
{
    public class GeometryEditorVM
    {
        public int NumberOfSpans
        {
            get;
            set;
        } = 1;
        public int SpanLength { get; set; } = 5;

        public GeometryEditorVM()
        {
            // if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;

        }
    }
}
