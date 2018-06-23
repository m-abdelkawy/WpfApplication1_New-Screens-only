using Design.Presentation.ViewModels;
using Desing.Core.Sap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WW.Cad.IO;
using WW.Cad.Model;
using WW.Cad.Model.Entities;
using WW.Cad.Model.Tables;
using WW.Math;

namespace Design.Core.Dxf
{
    public class DXFDimClass
    {
        #region Methods
        public static void DrawGridDims(DxfModel model, int nSpans, double[] comSpanVals)
        {
            //01. Grid Dims/*----------------*/
            DxfLayer gridspanLayer = new DxfLayer("DIMENSIONS");
            model.Layers.Add(gridspanLayer);

            DxfBlock blockGridSpan = new DxfBlock("ALIGNED_DIMENSIONS");
            model.Blocks.Add(blockGridSpan);

            DxfInsert insert = new DxfInsert(blockGridSpan, new Point3D(0, DXFPoints.endPointsBot[0].Y - 1, 0));
            insert.Layer = gridspanLayer;
            model.Entities.Add(insert);

            {
                DxfDimension.Aligned[] GridDimArr = new DxfDimension.Aligned[nSpans];

                for (int i = 0; i < GridDimArr.Length; i++)
                {
                    // Dimension with text aligned with dimension line.
                    DxfDimension.Aligned dimension = new DxfDimension.Aligned(model.CurrentDimensionStyle);
                    dimension.DimensionStyleOverrides.ArrowSize = 0.2d;
                    dimension.DimensionStyleOverrides.TextInsideHorizontal = false;
                    dimension.DimensionStyleOverrides.TextAboveDimensionLine = true;
                    dimension.Layer = gridspanLayer;
                    dimension.DimensionLineLocation = new Point3D(0, 0, 0);
                    dimension.ExtensionLine1StartPoint = new Point3D(comSpanVals[i], 0, 0);
                    dimension.ExtensionLine2StartPoint = new Point3D(comSpanVals[i + 1], 0, 0);
                    dimension.UseTextMiddlePoint = true;
                    dimension.TextMiddlePoint = new Point3D(comSpanVals[0] + 0.50 * (comSpanVals[i] + comSpanVals[i + 1]), 0.2d, 0d);
                    blockGridSpan.Entities.Add(dimension);
                }

            }
        }

