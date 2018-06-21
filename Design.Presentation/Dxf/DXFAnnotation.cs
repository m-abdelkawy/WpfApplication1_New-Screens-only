﻿using Design.Presentation.ViewModels;
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
    public class DXFAnnotation
    {

        #region Methods
        public static void AnnotationStirLeft(DxfModel model, int nSpans)
        {
            DxfLeader[] LeaderLt = new DxfLeader[nSpans];

            //Case of Cantilever at start

            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[0].SelectedRestraint != Restraints.NoRestraints)
            {
                LeaderLt[0] = new DxfLeader(model);
                LeaderLt[0].ArrowHeadEnabled = true;
                LeaderLt[0].Vertices.AddRange(
                 new Point3D[] {
                     new Point3D(DXFRebar.stirrupsLeft[0, 2].Start.X, (DXFPoints.startPointsTop[0].Y + DXFPoints.startPointsBot[0].Y)/2, 0),
                     new Point3D(DXFRebar.stirrupsLeft[0, 2].Start.X + 0.25, (DXFPoints.startPointsTop[0].Y + DXFPoints.startPointsBot[0].Y)/2, 0),
                     new Point3D(DXFRebar.stirrupsLeft[0, 2].Start.X + 0.25, DXFPoints.startPointsBot[0].Y - 1.50, 0),
                     new Point3D(DXFRebar.stirrupsLeft[0, 2].Start.X - 1, DXFPoints.startPointsBot[0].Y - 1.50, 0)
                 }
            );
                model.Entities.AddRange(LeaderLt[0]);
            }
            

            for (int i = 1; i < LeaderLt.Length; i++)
            {
                LeaderLt[i] = new DxfLeader(model);
                LeaderLt[i].ArrowHeadEnabled = true;
                LeaderLt[i].Vertices.AddRange(
                 new Point3D[] {
                     new Point3D(DXFRebar.stirrupsLeft[i, 2].Start.X, (DXFPoints.startPointsTop[i].Y + DXFPoints.startPointsBot[i].Y)/2, 0),
                     new Point3D(DXFRebar.stirrupsLeft[i, 2].Start.X + 0.25, (DXFPoints.startPointsTop[i].Y + DXFPoints.startPointsBot[i].Y)/2, 0),
                     new Point3D(DXFRebar.stirrupsLeft[i, 2].Start.X + 0.25, DXFPoints.startPointsBot[i].Y - 1.50, 0),
                     new Point3D(DXFRebar.stirrupsLeft[i, 2].Start.X - 1, DXFPoints.startPointsBot[i].Y - 1.50, 0)
                 }
            );
                model.Entities.AddRange(LeaderLt[i]);
            }
        }

        //public static void AnnotationStirMidSpan(DxfModel model, int nSpans, double thickness, DxfLine[,] stirrupsMidspan)
        //{
        //    DxfLeader[] LeaderMidSpan = new DxfLeader[nSpans];
        //    for (int i = 0; i < LeaderMidSpan.Length; i++)
        //    {
        //        LeaderMidSpan[i] = new DxfLeader(model);
        //        LeaderMidSpan[i].ArrowHeadEnabled = true;
        //        LeaderMidSpan[i].Vertices.AddRange(
        //         new Point3D[] {
        //             new Point3D(stirrupsMidspan[i, 2].Start.X, thickness/2, 0),
        //             new Point3D(stirrupsMidspan[i, 2].Start.X + 0.25, thickness/2, 0),
        //             new Point3D(stirrupsMidspan[i, 2].Start.X + 0.25, -1.50, 0),
        //             new Point3D(stirrupsMidspan[i, 2].Start.X - 1, -1.50, 0)
        //         }
        //    );
        //        model.Entities.AddRange(LeaderMidSpan[i]);
        //    }
        //}

        public static void AnnotationStirRight(DxfModel model, int nSpans)
        {
            DxfLeader[] LeaderRt = new DxfLeader[nSpans];
            for (int i = 0; i < LeaderRt.Length - 1; i++)
            {
                LeaderRt[i] = new DxfLeader(model);
                LeaderRt[i].ArrowHeadEnabled = true;
                LeaderRt[i].Vertices.AddRange(
                 new Point3D[] {
                     new Point3D(DXFRebar.stirrupsRight[i, 2].Start.X, (DXFPoints.startPointsTop[i].Y + DXFPoints.startPointsBot[i].Y)/2, 0),
                     new Point3D(DXFRebar.stirrupsRight[i, 2].Start.X - 0.50, (DXFPoints.startPointsTop[i].Y + DXFPoints.startPointsBot[i].Y)/2, 0),
                     new Point3D(DXFRebar.stirrupsRight[i, 2].Start.X - 0.50, DXFPoints.startPointsBot[i].Y - 1.50, 0),
                     new Point3D(DXFRebar.stirrupsRight[i, 2].Start.X - 1.50, DXFPoints.startPointsBot[i].Y - 1.50, 0)
                 }
            );
                model.Entities.AddRange(LeaderRt[i]);
            }

            //case of cantilever at end
            if (GeometryEditorVM.GeometryEditor.RestraintsCollection[RFTCanvas.SpanVals.Length].SelectedRestraint != Restraints.NoRestraints)
            {
                LeaderRt[LeaderRt.Length - 1] = new DxfLeader(model);
                LeaderRt[LeaderRt.Length - 1].ArrowHeadEnabled = true;
                LeaderRt[LeaderRt.Length - 1].Vertices.AddRange(
                 new Point3D[] {
                     new Point3D(DXFRebar.stirrupsRight[LeaderRt.Length - 1, 2].Start.X, (DXFPoints.startPointsTop[LeaderRt.Length - 1].Y + DXFPoints.startPointsBot[LeaderRt.Length - 1].Y)/2, 0),
                     new Point3D(DXFRebar.stirrupsRight[LeaderRt.Length - 1, 2].Start.X - 0.50, (DXFPoints.startPointsTop[LeaderRt.Length - 1].Y + DXFPoints.startPointsBot[LeaderRt.Length - 1].Y)/2, 0),
                     new Point3D(DXFRebar.stirrupsRight[LeaderRt.Length - 1, 2].Start.X - 0.50, DXFPoints.startPointsBot[LeaderRt.Length - 1].Y - 1.50, 0),
                     new Point3D(DXFRebar.stirrupsRight[LeaderRt.Length - 1, 2].Start.X - 1.50, DXFPoints.startPointsBot[LeaderRt.Length - 1].Y - 1.50, 0)
                 }
            );
                model.Entities.AddRange(LeaderRt[LeaderRt.Length - 1]);
            }
        }
        #endregion
    }
}
