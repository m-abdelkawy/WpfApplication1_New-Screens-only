using Design.Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design.Presentation.ViewModels
{
    public enum MaterialType
    {
        Concrete,
        Steel
    }
    public class MaterialEditorVM
    {
        private MaterialType _materialType;

        public  static int Counter=1;
        
        public string Name { get; set; }
        public MaterialType MaterialType
        {
            get
            {
                return _materialType;
            }
            set
            {
                _materialType = value;
                SetMaterialProperty();
            }
        }
        public double WeightPerUnitVolume { get; set; }
        public double MassPerUnitVolume { get; set; }
        public double E { get; set; }
        public double U { get; set; }
        public double A { get; set; }
        public double G { get; }
        public MaterialProperty MaterialProperty { get; set; }
        public MaterialEditorVM()
        {
            
            WeightPerUnitVolume = 11111;
            MaterialType = MaterialType.Concrete;
            SetMaterialProperty();
            Name = "Concrete- " + Counter;
            Counter++;

        }

        public override string ToString()
        {
            return Name;
        }
        private void SetMaterialProperty()
        {
            switch (MaterialType)
            {
                case MaterialType.Concrete:
                    MaterialProperty = new ConcreteMaterialProperties();
                    break;
                case MaterialType.Steel:
                    MaterialProperty = new SteelMaterialProperties();
                    break;

            }
        }

    }
}
