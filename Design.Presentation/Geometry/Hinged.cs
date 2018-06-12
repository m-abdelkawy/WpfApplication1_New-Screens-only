using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Design.Presentation.Geometry
{
    public class Hinged:GShape
    {
        public GTriangle Triangle { get; set; }
        public GLine Line { get; set; }
        
        public Point InsertionPoint { get; set; }
        public Hinged(GCanvas gCanvas,Point insertionPoint):base(gCanvas)
        {
            InsertionPoint = insertionPoint;
            GTriangle gTriangle = new GTriangle(GCanvas, InsertionPoint, 20);
            GLine gline = new GLine(GCanvas, InsertionPoint,new Point(InsertionPoint.X,InsertionPoint.Y+20));
        }
    }
}
