using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Design.Presentation.Geometry
{
    public class GPolygon : GShape
    {
        public Polygon Polygon { get; set; }
        public PointCollection Points { get; set; }
        public Point Position { get; }
        public Point Origin { get; set; }
        public GPolygon(GCanvas gCanvas, PointCollection points,[Optional] Point Position,[Optional] Point origin):base(gCanvas)
        {
            ///Set shape properties
            
            Points = points;
            this.Position = Position;
            
            Shape = Polygon = new Polygon();

            Polygon.Points = points;
            Shape.Stroke = Stroke;
            Shape.Fill = Fill;
            Shape.Visibility = Visibility;
            Shape.StrokeThickness = StrokeThickness;

            if (origin == null)
            {
                origin = Points.Last();
            }
            else
            {
                Origin = origin;
            }
            //Set polygon Properties
            if (Position!=null)
            {
                var left = Position.X - Origin.X;
                var top = Position.Y - Origin.Y;
                Polygon.Margin = new Thickness(left, top, 0, 0);
            }
            
            Polygon.HorizontalAlignment = HorizontalAlignment.Left;
            Polygon.VerticalAlignment = VerticalAlignment.Center;
            GCanvas.Shapes.Add(this);
            
        }
    }
}
