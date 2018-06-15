using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Design.Presentation.Geometry
{
    public class Fixed : GShape
    {
        public Point InsertionPoint { get; set; }
        public GRectangle Rectangle { get; set; }
        public double Height { get; set; }
        public GLine Line { get; set; }
        public Fixed(GCanvas gCanvas, Point InsertionPoint) : base(gCanvas)
        {
            Height = 15;
            Rectangle = new GRectangle(GCanvas, 30, 15,
             new Point(InsertionPoint.X * 20/*scale*/, InsertionPoint.Y + 15));
            
            Line = new GLine(GCanvas, new Point(InsertionPoint.X * 20/*scale*/, InsertionPoint.Y), new Point(InsertionPoint.X * 20/*scale*/, InsertionPoint.Y + Height+Height/2));
        }
    }
}
