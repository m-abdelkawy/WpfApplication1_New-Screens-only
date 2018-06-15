﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Design.Presentation.Geometry
{
    public class GTriangle : GPolygon
    {
        public Point Top { get; set; }
        public Point Right { get; set; }
        public Point Left { get; set; }
        public double SideLength { get; set; }
        public GTriangle(GCanvas gCanvas, Point top, double sideLength) : base(gCanvas)
        {
            Top = top;
            SideLength = sideLength*Scale;
            Right = new Point(top.X + SideLength, top.Y + SideLength);
            Left = new Point(top.X - SideLength, top.Y+SideLength );
            if (Points ==null)
            {
                Points = new PointCollection();
            }
            //add point to the polygon Points Collection
            Points.Add(Top);
            Points.Add(Right);
            Points.Add(Left);

            Fill = Brushes.Transparent;
            Stroke = Brushes.Green;
            StrokeThickness = 2;
        }
        public override void Hide()
        {
            base.Hide();
        }

        public override void Remove()
        {
            base.Remove();
        }

        public override void Render()
        {
            base.Render();
        }
    }
}
