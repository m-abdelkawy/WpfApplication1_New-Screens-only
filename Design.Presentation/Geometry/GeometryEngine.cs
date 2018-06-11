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
        
        public Dictionary<string,List<GShape>> Shapes { get; set; }
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
        public void AddShape(ShapesType shapes)
        {
            switch (shapes)
            {
                case ShapesType.Triangle:
                    DrawTriangle();
                    break;
                case ShapesType.Rectangle:
                    DrawRectangle();
                    break;
                case ShapesType.Circle:
                    DrawCircle();
                    break;
                case ShapesType.Arrow:
                    DrawArrow();
                    break;
                case ShapesType.Roller:
                    DrawRoller();
                    break;
                case ShapesType.Hinge:
                    DrawHinge();
                    break;
                case ShapesType.Fixed:
                    DrawFixed();
                    break;
                case ShapesType.Point:
                    DrawPoint();
                    break;
                case ShapesType.Cross:
                    DrawCross();
                    break;
                case ShapesType.ArrowLoad:
                    DrawArrowLoad();
                    break;
                default:
                    break;
            }
        }

        #region Helper Functions
        private void DrawTriangle()
        {

        }
        private void DrawRectangle()
        {


        }
        private void DrawRoller()
        {

        }
        private void DrawCircle()
        {

        }
        private void DrawArrow()
        {

        }
        private void DrawHinge()
        {

        }

        private void DrawFixed()
        {

        }
        private void DrawPoint()
        {

        }
        private void DrawCross()
        {

        }
        private void DrawArrowLoad()
        {

        }
        #endregion
    }
}
