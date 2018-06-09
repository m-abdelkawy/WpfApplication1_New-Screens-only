using Design.Presentation.Views.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desing.Core.Sap;
using System.ComponentModel;

namespace Design.Presentation.Model
{
    public class GridData
    {
        [DisplayName("Grid Id")]
        public int Id { get; set; }
        public double Span { get; set; }
        [DisplayName("Section Properties")]
        public SectionEditor SectionProperties { get; set; }
        [DisplayName("Start Restrain")]
        public Restraints StartRestraing { get; set; }
        [DisplayName("End Restrain")]
        public Restraints StartRestrains { get; set; }
    }
}
