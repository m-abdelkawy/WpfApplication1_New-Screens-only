using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Design.Presentation.Geometry
{
    public class GeometryEngine
    {
        public GCanvas GCanvas { get; set; }

        public Dictionary<string, List<GShape>> Shapes { get; set; }
        public GeometryEngine()
        {
            Shapes = new Dictionary<string, List<GShape>>()
            {
                {"Supports",new List<GShape>()},
                {"DistributedLoad",new List<GShape>()},
                {"ConcentratedLoad",new List<GShape>()},
                {"Beams",new List<GShape>()},
                {"Grid",new List<GShape>()},
                {"Points",new List<GShape>()},
            };

            GCanvas = new GCanvas();
        }

        public void Render(string listName)
        {
            var gShapes = Shapes[listName];
            //
            Render(gShapes);
        }
        public void Render(List<GShape> gShapes)
        {
            gShapes.ForEach(e => e.Render());
        }
        public void RenderAll()
        {
            GCanvas.Shapes.ForEach(e => e.Render());
        }
        public void RemoveAll()
        {
            GCanvas.Shapes.ForEach(e => e.Remove());
            Shapes.Clear();

        }
        public void Remove(string listName)
        {
            //get shapes list based on string key and remove them from th Gcanvas
            Remove(Shapes[listName]);
        }
        public void Remove(List<GShape> shapes)
        {
            shapes.ForEach(e => e.Remove());
            foreach (var shape in shapes)
            {
                GCanvas.Shapes.Remove(shape);
            }
        }
        //Shapes.ForEach(e => e.Remove());
        public void HideAll()
        {
            GCanvas.Shapes.ForEach(e => e.Hide());
        }
        public void Hide(string listName)
        {
            Hide(Shapes[listName]);
        }
        public void Hide(List<GShape> gShapes)
        {
            gShapes.ForEach(e => e.Hide());
        }

        #region Helper Functions

        #endregion
    }
}
