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
        private ObservableCollection<SectionEditorVM> _sections;
        private SectionEditorVM _selectedSection;

        public static GeometryEditorVM GeometryEditor { get; set; }
= new GeometryEditorVM(); //List of grid data
        public int NumberOfSpans
        {
            get { return _numberOfSpans; }
            set
            {
                _numberOfSpans = value;
                UpdateGeometry();
            }
        }
        public int SpanLength
        {
            get { return _spanLength; }
            set
            {
                _spanLength = value;
                UpdateGeometry();
            }
        }
        public ObservableCollection<SectionEditorVM> Sections
        {
            get { return _sections; }
            set { _sections = value; try { UpdateGeometry(); } catch { } }
        }
        public ObservableCollection<GridData> GridData { get; set; } //span, sec of 
        public SectionEditorVM SelectedSection
        {
            get
            {
                if (_selectedSection == null)
                {
                    _selectedSection = Sections.FirstOrDefault();
                };
                return _selectedSection;
            }
            set
            {
                _selectedSection = value;

            }
        }
        public ObservableCollection<RestraintsModel> RestraintsCollection { get; set; }//span, sec of 
        public GeometryEditorVM()
        {
            Sections = SectionEditorVM.Sections;
            GridData = new ObservableCollection<GridData>();
            RestraintsCollection = new ObservableCollection<RestraintsModel>();
        }
        private void UpdateGeometry()
        {
            GridData.Clear();
            RestraintsCollection.Clear();
            for (int i = 0; i < NumberOfSpans + 1; i++)
            {
                RestraintsCollection.Add(new RestraintsModel()
                {
                    Id = i + 1,
                    SelectedRestraint=Restraints.Fixed,
                });
            }
            for (int i = 0; i < NumberOfSpans; i++)
            {
                GridData.Add(new GridData()
                {
                    Id = i + 1,
                    SectionProperties = SectionEditorVM.Sections,
                    Span = SpanLength,
                    //Restrain = Restraints.Fixed,
                    SelectedSection = GeometryEditor.SelectedSection
                });

                

            }


        }

    }
}
