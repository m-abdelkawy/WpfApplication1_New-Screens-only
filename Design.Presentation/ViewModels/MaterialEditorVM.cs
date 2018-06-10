using Design.Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design.Presentation.ViewModels
{
   
    public class MaterialEditorVM
    {
        
        public  static int Counter=1;
        public static MaterialEditorVM Material { get; set; } = new MaterialEditorVM();
        public double Fcu { get; set; }
        public double Fy { get; set; }
        public double NoOfBranches { get; set; }
        public double FyStirrups { get; set; }
       
        public MaterialEditorVM()
        {
            
        }
    }
}
