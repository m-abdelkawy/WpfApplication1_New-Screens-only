using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Design.Presentation.Geometry
{
    public sealed class Arrow : GShape
    {
        public double Length { get; set; }
        public double HeadHeight { get; set; }
        public double HeadAngle { get; set; }

        public List<GLine> Lines { get; set; }
        public Point InsertionPoint { get; set; }
        public Direction Direction { get; set; }
        public Arrow(GCanvas gCanvas, Point insertionPoint, double length) :base(gCanvas)
        {
            GCanvas = gCanvas;
            InsertionPoint = insertionPoint;
            Length = length;
            HeadHeight = 5;
            var body = new GLine(GCanvas,InsertionPoint,new Point(InsertionPoint.X,InsertionPoint.Y+Length));
           
            var headRightLine = new GLine(GCanvas, InsertionPoint, new Point(InsertionPoint.X+HeadHeight, InsertionPoint.Y + HeadHeight));
            var headLeftLine = new GLine(GCanvas, InsertionPoint, new Point(InsertionPoint.X - HeadHeight, InsertionPoint.Y + HeadHeight));

            Lines = new List<GLine>()
            {
                body,
                headRightLine,
                headLeftLine
            };
        }
        public void Rotate(double angle)
        {
            foreach (var line in Lines)
            {
                line.Line.RenderTransform = new RotateTransform(angle,InsertionPoint.X,InsertionPoint.Y);
            }
        }
        public override void Remove()
        {
            Lines.ForEach(e => e.Remove());
            Lines.Clear();
        }

        /// <summary>
        /// Draws an Arrow
        /// </summary>

        //private void InternalDrawArrowGeometry(StreamGeometryContext context)
        //{
        //    double theta = Math.Atan2(Y1 - Y2, X1 - X2);
        //    double sint = Math.Sin(theta);
        //    double cost = Math.Cos(theta);

        //    Point pt1 = new Point(X1, this.Y1);
        //    Point pt2 = new Point(X2, this.Y2);

        //    Point pt3 = new Point(
        //        X2 + (HeadWidth * cost - HeadHeight * sint),
        //        Y2 + (HeadWidth * sint + HeadHeight * cost));

        //    Point pt4 = new Point(
        //        X2 + (HeadWidth * cost + HeadHeight * sint),
        //        Y2 - (HeadHeight * cost - HeadWidth * sint));

        //    context.BeginFigure(pt1, true, false);
        //    context.LineTo(pt2, true, true);
        //    context.LineTo(pt3, true, true);
        //    context.LineTo(pt2, true, true);
        //    context.LineTo(pt4, true, true);
        //}
    }

}