        public static void DrawLnetDims(DxfModel model, int nSpans, double[] comSpanVals)
        {
            //02. Ln Dims/*----------------*/
            DxfLayer lnspanLayer = new DxfLayer("DIMENSIONSLn");
            model.Layers.Add(lnspanLayer);

            DxfBlock blockLnSpan = new DxfBlock("ALIGNED_DIMENSIONSLn");
            model.Blocks.Add(blockLnSpan);

            DxfInsert insertLn = new DxfInsert(blockLnSpan, new Point3D(0, DXFPoints.endPointsBot[0].Y - 0.50, 0));
            insertLn.Layer = lnspanLayer;
            model.Entities.Add(insertLn);

            {
                DxfDimension.Aligned[] LnDimArr = new DxfDimension.Aligned[nSpans];

                //Case of Cantilever at start
                if (GeometryEditorVM.GeometryEditor.RestraintsCollection[0].SelectedRestraint != Restraints.NoRestraints)
                {
                    // Dimension with text aligned with dimension line.
                    DxfDimension.Aligned dimension = new DxfDimension.Aligned(model.CurrentDimensionStyle);
                    dimension.DimensionStyleOverrides.ArrowSize = 0.2d;
                    dimension.DimensionStyleOverrides.TextInsideHorizontal = false;
                    dimension.DimensionStyleOverrides.TextAboveDimensionLine = true;
                    dimension.Layer = lnspanLayer;
                    dimension.DimensionLineLocation = new Point3D(0, 0, 0);
                    dimension.ExtensionLine1StartPoint = new Point3D(DXFPoints.startPointsBot[0].X, 0, 0);
                    dimension.ExtensionLine2StartPoint = new Point3D(DXFPoints.endPointsBot[0].X, 0, 0);
                    dimension.UseTextMiddlePoint = true;
                    dimension.TextMiddlePoint = new Point3D(comSpanVals[0] + 0.50 * (comSpanVals[0] + comSpanVals[0 + 1]), 0.2d, 0d);
                    blockLnSpan.Entities.Add(dimension);
                }

                for (int i = 1; i < LnDimArr.Length - 1; i++)
                {
                    // Dimension with text aligned with dimension line.
                    DxfDimension.Aligned dimension = new DxfDimension.Aligned(model.CurrentDimensionStyle);
                    dimension.DimensionStyleOverrides.ArrowSize = 0.2d;
                    dimension.DimensionStyleOverrides.TextInsideHorizontal = false;
                    dimension.DimensionStyleOverrides.TextAboveDimensionLine = true;
                    dimension.Layer = lnspanLayer;
                    dimension.DimensionLineLocation = new Point3D(0, 0, 0);
                    dimension.ExtensionLine1StartPoint = new Point3D(DXFPoints.startPointsBot[i].X, 0, 0);
                    dimension.ExtensionLine2StartPoint = new Point3D(DXFPoints.endPointsBot[i].X, 0, 0);
                    dimension.UseTextMiddlePoint = true;
                    dimension.TextMiddlePoint = new Point3D(comSpanVals[0] + 0.50 * (comSpanVals[i] + comSpanVals[i + 1]), 0.2d, 0d);
                    blockLnSpan.Entities.Add(dimension);
                }

                //case of cantilever at end
                if (GeometryEditorVM.GeometryEditor.RestraintsCollection[RFTCanvas.SpanVals.Length].SelectedRestraint != Restraints.NoRestraints)
                {
                    // Dimension with text aligned with dimension line.
                    DxfDimension.Aligned dimension = new DxfDimension.Aligned(model.CurrentDimensionStyle);
                    dimension.DimensionStyleOverrides.ArrowSize = 0.2d;
                    dimension.DimensionStyleOverrides.TextInsideHorizontal = false;
                    dimension.DimensionStyleOverrides.TextAboveDimensionLine = true;
                    dimension.Layer = lnspanLayer;
                    dimension.DimensionLineLocation = new Point3D(0, 0, 0);
                    dimension.ExtensionLine1StartPoint = new Point3D(DXFPoints.startPointsBot[LnDimArr.Length - 1].X, 0, 0);
                    dimension.ExtensionLine2StartPoint = new Point3D(DXFPoints.endPointsBot[LnDimArr.Length - 1].X, 0, 0);
                    dimension.UseTextMiddlePoint = true;
                    dimension.TextMiddlePoint = new Point3D(comSpanVals[0] + 0.50 * (comSpanVals[LnDimArr.Length - 1] + comSpanVals[LnDimArr.Length - 1 + 1]), 0.2d, 0d);
                    blockLnSpan.Entities.Add(dimension);
                }
            }
        }

