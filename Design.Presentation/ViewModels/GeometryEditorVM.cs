using Design.Presentation.Model;
using Desing.Core.Sap;
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
        private int _numberOfSpans = 1;
        private int _spanLength = 5;

        public static GeometryEditorVM GeometryEditor { get; set; }
= new GeometryEditorVM();
        public int NumberOfSpans
        {
            get => _numberOfSpans;
            set
            {
                _numberOfSpans = value;
                UpdateGeometry();
            }
        }
        public int SpanLength
        {
            get => _spanLength;
            set
            {
                _spanLength = value;
                UpdateGeometry();
            }
        }
        public ObservableCollection<SectionEditorVM> Sections { get;
            set; }
        public ObservableCollection<GridData> GridData { get; set; }
        public SectionEditorVM SelectedSection { get; set; }
        public GeometryEditorVM()
        {
            Sections = SectionEditorVM.Sections;
            GridData = new ObservableCollection<GridData>();
        }

        private void UpdateGeometry()
        {
            GridData.Clear();
            for (int i = 0; i < NumberOfSpans; i++)
            {
                GridData.Add(new GridData()
                {
                    Id = i,
                    SectionProperties = SectionEditorVM.Sections,
                    Span = SpanLength,
                    Restrain = Restraints.Fixed,
                    SelectedSection = SelectedSection
                });
            }
        }
    }
}
