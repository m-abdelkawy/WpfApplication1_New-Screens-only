using Design.Presentation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Design.Presentation.Geometry
{
    public class ArrowLoad:GShape
    {
        public List<Arrow> Arrows { get; set; }
        public GLine HeaderLine { get; set; }

        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public double Height { get; set; }
        public double Spacings { get; set; }
        public ArrowLoad(GCanvas gCanvas,Point startPoint,Point endPoint, double loadValue):base(gCanvas)
        {
            GCanvas = gCanvas;
            StartPoint = startPoint;
            EndPoint = endPoint;
            Arrows = new List<Arrow>();
            Height = loadValue /*Scale*/;
            var span = Math.Sqrt(
                Math.Pow(startPoint.X - endPoint.X, 2) +
                Math.Pow(startPoint.Y - endPoint.Y, 2));
            Spacings = 10;
            var spaces = Split.Equal(span, Spacings);
            foreach (var space in spaces)
            {
                var arrow = new Arrow(GCanvas, new Point(StartPoint.X+space, StartPoint.Y), Height);
                arrow.Rotate(180);
                Arrows.Add(arrow);
            }

            HeaderLine = new GLine(GCanvas, 
                new Point(StartPoint.X, StartPoint.Y - Height),
                new Point(EndPoint.X, EndPoint.Y - Height));
            
        }

        public override void Remove()
        {
            GCanvas.Canvas.Children.Remove(HeaderLine.Line);
            Arrows.ForEach(e => e.Remove());
            Arrows.Clear();
        }
        
    }
}