        public static void DrawTopRFTRightDims(DxfModel model, int nSpans, double[] comSpanVals)
        {
            //03. TopRFT Right Dims/*----------------*/
            DxfLayer TopRFTDimLayerRt = new DxfLayer("DIMENSIONSTopRFTRt");
            model.Layers.Add(TopRFTDimLayerRt);

            DxfBlock blockTopRFTDimRt = new DxfBlock("ALIGNED_DIMENSIONSTopRFTRt");
            model.Blocks.Add(blockTopRFTDimRt);

            DxfInsert insertTopRFTDimRt = new DxfInsert(blockTopRFTDimRt, new Point3D(0, DXFPoints.startPointsTop[0].Y + 0.15, 0));
            insertTopRFTDimRt.Layer = TopRFTDimLayerRt;
            model.Entities.Add(insertTopRFTDimRt);

            {
                DxfDimension.Aligned[] TopRFTDimArrRt = new DxfDimension.Aligned[nSpans + 1];

                //Case Of Cantilever Start Span
                if (GeometryEditorVM.GeometryEditor.RestraintsCollection[0].SelectedRestraint != Restraints.NoRestraints)
                {
                    // Dimension with text aligned with dimension line.
                    DxfDimension.Aligned dimension = new DxfDimension.Aligned(model.CurrentDimensionStyle);
                    dimension.DimensionStyleOverrides.ArrowSize = 0.2d;
                    dimension.DimensionStyleOverrides.TextInsideHorizontal = false;
                    dimension.DimensionStyleOverrides.TextAboveDimensionLine = true;
                    dimension.Layer = TopRFTDimLayerRt;
                    dimension.DimensionLineLocation = new Point3D(0, 0, 0);
                    dimension.ExtensionLine1StartPoint = new Point3D(DXFPoints.startPointsTop[1].X, 0, 0);
                    dimension.ExtensionLine2StartPoint = new Point3D(DXFPoints.startPointsTop[1].X + Math.Max(0.33 * DXFRebar.Ln[1 - 1], 0.33 * DXFRebar.Ln[1]) + 0.25, 0, 0);
                    dimension.UseTextMiddlePoint = true;
                    dimension.TextMiddlePoint = new Point3D(comSpanVals[0] + 0.50 * (DXFPoints.startPointsTop[1].X + DXFPoints.startPointsTop[1].X + Math.Max(0.33 * DXFRebar.Ln[1 - 1], 0.33 * DXFRebar.Ln[1]) + 0.25), 0.2d, 0d);
                    blockTopRFTDimRt.Entities.Add(dimension);
                }

                for (int i = 2; i < TopRFTDimArrRt.Length - 2; i++)
                {
                    // Dimension with text aligned with dimension line.
                    DxfDimension.Aligned dimension = new DxfDimension.Aligned(model.CurrentDimensionStyle);
                    dimension.DimensionStyleOverrides.ArrowSize = 0.2d;
                    dimension.DimensionStyleOverrides.TextInsideHorizontal = false;
                    dimension.DimensionStyleOverrides.TextAboveDimensionLine = true;
                    dimension.Layer = TopRFTDimLayerRt;
                    dimension.DimensionLineLocation = new Point3D(0, 0, 0);
                    dimension.ExtensionLine1StartPoint = new Point3D(DXFPoints.startPointsTop[i].X, 0, 0);
                    dimension.ExtensionLine2StartPoint = new Point3D(DXFPoints.startPointsTop[i].X + Math.Max(0.33 * DXFRebar.Ln[i - 1], 0.33 * DXFRebar.Ln[i]) + 0.25, 0, 0);
                    dimension.UseTextMiddlePoint = true;
                    dimension.TextMiddlePoint = new Point3D(comSpanVals[0] + 0.50 * (DXFPoints.startPointsTop[i].X + DXFPoints.startPointsTop[i].X + Math.Max(0.33 * DXFRebar.Ln[i - 1], 0.33 * DXFRebar.Ln[i]) + 0.25), 0.2d, 0d);
                    blockTopRFTDimRt.Entities.Add(dimension);
                }
                //Case of Cantilever at end
                if (GeometryEditorVM.GeometryEditor.RestraintsCollection[RFTCanvas.SpanVals.Length].SelectedRestraint != Restraints.NoRestraints)
                {
                    // Dimension with text aligned with dimension line.
                    DxfDimension.Aligned dimension = new DxfDimension.Aligned(model.CurrentDimensionStyle);
                    dimension.DimensionStyleOverrides.ArrowSize = 0.2d;
                    dimension.DimensionStyleOverrides.TextInsideHorizontal = false;
                    dimension.DimensionStyleOverrides.TextAboveDimensionLine = true;
                    dimension.Layer = TopRFTDimLayerRt;
                    dimension.DimensionLineLocation = new Point3D(0, 0, 0);
                    dimension.ExtensionLine1StartPoint = new Point3D(DXFPoints.startPointsTop[TopRFTDimArrRt.Length - 1].X, 0, 0);
                    dimension.ExtensionLine2StartPoint = new Point3D(DXFPoints.startPointsTop[TopRFTDimArrRt.Length - 1].X + Math.Max(0.33 * DXFRebar.Ln[TopRFTDimArrRt.Length - 1 - 1], 0.33 * DXFRebar.Ln[TopRFTDimArrRt.Length - 1]) + 0.25, 0, 0);
                    dimension.UseTextMiddlePoint = true;
                    dimension.TextMiddlePoint = new Point3D(comSpanVals[0] + 0.50 * (DXFPoints.startPointsTop[TopRFTDimArrRt.Length - 1].X + DXFPoints.startPointsTop[TopRFTDimArrRt.Length - 1].X + Math.Max(0.33 * DXFRebar.Ln[TopRFTDimArrRt.Length - 1 - 1], 0.33 * DXFRebar.Ln[TopRFTDimArrRt.Length - 1]) + 0.25), 0.2d, 0d);
                    blockTopRFTDimRt.Entities.Add(dimension);
                }

            }
        }
            

