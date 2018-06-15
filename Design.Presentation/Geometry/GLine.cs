using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Design.Presentation.Geometry
{
    public class GLine : GShape
    {
        public Line Line { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public GLine(GCanvas gCanvas, Point startPoint, Point endPoint):base(gCanvas)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
           
            Shape = Line = new Line();
            Line.X1 = StartPoint.X /*Scale*/;
            Line.Y1 = StartPoint.Y;
            //
            Line.X2 = EndPoint.X /*Scale*/;
            Line.Y2 = EndPoint.Y;
            //Add this Shape to Canvas
            GCanvas.Shapes.Add(this);
        }
        
    }
}
