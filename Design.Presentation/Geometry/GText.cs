using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace Design.Presentation.Geometry
{
    class GText : GShape
    {
        public TextBlock TextBlock { get; set; }

        #region Text Properties
        public Brush ForegroundColor { get; set; }
        public Brush BackgroundColor { get; set; }
        public FontWeight Fontweight { get; set; }
        #endregion
        public string Text { get; set; }
        public Point InsertionPoint { get; set; }

        public double Height { get; set; }
        public GText(GCanvas gCanvas, Point insertionPoint,string text) : base(gCanvas)
        {
            TextBlock = new TextBlock();
            Text = text;
            // defaults;
            Height = 20;
            ForegroundColor = Brushes.Black;
            InsertionPoint = insertionPoint;
            BackgroundColor = Brushes.Transparent;
            TextBlock.RenderTransform = new TransformGroup();
            TextBlock.FontWeight = FontWeights.DemiBold;

            SetTranslate(InsertionPoint.X, InsertionPoint.Y);
        }

        #region Methods


        public override void Render()
        {

            TextBlock.Foreground = ForegroundColor;
            TextBlock.Background = BackgroundColor;
            TextBlock.Visibility = Visibility;
            TextBlock.Height = Height;
            TextBlock.Text = Text;

            // Canvas.SetLeft(TextBlock, 300);
            // Canvas.SetTop(TextBlock, 100);

            if (GCanvas.Canvas.Children.Contains(TextBlock))
            {
                return;
            }
            GCanvas.Canvas.Children.Add(TextBlock);

        }
        public override void New()
        {

        }

        public override void Remove()
        {
            if (!GCanvas.Canvas.Children.Contains(TextBlock)) return;
            GCanvas.Canvas.Children.Remove(TextBlock);
        }

        public override void SetScale(double value)
        {
            ((TransformGroup)TextBlock.RenderTransform).Children.Add(
                        new ScaleTransform(value, value));
        }

        public override void Hide()
        {
            TextBlock.Visibility = Visibility = Visibility.Collapsed;
        }
        public override void SetTranslate(double valueX, double valueY)
        {
            ((TransformGroup)TextBlock.RenderTransform).Children.Add(
                       new TranslateTransform(valueX, valueY));
        }
        public override void Rotate(double angle)
        {
            ((TransformGroup)TextBlock.RenderTransform).Children.Add(
                new RotateTransform(angle, InsertionPoint.X, InsertionPoint.Y));
        }
        #endregion
    }
}