        public static void DrawTopRFTLeftDims(DxfModel model, int nSpans, double[] comSpanVals)
        {
            //04.TopRFT Left Dims/*----------------*/
            DxfLayer TopRFTDimLayerLt = new DxfLayer("DIMENSIONSTopRFTLt");
            model.Layers.Add(TopRFTDimLayerLt);

            DxfBlock blockTopRFTDimLt = new DxfBlock("ALIGNED_DIMENSIONSTopRFTLt");
            model.Blocks.Add(blockTopRFTDimLt);

            DxfInsert insertTopRFTDimLt = new DxfInsert(blockTopRFTDimLt, new Point3D(0, DXFPoints.startPointsTop[0].Y + 0.15, 0));
            insertTopRFTDimLt.Layer = TopRFTDimLayerLt;
            model.Entities.Add(insertTopRFTDimLt);

            {
                DxfDimension.Aligned[] TopRFTDimArrLt = new DxfDimension.Aligned[nSpans + 1];

                for (int i = 1; i < TopRFTDimArrLt.Length - 2; i++)
                {
                    // Dimension with text aligned with dimension line.
                    DxfDimension.Aligned dimension = new DxfDimension.Aligned(model.CurrentDimensionStyle);
                    dimension.DimensionStyleOverrides.ArrowSize = 0.2d;
                    dimension.DimensionStyleOverrides.TextInsideHorizontal = false;
                    dimension.DimensionStyleOverrides.TextAboveDimensionLine = true;
                    dimension.Layer = TopRFTDimLayerLt;
                    dimension.DimensionLineLocation = new Point3D(0, 0, 0);
                    dimension.ExtensionLine1StartPoint = new Point3D(DXFPoints.endPointsTop[i - 1].X - Math.Max(0.33 * DXFRebar.Ln[i - 1], 0.33 * DXFRebar.Ln[i]) - 0.25, 0, 0);
                    dimension.ExtensionLine2StartPoint = new Point3D(DXFPoints.endPointsTop[i - 1].X, 0, 0);
                    dimension.UseTextMiddlePoint = true;
                    dimension.TextMiddlePoint = new Point3D(comSpanVals[0] + 0.50 * (DXFPoints.endPointsTop[i - 1].X - Math.Max(0.33 * DXFRebar.Ln[i - 1], 0.33 * DXFRebar.Ln[i]) - 0.25 + DXFPoints.endPointsTop[i - 1].X), 0.2d, 0d);
                    blockTopRFTDimLt.Entities.Add(dimension);
                }
                //Case of Cantilever at end
                if (GeometryEditorVM.GeometryEditor.RestraintsCollection[RFTCanvas.SpanVals.Length].SelectedRestraint != Restraints.NoRestraints)
                {
                    // Dimension with text aligned with dimension line.
                    DxfDimension.Aligned dimension = new DxfDimension.Aligned(model.CurrentDimensionStyle);
                    dimension.DimensionStyleOverrides.ArrowSize = 0.2d;
                    dimension.DimensionStyleOverrides.TextInsideHorizontal = false;
                    dimension.DimensionStyleOverrides.TextAboveDimensionLine = true;
                    dimension.Layer = TopRFTDimLayerLt;
                    dimension.DimensionLineLocation = new Point3D(0, 0, 0);
                    dimension.ExtensionLine1StartPoint = new Point3D(DXFPoints.endPointsTop[TopRFTDimArrLt.Length - 1 - 1].X - Math.Max(0.33 * DXFRebar.Ln[TopRFTDimArrLt.Length - 1 - 1], 0.33 * DXFRebar.Ln[TopRFTDimArrLt.Length - 1]) - 0.25, 0, 0);
                    dimension.ExtensionLine2StartPoint = new Point3D(DXFPoints.endPointsTop[TopRFTDimArrLt.Length - 1 - 1].X, 0, 0);
                    dimension.UseTextMiddlePoint = true;
                    dimension.TextMiddlePoint = new Point3D(comSpanVals[0] + 0.50 * (DXFPoints.endPointsTop[TopRFTDimArrLt.Length - 1 - 1].X - Math.Max(0.33 * DXFRebar.Ln[TopRFTDimArrLt.Length - 1 - 1], 0.33 * DXFRebar.Ln[TopRFTDimArrLt.Length - 1]) - 0.25 + DXFPoints.endPointsTop[TopRFTDimArrLt.Length - 1 - 1].X), 0.2d, 0d);
                    blockTopRFTDimLt.Entities.Add(dimension);
                }
            }
        }
        #endregion
    }
}
