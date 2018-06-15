using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Design.Presentation.Geometry
{
    public class Hinged : GShape
    {
        public GTriangle Triangle { get; set; }
        public GLine Line { get; set; }

        public Point InsertionPoint { get; set; }
        public Hinged(GCanvas gCanvas, Point insertionPoint) : base(gCanvas)
        {
            InsertionPoint = insertionPoint;
            Triangle = new GTriangle(GCanvas, InsertionPoint, 20);
            Line = new GLine(GCanvas, InsertionPoint, new Point(InsertionPoint.X, InsertionPoint.Y + 20));
        }
        public override void Remove()
        {
            //base.Remove();
            GCanvas.Canvas.Children.Remove(Line.Line);
            GCanvas.Canvas.Children.Remove(Triangle.Polygon);
            GCanvas.Shapes.Remove(Triangle);
            GCanvas.Shapes.Remove(Line);

        }
        public override void Hide()
        {
            Line.Hide();
            Triangle.Hide();
        }

        public override void Render()
        {
            Line.Render();
            Triangle.Render();
        }
    }
}
